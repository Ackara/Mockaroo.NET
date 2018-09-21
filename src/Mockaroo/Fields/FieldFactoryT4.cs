
namespace Acklann.Mockaroo.Fields
{
	public partial class FieldFactory
	{
		/// <summary>
		/// Creates a new <see cref="IField"/> instance.
		/// </summary>
		public static IField CreateInstance(DataType dataType)
		{
			switch(dataType)
			{
				default:
					throw new System.ArgumentException($"'{dataType}' is not a unknown data type.");

				case DataType.AnimalCommonName: 
					return new AnimalCommonNameField();
				case DataType.AnimalScientificName: 
					return new AnimalScientificNameField();
				case DataType.AppBundleID: 
					return new AppBundleIDField();
				case DataType.AppName: 
					return new AppNameField();
				case DataType.AppVersion: 
					return new AppVersionField();
				case DataType.Avatar: 
					return new AvatarField();
				case DataType.Base64ImageURL: 
					return new Base64ImageURLField();
				case DataType.BinomialDistribution: 
					return new BinomialDistributionField();
				case DataType.BitcoinAddress: 
					return new BitcoinAddressField();
				case DataType.Blank: 
					return new BlankField();
				case DataType.Boolean: 
					return new BooleanField();
				case DataType.Buzzword: 
					return new BuzzwordField();
				case DataType.CarMake: 
					return new CarMakeField();
				case DataType.CarModel: 
					return new CarModelField();
				case DataType.CarModelYear: 
					return new CarModelYearField();
				case DataType.CarVIN: 
					return new CarVINField();
				case DataType.CatchPhrase: 
					return new CatchPhraseField();
				case DataType.CharacterSequence: 
					return new CharacterSequenceField();
				case DataType.City: 
					return new CityField();
				case DataType.Color: 
					return new ColorField();
				case DataType.CompanyName: 
					return new CompanyNameField();
				case DataType.Country: 
					return new CountryField();
				case DataType.CountryCode: 
					return new CountryCodeField();
				case DataType.CreditCardNumber: 
					return new CreditCardNumberField();
				case DataType.CreditCardType: 
					return new CreditCardTypeField();
				case DataType.Currency: 
					return new CurrencyField();
				case DataType.CurrencyCode: 
					return new CurrencyCodeField();
				case DataType.CustomList: 
					return new CustomListField();
				case DataType.DatasetColumn: 
					return new DatasetColumnField();
				case DataType.Date: 
					return new DateField();
				case DataType.DepartmentCorporate: 
					return new DepartmentCorporateField();
				case DataType.DepartmentRetail: 
					return new DepartmentRetailField();
				case DataType.DigitSequence: 
					return new DigitSequenceField();
				case DataType.DomainName: 
					return new DomainNameField();
				case DataType.DrugCompany: 
					return new DrugCompanyField();
				case DataType.DrugNameBrand: 
					return new DrugNameBrandField();
				case DataType.DrugNameGeneric: 
					return new DrugNameGenericField();
				case DataType.DummyImageURL: 
					return new DummyImageURLField();
				case DataType.DUNSNumber: 
					return new DUNSNumberField();
				case DataType.EIN: 
					return new EINField();
				case DataType.EmailAddress: 
					return new EmailAddressField();
				case DataType.Encrypt: 
					return new EncryptField();
				case DataType.ExponentialDistribution: 
					return new ExponentialDistributionField();
				case DataType.FakeCompanyName: 
					return new FakeCompanyNameField();
				case DataType.FamilyNameChinese: 
					return new FamilyNameChineseField();
				case DataType.FDANDCCode: 
					return new FDANDCCodeField();
				case DataType.FileName: 
					return new FileNameField();
				case DataType.FirstName: 
					return new FirstNameField();
				case DataType.FirstNameEuropean: 
					return new FirstNameEuropeanField();
				case DataType.FirstNameFemale: 
					return new FirstNameFemaleField();
				case DataType.FirstNameMale: 
					return new FirstNameMaleField();
				case DataType.Formula: 
					return new FormulaField();
				case DataType.Frequency: 
					return new FrequencyField();
				case DataType.FullName: 
					return new FullNameField();
				case DataType.Gender: 
					return new GenderField();
				case DataType.GenderAbbreviated: 
					return new GenderAbbreviatedField();
				case DataType.GeometricDistribution: 
					return new GeometricDistributionField();
				case DataType.GivenNameChinese: 
					return new GivenNameChineseField();
				case DataType.GUID: 
					return new GUIDField();
				case DataType.HexColor: 
					return new HexColorField();
				case DataType.IBAN: 
					return new IBANField();
				case DataType.ICD10DiagnosisCode: 
					return new ICD10DiagnosisCodeField();
				case DataType.ICD10DxDescLong: 
					return new ICD10DxDescLongField();
				case DataType.ICD10DxDescShort: 
					return new ICD10DxDescShortField();
				case DataType.ICD10ProcDescLong: 
					return new ICD10ProcDescLongField();
				case DataType.ICD10ProcDescShort: 
					return new ICD10ProcDescShortField();
				case DataType.ICD10ProcedureCode: 
					return new ICD10ProcedureCodeField();
				case DataType.ICD9DiagnosisCode: 
					return new ICD9DiagnosisCodeField();
				case DataType.ICD9DxDescLong: 
					return new ICD9DxDescLongField();
				case DataType.ICD9DxDescShort: 
					return new ICD9DxDescShortField();
				case DataType.ICD9ProcDescLong: 
					return new ICD9ProcDescLongField();
				case DataType.ICD9ProcDescShort: 
					return new ICD9ProcDescShortField();
				case DataType.ICD9ProcedureCode: 
					return new ICD9ProcedureCodeField();
				case DataType.IPAddressV4: 
					return new IPAddressV4Field();
				case DataType.IPAddressV4CIDR: 
					return new IPAddressV4CIDRField();
				case DataType.IPAddressV6: 
					return new IPAddressV6Field();
				case DataType.IPAddressV6CIDR: 
					return new IPAddressV6CIDRField();
				case DataType.ISBN: 
					return new ISBNField();
				case DataType.JobTitle: 
					return new JobTitleField();
				case DataType.JSONArray: 
					return new JSONArrayField();
				case DataType.Language: 
					return new LanguageField();
				case DataType.LastName: 
					return new LastNameField();
				case DataType.Latitude: 
					return new LatitudeField();
				case DataType.LinkedInSkill: 
					return new LinkedInSkillField();
				case DataType.Longitude: 
					return new LongitudeField();
				case DataType.MACAddress: 
					return new MACAddressField();
				case DataType.MD5: 
					return new MD5Field();
				case DataType.MIMEType: 
					return new MIMETypeField();
				case DataType.Money: 
					return new MoneyField();
				case DataType.MongoDBObjectID: 
					return new MongoDBObjectIDField();
				case DataType.MovieGenres: 
					return new MovieGenresField();
				case DataType.MovieTitle: 
					return new MovieTitleField();
				case DataType.NaughtyString: 
					return new NaughtyStringField();
				case DataType.NHSNumber: 
					return new NHSNumberField();
				case DataType.NormalDistribution: 
					return new NormalDistributionField();
				case DataType.Number: 
					return new NumberField();
				case DataType.Paragraphs: 
					return new ParagraphsField();
				case DataType.Password: 
					return new PasswordField();
				case DataType.Phone: 
					return new PhoneField();
				case DataType.PlantCommonName: 
					return new PlantCommonNameField();
				case DataType.PlantFamily: 
					return new PlantFamilyField();
				case DataType.PlantScientificName: 
					return new PlantScientificNameField();
				case DataType.PoissonDistribution: 
					return new PoissonDistributionField();
				case DataType.PostalCode: 
					return new PostalCodeField();
				case DataType.ProductGrocery: 
					return new ProductGroceryField();
				case DataType.Race: 
					return new RaceField();
				case DataType.RegularExpression: 
					return new RegularExpressionField();
				case DataType.RepeatingElement: 
					return new RepeatingElementField();
				case DataType.RowNumber: 
					return new RowNumberField();
				case DataType.Scenario: 
					return new ScenarioField();
				case DataType.Sentences: 
					return new SentencesField();
				case DataType.Sequence: 
					return new SequenceField();
				case DataType.SHA1: 
					return new SHA1Field();
				case DataType.SHA256: 
					return new SHA256Field();
				case DataType.ShirtSize: 
					return new ShirtSizeField();
				case DataType.ShortHexColor: 
					return new ShortHexColorField();
				case DataType.Slogan: 
					return new SloganField();
				case DataType.SQLExpression: 
					return new SQLExpressionField();
				case DataType.SSN: 
					return new SSNField();
				case DataType.State: 
					return new StateField();
				case DataType.StateAbbreviated: 
					return new StateAbbreviatedField();
				case DataType.StockIndustry: 
					return new StockIndustryField();
				case DataType.StockMarket: 
					return new StockMarketField();
				case DataType.StockMarketCap: 
					return new StockMarketCapField();
				case DataType.StockName: 
					return new StockNameField();
				case DataType.StockSector: 
					return new StockSectorField();
				case DataType.StockSymbol: 
					return new StockSymbolField();
				case DataType.StreetAddress: 
					return new StreetAddressField();
				case DataType.StreetName: 
					return new StreetNameField();
				case DataType.StreetNumber: 
					return new StreetNumberField();
				case DataType.StreetSuffix: 
					return new StreetSuffixField();
				case DataType.Suffix: 
					return new SuffixField();
				case DataType.Template: 
					return new TemplateField();
				case DataType.Time: 
					return new TimeField();
				case DataType.TimeZone: 
					return new TimeZoneField();
				case DataType.Title: 
					return new TitleField();
				case DataType.TopLevelDomain: 
					return new TopLevelDomainField();
				case DataType.University: 
					return new UniversityField();
				case DataType.URL: 
					return new URLField();
				case DataType.UserAgent: 
					return new UserAgentField();
				case DataType.UserName: 
					return new UserNameField();
				case DataType.Words: 
					return new WordsField();
			}
		}

