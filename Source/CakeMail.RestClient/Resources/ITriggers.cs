using CakeMail.RestClient.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	/// <summary>
	/// Allows you to manage triggers
	/// </summary>
	public interface ITriggers
	{
		/// <summary>
		/// Create a trigger
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="name">Name of the trigger.</param>
		/// <param name="listId">ID of the list you want to associate the trigger with.</param>
		/// <param name="campaignId">ID of the campaign you want to associate the trigger with.</param>
		/// <param name="encoding">Encoding to be used for the trigger. Possible values: 'utf-8', 'iso-8859-x'</param>
		/// <param name="transferEncoding">Transfer encoding to be used for the trigger. Possible values: 'quoted-printable', 'base64'</param>
		/// <param name="clientId">Client ID of the client in which the mailing is created.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>ID of the new trigger</returns>
		Task<long> CreateAsync(string userKey, string name, long listId, long? campaignId = default(long?), MessageEncoding? encoding = default(MessageEncoding?), TransferEncoding? transferEncoding = default(TransferEncoding?), long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieve a trigger
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="triggerId">ID of the trigger</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The <see cref="Trigger">trigger</see></returns>
		Task<Trigger> GetAsync(string userKey, long triggerId, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Get a count of triggers matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the trigger status. Possible values: 'active', 'inactive'</param>
		/// <param name="action">Filter using the trigger action. Possible values: 'opt-in', 'douopt-in', 'opt-out', 'specific', 'annual'</param>
		/// <param name="listId">Filter using the ID of the trigger list.</param>
		/// <param name="campaignId">Filter using the ID of the trigger campaign.</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The count of triggers matching the filtering criteria</returns>
		Task<long> GetCountAsync(string userKey, TriggerStatus? status = default(TriggerStatus?), TriggerAction? action = default(TriggerAction?), long? listId = default(long?), long? campaignId = default(long?), long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieve a link
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="linkId">ID of the link.</param>
		/// <param name="clientId">Client ID of the client in which the link is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The <see cref="Link">link</see></returns>
		/// <remarks>
		/// This method is documented on CakeMail's web site (http://dev.cakemail.com/api/Trigger/GetLinkInfo) but unfortunately, it's not implemented.
		/// Invoking this method will result in an exception with the following error message: "Invalid Method: GetLinkInfo".
		/// </remarks>
		Task<Link> GetLinkAsync(string userKey, long linkId, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieve the links for a given trigger
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="triggerId">ID of the trigger.</param>
		/// <param name="limit">Limit the number of resulting log items.</param>
		/// <param name="offset">Offset the beginning of resulting log items.</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of <see cref="Link">links</see> matching the filter criteria</returns>
		Task<Link[]> GetLinksAsync(string userKey, long triggerId, int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Get a count of links matching the filter criteria
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="triggerId">ID of the trigger.</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The number of links matching the filtering criteria</returns>
		Task<long> GetLinksCountAsync(string userKey, long triggerId, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieve the links (with their statistics) for a given trigger
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="triggerId">ID of the trigger.</param>
		/// <param name="start">Filter using a start date</param>
		/// <param name="end">Filter using an end date</param>
		/// <param name="limit">Limit the number of resulting log items.</param>
		/// <param name="offset">Offset the beginning of resulting log items.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of <see cref="LinkStats">links with their statistics</see> matching the filter criteria</returns>
		Task<LinkStats[]> GetLinksWithStatsAsync(string userKey, long triggerId, DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Get a count of links (with their statistics) for a given trigger
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="triggerId">ID of the trigger.</param>
		/// <param name="start">Filter using a start date</param>
		/// <param name="end">Filter using an end date</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The number of links matching the filter criteria</returns>
		Task<long> GetLinksWithStatsCountAsync(string userKey, long triggerId, DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieve the log items for a given trigger
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="triggerId">ID of the trigger.</param>
		/// <param name="logType">Filter using the log action.</param>
		/// <param name="listMemberId">Filter using the ID of the member.</param>
		/// <param name="uniques">Return unique log item per member.</param>
		/// <param name="totals">Return all the log items.</param>
		/// <param name="start">Filter using a start date</param>
		/// <param name="end">Filter using an end date</param>
		/// <param name="limit">Limit the number of resulting log items.</param>
		/// <param name="offset">Offset the beginning of resulting log items.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of <see cref="LogItem">log items</see> matching the filter criteria</returns>
		Task<LogItem[]> GetLogsAsync(string userKey, long triggerId, LogType? logType = default(LogType?), long? listMemberId = default(long?), bool uniques = false, bool totals = false, DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Get a count of log items matching the filter criteria
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="triggerId">ID of the trigger.</param>
		/// <param name="logType">Filter using the log action.</param>
		/// <param name="listMemberId">Filter using the ID of the member.</param>
		/// <param name="uniques">Return unique log item per member.</param>
		/// <param name="totals">Return all the log items.</param>
		/// <param name="start">Filter using a start date</param>
		/// <param name="end">Filter using an end date</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The number of log items matching the filtering criteria</returns>
		Task<long> GetLogsCountAsync(string userKey, long triggerId, LogType? logType = default(LogType?), long? listMemberId = default(long?), bool uniques = false, bool totals = false, DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Get the multi-part version of a trigger
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="triggerId">ID of the trigger</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The <see cref="RawEmailMessage">multi-part message</see></returns>
		Task<RawEmailMessage> GetRawEmailMessageAsync(string userKey, long triggerId, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Get the rendered HTML version of a trigger.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="triggerId">ID of the trigger</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The rendered HTML</returns>
		Task<string> GetRawHtmlAsync(string userKey, long triggerId, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Get the rendered text version of a mailing.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="triggerId">ID of the trigger</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The rendered text</returns>
		Task<string> GetRawTextAsync(string userKey, long triggerId, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieve the triggers matching the filtering criteria
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the trigger status. Possible values: 'active', 'inactive'</param>
		/// <param name="action">Filter using the trigger action. Possible values: 'opt-in', 'douopt-in', 'opt-out', 'specific', 'annual'</param>
		/// <param name="listId">Filter using the ID of the trigger list.</param>
		/// <param name="campaignId">Filter using the ID of the trigger campaign.</param>
		/// <param name="limit">Limit the number of resulting triggers.</param>
		/// <param name="offset">Offset the beginning of resulting triggers.</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of <see cref="Trigger">triggers</see> matching the filter criteria</returns>
		Task<Trigger[]> GetTriggersAsync(string userKey, TriggerStatus? status = default(TriggerStatus?), TriggerAction? action = default(TriggerAction?), long? listId = default(long?), long? campaignId = default(long?), int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Send a test of a trigger.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="triggerId">ID of the trigger</param>
		/// <param name="recipientEmail">Email address where the test will be sent.</param>
		/// <param name="separated">True if you want the HTML and the text to be sent seperatly, false if you want to combine the HTML and the text in a multi-part email.</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the test email was sent</returns>
		Task<bool> SendTestEmailAsync(string userKey, long triggerId, string recipientEmail, bool separated = false, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Unleash a trigger
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="triggerId">ID of the trigger</param>
		/// <param name="listMemberId">ID of the member to unleash the trigger to.</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True is the trigger is unleashed</returns>
		Task<bool> UnleashAsync(string userKey, long triggerId, long listMemberId, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Update a trigger
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="triggerId">ID of the trigger</param>
		/// <param name="campaignId">ID of the campaign you want to associate the trigger with.</param>
		/// <param name="name">Name of the trigger</param>
		/// <param name="action">Action of the trigger. Possible values: 'opt-in', 'douopt-in', 'opt-out', 'specific', 'annual'</param>
		/// <param name="encoding">Encoding to be used for the trigger. Possible values: 'utf-8', 'iso-8859-x'</param>
		/// <param name="transferEncoding">Transfer encoding to be used for the trigger. Possible values: 'quoted-printable', 'base64'</param>
		/// <param name="subject">Subject of the trigger.</param>
		/// <param name="senderEmail">Email address of the sender of the trigger.</param>
		/// <param name="senderName">Name of the sender of the trigger.</param>
		/// <param name="replyTo">Email address of the reply-to of the trigger.</param>
		/// <param name="htmlContent">HTML content of the trigger.</param>
		/// <param name="textContent">Text content of the trigger.</param>
		/// <param name="trackOpens">Track the opens in the HTML version.</param>
		/// <param name="trackClicksInHtml">Track the clicks in the HTML version.</param>
		/// <param name="trackClicksInText">Track the clicks in the text version.</param>
		/// <param name="trackingParameters">Additional tracking parameters for links.</param>
		/// <param name="delay">Delay (in seconds) to be used when the trigger is unleashed.</param>
		/// <param name="status">Status of the trigger. Possible values: 'active', 'inactive'</param>
		/// <param name="dateField">DateTime field to be used for trigger with action 'specific' or 'annual'.</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the trigger was updated</returns>
		Task<bool> UpdateAsync(string userKey, long triggerId, long? campaignId = default(long?), string name = null, TriggerAction? action = default(TriggerAction?), MessageEncoding? encoding = default(MessageEncoding?), TransferEncoding? transferEncoding = default(TransferEncoding?), string subject = null, string senderEmail = null, string senderName = null, string replyTo = null, string htmlContent = null, string textContent = null, bool? trackOpens = default(bool?), bool? trackClicksInHtml = default(bool?), bool? trackClicksInText = default(bool?), string trackingParameters = null, int? delay = default(int?), TriggerStatus? status = default(TriggerStatus?), string dateField = null, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Delete a trigger
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="triggerId">ID of the trigger</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the trigger is deleted</returns>
		Task<bool> DeleteAsync(string userKey, long triggerId, long? clientId = null, CancellationToken cancellationToken = default);
	}
}
