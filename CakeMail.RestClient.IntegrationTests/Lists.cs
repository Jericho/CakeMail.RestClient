using CakeMail.RestClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CakeMail.RestClient.IntegrationTests
{
	public static class ListsTests
	{
		public static void ExecuteAllMethods(CakeMailRestClient api, string userKey, long clientId)
		{
			Console.WriteLine("");
			Console.WriteLine(new string('-', 25));
			Console.WriteLine("Executing LISTS methods...");

			var lists = api.Lists.GetLists(userKey, ListStatus.Active, null, ListsSortBy.Name, SortDirection.Descending, null, null, clientId);
			Console.WriteLine("All lists retrieved. Count = {0}", lists.Count());

			var listsCount = api.Lists.GetCount(userKey, ListStatus.Active, null, clientId);
			Console.WriteLine("Lists count = {0}", listsCount);

			var listId = api.Lists.Create(userKey, "Dummy list", "Bob Smith", "bobsmith@fictitiouscomapny.com", true, clientId);
			Console.WriteLine("New list created. Id: {0}", listId);

			var updated = api.Lists.Update(userKey, listId, name: "Updated name", clientId: clientId);
			Console.WriteLine("List updated: {0}", updated ? "success" : "failed");

			var list = api.Lists.Get(userKey, listId, false, false, clientId);
			Console.WriteLine("List retrieved: Name = {0}", list.Name);

			api.Lists.AddField(userKey, listId, "MyCustomField1", FieldType.Integer, clientId);
			api.Lists.AddField(userKey, listId, "MyCustomField2", FieldType.DateTime, clientId);
			api.Lists.AddField(userKey, listId, "MyCustomField3", FieldType.Text, clientId);
			api.Lists.AddField(userKey, listId, "MyCustomField4", FieldType.Memo, clientId);
			Console.WriteLine("Custom fields added to the list");

			var fields = api.Lists.GetFields(userKey, listId, clientId);
			Console.WriteLine("List contains the following fields: {0}", string.Join(", ", fields.Select(f => f.Name)));

			var listMemberId = api.Lists.Subscribe(userKey, listId, "integration@testing.com", true, true, new[]
			{
				new KeyValuePair<string, object>("MyCustomField1", 12345), 
				new KeyValuePair<string, object>("MyCustomField2", DateTime.UtcNow), 
				new KeyValuePair<string, object>("MyCustomField3", "qwerty") 
			}, clientId);
			Console.WriteLine("One member added to the list");

			var subscriber = api.Lists.GetMember(userKey, listId, listMemberId, clientId);
			Console.WriteLine("Subscriber retrieved: {0}", subscriber.Email);

			var member1 = new ListMember()
			{
				Email = "aa@aa.com",
				CustomFields = new Dictionary<string, object>()
				{
					{ "MyCustomField1", 12345 },
					{ "MyCustomField2", DateTime.UtcNow },
					{ "MyCustomField3", "qwerty" }
				}
			};
			var member2 = new ListMember()
			{
				Email = "bbb@bbb.com",
				CustomFields = new Dictionary<string, object>()
				{
					{ "MyCustomField1", 98765},
					{ "MyCustomField2", DateTime.MinValue },
					{ "MyCustomField3", "azerty" }
				}
			};
			var importResult = api.Lists.Import(userKey, listId, new[] { member1, member2 }, false, false, clientId);
			Console.WriteLine("Two members imported into the list");

			var members = api.Lists.GetMembers(userKey, listId, null, null, null, null, null, null, clientId);
			Console.WriteLine("All list members retrieved. Count = {0}", members.Count());

			var membersCount = api.Lists.GetMembersCount(userKey, listId, null, clientId);
			Console.WriteLine("Members count = {0}", membersCount);

			var customFieldsToUpdate = new[]
			{
				new KeyValuePair<string, object>("MyCustomField1", 555555), 
				new KeyValuePair<string, object>("MyCustomField3", "zzzzzzzzzzzzzzzzzzzzzzzzzz")
			};
			var memberUpdated = api.Lists.UpdateMember(userKey, listId, 1, customFieldsToUpdate, clientId);
			Console.WriteLine("Member updated: {0}", memberUpdated ? "success" : "failed");

			var logs = api.Lists.GetLogs(userKey, listId, LogType.Open, false, false, null, null, null, null, clientId);
			Console.WriteLine("Retrieved 'Opens'. Count = {0}", logs.Count());

			var firstSegmentId = api.Segments.Create(userKey, listId, "Segment #1", "(`email` LIKE \"aa%\")", clientId);
			var secondSegmentId = api.Segments.Create(userKey, listId, "Segment #2", "(`email` LIKE \"bb%\")", clientId);
			Console.WriteLine("Two segments created");

			var segments = api.Segments.GetSegments(userKey, listId, 0, 0, true, clientId);
			Console.WriteLine("Segments retrieved. Count = {0}", segments.Count());

			var firstSegmentDeleted = api.Segments.Delete(userKey, firstSegmentId, clientId);
			Console.WriteLine("First segment deleted");

			var secondSegmentDeleted = api.Segments.Delete(userKey, secondSegmentId, clientId);
			Console.WriteLine("Second segment deleted");

			var deleted = api.Lists.Delete(userKey, listId, clientId);
			Console.WriteLine("List deleted: {0}", deleted ? "success" : "failed");

			Console.WriteLine(new string('-', 25));
			Console.WriteLine("");
		}
	}
}
