using CakeMail.RestClient.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	public interface ITriggers
	{
		Task<long> CreateAsync(string userKey, string name, long listId, long? campaignId = default(long?), MessageEncoding? encoding = default(MessageEncoding?), TransferEncoding? transferEncoding = default(TransferEncoding?), long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<Trigger> GetAsync(string userKey, long triggerId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<long> GetCountAsync(string userKey, TriggerStatus? status = default(TriggerStatus?), TriggerAction? action = default(TriggerAction?), long? listId = default(long?), long? campaignId = default(long?), long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<Link> GetLinkAsync(string userKey, long linkId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<Link[]> GetLinksAsync(string userKey, long triggerId, int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<long> GetLinksCountAsync(string userKey, long triggerId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<LinkStats[]> GetLinksWithStatsAsync(string userKey, long triggerId, DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<long> GetLinksWithStatsCountAsync(string userKey, long triggerId, DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<LogItem[]> GetLogsAsync(string userKey, long triggerId, LogType? logType = default(LogType?), long? listMemberId = default(long?), bool uniques = false, bool totals = false, DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<long> GetLogsCountAsync(string userKey, long triggerId, LogType? logType = default(LogType?), long? listMemberId = default(long?), bool uniques = false, bool totals = false, DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<RawEmailMessage> GetRawEmailMessageAsync(string userKey, long triggerId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<string> GetRawHtmlAsync(string userKey, long triggerId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<string> GetRawTextAsync(string userKey, long triggerId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<Trigger[]> GetTriggersAsync(string userKey, TriggerStatus? status = default(TriggerStatus?), TriggerAction? action = default(TriggerAction?), long? listId = default(long?), long? campaignId = default(long?), int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> SendTestEmailAsync(string userKey, long triggerId, string recipientEmail, bool separated = false, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> UnleashAsync(string userKey, long triggerId, long listMemberId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> UpdateAsync(string userKey, long triggerId, long? campaignId = default(long?), string name = null, TriggerAction? action = default(TriggerAction?), MessageEncoding? encoding = default(MessageEncoding?), TransferEncoding? transferEncoding = default(TransferEncoding?), string subject = null, string senderEmail = null, string senderName = null, string replyTo = null, string htmlContent = null, string textContent = null, bool? trackOpens = default(bool?), bool? trackClicksInHtml = default(bool?), bool? trackClicksInText = default(bool?), string trackingParameters = null, int? delay = default(int?), TriggerStatus? status = default(TriggerStatus?), DateTime? date = default(DateTime?), long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
	}
}
