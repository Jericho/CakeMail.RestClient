﻿using CakeMail.RestClient.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	/// <summary>
	/// Allows you to manage lists.
	/// </summary>
	public interface ILists
	{
		/// <summary>
		/// Add a new field to a list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="name">Name of the field.</param>
		/// <param name="type">Type of the field. Possible values: 'text', 'integer', 'datetime' or 'mediumtext'.</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>True if the field was added to the list.</returns>
		Task<bool> AddFieldAsync(string userKey, long listId, string name, FieldType type, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Add a test email to the list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="email">The email address.</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>True if the email address was added.</returns>
		Task<bool> AddTestEmailAsync(string userKey, long listId, string email, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Create a list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="name">Name of the list.</param>
		/// <param name="defaultSenderName">Name of the default sender of the list.</param>
		/// <param name="defaultSenderEmailAddress">Email of the default sender of the list.</param>
		/// <param name="spamPolicyAccepted">Indicates if the anti-spam policy has been accepted.</param>
		/// <param name="clientId">Client ID of the client in which the list is created.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>ID of the new list.</returns>
		Task<long> CreateAsync(string userKey, string name, string defaultSenderName, string defaultSenderEmailAddress, bool spamPolicyAccepted = false, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Delete a list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>True if the list is deleted.</returns>
		Task<bool> DeleteAsync(string userKey, long listId, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Remove a field from a list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="name">Name of the field.</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>True if the field was removed from the list.</returns>
		Task<bool> DeleteFieldAsync(string userKey, long listId, string name, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Delete a member from the list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="listMemberId">ID of the member.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>True if the member was deleted.</returns>
		Task<bool> DeleteMemberAsync(string userKey, long listId, long listMemberId, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Delete a test email from a list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="email">The email address.</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>True if the email address was deleted.</returns>
		Task<bool> DeleteTestEmailAsync(string userKey, long listId, string email, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieve a list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="includeStatistics">True if you want the statistics.</param>
		/// <param name="calculateEngagement">True if you want the engagement information to be calculated.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The <see cref="List">list</see>.</returns>
		Task<List> GetAsync(string userKey, long listId, bool includeStatistics = true, bool calculateEngagement = false, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Get a count of lists matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the list status. Possible value 'active', 'archived'.</param>
		/// <param name="name">Filter using the list name.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The count of lists matching the filtering criteria.</returns>
		Task<long> GetCountAsync(string userKey, ListStatus? status = default, string name = null, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieve the list of fields of a list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>An enumeration of <see cref="ListField">fields</see>.</returns>
		Task<ListField[]> GetFieldsAsync(string userKey, long listId, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieve the lists matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the list status. Possible values: 'active', 'archived'.</param>
		/// <param name="name">Filter using the list name.</param>
		/// <param name="sortBy">Sort resulting lists. Possible values: 'name', 'created_on', 'active_members_count'.</param>
		/// <param name="sortDirection">Direction of the sorting. Possible values: 'asc', 'desc'.</param>
		/// <param name="limit">Limit the number of resulting lists.</param>
		/// <param name="offset">Offset the beginning of resulting lists.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>Enumeration of <see cref="List">lists</see> matching the filtering criteria.</returns>
		Task<List[]> GetListsAsync(string userKey, ListStatus? status = default, string name = null, ListsSortBy? sortBy = default, SortDirection? sortDirection = default, int? limit = 0, int? offset = 0, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieve the log items for a given list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="logType">Filter using the log action. Possible values: "subscribe", "in_queue", "opened", "clickthru", "forward", "unsubscribe", "view", "spam", "skipped".</param>
		/// <param name="uniques">Return unique log items per member.</param>
		/// <param name="totals">Return all the log items.</param>
		/// <param name="start">Filter using a start date.</param>
		/// <param name="end">Filter using an end date.</param>
		/// <param name="limit">Limit the number of resulting log items.</param>
		/// <param name="offset">Offset the beginning of resulting log items.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>An enumeration of <see cref="LogItem">log items</see> matching the filter criteria.</returns>
		Task<LogItem[]> GetLogsAsync(string userKey, long listId, LogType? logType = default, bool uniques = false, bool totals = false, DateTime? start = default, DateTime? end = default, int? limit = 0, int? offset = 0, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Get a count of log items matching the filter criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="logType">Filter using the log action. Possible values: "subscribe", "in_queue", "opened", "clickthru", "forward", "unsubscribe", "view", "spam", "skipped".</param>
		/// <param name="uniques">Return unique log items per member.</param>
		/// <param name="totals">Return all the log items.</param>
		/// <param name="start">Filter using a start date.</param>
		/// <param name="end">Filter using an end date.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The number of log items matching the filtering criteria.</returns>
		Task<long> GetLogsCountAsync(string userKey, long listId, LogType? logType = default, bool uniques = false, bool totals = false, DateTime? start = default, DateTime? end = default, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieve the information about a list member.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="listMemberId">ID of the member.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The <see cref=" ListMember">list mamber</see>.</returns>
		Task<ListMember> GetMemberAsync(string userKey, long listId, long listMemberId, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieve the list members matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="status">Filter using the member status. Possible values: 'active', 'unsubscribed', 'deleted', 'inactive_bounced', 'spam'.</param>
		/// <param name="query">Query to retrieve members from a segment.</param>
		/// <param name="sortBy">Sort resulting members. Possible values: 'id', 'email'.</param>
		/// <param name="sortDirection">Direction of the sorting. Possible values: 'asc', 'desc'.</param>
		/// <param name="limit">Limit the number of resulting members.</param>
		/// <param name="offset">Offset the beginning of resulting members.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>Enumeration of <see cref="List">lists</see> matching the filtering criteria.</returns>
		Task<ListMember[]> GetMembersAsync(string userKey, long listId, ListMemberStatus? status = default, string query = null, ListMembersSortBy? sortBy = default, SortDirection? sortDirection = default, int? limit = 0, int? offset = 0, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Get a count of list members matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="status">Filter using the member status. Possible values: 'active', 'unsubscribed', 'deleted', 'inactive_bounced', 'spam'.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>The number of list members matching the filtering criteria.</returns>
		Task<long> GetMembersCountAsync(string userKey, long listId, ListMemberStatus? status = default, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieve the lists of test email addresses for a given list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>Enumeration of <see cref="string">test email addresses</see>.</returns>
		Task<string[]> GetTestEmailsAsync(string userKey, long listId, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Add multiple subscribers to a list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="listMembers">Subscribers.</param>
		/// <param name="autoResponders">Trigger the autoresponders.</param>
		/// <param name="triggers">Trigger the welcome email.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>An enumeration of <see cref="ImportResult">results</see>.</returns>
		Task<ImportResult[]> ImportAsync(string userKey, long listId, IEnumerable<ListMember> listMembers, bool autoResponders = true, bool triggers = true, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Add a subscriber to a list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="email">Email address of the subscriber.</param>
		/// <param name="autoResponders">Trigger the autoresponders.</param>
		/// <param name="triggers">Trigger the welcome email.</param>
		/// <param name="customFields">Additional data for the subscriber.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>ID of the new subscriber.</returns>
		Task<long> SubscribeAsync(string userKey, long listId, string email, bool autoResponders = true, bool triggers = true, IDictionary<string, object> customFields = null, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Unsubscribe a member from the list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="email">Email address of the member.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>True if the member was unsubscribed.</returns>
		Task<bool> UnsubscribeAsync(string userKey, long listId, string email, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Unsubscribe a member from the list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="listMemberId">ID of the member.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>True if the member was unsubscribed.</returns>
		Task<bool> UnsubscribeAsync(string userKey, long listId, long listMemberId, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Update a list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="name">Name of the list.</param>
		/// <param name="language">Language of the list. e.g.: 'en_US' for English (US).</param>
		/// <param name="spamPolicyAccepted">Indicates if the anti-spam policy has been accepted.</param>
		/// <param name="status">Status of the list. Possible values: 'active', 'archived', 'deleted'.</param>
		/// <param name="senderName">Name of the default sender of the list.</param>
		/// <param name="senderEmail">Email of the default sender of the list.</param>
		/// <param name="goto_oi">Redirection URL on subscribing to the list.</param>
		/// <param name="goto_di">Redirection URL on confirming the subscription to the list.</param>
		/// <param name="goto_oo">Redirection URL on unsubscribing to the list.</param>
		/// <param name="webhook">Webhook URL for the list.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>True if the list was updated.</returns>
		Task<bool> UpdateAsync(string userKey, long listId, string name = null, string language = null, bool? spamPolicyAccepted = default, ListStatus? status = default, string senderName = null, string senderEmail = null, string goto_oi = null, string goto_di = null, string goto_oo = null, string webhook = null, long? clientId = default, CancellationToken cancellationToken = default);

		/// <summary>
		/// Update a list member.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="listMemberId">ID of the list member.</param>
		/// <param name="customFields">Additional data for the member.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>True if the member was updated.</returns>
		Task<bool> UpdateMemberAsync(string userKey, long listId, long listMemberId, IDictionary<string, object> customFields = null, long? clientId = default, CancellationToken cancellationToken = default);
	}
}
