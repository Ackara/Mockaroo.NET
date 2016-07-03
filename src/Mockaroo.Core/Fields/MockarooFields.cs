using System;
using System.Collections.Generic;

namespace Gigobyte.Mockaroo
{
	public enum DataType
	{
		AppBundleID,
		AppName,
		AppVersion,
		Avatar,
		Base64ImageURL,
		BitcoinAddress,
		Blank,
		Boolean,
		City,
		Color,
		CompanyName,
		Country,
		CountryCode,
		CreditCardNumber,
		CreditCardType,
		Currency,
		CurrencyCode,
		CustomList,
		DatasetColumn,
		Date,
		DomainName,
		DrugCompany,
		DrugNameBrand,
		DrugNameGeneric,
		DummyImageURL,
		EmailAddress,
		Encrypt,
		FamilyNameChinese,
		FDANDCCode,
		FileName,
		FirstName,
		FirstNameEuropean,
		FirstNameFemale,
		FirstNameMale,
		Formula,
		Frequency,
		FullName,
		Gender,
		GenderAbbreviated,
		GivenNameChinese,
		GUID,
		HexColor,
		IBAN,
		ICD9DiagnosisCode,
		ICD9DxDescLong,
		ICD9DxDescShort,
		ICD9ProcDescLong,
		ICD9ProcDescShort,
		ICD9ProcedureCode,
		IPAddressV4,
		IPAddressV4CIDR,
		IPAddressV6,
		IPAddressV6CIDR,
		ISBN,
		JobTitle,
		JSONArray,
		Language,
		LastName,
		Latitude,
		LinkedInSkill,
		Longitude,
		MACAddress,
		MD5,
		MIMEType,
		Money,
		MongoDBObjectID,
		NaughtyString,
		NormalDistribution,
		Number,
		Paragraphs,
		Password,
		Phone,
		PoissonDistribution,
		PostalCode,
		Race,
		RegularExpression,
		RowNumber,
		Scenario,
		Sentences,
		Sequence,
		SHA1,
		SHA256,
		ShirtSize,
		ShortHexColor,
		SQLExpression,
		SSN,
		State,
		StateAbbreviated,
		StreetAddress,
		StreetName,
		StreetNumber,
		StreetSuffix,
		Suffix,
		Template,
		Time,
		TimeZone,
		Title,
		TopLevelDomain,
		URL,
		UserAgent,
		UserName,
		Words,
	}

