using CakeMail.RestClient.Models;
using CakeMail.RestClient.Utilities;
using Pathoschild.Http.Client;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	public class Templates
	{
		#region Fields

		private readonly IClient _client;

		#endregion

		#region Constructor

		public Templates(IClient client)
		{
			_client = client;
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
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>ID of the new template category</returns>
		public Task<long> CreateCategoryAsync(string userKey, IDictionary<string, string> labels, bool isVisibleByDefault = true, bool templatesCanBeCopied = true, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var parameters = new List<KeyValuePair<string, object>>
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
			return _client
				.PostAsync("TemplateV2/CreateCategory")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<long>("id");
		}

		/// <summary>
		/// Delete a template category
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="categoryId">ID of the template category</param>
		/// <param name="clientId">Client ID of the client in which the template category is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the template category is deleted</returns>
		public async Task<bool> DeleteCategoryAsync(string userKey, long categoryId, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("category_id", categoryId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			// The data returned when deleting a category is a little bit unusual
			// Instead of returning a boolean value to indicate success, it returns an empty array!!!
			// For example:  {"status":"success","data":[]}
			await _client
				.PostAsync("TemplateV2/DeleteCategory")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsResponse()
				.ConfigureAwait(false);

			return true;
		}

		/// <summary>
		/// Retrieve a template category
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="categoryId">ID of the category</param>
		/// <param name="clientId">Client ID of the client in which the category is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The <see cref="TemplateCategory">category</see></returns>
		public Task<TemplateCategory> GetCategoryAsync(string userKey, long categoryId, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("category_id", categoryId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("TemplateV2/GetCategory")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<TemplateCategory>();
		}

		/// <summary>
		/// Retrieve the template categories matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="limit">Limit the number of resulting categories.</param>
		/// <param name="offset">Offset the beginning of resulting categories.</param>
		/// <param name="clientId">Client ID of the client in which the categories are located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>Enumeration of <see cref="TemplateCategory">categories</see> matching the filtering criteria</returns>
		public Task<TemplateCategory[]> GetCategoriesAsync(string userKey, int? limit = 0, int? offset = 0, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "false")
			};
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("TemplateV2/GetCategories")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<TemplateCategory[]>("categories");
		}

		/// <summary>
		/// Get a count of template categories matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="clientId">Client ID of the client in which the categories are located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The count of categories matching the filtering criteria</returns>
		public Task<long> GetCategoriesCountAsync(string userKey, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "true")
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("TemplateV2/GetCategories")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<long>("count");
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
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the category was updated</returns>
		public Task<bool> UpdateCategoryAsync(string userKey, long categoryId, IDictionary<string, string> labels, bool isVisibleByDefault = true, bool templatesCanBeCopied = true, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("category_id", categoryId),
				new KeyValuePair<string, object>("default", isVisibleByDefault ? "1" : "0"),
				new KeyValuePair<string, object>("templates_copyable", templatesCanBeCopied ? "1" : "0")
			};
			if (labels != null)
			{
				foreach (var label in labels)
				{
					parameters.Add(new KeyValuePair<string, object>(string.Format("label[{0}]", label.Key), label.Value));
				}
			}

			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("TemplateV2/SetCategory")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<bool>();
		}

		/// <summary>
		/// Retrieve the list of permissions for a category
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="categoryId">ID of the category</param>
		/// <param name="limit">Limit the number of resulting permissions.</param>
		/// <param name="offset">Offset the beginning of resulting permissions.</param>
		/// <param name="clientId">ID of the client in which the category is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>An enumeration of permissions</returns>
		public Task<TemplateCategoryVisibility[]> GetCategoryVisibilityAsync(string userKey, long categoryId, int? limit = 0, int? offset = 0, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("category_id", categoryId),
				new KeyValuePair<string, object>("count", "false")
			};
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("TemplateV2/GetCategoryVisibility")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<TemplateCategoryVisibility[]>("clients");
		}

		/// <summary>
		/// Get a count of permissions for a given template category.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="categoryId">ID of the category</param>
		/// <param name="clientId">ID of the client</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The count of permissions matching the filtering criteria</returns>
		public Task<long> GetCategoryVisibilityCountAsync(string userKey, long categoryId, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("category_id", categoryId),
				new KeyValuePair<string, object>("count", "true")
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("TemplateV2/GetCategoryVisibility")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<long>("count");
		}

		/// <summary>
		/// Set the permissions for a category
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="categoryId">ID of the category</param>
		/// <param name="clientVisibility">The list of clients and their associated boolean that indicates if they have access to the category</param>
		/// <param name="clientId">ID of the client in which the category is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the permissions are successfully updated</returns>
		public Task<bool> SetCategoryVisibilityAsync(string userKey, long categoryId, IDictionary<long, bool> clientVisibility, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("category_id", categoryId)
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

			return _client
				.PostAsync("TemplateV2/SetCategoryVisibility")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<bool>();
		}

		/// <summary>
		/// Create a template
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="labels">Name of the template.</param>
		/// <param name="content">Content of the template.</param>
		/// <param name="categoryId">ID of the category.</param>
		/// <param name="clientId">Client ID of the client in which the template is created.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>ID of the new template</returns>
		public Task<long> CreateAsync(string userKey, IDictionary<string, string> labels, string content, long categoryId, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var parameters = new List<KeyValuePair<string, object>>
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
			return _client
				.PostAsync("TemplateV2/CreateTemplate")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<long>("id");
		}

		/// <summary>
		/// Delete a template
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="templateId">ID of the template</param>
		/// <param name="clientId">Client ID of the client in which the template is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the template is deleted</returns>
		public async Task<bool> DeleteAsync(string userKey, long templateId, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("template_id", templateId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			// The data returned when deleting a template is a little bit unusual
			// Instead of returning a boolean value to indicate success, it returns an empty array!!!
			// For example:  {"status":"success","data":[]}
			await _client
				.PostAsync("TemplateV2/DeleteTemplate")
				.WithCancellationToken(cancellationToken)
				.AsResponse()
				.ConfigureAwait(false);

			return true;
		}

		/// <summary>
		/// Retrieve a template
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="templateId">ID of the template</param>
		/// <param name="clientId">Client ID of the client in which the template is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The <see cref="Template">template</see></returns>
		public Task<Template> GetAsync(string userKey, long templateId, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("template_id", templateId)
			};
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("TemplateV2/GetTemplate")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<Template>();
		}

		/// <summary>
		/// Retrieve the templates matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="categoryId">ID of the category</param>
		/// <param name="limit">Limit the number of resulting templates.</param>
		/// <param name="offset">Offset the beginning of resulting templates.</param>
		/// <param name="clientId">Client ID of the client in which the templates are located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>Enumeration of <see cref="Template">templates</see> matching the filtering criteria</returns>
		public Task<Template[]> GetTemplatesAsync(string userKey, long? categoryId = null, int? limit = 0, int? offset = 0, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "false")
			};
			if (categoryId.HasValue) parameters.Add(new KeyValuePair<string, object>("category_id", categoryId.Value));
			if (limit > 0) parameters.Add(new KeyValuePair<string, object>("limit", limit));
			if (offset > 0) parameters.Add(new KeyValuePair<string, object>("offset", offset));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("TemplateV2/GetTemplates")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<Template[]>("templates");
		}

		/// <summary>
		/// Get a count of templates matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="categoryId">ID of the category</param>
		/// <param name="clientId">Client ID of the client in which the templates are located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The count of templates matching the filtering criteria</returns>
		public Task<long> GetCountAsync(string userKey, long? categoryId = null, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var parameters = new List<KeyValuePair<string, object>>
			{
				new KeyValuePair<string, object>("user_key", userKey),
				new KeyValuePair<string, object>("count", "true")
			};
			if (categoryId.HasValue) parameters.Add(new KeyValuePair<string, object>("category_id", categoryId.Value));
			if (clientId.HasValue) parameters.Add(new KeyValuePair<string, object>("client_id", clientId.Value));

			return _client
				.PostAsync("TemplateV2/GetTemplates")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<long>("count");
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
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the category was updated</returns>
		public Task<bool> UpdateAsync(string userKey, long templateId, IDictionary<string, string> labels, string content = null, long? categoryId = null, long? clientId = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			var parameters = new List<KeyValuePair<string, object>>
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

			return _client
				.PostAsync("TemplateV2/SetTemplate")
				.WithFormUrlEncodedBody(parameters)
				.WithCancellationToken(cancellationToken)
				.AsCakeMailObject<bool>();
		}

		#endregion
	}
}
