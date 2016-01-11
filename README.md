# Mockaroo.NET
Mockaroo.NET is a portable class library that allows you to generate sample data based on your C# objects using the [Mockaroo API](https://www.mockaroo.com/).

## Getting Started
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

If Lorem Ipsum text not to your liking, you can fine-tune the data returned using the following.

```csharp
var schema = new Schema<Employee>();
schema.Replace(x=> x.Name, DataType.FullName);
schema.Replace(x=> x.Phone, DataType.Phone);

var client = new MockarooClient(your_api_key);
IEnumerable<Employee> data = await client.FetchDataAsync<Employee>(schema, records: 100);
```

## Available on NuGet
[![license](https://img.shields.io/badge/license-MIT%20License-blue.svg)](https://github.com/Ackara/Mockaroo.NET/blob/master/LICENSE)
[![version](https://img.shields.io/nuget/v/Gigobyte.Daterpillar.Core.svg?style=flat-square)](https://www.nuget.org/packages?q=Gigobyte.Daterpillar.Core)
[![downloads](https://img.shields.io/nuget/dt/Gigobyte.Daterpillar.Core.svg)](https://img.shields.io/nuget/dt/Gigobyte.Daterpillar.Core.svg)

```
PM> Install-Package Gigobyte.Mockaroo.Core
```

## License
Mockaroo is Copyright © 2015 Ackara Zangetsu and other contributors under the [MIT License](https://opensource.org/licenses/MIT).