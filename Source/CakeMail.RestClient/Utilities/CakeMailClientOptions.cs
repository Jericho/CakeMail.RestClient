using CakeMail.RestClient.Logging;

namespace CakeMail.RestClient.Utilities
{
	/// <summary>
	/// Options for the CakeMail client.
	/// </summary>
	public class CakeMailClientOptions
	{
		/// <summary>
		/// Gets or sets the log levels for successful calls (HTTP status code in the 200-299 range).
		/// </summary>
		public LogLevel LogLevelSuccessfulCalls { get; set; }

		/// <summary>
		/// Gets or sets the log levels for failed calls (HTTP status code outside of the 200-299 range).
		/// </summary>
		public LogLevel LogLevelFailedCalls { get; set; }
	}
}