	public static partial class Extensions
	{
		public static string ToMockarooTypeName(this DataType dataType)
		{
			switch(dataType)
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
				
				case DataType.BitcoinAddress:
				return "Bitcoin Address";
				
				case DataType.Blank:
				return "Blank";
				
				case DataType.Boolean:
				return "Boolean";
				
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
				
				case DataType.EmailAddress:
				return "Email Address";
				
				case DataType.Encrypt:
				return "Encrypt";
				
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
				
				case DataType.GivenNameChinese:
				return "Given Name (Chinese)";
				
				case DataType.GUID:
				return "GUID";
				
				case DataType.HexColor:
				return "Hex Color";
				
				case DataType.IBAN:
				return "IBAN";
				
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
				
				case DataType.SQLExpression:
				return "SQL Expression";
				
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
	}
}

namespace Gigobyte.Mockaroo.Fields
{
	public partial class FieldFactory
	{
		public FieldFactory()
		{
			_fieldTypes.Add(DataType.AppBundleID, Type.GetType("Gigobyte.Mockaroo.Fields.AppBundleIDField"));
			_fieldTypes.Add(DataType.AppName, Type.GetType("Gigobyte.Mockaroo.Fields.AppNameField"));
			_fieldTypes.Add(DataType.AppVersion, Type.GetType("Gigobyte.Mockaroo.Fields.AppVersionField"));
			_fieldTypes.Add(DataType.Avatar, Type.GetType("Gigobyte.Mockaroo.Fields.AvatarField"));
			_fieldTypes.Add(DataType.Base64ImageURL, Type.GetType("Gigobyte.Mockaroo.Fields.Base64ImageURLField"));
			_fieldTypes.Add(DataType.BitcoinAddress, Type.GetType("Gigobyte.Mockaroo.Fields.BitcoinAddressField"));
			_fieldTypes.Add(DataType.Blank, Type.GetType("Gigobyte.Mockaroo.Fields.BlankField"));
			_fieldTypes.Add(DataType.Boolean, Type.GetType("Gigobyte.Mockaroo.Fields.BooleanField"));
			_fieldTypes.Add(DataType.City, Type.GetType("Gigobyte.Mockaroo.Fields.CityField"));
			_fieldTypes.Add(DataType.Color, Type.GetType("Gigobyte.Mockaroo.Fields.ColorField"));
			_fieldTypes.Add(DataType.CompanyName, Type.GetType("Gigobyte.Mockaroo.Fields.CompanyNameField"));
			_fieldTypes.Add(DataType.Country, Type.GetType("Gigobyte.Mockaroo.Fields.CountryField"));
			_fieldTypes.Add(DataType.CountryCode, Type.GetType("Gigobyte.Mockaroo.Fields.CountryCodeField"));
			_fieldTypes.Add(DataType.CreditCardNumber, Type.GetType("Gigobyte.Mockaroo.Fields.CreditCardNumberField"));
			_fieldTypes.Add(DataType.CreditCardType, Type.GetType("Gigobyte.Mockaroo.Fields.CreditCardTypeField"));
			_fieldTypes.Add(DataType.Currency, Type.GetType("Gigobyte.Mockaroo.Fields.CurrencyField"));
			_fieldTypes.Add(DataType.CurrencyCode, Type.GetType("Gigobyte.Mockaroo.Fields.CurrencyCodeField"));
			_fieldTypes.Add(DataType.CustomList, Type.GetType("Gigobyte.Mockaroo.Fields.CustomListField"));
			_fieldTypes.Add(DataType.DatasetColumn, Type.GetType("Gigobyte.Mockaroo.Fields.DatasetColumnField"));
			_fieldTypes.Add(DataType.Date, Type.GetType("Gigobyte.Mockaroo.Fields.DateField"));
			_fieldTypes.Add(DataType.DomainName, Type.GetType("Gigobyte.Mockaroo.Fields.DomainNameField"));
			_fieldTypes.Add(DataType.DrugCompany, Type.GetType("Gigobyte.Mockaroo.Fields.DrugCompanyField"));
			_fieldTypes.Add(DataType.DrugNameBrand, Type.GetType("Gigobyte.Mockaroo.Fields.DrugNameBrandField"));
			_fieldTypes.Add(DataType.DrugNameGeneric, Type.GetType("Gigobyte.Mockaroo.Fields.DrugNameGenericField"));
			_fieldTypes.Add(DataType.DummyImageURL, Type.GetType("Gigobyte.Mockaroo.Fields.DummyImageURLField"));
			_fieldTypes.Add(DataType.EmailAddress, Type.GetType("Gigobyte.Mockaroo.Fields.EmailAddressField"));
			_fieldTypes.Add(DataType.Encrypt, Type.GetType("Gigobyte.Mockaroo.Fields.EncryptField"));
			_fieldTypes.Add(DataType.FamilyNameChinese, Type.GetType("Gigobyte.Mockaroo.Fields.FamilyNameChineseField"));
			_fieldTypes.Add(DataType.FDANDCCode, Type.GetType("Gigobyte.Mockaroo.Fields.FDANDCCodeField"));
			_fieldTypes.Add(DataType.FileName, Type.GetType("Gigobyte.Mockaroo.Fields.FileNameField"));
			_fieldTypes.Add(DataType.FirstName, Type.GetType("Gigobyte.Mockaroo.Fields.FirstNameField"));
			_fieldTypes.Add(DataType.FirstNameEuropean, Type.GetType("Gigobyte.Mockaroo.Fields.FirstNameEuropeanField"));
			_fieldTypes.Add(DataType.FirstNameFemale, Type.GetType("Gigobyte.Mockaroo.Fields.FirstNameFemaleField"));
			_fieldTypes.Add(DataType.FirstNameMale, Type.GetType("Gigobyte.Mockaroo.Fields.FirstNameMaleField"));
			_fieldTypes.Add(DataType.Formula, Type.GetType("Gigobyte.Mockaroo.Fields.FormulaField"));
			_fieldTypes.Add(DataType.Frequency, Type.GetType("Gigobyte.Mockaroo.Fields.FrequencyField"));
			_fieldTypes.Add(DataType.FullName, Type.GetType("Gigobyte.Mockaroo.Fields.FullNameField"));
			_fieldTypes.Add(DataType.Gender, Type.GetType("Gigobyte.Mockaroo.Fields.GenderField"));
			_fieldTypes.Add(DataType.GenderAbbreviated, Type.GetType("Gigobyte.Mockaroo.Fields.GenderAbbreviatedField"));
			_fieldTypes.Add(DataType.GivenNameChinese, Type.GetType("Gigobyte.Mockaroo.Fields.GivenNameChineseField"));
			_fieldTypes.Add(DataType.GUID, Type.GetType("Gigobyte.Mockaroo.Fields.GUIDField"));
			_fieldTypes.Add(DataType.HexColor, Type.GetType("Gigobyte.Mockaroo.Fields.HexColorField"));
			_fieldTypes.Add(DataType.IBAN, Type.GetType("Gigobyte.Mockaroo.Fields.IBANField"));
			_fieldTypes.Add(DataType.ICD9DiagnosisCode, Type.GetType("Gigobyte.Mockaroo.Fields.ICD9DiagnosisCodeField"));
			_fieldTypes.Add(DataType.ICD9DxDescLong, Type.GetType("Gigobyte.Mockaroo.Fields.ICD9DxDescLongField"));
			_fieldTypes.Add(DataType.ICD9DxDescShort, Type.GetType("Gigobyte.Mockaroo.Fields.ICD9DxDescShortField"));
			_fieldTypes.Add(DataType.ICD9ProcDescLong, Type.GetType("Gigobyte.Mockaroo.Fields.ICD9ProcDescLongField"));
			_fieldTypes.Add(DataType.ICD9ProcDescShort, Type.GetType("Gigobyte.Mockaroo.Fields.ICD9ProcDescShortField"));
			_fieldTypes.Add(DataType.ICD9ProcedureCode, Type.GetType("Gigobyte.Mockaroo.Fields.ICD9ProcedureCodeField"));
			_fieldTypes.Add(DataType.IPAddressV4, Type.GetType("Gigobyte.Mockaroo.Fields.IPAddressV4Field"));
			_fieldTypes.Add(DataType.IPAddressV4CIDR, Type.GetType("Gigobyte.Mockaroo.Fields.IPAddressV4CIDRField"));
			_fieldTypes.Add(DataType.IPAddressV6, Type.GetType("Gigobyte.Mockaroo.Fields.IPAddressV6Field"));
			_fieldTypes.Add(DataType.IPAddressV6CIDR, Type.GetType("Gigobyte.Mockaroo.Fields.IPAddressV6CIDRField"));
			_fieldTypes.Add(DataType.ISBN, Type.GetType("Gigobyte.Mockaroo.Fields.ISBNField"));
			_fieldTypes.Add(DataType.JobTitle, Type.GetType("Gigobyte.Mockaroo.Fields.JobTitleField"));
			_fieldTypes.Add(DataType.JSONArray, Type.GetType("Gigobyte.Mockaroo.Fields.JSONArrayField"));
			_fieldTypes.Add(DataType.Language, Type.GetType("Gigobyte.Mockaroo.Fields.LanguageField"));
			_fieldTypes.Add(DataType.LastName, Type.GetType("Gigobyte.Mockaroo.Fields.LastNameField"));
			_fieldTypes.Add(DataType.Latitude, Type.GetType("Gigobyte.Mockaroo.Fields.LatitudeField"));
			_fieldTypes.Add(DataType.LinkedInSkill, Type.GetType("Gigobyte.Mockaroo.Fields.LinkedInSkillField"));
			_fieldTypes.Add(DataType.Longitude, Type.GetType("Gigobyte.Mockaroo.Fields.LongitudeField"));
			_fieldTypes.Add(DataType.MACAddress, Type.GetType("Gigobyte.Mockaroo.Fields.MACAddressField"));
			_fieldTypes.Add(DataType.MD5, Type.GetType("Gigobyte.Mockaroo.Fields.MD5Field"));
			_fieldTypes.Add(DataType.MIMEType, Type.GetType("Gigobyte.Mockaroo.Fields.MIMETypeField"));
			_fieldTypes.Add(DataType.Money, Type.GetType("Gigobyte.Mockaroo.Fields.MoneyField"));
			_fieldTypes.Add(DataType.MongoDBObjectID, Type.GetType("Gigobyte.Mockaroo.Fields.MongoDBObjectIDField"));
			_fieldTypes.Add(DataType.NaughtyString, Type.GetType("Gigobyte.Mockaroo.Fields.NaughtyStringField"));
			_fieldTypes.Add(DataType.NormalDistribution, Type.GetType("Gigobyte.Mockaroo.Fields.NormalDistributionField"));
			_fieldTypes.Add(DataType.Number, Type.GetType("Gigobyte.Mockaroo.Fields.NumberField"));
			_fieldTypes.Add(DataType.Paragraphs, Type.GetType("Gigobyte.Mockaroo.Fields.ParagraphsField"));
			_fieldTypes.Add(DataType.Password, Type.GetType("Gigobyte.Mockaroo.Fields.PasswordField"));
			_fieldTypes.Add(DataType.Phone, Type.GetType("Gigobyte.Mockaroo.Fields.PhoneField"));
			_fieldTypes.Add(DataType.PoissonDistribution, Type.GetType("Gigobyte.Mockaroo.Fields.PoissonDistributionField"));
			_fieldTypes.Add(DataType.PostalCode, Type.GetType("Gigobyte.Mockaroo.Fields.PostalCodeField"));
			_fieldTypes.Add(DataType.Race, Type.GetType("Gigobyte.Mockaroo.Fields.RaceField"));
			_fieldTypes.Add(DataType.RegularExpression, Type.GetType("Gigobyte.Mockaroo.Fields.RegularExpressionField"));
			_fieldTypes.Add(DataType.RowNumber, Type.GetType("Gigobyte.Mockaroo.Fields.RowNumberField"));
			_fieldTypes.Add(DataType.Scenario, Type.GetType("Gigobyte.Mockaroo.Fields.ScenarioField"));
			_fieldTypes.Add(DataType.Sentences, Type.GetType("Gigobyte.Mockaroo.Fields.SentencesField"));
			_fieldTypes.Add(DataType.Sequence, Type.GetType("Gigobyte.Mockaroo.Fields.SequenceField"));
			_fieldTypes.Add(DataType.SHA1, Type.GetType("Gigobyte.Mockaroo.Fields.SHA1Field"));
			_fieldTypes.Add(DataType.SHA256, Type.GetType("Gigobyte.Mockaroo.Fields.SHA256Field"));
			_fieldTypes.Add(DataType.ShirtSize, Type.GetType("Gigobyte.Mockaroo.Fields.ShirtSizeField"));
			_fieldTypes.Add(DataType.ShortHexColor, Type.GetType("Gigobyte.Mockaroo.Fields.ShortHexColorField"));
			_fieldTypes.Add(DataType.SQLExpression, Type.GetType("Gigobyte.Mockaroo.Fields.SQLExpressionField"));
			_fieldTypes.Add(DataType.SSN, Type.GetType("Gigobyte.Mockaroo.Fields.SSNField"));
			_fieldTypes.Add(DataType.State, Type.GetType("Gigobyte.Mockaroo.Fields.StateField"));
			_fieldTypes.Add(DataType.StateAbbreviated, Type.GetType("Gigobyte.Mockaroo.Fields.StateAbbreviatedField"));
			_fieldTypes.Add(DataType.StreetAddress, Type.GetType("Gigobyte.Mockaroo.Fields.StreetAddressField"));
			_fieldTypes.Add(DataType.StreetName, Type.GetType("Gigobyte.Mockaroo.Fields.StreetNameField"));
			_fieldTypes.Add(DataType.StreetNumber, Type.GetType("Gigobyte.Mockaroo.Fields.StreetNumberField"));
			_fieldTypes.Add(DataType.StreetSuffix, Type.GetType("Gigobyte.Mockaroo.Fields.StreetSuffixField"));
			_fieldTypes.Add(DataType.Suffix, Type.GetType("Gigobyte.Mockaroo.Fields.SuffixField"));
			_fieldTypes.Add(DataType.Template, Type.GetType("Gigobyte.Mockaroo.Fields.TemplateField"));
			_fieldTypes.Add(DataType.Time, Type.GetType("Gigobyte.Mockaroo.Fields.TimeField"));
			_fieldTypes.Add(DataType.TimeZone, Type.GetType("Gigobyte.Mockaroo.Fields.TimeZoneField"));
			_fieldTypes.Add(DataType.Title, Type.GetType("Gigobyte.Mockaroo.Fields.TitleField"));
			_fieldTypes.Add(DataType.TopLevelDomain, Type.GetType("Gigobyte.Mockaroo.Fields.TopLevelDomainField"));
			_fieldTypes.Add(DataType.URL, Type.GetType("Gigobyte.Mockaroo.Fields.URLField"));
			_fieldTypes.Add(DataType.UserAgent, Type.GetType("Gigobyte.Mockaroo.Fields.UserAgentField"));
			_fieldTypes.Add(DataType.UserName, Type.GetType("Gigobyte.Mockaroo.Fields.UserNameField"));
			_fieldTypes.Add(DataType.Words, Type.GetType("Gigobyte.Mockaroo.Fields.WordsField"));
		}

