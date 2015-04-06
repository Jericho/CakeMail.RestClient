using CakeMail.RestClient.Models;
using System;
using System.Collections.Generic;

namespace CakeMail.RestClient
{
	public interface ICakeMailRestClient
	{
		#region Methods related to CAMPAIGNS

		/// <summary>
		/// Create a new campaign
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="name">Name of the campaign.</param>
		/// <param name="clientId">Client ID of the client in which the campaign is created.</param>
		/// <returns>ID of the new campaign</returns>
		int CreateCampaign(string userKey, string name, int? clientId = null);

		/// <summary>
		/// Delete a campaign.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="campaignId">ID of the campaign to delete.</param>
		/// <param name="clientId">Client ID of the client in which the campaign is located.</param>
		/// <returns>True if the campaign is deleted</returns>
		bool DeleteCampaign(string userKey, int campaignId, int? clientId = null);

		/// <summary>
		/// Retrieve a campaign.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="campaignId">ID of the campaign.</param>
		/// <param name="clientId">Client ID of the client in which the campaign is located.</param>
		/// <returns>The <see cref="Campaign">campaign</see></returns>
		Campaign GetCampaign(string userKey, int campaignId, int? clientId = null);

		/// <summary>
		/// Get a list of campaigns matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the campaign status. Possible value 'ongoing', 'closed'</param>
		/// <param name="name">Filter using the campaign name.</param>
		/// <param name="sortBy">Sort resulting campaigns. Possible value 'created_on', 'name'</param>
		/// <param name="sortDirection">Direction of the sorting. Possible value 'asc', 'desc'</param>
		/// <param name="limit">Limit the number of resulting campaigns.</param>
		/// <param name="offset">Offset the beginning of resulting campaigns.</param>
		/// <param name="clientId">Client ID of the client in which the campaign is located.</param>
		/// <returns>Enumeration of <see cref="Campaign">campaigns</see> matching the filtering criteria</returns>
		IEnumerable<Campaign> GetCampaigns(string userKey, string status = null, string name = null, string sortBy = null, string sortDirection = null, int limit = 0, int offset = 0, int? clientId = null);

		/// <summary>
		/// Get a count of campaigns matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the campaign status. Possible value 'ongoing', 'closed'</param>
		/// <param name="name">Filter using the campaign name.</param>
		/// <param name="clientId">Client ID of the client in which the campaign is located.</param>
		/// <returns>The count of campaigns matching the filtering criteria</returns>
		long GetCampaignsCount(string userKey, string status = null, string name = null, int? clientId = null);

		/// <summary>
		/// Update a campaign
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="campaignId">ID of the campaign.</param>
		/// <param name="name">The name of the campaign</param>
		/// <param name="clientId">Client ID of the client in which the campaign is located.</param>
		/// <returns>True if the record was updated.</returns>
		bool UpdateCampaign(string userKey, int campaignId, string name, int? clientId = null);

		#endregion

		#region Methods related to CLIENTS

		/// <summary>
		/// Create a client
		/// </summary>
		/// <param name="parentId">ID of the parent client.</param>
		/// <param name="name">Name of the client</param>
		/// <param name="address1">Address of the client</param>
		/// <param name="address2">Address of the client</param>
		/// <param name="city">City of the client</param>
		/// <param name="provinceId">ID of the province of the client</param>
		/// <param name="postalCode">Postal Code of the client</param>
		/// <param name="countryId">ID or the country of the client</param>
		/// <param name="website">Website URL of the client</param>
		/// <param name="phone">Phone number of the client</param>
		/// <param name="fax">Fax number of the client</param>
		/// <param name="adminEmail">Email address of the admin user</param>
		/// <param name="adminFirstName">First name of the admin user</param>
		/// <param name="adminLastName">Last name of the admin user</param>
		/// <param name="adminTitle">Title of the admin user</param>
		/// <param name="adminOfficePhone">Office phone of the admin user</param>
		/// <param name="adminMobilePhone">Mobile phone of the admin user</param>
		/// <param name="adminLanguage">Language of the admin user. e.g.: 'en-US' for English (US)</param>
		/// <param name="adminTimezoneId">ID of the timezone of the admin user</param>
		/// <param name="adminPassword">Password of the admin user</param>
		/// <param name="primaryContactSameAsAdmin">Is the primary contact the same person as the admin user?</param>
		/// <param name="primaryContactEmail">Email address of the primary contact</param>
		/// <param name="primaryContactFirstName">First name of the primary contact</param>
		/// <param name="primaryContactLastName">Last name of the primary contact</param>
		/// <param name="primaryContactTitle">Title of the primary contact</param>
		/// <param name="primaryContactOfficePhone">Office phone of the primary contact</param>
		/// <param name="primaryContactMobilePhone">Mobile phone of the primary contact</param>
		/// <param name="primaryContactLanguage">Language of the primary contact. e.g.: 'en-US' for English (US)</param>
		/// <param name="primaryContactTimezoneId">ID of the timezone of the primary contact</param>
		/// <param name="primaryContactPassword">Password of the primary contact</param>
		/// <returns>A confirmation code which must be used subsequently to 'activate' the client</returns>
		string CreateClient(int parentId, string name, string address1 = null, string address2 = null, string city = null, string provinceId = null, string postalCode = null, string countryId = null, string website = null, string phone = null, string fax = null, string adminEmail = null, string adminFirstName = null, string adminLastName = null, string adminTitle = null, string adminOfficePhone = null, string adminMobilePhone = null, string adminLanguage = null, int? adminTimezoneId = null, string adminPassword = null, bool primaryContactSameAsAdmin = true, string primaryContactEmail = null, string primaryContactFirstName = null, string primaryContactLastName = null, string primaryContactTitle = null, string primaryContactOfficePhone = null, string primaryContactMobilePhone = null, string primaryContactLanguage = null, int? primaryContactTimezoneId = null, string primaryContactPassword = null);

