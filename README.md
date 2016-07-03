# Mockaroo.NET

|Master|Development|
|------|-----------|
|![master](https://gigobyte.visualstudio.com/_apis/public/build/definitions/a2f0cdc6-0844-4f92-94e3-31763d79467f/26/badge)|![development](https://gigobyte.visualstudio.com/_apis/public/build/definitions/a2f0cdc6-0844-4f92-94e3-31763d79467f/26/badge)|

Mockaroo.NET is a portable class library that allows you to generate sample data based on C# objects using the [Mockaroo REST API](https://mockaroo.com/api/docs).

## How it works
Lets say you have the following class.

```csharp
public class Employee
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Phone { get; set; } 
} 
```

Now you want to generate a collection Employee objects to do some testing. All you have to do is.

```csharp
var client = new MockarooClient(your_api_key);
IEnumerable<Employee> data = await client.FetchDataAsync<Employee>(records: 100);
```

This will examine the Employee class properties, then generate random sample objects using the data fetched from the [Mockaroo Rest API](https://www.mockaroo.com/api/docs). The data returned will look something like the following.

```json
[{
	"Id": "156",
	"Name": "aliquam erat volutpat in congue etiam",
	"Phone": "adipiscing molestie hendrerit at vulputate"
}]
```

If Lorem Ipsum text is not to your liking, you can fine-tune the data returned using the following.

```csharp
var schema = new Schema<Employee>();
schema.Assign(x=> x.Name, DataType.FullName);
schema.Assign(x=> x.Phone, new PhoneField() { BlankPercentage = 50 });

var client = new MockarooClient(your_api_key);
IEnumerable<Employee> data = await client.FetchDataAsync<Employee>(schema, records: 1000);
```

The results will look like the following.

```json
[{
	"Id": "156",
	"Name": "John Doe",
	"Phone": "(340) 123-4567"
}]
```

Currently there are over 85+ data types to choose from, check out the [Mockaroo Docs](https://www.mockaroo.com/api/docs) to see the full list. You can also try it at [https://www.mockaroo.com/](https://www.mockaroo.com/) 

## Available on NuGet
[![license](https://img.shields.io/badge/license-MIT%20License-blue.svg)](https://github.com/Ackara/Mockaroo.NET/blob/master/LICENSE)
[![version](https://img.shields.io/nuget/v/Gigobyte.Mockaroo.Core.svg?style=flat-square)](https://www.nuget.org/packages?q=Gigobyte.Mockaroo.Core)

```
PM> Install-Package Gigobyte.Mockaroo.Core
```

## Copyright and License
Mockaroo is Copyright © 2015 Ackara and other contributors under the [MIT License](https://opensource.org/licenses/MIT).
