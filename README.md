# CakeMail.RestClient

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](http://jericho.mit-license.org/)
[![Build status](https://ci.appveyor.com/api/projects/status/m8lsx7snrc5jdrdi?svg=true)](https://ci.appveyor.com/project/Jericho/cakemail-restclient)
[![Coverage Status](https://coveralls.io/repos/Jericho/CakeMail.RestClient/badge.svg)](https://coveralls.io/r/Jericho/CakeMail.RestClient)

## About

CakeMail.RestClient is a C# client for the CakeMail service through its RESTful API.

## Contact

You can contact me on Twitter [@NorthernNomad](https://twitter.com/northernnomad).

## Nuget

CakeMailRestAPI is available as a Nuget package.

[![NuGet Version](http://img.shields.io/nuget/v/CakeMail.RestClient.svg)](https://www.nuget.org/packages/CakeMail.RestClient/)
[![NuGet Downloads](http://img.shields.io/nuget/dt/CakeMail.RestClient.svg)](https://www.nuget.org/packages/CakeMail.RestClient/)

## Release Notes

+ **2.0.0**
    - Unique identifiers changed to 'long' instead of 'int'.
    - "Magic strings" replaced with enums. For example, instead of specifying sort direction with 'asc' and 'desc', you can now use SortDirection.Ascending and SortDirection.Descending.


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

var loginInfo = cakeMail.Login(userName, password);
var userKey = loginInfo.UserKey;
```

Quick note regarding the user key: this value is valid for several days (I don't know exactly how long though) and therefore can be cached for a reasonable period of time. There is no need to repeatedly invoke the 'Login' method to get this value.

### Campaigns

A campaign is simply a way to logically group mailings toghether. You can think of campaigns as 'folders'. In fact, the CakeMail UI has a "Manage folders" button under the "Campaigns" tab where you will see all the campaigns.
A word of caution: the word 'Campaign' is used in the CakeMail UI to refer to mailings which is really confusing!

```csharp
var campaigns = cakeMail.GetCampaigns(userKey, status: MailingStatus.Ongoing, sortBy: MailingSortBy.Name, sortDirection: SortDirection.Ascending, limit: 50, offset: 0);
var campaignsCount = cakeMail.GetCampaignsCount(userKey, MailingStatus.Ongoing);

var campaignId = cakeMail.CreateCampaign(userKey, "2015 User Conference");
var campaign = cakeMail.GetCampaign(userKey, campaignId);
var deleted = cakeMail.DeleteCampaign(userKey, campaignId);

```

### Lists

A List is a collection of subscribers (or List Members, or Records). Each subscriber or List Member is uniquely identified by their email address, and may include an limited amount of Fields containing demographic information associated to each email address.

```csharp
var lists = cakeMail.GetLists(userKey, sortBy: ListSortBy.Name, sortDirection: SortDirection.Descending, limit: 50, offset: 0);
var listsCount = cakeMail.GetListsCount(userKey);

var listId = cakeMail.CreateList(userKey, "Customers and Prospects", "The XYZ Marketing Group", "marketing@yourcompany.com", true);
cakeMail.AddListField(userKey, listId, "first_name", "text");
cakeMail.AddListField(userKey, listId, "last_name", "text");
cakeMail.AddListField(userKey, listId, "customer_since", "datetime");
```

You can add members to your list like so:
```
cakeMail.Subscribe(userKey, listId, "bob_the_customer@hotmail.com", true, true, new[] {
    new KeyValuePair<string, object>("first_name", "Bob"), 
    new KeyValuePair<string, object>("last_name", "Smith"), 
    new KeyValuePair<string, object>("customer_since", DateTime.UtcNow) 
});
cakeMail.Subscribe(userKey, listId, "jane_the_prospect@hotmail.com", true, true, new[] {
    new KeyValuePair<string, object>("first_name", "Jane"), 
    new KeyValuePair<string, object>("last_name", "Doe")
});
```
or you can import a group of members:
```
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

var importResult = cakeMail.Import(userKey, listId, new[] { member1, member2 });
```

### Mailings

A mailing is an email campaign. It can be used to send standard email campaigns, A/B split campaigns or recurring campaigns.

```
var invitationMailingId = cakeMail.CreateMailing(userKey, "2015 User Conference invitation", campaignId);
cakeMail.UpdateMailing(userKey, invitationMailingId, listId: listId, htmlContent: "<html><body>You are invited to attend our annual user conference</body</html>", textContent: "You are invited to attend our annual user conference", subject: "Invitation to our 2015 user conference");
cakeMail.ScheduleMailing(userKey, invitationMailingId);

var reminderMailingId = cakeMail.CreateMailing(userKey, "2015 User Conference reminder", campaignId);
cakeMail.UpdateMailing(userKey, reminderMailingId, listId: listId, htmlContent: "<html><body>Don't forget our upcoming annual user conference</body</html>", textContent: "Don't forget our upcoming annual user conference", subject: "Reminder about our 2015 user conference");
cakeMail.ScheduleMailing(userKey, reminderMailingId, DateTime.UtcNow.AddDays(2));
```