		/// <summary>
		/// Activate a pending client
		/// </summary>
		/// <param name="confirmationCode">Confirmation code returned by the Create method.</param>
		/// <returns><see cref="ClientRegistrationInfo">Information</see> about the activated client</returns>
		ClientRegistrationInfo ConfirmClient(string confirmationCode);

		/// <summary>
		/// Retrieve a client
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="startDate">Start date to return stats about the client.</param>
		/// <param name="endDate">End date to return stats about the client.</param>
		/// <returns>The <see cref="Client">client</see></returns>
		Client GetClient(string userKey, int clientId, DateTime? startDate = null, DateTime? endDate = null);

		/// <summary>
		/// Retrieve a pending client
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="confirmationCode">Confirmation code to get the information of a pending client.</param>
		/// <returns>The <see cref="Client">client</see></returns>
		/// <remarks>Pending clients must be activated before they can start using the CakeMail service.</remarks>
		UnConfirmedClient GetClient(string userKey, string confirmationCode);

		/// <summary>
		/// Get a list of clients matching the filtering criteria
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the client status. Possible values: 'all', 'pending', 'trial', 'active', 'suspended_all'</param>
		/// <param name="name">Filter using the client name.</param>
		/// <param name="sortBy">Sort resulting campaigns. Possible values: 'company_name', 'registered_date', 'mailing_limit', 'month_limit', 'contact_limit', 'last_activity'</param>
		/// <param name="sortDirection">Direction of the sorting. Possible value 'asc', 'desc'</param>
		/// <param name="limit">Limit the number of resulting clients.</param>
		/// <param name="offset">Offset the beginning of resulting clients.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>Enumeration of <see cref="Client">clients</see> matching the filtering criteria</returns>
		IEnumerable<Client> GetClients(string userKey, string status = null, string name = null, string sortBy = null, string sortDirection = null, int limit = 0, int offset = 0, int? clientId = null);

		/// <summary>
		/// Get a count of clients matching the filtering criteria
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the client status. Possible values: 'all', 'pending', 'trial', 'active', 'suspended_all'</param>
		/// <param name="name">Filter using the client name.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>The number of clients matching the filtering criteria</returns>
		long GetClientsCount(string userKey, string status = null, string name = null, int? clientId = null);

		/// <summary>
		/// Update a client
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <param name="name">Name of the client</param>
		/// <param name="status">Status of the client. Possible values: 'trial', 'active', 'suspended_by_reseller', 'deleted'</param>
		/// <param name="parentId"></param>
		/// <param name="name">Name of the client</param>
		/// <param name="address1">Address of the client</param>
		/// <param name="address2">Address of the client</param>
		/// <param name="city">City of the client</param>
		/// <param name="provinceId">ID of the province of the client</param>
		/// <param name="postalCode">Postal Code of the client</param>
		/// <param name="countryId">ID or the country of the client</param>
		/// <param name="website">Website URL of the client</param>
		/// <param name="phone">Phone number of the client</param>
		/// <param name="fax">Fax number of the client</param>
		/// <param name="authDomain"></param>
		/// <param name="bounceDomain"></param>
		/// <param name="dkimDomain"></param>
		/// <param name="doptinIp"></param>
		/// <param name="forwardDomain"></param>
		/// <param name="forwardIp"></param>
		/// <param name="ipPool"></param>
		/// <param name="mdDomain"></param>
		/// <param name="isReseller">Is the client a reseller?</param>
		/// <param name="currency"></param>
		/// <param name="planType"></param>
		/// <param name="mailingLimit">Per campaign limit of the client.</param>
		/// <param name="monthLimit">Monthly emails sent limit of the client.</param>
		/// <param name="contactLimit">Number of contacts limit of the client.</param>
		/// <param name="defaultMailingLimit"></param>
		/// <param name="defaultMonthLimit"></param>
		/// <param name="defaultContactLimit"></param>
		/// <returns>True if the record was updated.</returns>
		bool UpdateClient(string userKey, int clientId, string name = null, string status = null, int? parentId = null, string address1 = null, string address2 = null, string city = null, string provinceId = null, string postalCode = null, string countryId = null, string website = null, string phone = null, string fax = null, string authDomain = null, string bounceDomain = null, string dkimDomain = null, string doptinIp = null, string forwardDomain = null, string forwardIp = null, string ipPool = null, string mdDomain = null, bool? isReseller = null, string currency = null, string planType = null, int? mailingLimit = null, int? monthLimit = null, int? contactLimit = null, int? defaultMailingLimit = null, int? defaultMonthLimit = null, int? defaultContactLimit = null);

