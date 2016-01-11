using System;
using System.Collections.Generic;

namespace Gigobyte.Mockaroo
{
	public enum DataType
	{
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
		IPAddressV6,
		ISBN,
		JobTitle,
		JSONArray,
		Language,
		LastName,
		Latitude,
		LinkedInSkill,
		Longitude,
		MACAddress,
		MIMEType,
		Money,
		MongoDBObjectID,
		NaughtyString,
		NormalDistribution,
		Number,
		Paragraphs,
		Password,
		Phone,
		PostalCode,
		Race,
		RegularExpression,
		RowNumber,
		Scenario,
		Sentences,
		Sequence,
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
				
				case DataType.IPAddressV6:
				return "IP Address v6";
				
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
			_fieldTypes.Add(DataType.IPAddressV6, Type.GetType("Gigobyte.Mockaroo.Fields.IPAddressV6Field"));
			_fieldTypes.Add(DataType.ISBN, Type.GetType("Gigobyte.Mockaroo.Fields.ISBNField"));
			_fieldTypes.Add(DataType.JobTitle, Type.GetType("Gigobyte.Mockaroo.Fields.JobTitleField"));
			_fieldTypes.Add(DataType.JSONArray, Type.GetType("Gigobyte.Mockaroo.Fields.JSONArrayField"));
			_fieldTypes.Add(DataType.Language, Type.GetType("Gigobyte.Mockaroo.Fields.LanguageField"));
			_fieldTypes.Add(DataType.LastName, Type.GetType("Gigobyte.Mockaroo.Fields.LastNameField"));
			_fieldTypes.Add(DataType.Latitude, Type.GetType("Gigobyte.Mockaroo.Fields.LatitudeField"));
			_fieldTypes.Add(DataType.LinkedInSkill, Type.GetType("Gigobyte.Mockaroo.Fields.LinkedInSkillField"));
			_fieldTypes.Add(DataType.Longitude, Type.GetType("Gigobyte.Mockaroo.Fields.LongitudeField"));
			_fieldTypes.Add(DataType.MACAddress, Type.GetType("Gigobyte.Mockaroo.Fields.MACAddressField"));
			_fieldTypes.Add(DataType.MIMEType, Type.GetType("Gigobyte.Mockaroo.Fields.MIMETypeField"));
			_fieldTypes.Add(DataType.Money, Type.GetType("Gigobyte.Mockaroo.Fields.MoneyField"));
			_fieldTypes.Add(DataType.MongoDBObjectID, Type.GetType("Gigobyte.Mockaroo.Fields.MongoDBObjectIDField"));
			_fieldTypes.Add(DataType.NaughtyString, Type.GetType("Gigobyte.Mockaroo.Fields.NaughtyStringField"));
			_fieldTypes.Add(DataType.NormalDistribution, Type.GetType("Gigobyte.Mockaroo.Fields.NormalDistributionField"));
			_fieldTypes.Add(DataType.Number, Type.GetType("Gigobyte.Mockaroo.Fields.NumberField"));
			_fieldTypes.Add(DataType.Paragraphs, Type.GetType("Gigobyte.Mockaroo.Fields.ParagraphsField"));
			_fieldTypes.Add(DataType.Password, Type.GetType("Gigobyte.Mockaroo.Fields.PasswordField"));
			_fieldTypes.Add(DataType.Phone, Type.GetType("Gigobyte.Mockaroo.Fields.PhoneField"));
			_fieldTypes.Add(DataType.PostalCode, Type.GetType("Gigobyte.Mockaroo.Fields.PostalCodeField"));
			_fieldTypes.Add(DataType.Race, Type.GetType("Gigobyte.Mockaroo.Fields.RaceField"));
			_fieldTypes.Add(DataType.RegularExpression, Type.GetType("Gigobyte.Mockaroo.Fields.RegularExpressionField"));
			_fieldTypes.Add(DataType.RowNumber, Type.GetType("Gigobyte.Mockaroo.Fields.RowNumberField"));
			_fieldTypes.Add(DataType.Scenario, Type.GetType("Gigobyte.Mockaroo.Fields.ScenarioField"));
			_fieldTypes.Add(DataType.Sentences, Type.GetType("Gigobyte.Mockaroo.Fields.SentencesField"));
			_fieldTypes.Add(DataType.Sequence, Type.GetType("Gigobyte.Mockaroo.Fields.SequenceField"));
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

