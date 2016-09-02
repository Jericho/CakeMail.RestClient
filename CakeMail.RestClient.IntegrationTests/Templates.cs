using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeMail.RestClient.IntegrationTests
{
	public static class TemplatesTests
	{
		public static async Task ExecuteAllMethods(CakeMailRestClient api, string userKey, long clientId)
		{
			Console.WriteLine("");
			Console.WriteLine(new string('-', 25));
			Console.WriteLine("Executing TEMPLATES methods...");

			var categoryLabels = new Dictionary<string, string> 
			{
				{ "en_US", "My Category" },
				{ "fr_FR", "Ma Catégorie" }
			};
			var categoryId = await api.Templates.CreateCategoryAsync(userKey, categoryLabels, true, true, clientId).ConfigureAwait(false);
			Console.WriteLine("New template category created. Id: {0}", categoryId);

			var category = await api.Templates.GetCategoryAsync(userKey, categoryId, clientId).ConfigureAwait(false);
			Console.WriteLine("Template category retrieved: Name = {0}", category.Name);

			var categories = await api.Templates.GetCategoriesAsync(userKey, 0, 0, clientId).ConfigureAwait(false);
			Console.WriteLine("All Template categories retrieved. Count = {0}", categories.Count());

			var categoriesCount = await api.Templates.GetCategoriesCountAsync(userKey, clientId).ConfigureAwait(false);
			Console.WriteLine("Template categories count = {0}", categoriesCount);

			var permissions = await api.Templates.GetCategoryVisibilityAsync(userKey, categoryId, null, null, clientId).ConfigureAwait(false);
			Console.WriteLine("Template category permissions: {0}", string.Join(", ", permissions.Select(p => string.Format("{0}={1}", p.CompanyName, p.Visible))));

			if (permissions.Any())
			{
				var newPermissions = permissions.ToDictionary(p => p.ClientId, p => false);
				var permissionsRevoked = await api.Templates.SetCategoryVisibilityAsync(userKey, categoryId, newPermissions, clientId).ConfigureAwait(false);
				Console.WriteLine("Template category permissions revoked: {0}", permissionsRevoked ? "success" : "failed");
			}

			var templateLabels = new Dictionary<string, string> 
			{
				{ "en_US", "My Template" },
				{ "fr_FR", "Mon Modèle" }
			};
			var templateContent = "<html><body>Hello World</body></html>";
			var templateId = await api.Templates.CreateAsync(userKey, templateLabels, templateContent, categoryId, clientId).ConfigureAwait(false);
			Console.WriteLine("Template created. Id = {0}", templateId);

			var templates = await api.Templates.GetTemplatesAsync(userKey, categoryId, null, null, clientId).ConfigureAwait(false);
			Console.WriteLine("All Templates retrieved. Count = {0}", templates.Count());

			var template = await api.Templates.GetAsync(userKey, templateId, clientId).ConfigureAwait(false);
			Console.WriteLine("Template retrieved: Name = {0}", template.Name);

			var templateDeleted = await api.Templates.DeleteAsync(userKey, templateId, clientId).ConfigureAwait(false);
			Console.WriteLine("Template deleted: {0}", templateDeleted ? "success" : "failed");

			var categoryDeleted = await api.Templates.DeleteCategoryAsync(userKey, categoryId, clientId).ConfigureAwait(false);
			Console.WriteLine("Template category deleted: {0}", categoryDeleted ? "success" : "failed");

			Console.WriteLine(new string('-', 25));
			Console.WriteLine("");
		}
	}
}