		/// <summary>
		/// Activate a client which has been previously suspended
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>True if the client was activated</returns>
		/// <remarks>This method is simply a shortcut for: UpdateClient(userKey, clientId, status: "active")</remarks>
		bool ActivateClient(string userKey, int clientId);

		/// <summary>
		/// Suspend a client
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>True if the client was suspended</returns>
		/// <remarks>This method is simply a shortcut for: UpdateClient(userKey, clientId, status: "suspended_by_reseller")</remarks>
		bool SuspendClient(string userKey, int clientId);

		/// <summary>
		/// Delete a client
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>True if the client was deleted</returns>
		/// <remarks>This method is simply a shortcut for: UpdateClient(userKey, clientId, status: "deleted")</remarks>
		bool DeleteClient(string userKey, int clientId);

		#endregion

		#region Methods related to PERMISSIONS

		/// <summary>
		/// Retrieve the list of permissions for a given user
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="userId">ID of the user.</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>An enumeration of permissions</returns>
		IEnumerable<string> GetUserPermissions(string userKey, int userId, int? clientId = null);

		/// <summary>
		/// Set the permissions granted to a given user
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="userId">ID of the user.</param>
		/// <param name="permissions">Enumeration of permissions</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>True if the operation succeeded</returns>
		bool SetUserPermissions(string userKey, int userId, IEnumerable<string> permissions, int? clientId = null);

		#endregion

		#region Methods related to COUNTRIES

		/// <summary>
		/// Get the list of countries
		/// </summary>
		/// <returns>An enumeration of <see cref="Country">countries</see></returns>
		IEnumerable<Country> GetCountries();

		/// <summary>
		/// Get the list of state/provinces for a given country
		/// </summary>
		/// <param name="countryId">ID of the country.</param>
		/// <returns>An enumeration of <see cref="Province">privinces</see></returns>
		IEnumerable<Province> GetProvinces(string countryId);

		#endregion

		#region Methods related to LISTS

		/// <summary>
		/// Create a list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="name">Name of the list.</param>
		/// <param name="defaultSenderName">Name of the default sender of the list.</param>
		/// <param name="defaultSenderEmailAddress">Email of the default sender of the list.</param>
		/// <param name="spamPolicyAccepted">Indicates if the anti-spam policy has been accepted</param>
		/// <param name="clientId">Client ID of the client in which the list is created.</param>
		/// <returns>ID of the new list</returns>
		int CreateList(string userKey, string name, string defaultSenderName, string defaultSenderEmailAddress, bool spamPolicyAccepted = false, int? clientId = null);

		/// <summary>
		/// Delete a list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>True if the list is deleted</returns>
		bool DeleteList(string userKey, int listId, int? clientId = null);

		/// <summary>
		/// Retrieve a list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list</param>
		/// <param name="subListId">ID of the segment</param>
		/// <param name="includeStatistics">True if you want the statistics</param>
		/// <param name="calculateEngagement">True if you want the engagement information to be calculated</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>The <see cref="List">list</see></returns>
		List GetList(string userKey, int listId, int? subListId = null, bool includeStatistics = true, bool calculateEngagement = false, int? clientId = null);

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
		/// <returns>Enumeration of <see cref="List">lists</see> matching the filtering criteria</returns>
		IEnumerable<List> GetLists(string userKey, string status = null, string name = null, string sortBy = null, string sortDirection = null, int limit = 0, int offset = 0, int? clientId = null);

		/// <summary>
		/// Get a count of lists matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the list status. Possible value 'active', 'archived'</param>
		/// <param name="name">Filter using the list name.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>The count of lists matching the filtering criteria</returns>
		long GetListsCount(string userKey, string name = null, int? clientId = null);

		/// <summary>
		/// Update a list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list</param>
		/// <param name="name">Name of the list or name of the segment if sublist_id is provided.</param>
		/// <param name="language">Language of the list. e.g.: 'en_US' for English (US)</param>
		/// <param name="spamPolicyAccepted">Indicates if the anti-spam policy has been accepted</param>
		/// <param name="status">Status of the list. Possible values: 'active', 'archived', 'deleted'</param>
		/// <param name="senderName">Name of the default sender of the list.</param>
		/// <param name="senderEmail">Email of the default sender of the list.</param>
		/// <param name="goto_oi">Redirection URL on subscribing to the list.</param>
		/// <param name="goto_di">Redirection URL on confirming the subscription to the list.</param>
		/// <param name="goto_oo">Redirection URL on unsubscribing to the list.</param>
		/// <param name="webhook">Webhook URL for the list.</param>
		/// <param name="query">Rules for the segment.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>True if the list was updated</returns>
		bool UpdateList(string userKey, int listId, string name = null, string language = null, bool? spamPolicyAccepted = null, string status = null, string senderName = null, string senderEmail = null, string goto_oi = null, string goto_di = null, string goto_oo = null, string webhook = null, int? clientId = null);

		/// <summary>
		/// Add a new field to a list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="name">Name of the field.</param>
		/// <param name="type">Type of the field. Possible values: 'text', 'integer', 'datetime' or 'mediumtext'</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <returns>True if the field was added to the list</returns>
		bool AddListField(string userKey, int listId, string name, string type, int? clientId = null);