	public partial class BitcoinAddressField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.BitcoinAddress; } }
	}
	public partial class BlankField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Blank; } }
	}
	public partial class BooleanField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Boolean; } }
	}
	public partial class CityField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.City; } }
	}
	public partial class ColorField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Color; } }
	}
	public partial class CompanyNameField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.CompanyName; } }
	}
	public partial class CountryField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Country; } }
	}
	public partial class CountryCodeField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.CountryCode; } }
	}
	public partial class CreditCardNumberField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.CreditCardNumber; } }
	}
	public partial class CreditCardTypeField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.CreditCardType; } }
	}
	public partial class CurrencyField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Currency; } }
	}
	public partial class CurrencyCodeField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.CurrencyCode; } }
	}
	public partial class CustomListField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.CustomList; } }
	}
	public partial class DatasetColumnField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.DatasetColumn; } }
	}
	public partial class DateField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Date; } }
	}
	public partial class DomainNameField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.DomainName; } }
	}
	public partial class DrugCompanyField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.DrugCompany; } }
	}
	public partial class DrugNameBrandField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.DrugNameBrand; } }
	}
	public partial class DrugNameGenericField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.DrugNameGeneric; } }
	}
	public partial class EmailAddressField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.EmailAddress; } }
	}
	public partial class EncryptField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Encrypt; } }
	}
	public partial class FamilyNameChineseField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.FamilyNameChinese; } }
	}
	public partial class FDANDCCodeField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.FDANDCCode; } }
	}
	public partial class FileNameField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.FileName; } }
	}
	public partial class FirstNameField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.FirstName; } }
	}
	public partial class FirstNameEuropeanField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.FirstNameEuropean; } }
	}
	public partial class FirstNameFemaleField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.FirstNameFemale; } }
	}
	public partial class FirstNameMaleField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.FirstNameMale; } }
	}
	public partial class FormulaField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Formula; } }
	}
	public partial class FrequencyField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Frequency; } }
	}
	public partial class FullNameField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.FullName; } }
	}
	public partial class GenderField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Gender; } }
	}
	public partial class GenderAbbreviatedField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.GenderAbbreviated; } }
	}
	public partial class GivenNameChineseField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.GivenNameChinese; } }
	}
	public partial class GUIDField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.GUID; } }
	}
	public partial class HexColorField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.HexColor; } }
	}
	public partial class IBANField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.IBAN; } }
	}
	public partial class ICD9DiagnosisCodeField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.ICD9DiagnosisCode; } }
	}
	public partial class ICD9DxDescLongField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.ICD9DxDescLong; } }
	}
	public partial class ICD9DxDescShortField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.ICD9DxDescShort; } }
	}
	public partial class ICD9ProcDescLongField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.ICD9ProcDescLong; } }
	}
	public partial class ICD9ProcDescShortField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.ICD9ProcDescShort; } }
	}
	public partial class ICD9ProcedureCodeField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.ICD9ProcedureCode; } }
	}
	public partial class IPAddressV4Field : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.IPAddressV4; } }
	}
	public partial class IPAddressV6Field : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.IPAddressV6; } }
	}
	public partial class ISBNField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.ISBN; } }
	}
	public partial class JobTitleField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.JobTitle; } }
	}
	public partial class JSONArrayField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.JSONArray; } }
	}
	public partial class LanguageField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Language; } }
	}
	public partial class LastNameField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.LastName; } }
	}
	public partial class LatitudeField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Latitude; } }
	}
	public partial class LinkedInSkillField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.LinkedInSkill; } }
	}
	public partial class LongitudeField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Longitude; } }
	}
	public partial class MACAddressField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.MACAddress; } }
	}
	public partial class MIMETypeField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.MIMEType; } }
	}
	public partial class MoneyField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Money; } }
	}
	public partial class MongoDBObjectIDField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.MongoDBObjectID; } }
	}
	public partial class NaughtyStringField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.NaughtyString; } }
	}
	public partial class NormalDistributionField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.NormalDistribution; } }
	}
	public partial class NumberField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Number; } }
	}
	public partial class ParagraphsField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Paragraphs; } }
	}
	public partial class PasswordField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Password; } }
	}
	public partial class PhoneField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Phone; } }
	}
	public partial class PostalCodeField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.PostalCode; } }
	}
	public partial class RaceField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Race; } }
	}
	public partial class RegularExpressionField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.RegularExpression; } }
	}
	public partial class RowNumberField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.RowNumber; } }
	}
	public partial class ScenarioField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Scenario; } }
	}
	public partial class SentencesField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Sentences; } }
	}
	public partial class SequenceField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Sequence; } }
	}
	public partial class ShirtSizeField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.ShirtSize; } }
	}
	public partial class ShortHexColorField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.ShortHexColor; } }
	}
	public partial class SQLExpressionField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.SQLExpression; } }
	}
	public partial class SSNField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.SSN; } }
	}
	public partial class StateField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.State; } }
	}
	public partial class StateAbbreviatedField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.StateAbbreviated; } }
	}
	public partial class StreetAddressField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.StreetAddress; } }
	}
	public partial class StreetNameField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.StreetName; } }
	}
	public partial class StreetNumberField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.StreetNumber; } }
	}
	public partial class StreetSuffixField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.StreetSuffix; } }
	}
	public partial class SuffixField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Suffix; } }
	}
	public partial class TemplateField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Template; } }
	}
	public partial class TimeField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Time; } }
	}
	public partial class TimeZoneField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.TimeZone; } }
	}
	public partial class TitleField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Title; } }
	}
	public partial class TopLevelDomainField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.TopLevelDomain; } }
	}
	public partial class URLField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.URL; } }
	}
	public partial class UserAgentField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.UserAgent; } }
	}
	public partial class UserNameField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.UserName; } }
	}
	public partial class WordsField : FieldBase
	{
		/// <summary>
		/// Get the mockaroo data type.
		/// </summary>
		public override DataType Type { get { return DataType.Words; } }
	}

}
