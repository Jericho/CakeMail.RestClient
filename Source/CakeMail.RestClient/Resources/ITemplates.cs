using CakeMail.RestClient.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.Resources
{
	public interface ITemplates
	{
		Task<long> CreateAsync(string userKey, IDictionary<string, string> labels, string content, long categoryId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<long> CreateCategoryAsync(string userKey, IDictionary<string, string> labels, bool isVisibleByDefault = true, bool templatesCanBeCopied = true, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> DeleteAsync(string userKey, long templateId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> DeleteCategoryAsync(string userKey, long categoryId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<Template> GetAsync(string userKey, long templateId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<TemplateCategory[]> GetCategoriesAsync(string userKey, int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<long> GetCategoriesCountAsync(string userKey, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<TemplateCategory> GetCategoryAsync(string userKey, long categoryId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<TemplateCategoryVisibility[]> GetCategoryVisibilityAsync(string userKey, long categoryId, int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<long> GetCategoryVisibilityCountAsync(string userKey, long categoryId, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<long> GetCountAsync(string userKey, long? categoryId = default(long?), long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<Template[]> GetTemplatesAsync(string userKey, long? categoryId = default(long?), int? limit = 0, int? offset = 0, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> SetCategoryVisibilityAsync(string userKey, long categoryId, IDictionary<long, bool> clientVisibility, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> UpdateAsync(string userKey, long templateId, IDictionary<string, string> labels, string content = null, long? categoryId = default(long?), long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
		Task<bool> UpdateCategoryAsync(string userKey, long categoryId, IDictionary<string, string> labels, bool isVisibleByDefault = true, bool templatesCanBeCopied = true, long? clientId = default(long?), CancellationToken cancellationToken = default(CancellationToken));
	}
}