		/// <summary>
		/// Remove a field from a list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="name">Name of the field.</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <returns>True if the field was removed from the list</returns>
		bool DeleteListField(string userKey, int listId, string name, int? clientId = null);

		/// <summary>
		/// Retrieve the list of fields of a list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <returns>An enumeration of <see cref="ListField">fields</see></returns>
		IEnumerable<ListField> GetListFields(string userKey, int listId, int? clientId = null);

		/// <summary>
		/// Add a test email to the list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="email">The email address</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <returns>True if the email address was added</returns>
		bool AddTestEmail(string userKey, int listId, string email, int? clientId = null);

		/// <summary>
		/// Delete a test email from a list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="email">The email address</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <returns>True if the email address was deleted</returns>
		bool DeleteTestEmail(string userKey, int listId, string email, int? clientId = null);

		/// <summary>
		/// Retrieve the lists of test email addresses for a given list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>Enumeration of <see cref="string">test email addresses</see></returns>
		IEnumerable<string> GetTestEmails(string userKey, int listId, int? clientId = null);

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
		/// <returns>ID of the new subscriber</returns>
		int Subscribe(string userKey, int listId, string email, bool autoResponders = true, bool triggers = true, IEnumerable<KeyValuePair<string, object>> customFields = null, int? clientId = null);

		/// <summary>
		/// Add multiple subscribers to a list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="autoResponders">Trigger the autoresponders.</param>
		/// <param name="triggers">Trigger the welcome email.</param>
		/// <param name="listMembers">Subscribers.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>An enumeration of <see cref="ImportResult">results</see></returns>
		IEnumerable<ImportResult> Import(string userKey, int listId, IEnumerable<ListMember> susbscribers, bool autoResponders = true, bool triggers = true, int? clientId = null);

		/// <summary>
		/// Unsubscribe a member from the list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="email">Email address of the member.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>True if the member was unsubscribed</returns>
		bool Unsubscribe(string userKey, int listId, string email, int? clientId = null);

		/// <summary>
		/// Unsubscribe a member from the list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="listMemberId">ID of the member.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>True if the member was unsubscribed</returns>
		bool Unsubscribe(string userKey, int listId, int listMemberId, int? clientId = null);

		/// <summary>
		/// Delete a member from the list.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="listMemberId">ID of the member.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>True if the member was deleted</returns>
		bool DeleteListMember(string userKey, int listId, int listMemberId, int? clientId = null);

		/// <summary>
		/// Retrieve the information about a list member
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="listMemberId">ID of the member.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>The <see cref=" Listmember">list mamber</see></returns>
		ListMember GetListMember(string userKey, int listId, int listMemberId, int? clientId = null);

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
		/// <returns>Enumeration of <see cref="List">lists</see> matching the filtering criteria</returns>
		IEnumerable<ListMember> GetListMembers(string userKey, int listId, string status = null, string query = null, string sortBy = null, string sortDirection = null, int limit = 0, int offset = 0, int? clientId = null);

		/// <summary>
		/// Get a count of list members matching the filtering criteria
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="status">Filter using the member status. Possible values: 'active', 'unsubscribed', 'deleted', 'inactive_bounced', 'spam'</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>The number of list members matching the filtering criteria</returns>
		long GetListMembersCount(string userKey, int listId, string status = null, int? clientId = null);

		/// <summary>
		/// Update a list member
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="listMemberId">ID of the list member</param>
		/// <param name="customFields">Additional data for the member</param>
		/// <param name="clientId">ID of the client.</param>
		/// <returns>True if the member was updated</returns>
		bool UpdateListMember(string userKey, int listId, int listMemberId, IEnumerable<KeyValuePair<string, object>> customFields = null, int? clientId = null);

		/// <summary>
		/// Retrieve the log items for a given list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="logType">Filter using the log action. Possible values: "subscribe", "in_queue", "opened", "clickthru", "forward", "unsubscribe", "view", "spam", "skipped"</param>
		/// <param name="start">Filter using a start date</param>
		/// <param name="end">Filter using an end date</param>
		/// <param name="limit">Limit the number of resulting log items.</param>
		/// <param name="offset">Offset the beginning of resulting log items.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>An enumeration of <see cref="LogItem">log items</see> matching the filter criteria</returns>
		IEnumerable<LogItem> GetListLogs(string userKey, int listId, string logType = null, bool uniques = false, bool totals = false, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null);

		/// <summary>
		/// Get a count of log items matching the filter criteria
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="logType">Filter using the log action. Possible values: "subscribe", "in_queue", "opened", "clickthru", "forward", "unsubscribe", "view", "spam", "skipped"</param>
		/// <param name="start">Filter using a start date</param>
		/// <param name="end">Filter using an end date</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>The number of log items matching the filtering criteria</returns>
		long GetListLogsCount(string userKey, int listId, string logType = null, bool uniques = false, bool totals = false, DateTime? start = null, DateTime? end = null, int? clientId = null);

		#endregion

		#region Methods related to SEGMENTS

