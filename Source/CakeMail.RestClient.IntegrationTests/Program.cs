using Logzio.DotNet.NLog;
using Microsoft.Extensions.DependencyInjection;
using NLog.Config;
using NLog.Extensions.Logging;
using NLog.Targets;
using System;
using System.Threading.Tasks;

namespace CakeMail.RestClient.IntegrationTests
{
	public class Program
	{
		public static async Task<int> Main(string[] args)
		{
			var services = new ServiceCollection();
			ConfigureServices(services);
			using var serviceProvider = services.BuildServiceProvider();
			var app = serviceProvider.GetService<TestsRunner>();
			return await app.RunAsync().ConfigureAwait(false);
		}

		private static void ConfigureServices(ServiceCollection services)
		{
			services
				.AddLogging(loggingBuilder => loggingBuilder.AddNLog(GetNLogConfiguration()))
				.AddTransient<TestsRunner>();
		}

		private static LoggingConfiguration GetNLogConfiguration()
		{
			// Configure logging
			var nLogConfig = new LoggingConfiguration();

			// Send logs to logz.io
			var logzioToken = Environment.GetEnvironmentVariable("LOGZIO_TOKEN");
			if (!string.IsNullOrEmpty(logzioToken))
			{
				var logzioTarget = new LogzioTarget { Token = logzioToken };
				logzioTarget.ContextProperties.Add(new TargetPropertyWithContext("source", "CakeMailRestClient_integration_tests"));
				logzioTarget.ContextProperties.Add(new TargetPropertyWithContext("CakeMailRestClient-Version", CakeMailRestClient.Version));

				nLogConfig.AddTarget("Logzio", logzioTarget);
				nLogConfig.AddRule(NLog.LogLevel.Debug, NLog.LogLevel.Fatal, "Logzio", "*");
			}

			// Send logs to console
			var consoleTarget = new ColoredConsoleTarget();
			nLogConfig.AddTarget("ColoredConsole", consoleTarget);
			nLogConfig.AddRule(NLog.LogLevel.Warn, NLog.LogLevel.Fatal, "ColoredConsole", "*");

			return nLogConfig;
		}
	}
}
