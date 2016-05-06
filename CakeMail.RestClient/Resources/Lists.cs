using CakeMail.RestClient.Models;
using CakeMail.RestClient.Utilities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	public class Lists
	{
		#region Fields

		private readonly CakeMailRestClient _cakeMailRestClient;

		#endregion

		#region Constructor

		public Lists(CakeMailRestClient cakeMailRestClient)
		{
			_cakeMailRestClient = cakeMailRestClient;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Create a list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="name">Name of the list.</param>
		/// <param name="defaultSenderName">Name of the default sender of the list.</param>
		/// <param name="defaultSenderEmailAddress">Email of the default sender of the list.</param>
		/// <param name="spamPolicyAccepted">Indicates if the anti-spam policy has been accepted</param>
		/// <param name="clientId">Client ID of the client in which the list is created.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>ID of the new list</returns>
		public async Task<long> CreateAsync(string userKey, string name, string defaultSenderName, string defaultSenderEmailAddress, bool spamPolicyAccepted = false, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			string path = "/List/Create/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("name", name),
				new KeyValuePair<string, object>("sender_name", defaultSenderName),
				new KeyValuePair<string, object>("sender_email", defaultSenderEmailAddress)
			};
			if (spamPolicyAccepted) parameters.Add(new KeyValuePair<string, object>("list_policy", "accepted"));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<long>(path, parameters, null, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Delete a list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the list is deleted</returns>
		public async Task<bool> DeleteAsync(string userKey, long listId, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			string path = "/List/Delete/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<bool>(path, parameters, null, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Retrieve a list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list</param>
		/// <param name="includeStatistics">True if you want the statistics</param>
		/// <param name="calculateEngagement">True if you want the engagement information to be calculated</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The <see cref="List">list</see></returns>
		public async Task<List> GetAsync(string userKey, long listId, bool includeStatistics = true, bool calculateEngagement = false, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var path = "/List/GetInfo/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("no_details", includeStatistics ? "false" : "true"),	// CakeMail expects 'false' if you want to include details
				new KeyValuePair<string, object>("with_engagement", calculateEngagement ? "true" : "false")
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<List>(path, parameters, null, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Retrieve the lists matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the list status. Possible values: 'active', 'archived'</param>
		/// <param name="name">Filter using the list name.</param>
		/// <param name="sortBy">Sort resulting lists. Possible values: 'name', 'created_on', 'active_members_count'</param>
		/// <param name="sortDirection">Direction of the sorting. Possible values: 'asc', 'desc'</param>
		/// <param name="limit">Limit the number of resulting lists.</param>
		/// <param name="offset">Offset the beginning of resulting lists.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>Enumeration of <see cref="List">lists</see> matching the filtering criteria</returns>
		public async Task<IEnumerable<List>> GetListsAsync(string userKey, ListStatus? status = null, string name = null, ListsSortBy? sortBy = null, SortDirection? sortDirection = null, int? limit = 0, int? offset = 0, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var path = "/List/GetList/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "false")
			};
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (sortBy.HasValue) parameters.Add(new KeyValuePair<string, object>("sort_by", sortBy.Value.GetEnumMemberValue()));
			if (sortDirection.HasValue) parameters.Add(new KeyValuePair<string, object>("direction", sortDirection.Value.GetEnumMemberValue()));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteArrayRequestAsync<List>(path, parameters, "lists", cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Get a count of lists matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the list status. Possible value 'active', 'archived'</param>
		/// <param name="name">Filter using the list name.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The count of lists matching the filtering criteria</returns>
		public async Task<long> GetCountAsync(string userKey, ListStatus? status = null, string name = null, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var path = "/List/GetList/";
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "true")
			};
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteCountRequestAsync(path, parameters, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Update a list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list</param>
		/// <param name="name">Name of the list.</param>
		/// <param name="language">Language of the list. e.g.: 'en_US' for English (US)</param>
		/// <param name="spamPolicyAccepted">Indicates if the anti-spam policy has been accepted</param>
		/// <param name="status">Status of the list. Possible values: 'active', 'archived', 'deleted'</param>
		/// <param name="senderName">Name of the default sender of the list.</param>
		/// <param name="senderEmail">Email of the default sender of the list.</param>
		/// <param name="goto_oi">Redirection URL on subscribing to the list.</param>
		/// <param name="goto_di">Redirection URL on confirming the subscription to the list.</param>
		/// <param name="goto_oo">Redirection URL on unsubscribing to the list.</param>
		/// <param name="webhook">Webhook URL for the list.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the list was updated</returns>
		public async Task<bool> UpdateAsync(string userKey, long listId, string name = null, string language = null, bool? spamPolicyAccepted = null, ListStatus? status = null, string senderName = null, string senderEmail = null, string goto_oi = null, string goto_di = null, string goto_oo = null, string webhook = null, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			string path = "/List/SetInfo/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId)
			};
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (language != null) parameters.Add(new KeyValuePair<string, object>("language", language));
			if (spamPolicyAccepted.HasValue) parameters.Add(new KeyValuePair<string, object>("list_policy", spamPolicyAccepted.Value ? "accepted" : "declined"));
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (senderName != null) parameters.Add(new KeyValuePair<string, object>("sender_name", senderName));
			if (senderEmail != null) parameters.Add(new KeyValuePair<string, object>("sender_email", senderEmail));
			if (goto_oi != null) parameters.Add(new KeyValuePair<string, object>("goto_oi", goto_oi));
			if (goto_di != null) parameters.Add(new KeyValuePair<string, object>("goto_di", goto_di));
			if (goto_oo != null) parameters.Add(new KeyValuePair<string, object>("goto_oo", goto_oo));
			if (webhook != null) parameters.Add(new KeyValuePair<string, object>("webhook", webhook));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<bool>(path, parameters, null, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Add a new field to a list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="name">Name of the field.</param>
		/// <param name="type">Type of the field. Possible values: 'text', 'integer', 'datetime' or 'mediumtext'</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the field was added to the list</returns>
		public async Task<bool> AddFieldAsync(string userKey, long listId, string name, FieldType type, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			string path = "/List/EditStructure/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("action", "add"),
				new KeyValuePair<string, object>("field", name),
				new KeyValuePair<string, object>("type", type.GetEnumMemberValue())
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<bool>(path, parameters, null, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Remove a field from a list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="name">Name of the field.</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the field was removed from the list</returns>
		public async Task<bool> DeleteFieldAsync(string userKey, long listId, string name, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			string path = "/List/EditStructure/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("action", "delete"),
				new KeyValuePair<string, object>("field", name)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<bool>(path, parameters, null, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Retrieve the list of fields of a list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of <see cref="ListField">fields</see></returns>
		public async Task<IEnumerable<ListField>> GetFieldsAsync(string userKey, long listId, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var path = "/List/GetFields/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			var fieldsStructure = await _cakeMailRestClient.ExecuteRequestAsync<ExpandoObject>(path, parameters, null, cancellationToken).ConfigureAwait(false);
			if (fieldsStructure == null) return Enumerable.Empty<ListField>();

			var fields = fieldsStructure.Select(x => new ListField { Name = x.Key, Type = x.Value.ToString().GetValueFromEnumMember<FieldType>() });
			return fields;
		}

		/// <summary>
		/// Add a test email to the list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="email">The email address</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the email address was added</returns>
		public async Task<bool> AddTestEmailAsync(string userKey, long listId, string email, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			string path = "/List/AddTestEmail/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("email", email)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<bool>(path, parameters, null, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Delete a test email from a list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="email">The email address</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the email address was deleted</returns>
		public async Task<bool> DeleteTestEmailAsync(string userKey, long listId, string email, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			string path = "/List/DeleteTestEmail/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("email", email)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<bool>(path, parameters, null, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Retrieve the lists of test email addresses for a given list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>Enumeration of <see cref="string">test email addresses</see></returns>
		public async Task<IEnumerable<string>> GetTestEmailsAsync(string userKey, long listId, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var path = "/List/GetTestEmails/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteArrayRequestAsync<string>(path, parameters, "testemails", cancellationToken).ConfigureAwait(false);
		}

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
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>ID of the new subscriber</returns>
		public async Task<long> SubscribeAsync(string userKey, long listId, string email, bool autoResponders = true, bool triggers = true, IEnumerable<KeyValuePair<string, object>> customFields = null, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			string path = "/List/SubscribeEmail/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("autoresponders", autoResponders ? "true" : "false"),
				new KeyValuePair<string, object>("triggers", triggers ? "true" : "false"),
				new KeyValuePair<string, object>("email", email)
			};
			if (customFields != null)
			{
				foreach (var customField in customFields)
				{
					if (customField.Value is DateTime) parameters.Add(new KeyValuePair<string, object>(string.Format("data[{0}]", customField.Key), ((DateTime)customField.Value).ToCakeMailString()));
					else parameters.Add(new KeyValuePair<string, object>(string.Format("data[{0}]", customField.Key), customField.Value));
				}
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<long>(path, parameters, null, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Add multiple subscribers to a list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="autoResponders">Trigger the autoresponders.</param>
		/// <param name="triggers">Trigger the welcome email.</param>
		/// <param name="listMembers">Subscribers.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of <see cref="ImportResult">results</see></returns>
		public async Task<IEnumerable<ImportResult>> ImportAsync(string userKey, long listId, IEnumerable<ListMember> listMembers, bool autoResponders = true, bool triggers = true, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			string path = "/List/Import/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("import_to", "active"),
				new KeyValuePair<string, object>("autoresponders", autoResponders ? "true" : "false"),
				new KeyValuePair<string, object>("triggers", triggers ? "true" : "false")
			};
			if (listMembers != null)
			{
				foreach (var item in listMembers.Select((member, i) => new { Index = i, Email = member.Email, CustomFields = member.CustomFields }))
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("record[{0}][email]", item.Index), item.Email));
					if (item.CustomFields != null)
					{
						foreach (var customField in item.CustomFields)
						{
							if (customField.Value is DateTime) parameters.Add(new KeyValuePair<string, object>(string.Format("record[{0}][{1}]", item.Index, customField.Key), ((DateTime)customField.Value).ToCakeMailString()));
							else parameters.Add(new KeyValuePair<string, object>(string.Format("record[{0}][{1}]", item.Index, customField.Key), customField.Value));
						}
					}
				}
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteArrayRequestAsync<ImportResult>(path, parameters, null, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Unsubscribe a member from the list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="email">Email address of the member.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the member was unsubscribed</returns>
		public async Task<bool> UnsubscribeAsync(string userKey, long listId, string email, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			string path = "/List/UnsubscribeEmail/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("email", email)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<bool>(path, parameters, null, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Unsubscribe a member from the list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="listMemberId">ID of the member.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the member was unsubscribed</returns>
		public async Task<bool> UnsubscribeAsync(string userKey, long listId, long listMemberId, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			string path = "/List/UnsubscribeEmail/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("record_id", listMemberId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<bool>(path, parameters, null, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Delete a member from the list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="listMemberId">ID of the member.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the member was deleted</returns>
		public async Task<bool> DeleteMemberAsync(string userKey, long listId, long listMemberId, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			string path = "/List/DeleteRecord/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("record_id", listMemberId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<bool>(path, parameters, null, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Retrieve the information about a list member
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="listMemberId">ID of the member.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The <see cref=" ListMember">list mamber</see></returns>
		public async Task<ListMember> GetMemberAsync(string userKey, long listId, long listMemberId, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var path = "/List/GetRecord/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("record_id", listMemberId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			var listMember = await _cakeMailRestClient.ExecuteRequestAsync<ListMember>(path, parameters, null, cancellationToken).ConfigureAwait(false);
			return listMember;
		}

		/// <summary>
		/// Retrieve the list members matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="status">Filter using the member status. Possible values: 'active', 'unsubscribed', 'deleted', 'inactive_bounced', 'spam'</param>
		/// <param name="query">Query to retrieve members from a segment.</param>
		/// <param name="sortBy">Sort resulting members. Possible values: 'id', 'email'</param>
		/// <param name="sortDirection">Direction of the sorting. Possible values: 'asc', 'desc'</param>
		/// <param name="limit">Limit the number of resulting members.</param>
		/// <param name="offset">Offset the beginning of resulting members.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>Enumeration of <see cref="List">lists</see> matching the filtering criteria</returns>
		public async Task<IEnumerable<ListMember>> GetMembersAsync(string userKey, long listId, ListMemberStatus? status = null, string query = null, ListMembersSortBy? sortBy = null, SortDirection? sortDirection = null, int? limit = 0, int? offset = 0, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var path = "/List/Show/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("count", "false")
			};
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (query != null) parameters.Add(new KeyValuePair<string, object>("query", query));
			if (sortBy.HasValue) parameters.Add(new KeyValuePair<string, object>("sort_by", sortBy.Value.GetEnumMemberValue()));
			if (sortDirection.HasValue) parameters.Add(new KeyValuePair<string, object>("direction", sortDirection.Value.GetEnumMemberValue()));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteArrayRequestAsync<ListMember>(path, parameters, "records", cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Get a count of list members matching the filtering criteria
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="status">Filter using the member status. Possible values: 'active', 'unsubscribed', 'deleted', 'inactive_bounced', 'spam'</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The number of list members matching the filtering criteria</returns>
		public async Task<long> GetMembersCountAsync(string userKey, long listId, ListMemberStatus? status = null, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var path = "/List/Show/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("count", "true")
			};
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteCountRequestAsync(path, parameters, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Update a list member
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="listMemberId">ID of the list member</param>
		/// <param name="customFields">Additional data for the member</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the member was updated</returns>
		public async Task<bool> UpdateMemberAsync(string userKey, long listId, long listMemberId, IEnumerable<KeyValuePair<string, object>> customFields = null, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			string path = "/List/UpdateRecord/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("record_id", listMemberId),
			};
			if (customFields != null)
			{
				foreach (var customField in customFields)
				{
					if (customField.Value is DateTime) parameters.Add(new KeyValuePair<string, object>(string.Format("data[{0}]", customField.Key), ((DateTime)customField.Value).ToCakeMailString()));
					else parameters.Add(new KeyValuePair<string, object>(string.Format("data[{0}]", customField.Key), customField.Value));
				}
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteRequestAsync<bool>(path, parameters, null, cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Retrieve the log items for a given list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="logType">Filter using the log action. Possible values: "subscribe", "in_queue", "opened", "clickthru", "forward", "unsubscribe", "view", "spam", "skipped"</param>
		/// <param name="uniques">Return unique log items per member</param>
		/// <param name="totals">Return all the log items</param>
		/// <param name="start">Filter using a start date</param>
		/// <param name="end">Filter using an end date</param>
		/// <param name="limit">Limit the number of resulting log items.</param>
		/// <param name="offset">Offset the beginning of resulting log items.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of <see cref="LogItem">log items</see> matching the filter criteria</returns>
		public async Task<IEnumerable<LogItem>> GetLogsAsync(string userKey, long listId, LogType? logType = null, bool uniques = false, bool totals = false, DateTime? start = null, DateTime? end = null, int? limit = 0, int? offset = 0, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			string path = "/List/GetLog/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("totals", totals ? "true" : "false"),
				new KeyValuePair<string, object>("uniques", uniques ? "true" : "false"),
				new KeyValuePair<string, object>("count", "false")
			};
			if (logType.HasValue) parameters.Add(new KeyValuePair<string, object>("action", logType.Value.GetEnumMemberValue()));
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_time", start.Value.ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_time", end.Value.ToCakeMailString()));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteArrayRequestAsync<LogItem>(path, parameters, "logs", cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		/// Get a count of log items matching the filter criteria
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="logType">Filter using the log action. Possible values: "subscribe", "in_queue", "opened", "clickthru", "forward", "unsubscribe", "view", "spam", "skipped"</param>
		/// <param name="uniques">Return unique log items per member</param>
		/// <param name="totals">Return all the log items</param>
		/// <param name="start">Filter using a start date</param>
		/// <param name="end">Filter using an end date</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The number of log items matching the filtering criteria</returns>
		public async Task<long> GetLogsCountAsync(string userKey, long listId, LogType? logType = null, bool uniques = false, bool totals = false, DateTime? start = null, DateTime? end = null, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			string path = "/List/GetLog/";

			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("list_id", listId),
				new KeyValuePair<string, object>("totals", totals ? "true" : "false"),
				new KeyValuePair<string, object>("uniques", uniques ? "true" : "false"),
				new KeyValuePair<string, object>("count", "true")
			};
			if (logType.HasValue) parameters.Add(new KeyValuePair<string, object>("action", logType.Value.GetEnumMemberValue()));
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_time", start.Value.ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_time", end.Value.ToCakeMailString()));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return await _cakeMailRestClient.ExecuteCountRequestAsync(path, parameters, cancellationToken).ConfigureAwait(false);
		}

		#endregion
	}
}
