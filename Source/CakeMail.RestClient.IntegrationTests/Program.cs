using CakeMail.RestClient.Logging;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.IntegrationTests
{
	public class Program
	{
		static async Task<int> Main()
		{
			// -----------------------------------------------------------------------------

			// Do you want to proxy requests through Fiddler (useful for debugging)?
			var useFiddler = false;

			// As an alternative to Fiddler, you can display debug information about
			// every HTTP request/response in the console. This is useful for debugging
			// purposes but the amount of information can be overwhelming.
			var debugHttpMessagesToConsole = false;
			// -----------------------------------------------------------------------------

			var proxy = useFiddler ? new WebProxy("http://localhost:8888") : null;
			var apiKey = Environment.GetEnvironmentVariable("CAKEMAIL_APIKEY");
			var userName = Environment.GetEnvironmentVariable("CAKEMAIL_USERNAME");
			var password = Environment.GetEnvironmentVariable("CAKEMAIL_PASSWORD");
			var overrideClientId = Environment.GetEnvironmentVariable("CAKEMAIL_OVERRIDECLIENTID");

			if (debugHttpMessagesToConsole)
			{
				LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());
			}

			var source = new CancellationTokenSource();
			Console.CancelKeyPress += (s, e) =>
			{
				e.Cancel = true;
				source.Cancel();
			};

			try
			{
				var client = new CakeMailRestClient(apiKey, proxy);
				var loginInfo = client.Users.LoginAsync(userName, password).Result;
				var clientId = string.IsNullOrEmpty(overrideClientId) ? loginInfo.ClientId : long.Parse(overrideClientId);
				var userKey = loginInfo.UserKey;

				var tasks = new Task[]
				{
					ExecuteAsync(client, userKey, clientId, source, TimezonesTests.ExecuteAllMethods),
					ExecuteAsync(client, userKey, clientId, source, CountriesTests.ExecuteAllMethods),
					ExecuteAsync(client, userKey, clientId, source, ClientsTests.ExecuteAllMethods),
					ExecuteAsync(client, userKey, clientId, source, UsersTests.ExecuteAllMethods),
					ExecuteAsync(client, userKey, clientId, source, PermissionsTests.ExecuteAllMethods),
					ExecuteAsync(client, userKey, clientId, source, CampaignsTests.ExecuteAllMethods),
					ExecuteAsync(client, userKey, clientId, source, ListsTests.ExecuteAllMethods),
					ExecuteAsync(client, userKey, clientId, source, TemplatesTests.ExecuteAllMethods),
					ExecuteAsync(client, userKey, clientId, source, SuppressionListsTests.ExecuteAllMethods),
					ExecuteAsync(client, userKey, clientId, source, RelaysTests.ExecuteAllMethods),
					ExecuteAsync(client, userKey, clientId, source, TriggersTests.ExecuteAllMethods),
					ExecuteAsync(client, userKey, clientId, source, MailingsTests.ExecuteAllMethods)
				};
				await Task.WhenAll(tasks).ConfigureAwait(false);
				return await Task.FromResult(0); // Success.
			}
			catch (OperationCanceledException)
			{
				return 1223; // Cancelled.
			}
			catch (Exception e)
			{
				source.Cancel();
				var log = new StringWriter();
				await log.WriteLineAsync("\n\n**************************************************").ConfigureAwait(false);
				await log.WriteLineAsync("**************************************************").ConfigureAwait(false);
				await log.WriteLineAsync($"AN EXCEPTION OCCURED: {e.GetBaseException().Message}").ConfigureAwait(false);
				await log.WriteLineAsync("**************************************************").ConfigureAwait(false);
				await log.WriteLineAsync("**************************************************").ConfigureAwait(false);
				await Console.Out.WriteLineAsync(log.ToString()).ConfigureAwait(false);
				return 1; // Exception
			}
			finally
			{
				var log = new StringWriter();
				await log.WriteLineAsync("\n\n**************************************************").ConfigureAwait(false);
				await log.WriteLineAsync("All tests completed").ConfigureAwait(false);
				await log.WriteLineAsync("Press any key to exit").ConfigureAwait(false);
				Prompt(log.ToString());
			}
		}

		private static char Prompt(string prompt)
		{
			while (Console.KeyAvailable)
			{
				Console.ReadKey(false);
			}
			Console.Out.WriteLine(prompt);
			var result = Console.ReadKey();
			return result.KeyChar;
		}

		private static async Task<int> ExecuteAsync(ICakeMailRestClient client, string userKey, long clientId, CancellationTokenSource cts, Func<ICakeMailRestClient, string, long, TextWriter, CancellationToken, Task> asyncTask)
		{
			var log = new StringWriter();

			try
			{
				await asyncTask(client, userKey, clientId, log, cts.Token).ConfigureAwait(false);
			}
			catch (OperationCanceledException)
			{
				await log.WriteLineAsync($"-----> TASK CANCELLED").ConfigureAwait(false);
				return 1223; // Cancelled.
			}
			catch (Exception e)
			{
				await log.WriteLineAsync($"-----> AN EXCEPTION OCCURED: {e.GetBaseException().Message}").ConfigureAwait(false);
				throw;
			}
			finally
			{
				await Console.Out.WriteLineAsync(log.ToString()).ConfigureAwait(false);
			}

			return 0;   // Success
		}
	}
}
