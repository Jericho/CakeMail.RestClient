using CakeMail.RestClient.Models;
using System;
using System.Collections.Generic;

namespace CakeMail.RestClient
{
	public interface ICakeMailRestClient
	{
		#region Methods related to CAMPAIGNS

		/// <summary>
		/// Method to create a new campaign
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="name">Name of the campaign.</param>
		/// <param name="clientId">Client ID of the client in which the campaign is created.</param>
		/// <returns>ID of the new campaign</returns>
		int CreateCampaign(string userKey, string name, int? clientId = null);

		/// <summary>
		/// Method to delete a campaign.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="campaignId">ID of the campaign to delete.</param>
		/// <param name="clientId">Client ID of the client in which the campaign is located.</param>
		/// <returns>True if the campaign is deleted</returns>
		bool DeleteCampaign(string userKey, int campaignId, int? clientId = null);

		/// <summary>
		/// Method to get information about a campaign.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="campaignId">ID of the campaign.</param>
		/// <param name="clientId">Client ID of the client in which the campaign is located.</param>
		/// <returns>The campaign</returns>
		Campaign GetCampaign(string userKey, int campaignId, int? clientId = null);

		IEnumerable<Campaign> GetCampaigns(string userKey, string status = null, string name = null, string sortBy = null, string sortDirection = null, int limit = 0, int offset = 0, int? clientId = null);

		long GetCampaignsCount(string userKey, string status = null, string name = null, int? clientId = null);

		bool UpdateCampaign(string userKey, int campaignId, string name, int? clientId = null);

		#endregion

		#region Methods related to CLIENTS

		string CreateClient(int parentId, string name, string address1 = null, string address2 = null, string city = null, string provinceId = null, string postalCode = null, string countryId = null, string website = null, string phone = null, string fax = null, string adminEmail = null, string adminFirstName = null, string adminLastName = null, string adminTitle = null, string adminOfficePhone = null, string adminMobilePhone = null, string adminLanguage = null, int? adminTimezoneId = null, string adminPassword = null, bool primaryContactSameAsAdmin = true, string primaryContactEmail = null, string primaryContactFirstName = null, string primaryContactLastName = null, string primaryContactTitle = null, string primaryContactOfficePhone = null, string primaryContactMobilePhone = null, string primaryContactLanguage = null, int? primaryContactTimezoneId = null, string primaryContactPassword = null);

		ActivationInfo ActivateClient(string confirmation);

		Client GetClient(string userKey, int clientId, DateTime? startDate = null, DateTime? endDate = null);

		IEnumerable<Client> GetClients(string userKey, string status = null, string name = null, string sortBy = null, string sortDirection = null, int limit = 0, int offset = 0, int? clientId = null);

		long GetClientsCount(string userKey, string status = null, string name = null, int? clientId = null);

		bool UpdateClient(string userKey, int clientId, string name = null, string status = null, int? parentId = null, string address1 = null, string address2 = null, string city = null, string provinceId = null, string postalCode = null, string countryId = null, string website = null, string phone = null, string fax = null, string authDomain = null, string bounceDomain = null, string dkimDomain = null, string doptinIp = null, string forwardDomain = null, string forwardIp = null, string ipPool = null, string mdDomain = null, bool? isReseller = null, string currency = null, string planType = null, string mailingLimit = null, string monthLimit = null, string defaultMailingLimit = null, string defaultMonthLimit = null);

		#endregion

		#region Methods related to PERMISSIONS

		IEnumerable<string> GetUserPermissions(string userKey, int userId, int? clientId = null);

		bool SetUserPermissions(string userKey, int userId, IEnumerable<string> permissions, int? clientId = null);

		#endregion

		#region Methods related to COUNTRIES

		IEnumerable<Country> GetCountries();

		IEnumerable<Province> GetProvinces(string countryId);

		#endregion

		#region Methods related to LISTS

		int CreateList(string userKey, string name, string defaultSenderName, string defaultSenderEmailAddress, int? clientId = null);

		bool DeleteList(string userKey, int listId, int? clientId = null);

		List GetList(string userKey, int listId, int? subListId = null, bool includeDetails = true, bool calculateEngagement = false, int? clientId = null);

		IEnumerable<List> GetLists(string userKey, string name = null, string sortBy = null, string sortDirection = null, int limit = 0, int offset = 0, int? clientId = null);

		long GetListsCount(string userKey, string name = null, int? clientId = null);

		bool UpdateList(string userKey, int listId, int? subListId = null, string name = null, string language = null, string policy = null, string status = null, string senderName = null, string senderEmail = null, string goto_oi = null, string goto_di = null, string goto_oo = null, string webhook = null, string query = null, int? clientId = null);

		bool AddListField(string userKey, int listId, string fieldName, string fieldType, int? clientId = null);

		bool DeleteListField(string userKey, int listId, string fieldName, int? clientId = null);

		IEnumerable<ListField> GetListFields(string userKey, int listId, int? clientId = null);

		int CreateSublist(string userKey, int listId, string name, string query = null, int? clientId = null);

		bool DeleteSublist(string userKey, int sublistId, int? clientId = null);

		IEnumerable<List> GetSublists(string userKey, int listId, int limit = 0, int offset = 0, bool includeDetails = true, int? clientId = null);

		long GetSublistsCount(string userKey, int listId, int? clientId = null);

		bool AddTestEmail(string userKey, int listId, string email, int? clientId = null);

		bool DeleteTestEmail(string userKey, int listId, string email, int? clientId = null);

		IEnumerable<string> GetTestEmails(string userKey, int listId, int? clientId = null);

