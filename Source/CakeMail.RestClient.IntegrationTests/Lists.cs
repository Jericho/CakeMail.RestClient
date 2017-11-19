using CakeMail.RestClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeMail.RestClient.IntegrationTests
{
	public static class ListsTests
	{
		public static async Task ExecuteAllMethods(CakeMailRestClient api, string userKey, long clientId)
		{
			Console.WriteLine("");
			Console.WriteLine(new string('-', 25));
			Console.WriteLine("Executing LISTS methods...");

			var lists = await api.Lists.GetListsAsync(userKey, ListStatus.Active, null, ListsSortBy.Name, SortDirection.Descending, null, null, clientId).ConfigureAwait(false);
			Console.WriteLine("All lists retrieved. Count = {0}", lists.Count());

			var listsCount = await api.Lists.GetCountAsync(userKey, ListStatus.Active, null, clientId).ConfigureAwait(false);
			Console.WriteLine("Lists count = {0}", listsCount);

			var listId = await api.Lists.CreateAsync(userKey, "Dummy list", "Bob Smith", "bobsmith@fictitiouscomapny.com", true, clientId).ConfigureAwait(false);
			Console.WriteLine("New list created. Id: {0}", listId);

			var updated = await api.Lists.UpdateAsync(userKey, listId, name: "Updated name", clientId: clientId).ConfigureAwait(false);
			Console.WriteLine("List updated: {0}", updated ? "success" : "failed");

			var list = await api.Lists.GetAsync(userKey, listId, false, false, clientId).ConfigureAwait(false);
			Console.WriteLine("List retrieved: Name = {0}", list.Name);

			await api.Lists.AddFieldAsync(userKey, listId, "MyCustomField1", FieldType.Integer, clientId).ConfigureAwait(false);
			await api.Lists.AddFieldAsync(userKey, listId, "MyCustomField2", FieldType.DateTime, clientId).ConfigureAwait(false);
			await api.Lists.AddFieldAsync(userKey, listId, "MyCustomField3", FieldType.Text, clientId).ConfigureAwait(false);
			await api.Lists.AddFieldAsync(userKey, listId, "MyCustomField4", FieldType.Memo, clientId).ConfigureAwait(false);
			Console.WriteLine("Custom fields added to the list");

			var fields = await api.Lists.GetFieldsAsync(userKey, listId, clientId).ConfigureAwait(false);
			Console.WriteLine("List contains the following fields: {0}", string.Join(", ", fields.Select(f => f.Name)));

			var subscriberCustomFields = new Dictionary<string, object>()
			{
				{"MyCustomField1", 12345 },
				{ "MyCustomField2", DateTime.UtcNow },
				{ "MyCustomField3", "qwerty" }
			};
			var listMemberId = await api.Lists.SubscribeAsync(userKey, listId, "integration@testing.com", true, true, subscriberCustomFields, clientId).ConfigureAwait(false);
			Console.WriteLine("One member added to the list");

			var query = "`email`=\"integration@testing.com\"";
			var subscribers = await api.Lists.GetMembersAsync(userKey, listId, query: query, clientId: clientId).ConfigureAwait(false);
			Console.WriteLine("Subscribers retrieved: {0}", subscribers.Count());

			var subscriber = await api.Lists.GetMemberAsync(userKey, listId, listMemberId, clientId).ConfigureAwait(false);
			Console.WriteLine("Subscriber retrieved: {0}", subscriber.Email);

			var member1 = new ListMember
			{
				Email = "aa@aa.com",
				CustomFields = new Dictionary<string, object>
				{
					{ "MyCustomField1", 12345 },
					{ "MyCustomField2", DateTime.UtcNow },
					{ "MyCustomField3", "qwerty" }
				}
			};
			var member2 = new ListMember
			{
				Email = "bbb@bbb.com",
				CustomFields = new Dictionary<string, object>
				{
					{ "MyCustomField1", 98765 },
					{ "MyCustomField2", DateTime.MinValue },
					{ "MyCustomField3", "azerty" }
				}
			};
			var importResult = await api.Lists.ImportAsync(userKey, listId, new[] { member1, member2 }, false, false, clientId).ConfigureAwait(false);
			Console.WriteLine("Two members imported into the list");

			var members = await api.Lists.GetMembersAsync(userKey, listId, null, null, null, null, null, null, clientId).ConfigureAwait(false);
			Console.WriteLine("All list members retrieved. Count = {0}", members.Count());

			var membersCount = await api.Lists.GetMembersCountAsync(userKey, listId, null, clientId).ConfigureAwait(false);
			Console.WriteLine("Members count = {0}", membersCount);

			var customFieldsToUpdate = new Dictionary<string, object>
			{
				{ "MyCustomField1", 555555 },
				{ "MyCustomField3", "zzzzzzzzzzzzzzzzzzzzzzzzzz" }
			};
			var memberUpdated = await api.Lists.UpdateMemberAsync(userKey, listId, 1, customFieldsToUpdate, clientId).ConfigureAwait(false);
			Console.WriteLine("Member updated: {0}", memberUpdated ? "success" : "failed");

			var logs = await api.Lists.GetLogsAsync(userKey, listId, LogType.Open, false, false, null, null, null, null, clientId).ConfigureAwait(false);
			Console.WriteLine("Retrieved 'Opens'. Count = {0}", logs.Count());

			var firstSegmentId = await api.Segments.CreateAsync(userKey, listId, "Segment #1", "(`email` LIKE \"aa%\")", clientId).ConfigureAwait(false);
			var secondSegmentId = await api.Segments.CreateAsync(userKey, listId, "Segment #2", "(`email` LIKE \"bb%\")", clientId).ConfigureAwait(false);
			Console.WriteLine("Two segments created");

			var segments = await api.Segments.GetSegmentsAsync(userKey, listId, 0, 0, true, clientId).ConfigureAwait(false);
			Console.WriteLine("Segments retrieved. Count = {0}", segments.Count());

			var firstSegmentDeleted = await api.Segments.DeleteAsync(userKey, firstSegmentId, clientId).ConfigureAwait(false);
			Console.WriteLine("First segment deleted");

			var secondSegmentDeleted = await api.Segments.DeleteAsync(userKey, secondSegmentId, clientId).ConfigureAwait(false);
			Console.WriteLine("Second segment deleted");

			var deleted = await api.Lists.DeleteAsync(userKey, listId, clientId).ConfigureAwait(false);
			Console.WriteLine("List deleted: {0}", deleted ? "success" : "failed");

			Console.WriteLine(new string('-', 25));
			Console.WriteLine("");
		}
	}
}
