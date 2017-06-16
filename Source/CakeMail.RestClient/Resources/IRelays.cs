using CakeMail.RestClient.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	public interface IRelays
	{
		Task<RelayBounceLog[]> GetBounceLogsAsync(string userKey, long? trackingId = default(long?), DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<RelayClickLog[]> GetClickLogsAsync(string userKey, long? trackingId = default(long?), DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<RelayOpenLog[]> GetOpenLogsAsync(string userKey, long? trackingId = default(long?), DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<RelayLog[]> GetSentLogsAsync(string userKey, long? trackingId = default(long?), DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> SendWithoutTrackingAsync(string userKey, string recipientEmailAddress, string subject, string html, string text, string senderEmail, string senderName = null, IDictionary<string, object> mergeData = null, MessageEncoding? encoding = default(MessageEncoding?), long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> SendWithTrackingAsync(string userKey, long trackingId, string recipientEmailAddress, string subject, string html, string text, string senderEmail, string senderName = null, IDictionary<string, object> mergeData = null, MessageEncoding? encoding = default(MessageEncoding?), long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
	}
}
