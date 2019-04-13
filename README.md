# Mockaroo.NET

[![version](https://img.shields.io/nuget/v/Mockaroo.NET.svg?style=flat-square)](https://www.nuget.org/packages?q=Mockaroo.NET)

## The Problem

You are working on a .NET project and you have to do some testing, but first you need load data into your program. Creating data by hand can be tedious, especially when you are at the phase where refactoring is frequent.

## The Solution

**Mockaroo.NET** is a netstandard library that allows you to generate sample data based on your classes using the [Mockaroo REST API](https://mockaroo.com/api/docs). 

### Usage
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

The method will examine the `Employee` class properties, then generate objects using the data fetched from the [Mockaroo Rest API](https://www.mockaroo.com/api/docs). The data returned will look something like the following.

```json
[{
	"Id": "156",
	"Name": "aliquam erat volutpat in congue etiam",
	"Phone": "adipiscing molestie hendrerit at vulputate"
}]
```

If Lorem Ipsum text is not to your liking, you can fine-tune the data by using the following.

```csharp
var schema = new Schema<Employee>();
schema.Replace(x=> x.Name, DataType.FullName);
schema.Replace(x=> x.Phone, new PhoneField() { BlankPercentage = 50 });

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

Currently there are over 140+ data types to choose from, check out the Mockaroo [documentation](https://www.mockaroo.com/api/docs) to see the full list. You can also try it at [mockaroo.com](https://www.mockaroo.com/) .

#### Reusing Data

The number of calls one can make to [Mockaroo](https://www.mockaroo.com/api/docs) is limited, therefore it is a good idea to save and reuse the data retrieved from previous calls. The `MockarooRepository` class enables you to do just that. In addition, reusing data also means that the returned objects will be consistent instead of returning fresh data for each call. However keep in mind that if the `Schema` changes a new dataset will be retrieved.

## Contributing

**Prequistes:**
* Visual Studio 2015+
* [Mockaroo API Key](https://mockaroo.com/users/sign_up)

**Note:** 
Run `PS> .\build.ps1 configure ` to create the api key.
