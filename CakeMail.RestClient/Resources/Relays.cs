using CakeMail.RestClient.Models;
using CakeMail.RestClient.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	public class Relays
	{
		#region Fields

		private readonly CakeMailRestClient _cakeMailRestClient;

		#endregion

		#region Constructor

		public Relays(CakeMailRestClient cakeMailRestClient)
		{
			_cakeMailRestClient = cakeMailRestClient;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Send a one-off email without tracking opens and clicks
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="recipientEmailAddress">The email address of the recipient.</param>
		/// <param name="subject">Subject of the relay</param>
		/// <param name="html">HTML content of the relay.</param>
		/// <param name="text">Text content of the relay.</param>
		/// <param name="senderEmail">Email address of the sender of the relay.</param>
		/// <param name="senderName">Name of the sender of the relay.</param>
		/// <param name="encoding">Encoding to be used for the relay. Possible values: 'utf-8', 'iso-8859-x'</param>
		/// <param name="clientId">Client ID of the client in which the relay is located.</param>
		/// <returns>True if the email is sent</returns>
		public async Task<bool> SendWithoutTrackingAsync(string userKey, string recipientEmailAddress, string subject, string html, string text, string senderEmail, string senderName = null, MessageEncoding? encoding = null, long? clientId = null)
		{
			string path = "/Relay/Send/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("email", recipientEmailAddress),
				new KeyValuePair<string, object>("subject", subject),
				new KeyValuePair<string, object>("html_message", html),
				new KeyValuePair<string, object>("text_message", text),
				new KeyValuePair<string, object>("sender_email", senderEmail),
				new KeyValuePair<string, object>("track_opening", "false"),
				new KeyValuePair<string, object>("track_clicks_in_html", "false"),
				new KeyValuePair<string, object>("track_clicks_in_text", "false")
			};
			if (senderName != null) parameters.Add(new KeyValuePair<string, object>("sender_name", senderName));
			if (encoding.HasValue) parameters.Add(new KeyValuePair<string, object>("encoding", encoding.Value.GetEnumMemberValue()));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<bool>(path, parameters).ConfigureAwait(false);
		}

		/// <summary>
		/// Send a one-off email. Track opens and clicks.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="trackingId">ID for tracking purposes.</param>
		/// <param name="recipientEmailAddress">The email address of the recipient.</param>
		/// <param name="subject">Subject of the relay</param>
		/// <param name="html">HTML content of the relay.</param>
		/// <param name="text">Text content of the relay.</param>
		/// <param name="senderEmail">Email address of the sender of the relay.</param>
		/// <param name="senderName">Name of the sender of the relay.</param>
		/// <param name="encoding">Encoding to be used for the relay. Possible values: 'utf-8', 'iso-8859-x'</param>
		/// <param name="clientId">Client ID of the client in which the relay is located.</param>
		/// <returns>True if the email is sent</returns>
		public async Task<bool> SendWithTrackingAsync(string userKey, long trackingId, string recipientEmailAddress, string subject, string html, string text, string senderEmail, string senderName = null, MessageEncoding? encoding = null, long? clientId = null)
		{
			string path = "/Relay/Send/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("tracking_id", trackingId),
				new KeyValuePair<string, object>("email", recipientEmailAddress),
				new KeyValuePair<string, object>("subject", subject),
				new KeyValuePair<string, object>("html_message", html),
				new KeyValuePair<string, object>("text_message", text),
				new KeyValuePair<string, object>("sender_email", senderEmail),
				new KeyValuePair<string, object>("track_opening", "true"),
				new KeyValuePair<string, object>("track_clicks_in_html", "true"),
				new KeyValuePair<string, object>("track_clicks_in_text", "true")
			};
			if (senderName != null) parameters.Add(new KeyValuePair<string, object>("sender_name", senderName));
			if (encoding.HasValue) parameters.Add(new KeyValuePair<string, object>("encoding", encoding.Value.GetEnumMemberValue()));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<bool>(path, parameters).ConfigureAwait(false);
		}

		/// <summary>
		/// Retrieve the log items for either a given tracked relay or for a client
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="trackingId">ID of the tracked relay. If this value is omitted, all log items for the client will be returned</param>
		/// <param name="start">Filter using a start date</param>
		/// <param name="end">Filter using an end date</param>
		/// <param name="limit">Limit the number of resulting log items.</param>
		/// <param name="offset">Offset the beginning of resulting log items.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>An enumeration of <see cref="RelayLog">log items</see> matching the filter criteria</returns>
		public async Task<IEnumerable<RelayLog>> GetSentLogsAsync(string userKey, long? trackingId = null, DateTime? start = null, DateTime? end = null, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			return await GetLogsAsync<RelayLog>(userKey, "sent", "sent_logs", trackingId, start, end, limit, offset, clientId).ConfigureAwait(false);
		}

		/// <summary>
		/// Retrieve the log items for either a given tracked relay or for a client
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="trackingId">ID of the tracked relay. If this value is omitted, all log items for the client will be returned</param>
		/// <param name="start">Filter using a start date</param>
		/// <param name="end">Filter using an end date</param>
		/// <param name="limit">Limit the number of resulting log items.</param>
		/// <param name="offset">Offset the beginning of resulting log items.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>An enumeration of <see cref="RelayOpenLog">log items</see> matching the filter criteria</returns>
		public async Task<IEnumerable<RelayOpenLog>> GetOpenLogsAsync(string userKey, long? trackingId = null, DateTime? start = null, DateTime? end = null, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			return await GetLogsAsync<RelayOpenLog>(userKey, "open", "open_logs", trackingId, start, end, limit, offset, clientId).ConfigureAwait(false);
		}

		/// <summary>
		/// Retrieve the log items for either a given tracked relay or for a client
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="trackingId">ID of the tracked relay. If this value is omitted, all log items for the client will be returned</param>
		/// <param name="start">Filter using a start date</param>
		/// <param name="end">Filter using an end date</param>
		/// <param name="limit">Limit the number of resulting log items.</param>
		/// <param name="offset">Offset the beginning of resulting log items.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>An enumeration of <see cref="RelayClickLog">log items</see> matching the filter criteria</returns>
		public async Task<IEnumerable<RelayClickLog>> GetClickLogsAsync(string userKey, long? trackingId = null, DateTime? start = null, DateTime? end = null, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			return await GetLogsAsync<RelayClickLog>(userKey, "clickthru", "clickthru_logs", trackingId, start, end, limit, offset, clientId).ConfigureAwait(false);
		}

		/// <summary>
		/// Retrieve the log items for either a given tracked relay or for a client
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="trackingId">ID of the tracked relay. If this value is omitted, all log items for the client will be returned</param>
		/// <param name="start">Filter using a start date</param>
		/// <param name="end">Filter using an end date</param>
		/// <param name="limit">Limit the number of resulting log items.</param>
		/// <param name="offset">Offset the beginning of resulting log items.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>An enumeration of <see cref="RelayBounceLog">log items</see> matching the filter criteria</returns>
		public async Task<IEnumerable<RelayBounceLog>> GetBounceLogsAsync(string userKey, long? trackingId = null, DateTime? start = null, DateTime? end = null, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			return await GetLogsAsync<RelayBounceLog>(userKey, "bounce", "bounce_logs", trackingId, start, end, limit, offset, clientId).ConfigureAwait(false);
		}

		#endregion

		#region PRIVATE METHODS

		private async Task<IEnumerable<T>> GetLogsAsync<T>(string userKey, string logType, string arrayPropertyName, long? trackingId = null, DateTime? start = null, DateTime? end = null, int? limit = 0, int? offset = 0, long? clientId = null) where T : RelayLog, new()
		{
			string path = "/Relay/GetLogs/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("log_type", logType)
			};
			if (trackingId.HasValue) parameters.Add(new KeyValuePair<string, object>("tracking_id", trackingId.Value));
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_time", start.Value.ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_time", end.Value.ToCakeMailString()));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteArrayRequestAsync<T>(path, parameters, arrayPropertyName).ConfigureAwait(false);
		}

		#endregion
	}
}
