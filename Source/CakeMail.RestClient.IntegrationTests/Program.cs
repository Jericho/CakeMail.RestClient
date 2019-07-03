using CakeMail.RestClient.IntegrationTests.Tests;
using CakeMail.RestClient.Utilities;
using Logzio.DotNet.NLog;
using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.IntegrationTests
{
	public class Program
	{
		private const int MAX_CAKEMAIL_API_CONCURRENCY = 5;

		private enum ResultCodes
		{
			Success = 0,
			Exception = 1,
			Cancelled = 1223
		}

		static async Task<int> Main()
		{
			// -----------------------------------------------------------------------------
			// Do you want to proxy requests through Fiddler? Can be useful for debugging.
			var useFiddler = false;

			// Logging options.
			var options = new CakeMailClientOptions()
			{
				LogLevelFailedCalls = CakeMail.RestClient.Logging.LogLevel.Error,
				LogLevelSuccessfulCalls = CakeMail.RestClient.Logging.LogLevel.Debug
			};
			// -----------------------------------------------------------------------------

			// Configure logging
			var nLogConfig = new LoggingConfiguration();

			// Send logs to logz.io
			var logzioToken = Environment.GetEnvironmentVariable("LOGZIO_TOKEN");
			if (!string.IsNullOrEmpty(logzioToken))
			{
				var logzioTarget = new LogzioTarget { Token = logzioToken };
				logzioTarget.ContextProperties.Add(new TargetPropertyWithContext("source", "StrongGrid_integration_tests"));
				logzioTarget.ContextProperties.Add(new TargetPropertyWithContext("StrongGrid-Version", CakeMailRestClient.Version));

				nLogConfig.AddTarget("Logzio", logzioTarget);
				nLogConfig.AddRule(NLog.LogLevel.Debug, NLog.LogLevel.Fatal, "Logzio", "*");
			}

			// Send logs to console
			var consoleTarget = new ColoredConsoleTarget();
			nLogConfig.AddTarget("ColoredConsole", consoleTarget);
			nLogConfig.AddRule(NLog.LogLevel.Warn, NLog.LogLevel.Fatal, "ColoredConsole", "*");

			LogManager.Configuration = nLogConfig;

			// Configure CakeMail client
			var apiKey = Environment.GetEnvironmentVariable("CAKEMAIL_APIKEY");
			var userName = Environment.GetEnvironmentVariable("CAKEMAIL_USERNAME");
			var password = Environment.GetEnvironmentVariable("CAKEMAIL_PASSWORD");
			var overrideClientId = Environment.GetEnvironmentVariable("CAKEMAIL_OVERRIDECLIENTID");
			var proxy = useFiddler ? new WebProxy("http://localhost:8888") : null;
			var client = new CakeMailRestClient(apiKey, proxy);
			var loginInfo = client.Users.LoginAsync(userName, password).Result;
			var clientId = string.IsNullOrEmpty(overrideClientId) ? loginInfo.ClientId : long.Parse(overrideClientId);
			var userKey = loginInfo.UserKey;

			// Configure Console
			var source = new CancellationTokenSource();
			Console.CancelKeyPress += (s, e) =>
			{
				e.Cancel = true;
				source.Cancel();
			};

			// Ensure the Console is tall enough and centered on the screen
			Console.WindowHeight = Math.Min(60, Console.LargestWindowHeight);
			ConsoleUtils.CenterConsole();

			// These are the integration tests that we will execute
			var integrationTests = new Type[]
			{
				typeof(CampaignsTests),
				typeof(ClientsTests),
				typeof(CountriesTests),
				typeof(ListsTests),
				typeof(MailingsTests),
				typeof(PermissionsTests),
				typeof(RelaysTests),
				typeof(SuppressionListsTests),
				typeof(TemplatesTests),
				typeof(TimezonesTests),
				typeof(TriggersTests),
				typeof(UsersTests),
			};

			// Execute the async tests in parallel (with max degree of parallelism)
			var results = await integrationTests.ForEachAsync(
				async integrationTestType =>
				{
					var log = new StringWriter();

					try
					{
						var integrationTest = (IIntegrationTest)Activator.CreateInstance(integrationTestType);
						await integrationTest.Execute(client, userKey, clientId, log, source.Token).ConfigureAwait(false);
						return (TestName: integrationTestType.Name, ResultCode: ResultCodes.Success, Message: string.Empty);
					}
					catch (OperationCanceledException)
					{
						await log.WriteLineAsync($"-----> TASK CANCELLED").ConfigureAwait(false);
						return (TestName: integrationTestType.Name, ResultCode: ResultCodes.Cancelled, Message: "Task cancelled");
					}
					catch (Exception e)
					{
						var exceptionMessage = e.GetBaseException().Message;
						await log.WriteLineAsync($"-----> AN EXCEPTION OCCURRED: {exceptionMessage}").ConfigureAwait(false);
						return (TestName: integrationTestType.Name, ResultCode: ResultCodes.Exception, Message: exceptionMessage);
					}
					finally
					{
						await Console.Out.WriteLineAsync(log.ToString()).ConfigureAwait(false);
					}
				}, MAX_CAKEMAIL_API_CONCURRENCY)
			.ConfigureAwait(false);

			// Display summary
			var summary = new StringWriter();
			await summary.WriteLineAsync("\n\n**************************************************").ConfigureAwait(false);
			await summary.WriteLineAsync("******************** SUMMARY *********************").ConfigureAwait(false);
			await summary.WriteLineAsync("**************************************************").ConfigureAwait(false);

			var resultsWithMessage = results
				.Where(r => !string.IsNullOrEmpty(r.Message))
				.ToArray();

			if (resultsWithMessage.Any())
			{
				foreach (var (TestName, ResultCode, Message) in resultsWithMessage)
				{
					const int TEST_NAME_MAX_LENGTH = 25;
					var name = TestName.Length <= TEST_NAME_MAX_LENGTH ? TestName : TestName.Substring(0, TEST_NAME_MAX_LENGTH - 3) + "...";
					await summary.WriteLineAsync($"{name.PadRight(TEST_NAME_MAX_LENGTH, ' ')} : {Message}").ConfigureAwait(false);
				}
			}
			else
			{
				await summary.WriteLineAsync("All tests completed succesfully").ConfigureAwait(false);
			}

			await summary.WriteLineAsync("**************************************************").ConfigureAwait(false);
			await Console.Out.WriteLineAsync(summary.ToString()).ConfigureAwait(false);

			// Prompt user to press a key in order to allow reading the log in the console
			var promptLog = new StringWriter();
			await promptLog.WriteLineAsync("\n\n**************************************************").ConfigureAwait(false);
			await promptLog.WriteLineAsync("Press any key to exit").ConfigureAwait(false);
			Prompt(promptLog.ToString());

			// Return code indicating success/failure
			var resultCode = (int)ResultCodes.Success;
			if (results.Any(result => result.ResultCode != ResultCodes.Success))
			{
				if (results.Any(result => result.ResultCode == ResultCodes.Exception)) resultCode = (int)ResultCodes.Exception;
				else if (results.Any(result => result.ResultCode == ResultCodes.Cancelled)) resultCode = (int)ResultCodes.Cancelled;
				else resultCode = (int)results.First(result => result.ResultCode != ResultCodes.Success).ResultCode;
			}

			return await Task.FromResult(resultCode);
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
	}
}
