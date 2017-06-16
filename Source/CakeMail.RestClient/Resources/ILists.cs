using CakeMail.RestClient.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	public interface ILists
	{
		Task<bool> AddFieldAsync(string userKey, long listId, string name, FieldType type, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> AddTestEmailAsync(string userKey, long listId, string email, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<long> CreateAsync(string userKey, string name, string defaultSenderName, string defaultSenderEmailAddress, bool spamPolicyAccepted = false, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> DeleteAsync(string userKey, long listId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> DeleteFieldAsync(string userKey, long listId, string name, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> DeleteMemberAsync(string userKey, long listId, long listMemberId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> DeleteTestEmailAsync(string userKey, long listId, string email, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<List> GetAsync(string userKey, long listId, bool includeStatistics = true, bool calculateEngagement = false, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<long> GetCountAsync(string userKey, ListStatus? status = default(ListStatus?), string name = null, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<ListField[]> GetFieldsAsync(string userKey, long listId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<List[]> GetListsAsync(string userKey, ListStatus? status = default(ListStatus?), string name = null, ListsSortBy? sortBy = default(ListsSortBy?), SortDirection? sortDirection = default(SortDirection?), int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<LogItem[]> GetLogsAsync(string userKey, long listId, LogType? logType = default(LogType?), bool uniques = false, bool totals = false, DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<long> GetLogsCountAsync(string userKey, long listId, LogType? logType = default(LogType?), bool uniques = false, bool totals = false, DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<ListMember> GetMemberAsync(string userKey, long listId, long listMemberId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<ListMember[]> GetMembersAsync(string userKey, long listId, ListMemberStatus? status = default(ListMemberStatus?), string query = null, ListMembersSortBy? sortBy = default(ListMembersSortBy?), SortDirection? sortDirection = default(SortDirection?), int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<long> GetMembersCountAsync(string userKey, long listId, ListMemberStatus? status = default(ListMemberStatus?), long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<string[]> GetTestEmailsAsync(string userKey, long listId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<ImportResult[]> ImportAsync(string userKey, long listId, IEnumerable<ListMember> listMembers, bool autoResponders = true, bool triggers = true, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<long> SubscribeAsync(string userKey, long listId, string email, bool autoResponders = true, bool triggers = true, IEnumerable<KeyValuePair<string, object>> customFields = null, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> UnsubscribeAsync(string userKey, long listId, string email, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> UnsubscribeAsync(string userKey, long listId, long listMemberId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> UpdateAsync(string userKey, long listId, string name = null, string language = null, bool? spamPolicyAccepted = default(bool?), ListStatus? status = default(ListStatus?), string senderName = null, string senderEmail = null, string goto_oi = null, string goto_di = null, string goto_oo = null, string webhook = null, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> UpdateMemberAsync(string userKey, long listId, long listMemberId, IEnumerable<KeyValuePair<string, object>> customFields = null, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
	}
}
