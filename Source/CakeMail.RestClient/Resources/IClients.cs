using CakeMail.RestClient.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	public interface IClients
	{
		Task<bool> ActivateAsync(string userKey, long clientId, CancellationToken cancellationToken = default(CancellationToken));
		Task<ClientRegistrationInfo> ConfirmAsync(string confirmationCode, CancellationToken cancellationToken = default(CancellationToken));
		Task<string> CreateAsync(long parentId, string name, string address1 = null, string address2 = null, string city = null, string provinceId = null, string postalCode = null, string countryId = null, string website = null, string phone = null, string fax = null, string adminEmail = null, string adminFirstName = null, string adminLastName = null, string adminTitle = null, string adminOfficePhone = null, string adminMobilePhone = null, string adminLanguage = null, long? adminTimezoneId = default(long?), string adminPassword = null, bool primaryContactSameAsAdmin = true, string primaryContactEmail = null, string primaryContactFirstName = null, string primaryContactLastName = null, string primaryContactTitle = null, string primaryContactOfficePhone = null, string primaryContactMobilePhone = null, string primaryContactLanguage = null, long? primaryContactTimezoneId = default(long?), string primaryContactPassword = null, CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> DeleteAsync(string userKey, long clientId, CancellationToken cancellationToken = default(CancellationToken));
		Task<UnConfirmedClient> GetAsync(string userKey, string confirmationCode, CancellationToken cancellationToken = default(CancellationToken));
		Task<Client> GetAsync(string userKey, long clientId, DateTime? startDate = default(DateTime?), DateTime? endDate = default(DateTime?), CancellationToken cancellationToken = default(CancellationToken));
		Task<long> GetCountAsync(string userKey, ClientStatus? status = default(ClientStatus?), string name = null, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<Client[]> GetListAsync(string userKey, ClientStatus? status = default(ClientStatus?), string name = null, ClientsSortBy? sortBy = default(ClientsSortBy?), SortDirection? sortDirection = default(SortDirection?), int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> SuspendAsync(string userKey, long clientId, CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> UpdateAsync(string userKey, long clientId, string name = null, ClientStatus? status = default(ClientStatus?), long? parentId = default(long?), string address1 = null, string address2 = null, string city = null, string provinceId = null, string postalCode = null, string countryId = null, string website = null, string phone = null, string fax = null, string authDomain = null, string bounceDomain = null, string dkimDomain = null, string doptinIp = null, string forwardDomain = null, string forwardIp = null, string ipPool = null, string mdDomain = null, bool? isReseller = default(bool?), string currency = null, string planType = null, int? mailingLimit = default(int?), int? monthLimit = default(int?), int? contactLimit = default(int?), int? defaultMailingLimit = default(int?), int? defaultMonthLimit = default(int?), int? defaultContactLimit = default(int?), CancellationToken cancellationToken = default(CancellationToken));
	}
}
