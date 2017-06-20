using CakeMail.RestClient.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	/// <summary>
	/// Allows you to manage relays
	/// </summary>
	public interface IRelays
	{
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
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of <see cref="RelayBounceLog">log items</see> matching the filter criteria</returns>
		Task<RelayBounceLog[]> GetBounceLogsAsync(string userKey, long? trackingId = default(long?), DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));

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
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of <see cref="RelayClickLog">log items</see> matching the filter criteria</returns>
		Task<RelayClickLog[]> GetClickLogsAsync(string userKey, long? trackingId = default(long?), DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));

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
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of <see cref="RelayOpenLog">log items</see> matching the filter criteria</returns>
		Task<RelayOpenLog[]> GetOpenLogsAsync(string userKey, long? trackingId = default(long?), DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));

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
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of <see cref="RelayLog">log items</see> matching the filter criteria</returns>
		Task<RelayLog[]> GetSentLogsAsync(string userKey, long? trackingId = default(long?), DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));

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
		/// <param name="mergeData">Data to be merged into the content of the email</param>
		/// <param name="encoding">Encoding to be used for the relay. Possible values: 'utf-8', 'iso-8859-x'</param>
		/// <param name="clientId">Client ID of the client in which the relay is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the email is sent</returns>
		Task<bool> SendWithoutTrackingAsync(string userKey, string recipientEmailAddress, string subject, string html, string text, string senderEmail, string senderName = null, IDictionary<string, object> mergeData = null, MessageEncoding? encoding = default(MessageEncoding?), long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));

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
		/// <param name="mergeData">Data to be merged into the content of the email</param>
		/// <param name="encoding">Encoding to be used for the relay. Possible values: 'utf-8', 'iso-8859-x'</param>
		/// <param name="clientId">Client ID of the client in which the relay is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the email is sent</returns>
		Task<bool> SendWithTrackingAsync(string userKey, long trackingId, string recipientEmailAddress, string subject, string html, string text, string senderEmail, string senderName = null, IDictionary<string, object> mergeData = null, MessageEncoding? encoding = default(MessageEncoding?), long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
	}
}
