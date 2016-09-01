using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeMail.RestClient.UnitTests
{
	[TestClass]
	public class TemplatesTests
	{
		private const string API_KEY = "...dummy API key...";
		private const string USER_KEY = "...dummy USER key...";
		private const long CLIENT_ID = 999;
		private const long TEMPLATE_ID = 123;
		private const long CATEGORY_ID = 111;

		[TestMethod]
		public async Task CreateTemplateCategory_with_minimal_parameters()
		{
			// Arrange
			var labels = new Dictionary<string, string>
			{
				{ "en_US", "My Category" },
				{ "fr_FR", "Ma Catégorie" }
			};
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "default", Value = "1" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "templates_copyable", Value = "1" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "label[0][language]", Value = "en_US" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "label[0][name]", Value = "My Category" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "label[1][language]", Value = "fr_FR" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "label[1][name]", Value = "Ma Catégorie" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\"}}}}", CATEGORY_ID);
			var mockRestClient = new MockRestClient("/TemplateV2/CreateCategory/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.CreateCategoryAsync(USER_KEY, labels);

			// Assert
			Assert.AreEqual(CATEGORY_ID, result);
		}

		[TestMethod]
		public async Task CreateTemplateCategory_with_no_labels()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "default", Value = "1" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "templates_copyable", Value = "1" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\"}}}}", CATEGORY_ID);
			var mockRestClient = new MockRestClient("/TemplateV2/CreateCategory/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.CreateCategoryAsync(USER_KEY, null);

			// Assert
			Assert.AreEqual(CATEGORY_ID, result);
		}

		[TestMethod]
		public async Task CreateTemplateCategory_with_isvisiblebydefault_false()
		{
			// Arrange
			var labels = new Dictionary<string, string>
			{
				{ "en_US", "My Category" },
				{ "fr_FR", "Ma Catégorie" }
			};
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "default", Value = "0" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "templates_copyable", Value = "1" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "label[0][language]", Value = "en_US" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "label[0][name]", Value = "My Category" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "label[1][language]", Value = "fr_FR" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "label[1][name]", Value = "Ma Catégorie" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\"}}}}", CATEGORY_ID);
			var mockRestClient = new MockRestClient("/TemplateV2/CreateCategory/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.CreateCategoryAsync(USER_KEY, labels, isVisibleByDefault: false);

			// Assert
			Assert.AreEqual(CATEGORY_ID, result);
		}

		[TestMethod]
		public async Task CreateTemplateCategory_with_templatescanbecopied_false()
		{
			// Arrange
			var labels = new Dictionary<string, string>
			{
				{ "en_US", "My Category" },
				{ "fr_FR", "Ma Catégorie" }
			};
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "default", Value = "1" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "templates_copyable", Value = "0" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "label[0][language]", Value = "en_US" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "label[0][name]", Value = "My Category" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "label[1][language]", Value = "fr_FR" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "label[1][name]", Value = "Ma Catégorie" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\"}}}}", CATEGORY_ID);
			var mockRestClient = new MockRestClient("/TemplateV2/CreateCategory/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.CreateCategoryAsync(USER_KEY, labels, templatesCanBeCopied: false);

			// Assert
			Assert.AreEqual(CATEGORY_ID, result);
		}

		[TestMethod]
		public async Task CreateTemplateCategory_with_clientid()
		{
			// Arrange
			var labels = new Dictionary<string, string>
			{
				{ "en_US", "My Category" },
				{ "fr_FR", "Ma Catégorie" }
			};
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "default", Value = "1" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "templates_copyable", Value = "1" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "label[0][language]", Value = "en_US" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "label[0][name]", Value = "My Category" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "label[1][language]", Value = "fr_FR" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "label[1][name]", Value = "Ma Catégorie" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
				};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\"}}}}", CATEGORY_ID);
			var mockRestClient = new MockRestClient("/TemplateV2/CreateCategory/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.CreateCategoryAsync(USER_KEY, labels, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(CATEGORY_ID, result);
		}

		[TestMethod]
		public async Task DeleteTemplateCategory_with_minimal_parameters()
		{
			// Arrange
			var CATEGORY_ID = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "category_id", Value = CATEGORY_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/TemplateV2/DeleteCategory/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.DeleteCategoryAsync(USER_KEY, CATEGORY_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task DeleteTemplateCategory_with_clientid()
		{
			// Arrange
			var CATEGORY_ID = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "category_id", Value = CATEGORY_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/TemplateV2/DeleteCategory/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.DeleteCategoryAsync(USER_KEY, CATEGORY_ID, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task GetTemplateCategory_with_minimal_parameters()
		{
			// Arrange
			var CATEGORY_ID = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "category_id", Value = CATEGORY_ID }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"status\":\"active\",\"owner_client_id\":\"{1}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #1\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}}}", CATEGORY_ID, CLIENT_ID);
			var mockRestClient = new MockRestClient("/TemplateV2/GetCategory/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.GetCategoryAsync(USER_KEY, CATEGORY_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(CATEGORY_ID, result.Id);
		}

		[TestMethod]
		public async Task GetTemplateCategory_with_clientid()
		{
			// Arrange
			var CATEGORY_ID = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "category_id", Value = CATEGORY_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"id\":\"{0}\",\"status\":\"active\",\"owner_client_id\":\"{1}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #1\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}}}", CATEGORY_ID, CLIENT_ID);
			var mockRestClient = new MockRestClient("/TemplateV2/GetCategory/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.GetCategoryAsync(USER_KEY, CATEGORY_ID, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(CATEGORY_ID, result.Id);
		}

		[TestMethod]
		public async Task GetTemplateCategories_with_minimal_parameters()
		{
			// Arrange
			var jsonCategory1 = string.Format("{{\"id\":\"111\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #1\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);
			var jsonCategory2 = string.Format("{{\"id\":\"222\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #2\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"categories\":[{0},{1}]}}}}", jsonCategory1, jsonCategory2);
			var mockRestClient = new MockRestClient("/TemplateV2/GetCategories/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.GetCategoriesAsync(USER_KEY);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(111, result.ToArray()[0].Id);
			Assert.AreEqual(222, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetTemplateCategories_with_limit()
		{
			// Arrange
			var limit = 5;
			var jsonCategory1 = string.Format("{{\"id\":\"111\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #1\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);
			var jsonCategory2 = string.Format("{{\"id\":\"222\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #2\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "limit", Value = limit },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"categories\":[{0},{1}]}}}}", jsonCategory1, jsonCategory2);
			var mockRestClient = new MockRestClient("/TemplateV2/GetCategories/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.GetCategoriesAsync(USER_KEY, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(111, result.ToArray()[0].Id);
			Assert.AreEqual(222, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetTemplateCategories_with_offset()
		{
			// Arrange
			var offset = 25;
			var jsonCategory1 = string.Format("{{\"id\":\"111\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #1\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);
			var jsonCategory2 = string.Format("{{\"id\":\"222\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #2\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "offset", Value = offset },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"categories\":[{0},{1}]}}}}", jsonCategory1, jsonCategory2);
			var mockRestClient = new MockRestClient("/TemplateV2/GetCategories/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.GetCategoriesAsync(USER_KEY, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(111, result.ToArray()[0].Id);
			Assert.AreEqual(222, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetTemplateCategories_with_clientid()
		{
			// Arrange
			var jsonCategory1 = string.Format("{{\"id\":\"111\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #1\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);
			var jsonCategory2 = string.Format("{{\"id\":\"222\",\"status\":\"active\",\"owner_client_id\":\"{0}\",\"templates_copyable\":\"1\",\"default\":\"0\",\"name\":\"Testing #2\",\"amount_templates\":\"1\",\"amount_clients\":\"0\",\"level\":\"1\"}}", CLIENT_ID);
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"categories\":[{0},{1}]}}}}", jsonCategory1, jsonCategory2);
			var mockRestClient = new MockRestClient("/TemplateV2/GetCategories/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.GetCategoriesAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(111, result.ToArray()[0].Id);
			Assert.AreEqual(222, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetTemplateCategoriesCount_with_minimal_parameters()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/TemplateV2/GetCategories/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.GetCategoriesCountAsync(USER_KEY);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetTemplateCategoriesCount_with_clientid()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/TemplateV2/GetCategories/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.GetCategoriesCountAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task UpdateTemplateCategory_with_minimal_parameters()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "category_id", Value = CATEGORY_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "default", Value = "1" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "templates_copyable", Value = "1" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/TemplateV2/SetCategory/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.UpdateCategoryAsync(USER_KEY, CATEGORY_ID, null);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTemplateCategory_with_labels()
		{
			// Arrange
			var labels = new Dictionary<string, string>
			{
				{ "en_US", "My Category" },
				{ "fr_FR", "Ma Catégorie" }
			};
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "category_id", Value = CATEGORY_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "default", Value = "1" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "templates_copyable", Value = "1" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "label[en_US]", Value = "My Category" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "label[fr_FR]", Value = "Ma Catégorie" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/TemplateV2/SetCategory/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.UpdateCategoryAsync(USER_KEY, CATEGORY_ID, labels);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTemplateCategory_with_default_true()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "category_id", Value = CATEGORY_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "default", Value = "1" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "templates_copyable", Value = "1" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/TemplateV2/SetCategory/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.UpdateCategoryAsync(USER_KEY, CATEGORY_ID, null, isVisibleByDefault: true);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTemplateCategory_with_default_false()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "category_id", Value = CATEGORY_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "default", Value = "0" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "templates_copyable", Value = "1" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/TemplateV2/SetCategory/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.UpdateCategoryAsync(USER_KEY, CATEGORY_ID, null, isVisibleByDefault: false);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTemplateCategory_with_copyable_true()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "category_id", Value = CATEGORY_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "default", Value = "1" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "templates_copyable", Value = "1" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/TemplateV2/SetCategory/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.UpdateCategoryAsync(USER_KEY, CATEGORY_ID, null, templatesCanBeCopied: true);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTemplateCategory_with_copyable_false()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "category_id", Value = CATEGORY_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "default", Value = "1" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "templates_copyable", Value = "0" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/TemplateV2/SetCategory/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.UpdateCategoryAsync(USER_KEY, CATEGORY_ID, null, templatesCanBeCopied: false);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTemplateCategory_with_clientid()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "category_id", Value = CATEGORY_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "default", Value = "1" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "templates_copyable", Value = "1" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/TemplateV2/SetCategory/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.UpdateCategoryAsync(USER_KEY, CATEGORY_ID, null, clientId: CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task GetTemplateCategoryVisibility_with_minimal_parameters()
		{
			// Arrange
			var CATEGORY_ID = 12345L;
			var jsonVisibility1 = "{\"client_id\":\"111\",\"company_name\":\"Fictitious Company #1\",\"visible\":\"1\"}";
			var jsonVisibility2 = "{\"client_id\":\"111\",\"company_name\":\"Fictitious Company #2\",\"visible\":\"0\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "category_id", Value = CATEGORY_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonVisibility1, jsonVisibility2);
			var mockRestClient = new MockRestClient("/TemplateV2/GetCategoryVisibility/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.GetCategoryVisibilityAsync(USER_KEY, CATEGORY_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.IsTrue(result.ToArray()[0].Visible);
			Assert.IsFalse(result.ToArray()[1].Visible);
		}

		[TestMethod]
		public async Task GetTemplateCategoryVisibility_with_limit()
		{
			// Arrange
			var CATEGORY_ID = 12345L;
			var limit = 5;
			var jsonVisibility1 = "{\"client_id\":\"111\",\"company_name\":\"Fictitious Company #1\",\"visible\":\"1\"}";
			var jsonVisibility2 = "{\"client_id\":\"111\",\"company_name\":\"Fictitious Company #2\",\"visible\":\"0\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "category_id", Value = CATEGORY_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "limit", Value = limit },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonVisibility1, jsonVisibility2);
			var mockRestClient = new MockRestClient("/TemplateV2/GetCategoryVisibility/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.GetCategoryVisibilityAsync(USER_KEY, CATEGORY_ID, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetTemplateCategoryVisibility_with_offset()
		{
			// Arrange
			var CATEGORY_ID = 12345L;
			var offset = 25;
			var jsonVisibility1 = "{\"client_id\":\"111\",\"company_name\":\"Fictitious Company #1\",\"visible\":\"1\"}";
			var jsonVisibility2 = "{\"client_id\":\"111\",\"company_name\":\"Fictitious Company #2\",\"visible\":\"0\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "category_id", Value = CATEGORY_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "offset", Value = offset },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonVisibility1, jsonVisibility2);
			var mockRestClient = new MockRestClient("/TemplateV2/GetCategoryVisibility/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.GetCategoryVisibilityAsync(USER_KEY, CATEGORY_ID, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetTemplateCategoryVisibility_with_clientid()
		{
			// Arrange
			var CATEGORY_ID = 12345L;
			var jsonVisibility1 = "{\"client_id\":\"111\",\"company_name\":\"Fictitious Company #1\",\"visible\":\"1\"}";
			var jsonVisibility2 = "{\"client_id\":\"111\",\"company_name\":\"Fictitious Company #2\",\"visible\":\"0\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "category_id", Value = CATEGORY_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"clients\":[{0},{1}]}}}}", jsonVisibility1, jsonVisibility2);
			var mockRestClient = new MockRestClient("/TemplateV2/GetCategoryVisibility/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.GetCategoryVisibilityAsync(USER_KEY, CATEGORY_ID, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
		}

		[TestMethod]
		public async Task GetTemplateCategoryVisibilityCount_with_minimal_parameters()
		{
			// Arrange
			var CATEGORY_ID = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "category_id", Value = CATEGORY_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/TemplateV2/GetCategoryVisibility/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.GetCategoryVisibilityCountAsync(USER_KEY, CATEGORY_ID);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetTemplateCategoryVisibilityCount_with_clientid()
		{
			// Arrange
			var CATEGORY_ID = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "category_id", Value = CATEGORY_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/TemplateV2/GetCategoryVisibility/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.GetCategoryVisibilityCountAsync(USER_KEY, CATEGORY_ID, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task SetTemplateCategoryVisibility_with_minimal_parameters()
		{
			// Arrange
			var CATEGORY_ID = 12345L;
			var clientVisibility = new Dictionary<long, bool>
			{
				{ 111L, true },
				{ 222L, false }
			};
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "category_id", Value = CATEGORY_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client[0][client_id]", Value = 111L },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client[0][visible]", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client[1][client_id]", Value = 222L },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client[1][visible]", Value = "false" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/TemplateV2/SetCategoryVisibility/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.SetCategoryVisibilityAsync(USER_KEY, CATEGORY_ID, clientVisibility);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task SetTemplateCategoryVisibility_with_empty_array()
		{
			// Arrange
			var CATEGORY_ID = 12345L;
			var clientVisibility = (IDictionary<long, bool>)null;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "category_id", Value = CATEGORY_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/TemplateV2/SetCategoryVisibility/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.SetCategoryVisibilityAsync(USER_KEY, CATEGORY_ID, clientVisibility);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task SetTemplateCategoryVisibility_with_clientid()
		{
			// Arrange
			var CATEGORY_ID = 12345L;
			var clientVisibility = new Dictionary<long, bool>
			{
				{ 111L, true },
				{ 222L, false }
			};
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "category_id", Value = CATEGORY_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client[0][client_id]", Value = 111L },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client[0][visible]", Value = "true" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client[1][client_id]", Value = 222L },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client[1][visible]", Value = "false" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/TemplateV2/SetCategoryVisibility/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.SetCategoryVisibilityAsync(USER_KEY, CATEGORY_ID, clientVisibility, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task CreateTemplate_with_minimal_parameters()
		{
			// Arrange
			var content = "this is the content";
			var labels = new Dictionary<string, string>
			{
				{ "en_US", "My Template" },
				{ "fr_FR", "Mon Modèle" }
			};
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "label[0][language]", Value = "en_US" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "label[0][name]", Value = "My Template" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "label[1][language]", Value = "fr_FR" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "label[1][name]", Value = "Mon Modèle" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "content", Value = content },
				new Parameter { Type = ParameterType.GetOrPost, Name = "category_id", Value = CATEGORY_ID }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", TEMPLATE_ID);
			var mockRestClient = new MockRestClient("/TemplateV2/CreateTemplate/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.CreateAsync(USER_KEY, labels, content, CATEGORY_ID);

			// Assert
			Assert.AreEqual(TEMPLATE_ID, result);
		}

		[TestMethod]
		public async Task CreateTemplate_with_no_labels()
		{
			// Arrange
			var content = "this is the content";
			var labels = new Dictionary<string, string>
			{
				{ "en_US", "My Template" },
				{ "fr_FR", "Mon modèle" }
			};
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "content", Value = content },
				new Parameter { Type = ParameterType.GetOrPost, Name = "category_id", Value = CATEGORY_ID }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", TEMPLATE_ID);
			var mockRestClient = new MockRestClient("/TemplateV2/CreateTemplate/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.CreateAsync(USER_KEY, null, content, CATEGORY_ID);

			// Assert
			Assert.AreEqual(TEMPLATE_ID, result);
		}

		[TestMethod]
		public async Task CreateTemplate_with_clientid()
		{
			// Arrange
			var content = "this is the content";
			var labels = new Dictionary<string, string>
			{
				{ "en_US", "My Template" },
				{ "fr_FR", "Mon Modèle" }
			};
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "label[0][language]", Value = "en_US" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "label[0][name]", Value = "My Template" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "label[1][language]", Value = "fr_FR" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "label[1][name]", Value = "Mon Modèle" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "content", Value = content },
				new Parameter { Type = ParameterType.GetOrPost, Name = "category_id", Value = CATEGORY_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
				};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":\"{0}\"}}", TEMPLATE_ID);
			var mockRestClient = new MockRestClient("/TemplateV2/CreateTemplate/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.CreateAsync(USER_KEY, labels, content, CATEGORY_ID, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(TEMPLATE_ID, result);
		}

		[TestMethod]
		public async Task DeleteTemplate_with_minimal_parameters()
		{
			// Arrange
			var TEMPLATE_ID = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "template_id", Value = TEMPLATE_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/TemplateV2/DeleteTemplate/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.DeleteAsync(USER_KEY, TEMPLATE_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task DeleteTemplate_with_clientid()
		{
			// Arrange
			var TEMPLATE_ID = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "template_id", Value = TEMPLATE_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/TemplateV2/DeleteTemplate/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.DeleteAsync(USER_KEY, TEMPLATE_ID, CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task GetTemplate_with_minimal_parameters()
		{
			// Arrange
			var TEMPLATE_ID = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "template_id", Value = TEMPLATE_ID }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"labels\":[],\"id\":\"{0}\",\"status\":\"active\",\"category_id\":\"7480\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"aaa\",\"description\":\"aaa\"}}}}", TEMPLATE_ID);
			var mockRestClient = new MockRestClient("/TemplateV2/GetTemplate/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.GetAsync(USER_KEY, TEMPLATE_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(TEMPLATE_ID, result.Id);
		}

		[TestMethod]
		public async Task GetTemplate_with_clientid()
		{
			// Arrange
			var TEMPLATE_ID = 12345L;
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "template_id", Value = TEMPLATE_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"labels\":[],\"id\":\"{0}\",\"status\":\"active\",\"category_id\":\"7480\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"aaa\",\"description\":\"aaa\"}}}}", TEMPLATE_ID);
			var mockRestClient = new MockRestClient("/TemplateV2/GetTemplate/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.GetAsync(USER_KEY, TEMPLATE_ID, CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(TEMPLATE_ID, result.Id);
		}

		[TestMethod]
		public async Task GetTemplates_with_minimal_parameters()
		{
			// Arrange
			var jsonTemplate1 = "{\"labels\":[],\"id\":\"111\",\"status\":\"active\",\"category_id\":\"123\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"aaa\",\"description\":\"aaa\"}";
			var jsonTemplate2 = "{\"labels\":[],\"id\":\"222\",\"status\":\"active\",\"category_id\":\"123\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"bbb\",\"description\":\"bbb\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"templates\":[{0},{1}]}}}}", jsonTemplate1, jsonTemplate2);
			var mockRestClient = new MockRestClient("/TemplateV2/GetTemplates/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.GetTemplatesAsync(USER_KEY);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(111, result.ToArray()[0].Id);
			Assert.AreEqual(222, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetTemplates_with_CATEGORY_ID()
		{
			// Arrange
			var jsonTemplate1 = "{\"labels\":[],\"id\":\"111\",\"status\":\"active\",\"category_id\":\"123\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"aaa\",\"description\":\"aaa\"}";
			var jsonTemplate2 = "{\"labels\":[],\"id\":\"222\",\"status\":\"active\",\"category_id\":\"123\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"bbb\",\"description\":\"bbb\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "category_id", Value = CATEGORY_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"templates\":[{0},{1}]}}}}", jsonTemplate1, jsonTemplate2);
			var mockRestClient = new MockRestClient("/TemplateV2/GetTemplates/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.GetTemplatesAsync(USER_KEY, categoryId: CATEGORY_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(111, result.ToArray()[0].Id);
			Assert.AreEqual(222, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetTemplates_with_limit()
		{
			// Arrange
			var limit = 5;
			var jsonTemplate1 = "{\"labels\":[],\"id\":\"111\",\"status\":\"active\",\"category_id\":\"123\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"aaa\",\"description\":\"aaa\"}";
			var jsonTemplate2 = "{\"labels\":[],\"id\":\"222\",\"status\":\"active\",\"category_id\":\"123\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"bbb\",\"description\":\"bbb\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "limit", Value = limit },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"templates\":[{0},{1}]}}}}", jsonTemplate1, jsonTemplate2);
			var mockRestClient = new MockRestClient("/TemplateV2/GetTemplates/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.GetTemplatesAsync(USER_KEY, limit: limit);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(111, result.ToArray()[0].Id);
			Assert.AreEqual(222, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetTemplates_with_offset()
		{
			// Arrange
			var offset = 25;
			var jsonTemplate1 = "{\"labels\":[],\"id\":\"111\",\"status\":\"active\",\"category_id\":\"123\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"aaa\",\"description\":\"aaa\"}";
			var jsonTemplate2 = "{\"labels\":[],\"id\":\"222\",\"status\":\"active\",\"category_id\":\"123\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"bbb\",\"description\":\"bbb\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "offset", Value = offset },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"templates\":[{0},{1}]}}}}", jsonTemplate1, jsonTemplate2);
			var mockRestClient = new MockRestClient("/TemplateV2/GetTemplates/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.GetTemplatesAsync(USER_KEY, offset: offset);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(111, result.ToArray()[0].Id);
			Assert.AreEqual(222, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetTemplates_with_clientid()
		{
			// Arrange
			var jsonTemplate1 = "{\"labels\":[],\"id\":\"111\",\"status\":\"active\",\"category_id\":\"123\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"aaa\",\"description\":\"aaa\"}";
			var jsonTemplate2 = "{\"labels\":[],\"id\":\"222\",\"status\":\"active\",\"category_id\":\"123\",\"type\":\"html\",\"content\":\"this is the content\",\"thumbnail\":null,\"last_modified\":\"2012-05-29 14:05:28\",\"editor_version\":\"0\",\"name\":\"bbb\",\"description\":\"bbb\"}";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "false" }
			};
			var jsonResponse = string.Format("{{\"status\":\"success\",\"data\":{{\"templates\":[{0},{1}]}}}}", jsonTemplate1, jsonTemplate2);
			var mockRestClient = new MockRestClient("/TemplateV2/GetTemplates/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.GetTemplatesAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(111, result.ToArray()[0].Id);
			Assert.AreEqual(222, result.ToArray()[1].Id);
		}

		[TestMethod]
		public async Task GetTemplatesCount_with_minimal_parameters()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/TemplateV2/GetTemplates/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.GetCountAsync(USER_KEY);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetTemplatesCount_with_CATEGORY_ID()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "category_id", Value = CATEGORY_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/TemplateV2/GetTemplates/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.GetCountAsync(USER_KEY, categoryId: CATEGORY_ID);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task GetTemplatesCount_with_clientid()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "count", Value = "true" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":{\"count\":\"2\"}}";
			var mockRestClient = new MockRestClient("/TemplateV2/GetTemplates/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.GetCountAsync(USER_KEY, clientId: CLIENT_ID);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public async Task UpdateTemplate_with_minimal_parameters()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "template_id", Value = TEMPLATE_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/TemplateV2/SetTemplate/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.UpdateAsync(USER_KEY, TEMPLATE_ID, null);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTemplate_with_labels()
		{
			// Arrange
			var labels = new Dictionary<string, string>
			{
				{ "en_US", "My Category" },
				{ "fr_FR", "Ma Catégorie" }
			};
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "template_id", Value = TEMPLATE_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "label[en_US]", Value = "My Category" },
				new Parameter { Type = ParameterType.GetOrPost, Name = "label[fr_FR]", Value = "Ma Catégorie" }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/TemplateV2/SetTemplate/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.UpdateAsync(USER_KEY, TEMPLATE_ID, labels);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTemplate_with_content()
		{
			// Arrange
			var content = "this is the content";
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "template_id", Value = TEMPLATE_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "content", Value = content }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/TemplateV2/SetTemplate/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.UpdateAsync(USER_KEY, TEMPLATE_ID, null, content: content);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTemplate_with_CATEGORY_ID()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "template_id", Value = TEMPLATE_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "category_id", Value = CATEGORY_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/TemplateV2/SetTemplate/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.UpdateAsync(USER_KEY, TEMPLATE_ID, null, categoryId: CATEGORY_ID);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public async Task UpdateTemplate_with_clientid()
		{
			// Arrange
			var parameters = new[]
			{
				new Parameter { Type = ParameterType.GetOrPost, Name = "template_id", Value = TEMPLATE_ID },
				new Parameter { Type = ParameterType.GetOrPost, Name = "client_id", Value = CLIENT_ID }
			};
			var jsonResponse = "{\"status\":\"success\",\"data\":\"true\"}";
			var mockRestClient = new MockRestClient("/TemplateV2/SetTemplate/", parameters, jsonResponse);

			// Act
			var apiClient = new CakeMailRestClient(API_KEY, mockRestClient.Object);
			var result = await apiClient.Templates.UpdateAsync(USER_KEY, TEMPLATE_ID, null, clientId: CLIENT_ID);

			// Assert
			Assert.IsTrue(result);
		}
	}
}