		/// <summary>
		/// Create a list segment
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list.</param>
		/// <param name="name">Name of the segment.</param>
		/// <param name="query">Rules for the segment.</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <returns>ID of the new segment</returns>
		/// <remarks>
		/// Here is what I have discovered about the query: 
		///		1) the entire query must be surrounded by parenthesis: (...your query...)
		///		2) field names must be surrounded with the 'special' quote: `yourfieldname`. On my US english keyboard, this 'special quote is the key directly above the 'Tab' and to the left of the '1'.
		///		3) The percent sign is the wilcard
		///		Here's an example: (`email` LIKE "a%")
		///	</remarks>
		int CreateSegment(string userKey, int listId, string name, string query = null, int? clientId = null);

		/// <summary>
		/// Update a segment (AKA sublist)
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="segmentId">ID of the segment</param>
		/// <param name="listId">ID of the list</param>
		/// <param name="name">Name of the segment.</param>
		/// <param name="language">Language of the segment. e.g.: 'en_US' for English (US)</param>
		/// <param name="spamPolicyAccepted">Indicates if the anti-spam policy has been accepted</param>
		/// <param name="status">Status of the segment. Possible values: 'active', 'archived', 'deleted'</param>
		/// <param name="senderName">Name of the default sender of the segment.</param>
		/// <param name="senderEmail">Email of the default sender of the segment.</param>
		/// <param name="goto_oi">Redirection URL on subscribing to the segment.</param>
		/// <param name="goto_di">Redirection URL on confirming the subscription to the segment.</param>
		/// <param name="goto_oo">Redirection URL on unsubscribing to the segment.</param>
		/// <param name="webhook">Webhook URL for the segment.</param>
		/// <param name="query">Rules for the segment.</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <returns>True if the segment was updated</returns>
		/// <remarks>A segment is sometimes referred to as a 'sub-list'</remarks>
		bool UpdateSegment(string userKey, int segmentId, int listId, string name = null, string query = null, int? clientId = null);

		/// <summary>
		/// Delete a segment
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="segmentId">ID of the segment</param>
		/// <param name="clientId">Client ID of the client in which the segment is located.</param>
		/// <returns>True if the segment was deleted</returns>
		bool DeleteSegment(string userKey, int segmentId, int? clientId = null);

		/// <summary>
		/// Retrieve the segments matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list</param>
		/// <param name="limit">Limit the number of resulting segments.</param>
		/// <param name="offset">Offset the beginning of resulting segments.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>Enumeration of <see cref="Segment">segments</see> matching the filtering criteria</returns>
		IEnumerable<Segment> GetSegments(string userKey, int listId, int limit = 0, int offset = 0, bool includeDetails = true, int? clientId = null);

		/// <summary>
		/// Get a count of segments matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the list</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>The count of campaigns matching the filtering criteria</returns>
		long GetSegmentsCount(string userKey, int listId, int? clientId = null);

		#endregion

		#region Methods related to MAILINGS

		/// <summary>
		/// Create a mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="name">Name of the mailing.</param>
		/// <param name="campaignId">ID of the campaign you want to associate the mailing with.</param>
		/// <param name="type">Type of the mailing. Possible values: 'standard', 'recurring', 'absplit'</param>
		/// <param name="recurringId">ID of the first recurrence in case of a 'recurring' or 'absplit' mailing.</param>
		/// <param name="encoding">Encoding to be used for the mailing. Possible values: 'utf-8', 'iso-8859-x'</param>
		/// <param name="transferEncoding">Transfer encoding to be used for the mailing. Possible values: 'quoted-printable', 'base64'</param>
		/// <param name="clientId">Client ID of the client in which the mailing is created.</param>
		/// <returns>ID of the new mailing</returns>
		int CreateMailing(string userKey, string name, int? campaignId = null, string type = "standard", int? recurringId = null, string encoding = null, string transferEncoding = null, int? clientId = null);

		/// <summary>
		/// Delete a mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>True if the mailing is deleted</returns>
		bool DeleteMailing(string userKey, int mailingId, int? clientId = null);

		/// <summary>
		/// Retrieve a mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="listId">ID of the mailing</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>The <see cref="Mailing">mailing</see></returns>
		Mailing GetMailing(string userKey, int mailingId, int? clientId = null);

		/// <summary>
		/// Retrieve the mailings matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the mailing status. Possible values: 'incomplete', 'scheduled', 'delivering', 'delivered'</param>
		/// <param name="type">Filter using the mailing type. Possible values: 'standard', 'recurring', 'absplit'</param>
		/// <param name="name">Filter using the mailing name.</param>
		/// <param name="listId">Filter using the ID of the mailing list.</param>
		/// <param name="campaignId">Filter using the ID of the mailing campaign.</param>
		/// <param name="recurringId">Filter using the ID of the mailing recurrence.</param>
		/// <param name="start">Filter using a start date.</param>
		/// <param name="end">Filter using a end date.</param>
		/// <param name="sortBy">Sort resulting mailings. Possible values: 'name', 'created_on', 'scheduled_for', 'scheduled_on', 'active_emails'</param>
		/// <param name="sortDirection">Direction of the sorting. Possible values: 'asc', 'desc'</param>
		/// <param name="limit">Limit the number of resulting lists.</param>
		/// <param name="offset">Offset the beginning of resulting lists.</param>
		/// <param name="clientId">Client ID of the client in which the list is located.</param>
		/// <returns>Enumeration of <see cref="Mailing">mailings</see> matching the filtering criteria</returns>
		IEnumerable<Mailing> GetMailings(string userKey, string status = null, string type = null, string name = null, int? listId = null, int? campaignId = null, int? recurringId = null, DateTime? start = null, DateTime? end = null, string sortBy = null, string sortDirection = null, int limit = 0, int offset = 0, int? clientId = null);

