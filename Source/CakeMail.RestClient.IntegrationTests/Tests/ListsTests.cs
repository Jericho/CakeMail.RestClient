using CakeMail.RestClient.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.IntegrationTests.Tests
{
	public static class ListsTests
	{
		public static async Task ExecuteAllMethods(ICakeMailRestClient client, string userKey, long clientId, TextWriter log, CancellationToken cancellationToken)
		{
			await log.WriteLineAsync("\n***** LISTS *****").ConfigureAwait(false);

			var lists = await client.Lists.GetListsAsync(userKey, ListStatus.Active, null, ListsSortBy.Name, SortDirection.Descending, null, null, clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"All lists retrieved. Count = {lists.Count()}").ConfigureAwait(false);

			var listsCount = await client.Lists.GetCountAsync(userKey, ListStatus.Active, null, clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"Lists count = {listsCount}").ConfigureAwait(false);

			var listId = await client.Lists.CreateAsync(userKey, "Dummy list", "Bob Smith", "bobsmith@fictitiouscomapny.com", true, clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"New list created. Id: {listId}").ConfigureAwait(false);

			var updated = await client.Lists.UpdateAsync(userKey, listId, name: "Updated name", clientId: clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"List updated: {(updated ? "success" : "failed")}").ConfigureAwait(false);

			var list = await client.Lists.GetAsync(userKey, listId, false, false, clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"List retrieved: Name = {list.Name}").ConfigureAwait(false);

			await client.Lists.AddFieldAsync(userKey, listId, "MyCustomField1", FieldType.Integer, clientId).ConfigureAwait(false);
			await client.Lists.AddFieldAsync(userKey, listId, "MyCustomField2", FieldType.DateTime, clientId).ConfigureAwait(false);
			await client.Lists.AddFieldAsync(userKey, listId, "MyCustomField3", FieldType.Text, clientId).ConfigureAwait(false);
			await client.Lists.AddFieldAsync(userKey, listId, "MyCustomField4", FieldType.Memo, clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"Custom fields added to the list").ConfigureAwait(false);

			var fields = await client.Lists.GetFieldsAsync(userKey, listId, clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"List contains the following fields: {string.Join(", ", fields.Select(f => f.Name))}").ConfigureAwait(false);

			var subscriberCustomFields = new Dictionary<string, object>()
			{
				{"MyCustomField1", 12345 },
				{ "MyCustomField2", DateTime.UtcNow },
				{ "MyCustomField3", "qwerty" }
			};
			var listMemberId = await client.Lists.SubscribeAsync(userKey, listId, "integration@testing.com", true, true, subscriberCustomFields, clientId).ConfigureAwait(false);
			await log.WriteLineAsync("One member added to the list").ConfigureAwait(false);

			var query = "`email`=\"integration@testing.com\"";
			var subscribers = await client.Lists.GetMembersAsync(userKey, listId, query: query, clientId: clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"Subscribers retrieved: {subscribers.Count()}").ConfigureAwait(false);

			var subscriber = await client.Lists.GetMemberAsync(userKey, listId, listMemberId, clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"Subscriber retrieved: {subscriber.Email}").ConfigureAwait(false);

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
			var importResult = await client.Lists.ImportAsync(userKey, listId, new[] { member1, member2 }, false, false, clientId).ConfigureAwait(false);
			await log.WriteLineAsync("Two members imported into the list").ConfigureAwait(false);

			var members = await client.Lists.GetMembersAsync(userKey, listId, null, null, null, null, null, null, clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"All list members retrieved. Count = {members.Count()}").ConfigureAwait(false);

			var membersCount = await client.Lists.GetMembersCountAsync(userKey, listId, null, clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"Members count = {membersCount}").ConfigureAwait(false);

			var customFieldsToUpdate = new Dictionary<string, object>
			{
				{ "MyCustomField1", 555555 },
				{ "MyCustomField3", "zzzzzzzzzzzzzzzzzzzzzzzzzz" }
			};
			var memberUpdated = await client.Lists.UpdateMemberAsync(userKey, listId, 1, customFieldsToUpdate, clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"Member updated: {(memberUpdated ? "success" : "failed")}").ConfigureAwait(false);

			var logs = await client.Lists.GetLogsAsync(userKey, listId, LogType.Open, false, false, null, null, null, null, clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"Retrieved 'Opens'. Count = {logs.Count()}").ConfigureAwait(false);

			var firstSegmentId = await client.Segments.CreateAsync(userKey, listId, "Segment #1", "(`email` LIKE \"aa%\")", clientId).ConfigureAwait(false);
			var secondSegmentId = await client.Segments.CreateAsync(userKey, listId, "Segment #2", "(`email` LIKE \"bb%\")", clientId).ConfigureAwait(false);
			await log.WriteLineAsync("Two segments created").ConfigureAwait(false);

			var segments = await client.Segments.GetSegmentsAsync(userKey, listId, 0, 0, true, clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"Segments retrieved. Count = {segments.Count()}").ConfigureAwait(false);

			var firstSegmentDeleted = await client.Segments.DeleteAsync(userKey, firstSegmentId, clientId).ConfigureAwait(false);
			await log.WriteLineAsync("First segment deleted").ConfigureAwait(false);

			var secondSegmentDeleted = await client.Segments.DeleteAsync(userKey, secondSegmentId, clientId).ConfigureAwait(false);
			await log.WriteLineAsync("Second segment deleted").ConfigureAwait(false);

			var deleted = await client.Lists.DeleteAsync(userKey, listId, clientId).ConfigureAwait(false);
			await log.WriteLineAsync($"List deleted: {(deleted ? "success" : "failed")}").ConfigureAwait(false);
		}
	}
}