		int Subscribe(string userKey, int listId, string email, bool autoResponders = true, bool triggers = true, IEnumerable<KeyValuePair<string, object>> customFields = null, int? clientId = null);

		IEnumerable<ImportResult> Import(string userKey, int listId, bool autoResponders = true, bool triggers = true, IEnumerable<ListMember> listMembers = null, int? clientId = null);

		bool Unsubscribe(string userKey, int listId, string email, int? clientId = null);

		bool Unsubscribe(string userKey, int listId, int listMemberId, int? clientId = null);

		bool DeleteListMember(string userKey, int listId, int listMemberId, int? clientId = null);

		ListMember GetListMember(string userKey, int listId, int listMemberId, int? clientId = null);

		IEnumerable<ListMember> GetListMembers(string userKey, int listId, string status = null, string sortBy = null, string sortDirection = null, int limit = 0, int offset = 0, int? clientId = null);

		long GetListMembersCount(string userKey, int listId, string status = null, int? clientId = null);

		bool UpdateListMember(string userKey, int listId, int listMemberId, IEnumerable<KeyValuePair<string, object>> customFields = null, int? clientId = null);

		IEnumerable<LogItem> GetListLogs(string userKey, int listId, string logType = null, bool uniques = false, bool totals = false, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null);

		long GetListLogsCount(string userKey, int listId, string logType = null, bool uniques = false, bool totals = false, DateTime? start = null, DateTime? end = null, int? clientId = null);

		#endregion

		#region Methods related to MAILINGS

		int CreateMailing(string userKey, string name, int? campaignId = null, string type = "standard", int? recurringId = null, string encoding = null, string transferEncoding = null, int? clientId = null);

		bool DeleteMailing(string userKey, int mailingId, int? clientId = null);

		Mailing GetMailing(string userKey, int mailingId, int? clientId = null);

		IEnumerable<Mailing> GetMailings(string userKey, string status = null, string type = null, string name = null, int? listId = null, int? campaignId = null, int? recurringId = null, DateTime? start = null, DateTime? end = null, string sortBy = null, string sortDirection = null, int limit = 0, int offset = 0, int? clientId = null);

		long GetMailingsCount(string userKey, string status = null, string type = null, string name = null, int? listId = null, int? campaignId = null, int? recurringId = null, DateTime? start = null, DateTime? end = null, int? clientId = null);

		bool UpdateMailing(string userKey, int mailingId, int? campaignId = null, int? listId = null, int? sublistId = null, string name = null, string type = null, string encoding = null, string transferEncoding = null, string subject = null, string senderEmail = null, string senderName = null, string replyTo = null, string htmlContent = null, string textContent = null, bool? trackOpens = null, bool? trackClicksInHtml = null, bool? trackClicksInText = null, string trackingParameters = null, DateTime? endingOn = null, int? maxRecurrences = null, string recurringConditions = null, int? clientId = null);

		bool SendMailingTestEmail(string userKey, int mailingId, string recipientEmail, bool separated = false, int? clientId = null);

		RawEmailMessage GetMailingRawEmailMessage(string userKey, int mailingId, int? clientId = null);

		string GetMailingRawHtml(string userKey, int mailingId, int? clientId = null);

		string GetMailingRawText(string userKey, int mailingId, int? clientId = null);

		bool ScheduleMailing(string userKey, int mailingId, DateTime? date = null, int? clientId = null);

		bool UnscheduleMailing(string userKey, int mailingId, int? clientId = null);

		bool SuspendMailing(string userKey, int mailingId, int? clientId = null);

		bool ResumeMailing(string userKey, int mailingId, int? clientId = null);

		IEnumerable<LogItem> GetMailingLogs(string userKey, int mailingId, string logType = null, int? listMemberId = null, bool uniques = false, bool totals = false, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null);

		long GetMailingLogsCount(string userKey, int mailingId, string logType = null, int? listMemberId = null, bool uniques = false, bool totals = false, DateTime? start = null, DateTime? end = null, int? clientId = null);

		IEnumerable<Link> GetMailingLinks(string userKey, int mailingId, int limit = 0, int offset = 0, int? clientId = null);

		long GetMailingLinksCount(string userKey, int mailingId, int? clientId = null);

		Link GetMailingLink(string userKey, int linkId, int? clientId = null);

		IEnumerable<LogItem> GetMailingLinksLogs(string userKey, int mailingId, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null);

		#endregion

		#region Methods related to RELAYS

		bool SendRelay(string userKey, string email, string senderEmail, string senderName, string html, string text, string subject, string encoding, bool trackOpens, bool trackClicksInHtml, bool trackClicksInText, int trackingId, int? clientId = null);

		IEnumerable<RelayLog> GetRelaySentLogs(string userKey, int? trackingId = null, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null);

		IEnumerable<RelayOpenLog> GetRelayOpenLogs(string userKey, int? trackingId = null, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null);

		IEnumerable<RelayClickLog> GetRelayClickLogs(string userKey, int? trackingId = null, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null);

		IEnumerable<RelayBounceLog> GetRelayBounceLogs(string userKey, int? trackingId = null, DateTime? start = null, DateTime? end = null, int limit = 0, int offset = 0, int? clientId = null);

		#endregion

		#region Methods related to SUPPRESSION LISTS

		IEnumerable<SuppressEmailResult> AddEmailAddressesToSuppressionList(string userKey, IEnumerable<string> emailAddresses, int? clientId = null);

		IEnumerable<SuppressDomainResult> AddDomainsToSuppressionList(string userKey, IEnumerable<string> domains, int? clientId = null);

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