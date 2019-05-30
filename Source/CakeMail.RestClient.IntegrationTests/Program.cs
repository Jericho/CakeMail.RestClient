using CakeMail.RestClient.IntegrationTests.Tests;
using CakeMail.RestClient.Logging;
using CakeMail.RestClient.Utilities;
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
			// Do you want to proxy requests through Fiddler? Can be useful for debugging.
			var useFiddler = false;

			// Do you want debug information displayed in the console?
			var logToConsole = true;

			// Logging options.
			var options = new CakeMailClientOptions()
			{
				LogLevelFailedCalls = LogLevel.Error,
				LogLevelSuccessfulCalls = LogLevel.Debug
			};

			// To see only errors, set this value to 'LogLevel.Error'.
			// To see every single call made to SendGrid's API, set this value to 'LogLevel.Debug'.
			var minLogLevel = LogLevel.Error;
			// -----------------------------------------------------------------------------

			var proxy = useFiddler ? new WebProxy("http://localhost:8888") : null;
			var apiKey = Environment.GetEnvironmentVariable("CAKEMAIL_APIKEY");
			var userName = Environment.GetEnvironmentVariable("CAKEMAIL_USERNAME");
			var password = Environment.GetEnvironmentVariable("CAKEMAIL_PASSWORD");
			var overrideClientId = Environment.GetEnvironmentVariable("CAKEMAIL_OVERRIDECLIENTID");

			if (logToConsole)
			{
				LogProvider.SetCurrentLogProvider(new ColoredConsoleLogProvider(minLogLevel));
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
