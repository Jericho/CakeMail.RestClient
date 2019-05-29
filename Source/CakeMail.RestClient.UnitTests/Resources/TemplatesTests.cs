using RichardSzalay.MockHttp;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CakeMail.RestClient.UnitTests.Resources
{
	public class TemplatesTests
	{
		private const string API_KEY = "...dummy API key...";
		private const string USER_KEY = "...dummy USER key...";
		private const long CLIENT_ID = 999;
		private const long TEMPLATE_ID = 123;
		private const long CATEGORY_ID = 111;

		[Fact]
		public async Task CreateTemplateCategory_with_minimal_parameters()
		{
			// Arrange
			var labels = new Dictionary<string, string>
			{
				{ "en_US", "My Category" },
				{ "fr_FR", "Ma Catégorie" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\"}}}}", CATEGORY_ID);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/CreateCategory")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.CreateCategoryAsync(USER_KEY, labels);

			// Assert
			result.ShouldBe(CATEGORY_ID);
		}

		[Fact]
		public async Task CreateTemplateCategory_with_no_labels()
		{
			// Arrange
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\"}}}}", CATEGORY_ID);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/CreateCategory")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.CreateCategoryAsync(USER_KEY, null);

			// Assert
			result.ShouldBe(CATEGORY_ID);
		}

		[Fact]
		public async Task CreateTemplateCategory_with_isvisiblebydefault_false()
		{
			// Arrange
			var labels = new Dictionary<string, string>
			{
				{ "en_US", "My Category" },
				{ "fr_FR", "Ma Catégorie" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\"}}}}", CATEGORY_ID);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/CreateCategory")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.CreateCategoryAsync(USER_KEY, labels, isVisibleByDefault: false);

			// Assert
			result.ShouldBe(CATEGORY_ID);
		}

		[Fact]
		public async Task CreateTemplateCategory_with_templatescanbecopied_false()
		{
			// Arrange
			var labels = new Dictionary<string, string>
			{
				{ "en_US", "My Category" },
				{ "fr_FR", "Ma Catégorie" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\"}}}}", CATEGORY_ID);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/CreateCategory")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.CreateCategoryAsync(USER_KEY, labels, templatesCanBeCopied: false);

			// Assert
			result.ShouldBe(CATEGORY_ID);
		}

		[Fact]
		public async Task CreateTemplateCategory_with_clientid()
		{
			// Arrange
			var labels = new Dictionary<string, string>
			{
				{ "en_US", "My Category" },
				{ "fr_FR", "Ma Catégorie" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\"}}}}", CATEGORY_ID);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/CreateCategory")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.CreateCategoryAsync(USER_KEY, labels, clientId: CLIENT_ID);

			// Assert
			result.ShouldBe(CATEGORY_ID);
		}

		[Fact]
		public async Task DeleteTemplateCategory_with_minimal_parameters()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/DeleteCategory")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.DeleteCategoryAsync(USER_KEY, CATEGORY_ID);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task DeleteTemplateCategory_with_clientid()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/DeleteCategory")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.DeleteCategoryAsync(USER_KEY, CATEGORY_ID, CLIENT_ID);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task GetTemplateCategory_with_minimal_parameters()
		{
			// Arrange
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"status\":\"active\",\"owner_client_id\":\"{1}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #1\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}}}", CATEGORY_ID, CLIENT_ID);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/GetCategory")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.GetCategoryAsync(USER_KEY, CATEGORY_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(CATEGORY_ID);
		}

		[Fact]
		public async Task GetTemplateCategory_with_clientid()
		{
			// Arrange
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"status\":\"active\",\"owner_client_id\":\"{1}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #1\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}}}", CATEGORY_ID, CLIENT_ID);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/GetCategory")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.GetCategoryAsync(USER_KEY, CATEGORY_ID, CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(CATEGORY_ID);
		}

		[Fact]
		public async Task GetTemplateCategories_with_minimal_parameters()
		{
			// Arrange
			var jsonCategory1 = string.Format("{{\"id\":\"111\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #1\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);
			var jsonCategory2 = string.Format("{{\"id\":\"222\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #2\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"categories\":[{0},{1}]}}}}", jsonCategory1, jsonCategory2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/GetCategories")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.GetCategoriesAsync(USER_KEY);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(111);
			result.ToArray()[1].Id.ShouldBe(222);
		}

		[Fact]
		public async Task GetTemplateCategories_with_limit()
		{
			// Arrange
			var limit = 5;
			var jsonCategory1 = string.Format("{{\"id\":\"111\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #1\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);
			var jsonCategory2 = string.Format("{{\"id\":\"222\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #2\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"categories\":[{0},{1}]}}}}", jsonCategory1, jsonCategory2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/GetCategories")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.GetCategoriesAsync(USER_KEY, limit: limit);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(111);
			result.ToArray()[1].Id.ShouldBe(222);
		}

		[Fact]
		public async Task GetTemplateCategories_with_offset()
		{
			// Arrange
			var offset = 25;
			var jsonCategory1 = string.Format("{{\"id\":\"111\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #1\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);
			var jsonCategory2 = string.Format("{{\"id\":\"222\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #2\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"categories\":[{0},{1}]}}}}", jsonCategory1, jsonCategory2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/GetCategories")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.GetCategoriesAsync(USER_KEY, offset: offset);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(111);
			result.ToArray()[1].Id.ShouldBe(222);
		}

		[Fact]
		public async Task GetTemplateCategories_with_clientid()
		{
			// Arrange
			var jsonCategory1 = string.Format("{{\"id\":\"111\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #1\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);
			var jsonCategory2 = string.Format("{{\"id\":\"222\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #2\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"categories\":[{0},{1}]}}}}", jsonCategory1, jsonCategory2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/GetCategories")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.GetCategoriesAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(111);
			result.ToArray()[1].Id.ShouldBe(222);
		}

		[Fact]
		public async Task GetTemplateCategoriesCount_with_minimal_parameters()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/GetCategories")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.GetCategoriesCountAsync(USER_KEY);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetTemplateCategoriesCount_with_clientid()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/GetCategories")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.GetCategoriesCountAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task UpdateTemplateCategory_with_minimal_parameters()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/SetCategory")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.UpdateCategoryAsync(USER_KEY, CATEGORY_ID, null);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateTemplateCategory_with_labels()
		{
			// Arrange
			var labels = new Dictionary<string, string>
			{
				{ "en_US", "My Category" },
				{ "fr_FR", "Ma Catégorie" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/SetCategory")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.UpdateCategoryAsync(USER_KEY, CATEGORY_ID, labels);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateTemplateCategory_with_default_true()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/SetCategory")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.UpdateCategoryAsync(USER_KEY, CATEGORY_ID, null, isVisibleByDefault: true);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateTemplateCategory_with_default_false()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/SetCategory")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.UpdateCategoryAsync(USER_KEY, CATEGORY_ID, null, isVisibleByDefault: false);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateTemplateCategory_with_copyable_true()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/SetCategory")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.UpdateCategoryAsync(USER_KEY, CATEGORY_ID, null, templatesCanBeCopied: true);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateTemplateCategory_with_copyable_false()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/SetCategory")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.UpdateCategoryAsync(USER_KEY, CATEGORY_ID, null, templatesCanBeCopied: false);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateTemplateCategory_with_clientid()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/SetCategory")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.UpdateCategoryAsync(USER_KEY, CATEGORY_ID, null, clientId: CLIENT_ID);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task GetTemplateCategoryVisibility_with_minimal_parameters()
		{
			// Arrange
			var jsonVisibility1 = "{\"client_id\":\"111\",\"company_name\":\"Fictitious Company #1\",\"visible\":\"1\"}";
			var jsonVisibility2 = "{\"client_id\":\"111\",\"company_name\":\"Fictitious Company #2\",\"visible\":\"0\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonVisibility1, jsonVisibility2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/GetCategoryVisibility")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.GetCategoryVisibilityAsync(USER_KEY, CATEGORY_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Visible.ShouldBeTrue();
			result.ToArray()[1].Visible.ShouldBeFalse();
		}

		[Fact]
		public async Task GetTemplateCategoryVisibility_with_limit()
		{
			// Arrange
			var limit = 5;
			var jsonVisibility1 = "{\"client_id\":\"111\",\"company_name\":\"Fictitious Company #1\",\"visible\":\"1\"}";
			var jsonVisibility2 = "{\"client_id\":\"111\",\"company_name\":\"Fictitious Company #2\",\"visible\":\"0\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonVisibility1, jsonVisibility2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/GetCategoryVisibility")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.GetCategoryVisibilityAsync(USER_KEY, CATEGORY_ID, limit: limit);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetTemplateCategoryVisibility_with_offset()
		{
			// Arrange
			var offset = 25;
			var jsonVisibility1 = "{\"client_id\":\"111\",\"company_name\":\"Fictitious Company #1\",\"visible\":\"1\"}";
			var jsonVisibility2 = "{\"client_id\":\"111\",\"company_name\":\"Fictitious Company #2\",\"visible\":\"0\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonVisibility1, jsonVisibility2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/GetCategoryVisibility")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.GetCategoryVisibilityAsync(USER_KEY, CATEGORY_ID, offset: offset);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetTemplateCategoryVisibility_with_clientid()
		{
			// Arrange
			var jsonVisibility1 = "{\"client_id\":\"111\",\"company_name\":\"Fictitious Company #1\",\"visible\":\"1\"}";
			var jsonVisibility2 = "{\"client_id\":\"111\",\"company_name\":\"Fictitious Company #2\",\"visible\":\"0\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonVisibility1, jsonVisibility2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/GetCategoryVisibility")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.GetCategoryVisibilityAsync(USER_KEY, CATEGORY_ID, clientId: CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
		}

		[Fact]
		public async Task GetTemplateCategoryVisibilityCount_with_minimal_parameters()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/GetCategoryVisibility")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.GetCategoryVisibilityCountAsync(USER_KEY, CATEGORY_ID);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetTemplateCategoryVisibilityCount_with_clientid()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/GetCategoryVisibility")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.GetCategoryVisibilityCountAsync(USER_KEY, CATEGORY_ID, clientId: CLIENT_ID);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task SetTemplateCategoryVisibility_with_minimal_parameters()
		{
			// Arrange
			var clientVisibility = new Dictionary<long, bool>
			{
				{ 111L, true },
				{ 222L, false }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/SetCategoryVisibility")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.SetCategoryVisibilityAsync(USER_KEY, CATEGORY_ID, clientVisibility);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task SetTemplateCategoryVisibility_with_empty_array()
		{
			// Arrange
			var clientVisibility = (IDictionary<long, bool>)null;
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/SetCategoryVisibility")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.SetCategoryVisibilityAsync(USER_KEY, CATEGORY_ID, clientVisibility);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task SetTemplateCategoryVisibility_with_clientid()
		{
			// Arrange
			var clientVisibility = new Dictionary<long, bool>
			{
				{ 111L, true },
				{ 222L, false }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/SetCategoryVisibility")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.SetCategoryVisibilityAsync(USER_KEY, CATEGORY_ID, clientVisibility, CLIENT_ID);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task CreateTemplate_with_minimal_parameters()
		{
			// Arrange
			var content = "Sample content 1";
			var labels = new Dictionary<string, string>
			{
				{ "en_US", "My Template" },
				{ "fr_FR", "Mon Modèle" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\"}}}}", TEMPLATE_ID);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/CreateTemplate")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.CreateAsync(USER_KEY, labels, content, CATEGORY_ID);

			// Assert
			result.ShouldBe(TEMPLATE_ID);
		}

		[Fact]
		public async Task CreateTemplate_with_no_labels()
		{
			// Arrange
			var content = "Sample content 2";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\"}}}}", TEMPLATE_ID);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/CreateTemplate")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.CreateAsync(USER_KEY, null, content, CATEGORY_ID);

			// Assert
			result.ShouldBe(TEMPLATE_ID);
		}

		[Fact]
		public async Task CreateTemplate_with_clientid()
		{
			// Arrange
			var content = "Sample content 3";
			var labels = new Dictionary<string, string>
			{
				{ "en_US", "My Template" },
				{ "fr_FR", "Mon Modèle" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\"}}}}", TEMPLATE_ID);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/CreateTemplate")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.CreateAsync(USER_KEY, labels, content, CATEGORY_ID, clientId: CLIENT_ID);

			// Assert
			result.ShouldBe(TEMPLATE_ID);
		}

		[Fact]
		public async Task DeleteTemplate_with_minimal_parameters()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/DeleteTemplate")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.DeleteAsync(USER_KEY, TEMPLATE_ID);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task DeleteTemplate_with_clientid()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/DeleteTemplate")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.DeleteAsync(USER_KEY, TEMPLATE_ID, CLIENT_ID);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task GetTemplate_with_minimal_parameters()
		{
			// Arrange
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"labels\":[],\"id\":\"{0}\",\"status\":\"active\",\"category_id\":\"7480\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"aaa\",\"description\":\"aaa\"}}}}", TEMPLATE_ID);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/GetTemplate")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.GetAsync(USER_KEY, TEMPLATE_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(TEMPLATE_ID);
		}

		[Fact]
		public async Task GetTemplate_with_clientid()
		{
			// Arrange
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"labels\":[],\"id\":\"{0}\",\"status\":\"active\",\"category_id\":\"7480\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"aaa\",\"description\":\"aaa\"}}}}", TEMPLATE_ID);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/GetTemplate")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.GetAsync(USER_KEY, TEMPLATE_ID, CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Id.ShouldBe(TEMPLATE_ID);
		}

		[Fact]
		public async Task GetTemplates_with_minimal_parameters()
		{
			// Arrange
			var jsonTemplate1 = "{\"labels\":[],\"id\":\"111\",\"status\":\"active\",\"category_id\":\"123\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"aaa\",\"description\":\"aaa\"}";
			var jsonTemplate2 = "{\"labels\":[],\"id\":\"222\",\"status\":\"active\",\"category_id\":\"123\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"bbb\",\"description\":\"bbb\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"templates\":[{0},{1}]}}}}", jsonTemplate1, jsonTemplate2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/GetTemplates")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.GetTemplatesAsync(USER_KEY);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(111);
			result.ToArray()[1].Id.ShouldBe(222);
		}

		[Fact]
		public async Task GetTemplates_with_CATEGORY_ID()
		{
			// Arrange
			var jsonTemplate1 = "{\"labels\":[],\"id\":\"111\",\"status\":\"active\",\"category_id\":\"123\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"aaa\",\"description\":\"aaa\"}";
			var jsonTemplate2 = "{\"labels\":[],\"id\":\"222\",\"status\":\"active\",\"category_id\":\"123\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"bbb\",\"description\":\"bbb\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"templates\":[{0},{1}]}}}}", jsonTemplate1, jsonTemplate2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/GetTemplates")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.GetTemplatesAsync(USER_KEY, categoryId: CATEGORY_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(111);
			result.ToArray()[1].Id.ShouldBe(222);
		}

		[Fact]
		public async Task GetTemplates_with_limit()
		{
			// Arrange
			var limit = 5;
			var jsonTemplate1 = "{\"labels\":[],\"id\":\"111\",\"status\":\"active\",\"category_id\":\"123\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"aaa\",\"description\":\"aaa\"}";
			var jsonTemplate2 = "{\"labels\":[],\"id\":\"222\",\"status\":\"active\",\"category_id\":\"123\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"bbb\",\"description\":\"bbb\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"templates\":[{0},{1}]}}}}", jsonTemplate1, jsonTemplate2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/GetTemplates")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.GetTemplatesAsync(USER_KEY, limit: limit);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(111);
			result.ToArray()[1].Id.ShouldBe(222);
		}

		[Fact]
		public async Task GetTemplates_with_offset()
		{
			// Arrange
			var offset = 25;
			var jsonTemplate1 = "{\"labels\":[],\"id\":\"111\",\"status\":\"active\",\"category_id\":\"123\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"aaa\",\"description\":\"aaa\"}";
			var jsonTemplate2 = "{\"labels\":[],\"id\":\"222\",\"status\":\"active\",\"category_id\":\"123\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"bbb\",\"description\":\"bbb\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"templates\":[{0},{1}]}}}}", jsonTemplate1, jsonTemplate2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/GetTemplates")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.GetTemplatesAsync(USER_KEY, offset: offset);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(111);
			result.ToArray()[1].Id.ShouldBe(222);
		}

		[Fact]
		public async Task GetTemplates_with_clientid()
		{
			// Arrange
			var jsonTemplate1 = "{\"labels\":[],\"id\":\"111\",\"status\":\"active\",\"category_id\":\"123\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"aaa\",\"description\":\"aaa\"}";
			var jsonTemplate2 = "{\"labels\":[],\"id\":\"222\",\"status\":\"active\",\"category_id\":\"123\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"bbb\",\"description\":\"bbb\"}";
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"templates\":[{0},{1}]}}}}", jsonTemplate1, jsonTemplate2);
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/GetTemplates")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.GetTemplatesAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			result.ShouldNotBeNull();
			result.Count().ShouldBe(2);
			result.ToArray()[0].Id.ShouldBe(111);
			result.ToArray()[1].Id.ShouldBe(222);
		}

		[Fact]
		public async Task GetTemplatesCount_with_minimal_parameters()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/GetTemplates")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.GetCountAsync(USER_KEY);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetTemplatesCount_with_CATEGORY_ID()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/GetTemplates")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.GetCountAsync(USER_KEY, categoryId: CATEGORY_ID);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task GetTemplatesCount_with_clientid()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/GetTemplates")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.GetCountAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			result.ShouldBe(2);
		}

		[Fact]
		public async Task UpdateTemplate_with_minimal_parameters()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/SetTemplate")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.UpdateAsync(USER_KEY, TEMPLATE_ID, null);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateTemplate_with_labels()
		{
			// Arrange
			var labels = new Dictionary<string, string>
			{
				{ "en_US", "My Category" },
				{ "fr_FR", "Ma Catégorie" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/SetTemplate")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.UpdateAsync(USER_KEY, TEMPLATE_ID, labels);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateTemplate_with_content()
		{
			// Arrange
			var content = "Sample content 4";
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/SetTemplate")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.UpdateAsync(USER_KEY, TEMPLATE_ID, null, content: content);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateTemplate_with_CATEGORY_ID()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/SetTemplate")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.UpdateAsync(USER_KEY, TEMPLATE_ID, null, categoryId: CATEGORY_ID);

			// Assert
			result.ShouldBeTrue();
		}

		[Fact]
		public async Task UpdateTemplate_with_clientid()
		{
			// Arrange
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockHttp = new MockHttpMessageHandler();
			mockHttp.Expect(HttpMethod.Post, Utils.GetCakeMailApiUri("TemplateV2/SetTemplate")).Respond("application/json", jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, httpClient: mockHttp.ToHttpClient());
			var result = await apiClient.Templates.UpdateAsync(USER_KEY, TEMPLATE_ID, null, clientId: CLIENT_ID);

			// Assert
			result.ShouldBeTrue();
		}
	}
}
