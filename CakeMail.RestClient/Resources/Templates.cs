using CakeMail.RestClient.Models;
using System.Collections.Generic;
using System.Linq;

namespace CakeMail.RestClient.Resources
{
	public class Templates
	{
		#region Fields

		private readonly CakeMailRestClient _cakeMailRestClient;

		#endregion

		#region Constructor

		public Templates(CakeMailRestClient cakeMailRestClient)
		{
			_cakeMailRestClient = cakeMailRestClient;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Create a template category
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="labels">Name of the category.</param>
		/// <param name="isVisibleByDefault">Is the category visible by default.</param>
		/// <param name="templatesCanBeCopied">Are the templates in the category copyable.</param>
		/// <param name="clientId">Client ID of the client in which the category is created.</param>
		/// <returns>ID of the new template category</returns>
		public long CreateCategory(string userKey, IDictionary<string, string> labels, bool isVisibleByDefault = true, bool templatesCanBeCopied = true, long? clientId = null)
		{
			string path = "/TemplateV2/CreateCategory/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("default", isVisibleByDefault ? "1" : "0"),
				new KeyValuePair<string, object>("templates_copyable", templatesCanBeCopied ? "1" : "0")
			};
			if (labels != null)
			{
				foreach (var item in labels.Select((label, i) => new { Index = i, Language = label.Key, Name = label.Value }))
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("label[{0}][language]", item.Index), item.Language));
					parameters.Add(new KeyValuePair<string, object>(string.Format("label[{0}][name]", item.Index), item.Name));
				}
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			// The data returned when creating a new category is a little bit unusual
			// Instead of simply returning the unique identifier of the new record like all the other 'Create' methods, for example: {"status":"success","data":"4593766"}
			// this method return an object with a single property called 'id' containing the unique identifier of the new record, like this: {"status":"success","data":{"id":"14052"}}
			return _cakeMailRestClient.ExecuteRequest<long>(path, parameters, "id");
		}

		/// <summary>
		/// Delete a template category
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="categoryId">ID of the template category</param>
		/// <param name="clientId">Client ID of the client in which the template category is located.</param>
		/// <returns>True if the template category is deleted</returns>
		public bool DeleteCategory(string userKey, long categoryId, long? clientId = null)
		{
			string path = "/TemplateV2/DeleteCategory/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("category_id", categoryId),
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			// The data returned when deleting a category is a little bit unusual
			// Instead of returning a boolean value to indicate success, it returns an empty array!!!
			// For example:  {"status":"success","data":[]}
			_cakeMailRestClient.ExecuteRequest<object>(path, parameters);
			return true;
		}

		/// <summary>
		/// Retrieve a template category
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="categoryId">ID of the category</param>
		/// <param name="clientId">Client ID of the client in which the category is located.</param>
		/// <returns>The <see cref="TemplateCategory">category</see></returns>
		public TemplateCategory GetCategory(string userKey, long categoryId, long? clientId = null)
		{
			var path = "/TemplateV2/GetCategory/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("category_id", categoryId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _cakeMailRestClient.ExecuteRequest<TemplateCategory>(path, parameters);
		}

		/// <summary>
		/// Retrieve the template categories matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="limit">Limit the number of resulting categories.</param>
		/// <param name="offset">Offset the beginning of resulting categories.</param>
		/// <param name="clientId">Client ID of the client in which the categories are located.</param>
		/// <returns>Enumeration of <see cref="TemplateCategory">categories</see> matching the filtering criteria</returns>
		public IEnumerable<TemplateCategory> GetCategories(string userKey, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			var path = "/TemplateV2/GetCategories/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "false")
			};
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _cakeMailRestClient.ExecuteArrayRequest<TemplateCategory>(path, parameters, "categories");
		}

		/// <summary>
		/// Get a count of template categories matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="clientId">Client ID of the client in which the categories are located.</param>
		/// <returns>The count of categories matching the filtering criteria</returns>
		public long GetCategoriesCount(string userKey, long? clientId = null)
		{
			var path = "/TemplateV2/GetCategories/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "true")
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _cakeMailRestClient.ExecuteCountRequest(path, parameters);
		}

		/// <summary>
		/// Update a template category
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="categoryId">ID of the category</param>
		/// <param name="labels">Name of the category.</param>
		/// <param name="isVisibleByDefault">Is the category visible by default.</param>
		/// <param name="templatesCanBeCopied">Are the templates in the category copyable.</param>
		/// <param name="clientId">Client ID of the client in which the category is located.</param>
		/// <returns>True if the category was updated</returns>
		public bool UpdateCategory(string userKey, long categoryId, IDictionary<string, string> labels, bool isVisibleByDefault = true, bool templatesCanBeCopied = true, long? clientId = null)
		{
			string path = "/TemplateV2/SetCategory/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("category_id", categoryId),
				new KeyValuePair<string, object>("default", isVisibleByDefault ? "1" : "0"),
				new KeyValuePair<string, object>("templates_copyable", templatesCanBeCopied ? "1" : "0"),
			};
			if (labels != null)
			{
				foreach (var label in labels)
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("label[{0}]", label.Key), label.Value));
				}
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _cakeMailRestClient.ExecuteRequest<bool>(path, parameters);
		}

		/// <summary>
		/// Retrieve the list of permissions for a category
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="categoryId">ID of the category</param>
		/// <param name="limit">Limit the number of resulting permissions.</param>
		/// <param name="offset">Offset the beginning of resulting permissions.</param>
		/// <param name="clientId">ID of the client in which the category is located.</param>
		/// <returns>An enumeration of permissions</returns>
		public IEnumerable<TemplateCategoryVisibility> GetCategoryVisibility(string userKey, long categoryId, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			var path = "/TemplateV2/GetCategoryVisibility/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("category_id", categoryId),
				new KeyValuePair<string, object>("count", "false")
			};
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _cakeMailRestClient.ExecuteArrayRequest<TemplateCategoryVisibility>(path, parameters, "clients");
		}

		/// <summary>
		/// Get a count of permissions for a given template category.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="categoryId">ID of the category</param>
		/// <param name="clientId">ID of the client</param>
		/// <returns>The count of permissions matching the filtering criteria</returns>
		public long GetCategoryVisibilityCount(string userKey, long categoryId, long? clientId = null)
		{
			var path = "/TemplateV2/GetCategoryVisibility/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("category_id", categoryId),
				new KeyValuePair<string, object>("count", "true")
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _cakeMailRestClient.ExecuteCountRequest(path, parameters);
		}

		/// <summary>
		/// Set the permissions for a category
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="categoryId">ID of the category</param>
		/// <param name="clientVisibility">The list of clients and their associated boolean that indicates if they have access to the category</param>
		/// <param name="clientId">ID of the client in which the category is located.</param>
		/// <returns>True if the permissions are successfully updated</returns>
		public bool SetCategoryVisibility(string userKey, long categoryId, IDictionary<long, bool> clientVisibility, long? clientId = null)
		{
			var path = "/TemplateV2/SetCategoryVisibility/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("category_id", categoryId),
			};
			if (clientVisibility != null)
			{
				foreach (var item in clientVisibility.Select((visibility, i) => new { Index = i, ClientId = visibility.Key, Visible = visibility.Value }))
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("client[{0}][client_id]", item.Index), item.ClientId));
					parameters.Add(new KeyValuePair<string, object>(string.Format("client[{0}][visible]", item.Index), item.Visible ? "true" : "false"));
				}
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _cakeMailRestClient.ExecuteRequest<bool>(path, parameters);
		}

		/// <summary>
		/// Create a template
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="labels">Name of the template.</param>
		/// <param name="content">Content of the template.</param>
		/// <param name="categoryId">ID of the category.</param>
		/// <param name="clientId">Client ID of the client in which the template is created.</param>
		/// <returns>ID of the new template</returns>
		public long Create(string userKey, IDictionary<string, string> labels, string content, long categoryId, long? clientId = null)
		{
			string path = "/TemplateV2/CreateTemplate/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("content", content),
				new KeyValuePair<string, object>("category_id", categoryId)
			};
			if (labels != null)
			{
				foreach (var item in labels.Select((label, i) => new { Index = i, Language = label.Key, Name = label.Value }))
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("label[{0}][language]", item.Index), item.Language));
					parameters.Add(new KeyValuePair<string, object>(string.Format("label[{0}][name]", item.Index), item.Name));
				}
			}
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			// The data returned when creating a new template is a little bit unusual
			// Instead of simply returning the unique identifier of the new record like all the other 'Create' methods, for example: {"status":"success","data":"4593766"}
			// this method return an object with a single property called 'id' containing the unique identifier of the new record, like this: {"status":"success","data":{"id":"14052"}}
			return _cakeMailRestClient.ExecuteRequest<long>(path, parameters, "id");
		}

		/// <summary>
		/// Delete a template
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="templateId">ID of the template</param>
		/// <param name="clientId">Client ID of the client in which the template is located.</param>
		/// <returns>True if the template is deleted</returns>
		public bool Delete(string userKey, long templateId, long? clientId = null)
		{
			string path = "/TemplateV2/DeleteTemplate/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("template_id", templateId),
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			// The data returned when deleting a template is a little bit unusual
			// Instead of returning a boolean value to indicate success, it returns an empty array!!!
			// For example:  {"status":"success","data":[]}
			_cakeMailRestClient.ExecuteRequest(path, parameters);
			return true;
		}

		/// <summary>
		/// Retrieve a template
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="templateId">ID of the template</param>
		/// <param name="clientId">Client ID of the client in which the template is located.</param>
		/// <returns>The <see cref="Template">template</see></returns>
		public Template Get(string userKey, long templateId, long? clientId = null)
		{
			var path = "/TemplateV2/GetTemplate/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("template_id", templateId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _cakeMailRestClient.ExecuteRequest<Template>(path, parameters);
		}

		/// <summary>
		/// Retrieve the templates matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="categoryId">ID of the category</param>
		/// <param name="limit">Limit the number of resulting templates.</param>
		/// <param name="offset">Offset the beginning of resulting templates.</param>
		/// <param name="clientId">Client ID of the client in which the templates are located.</param>
		/// <returns>Enumeration of <see cref="Template">templates</see> matching the filtering criteria</returns>
		public IEnumerable<Template> GetTemplates(string userKey, long? categoryId = null, int? limit = 0, int? offset = 0, long? clientId = null)
		{
			var path = "/TemplateV2/GetTemplates/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "false")
			};
			if (categoryId.HasValue) parameters.Add(new KeyValuePair<string, object>("category_id", categoryId.Value));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _cakeMailRestClient.ExecuteArrayRequest<Template>(path, parameters, "templates");
		}

		/// <summary>
		/// Get a count of templates matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="categoryId">ID of the category</param>
		/// <param name="clientId">Client ID of the client in which the templates are located.</param>
		/// <returns>The count of templates matching the filtering criteria</returns>
		public long GetCount(string userKey, long? categoryId = null, long? clientId = null)
		{
			var path = "/TemplateV2/GetTemplates/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "true")
			};
			if (categoryId.HasValue) parameters.Add(new KeyValuePair<string, object>("category_id", categoryId.Value));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _cakeMailRestClient.ExecuteCountRequest(path, parameters);
		}

		/// <summary>
		/// Update a template
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="templateId">ID of the template</param>
		/// <param name="labels">Name of the template.</param>
		/// <param name="content">Content of the template.</param>
		/// <param name="categoryId">ID of the category.</param>
		/// <param name="clientId">Client ID of the client in which the template is located.</param>
		/// <returns>True if the category was updated</returns>
		public bool Update(string userKey, long templateId, IDictionary<string, string> labels, string content = null, long? categoryId = null, long? clientId = null)
		{
			string path = "/TemplateV2/SetTemplate/";

			var parameters = new List<KeyValuePair<string, object>>()
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("template_id", templateId)
			};
			if (labels != null)
			{
				foreach (var label in labels)
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("label[{0}]", label.Key), label.Value));
				}
			}
			if (content != null) parameters.Add(new KeyValuePair<string, object>("content", content));
			if (categoryId.HasValue) parameters.Add(new KeyValuePair<string, object>("category_id", categoryId.Value));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _cakeMailRestClient.ExecuteRequest<bool>(path, parameters);
		}

		#endregion
	}
}
