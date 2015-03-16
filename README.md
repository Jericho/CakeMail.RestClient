# CakeMail.RestClient

[![Build status](https://ci.appveyor.com/api/projects/status/m8lsx7snrc5jdrdi?svg=true)](https://ci.appveyor.com/project/Jericho/cakemail-restclient)

## About

CakeMail.RestClient is a C# client for the CakeMail service through its RESTful API.

## Features


## Contact

You can contact me on Twitter [@NorthernNomad](https://twitter.com/northernnomad).

## Nuget

CakeMailRestAPI will eventually be available as a Nuget package.

## Release Notes

+ **1.0**    Initial release
 
## Usage

```csharp
var apiKey = "... your api key ...";
var userName = "youremail@whatever.com";
var password = "yourpassword";

var restClient = new CakeMailRestClient(apiKey);

var loginInfo = restClient.Login(userName, password);
var user = restClient.GetUser(loginInfo.UserKey, loginInfo.UserId, loginInfo.ClientId);
```