		public IField Create(DataType dataType)
		{
			return (IField)Activator.CreateInstance(_fieldTypes[dataType]);
		}

		#region Private Members

		private IDictionary<DataType, Type> _fieldTypes = new Dictionary<DataType, Type>();

		#endregion Private Members
	}

	/// <summary>
	/// Represents a App Bundle ID field.
	/// </summary>
	public partial class AppBundleIDField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.AppBundleID; } }
	}
	/// <summary>
	/// Represents a App Name field.
	/// </summary>
	public partial class AppNameField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.AppName; } }
	}
	/// <summary>
	/// Represents a App Version field.
	/// </summary>
	public partial class AppVersionField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.AppVersion; } }
	}
	/// <summary>
	/// Represents a Avatar field.
	/// </summary>
	public partial class AvatarField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Avatar; } }
	}
	/// <summary>
	/// Represents a Base64 Image URL field.
	/// </summary>
	public partial class Base64ImageURLField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Base64ImageURL; } }
	}
	/// <summary>
	/// Represents a Bitcoin Address field.
	/// </summary>
	public partial class BitcoinAddressField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.BitcoinAddress; } }
	}
	/// <summary>
	/// Represents a Blank field.
	/// </summary>
	public partial class BlankField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Blank; } }
	}
	/// <summary>
	/// Represents a Boolean field.
	/// </summary>
	public partial class BooleanField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Boolean; } }
	}
	/// <summary>
	/// Represents a City field.
	/// </summary>
	public partial class CityField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.City; } }
	}
	/// <summary>
	/// Represents a Color field.
	/// </summary>
	public partial class ColorField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Color; } }
	}
	/// <summary>
	/// Represents a Company Name field.
	/// </summary>
	public partial class CompanyNameField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.CompanyName; } }
	}
	/// <summary>
	/// Represents a Country field.
	/// </summary>
	public partial class CountryField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Country; } }
	}
	/// <summary>
	/// Represents a Country Code field.
	/// </summary>
	public partial class CountryCodeField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.CountryCode; } }
	}
	/// <summary>
	/// Represents a Credit Card # field.
	/// </summary>
	public partial class CreditCardNumberField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.CreditCardNumber; } }
	}
	/// <summary>
	/// Represents a Credit Card Type field.
	/// </summary>
	public partial class CreditCardTypeField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.CreditCardType; } }
	}
	/// <summary>
	/// Represents a Currency field.
	/// </summary>
	public partial class CurrencyField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Currency; } }
	}
	/// <summary>
	/// Represents a Currency Code field.
	/// </summary>
	public partial class CurrencyCodeField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.CurrencyCode; } }
	}
	/// <summary>
	/// Represents a Custom List field.
	/// </summary>
	public partial class CustomListField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.CustomList; } }
	}
	/// <summary>
	/// Represents a Dataset Column field.
	/// </summary>
	public partial class DatasetColumnField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.DatasetColumn; } }
	}
	/// <summary>
	/// Represents a Date field.
	/// </summary>
	public partial class DateField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Date; } }
	}
	/// <summary>
	/// Represents a Domain Name field.
	/// </summary>
	public partial class DomainNameField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.DomainName; } }
	}
	/// <summary>
	/// Represents a Drug Company field.
	/// </summary>
	public partial class DrugCompanyField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.DrugCompany; } }
	}
	/// <summary>
	/// Represents a Drug Name (Brand) field.
	/// </summary>
	public partial class DrugNameBrandField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.DrugNameBrand; } }
	}
	/// <summary>
	/// Represents a Drug Name (Generic) field.
	/// </summary>
	public partial class DrugNameGenericField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.DrugNameGeneric; } }
	}
	/// <summary>
	/// Represents a Dummy Image URL field.
	/// </summary>
	public partial class DummyImageURLField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.DummyImageURL; } }
	}
	/// <summary>
	/// Represents a Email Address field.
	/// </summary>
	public partial class EmailAddressField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.EmailAddress; } }
	}
	/// <summary>
	/// Represents a Encrypt field.
	/// </summary>
	public partial class EncryptField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Encrypt; } }
	}
	/// <summary>
	/// Represents a Family Name (Chinese) field.
	/// </summary>
	public partial class FamilyNameChineseField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.FamilyNameChinese; } }
	}
	/// <summary>
	/// Represents a FDA NDC Code field.
	/// </summary>
	public partial class FDANDCCodeField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.FDANDCCode; } }
	}
	/// <summary>
	/// Represents a File Name field.
	/// </summary>
	public partial class FileNameField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.FileName; } }
	}
	/// <summary>
	/// Represents a First Name field.
	/// </summary>
	public partial class FirstNameField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.FirstName; } }
	}
	/// <summary>
	/// Represents a First Name (European) field.
	/// </summary>
	public partial class FirstNameEuropeanField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.FirstNameEuropean; } }
	}
	/// <summary>
	/// Represents a First Name (Female) field.
	/// </summary>
	public partial class FirstNameFemaleField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.FirstNameFemale; } }
	}
	/// <summary>
	/// Represents a First Name (Male) field.
	/// </summary>
	public partial class FirstNameMaleField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.FirstNameMale; } }
	}
	/// <summary>
	/// Represents a Formula field.
	/// </summary>
	public partial class FormulaField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Formula; } }
	}
	/// <summary>
	/// Represents a Frequency field.
	/// </summary>
	public partial class FrequencyField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Frequency; } }
	}
	/// <summary>
	/// Represents a Full Name field.
	/// </summary>
	public partial class FullNameField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.FullName; } }
	}
	/// <summary>
	/// Represents a Gender field.
	/// </summary>
	public partial class GenderField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Gender; } }
	}
	/// <summary>
	/// Represents a Gender (abbrev) field.
	/// </summary>
	public partial class GenderAbbreviatedField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.GenderAbbreviated; } }
	}
	/// <summary>
	/// Represents a Given Name (Chinese) field.
	/// </summary>
	public partial class GivenNameChineseField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.GivenNameChinese; } }
	}
	/// <summary>
	/// Represents a GUID field.
	/// </summary>
	public partial class GUIDField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.GUID; } }
	}
	/// <summary>
	/// Represents a Hex Color field.
	/// </summary>
	public partial class HexColorField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.HexColor; } }
	}
	/// <summary>
	/// Represents a IBAN field.
	/// </summary>
	public partial class IBANField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.IBAN; } }
	}
	/// <summary>
	/// Represents a ICD9 Diagnosis Code field.
	/// </summary>
	public partial class ICD9DiagnosisCodeField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.ICD9DiagnosisCode; } }
	}
	/// <summary>
	/// Represents a ICD9 Dx Desc (Long) field.
	/// </summary>
	public partial class ICD9DxDescLongField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.ICD9DxDescLong; } }
	}
	/// <summary>
	/// Represents a ICD9 Dx Desc (Short) field.
	/// </summary>
	public partial class ICD9DxDescShortField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.ICD9DxDescShort; } }
	}
	/// <summary>
	/// Represents a ICD9 Proc Desc (Long) field.
	/// </summary>
	public partial class ICD9ProcDescLongField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.ICD9ProcDescLong; } }
	}
	/// <summary>
	/// Represents a ICD9 Proc Desc (Short) field.
	/// </summary>
	public partial class ICD9ProcDescShortField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.ICD9ProcDescShort; } }
	}
	/// <summary>
	/// Represents a ICD9 Procedure Code field.
	/// </summary>
	public partial class ICD9ProcedureCodeField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.ICD9ProcedureCode; } }
	}
	/// <summary>
	/// Represents a IP Address v4 field.
	/// </summary>
	public partial class IPAddressV4Field : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.IPAddressV4; } }
	}
	/// <summary>
	/// Represents a IP Address v4 CIDR field.
	/// </summary>
	public partial class IPAddressV4CIDRField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.IPAddressV4CIDR; } }
	}
	/// <summary>
	/// Represents a IP Address v6 field.
	/// </summary>
	public partial class IPAddressV6Field : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.IPAddressV6; } }
	}
	/// <summary>
	/// Represents a IP Address v6 CIDR field.
	/// </summary>
	public partial class IPAddressV6CIDRField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.IPAddressV6CIDR; } }
	}
	/// <summary>
	/// Represents a ISBN field.
	/// </summary>
	public partial class ISBNField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.ISBN; } }
	}
	/// <summary>
	/// Represents a Job Title field.
	/// </summary>
	public partial class JobTitleField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.JobTitle; } }
	}
	/// <summary>
	/// Represents a JSON Array field.
	/// </summary>
	public partial class JSONArrayField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.JSONArray; } }
	}
	/// <summary>
	/// Represents a Language field.
	/// </summary>
	public partial class LanguageField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Language; } }
	}
	/// <summary>
	/// Represents a Last Name field.
	/// </summary>
	public partial class LastNameField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.LastName; } }
	}
	/// <summary>
	/// Represents a Latitude field.
	/// </summary>
	public partial class LatitudeField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Latitude; } }
	}
	/// <summary>
	/// Represents a LinkedIn Skill field.
	/// </summary>
	public partial class LinkedInSkillField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.LinkedInSkill; } }
	}
	/// <summary>
	/// Represents a Longitude field.
	/// </summary>
	public partial class LongitudeField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Longitude; } }
	}
	/// <summary>
	/// Represents a MAC Address field.
	/// </summary>
	public partial class MACAddressField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.MACAddress; } }
	}
	/// <summary>
	/// Represents a MD5 field.
	/// </summary>
	public partial class MD5Field : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.MD5; } }
	}
	/// <summary>
	/// Represents a MIME Type field.
	/// </summary>
	public partial class MIMETypeField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.MIMEType; } }
	}
	/// <summary>
	/// Represents a Money field.
	/// </summary>
	public partial class MoneyField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Money; } }
	}
	/// <summary>
	/// Represents a MongoDB ObjectID field.
	/// </summary>
	public partial class MongoDBObjectIDField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.MongoDBObjectID; } }
	}
	/// <summary>
	/// Represents a Naughty String field.
	/// </summary>
	public partial class NaughtyStringField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.NaughtyString; } }
	}
	/// <summary>
	/// Represents a Normal Distribution field.
	/// </summary>
	public partial class NormalDistributionField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.NormalDistribution; } }
	}
	/// <summary>
	/// Represents a Number field.
	/// </summary>
	public partial class NumberField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Number; } }
	}
	/// <summary>
	/// Represents a Paragraphs field.
	/// </summary>
	public partial class ParagraphsField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Paragraphs; } }
	}
	/// <summary>
	/// Represents a Password field.
	/// </summary>
	public partial class PasswordField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Password; } }
	}
	/// <summary>
	/// Represents a Phone field.
	/// </summary>
	public partial class PhoneField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Phone; } }
	}
	/// <summary>
	/// Represents a Poisson Distribution field.
	/// </summary>
	public partial class PoissonDistributionField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.PoissonDistribution; } }
	}
	/// <summary>
	/// Represents a Postal Code field.
	/// </summary>
	public partial class PostalCodeField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.PostalCode; } }
	}
	/// <summary>
	/// Represents a Race field.
	/// </summary>
	public partial class RaceField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Race; } }
	}
	/// <summary>
	/// Represents a Regular Expression field.
	/// </summary>
	public partial class RegularExpressionField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.RegularExpression; } }
	}
	/// <summary>
	/// Represents a Row Number field.
	/// </summary>
	public partial class RowNumberField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.RowNumber; } }
	}
	/// <summary>
	/// Represents a Scenario field.
	/// </summary>
	public partial class ScenarioField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Scenario; } }
	}
	/// <summary>
	/// Represents a Sentences field.
	/// </summary>
	public partial class SentencesField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Sentences; } }
	}
	/// <summary>
	/// Represents a Sequence field.
	/// </summary>
	public partial class SequenceField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Sequence; } }
	}
	/// <summary>
	/// Represents a SHA1 field.
	/// </summary>
	public partial class SHA1Field : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.SHA1; } }
	}
	/// <summary>
	/// Represents a SHA256 field.
	/// </summary>
	public partial class SHA256Field : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.SHA256; } }
	}
	/// <summary>
	/// Represents a Shirt Size field.
	/// </summary>
	public partial class ShirtSizeField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.ShirtSize; } }
	}
	/// <summary>
	/// Represents a Short Hex Color field.
	/// </summary>
	public partial class ShortHexColorField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.ShortHexColor; } }
	}
	/// <summary>
	/// Represents a SQL Expression field.
	/// </summary>
	public partial class SQLExpressionField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.SQLExpression; } }
	}
	/// <summary>
	/// Represents a SSN field.
	/// </summary>
	public partial class SSNField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.SSN; } }
	}
	/// <summary>
	/// Represents a State field.
	/// </summary>
	public partial class StateField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.State; } }
	}
	/// <summary>
	/// Represents a State (abbrev) field.
	/// </summary>
	public partial class StateAbbreviatedField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.StateAbbreviated; } }
	}
	/// <summary>
	/// Represents a Street Address field.
	/// </summary>
	public partial class StreetAddressField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.StreetAddress; } }
	}
	/// <summary>
	/// Represents a Street Name field.
	/// </summary>
	public partial class StreetNameField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.StreetName; } }
	}
	/// <summary>
	/// Represents a Street Number field.
	/// </summary>
	public partial class StreetNumberField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.StreetNumber; } }
	}
	/// <summary>
	/// Represents a Street Suffix field.
	/// </summary>
	public partial class StreetSuffixField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.StreetSuffix; } }
	}
	/// <summary>
	/// Represents a Suffix field.
	/// </summary>
	public partial class SuffixField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Suffix; } }
	}
	/// <summary>
	/// Represents a Template field.
	/// </summary>
	public partial class TemplateField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Template; } }
	}
	/// <summary>
	/// Represents a Time field.
	/// </summary>
	public partial class TimeField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Time; } }
	}
	/// <summary>
	/// Represents a Time Zone field.
	/// </summary>
	public partial class TimeZoneField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.TimeZone; } }
	}
	/// <summary>
	/// Represents a Title field.
	/// </summary>
	public partial class TitleField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Title; } }
	}
	/// <summary>
	/// Represents a Top Level Domain field.
	/// </summary>
	public partial class TopLevelDomainField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.TopLevelDomain; } }
	}
	/// <summary>
	/// Represents a URL field.
	/// </summary>
	public partial class URLField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.URL; } }
	}
	/// <summary>
	/// Represents a User Agent field.
	/// </summary>
	public partial class UserAgentField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.UserAgent; } }
	}
	/// <summary>
	/// Represents a Username field.
	/// </summary>
	public partial class UserNameField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.UserName; } }
	}
	/// <summary>
	/// Represents a Words field.
	/// </summary>
	public partial class WordsField : FieldBase
	{
		/// <summary>
		/// Get the Mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Words; } }
	}

}
