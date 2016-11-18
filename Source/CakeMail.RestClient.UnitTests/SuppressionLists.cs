using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;

namespace CakeMail.RestClient.UnitTests
{
	[TestClass]
	public class SuppressionListsTests
	{
		private const string API_KEY = "...dummy API key...";
		private const string USER_KEY = "...dummy USER key...";
		private const long CLIENT_ID = 999;

		[TestMethod]
		public async Task AddEmailAddressesToSuppressionList_with_minimal_parameters()
		{
			// Arrange
			var emailAddresses = new[] { "aaa@aaa.com", "bbb@bbb.com", "ccc@ccc.com" };
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "email[0]", Value = emailAddresses[0] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "email[1]", Value = emailAddresses[1] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "email[2]", Value = emailAddresses[2] }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"email\":\"aaa@aaa.com\"},{\"email\":\"bbb@bbb.com\"},{\"email\":\"ccc@ccc.com\"}]}";
			var mockRestClient = new MockRestClient("/SuppressionList/ImportEmails/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.AddEmailAddressesAsync(USER_KEY, emailAddresses);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(3);
		}

		[TestMethod]
		public async Task AddEmailAddressesToSuppressionList_with_null_array()
		{
			// Arrange
			var parameters = new Parameter[]
			{
				// There are no parameters
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"email\":\"aaa@aaa.com\"},{\"email\":\"bbb@bbb.com\"},{\"email\":\"ccc@ccc.com\"}]}";
			var mockRestClient = new MockRestClient("/SuppressionList/ImportEmails/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.AddEmailAddressesAsync(USER_KEY, null);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(3);
		}

		[TestMethod]
		public async Task AddEmailAddressesToSuppressionList_with_clientid()
		{
			// Arrange
			var emailAddresses = new[] { "aaa@aaa.com", "bbb@bbb.com", "ccc@ccc.com" };
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "email[0]", Value = emailAddresses[0] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "email[1]", Value = emailAddresses[1] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "email[2]", Value = emailAddresses[2] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"email\":\"aaa@aaa.com\"},{\"email\":\"bbb@bbb.com\"},{\"email\":\"ccc@ccc.com\"}]}";
			var mockRestClient = new MockRestClient("/SuppressionList/ImportEmails/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.AddEmailAddressesAsync(USER_KEY, emailAddresses, CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(3);
		}

		[TestMethod]
		public async Task AddDomainsToSuppressionList_with_minimal_parameters()
		{
			// Arrange
			var domains = new[] { "aaa.com", "bbb.com", "ccc.com" };
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "domain[0]", Value = domains[0] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "domain[1]", Value = domains[1] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "domain[2]", Value = domains[2] }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"domain\":\"aaa.com\"},{\"domain\":\"bbb.com\"},{\"domain\":\"ccc.com\"}]}";
			var mockRestClient = new MockRestClient("/SuppressionList/ImportDomains/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.AddDomainsAsync(USER_KEY, domains);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(3);
		}

		[TestMethod]
		public async Task AddDomainsToSuppressionList_with_null_array()
		{
			// Arrange
			var parameters = new Parameter[]
			{
				// There are no parameters
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":[]}";
			var mockRestClient = new MockRestClient("/SuppressionList/ImportDomains/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.AddDomainsAsync(USER_KEY, null);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(0);
		}

		[TestMethod]
		public async Task AddDomainsToSuppressionList_with_clientid()
		{
			// Arrange
			var domains = new[] { "aaa.com", "bbb.com", "ccc.com" };
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "domain[0]", Value = domains[0] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "domain[1]", Value = domains[1] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "domain[2]", Value = domains[2] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"domain\":\"aaa.com\"},{\"domain\":\"bbb.com\"},{\"domain\":\"ccc.com\"}]}";
			var mockRestClient = new MockRestClient("/SuppressionList/ImportDomains/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.AddDomainsAsync(USER_KEY, domains, CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(3);
		}

		[TestMethod]
		public async Task AddLocalPartsToSuppressionList_with_minimal_parameters()
		{
			// Arrange
			var localParts = new[] { "administrator", "manager", "info" };
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "localpart[0]", Value = localParts[0] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "localpart[1]", Value = localParts[1] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "localpart[2]", Value = localParts[2] }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"localparts\":[{\"localpart\":\"administrator\"},{\"localpart\":\"manager\"},{\"localpart\":\"info\"}]}}";
			var mockRestClient = new MockRestClient("/SuppressionList/ImportLocalparts/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.AddLocalPartsAsync(USER_KEY, localParts);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(3);
		}

		[TestMethod]
		public async Task AddLocalPartsToSuppressionList_with_null_array()
		{
			// Arrange
			var parameters = new Parameter[]
			{
				// There are no parameters
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"localparts\":[]}}";
			var mockRestClient = new MockRestClient("/SuppressionList/ImportLocalparts/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.AddLocalPartsAsync(USER_KEY, null);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(0);
		}

		[TestMethod]
		public async Task AddLocalPartsToSuppressionList_with_clientid()
		{
			// Arrange
			var localParts = new[] { "administrator", "manager", "info" };
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "localpart[0]", Value = localParts[0] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "localpart[1]", Value = localParts[1] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "localpart[2]", Value = localParts[2] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"localparts\":[{\"localpart\":\"administrator\"},{\"localpart\":\"manager\"},{\"localpart\":\"info\"}]}}";
			var mockRestClient = new MockRestClient("/SuppressionList/ImportLocalparts/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.AddLocalPartsAsync(USER_KEY, localParts, CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(3);
		}

		[TestMethod]
		public async Task RemoveEmailAddressesFromSuppressionList_with_minimal_parameters()
		{
			// Arrange
			var emailAddresses = new[] { "aaa@aaa.com", "bbb@bbb.com", "ccc@ccc.com" };
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "email[0]", Value = emailAddresses[0] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "email[1]", Value = emailAddresses[1] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "email[2]", Value = emailAddresses[2] }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"email\":\"aaa@aaa.com\"},{\"email\":\"bbb@bbb.com\"},{\"email\":\"ccc@ccc.com\"}]}";
			var mockRestClient = new MockRestClient("/SuppressionList/DeleteEmails/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.RemoveEmailAddressesAsync(USER_KEY, emailAddresses);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(3);
		}

		[TestMethod]
		public async Task RemoveEmailAddressesFromSuppressionList_with_null_array()
		{
			// Arrange
			var parameters = new Parameter[]
			{
				// There are no parameters
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":[]}";
			var mockRestClient = new MockRestClient("/SuppressionList/DeleteEmails/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.RemoveEmailAddressesAsync(USER_KEY, null);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(0);
		}

		[TestMethod]
		public async Task RemoveEmailAddressesFromSuppressionList_with_clientid()
		{
			// Arrange
			var emailAddresses = new[] { "aaa@aaa.com", "bbb@bbb.com", "ccc@ccc.com" };
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "email[0]", Value = emailAddresses[0] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "email[1]", Value = emailAddresses[1] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "email[2]", Value = emailAddresses[2] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"email\":\"aaa@aaa.com\"},{\"email\":\"bbb@bbb.com\"},{\"email\":\"ccc@ccc.com\"}]}";
			var mockRestClient = new MockRestClient("/SuppressionList/DeleteEmails/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.RemoveEmailAddressesAsync(USER_KEY, emailAddresses, CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(3);
		}

		[TestMethod]
		public async Task RemoveDomainsFromSuppressionList_with_minimal_parameters()
		{
			// Arrange
			var domains = new[] { "aaa.com", "bbb.com", "ccc.com" };
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "domain[0]", Value = domains[0] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "domain[1]", Value = domains[1] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "domain[2]", Value = domains[2] }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"domain\":\"aaa.com\"},{\"domain\":\"bbb.com\"},{\"domain\":\"ccc.com\"}]}";
			var mockRestClient = new MockRestClient("/SuppressionList/DeleteDomains/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.RemoveDomainsAsync(USER_KEY, domains);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(3);
		}

		[TestMethod]
		public async Task RemoveDomainsFromSuppressionList_with_null_array()
		{
			// Arrange
			var parameters = new Parameter[]
			{
				// There are no parameters
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":[]}";
			var mockRestClient = new MockRestClient("/SuppressionList/DeleteDomains/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.RemoveDomainsAsync(USER_KEY, null);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(0);
		}

		[TestMethod]
		public async Task RemoveDomainsFromSuppressionList_with_clientid()
		{
			// Arrange
			var domains = new[] { "aaa.com", "bbb.com", "ccc.com" };
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "domain[0]", Value = domains[0] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "domain[1]", Value = domains[1] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "domain[2]", Value = domains[2] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":[{\"domain\":\"aaa.com\"},{\"domain\":\"bbb.com\"},{\"domain\":\"ccc.com\"}]}";
			var mockRestClient = new MockRestClient("/SuppressionList/DeleteDomains/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.RemoveDomainsAsync(USER_KEY, domains, CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(3);
		}

		[TestMethod]
		public async Task RemoveLocalPartsFromSuppressionList_with_minimal_parameters()
		{
			// Arrange
			var localParts = new[] { "administrator", "manager", "info" };
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "localpart[0]", Value = localParts[0] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "localpart[1]", Value = localParts[1] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "localpart[2]", Value = localParts[2] }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"localparts\":[{\"localpart\":\"administrator\"},{\"localpart\":\"manager\"},{\"localpart\":\"info\"}]}}";
			var mockRestClient = new MockRestClient("/SuppressionList/DeleteLocalparts/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.RemoveLocalPartsAsync(USER_KEY, localParts);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(3);
		}

		[TestMethod]
		public async Task RemoveLocalPartsFromSuppressionList_with_null_array()
		{
			// Arrange
			var parameters = new Parameter[]
			{
				// There are no parameters
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"localparts\":[]}}";
			var mockRestClient = new MockRestClient("/SuppressionList/DeleteLocalparts/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.RemoveLocalPartsAsync(USER_KEY, null);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(0);
		}

		[TestMethod]
		public async Task RemoveLocalPartsFromSuppressionList_with_clientid()
		{
			// Arrange
			var localParts = new[] { "administrator", "manager", "info" };
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "localpart[0]", Value = localParts[0] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "localpart[1]", Value = localParts[1] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "localpart[2]", Value = localParts[2] },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"localparts\":[{\"localpart\":\"administrator\"},{\"localpart\":\"manager\"},{\"localpart\":\"info\"}]}}";
			var mockRestClient = new MockRestClient("/SuppressionList/DeleteLocalparts/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.RemoveLocalPartsAsync(USER_KEY, localParts, CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(3);
		}

		[TestMethod]
		public async Task GetSuppressedEmailAddresses_with_minimal_parameters()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"emails\":[{\"email\":\"aaa@aaa.com\",\"source_type\":\"manual\"},{\"email\":\"bbb@bbb.com\",\"source_type\":\"manual\"}]}}";
			var mockRestClient = new MockRestClient("/SuppressionList/ExportEmails/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.GetEmailAddressesAsync(USER_KEY);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[TestMethod]
		public async Task GetSuppressedEmailAddresses_with_limit()
		{
			// Arrange
			var limit = 5;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "limit", Value = limit },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"emails\":[{\"email\":\"aaa@aaa.com\",\"source_type\":\"manual\"},{\"email\":\"bbb@bbb.com\",\"source_type\":\"manual\"}]}}";
			var mockRestClient = new MockRestClient("/SuppressionList/ExportEmails/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.GetEmailAddressesAsync(USER_KEY, limit: limit);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[TestMethod]
		public async Task GetSuppressedEmailAddresses_with_offset()
		{
			// Arrange
			var offset = 25;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "offset", Value = offset },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"emails\":[{\"email\":\"aaa@aaa.com\",\"source_type\":\"manual\"},{\"email\":\"bbb@bbb.com\",\"source_type\":\"manual\"}]}}";
			var mockRestClient = new MockRestClient("/SuppressionList/ExportEmails/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.GetEmailAddressesAsync(USER_KEY, offset: offset);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[TestMethod]
		public async Task GetSuppressedEmailAddresses_with_clientid()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"emails\":[{\"email\":\"aaa@aaa.com\",\"source_type\":\"manual\"},{\"email\":\"bbb@bbb.com\",\"source_type\":\"manual\"}]}}";
			var mockRestClient = new MockRestClient("/SuppressionList/ExportEmails/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.GetEmailAddressesAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[TestMethod]
		public async Task GetSuppressedDomains_with_minimal_parameters()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"domains\":[{\"domain\":\"aaa.org\"},{\"domain\":\"bbb.com\"}]}}";
			var mockRestClient = new MockRestClient("/SuppressionList/ExportDomains/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.GetDomainsAsync(USER_KEY);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[TestMethod]
		public async Task GetSuppressedDomains_with_limit()
		{
			// Arrange
			var limit = 5;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "limit", Value = limit },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"domains\":[{\"domain\":\"aaa.org\"},{\"domain\":\"bbb.com\"}]}}";
			var mockRestClient = new MockRestClient("/SuppressionList/ExportDomains/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.GetDomainsAsync(USER_KEY, limit: limit);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[TestMethod]
		public async Task GetSuppressedDomains_with_offset()
		{
			// Arrange
			var offset = 25;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "offset", Value = offset },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"domains\":[{\"domain\":\"aaa.org\"},{\"domain\":\"bbb.com\"}]}}";
			var mockRestClient = new MockRestClient("/SuppressionList/ExportDomains/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.GetDomainsAsync(USER_KEY, offset: offset);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[TestMethod]
		public async Task GetSuppressedDomains_with_clientid()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"domains\":[{\"domain\":\"aaa.org\"},{\"domain\":\"bbb.com\"}]}}";
			var mockRestClient = new MockRestClient("/SuppressionList/ExportDomains/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.GetDomainsAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[TestMethod]
		public async Task GetSuppressedLocalParts_with_minimal_parameters()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"localparts\":[{\"localpart\":\"administrator\"},{\"localpart\":\"info\"}]}}";
			var mockRestClient = new MockRestClient("/SuppressionList/ExportLocalparts/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.GetLocalPartsAsync(USER_KEY);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[TestMethod]
		public async Task GetSuppressedLocalParts_with_limit()
		{
			// Arrange
			var limit = 5;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "limit", Value = limit },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"localparts\":[{\"localpart\":\"administrator\"},{\"localpart\":\"info\"}]}}";
			var mockRestClient = new MockRestClient("/SuppressionList/ExportLocalparts/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.GetLocalPartsAsync(USER_KEY, limit: limit);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[TestMethod]
		public async Task GetSuppressedLocalParts_with_offset()
		{
			// Arrange
			var offset = 25;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "offset", Value = offset },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"localparts\":[{\"localpart\":\"administrator\"},{\"localpart\":\"info\"}]}}";
			var mockRestClient = new MockRestClient("/SuppressionList/ExportLocalparts/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.GetLocalPartsAsync(USER_KEY, offset: offset);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[TestMethod]
		public async Task GetSuppressedLocalParts_with_clientid()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"localparts\":[{\"localpart\":\"administrator\"},{\"localpart\":\"info\"}]}}";
			var mockRestClient = new MockRestClient("/SuppressionList/ExportLocalparts/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.GetLocalPartsAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[TestMethod]
		public async Task GetSuppressedEmailAddressesCount_with_minimal_parameters()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/SuppressionList/ExportEmails/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.GetEmailAddressesCountAsync(USER_KEY);

			// Assert
			result.ShouldBe(2);
		}

		[TestMethod]
		public async Task GetSuppressedEmailAddressesCount_with_clientid()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/SuppressionList/ExportEmails/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.GetEmailAddressesCountAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			result.ShouldBe(2);
		}

		[TestMethod]
		public async Task GetSuppressedDomainsCount_with_minimal_parameters()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/SuppressionList/ExportDomains/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.GetDomainsCountAsync(USER_KEY);

			// Assert
			result.ShouldBe(2);
		}

		[TestMethod]
		public async Task GetSuppressedDomainsCount_with_clientid()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/SuppressionList/ExportDomains/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.GetDomainsCountAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			result.ShouldBe(2);
		}

		[TestMethod]
		public async Task GetSuppressedLocalPartsCount_with_minimal_parameters()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/SuppressionList/ExportLocalparts/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.GetLocalPartsCountAsync(USER_KEY);

			// Assert
			result.ShouldBe(2);
		}

		[TestMethod]
		public async Task GetSuppressedLocalPartsCount_with_clientid()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/SuppressionList/ExportLocalparts/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.SuppressionLists.GetLocalPartsCountAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			result.ShouldBe(2);
		}
	}
}
