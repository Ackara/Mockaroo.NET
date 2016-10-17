namespace Gigobyte.Mockaroo
{
	/// <summary>
    /// Provides helper methods for the <see cref="Gigobyte.Mockaroo" /> namespace.
    /// </summary>
	public static partial class Extensions
	{
		/// <summary>
		/// Converts this <see cref="DataType" /> instance to its equivalent <see cref="string" /> representation.
		/// </summary>
		/// <param name="dataType">Type of the data.</param>
		/// <returns>A <see cref="string" />.</returns>
		public static string ToMockarooTypeName(this DataType dataType)
		{
			switch (dataType)
			{
				default:
					return string.Empty;
				case DataType.AppBundleID:
					return "App Bundle ID";
				case DataType.AppName:
					return "App Name";
				case DataType.AppVersion:
					return "App Version";
				case DataType.Avatar:
					return "Avatar";
				case DataType.Base64ImageURL:
					return "Base64 Image URL";
				case DataType.BinomialDistribution:
					return "Binomial Distribution";
				case DataType.BitcoinAddress:
					return "Bitcoin Address";
				case DataType.Blank:
					return "Blank";
				case DataType.Boolean:
					return "Boolean";
				case DataType.Buzzword:
					return "Buzzword";
				case DataType.CatchPhrase:
					return "Catch Phrase";
				case DataType.City:
					return "City";
				case DataType.Color:
					return "Color";
				case DataType.CompanyName:
					return "Company Name";
				case DataType.Country:
					return "Country";
				case DataType.CountryCode:
					return "Country Code";
				case DataType.CreditCardNumber:
					return "Credit Card #";
				case DataType.CreditCardType:
					return "Credit Card Type";
				case DataType.Currency:
					return "Currency";
				case DataType.CurrencyCode:
					return "Currency Code";
				case DataType.CustomList:
					return "Custom List";
				case DataType.DatasetColumn:
					return "Dataset Column";
				case DataType.Date:
					return "Date";
				case DataType.DomainName:
					return "Domain Name";
				case DataType.DrugCompany:
					return "Drug Company";
				case DataType.DrugNameBrand:
					return "Drug Name (Brand)";
				case DataType.DrugNameGeneric:
					return "Drug Name (Generic)";
				case DataType.DummyImageURL:
					return "Dummy Image URL";
				case DataType.DUNSNumber:
					return "DUNS Number";
				case DataType.EIN:
					return "EIN";
				case DataType.EmailAddress:
					return "Email Address";
				case DataType.Encrypt:
					return "Encrypt";
				case DataType.ExponentialDistribution:
					return "Exponential Distribution";
				case DataType.FakeCompanyName:
					return "Fake Company Name";
				case DataType.FamilyNameChinese:
					return "Family Name (Chinese)";
				case DataType.FDANDCCode:
					return "FDA NDC Code";
				case DataType.FileName:
					return "File Name";
				case DataType.FirstName:
					return "First Name";
				case DataType.FirstNameEuropean:
					return "First Name (European)";
				case DataType.FirstNameFemale:
					return "First Name (Female)";
				case DataType.FirstNameMale:
					return "First Name (Male)";
				case DataType.Formula:
					return "Formula";
				case DataType.Frequency:
					return "Frequency";
				case DataType.FullName:
					return "Full Name";
				case DataType.Gender:
					return "Gender";
				case DataType.GenderAbbreviated:
					return "Gender (abbrev)";
				case DataType.GeometricDistribution:
					return "Geometric Distribution";
				case DataType.GivenNameChinese:
					return "Given Name (Chinese)";
				case DataType.GUID:
					return "GUID";
				case DataType.HexColor:
					return "Hex Color";
				case DataType.IBAN:
					return "IBAN";
				case DataType.ICD10DiagnosisCode:
					return "ICD10 Diagnosis Code";
				case DataType.ICD10DxDescLong:
					return "ICD10 Dx Desc (Long)";
				case DataType.ICD10DxDescShort:
					return "ICD10 Dx Desc (Short)";
				case DataType.ICD10ProcDescLong:
					return "ICD10 Proc Desc (Long)";
				case DataType.ICD10ProcDescShort:
					return "ICD10 Proc Desc (Short)";
				case DataType.ICD10ProcedureCode:
					return "ICD10 Procedure Code";
				case DataType.ICD9DiagnosisCode:
					return "ICD9 Diagnosis Code";
				case DataType.ICD9DxDescLong:
					return "ICD9 Dx Desc (Long)";
				case DataType.ICD9DxDescShort:
					return "ICD9 Dx Desc (Short)";
				case DataType.ICD9ProcDescLong:
					return "ICD9 Proc Desc (Long)";
				case DataType.ICD9ProcDescShort:
					return "ICD9 Proc Desc (Short)";
				case DataType.ICD9ProcedureCode:
					return "ICD9 Procedure Code";
				case DataType.IPAddressV4:
					return "IP Address v4";
				case DataType.IPAddressV4CIDR:
					return "IP Address v4 CIDR";
				case DataType.IPAddressV6:
					return "IP Address v6";
				case DataType.IPAddressV6CIDR:
					return "IP Address v6 CIDR";
				case DataType.ISBN:
					return "ISBN";
				case DataType.JobTitle:
					return "Job Title";
				case DataType.JSONArray:
					return "JSON Array";
				case DataType.Language:
					return "Language";
				case DataType.LastName:
					return "Last Name";
				case DataType.Latitude:
					return "Latitude";
				case DataType.LinkedInSkill:
					return "LinkedIn Skill";
				case DataType.Longitude:
					return "Longitude";
				case DataType.MACAddress:
					return "MAC Address";
				case DataType.MD5:
					return "MD5";
				case DataType.MIMEType:
					return "MIME Type";
				case DataType.Money:
					return "Money";
				case DataType.MongoDBObjectID:
					return "MongoDB ObjectID";
				case DataType.NaughtyString:
					return "Naughty String";
				case DataType.NormalDistribution:
					return "Normal Distribution";
				case DataType.Number:
					return "Number";
				case DataType.Paragraphs:
					return "Paragraphs";
				case DataType.Password:
					return "Password";
				case DataType.Phone:
					return "Phone";
				case DataType.PoissonDistribution:
					return "Poisson Distribution";
				case DataType.PostalCode:
					return "Postal Code";
				case DataType.Race:
					return "Race";
				case DataType.RegularExpression:
					return "Regular Expression";
				case DataType.RowNumber:
					return "Row Number";
				case DataType.Scenario:
					return "Scenario";
				case DataType.Sentences:
					return "Sentences";
				case DataType.Sequence:
					return "Sequence";
				case DataType.SHA1:
					return "SHA1";
				case DataType.SHA256:
					return "SHA256";
				case DataType.ShirtSize:
					return "Shirt Size";
				case DataType.ShortHexColor:
					return "Short Hex Color";
				case DataType.Slogan:
					return "Slogan";
				case DataType.SSN:
					return "SSN";
				case DataType.State:
					return "State";
				case DataType.StateAbbreviated:
					return "State (abbrev)";
				case DataType.StreetAddress:
					return "Street Address";
				case DataType.StreetName:
					return "Street Name";
				case DataType.StreetNumber:
					return "Street Number";
				case DataType.StreetSuffix:
					return "Street Suffix";
				case DataType.Suffix:
					return "Suffix";
				case DataType.Template:
					return "Template";
				case DataType.Time:
					return "Time";
				case DataType.TimeZone:
					return "Time Zone";
				case DataType.Title:
					return "Title";
				case DataType.TopLevelDomain:
					return "Top Level Domain";
				case DataType.URL:
					return "URL";
				case DataType.UserAgent:
					return "User Agent";
				case DataType.UserName:
					return "Username";
				case DataType.Words:
					return "Words";
			}
		}

		/// <summary>
		/// Converts the string representation of a <see cref="DataType" /> into its <see cref="DataType" /> equivalent.
		/// </summary>
		/// <param name="typeName">Name of the data type.</param>
        /// <returns>A <see cref="DataType" />.</returns>
		/// <exception cref="System.ArgumentException"></exception>
		public static DataType ToDataType(this string typeName)
		{
			switch (typeName)
			{
				default:
					throw new System.ArgumentException($"'{typeName}' is not a valid value.", nameof(typeName));
				case "App Bundle ID":
					return DataType.AppBundleID;
				case "App Name":
					return DataType.AppName;
				case "App Version":
					return DataType.AppVersion;
				case "Avatar":
					return DataType.Avatar;
				case "Base64 Image URL":
					return DataType.Base64ImageURL;
				case "Binomial Distribution":
					return DataType.BinomialDistribution;
				case "Bitcoin Address":
					return DataType.BitcoinAddress;
				case "Blank":
					return DataType.Blank;
				case "Boolean":
					return DataType.Boolean;
				case "Buzzword":
					return DataType.Buzzword;
				case "Catch Phrase":
					return DataType.CatchPhrase;
				case "City":
					return DataType.City;
				case "Color":
					return DataType.Color;
				case "Company Name":
					return DataType.CompanyName;
				case "Country":
					return DataType.Country;
				case "Country Code":
					return DataType.CountryCode;
				case "Credit Card #":
					return DataType.CreditCardNumber;
				case "Credit Card Type":
					return DataType.CreditCardType;
				case "Currency":
					return DataType.Currency;
				case "Currency Code":
					return DataType.CurrencyCode;
				case "Custom List":
					return DataType.CustomList;
				case "Dataset Column":
					return DataType.DatasetColumn;
				case "Date":
					return DataType.Date;
				case "Domain Name":
					return DataType.DomainName;
				case "Drug Company":
					return DataType.DrugCompany;
				case "Drug Name (Brand)":
					return DataType.DrugNameBrand;
				case "Drug Name (Generic)":
					return DataType.DrugNameGeneric;
				case "Dummy Image URL":
					return DataType.DummyImageURL;
				case "DUNS Number":
					return DataType.DUNSNumber;
				case "EIN":
					return DataType.EIN;
				case "Email Address":
					return DataType.EmailAddress;
				case "Encrypt":
					return DataType.Encrypt;
				case "Exponential Distribution":
					return DataType.ExponentialDistribution;
				case "Fake Company Name":
					return DataType.FakeCompanyName;
				case "Family Name (Chinese)":
					return DataType.FamilyNameChinese;
				case "FDA NDC Code":
					return DataType.FDANDCCode;
				case "File Name":
					return DataType.FileName;
				case "First Name":
					return DataType.FirstName;
				case "First Name (European)":
					return DataType.FirstNameEuropean;
				case "First Name (Female)":
					return DataType.FirstNameFemale;
				case "First Name (Male)":
					return DataType.FirstNameMale;
				case "Formula":
					return DataType.Formula;
				case "Frequency":
					return DataType.Frequency;
				case "Full Name":
					return DataType.FullName;
				case "Gender":
					return DataType.Gender;
				case "Gender (abbrev)":
					return DataType.GenderAbbreviated;
				case "Geometric Distribution":
					return DataType.GeometricDistribution;
				case "Given Name (Chinese)":
					return DataType.GivenNameChinese;
				case "GUID":
					return DataType.GUID;
				case "Hex Color":
					return DataType.HexColor;
				case "IBAN":
					return DataType.IBAN;
				case "ICD10 Diagnosis Code":
					return DataType.ICD10DiagnosisCode;
				case "ICD10 Dx Desc (Long)":
					return DataType.ICD10DxDescLong;
				case "ICD10 Dx Desc (Short)":
					return DataType.ICD10DxDescShort;
				case "ICD10 Proc Desc (Long)":
					return DataType.ICD10ProcDescLong;
				case "ICD10 Proc Desc (Short)":
					return DataType.ICD10ProcDescShort;
				case "ICD10 Procedure Code":
					return DataType.ICD10ProcedureCode;
				case "ICD9 Diagnosis Code":
					return DataType.ICD9DiagnosisCode;
				case "ICD9 Dx Desc (Long)":
					return DataType.ICD9DxDescLong;
				case "ICD9 Dx Desc (Short)":
					return DataType.ICD9DxDescShort;
				case "ICD9 Proc Desc (Long)":
					return DataType.ICD9ProcDescLong;
				case "ICD9 Proc Desc (Short)":
					return DataType.ICD9ProcDescShort;
				case "ICD9 Procedure Code":
					return DataType.ICD9ProcedureCode;
				case "IP Address v4":
					return DataType.IPAddressV4;
				case "IP Address v4 CIDR":
					return DataType.IPAddressV4CIDR;
				case "IP Address v6":
					return DataType.IPAddressV6;
				case "IP Address v6 CIDR":
					return DataType.IPAddressV6CIDR;
				case "ISBN":
					return DataType.ISBN;
				case "Job Title":
					return DataType.JobTitle;
				case "JSON Array":
					return DataType.JSONArray;
				case "Language":
					return DataType.Language;
				case "Last Name":
					return DataType.LastName;
				case "Latitude":
					return DataType.Latitude;
				case "LinkedIn Skill":
					return DataType.LinkedInSkill;
				case "Longitude":
					return DataType.Longitude;
				case "MAC Address":
					return DataType.MACAddress;
				case "MD5":
					return DataType.MD5;
				case "MIME Type":
					return DataType.MIMEType;
				case "Money":
					return DataType.Money;
				case "MongoDB ObjectID":
					return DataType.MongoDBObjectID;
				case "Naughty String":
					return DataType.NaughtyString;
				case "Normal Distribution":
					return DataType.NormalDistribution;
				case "Number":
					return DataType.Number;
				case "Paragraphs":
					return DataType.Paragraphs;
				case "Password":
					return DataType.Password;
				case "Phone":
					return DataType.Phone;
				case "Poisson Distribution":
					return DataType.PoissonDistribution;
				case "Postal Code":
					return DataType.PostalCode;
				case "Race":
					return DataType.Race;
				case "Regular Expression":
					return DataType.RegularExpression;
				case "Row Number":
					return DataType.RowNumber;
				case "Scenario":
					return DataType.Scenario;
				case "Sentences":
					return DataType.Sentences;
				case "Sequence":
					return DataType.Sequence;
				case "SHA1":
					return DataType.SHA1;
				case "SHA256":
					return DataType.SHA256;
				case "Shirt Size":
					return DataType.ShirtSize;
				case "Short Hex Color":
					return DataType.ShortHexColor;
				case "Slogan":
					return DataType.Slogan;
				case "SSN":
					return DataType.SSN;
				case "State":
					return DataType.State;
				case "State (abbrev)":
					return DataType.StateAbbreviated;
				case "Street Address":
					return DataType.StreetAddress;
				case "Street Name":
					return DataType.StreetName;
				case "Street Number":
					return DataType.StreetNumber;
				case "Street Suffix":
					return DataType.StreetSuffix;
				case "Suffix":
					return DataType.Suffix;
				case "Template":
					return DataType.Template;
				case "Time":
					return DataType.Time;
				case "Time Zone":
					return DataType.TimeZone;
				case "Title":
					return DataType.Title;
				case "Top Level Domain":
					return DataType.TopLevelDomain;
				case "URL":
					return DataType.URL;
				case "User Agent":
					return DataType.UserAgent;
				case "Username":
					return DataType.UserName;
				case "Words":
					return DataType.Words;
			}
		}
	}
}
