using CakeMail.RestClient.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	public interface IMailings
	{
		Task<long> CreateAsync(string userKey, string name, long? campaignId = default(long?), MailingType? type = MailingType.Standard, long? recurringId = default(long?), MessageEncoding? encoding = default(MessageEncoding?), TransferEncoding? transferEncoding = default(TransferEncoding?), long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> DeleteAsync(string userKey, long mailingId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<Mailing> GetAsync(string userKey, long mailingId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<long> GetCountAsync(string userKey, MailingStatus? status = default(MailingStatus?), MailingType? type = default(MailingType?), string name = null, long? listId = default(long?), long? campaignId = default(long?), long? recurringId = default(long?), DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<Link> GetLinkAsync(string userKey, long linkId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<Link[]> GetLinksAsync(string userKey, long mailingId, int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<long> GetLinksCountAsync(string userKey, long mailingId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<LinkStats[]> GetLinksWithStatsAsync(string userKey, long mailingId, DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<long> GetLinksWithStatsCountAsync(string userKey, long mailingId, DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<LogItem[]> GetLogsAsync(string userKey, long mailingId, LogType? logType = default(LogType?), long? listMemberId = default(long?), bool uniques = false, bool totals = false, DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<long> GetLogsCountAsync(string userKey, long mailingId, LogType? logType = default(LogType?), long? listMemberId = default(long?), bool uniques = false, bool totals = false, DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<Mailing[]> GetMailingsAsync(string userKey, MailingStatus? status = default(MailingStatus?), MailingType? type = default(MailingType?), string name = null, long? listId = default(long?), long? campaignId = default(long?), long? recurringId = default(long?), DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), MailingsSortBy? sortBy = default(MailingsSortBy?), SortDirection? sortDirection = default(SortDirection?), int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<RawEmailMessage> GetRawEmailMessageAsync(string userKey, long mailingId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<string> GetRawHtmlAsync(string userKey, long mailingId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<string> GetRawTextAsync(string userKey, long mailingId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> ResumeAsync(string userKey, long mailingId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> ScheduleAsync(string userKey, long mailingId, DateTime? date = default(DateTime?), long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> SendTestEmailAsync(string userKey, long mailingId, string recipientEmail, bool separated = false, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> SuspendAsync(string userKey, long mailingId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> UnscheduleAsync(string userKey, long mailingId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> UpdateAsync(string userKey, long mailingId, long? campaignId = default(long?), long? listId = default(long?), long? sublistId = default(long?), string name = null, MailingType? type = default(MailingType?), MessageEncoding? encoding = default(MessageEncoding?), TransferEncoding? transferEncoding = default(TransferEncoding?), string subject = null, string senderEmail = null, string senderName = null, string replyTo = null, string htmlContent = null, string textContent = null, bool? trackOpens = default(bool?), bool? trackClicksInHtml = default(bool?), bool? trackClicksInText = default(bool?), string trackingParameters = null, DateTime? endingOn = default(DateTime?), int? maxRecurrences = default(int?), string recurringConditions = null, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
	}
}
