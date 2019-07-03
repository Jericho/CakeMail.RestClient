using CakeMail.RestClient.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	/// <summary>
	/// Allows you to manage mailings.
	/// </summary>
	public interface IMailings
	{
		/// <summary>
		/// Create a mailing.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="name">Name of the mailing.</param>
		/// <param name="campaignId">ID of the campaign you want to associate the mailing with.</param>
		/// <param name="type">Type of the mailing. Possible values: 'standard', 'recurring', 'absplit'.</param>
		/// <param name="recurringId">ID of the first recurrence in case of a 'recurring' or 'absplit' mailing.</param>
		/// <param name="encoding">Encoding to be used for the mailing. Possible values: 'utf-8', 'iso-8859-x'.</param>
		/// <param name="transferEncoding">Transfer encoding to be used for the mailing. Possible values: 'quoted-printable', 'base64'.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is created.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>ID of the new mailing.</returns>
		Task<long> CreateAsync(string userKey, string name, long? campaignId = default, MailingType type = MailingType.Standard, long? recurringId = default, MessageEncoding? encoding = default, TransferEncoding? transferEncoding = default, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Delete a mailing.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>True if the mailing is deleted.</returns>
		Task<bool> DeleteAsync(string userKey, long mailingId, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieve a mailing.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The <see cref="Mailing">mailing</see>.</returns>
		Task<Mailing> GetAsync(string userKey, long mailingId, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Get a count of mailings matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the mailing status. Possible values: 'incomplete', 'scheduled', 'delivering', 'delivered'.</param>
		/// <param name="type">Filter using the mailing type. Possible values: 'standard', 'recurring', 'absplit'.</param>
		/// <param name="name">Filter using the mailing name.</param>
		/// <param name="listId">Filter using the ID of the mailing list.</param>
		/// <param name="campaignId">Filter using the ID of the mailing campaign.</param>
		/// <param name="recurringId">Filter using the ID of the mailing recurrence.</param>
		/// <param name="start">Filter using a start date.</param>
		/// <param name="end">Filter using a end date.</param>
		/// <param name="clientId">Client ID of the client in which the mailings are located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The count of mailings matching the filtering criteria.</returns>
		Task<long> GetCountAsync(string userKey, MailingStatus? status = default, MailingType? type = default, string name = null, long? listId = default, long? campaignId = default, long? recurringId = default, DateTime? start = default, DateTime? end = default, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieve a link.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="linkId">ID of the link.</param>
		/// <param name="clientId">Client ID of the client in which the link is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The <see cref="Link">link</see>.</returns>
		Task<Link> GetLinkAsync(string userKey, long linkId, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieve the links for a given mailing.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing.</param>
		/// <param name="limit">Limit the number of resulting log items.</param>
		/// <param name="offset">Offset the beginning of resulting log items.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>An enumeration of <see cref="Link">links</see> matching the filter criteria.</returns>
		/// <remarks>
		/// The CakeMail API returns an empty array if you attempt to get the links in a mailing that has not been sent, even if the HTML contains multiple links.
		/// </remarks>
		Task<Link[]> GetLinksAsync(string userKey, long mailingId, int? limit = 0, int? offset = 0, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Get a count of links matching the filter criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The number of links matching the filtering criteria.</returns>
		Task<long> GetLinksCountAsync(string userKey, long mailingId, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieve the links (with their statistics) for a given mailing.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing.</param>
		/// <param name="start">Filter using a start date.</param>
		/// <param name="end">Filter using an end date.</param>
		/// <param name="limit">Limit the number of resulting links.</param>
		/// <param name="offset">Offset the beginning of resulting links.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>An enumeration of <see cref="LinkStats">links with their statistics</see> matching the filter criteria.</returns>
		Task<LinkStats[]> GetLinksWithStatsAsync(string userKey, long mailingId, DateTime? start = default, DateTime? end = default, int? limit = 0, int? offset = 0, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Get a count of links (with their statistics) for a given mailing.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing.</param>
		/// <param name="start">Filter using a start date.</param>
		/// <param name="end">Filter using an end date.</param>
		/// <param name="limit">Limit the number of resulting links.</param>
		/// <param name="offset">Offset the beginning of resulting links.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The number of links matching the filter criteria.</returns>
		Task<long> GetLinksWithStatsCountAsync(string userKey, long mailingId, DateTime? start = default, DateTime? end = default, int? limit = 0, int? offset = 0, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieve the log items for a given mailing.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing.</param>
		/// <param name="logType">Filter using the log action. Possible values: "in_queue", "opened", "clickthru", "forward", "unsubscribe", "view", "spam", "skipped".</param>
		/// <param name="listMemberId">Filter using the ID of the member.</param>
		/// <param name="uniques">Return unique log item per member.</param>
		/// <param name="totals">Return all the log items.</param>
		/// <param name="start">Filter using a start date.</param>
		/// <param name="end">Filter using an end date.</param>
		/// <param name="limit">Limit the number of resulting log items.</param>
		/// <param name="offset">Offset the beginning of resulting log items.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>An enumeration of <see cref="LogItem">log items</see> matching the filter criteria.</returns>
		/// <remarks>
		/// CakeMail throws an exception if you attempt to retrieve the logs for a mailing that hasn't been sent.
		/// The current error message is cryptic: Table 'api_cake_logs.mailing_xxxxxxx_big' doesn't exist.
		/// I was assured in May 2015 that they will improve this message to make it more informative.
		/// </remarks>
		Task<LogItem[]> GetLogsAsync(string userKey, long mailingId, LogType? logType = default, long? listMemberId = default, bool uniques = false, bool totals = false, DateTime? start = default, DateTime? end = default, int? limit = 0, int? offset = 0, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Get a count of log items matching the filter criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing.</param>
		/// <param name="logType">Filter using the log action. Possible values: "in_queue", "opened", "clickthru", "forward", "unsubscribe", "view", "spam", "skipped".</param>
		/// <param name="listMemberId">Filter using the ID of the member.</param>
		/// <param name="uniques">Return unique log item per member.</param>
		/// <param name="totals">Return all the log items.</param>
		/// <param name="start">Filter using a start date.</param>
		/// <param name="end">Filter using an end date.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The number of log items matching the filtering criteria.</returns>
		Task<long> GetLogsCountAsync(string userKey, long mailingId, LogType? logType = default, long? listMemberId = default, bool uniques = false, bool totals = false, DateTime? start = default, DateTime? end = default, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieve the mailings matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the mailing status. Possible values: 'incomplete', 'scheduled', 'delivering', 'delivered'.</param>
		/// <param name="type">Filter using the mailing type. Possible values: 'standard', 'recurring', 'absplit'.</param>
		/// <param name="name">Filter using the mailing name.</param>
		/// <param name="listId">Filter using the ID of the mailing list.</param>
		/// <param name="campaignId">Filter using the ID of the mailing campaign.</param>
		/// <param name="recurringId">Filter using the ID of the mailing recurrence.</param>
		/// <param name="start">Filter using a start date.</param>
		/// <param name="end">Filter using a end date.</param>
		/// <param name="sortBy">Sort resulting mailings. Possible values: 'name', 'created_on', 'scheduled_for', 'scheduled_on', 'active_emails'.</param>
		/// <param name="sortDirection">Direction of the sorting. Possible values: 'asc', 'desc'.</param>
		/// <param name="limit">Limit the number of resulting mailings.</param>
		/// <param name="offset">Offset the beginning of resulting mailings.</param>
		/// <param name="clientId">Client ID of the client in which the mailings are located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>Enumeration of <see cref="Mailing">mailings</see> matching the filtering criteria.</returns>
		Task<Mailing[]> GetMailingsAsync(string userKey, MailingStatus? status = default, MailingType? type = default, string name = null, long? listId = default, long? campaignId = default, long? recurringId = default, DateTime? start = default, DateTime? end = default, MailingsSortBy? sortBy = default, SortDirection? sortDirection = default, int? limit = 0, int? offset = 0, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Get the multi-part version of a mailing.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The <see cref="RawEmailMessage">multi-part message</see>.</returns>
		Task<RawEmailMessage> GetRawEmailMessageAsync(string userKey, long mailingId, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Get the rendered HTML version of a mailing.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The rendered HTML.</returns>
		Task<string> GetRawHtmlAsync(string userKey, long mailingId, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Get the rendered text version of a mailing.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The rendered text.</returns>
		Task<string> GetRawTextAsync(string userKey, long mailingId, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Resume a suspended mailing.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>True if the mailing is resumed.</returns>
		Task<bool> ResumeAsync(string userKey, long mailingId, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Schedule a mailing.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing.</param>
		/// <param name="date">Date when the mailing is scheduled. If not provided, the mailing will be sent right away.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>True if the mailing is scheduled.</returns>
		Task<bool> ScheduleAsync(string userKey, long mailingId, DateTime? date = default, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Send a test of a mailing.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing.</param>
		/// <param name="recipientEmail">Email address where the test will be sent.</param>
		/// <param name="separated">True if you want the HTML and the text to be sent seperatly, false if you want to combine the HTML and the text in a multi-part email.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>True if the test email was sent.</returns>
		Task<bool> SendTestEmailAsync(string userKey, long mailingId, string recipientEmail, bool separated = false, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Suspend a delivering mailing.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>True if the mailing is suspended.</returns>
		Task<bool> SuspendAsync(string userKey, long mailingId, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Unschedule a mailing.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>True if the mailing is unscheduled.</returns>
		Task<bool> UnscheduleAsync(string userKey, long mailingId, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Update a mailing.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing.</param>
		/// <param name="campaignId">ID of the campaign you want to associate the mailing with.</param>
		/// <param name="listId">ID of the list you want to associate the mailing with.</param>
		/// <param name="sublistId">ID of the segment you want to associate the mailing with.</param>
		/// <param name="name">Name of the mailing.</param>
		/// <param name="type">Type of the mailing. Possible values: 'standard', 'recurring', 'absplit'.</param>
		/// <param name="encoding">Encoding to be used for the mailing. Possible values: 'utf-8', 'iso-8859-x'.</param>
		/// <param name="transferEncoding">Transfer encoding to be used for the mailing. Possible values: 'quoted-printable', 'base64'.</param>
		/// <param name="subject">Subject of the mailing.</param>
		/// <param name="senderEmail">Email address of the sender of the mailing.</param>
		/// <param name="senderName">Name of the sender of the mailing.</param>
		/// <param name="replyTo">Email address of the reply-to of the mailing.</param>
		/// <param name="htmlContent">HTML content of the mailing.</param>
		/// <param name="textContent">Text content of the mailing.</param>
		/// <param name="trackOpens">Indicates if opens in the HTML version are tracked.</param>
		/// <param name="trackClicksInHtml">Indicates if clicks in the HTML version are tracked.</param>
		/// <param name="trackClicksInText">Indicates if clicks in the text version are tracked.</param>
		/// <param name="trackingParameters">Additional tracking parameters for links.</param>
		/// <param name="endingOn">The date to end a 'recurring' mailing.</param>
		/// <param name="maxRecurrences">The number of recurrences for a 'recurring' mailing.</param>
		/// <param name="recurringConditions">The recurring conditions for a 'recurring' mailing.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>True if the mailing was updated.</returns>
		Task<bool> UpdateAsync(string userKey, long mailingId, long? campaignId = default, long? listId = default, long? sublistId = default, string name = null, MailingType? type = default, MessageEncoding? encoding = default, TransferEncoding? transferEncoding = default, string subject = null, string senderEmail = null, string senderName = null, string replyTo = null, string htmlContent = null, string textContent = null, bool? trackOpens = default, bool? trackClicksInHtml = default, bool? trackClicksInText = default, string trackingParameters = null, DateTime? endingOn = default, int? maxRecurrences = default, string recurringConditions = null, long? clientId = default, CancellationToken cancellationToken = default);
	}
}
