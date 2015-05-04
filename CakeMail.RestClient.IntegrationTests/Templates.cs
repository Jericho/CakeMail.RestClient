using System;
using System.Collections.Generic;
using System.Linq;

namespace CakeMail.RestClient.IntegrationTests
{
	public static class TemplatesTests
	{
		public static void ExecuteAllMethods(CakeMailRestClient api, string userKey, long clientId)
		{
			Console.WriteLine("");
			Console.WriteLine(new string('-', 25));
			Console.WriteLine("Executing TEMPLATES methods...");

			var categoryLabels = new Dictionary<string, string>() 
			{
				{ "en_US", "My Category" },
				{ "fr_FR", "Ma Catégorie" }
			};
			var categoryId = api.CreateTemplateCategory(userKey, categoryLabels, true, true, clientId);
			Console.WriteLine("New template category created. Id: {0}", categoryId);

			var category = api.GetTemplateCategory(userKey, categoryId, clientId);
			Console.WriteLine("Template category retrieved: Name = {0}", category.Name);

			var categories = api.GetTemplateCategories(userKey, 0, 0, clientId);
			Console.WriteLine("All Template categories retrieved. Count = {0}", categories.Count());

			var categoriesCount = api.GetTemplateCategoriesCount(userKey, clientId);
			Console.WriteLine("Template categories count = {0}", categoriesCount);

			var permissions = api.GetTemplateCategoryVisibility(userKey, categoryId, null, null, clientId);
			Console.WriteLine("Template category permissions: {0}", string.Join(", ", permissions.Select(p => string.Format("{0}={1}", p.CompanyName, p.Visible))));

			if (permissions.Any())
			{
				var newPermissions = permissions.ToDictionary(p => p.ClientId, p => false);
				var permissionsRevoked = api.SetTemplateCategoryVisibility(userKey, categoryId, newPermissions, clientId);
				Console.WriteLine("Template category permissions revoked: {0}", permissionsRevoked ? "success" : "failed");
			}

			var templateLabels = new Dictionary<string, string>() 
			{
				{ "en_US", "My Template" },
				{ "fr_FR", "Mon Modèle" }
			};
			var templateContent = "<html><body>Hello World</body></html>";
			var templateId = api.CreateTemplate(userKey, templateLabels, templateContent, categoryId, clientId);
			Console.WriteLine("Template created. Id = {0}", templateId);

			var templates = api.GetTemplates(userKey, categoryId, null, null, clientId);
			Console.WriteLine("All Templates retrieved. Count = {0}", templates.Count());

			var template = api.GetTemplate(userKey, templateId, clientId);
			Console.WriteLine("Template retrieved: Name = {0}", template.Name);

			var templateDeleted = api.DeleteTemplate(userKey, templateId, clientId);
			Console.WriteLine("Template deleted: {0}", templateDeleted ? "success" : "failed");

			var categoryDeleted = api.DeleteTemplateCategory(userKey, categoryId, clientId);
			Console.WriteLine("Template category deleted: {0}", categoryDeleted ? "success" : "failed");

			Console.WriteLine(new string('-', 25));
			Console.WriteLine("");
		}
	}
}