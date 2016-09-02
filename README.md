# CakeMail.RestClient

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](http://jericho.mit-license.org/)
[![Build status](https://ci.appveyor.com/api/projects/status/m8lsx7snrc5jdrdi?svg=true)](https://ci.appveyor.com/project/Jericho/cakemail-restclient)
[![Coverage Status](https://coveralls.io/repos/Jericho/CakeMail.RestClient/badge.svg)](https://coveralls.io/r/Jericho/CakeMail.RestClient)

## About

CakeMail.RestClient is a C# client for the CakeMail service through its RESTful API.

## Nuget

CakeMailRestAPI is available as a Nuget package.

[![NuGet Version](http://img.shields.io/nuget/v/CakeMail.RestClient.svg)](https://www.nuget.org/packages/CakeMail.RestClient/)
[![AppVeyor](https://img.shields.io/appveyor/ci/Jericho/cakemail-restclient.svg)](https://ci.appveyor.com/project/Jericho/cakemail-restclient)

## Release Notes

+ **4.0.0**
    - All methods are now async.
    - You can pass a cancellation token when invoking an async method.

This means, for example, that the following v3.0 call:

```csharp
var count = cakeMail.Campaigns.GetCount(userKey, MailingStatus.Ongoing);
```

Has been replaced with the following v4.0 call :

```csharp
var count = await cakeMail.Campaigns.GetCountAsync(userKey, MailingStatus.Ongoing);
```

+ **3.0.0**
    - Methods are now logically grouped in separate resources. For instance, all methods related to users are grouped in a resource called 'Users', all methods related to campaigns are grouped in a resource called 'Campaigns', and so on.
    - Methods have been renamed to avoid repetition. For example, GetCampaignsCount has been renamed GetCount off of the new 'Campaigns' resource.

This means, for example, that the following v2.0 call:

```csharp
var count = cakeMail.GetCampaignsCount(userKey, MailingStatus.Ongoing);
```

Has been replaced with the following v3.0 call :

```csharp
var count = cakeMail.Campaigns.GetCount(userKey, MailingStatus.Ongoing);
```


+ **2.0.0**
    - Unique identifiers changed to 'long' instead of 'int'.
    - "Magic strings" replaced with enums. For example, instead of specifying sort direction with 'asc' and 'desc', you can now use SortDirection.Ascending and SortDirection.Descending.
    - Fix bug in CreateTemplateCategory which prevents creating new categories
    - Fix bug in DeleteTemplateCategory which causes an exception to be thrown despite the fact the category was successfuly deleted
    - Fix bug in GetListMembers which causes exception: 'Json does not contain property members'
    - Fix GetTriggerLinksLogs
    - Added XML comments file for convenient intellisense in Visual Studio

+ **1.0.0**
    - Initial release

## Installation

The easiest way to include CakeMail.RestClient in your C# project is by grabing the nuget package:

```
PM> Install-Package CakeMail.RestClient
```

Once you have the CakeMail.RestClient library properly referenced in your project, add the following namespace:

```csharp
using CakeMail.RestClient;
```

## Usage

### Login

```csharp
var apiKey = "... your api key ...";
var userName = "youremail@whatever.com";
var password = "yourpassword";

var cakeMail = new CakeMailRestClient(apiKey);

var loginInfo = await cakeMail.Users.LoginAsync(userName, password);
var userKey = loginInfo.UserKey;
```

Quick note regarding the user key: this value is valid for several days (I don't know exactly how long though) and therefore can be cached for a reasonable period of time. There is no need to repeatedly invoke the 'Login' method to get this value.

### Campaigns

A campaign is simply a way to logically group mailings toghether. You can think of campaigns as 'folders'. In fact, the CakeMail UI has a "Manage folders" button under the "Campaigns" tab where you will see all the campaigns.
A word of caution: the word 'Campaign' is used in the CakeMail UI to refer to mailings which is really confusing!

```csharp
var campaigns = await cakeMail.Campaigns.GetCampaignsAsync(userKey, status: MailingStatus.Ongoing, sortBy: MailingSortBy.Name, sortDirection: SortDirection.Ascending, limit: 50, offset: 0);
var campaignsCount = await cakeMail.Campaigns.GetCountAsync(userKey, MailingStatus.Ongoing);

var campaignId = await cakeMail.Campaigns.CreateAsync(userKey, "2015 User Conference");
var campaign = await cakeMail.Campaigns.GetAsync(userKey, campaignId);
var deleted = await cakeMail.Campaigns.DeleteAsync(userKey, campaignId);

```

### Lists

A List is a collection of subscribers (or List Members, or Records). Each subscriber or List Member is uniquely identified by their email address, and may include an limited amount of Fields containing demographic information associated to each email address.

```csharp

var lists = await cakeMail.Lists.GetListsAsync(userKey, sortBy: ListSortBy.Name, sortDirection: SortDirection.Descending, limit: 50, offset: 0);
var listsCount = await cakeMail.Lists.GetCountAsync(userKey);

var listId = await cakeMail.Lists.CreateAsync(userKey, "Customers and Prospects", "The XYZ Marketing Group", "marketing@yourcompany.com", true);
await cakeMail.Lists.AddFieldAsync(userKey, listId, "first_name", "text");
await cakeMail.Lists.AddFieldAsync(userKey, listId, "last_name", "text");
await cakeMail.Lists.AddFieldAsync(userKey, listId, "customer_since", "datetime");
```

You can add members to your list like so:

```csharp

await cakeMail.Lists.SubscribeAsync(userKey, listId, "bob_the_customer@hotmail.com", true, true, new[]
{
    new KeyValuePair<string, object>("first_name", "Bob"), 
    new KeyValuePair<string, object>("last_name", "Smith"), 
    new KeyValuePair<string, object>("customer_since", DateTime.UtcNow) 
});
await cakeMail.Lists.SubscribeAsync(userKey, listId, "jane_the_prospect@hotmail.com", true, true, new[]
{
    new KeyValuePair<string, object>("first_name", "Jane"), 
    new KeyValuePair<string, object>("last_name", "Doe")
});
```

or you can import a group of members:

```csharp
var member1 = new ListMember()
{
    Email = "bob_the_customer@hotmail.com",
    CustomFields = new Dictionary<string, object>()
    {
        { "first_name", "Bob" },
        { "last_name", "Smith" },
        { "customer_since", DateTime.UtcNow }
    }
};

var member2 = new ListMember()
{
    Email = "jane_the_prospect@hotmail.com",
    CustomFields = new Dictionary<string, object>()
    {
        { "first_name", "Jane" },
        { "last_name", "Doe" }
    }
};

var importResult = await cakeMail.Lists.ImportAsync(userKey, listId, new[] { member1, member2 });
```

### Mailings

A mailing is an email campaign. It can be used to send standard email campaigns, A/B split campaigns or recurring campaigns.

```csharp
var invitationMailingId = await cakeMail.Mailings.CreateAsync(userKey, "2015 User Conference invitation", campaignId);
await cakeMail.Mailings.UpdateAsync(userKey, invitationMailingId, listId: listId, htmlContent: "<html><body>You are invited to attend our annual user conference</body></html>", textContent: "You are invited to attend our annual user conference", subject: "Invitation to our 2015 user conference");
await cakeMail.Mailings.SheduleAsync(userKey, invitationMailingId);

var reminderMailingId = await cakeMail.Mailings.CreateAsync(userKey, "2015 User Conference reminder", campaignId);
await cakeMail.Mailings.UpdateAsync(userKey, reminderMailingId, listId: listId, htmlContent: "<html><body>Don't forget our upcoming annual user conference</body></html>", textContent: "Don't forget our upcoming annual user conference", subject: "Reminder about our 2015 user conference");
await cakeMail.Mailings.SheduleAsync(userKey, reminderMailingId, DateTime.UtcNow.AddDays(2));
```