		/// <summary>
		/// Get a count of mailings matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="status">Filter using the mailing status. Possible values: 'incomplete', 'scheduled', 'delivering', 'delivered'</param>
		/// <param name="type">Filter using the mailing type. Possible values: 'standard', 'recurring', 'absplit'</param>
		/// <param name="name">Filter using the mailing name.</param>
		/// <param name="listId">Filter using the ID of the mailing list.</param>
		/// <param name="campaignId">Filter using the ID of the mailing campaign.</param>
		/// <param name="recurringId">Filter using the ID of the mailing recurrence.</param>
		/// <param name="start">Filter using a start date.</param>
		/// <param name="end">Filter using a end date.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>The count of mailings matching the filtering criteria</returns>
		long GetMailingsCount(string userKey, string status = null, string type = null, string name = null, int? listId = null, int? campaignId = null, int? recurringId = null, DateTime? start = null, DateTime? end = null, int? clientId = null);

		/// <summary>
		/// Update a mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="campaignId">ID of the campaign you want to associate the mailing with</param>
		/// <param name="listId">ID of the list you want to associate the mailing with.</param>
		/// <param name="sublistId">ID of the segment you want to associate the mailing with.</param>
		/// <param name="name">Name of the mailing.</param>
		/// <param name="type">Type of the mailing. Possible values: 'standard', 'recurring', 'absplit'</param>
		/// <param name="encoding">Encoding to be used for the mailing. Possible values: 'utf-8', 'iso-8859-x'</param>
		/// <param name="transferEncoding">Transfer encoding to be used for the mailing. Possible values: 'quoted-printable', 'base64'</param>
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
		/// <returns>True if the mailing was updated</returns>
		bool UpdateMailing(string userKey, int mailingId, int? campaignId = null, int? listId = null, int? sublistId = null, string name = null, string type = null, string encoding = null, string transferEncoding = null, string subject = null, string senderEmail = null, string senderName = null, string replyTo = null, string htmlContent = null, string textContent = null, bool? trackOpens = null, bool? trackClicksInHtml = null, bool? trackClicksInText = null, string trackingParameters = null, DateTime? endingOn = null, int? maxRecurrences = null, string recurringConditions = null, int? clientId = null);

		/// <summary>
		/// Send a test of a mailing.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="recipientEmail">Email address where the test will be sent.</param>
		/// <param name="separated">True if you want the HTML and the text to be sent seperatly, false if you want to combine the HTML and the text in a multi-part email.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>True if the test email was sent</returns>
		bool SendMailingTestEmail(string userKey, int mailingId, string recipientEmail, bool separated = false, int? clientId = null);

		/// <summary>
		/// Get the multi-part version of a mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>The <see cref="RawEmailMessage">multi-part message</see></returns>
		RawEmailMessage GetMailingRawEmailMessage(string userKey, int mailingId, int? clientId = null);

		/// <summary>
		/// Get the rendered HTML version of a mailing.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>The rendered HTML</returns>
		string GetMailingRawHtml(string userKey, int mailingId, int? clientId = null);

		/// <summary>
		/// Get the rendered text version of a mailing.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>The rendered text</returns>
		string GetMailingRawText(string userKey, int mailingId, int? clientId = null);

		/// <summary>
		/// Schedule a mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="date">Date when the mailing is scheduled. If not provided, the mailing will be sent right away.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>True if the mailing is scheduled</returns>
		bool ScheduleMailing(string userKey, int mailingId, DateTime? date = null, int? clientId = null);

		/// <summary>
		/// Unschedule a mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>True if the mailing is unscheduled</returns>
		bool UnscheduleMailing(string userKey, int mailingId, int? clientId = null);

		/// <summary>
		/// Suspend a delivering mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>True if the mailing is suspended</returns>
		bool SuspendMailing(string userKey, int mailingId, int? clientId = null);

		/// <summary>
		/// Resume a suspended mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>True if the mailing is resumed</returns>
		bool ResumeMailing(string userKey, int mailingId, int? clientId = null);

		/// <summary>
		/// Retrieve the log items for a given mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing.</param>
		/// <param name="logType">Filter using the log action. Possible values: "in_queue", "opened", "clickthru", "forward", "unsubscribe", "view", "spam", "skipped"</param>
		/// <param name="listMemberId">Filter using the ID of the member.</param>
		/// <param name="uniques">Return unique log item per member.</param>
		/// <param name="totals">Return all the log items.</param>
		/// <param name="start">Filter using a start date</param>
		/// <param name="end">Filter using an end date</param>
		/// <param name="limit">Limit the number of resulting log items.</param>
		/// <param name="offset">Offset the beginning of resulting log items.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>An enumeration of <see cref="LogItem">log items</see> matching the filter criteria</returns>
		IEnumerable<LogItem> GetMailingLogs(string userKey, int mailingId, string logType = null, int? listMemberId = null, bool uniques = false, bool totals = false, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null);

