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
		CreditCard,
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
		Genderabbrev,
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
		Stateabbrev,
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
		Username,
		Words,
	}

	public static partial class Extensions
	{
		public static string GetName(this DataType dataType)
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
				
				case DataType.CreditCard:
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
				
				case DataType.Genderabbrev:
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
				
				case DataType.Stateabbrev:
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
				
				case DataType.Username:
				return "Username";
				
				case DataType.Words:
				return "Words";
			}
		}
	}
}
