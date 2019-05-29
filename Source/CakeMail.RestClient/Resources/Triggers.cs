using CakeMail.RestClient.Models;
using CakeMail.RestClient.Utilities;
using Pathoschild.Http.Client;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	/// <summary>
	/// Allows you to manage triggers
	/// </summary>
	/// <seealso cref="CakeMail.RestClient.Resources.ITriggers" />
	public class Triggers : ITriggers
	{
		#region Fields

		private readonly IClient _client;

		#endregion

		#region Constructor

		internal Triggers(IClient client)
		{
			_client = client;
		}

		#endregion

		#region Public Methods

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
		public Task<long> CreateAsync(string userKey, string name, long listId, long? campaignId = null, MessageEncoding? encoding = null, TransferEncoding? transferEncoding = null, long? clientId = null, CancellationToken cancellationToken = default)
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("name", name),
				new KeyValuePair<string, object>("list_id", listId)
			};
			if (encoding.HasValue) parameters.Add(new KeyValuePair<string, object>("encoding", encoding.Value.GetEnumMemberValue()));
			if (transferEncoding.HasValue) parameters.Add(new KeyValuePair<string, object>("transfer_encoding", transferEncoding.Value.GetEnumMemberValue()));
			if (campaignId.HasValue) parameters.Add(new KeyValuePair<string, object>("campaign_id", campaignId.Value));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("Trigger/Create")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<long>();
		}

		/// <summary>
		/// Retrieve a trigger
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="triggerId">ID of the trigger</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The <see cref="Trigger">trigger</see></returns>
		public Task<Trigger> GetAsync(string userKey, long triggerId, long? clientId = null, CancellationToken cancellationToken = default)
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("Trigger/GetInfo")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<Trigger>();
		}

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
		public Task<bool> UpdateAsync(string userKey, long triggerId, long? campaignId = null, string name = null, TriggerAction? action = null, MessageEncoding? encoding = null, TransferEncoding? transferEncoding = null, string subject = null, string senderEmail = null, string senderName = null, string replyTo = null, string htmlContent = null, string textContent = null, bool? trackOpens = null, bool? trackClicksInHtml = null, bool? trackClicksInText = null, string trackingParameters = null, int? delay = null, TriggerStatus? status = null, string dateField = null, long? clientId = null, CancellationToken cancellationToken = default)
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId)
			};
			if (campaignId.HasValue) parameters.Add(new KeyValuePair<string, object>("campaign_id", campaignId.Value));
			if (name != null) parameters.Add(new KeyValuePair<string, object>("name", name));
			if (action.HasValue) parameters.Add(new KeyValuePair<string, object>("action", action.Value.GetEnumMemberValue()));
			if (encoding.HasValue) parameters.Add(new KeyValuePair<string, object>("encoding", encoding.Value.GetEnumMemberValue()));
			if (transferEncoding.HasValue) parameters.Add(new KeyValuePair<string, object>("transfer_encoding", transferEncoding.Value.GetEnumMemberValue()));
			if (subject != null) parameters.Add(new KeyValuePair<string, object>("subject", subject));
			if (senderEmail != null) parameters.Add(new KeyValuePair<string, object>("sender_email", senderEmail));
			if (senderName != null) parameters.Add(new KeyValuePair<string, object>("sender_name", senderName));
			if (replyTo != null) parameters.Add(new KeyValuePair<string, object>("reply_to", replyTo));
			if (htmlContent != null) parameters.Add(new KeyValuePair<string, object>("html_message", htmlContent));
			if (textContent != null) parameters.Add(new KeyValuePair<string, object>("text_message", textContent));
			if (trackOpens.HasValue) parameters.Add(new KeyValuePair<string, object>("opening_stats", trackOpens.Value ? "true" : "false"));
			if (trackClicksInHtml.HasValue) parameters.Add(new KeyValuePair<string, object>("clickthru_html", trackClicksInHtml.Value ? "true" : "false"));
			if (trackClicksInText.HasValue) parameters.Add(new KeyValuePair<string, object>("clickthru_text", trackClicksInText.Value ? "true" : "false"));
			if (trackingParameters != null) parameters.Add(new KeyValuePair<string, object>("tracking_params", trackingParameters));
			if (delay != null) parameters.Add(new KeyValuePair<string, object>("delay", delay));
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (!string.IsNullOrEmpty(dateField)) parameters.Add(new KeyValuePair<string, object>("date_field", dateField));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("Trigger/SetInfo")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<bool>();
		}

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
		public Task<Trigger[]> GetTriggersAsync(string userKey, TriggerStatus? status = null, TriggerAction? action = null, long? listId = null, long? campaignId = null, int? limit = 0, int? offset = 0, long? clientId = null, CancellationToken cancellationToken = default)
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "false")
			};
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (action.HasValue) parameters.Add(new KeyValuePair<string, object>("action", action.Value.GetEnumMemberValue()));
			if (listId.HasValue) parameters.Add(new KeyValuePair<string, object>("list_id", listId.Value));
			if (campaignId.HasValue) parameters.Add(new KeyValuePair<string, object>("campaign_id", campaignId.Value));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("Trigger/GetList")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<Trigger[]>("triggers");
		}

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
		public Task<long> GetCountAsync(string userKey, TriggerStatus? status = null, TriggerAction? action = null, long? listId = null, long? campaignId = null, long? clientId = null, CancellationToken cancellationToken = default)
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "true")
			};
			if (status.HasValue) parameters.Add(new KeyValuePair<string, object>("status", status.Value.GetEnumMemberValue()));
			if (action.HasValue) parameters.Add(new KeyValuePair<string, object>("action", action.Value.GetEnumMemberValue()));
			if (listId.HasValue) parameters.Add(new KeyValuePair<string, object>("list_id", listId.Value));
			if (campaignId.HasValue) parameters.Add(new KeyValuePair<string, object>("campaign_id", campaignId.Value));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("Trigger/GetList")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<long>("count");
		}

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
		public Task<bool> SendTestEmailAsync(string userKey, long triggerId, string recipientEmail, bool separated = false, long? clientId = null, CancellationToken cancellationToken = default)
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId),
				new KeyValuePair<string, object>("test_email", recipientEmail),
				new KeyValuePair<string, object>("test_type", separated ? "separated" : "merged")
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("Trigger/SendTestEmail")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<bool>();
		}

		/// <summary>
		/// Get the multi-part version of a trigger
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="triggerId">ID of the trigger</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The <see cref="RawEmailMessage">multi-part message</see></returns>
		public Task<RawEmailMessage> GetRawEmailMessageAsync(string userKey, long triggerId, long? clientId = null, CancellationToken cancellationToken = default)
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("Trigger/GetEmailMessage")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<RawEmailMessage>();
		}

		/// <summary>
		/// Get the rendered HTML version of a trigger.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="triggerId">ID of the trigger</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The rendered HTML</returns>
		public Task<string> GetRawHtmlAsync(string userKey, long triggerId, long? clientId = null, CancellationToken cancellationToken = default)
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("Trigger/GetHtmlMessage")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<string>();
		}

		/// <summary>
		/// Get the rendered text version of a mailing.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="triggerId">ID of the trigger</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The rendered text</returns>
		public Task<string> GetRawTextAsync(string userKey, long triggerId, long? clientId = null, CancellationToken cancellationToken = default)
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("Trigger/GetTextMessage")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<string>();
		}

		/// <summary>
		/// Unleash a trigger
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="triggerId">ID of the trigger</param>
		/// <param name="listMemberId">ID of the member to unleash the trigger to.</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True is the trigger is unleashed</returns>
		public Task<bool> UnleashAsync(string userKey, long triggerId, long listMemberId, long? clientId = null, CancellationToken cancellationToken = default)
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId),
				new KeyValuePair<string, object>("record_id", listMemberId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("Trigger/Unleash")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<bool>();
		}

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
		public Task<LogItem[]> GetLogsAsync(string userKey, long triggerId, LogType? logType = null, long? listMemberId = null, bool uniques = false, bool totals = false, DateTime? start = null, DateTime? end = null, int? limit = 0, int? offset = 0, long? clientId = null, CancellationToken cancellationToken = default)
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId),
				new KeyValuePair<string, object>("totals", totals ? "true" : "false"),
				new KeyValuePair<string, object>("uniques", uniques ? "true" : "false"),
				new KeyValuePair<string, object>("count", "false")
			};
			if (logType.HasValue) parameters.Add(new KeyValuePair<string, object>("action", logType.Value.GetEnumMemberValue()));
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_time", start.Value.ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_time", end.Value.ToCakeMailString()));
			if (listMemberId.HasValue) parameters.Add(new KeyValuePair<string, object>("record_id", listMemberId.Value));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("Trigger/GetLog")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<LogItem[]>("logs");
		}

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
		public Task<long> GetLogsCountAsync(string userKey, long triggerId, LogType? logType = null, long? listMemberId = null, bool uniques = false, bool totals = false, DateTime? start = null, DateTime? end = null, long? clientId = null, CancellationToken cancellationToken = default)
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId),
				new KeyValuePair<string, object>("totals", totals ? "true" : "false"),
				new KeyValuePair<string, object>("uniques", uniques ? "true" : "false"),
				new KeyValuePair<string, object>("count", "true")
			};
			if (logType.HasValue) parameters.Add(new KeyValuePair<string, object>("action", logType.Value.GetEnumMemberValue()));
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_time", start.Value.ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_time", end.Value.ToCakeMailString()));
			if (listMemberId.HasValue) parameters.Add(new KeyValuePair<string, object>("record_id", listMemberId.Value));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("Trigger/GetLog")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<long>("count");
		}

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
		public Task<Link[]> GetLinksAsync(string userKey, long triggerId, int? limit = 0, int? offset = 0, long? clientId = null, CancellationToken cancellationToken = default)
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId),
				new KeyValuePair<string, object>("count", "false")
			};
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("Trigger/GetLinks")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<Link[]>("links");
		}

		/// <summary>
		/// Get a count of links matching the filter criteria
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="triggerId">ID of the trigger.</param>
		/// <param name="clientId">Client ID of the client in which the trigger is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The number of links matching the filtering criteria</returns>
		public Task<long> GetLinksCountAsync(string userKey, long triggerId, long? clientId = null, CancellationToken cancellationToken = default)
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId),
				new KeyValuePair<string, object>("count", "true")
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("Trigger/GetLinks")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<long>("count");
		}

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
		public Task<Link> GetLinkAsync(string userKey, long linkId, long? clientId = null, CancellationToken cancellationToken = default)
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("link_id", linkId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("Trigger/GetLinkInfo")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<Link>();
		}

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
		public Task<LinkStats[]> GetLinksWithStatsAsync(string userKey, long triggerId, DateTime? start = null, DateTime? end = null, int? limit = 0, int? offset = 0, long? clientId = null, CancellationToken cancellationToken = default)
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId),
				new KeyValuePair<string, object>("count", "false")
			};
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_time", start.Value.ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_time", end.Value.ToCakeMailString()));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("Trigger/GetLinksLog")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<LinkStats[]>("links");
		}

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
		public Task<long> GetLinksWithStatsCountAsync(string userKey, long triggerId, DateTime? start = null, DateTime? end = null, long? clientId = null, CancellationToken cancellationToken = default)
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("trigger_id", triggerId),
				new KeyValuePair<string, object>("count", "true")
			};
			if (start.HasValue) parameters.Add(new KeyValuePair<string, object>("start_time", start.Value.ToCakeMailString()));
			if (end.HasValue) parameters.Add(new KeyValuePair<string, object>("end_time", end.Value.ToCakeMailString()));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("Trigger/GetLinksLog")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<long>("count");
		}

		#endregion
	}
}