		/// <summary>
		/// Get a count of log items matching the filter criteria
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing.</param>
		/// <param name="logType">Filter using the log action. Possible values: "in_queue", "opened", "clickthru", "forward", "unsubscribe", "view", "spam", "skipped"</param>
		/// <param name="listMemberId">Filter using the ID of the member.</param>
		/// <param name="uniques">Return unique log item per member.</param>
		/// <param name="totals">Return all the log items.</param>
		/// <param name="start">Filter using a start date</param>
		/// <param name="end">Filter using an end date</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>The number of log items matching the filtering criteria</returns>
		long GetMailingLogsCount(string userKey, int mailingId, string logType = null, int? listMemberId = null, bool uniques = false, bool totals = false, DateTime? start = null, DateTime? end = null, int? clientId = null);

		/// <summary>
		/// Retrieve the links for a given mailing
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing.</param>
		/// <param name="limit">Limit the number of resulting log items.</param>
		/// <param name="offset">Offset the beginning of resulting log items.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>An enumeration of <see cref="Link">links</see> matching the filter criteria</returns>
		IEnumerable<Link> GetMailingLinks(string userKey, int mailingId, int limit = 0, int offset = 0, int? clientId = null);

		/// <summary>
		/// Get a count of links matching the filter criteria
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="mailingId">ID of the mailing.</param>
		/// <param name="clientId">Client ID of the client in which the mailing is located.</param>
		/// <returns>The number of links matching the filtering criteria</returns>
		long GetMailingLinksCount(string userKey, int mailingId, int? clientId = null);

		/// <summary>
		/// Retrieve a link
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="linkId">ID of the link.</param>
		/// <param name="clientId">Client ID of the client in which the link is located.</param>
		/// <returns>The <see cref="Link">link</see></returns>
		Link GetMailingLink(string userKey, int linkId, int? clientId = null);

		IEnumerable<LogItem> GetMailingLinksLogs(string userKey, int mailingId, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null);

		#endregion

		#region Methods related to RELAYS

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
		bool SendRelay(string userKey, string recipientEmailAddress, string subject, string html, string text, string senderEmail, string senderName = null, string encoding = null, int? clientId = null);

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
		bool SendTrackedRelay(string userKey, int trackingId, string recipientEmailAddress, string subject, string html, string text, string senderEmail, string senderName = null, string encoding = null, int? clientId = null);

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
		IEnumerable<RelayLog> GetRelaySentLogs(string userKey, int? trackingId = null, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null);

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
		IEnumerable<RelayOpenLog> GetRelayOpenLogs(string userKey, int? trackingId = null, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null);

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
		IEnumerable<RelayClickLog> GetRelayClickLogs(string userKey, int? trackingId = null, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null);

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
		IEnumerable<RelayBounceLog> GetRelayBounceLogs(string userKey, int? trackingId = null, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null);

		#endregion

		#region Methods related to SUPPRESSION LISTS

		/// <summary>
		/// Add email addresses to the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="emailAddresses">The email addresses to add to the suppression list</param>
		/// <param name="clientId">Client ID of the client.</param>
		/// <returns>An enumeration of <see cref="SuppressEmailResult">results</see>. Each item in this enumeration indicates the result of adding an email address to the suppression list.</returns>
		IEnumerable<SuppressEmailResult> AddEmailAddressesToSuppressionList(string userKey, IEnumerable<string> emailAddresses, int? clientId = null);

		/// <summary>
		/// Add domains to the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="domains">The domains to add to the suppression list</param>
		/// <param name="clientId">Client ID of the client.</param>
		/// <returns>An enumeration of <see cref="SuppressDomainResult">results</see>. Each item in this enumeration indicates the result of adding a domain to the suppression list.</returns>
		IEnumerable<SuppressDomainResult> AddDomainsToSuppressionList(string userKey, IEnumerable<string> domains, int? clientId = null);

		/// <summary>
		/// Add localparts to the suppression list
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="domains">The localparts to add to the suppression list</param>
		/// <param name="clientId">Client ID of the client.</param>
		/// <returns>An enumeration of <see cref="SuppressLocalPartResult">results</see>. Each item in this enumeration indicates the result of adding a localpart to the suppression list.</returns>
		IEnumerable<SuppressLocalPartResult> AddLocalPartsToSuppressionList(string userKey, IEnumerable<string> localParts, int? clientId = null);

		IEnumerable<SuppressEmailResult> RemoveEmailAddressesFromSuppressionList(string userKey, IEnumerable<string> emailAddresses, int? clientId = null);

		IEnumerable<SuppressDomainResult> RemoveDomainsFromSuppressionList(string userKey, IEnumerable<string> domains, int? clientId = null);

		IEnumerable<SuppressLocalPartResult> RemoveLocalPartsFromSuppressionList(string userKey, IEnumerable<string> localParts, int? clientId = null);

		IEnumerable<SuppressedEmail> GetSuppressedEmailAddresses(string userKey, int limit = 0, int offset = 0, int? clientId = null);

		IEnumerable<string> GetSuppressedDomains(string userKey, int limit = 0, int offset = 0, int? clientId = null);

		IEnumerable<string> GetSuppressedLocalParts(string userKey, int limit = 0, int offset = 0, int? clientId = null);