		/// <summary>
		/// Converts a <see cref="DataType" /> instance to its equivalent <see cref="string" /> representation.
		/// </summary>
		/// <param name="dataType">Type of the data.</param>
		/// <returns>A <see cref="string" />.</returns>
		public static string ToString(DataType dataType)
		{
			switch (dataType)
			{
				default:
					return string.Empty;
				case DataType.AnimalCommonName:
					return "Animal Common Name";
				case DataType.AnimalScientificName:
					return "Animal Scientific Name";
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
				case DataType.CarMake:
					return "Car Make";
				case DataType.CarModel:
					return "Car Model";
				case DataType.CarModelYear:
					return "Car Model Year";
				case DataType.CarVIN:
					return "Car VIN";
				case DataType.CatchPhrase:
					return "Catch Phrase";
				case DataType.CharacterSequence:
					return "Character Sequence";
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
				case DataType.DepartmentCorporate:
					return "Department (Corporate)";
				case DataType.DepartmentRetail:
					return "Department (Retail)";
				case DataType.DigitSequence:
					return "Digit Sequence";
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
				case DataType.MovieGenres:
					return "Movie Genres";
				case DataType.MovieTitle:
					return "Movie Title";
				case DataType.NaughtyString:
					return "Naughty String";
				case DataType.NHSNumber:
					return "NHS Number";
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
				case DataType.PlantCommonName:
					return "Plant Common Name";
				case DataType.PlantFamily:
					return "Plant Family";
				case DataType.PlantScientificName:
					return "Plant Scientific Name";
				case DataType.PoissonDistribution:
					return "Poisson Distribution";
				case DataType.PostalCode:
					return "Postal Code";
				case DataType.ProductGrocery:
					return "Product (Grocery)";
				case DataType.Race:
					return "Race";
				case DataType.RegularExpression:
					return "Regular Expression";
				case DataType.RepeatingElement:
					return "Repeating Element";
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
				case DataType.SQLExpression:
					return "SQL Expression";
				case DataType.SSN:
					return "SSN";
				case DataType.State:
					return "State";
				case DataType.StateAbbreviated:
					return "State (abbrev)";
				case DataType.StockIndustry:
					return "Stock Industry";
				case DataType.StockMarket:
					return "Stock Market";
				case DataType.StockMarketCap:
					return "Stock Market Cap";
				case DataType.StockName:
					return "Stock Name";
				case DataType.StockSector:
					return "Stock Sector";
				case DataType.StockSymbol:
					return "Stock Symbol";
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
				case DataType.University:
					return "University";
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