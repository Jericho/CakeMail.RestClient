using CakeMail.RestClient.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	/// <summary>
	/// Allows you to manage templates
	/// </summary>
	public interface ITemplates
	{
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
		Task<long> CreateAsync(string userKey, IDictionary<string, string> labels, string content, long categoryId, long? clientId = default(long?), CancellationToken cancellationToken = default);

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
		Task<long> CreateCategoryAsync(string userKey, IDictionary<string, string> labels, bool isVisibleByDefault = true, bool templatesCanBeCopied = true, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Delete a template
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="templateId">ID of the template</param>
		/// <param name="clientId">Client ID of the client in which the template is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the template is deleted</returns>
		Task<bool> DeleteAsync(string userKey, long templateId, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Delete a template category
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="categoryId">ID of the template category</param>
		/// <param name="clientId">Client ID of the client in which the template category is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the template category is deleted</returns>
		Task<bool> DeleteCategoryAsync(string userKey, long categoryId, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieve a template
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="templateId">ID of the template</param>
		/// <param name="clientId">Client ID of the client in which the template is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The <see cref="Template">template</see></returns>
		Task<Template> GetAsync(string userKey, long templateId, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieve the template categories matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="limit">Limit the number of resulting categories.</param>
		/// <param name="offset">Offset the beginning of resulting categories.</param>
		/// <param name="clientId">Client ID of the client in which the categories are located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>Enumeration of <see cref="TemplateCategory">categories</see> matching the filtering criteria</returns>
		Task<TemplateCategory[]> GetCategoriesAsync(string userKey, int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Get a count of template categories matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="clientId">Client ID of the client in which the categories are located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The count of categories matching the filtering criteria</returns>
		Task<long> GetCategoriesCountAsync(string userKey, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Retrieve a template category
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="categoryId">ID of the category</param>
		/// <param name="clientId">Client ID of the client in which the category is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The <see cref="TemplateCategory">category</see></returns>
		Task<TemplateCategory> GetCategoryAsync(string userKey, long categoryId, long? clientId = default(long?), CancellationToken cancellationToken = default);

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
		Task<TemplateCategoryVisibility[]> GetCategoryVisibilityAsync(string userKey, long categoryId, int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Get a count of permissions for a given template category.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="categoryId">ID of the category</param>
		/// <param name="clientId">ID of the client</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The count of permissions matching the filtering criteria</returns>
		Task<long> GetCategoryVisibilityCountAsync(string userKey, long categoryId, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Get a count of templates matching the filtering criteria.
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="categoryId">ID of the category</param>
		/// <param name="clientId">Client ID of the client in which the templates are located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>The count of templates matching the filtering criteria</returns>
		Task<long> GetCountAsync(string userKey, long? categoryId = default(long?), long? clientId = default(long?), CancellationToken cancellationToken = default);

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
		Task<Template[]> GetTemplatesAsync(string userKey, long? categoryId = default(long?), int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default);

		/// <summary>
		/// Set the permissions for a category
		/// </summary>
		/// <param name="userKey">User Key of the user who initiates the call.</param>
		/// <param name="categoryId">ID of the category</param>
		/// <param name="clientVisibility">The list of clients and their associated boolean that indicates if they have access to the category</param>
		/// <param name="clientId">ID of the client in which the category is located.</param>
		/// <param name="cancellationToken">The cancellation token</param>
		/// <returns>True if the permissions are successfully updated</returns>
		Task<bool> SetCategoryVisibilityAsync(string userKey, long categoryId, IDictionary<long, bool> clientVisibility, long? clientId = default(long?), CancellationToken cancellationToken = default);

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
		Task<bool> UpdateAsync(string userKey, long templateId, IDictionary<string, string> labels, string content = null, long? categoryId = default(long?), long? clientId = default(long?), CancellationToken cancellationToken = default);

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
		Task<bool> UpdateCategoryAsync(string userKey, long categoryId, IDictionary<string, string> labels, bool isVisibleByDefault = true, bool templatesCanBeCopied = true, long? clientId = default(long?), CancellationToken cancellationToken = default);
	}
}