		#endregion

		#region Methods related to TIMEZONES

		IEnumerable<Timezone> GetTimezones();

		#endregion

		#region Methods related to TRIGGERS

		int CreateTrigger(string userKey, string name, int listId, string encoding = null, string transferEncoding = null, int? campaignId = null, int? clientId = null);

		//bool DeleteTrigger(string userKey, int triggerId, int? clientId = null);

		Trigger GetTrigger(string userKey, int triggerId, int? clientId = null);

		bool UpdateTrigger(string userKey, int triggerId, int? campaignId = null, string name = null, string action = null, string encoding = null, string transferEncoding = null, string subject = null, string senderEmail = null, string senderName = null, string replyTo = null, string htmlContent = null, string textContent = null, bool? trackOpens = null, bool? trackClicksInHtml = null, bool? trackClicksInText = null, string trackingParameters = null, int? delay = null, string status = null, string dateField = null, int? clientId = null);

		IEnumerable<Trigger> GetTriggers(string userKey, string status = null, string action = null, int? listId = null, int? campaignId = null, int limit = 0, int offset = 0, int? clientId = null);

		long GetTriggersCount(string userKey, string status = null, string action = null, int? listId = null, int? campaignId = null, int? clientId = null);

		bool SendTriggerTestEmail(string userKey, int triggerId, string recipientEmail, bool separated = false, int? clientId = null);

		RawEmailMessage GetTriggerRawEmailMessage(string userKey, int triggerId, int? clientId = null);

		string GetTriggerRawHtml(string userKey, int triggerId, int? clientId = null);

		string GetTriggerRawText(string userKey, int triggerId, int? clientId = null);

		bool UnleashTrigger(string userKey, int triggerId, int listMemberId, int? clientId = null);

		IEnumerable<LogItem> GetTriggerLogs(string userKey, int triggerId, string logType = null, int? listMemberId = null, bool uniques = false, bool totals = false, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null);

		long GetTriggerLogsCount(string userKey, int triggerId, string logType = null, int? listMemberId = null, bool uniques = false, bool totals = false, DateTime? start = null, DateTime? end = null, int? clientId = null);

		IEnumerable<Link> GetTriggerLinks(string userKey, int triggerId, int limit = 0, int offset = 0, int? clientId = null);

		long GetTriggerLinksCount(string userKey, int triggerId, int? clientId = null);

		Link GeTriggerLink(string userKey, int linkId, int? clientId = null);

		IEnumerable<LogItem> GetTriggerLinksLogs(string userKey, int triggerId, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null);

		#endregion

		#region Methods related to TEMPLATES

		int CreateTemplateCategory(string userKey, IDictionary<string, string> labels, bool isVisibleByDefault = true, bool templatesCanBeCopied = true, int? clientId = null);

		bool DeleteTemplateCategory(string userKey, int categoryId, int? clientId = null);

		TemplateCategory GetTemplateCategory(string userKey, int categoryId, int? clientId = null);

		IEnumerable<TemplateCategory> GetTemplateCategories(string userKey, int limit = 0, int offset = 0, int? clientId = null);

		long GetTemplateCategoriesCount(string userKey, int? clientId = null);

		bool UpdateTemplateCategory(string userKey, int categoryId, IDictionary<string, string> labels, bool isVisibleByDefault = true, bool templatesCanBeCopied = true, int? clientId = null);

		IEnumerable<string> GetTemplateCategoryVisibility(string userKey, int categoryId, int limit = 0, int offset = 0, int? clientId = null);

		long SetTemplateCategoryVisibility(string userKey, int categoryId, IDictionary<int, bool> clientVisibility, int? clientId = null);

		int CreateTemplate(string userKey, IDictionary<string, string> labels, string content = null, int? categoryId = null, int? clientId = null);

		bool DeleteTemplate(string userKey, int templateId, int? clientId = null);

		TemplateCategory GetTemplate(string userKey, int templateId, int? clientId = null);

		IEnumerable<TemplateCategory> GetTemplates(string userKey, int? categoryId = null, int limit = 0, int offset = 0, int? clientId = null);

		long GetTemplatesCount(string userKey, int? categoryId = null, int? clientId = null);

		bool UpdateTemplate(string userKey, int templateId, IDictionary<string, string> labels, string content = null, int? categoryId = null, int? clientId = null);

		#endregion

		#region Methods related to USERS

		int CreateUser(string userKey, string email, string firstName, string lastName, string title, string officePhone, string mobilePhone, string language, string password, int timezoneId = 542, int? clientId = null);

		bool DeactivateUser(string userKey, int userId, int? clientId = null);

		bool DeleteUser(string userKey, int userId, int? clientId = null);

		User GetUser(string userKey, int userId, int? clientId = null);

		IEnumerable<User> GetUsers(string userKey, string status = null, int limit = 0, int offset = 0, int? clientId = null);

		long GetUsersCount(string userKey, string status = null, int? clientId = null);

		bool UpdateUser(string userKey, int userId, string status, string email, string firstName, string lastName, string title, string officePhone, string mobilePhone, string language, string timezoneId, string password, int? clientId = null);

		LoginInfo Login(string userName, string password, int? clientId = null);

		#endregion
	}
}