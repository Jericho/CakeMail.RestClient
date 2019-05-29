using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.IntegrationTests.Tests
{
	public static class TemplatesTests
	{
		public static async Task ExecuteAllMethods(ICakeMailRestClient client, string userKey, long clientId, TextWriter log, CancellationToken cancellationToken)
		{
			await log.WriteLineAsync("\n***** TEMPLATES *****").ConfigureAwait(false);

			var categoryLabels = new Dictionary<string, string>
			{
				{ "en_US", "My Category" },
				{ "fr_FR", "Ma Catégorie" }
			};
			var categoryId = await client.Templates.CreateCategoryAsync(userKey, categoryLabels, true, true, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"New template category created. Id: {categoryId}").ConfigureAwait(false);

			var category = await client.Templates.GetCategoryAsync(userKey, categoryId, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Template category retrieved: Name = {category.Name}").ConfigureAwait(false);

			categoryLabels = new Dictionary<string, string>
			{
				{ "en_US", "My Category UPDATED" },
				{ "fr_FR", "Ma Catégorie UPDATED" }
			};
			var categoryUpdated = await client.Templates.UpdateCategoryAsync(userKey, categoryId, categoryLabels, true, true, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Template category updated. Id: {categoryId}").ConfigureAwait(false);

			var categories = await client.Templates.GetCategoriesAsync(userKey, 0, 0, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"All Template categories retrieved. Count = {categories.Count()}").ConfigureAwait(false);

			var categoriesCount = await client.Templates.GetCategoriesCountAsync(userKey, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Template categories count = {categoriesCount}").ConfigureAwait(false);

			var permissions = await client.Templates.GetCategoryVisibilityAsync(userKey, categoryId, null, null, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Template category permissions: {string.Join(", ", permissions.Select(p => string.Format("{0}={1}", p.CompanyName, p.Visible)))}").ConfigureAwait(false);

			if (permissions.Any())
			{
				var newPermissions = permissions.ToDictionary(p => p.ClientId, p => false);
				var permissionsRevoked = await client.Templates.SetCategoryVisibilityAsync(userKey, categoryId, newPermissions, clientId, cancellationToken).ConfigureAwait(false);
				await log.WriteLineAsync($"Template category permissions revoked: {(permissionsRevoked ? "success" : "failed")}").ConfigureAwait(false);
			}

			var templateLabels = new Dictionary<string, string>
			{
				{ "en_US", "My Template" },
				{ "fr_FR", "Mon Modèle" }
			};
			var templateContent = "<html><body>Hello World</body></html>";
			var templateId = await client.Templates.CreateAsync(userKey, templateLabels, templateContent, categoryId, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Template created. Id = {templateId}").ConfigureAwait(false);

			templateLabels = new Dictionary<string, string>
			{
				{ "en_US", "My Template UPDATED" },
				{ "fr_FR", "Ma Modèle UPDATED" }
			};
			var templateUpdated = await client.Templates.UpdateAsync(userKey, templateId, templateLabels, templateContent, categoryId, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Template updated. Id: {templateId}").ConfigureAwait(false);

			var templates = await client.Templates.GetTemplatesAsync(userKey, categoryId, null, null, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"All Templates retrieved. Count = {templates.Count()}").ConfigureAwait(false);

			var template = await client.Templates.GetAsync(userKey, templateId, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Template retrieved: Name = {template.Name}").ConfigureAwait(false);

			var templateDeleted = await client.Templates.DeleteAsync(userKey, templateId, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Template deleted: {(templateDeleted ? "success" : "failed")}").ConfigureAwait(false);

			var categoryDeleted = await client.Templates.DeleteCategoryAsync(userKey, categoryId, clientId, cancellationToken).ConfigureAwait(false);
			await log.WriteLineAsync($"Template category deleted: {(categoryDeleted ? "success" : "failed")}").ConfigureAwait(false);
		}
	}
}
