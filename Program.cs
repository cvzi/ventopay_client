using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using vp.mocca.app.service.core.Interfaces;

/*
 * Most of this file is auto-generated from https://appservice.ventopay.com/ventopayappservice.svc via
 * svcutil.exe https://appservice.ventopay.com/ventopayappservice.svc?wsdl
 */

namespace VentoClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // Use the 'client' variable to call operations on the service.
            Console.Write("Creating Client");

            bool useHttps = false;
            VentopayMobileServiceClient client;
            if (useHttps)
            {
                // This HTTPS config does not work for some reason
                var binding = new WSHttpBinding();
                binding.Security.Mode = SecurityMode.Transport;
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;

                EndpointAddress address = new EndpointAddress("https://appservice.ventopay.com/ventopayappservice.svc/basicenc");

                client = new VentopayMobileServiceClient(binding, address);
                client.ClientCredentials.ClientCertificate.Certificate = new X509Certificate2("client_certificate.pfx", "qwe123!!");
            }
            else
            {
                client = new VentopayMobileServiceClient("BasicHttpBinding_IVentopayMobileService");
            }
            Console.Write(": " + client.ToString() + " -> Ok\n");

            ListRestaurants(client);

            // Ask specific restaurants for their menu
            ShowMenu(client, "a7613c8f-19b7-4667-8682-18784067c2fe");

            ShowMenu(client, "0d683283-815f-41a9-aa9c-8d595fa44c94");

            // Always close the client.
            client.Close();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        private static void ListRestaurants(VentopayMobileServiceClient client)
        {
            Console.Write("VpMob_GetRestaurant()");
            var getRestaurantError = client.VpMob_GetRestaurant(out RestaurantConnection_DTO[] restaurantConnections);

            Console.Write(" -> " + (getRestaurantError == CardStatusError_CS.ERR_NONE ? "Ok" : getRestaurantError.ToString()) + "\n\n");

            if (restaurantConnections != null)
            {
                Console.WriteLine("Restaurants:\n");
                foreach (var conn in restaurantConnections)
                {
                    Console.WriteLine(conn.Name + "(" + conn.City + ")");
                    Console.WriteLine("Restaurant_ID:    " + conn.Restaurant_ID);
                    Console.WriteLine("ID:               " + conn.ID);
                    Console.WriteLine("RestaurantNumber: " + conn.RestaurantNumber);
                    Console.Write("Modules:          ");
                    foreach (var module in conn.AvailableModules)
                    {
                      Console.Write("* " + module.Name + "\n                  ");
                    }
                    Console.WriteLine("");
                    Console.WriteLine("-------------------\n");
                }
            }
        }

        private static void ShowMenu(VentopayMobileServiceClient client, string restaurantID)
        {
            Console.Write("VpMob_GetMenu(\"" + restaurantID + "\")");

            CardStatusError_CS getMenuError = client.VpMob_GetMenu(restaurantID, out string url, out byte[] pdf);
            Console.Write(" -> " + (getMenuError == CardStatusError_CS.ERR_NONE ? "Ok" : getMenuError.ToString()) + "\n\n");

            if (url != null && url.Length > 0)
            {
                Console.WriteLine("URL: \"" + url + "\"");
                System.Diagnostics.Process.Start(url);
            }
            if (pdf != null)
            {
                Console.WriteLine("PDF with size " + pdf.Length / 1024 + "KB");
                if (pdf.Length > 0)
                {
                    var fileName = "out.pdf";
                    Console.Write("Writing PDF to \"" + Path.GetFullPath(fileName) + "\"");
                    File.WriteAllBytes(fileName, pdf);
                    Console.WriteLine(" -> Ok!");
                    System.Diagnostics.Process.Start(fileName);
                }
            }
            Console.WriteLine("");
        }
    }


    // ################### Auto-generated code #############################


    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "PaymentType_CS", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public enum PaymentType_CS : int
    {

        [System.Runtime.Serialization.EnumMemberAttribute()]
        CASH = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        EC = 1,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ID = 2,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        GOOGLEPAY = 3,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        APPLEPAY = 4,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "CardStatusError_CS", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public enum CardStatusError_CS : int
    {

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_NONE = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_CARD_NOT_FOUND = 1,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_CARD_NOT_VALID = 2,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_PERSON_NOT_FOUND = 3,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_TOO_MANY_PERSONS = 4,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_INVALID_USERDATA = 5,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_INVALID_USERNAME = 6,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_INVALID_PASSWORD = 7,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_INVALID_EMAIL = 8,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_SALDO_TOO_HIGH = 9,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_VALUE_TOO_HIGH = 10,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_MONTH_SUM = 11,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_OTHER = 12,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_BONNUMBER_NOT_FOUND = 13,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_TOO_MANY_CARDS = 14,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_RESTAURANT_NOT_FOUND = 15,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_TOO_LESS_TURNOVER = 16,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_NO_BONUS = 17,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_NO_REPORTING = 18,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_INCOMPLETE_CONFIGURATION = 19,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_SENDING_EMAIL = 20,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_MISSING_PARAMETER = 21,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_INVALID_PARAMETER = 22,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_INVALID_CARDDATA = 23,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_CARD_ALREADY_OWNED = 24,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_CARD_ALREADY_OWNED_OTHER_PERSON = 25,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_CALCULATING_NUTRIENT_RANGEINFORMATIONS = 26,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_CALCULATING_NUTRIENT_RANGEINFORMATIONS_MISSING_PERSONDETAIL = 27,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_FEEDBACK_INVALID_ANSWERDATA = 28,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_ALREADY_REGISTERED = 29,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_BONLINE_NOT_FOUND = 30,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_LANGUAGE_UNKNOWN = 31,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_IBAN_INVALID = 32,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_BIC_INVALID = 33,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_REPORT_NO_DATA_AVAILABLE = 34,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_NO_TOKENS = 35,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_CUSTOMERGROUP_CHANGE_LIMIT_REACHED = 36,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_USERDATA_EXPIRED = 37,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_ChangePassword = 38,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_WaitingFor3DS = 39,
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "Bonline_CS", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial struct Bonline_CS : System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string AdditionalArticleBonLineEndTextField;

        private string AdditionalArticleBonLineTextIdentifierField;

        private string AdditionalBonLineEndTextField;

        private string AdditionalBonLineTextIdentifierField;

        private string ArticleNameField;

        private int ArticleNumberField;

        private string BonNumberField;

        private decimal DiscountField;

        private decimal GrossPriceField;

        private string ProductGroupNameField;

        private int QuantityField;

        private decimal SubsidyField;

        private decimal VATValueField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AdditionalArticleBonLineEndText
        {
            get
            {
                return this.AdditionalArticleBonLineEndTextField;
            }
            set
            {
                this.AdditionalArticleBonLineEndTextField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AdditionalArticleBonLineTextIdentifier
        {
            get
            {
                return this.AdditionalArticleBonLineTextIdentifierField;
            }
            set
            {
                this.AdditionalArticleBonLineTextIdentifierField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AdditionalBonLineEndText
        {
            get
            {
                return this.AdditionalBonLineEndTextField;
            }
            set
            {
                this.AdditionalBonLineEndTextField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AdditionalBonLineTextIdentifier
        {
            get
            {
                return this.AdditionalBonLineTextIdentifierField;
            }
            set
            {
                this.AdditionalBonLineTextIdentifierField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ArticleName
        {
            get
            {
                return this.ArticleNameField;
            }
            set
            {
                this.ArticleNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ArticleNumber
        {
            get
            {
                return this.ArticleNumberField;
            }
            set
            {
                this.ArticleNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BonNumber
        {
            get
            {
                return this.BonNumberField;
            }
            set
            {
                this.BonNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Discount
        {
            get
            {
                return this.DiscountField;
            }
            set
            {
                this.DiscountField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal GrossPrice
        {
            get
            {
                return this.GrossPriceField;
            }
            set
            {
                this.GrossPriceField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ProductGroupName
        {
            get
            {
                return this.ProductGroupNameField;
            }
            set
            {
                this.ProductGroupNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Quantity
        {
            get
            {
                return this.QuantityField;
            }
            set
            {
                this.QuantityField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Subsidy
        {
            get
            {
                return this.SubsidyField;
            }
            set
            {
                this.SubsidyField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal VATValue
        {
            get
            {
                return this.VATValueField;
            }
            set
            {
                this.VATValueField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "Bon_CS", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial struct Bon_CS : System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private System.DateTime BonCreationDateField;

        private string BonEndTextField;

        private string BonNumberField;

        private decimal BonPaymentTypeBAAmountField;

        private decimal BonPaymentTypeCAAmountField;

        private decimal BonPaymentTypeCHAmountField;

        private decimal BonPaymentTypeCHOAmountField;

        private decimal BonPaymentTypeIDAmountField;

        private decimal BonPaymentTypeOtherAmountField;

        private decimal BonPaymentTypeSUAmountField;

        private decimal BonPaymentTypeTOAmountField;

        private decimal BonPaymentTypeVOAmountField;

        private decimal BonTotalField;

        private VentoClient.BonType_CS BonTypeField;

        private ulong CardNumberField;

        private uint CashPointNumberField;

        private ulong PersonNumberField;

        private string PersonNumberStringField;

        private string SignatureField;

        private byte[] SignatureImageField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime BonCreationDate
        {
            get
            {
                return this.BonCreationDateField;
            }
            set
            {
                this.BonCreationDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BonEndText
        {
            get
            {
                return this.BonEndTextField;
            }
            set
            {
                this.BonEndTextField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BonNumber
        {
            get
            {
                return this.BonNumberField;
            }
            set
            {
                this.BonNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal BonPaymentTypeBAAmount
        {
            get
            {
                return this.BonPaymentTypeBAAmountField;
            }
            set
            {
                this.BonPaymentTypeBAAmountField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal BonPaymentTypeCAAmount
        {
            get
            {
                return this.BonPaymentTypeCAAmountField;
            }
            set
            {
                this.BonPaymentTypeCAAmountField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal BonPaymentTypeCHAmount
        {
            get
            {
                return this.BonPaymentTypeCHAmountField;
            }
            set
            {
                this.BonPaymentTypeCHAmountField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal BonPaymentTypeCHOAmount
        {
            get
            {
                return this.BonPaymentTypeCHOAmountField;
            }
            set
            {
                this.BonPaymentTypeCHOAmountField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal BonPaymentTypeIDAmount
        {
            get
            {
                return this.BonPaymentTypeIDAmountField;
            }
            set
            {
                this.BonPaymentTypeIDAmountField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal BonPaymentTypeOtherAmount
        {
            get
            {
                return this.BonPaymentTypeOtherAmountField;
            }
            set
            {
                this.BonPaymentTypeOtherAmountField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal BonPaymentTypeSUAmount
        {
            get
            {
                return this.BonPaymentTypeSUAmountField;
            }
            set
            {
                this.BonPaymentTypeSUAmountField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal BonPaymentTypeTOAmount
        {
            get
            {
                return this.BonPaymentTypeTOAmountField;
            }
            set
            {
                this.BonPaymentTypeTOAmountField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal BonPaymentTypeVOAmount
        {
            get
            {
                return this.BonPaymentTypeVOAmountField;
            }
            set
            {
                this.BonPaymentTypeVOAmountField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal BonTotal
        {
            get
            {
                return this.BonTotalField;
            }
            set
            {
                this.BonTotalField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public VentoClient.BonType_CS BonType
        {
            get
            {
                return this.BonTypeField;
            }
            set
            {
                this.BonTypeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public ulong CardNumber
        {
            get
            {
                return this.CardNumberField;
            }
            set
            {
                this.CardNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public uint CashPointNumber
        {
            get
            {
                return this.CashPointNumberField;
            }
            set
            {
                this.CashPointNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public ulong PersonNumber
        {
            get
            {
                return this.PersonNumberField;
            }
            set
            {
                this.PersonNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PersonNumberString
        {
            get
            {
                return this.PersonNumberStringField;
            }
            set
            {
                this.PersonNumberStringField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Signature
        {
            get
            {
                return this.SignatureField;
            }
            set
            {
                this.SignatureField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] SignatureImage
        {
            get
            {
                return this.SignatureImageField;
            }
            set
            {
                this.SignatureImageField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "BonType_CS", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public enum BonType_CS : int
    {

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Charging = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Payout = 1,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Consumption = 2,
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "Bonline_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class Bonline_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string AdditionalArticleBonLineEndTextField;

        private string AdditionalArticleBonLineTextIdentifierField;

        private string AdditionalBonLineEndTextField;

        private string AdditionalBonLineTextIdentifierField;

        private VentoClient.AllergenInformation_DTO[] AllergenInfoField;

        private string ArticleDeliveryMailAddressField;

        private string ArticleDetailNameField;

        private string ArticleDocumentField;

        private System.Nullable<int> ArticleGroupTypeIdField;

        private string ArticleGroupTypeNameField;

        private string ArticleGroupTypeShortNameField;

        private System.Guid ArticleIdField;

        private string ArticleNameField;

        private int ArticleNumberField;

        private string ArticleShortNameField;

        private System.DateTime BonCreationDateField;

        private string BonNumberField;

        private System.DateTime BonlineCreationDateField;

        private decimal DiscountField;

        private VentoClient.FeedbackProgram_DTO[] FeedbackProgramsField;

        private string FreeTextField;

        private decimal GrossPriceField;

        private string GuestDescriptionField;

        private System.Guid IDField;

        private System.Nullable<bool> IsAutomatedDiscountField;

        private System.Nullable<bool> IsBonusDiscountField;

        private System.Nullable<decimal> NutrientCorrectionFactorField;

        private VentoClient.NutrientInformation_DTO[] NutrientInfoField;

        private System.Nullable<int> ProductGroupTypeIdField;

        private string ProductGroupTypeNameField;

        private string ProductGroupTypeShortNameField;

        private int QuantityField;

        private decimal ScaleQuantityField;

        private decimal SingleItemGrossPriceField;

        private decimal SubsidyField;

        private decimal VATValueField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AdditionalArticleBonLineEndText
        {
            get
            {
                return this.AdditionalArticleBonLineEndTextField;
            }
            set
            {
                this.AdditionalArticleBonLineEndTextField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AdditionalArticleBonLineTextIdentifier
        {
            get
            {
                return this.AdditionalArticleBonLineTextIdentifierField;
            }
            set
            {
                this.AdditionalArticleBonLineTextIdentifierField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AdditionalBonLineEndText
        {
            get
            {
                return this.AdditionalBonLineEndTextField;
            }
            set
            {
                this.AdditionalBonLineEndTextField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AdditionalBonLineTextIdentifier
        {
            get
            {
                return this.AdditionalBonLineTextIdentifierField;
            }
            set
            {
                this.AdditionalBonLineTextIdentifierField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public VentoClient.AllergenInformation_DTO[] AllergenInfo
        {
            get
            {
                return this.AllergenInfoField;
            }
            set
            {
                this.AllergenInfoField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ArticleDeliveryMailAddress
        {
            get
            {
                return this.ArticleDeliveryMailAddressField;
            }
            set
            {
                this.ArticleDeliveryMailAddressField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ArticleDetailName
        {
            get
            {
                return this.ArticleDetailNameField;
            }
            set
            {
                this.ArticleDetailNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ArticleDocument
        {
            get
            {
                return this.ArticleDocumentField;
            }
            set
            {
                this.ArticleDocumentField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> ArticleGroupTypeId
        {
            get
            {
                return this.ArticleGroupTypeIdField;
            }
            set
            {
                this.ArticleGroupTypeIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ArticleGroupTypeName
        {
            get
            {
                return this.ArticleGroupTypeNameField;
            }
            set
            {
                this.ArticleGroupTypeNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ArticleGroupTypeShortName
        {
            get
            {
                return this.ArticleGroupTypeShortNameField;
            }
            set
            {
                this.ArticleGroupTypeShortNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid ArticleId
        {
            get
            {
                return this.ArticleIdField;
            }
            set
            {
                this.ArticleIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ArticleName
        {
            get
            {
                return this.ArticleNameField;
            }
            set
            {
                this.ArticleNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ArticleNumber
        {
            get
            {
                return this.ArticleNumberField;
            }
            set
            {
                this.ArticleNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ArticleShortName
        {
            get
            {
                return this.ArticleShortNameField;
            }
            set
            {
                this.ArticleShortNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime BonCreationDate
        {
            get
            {
                return this.BonCreationDateField;
            }
            set
            {
                this.BonCreationDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BonNumber
        {
            get
            {
                return this.BonNumberField;
            }
            set
            {
                this.BonNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime BonlineCreationDate
        {
            get
            {
                return this.BonlineCreationDateField;
            }
            set
            {
                this.BonlineCreationDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Discount
        {
            get
            {
                return this.DiscountField;
            }
            set
            {
                this.DiscountField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public VentoClient.FeedbackProgram_DTO[] FeedbackPrograms
        {
            get
            {
                return this.FeedbackProgramsField;
            }
            set
            {
                this.FeedbackProgramsField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FreeText
        {
            get
            {
                return this.FreeTextField;
            }
            set
            {
                this.FreeTextField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal GrossPrice
        {
            get
            {
                return this.GrossPriceField;
            }
            set
            {
                this.GrossPriceField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string GuestDescription
        {
            get
            {
                return this.GuestDescriptionField;
            }
            set
            {
                this.GuestDescriptionField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid ID
        {
            get
            {
                return this.IDField;
            }
            set
            {
                this.IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<bool> IsAutomatedDiscount
        {
            get
            {
                return this.IsAutomatedDiscountField;
            }
            set
            {
                this.IsAutomatedDiscountField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<bool> IsBonusDiscount
        {
            get
            {
                return this.IsBonusDiscountField;
            }
            set
            {
                this.IsBonusDiscountField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> NutrientCorrectionFactor
        {
            get
            {
                return this.NutrientCorrectionFactorField;
            }
            set
            {
                this.NutrientCorrectionFactorField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public VentoClient.NutrientInformation_DTO[] NutrientInfo
        {
            get
            {
                return this.NutrientInfoField;
            }
            set
            {
                this.NutrientInfoField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> ProductGroupTypeId
        {
            get
            {
                return this.ProductGroupTypeIdField;
            }
            set
            {
                this.ProductGroupTypeIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ProductGroupTypeName
        {
            get
            {
                return this.ProductGroupTypeNameField;
            }
            set
            {
                this.ProductGroupTypeNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ProductGroupTypeShortName
        {
            get
            {
                return this.ProductGroupTypeShortNameField;
            }
            set
            {
                this.ProductGroupTypeShortNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Quantity
        {
            get
            {
                return this.QuantityField;
            }
            set
            {
                this.QuantityField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ScaleQuantity
        {
            get
            {
                return this.ScaleQuantityField;
            }
            set
            {
                this.ScaleQuantityField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal SingleItemGrossPrice
        {
            get
            {
                return this.SingleItemGrossPriceField;
            }
            set
            {
                this.SingleItemGrossPriceField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Subsidy
        {
            get
            {
                return this.SubsidyField;
            }
            set
            {
                this.SubsidyField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal VATValue
        {
            get
            {
                return this.VATValueField;
            }
            set
            {
                this.VATValueField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "AllergenInformation_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class AllergenInformation_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private int IDField;

        private string NameField;

        private string ShortNameField;

        private System.Data.Linq.Binary SymbolField;

        private string TextKeyField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID
        {
            get
            {
                return this.IDField;
            }
            set
            {
                this.IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ShortName
        {
            get
            {
                return this.ShortNameField;
            }
            set
            {
                this.ShortNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Data.Linq.Binary Symbol
        {
            get
            {
                return this.SymbolField;
            }
            set
            {
                this.SymbolField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TextKey
        {
            get
            {
                return this.TextKeyField;
            }
            set
            {
                this.TextKeyField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "FeedbackProgram_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class FeedbackProgram_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private VentoClient.FeedbackProgramAnswer_DTO[] AnswerSetField;

        private System.Nullable<System.Guid> ArticleIdField;

        private int FeedbackProgramIdField;

        private string FeedbackProgramType_NameField;

        private string FeedbackProgramType_ShortNameField;

        private string FeedbackType_NameField;

        private string FeedbackType_ShortNameField;

        private int IDField;

        private string NameField;

        private string QuestionField;

        private string ShortNameField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public VentoClient.FeedbackProgramAnswer_DTO[] AnswerSet
        {
            get
            {
                return this.AnswerSetField;
            }
            set
            {
                this.AnswerSetField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.Guid> ArticleId
        {
            get
            {
                return this.ArticleIdField;
            }
            set
            {
                this.ArticleIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int FeedbackProgramId
        {
            get
            {
                return this.FeedbackProgramIdField;
            }
            set
            {
                this.FeedbackProgramIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FeedbackProgramType_Name
        {
            get
            {
                return this.FeedbackProgramType_NameField;
            }
            set
            {
                this.FeedbackProgramType_NameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FeedbackProgramType_ShortName
        {
            get
            {
                return this.FeedbackProgramType_ShortNameField;
            }
            set
            {
                this.FeedbackProgramType_ShortNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FeedbackType_Name
        {
            get
            {
                return this.FeedbackType_NameField;
            }
            set
            {
                this.FeedbackType_NameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FeedbackType_ShortName
        {
            get
            {
                return this.FeedbackType_ShortNameField;
            }
            set
            {
                this.FeedbackType_ShortNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID
        {
            get
            {
                return this.IDField;
            }
            set
            {
                this.IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Question
        {
            get
            {
                return this.QuestionField;
            }
            set
            {
                this.QuestionField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ShortName
        {
            get
            {
                return this.ShortNameField;
            }
            set
            {
                this.ShortNameField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "NutrientInformation_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class NutrientInformation_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private VentoClient.NutrientRangeInformation_DTO[] AlertLevelsField;

        private System.DateTime CalculationDateField;

        private decimal CurrentValueField;

        private int NutrientIDField;

        private string NutrientNameField;

        private string NutrientShortNameField;

        private System.Nullable<decimal> SuggestedLunchValueField;

        private string UnitNameField;

        private string UnitShortNameField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public VentoClient.NutrientRangeInformation_DTO[] AlertLevels
        {
            get
            {
                return this.AlertLevelsField;
            }
            set
            {
                this.AlertLevelsField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime CalculationDate
        {
            get
            {
                return this.CalculationDateField;
            }
            set
            {
                this.CalculationDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal CurrentValue
        {
            get
            {
                return this.CurrentValueField;
            }
            set
            {
                this.CurrentValueField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int NutrientID
        {
            get
            {
                return this.NutrientIDField;
            }
            set
            {
                this.NutrientIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string NutrientName
        {
            get
            {
                return this.NutrientNameField;
            }
            set
            {
                this.NutrientNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string NutrientShortName
        {
            get
            {
                return this.NutrientShortNameField;
            }
            set
            {
                this.NutrientShortNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> SuggestedLunchValue
        {
            get
            {
                return this.SuggestedLunchValueField;
            }
            set
            {
                this.SuggestedLunchValueField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UnitName
        {
            get
            {
                return this.UnitNameField;
            }
            set
            {
                this.UnitNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UnitShortName
        {
            get
            {
                return this.UnitShortNameField;
            }
            set
            {
                this.UnitShortNameField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "FeedbackProgramAnswer_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class FeedbackProgramAnswer_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string AnswerField;

        private int IDField;

        private bool IsFileField;

        private bool IsFreeTextField;

        private System.Nullable<int> StarRatingCountField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Answer
        {
            get
            {
                return this.AnswerField;
            }
            set
            {
                this.AnswerField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID
        {
            get
            {
                return this.IDField;
            }
            set
            {
                this.IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsFile
        {
            get
            {
                return this.IsFileField;
            }
            set
            {
                this.IsFileField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsFreeText
        {
            get
            {
                return this.IsFreeTextField;
            }
            set
            {
                this.IsFreeTextField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> StarRatingCount
        {
            get
            {
                return this.StarRatingCountField;
            }
            set
            {
                this.StarRatingCountField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "NutrientRangeInformation_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class NutrientRangeInformation_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string ColorCodeField;

        private decimal MaximumValueField;

        private decimal MinimumValueField;

        private string NameField;

        private string ShortNameField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ColorCode
        {
            get
            {
                return this.ColorCodeField;
            }
            set
            {
                this.ColorCodeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal MaximumValue
        {
            get
            {
                return this.MaximumValueField;
            }
            set
            {
                this.MaximumValueField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal MinimumValue
        {
            get
            {
                return this.MinimumValueField;
            }
            set
            {
                this.MinimumValueField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ShortName
        {
            get
            {
                return this.ShortNameField;
            }
            set
            {
                this.ShortNameField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "Bon_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class Bon_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private System.DateTime BonCreationDateField;

        private string BonEndTextField;

        private string BonNumberField;

        private decimal BonPaymentTypeBAAmountField;

        private decimal BonPaymentTypeCAAmountField;

        private decimal BonPaymentTypeCHAmountField;

        private decimal BonPaymentTypeCHOAmountField;

        private decimal BonPaymentTypeCSCAmountField;

        private decimal BonPaymentTypeIDAmountField;

        private decimal BonPaymentTypeOtherAmountField;

        private decimal BonPaymentTypeSAAmountField;

        private decimal BonPaymentTypeSUAmountField;

        private decimal BonPaymentTypeTOAmountField;

        private decimal BonPaymentTypeVOAmountField;

        private decimal BonTotalField;

        private VentoClient.BonType_CS BonTypeField;

        private System.Nullable<System.Guid> CancelBon_IDField;

        private System.Nullable<System.DateTime> CancelDateField;

        private ulong CardNumberField;

        private string CardNumberStringField;

        private System.Nullable<decimal> CardTransaction_SaldoNewField;

        private System.Nullable<decimal> CardTransaction_SaldoOldField;

        private System.Guid CashPointIdField;

        private string CashPointNameField;

        private uint CashPointNumberField;

        private string CashPointTypeNameField;

        private decimal ChargingTotalField;

        private string CityField;

        private System.Nullable<decimal> CurrentMonthSumField;

        private string CustomerGroup_InvoiceAllocationTypeShortNameField;

        private int CustomerGroup_NumberField;

        private string CustomerGroup_PaymenttypeShortNameField;

        private string EMailField;

        private string EventNameField;

        private string EventNumberField;

        private string FaxNumberField;

        private string FreeTextField;

        private string IDField;

        private System.Nullable<System.DateTime> Order_DeliveryDateAndTimeField;

        private ulong PersonNumberField;

        private string PersonNumberStringField;

        private string Person_FirstNameField;

        private string Person_LastNameField;

        private string PostalCodeField;

        private int PrintCounterField;

        private string RestaurantNameField;

        private int RestaurantNumberField;

        private string SignatureField;

        private byte[] SignatureImageField;

        private string StreetField;

        private string TelephoneNumberField;

        private VentoClient.TseTransaction_DTO TseTransactionField;

        private string UIDField;

        private string User_FirstNameField;

        private string User_LastNameField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime BonCreationDate
        {
            get
            {
                return this.BonCreationDateField;
            }
            set
            {
                this.BonCreationDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BonEndText
        {
            get
            {
                return this.BonEndTextField;
            }
            set
            {
                this.BonEndTextField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BonNumber
        {
            get
            {
                return this.BonNumberField;
            }
            set
            {
                this.BonNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal BonPaymentTypeBAAmount
        {
            get
            {
                return this.BonPaymentTypeBAAmountField;
            }
            set
            {
                this.BonPaymentTypeBAAmountField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal BonPaymentTypeCAAmount
        {
            get
            {
                return this.BonPaymentTypeCAAmountField;
            }
            set
            {
                this.BonPaymentTypeCAAmountField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal BonPaymentTypeCHAmount
        {
            get
            {
                return this.BonPaymentTypeCHAmountField;
            }
            set
            {
                this.BonPaymentTypeCHAmountField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal BonPaymentTypeCHOAmount
        {
            get
            {
                return this.BonPaymentTypeCHOAmountField;
            }
            set
            {
                this.BonPaymentTypeCHOAmountField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal BonPaymentTypeCSCAmount
        {
            get
            {
                return this.BonPaymentTypeCSCAmountField;
            }
            set
            {
                this.BonPaymentTypeCSCAmountField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal BonPaymentTypeIDAmount
        {
            get
            {
                return this.BonPaymentTypeIDAmountField;
            }
            set
            {
                this.BonPaymentTypeIDAmountField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal BonPaymentTypeOtherAmount
        {
            get
            {
                return this.BonPaymentTypeOtherAmountField;
            }
            set
            {
                this.BonPaymentTypeOtherAmountField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal BonPaymentTypeSAAmount
        {
            get
            {
                return this.BonPaymentTypeSAAmountField;
            }
            set
            {
                this.BonPaymentTypeSAAmountField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal BonPaymentTypeSUAmount
        {
            get
            {
                return this.BonPaymentTypeSUAmountField;
            }
            set
            {
                this.BonPaymentTypeSUAmountField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal BonPaymentTypeTOAmount
        {
            get
            {
                return this.BonPaymentTypeTOAmountField;
            }
            set
            {
                this.BonPaymentTypeTOAmountField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal BonPaymentTypeVOAmount
        {
            get
            {
                return this.BonPaymentTypeVOAmountField;
            }
            set
            {
                this.BonPaymentTypeVOAmountField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal BonTotal
        {
            get
            {
                return this.BonTotalField;
            }
            set
            {
                this.BonTotalField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public VentoClient.BonType_CS BonType
        {
            get
            {
                return this.BonTypeField;
            }
            set
            {
                this.BonTypeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.Guid> CancelBon_ID
        {
            get
            {
                return this.CancelBon_IDField;
            }
            set
            {
                this.CancelBon_IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> CancelDate
        {
            get
            {
                return this.CancelDateField;
            }
            set
            {
                this.CancelDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public ulong CardNumber
        {
            get
            {
                return this.CardNumberField;
            }
            set
            {
                this.CardNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CardNumberString
        {
            get
            {
                return this.CardNumberStringField;
            }
            set
            {
                this.CardNumberStringField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> CardTransaction_SaldoNew
        {
            get
            {
                return this.CardTransaction_SaldoNewField;
            }
            set
            {
                this.CardTransaction_SaldoNewField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> CardTransaction_SaldoOld
        {
            get
            {
                return this.CardTransaction_SaldoOldField;
            }
            set
            {
                this.CardTransaction_SaldoOldField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid CashPointId
        {
            get
            {
                return this.CashPointIdField;
            }
            set
            {
                this.CashPointIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CashPointName
        {
            get
            {
                return this.CashPointNameField;
            }
            set
            {
                this.CashPointNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public uint CashPointNumber
        {
            get
            {
                return this.CashPointNumberField;
            }
            set
            {
                this.CashPointNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CashPointTypeName
        {
            get
            {
                return this.CashPointTypeNameField;
            }
            set
            {
                this.CashPointTypeNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ChargingTotal
        {
            get
            {
                return this.ChargingTotalField;
            }
            set
            {
                this.ChargingTotalField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string City
        {
            get
            {
                return this.CityField;
            }
            set
            {
                this.CityField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> CurrentMonthSum
        {
            get
            {
                return this.CurrentMonthSumField;
            }
            set
            {
                this.CurrentMonthSumField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CustomerGroup_InvoiceAllocationTypeShortName
        {
            get
            {
                return this.CustomerGroup_InvoiceAllocationTypeShortNameField;
            }
            set
            {
                this.CustomerGroup_InvoiceAllocationTypeShortNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int CustomerGroup_Number
        {
            get
            {
                return this.CustomerGroup_NumberField;
            }
            set
            {
                this.CustomerGroup_NumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CustomerGroup_PaymenttypeShortName
        {
            get
            {
                return this.CustomerGroup_PaymenttypeShortNameField;
            }
            set
            {
                this.CustomerGroup_PaymenttypeShortNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EMail
        {
            get
            {
                return this.EMailField;
            }
            set
            {
                this.EMailField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EventName
        {
            get
            {
                return this.EventNameField;
            }
            set
            {
                this.EventNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EventNumber
        {
            get
            {
                return this.EventNumberField;
            }
            set
            {
                this.EventNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FaxNumber
        {
            get
            {
                return this.FaxNumberField;
            }
            set
            {
                this.FaxNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FreeText
        {
            get
            {
                return this.FreeTextField;
            }
            set
            {
                this.FreeTextField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ID
        {
            get
            {
                return this.IDField;
            }
            set
            {
                this.IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> Order_DeliveryDateAndTime
        {
            get
            {
                return this.Order_DeliveryDateAndTimeField;
            }
            set
            {
                this.Order_DeliveryDateAndTimeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public ulong PersonNumber
        {
            get
            {
                return this.PersonNumberField;
            }
            set
            {
                this.PersonNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PersonNumberString
        {
            get
            {
                return this.PersonNumberStringField;
            }
            set
            {
                this.PersonNumberStringField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Person_FirstName
        {
            get
            {
                return this.Person_FirstNameField;
            }
            set
            {
                this.Person_FirstNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Person_LastName
        {
            get
            {
                return this.Person_LastNameField;
            }
            set
            {
                this.Person_LastNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PostalCode
        {
            get
            {
                return this.PostalCodeField;
            }
            set
            {
                this.PostalCodeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int PrintCounter
        {
            get
            {
                return this.PrintCounterField;
            }
            set
            {
                this.PrintCounterField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RestaurantName
        {
            get
            {
                return this.RestaurantNameField;
            }
            set
            {
                this.RestaurantNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int RestaurantNumber
        {
            get
            {
                return this.RestaurantNumberField;
            }
            set
            {
                this.RestaurantNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Signature
        {
            get
            {
                return this.SignatureField;
            }
            set
            {
                this.SignatureField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] SignatureImage
        {
            get
            {
                return this.SignatureImageField;
            }
            set
            {
                this.SignatureImageField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Street
        {
            get
            {
                return this.StreetField;
            }
            set
            {
                this.StreetField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TelephoneNumber
        {
            get
            {
                return this.TelephoneNumberField;
            }
            set
            {
                this.TelephoneNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public VentoClient.TseTransaction_DTO TseTransaction
        {
            get
            {
                return this.TseTransactionField;
            }
            set
            {
                this.TseTransactionField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UID
        {
            get
            {
                return this.UIDField;
            }
            set
            {
                this.UIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string User_FirstName
        {
            get
            {
                return this.User_FirstNameField;
            }
            set
            {
                this.User_FirstNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string User_LastName
        {
            get
            {
                return this.User_LastNameField;
            }
            set
            {
                this.User_LastNameField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "TseTransaction_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class TseTransaction_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string ClientIdField;

        private System.Nullable<System.DateTime> FinishTransactionTimeStampField;

        private bool IsTseOutOfOrderField;

        private string ProcessDataField;

        private string ProcessTypeNameField;

        private string PublicKeyField;

        private string QrDataField;

        private string SerialNumberField;

        private string SignatureField;

        private string SignatureAlgorithmField;

        private string SignatureCounterField;

        private byte[] SignatureImageField;

        private System.Nullable<System.DateTime> StartTransactionTimeStampField;

        private string TimeFormatField;

        private string TransactionNumberField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ClientId
        {
            get
            {
                return this.ClientIdField;
            }
            set
            {
                this.ClientIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> FinishTransactionTimeStamp
        {
            get
            {
                return this.FinishTransactionTimeStampField;
            }
            set
            {
                this.FinishTransactionTimeStampField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsTseOutOfOrder
        {
            get
            {
                return this.IsTseOutOfOrderField;
            }
            set
            {
                this.IsTseOutOfOrderField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ProcessData
        {
            get
            {
                return this.ProcessDataField;
            }
            set
            {
                this.ProcessDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ProcessTypeName
        {
            get
            {
                return this.ProcessTypeNameField;
            }
            set
            {
                this.ProcessTypeNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PublicKey
        {
            get
            {
                return this.PublicKeyField;
            }
            set
            {
                this.PublicKeyField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string QrData
        {
            get
            {
                return this.QrDataField;
            }
            set
            {
                this.QrDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SerialNumber
        {
            get
            {
                return this.SerialNumberField;
            }
            set
            {
                this.SerialNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Signature
        {
            get
            {
                return this.SignatureField;
            }
            set
            {
                this.SignatureField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SignatureAlgorithm
        {
            get
            {
                return this.SignatureAlgorithmField;
            }
            set
            {
                this.SignatureAlgorithmField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SignatureCounter
        {
            get
            {
                return this.SignatureCounterField;
            }
            set
            {
                this.SignatureCounterField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] SignatureImage
        {
            get
            {
                return this.SignatureImageField;
            }
            set
            {
                this.SignatureImageField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> StartTransactionTimeStamp
        {
            get
            {
                return this.StartTransactionTimeStampField;
            }
            set
            {
                this.StartTransactionTimeStampField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TimeFormat
        {
            get
            {
                return this.TimeFormatField;
            }
            set
            {
                this.TimeFormatField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TransactionNumber
        {
            get
            {
                return this.TransactionNumberField;
            }
            set
            {
                this.TransactionNumberField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "Module_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class Module_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string[] CustomergroupNumbersField;

        private int IDField;

        private System.Nullable<bool> IsWebsiteToEmbedField;

        private string NameField;

        private System.Data.Linq.Binary PDFField;

        private string[] RoleShortNamesField;

        private string ShortNameField;

        private string SingleSignOnInformationField;

        private System.Data.Linq.Binary SymbolField;

        private string WebsiteUrlField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] CustomergroupNumbers
        {
            get
            {
                return this.CustomergroupNumbersField;
            }
            set
            {
                this.CustomergroupNumbersField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID
        {
            get
            {
                return this.IDField;
            }
            set
            {
                this.IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<bool> IsWebsiteToEmbed
        {
            get
            {
                return this.IsWebsiteToEmbedField;
            }
            set
            {
                this.IsWebsiteToEmbedField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Data.Linq.Binary PDF
        {
            get
            {
                return this.PDFField;
            }
            set
            {
                this.PDFField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] RoleShortNames
        {
            get
            {
                return this.RoleShortNamesField;
            }
            set
            {
                this.RoleShortNamesField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ShortName
        {
            get
            {
                return this.ShortNameField;
            }
            set
            {
                this.ShortNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SingleSignOnInformation
        {
            get
            {
                return this.SingleSignOnInformationField;
            }
            set
            {
                this.SingleSignOnInformationField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Data.Linq.Binary Symbol
        {
            get
            {
                return this.SymbolField;
            }
            set
            {
                this.SymbolField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string WebsiteUrl
        {
            get
            {
                return this.WebsiteUrlField;
            }
            set
            {
                this.WebsiteUrlField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "NewsFeedItem_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class NewsFeedItem_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string HeaderTextField;

        private int IDField;

        private System.DateTime InformationDateField;

        private string InformationTextField;

        private System.Nullable<bool> IsToDeactivateOnSelectionField;

        private byte[] LanguageIdField;

        private System.Nullable<int> ModuleIdField;

        private string ModuleNameField;

        private string NewsFeedType_ShortNameField;

        private byte[] PDFField;

        private byte[] PictureField;

        private System.Nullable<int> RemainingItemsField;

        private string URLField;

        private System.DateTime ValidFromField;

        private System.Nullable<System.DateTime> ValidUntilField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string HeaderText
        {
            get
            {
                return this.HeaderTextField;
            }
            set
            {
                this.HeaderTextField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID
        {
            get
            {
                return this.IDField;
            }
            set
            {
                this.IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime InformationDate
        {
            get
            {
                return this.InformationDateField;
            }
            set
            {
                this.InformationDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string InformationText
        {
            get
            {
                return this.InformationTextField;
            }
            set
            {
                this.InformationTextField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<bool> IsToDeactivateOnSelection
        {
            get
            {
                return this.IsToDeactivateOnSelectionField;
            }
            set
            {
                this.IsToDeactivateOnSelectionField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] LanguageId
        {
            get
            {
                return this.LanguageIdField;
            }
            set
            {
                this.LanguageIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> ModuleId
        {
            get
            {
                return this.ModuleIdField;
            }
            set
            {
                this.ModuleIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ModuleName
        {
            get
            {
                return this.ModuleNameField;
            }
            set
            {
                this.ModuleNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string NewsFeedType_ShortName
        {
            get
            {
                return this.NewsFeedType_ShortNameField;
            }
            set
            {
                this.NewsFeedType_ShortNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] PDF
        {
            get
            {
                return this.PDFField;
            }
            set
            {
                this.PDFField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] Picture
        {
            get
            {
                return this.PictureField;
            }
            set
            {
                this.PictureField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> RemainingItems
        {
            get
            {
                return this.RemainingItemsField;
            }
            set
            {
                this.RemainingItemsField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string URL
        {
            get
            {
                return this.URLField;
            }
            set
            {
                this.URLField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ValidFrom
        {
            get
            {
                return this.ValidFromField;
            }
            set
            {
                this.ValidFromField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> ValidUntil
        {
            get
            {
                return this.ValidUntilField;
            }
            set
            {
                this.ValidUntilField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "PersonInformation_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class PersonInformation_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string AccountHolderField;

        private System.Nullable<decimal> ActivityFactorField;

        private VentoClient.ActivityLevel_DTO ActivityLevelField;

        private System.Nullable<System.Guid> ActivityLevelIdField;

        private string AdditionalInfo1Field;

        private string AdditionalInfo10Field;

        private string AdditionalInfo2Field;

        private string AdditionalInfo3Field;

        private string AdditionalInfo4Field;

        private string AdditionalInfo5Field;

        private string AdditionalInfo6Field;

        private string AdditionalInfo7Field;

        private string AdditionalInfo8Field;

        private string AdditionalInfo9Field;

        private string AddressField;

        private VentoClient.Address_DTO AddressDtoField;

        private string BicField;

        private System.Nullable<System.DateTime> BirthdayField;

        private System.Nullable<decimal> BreadUnitsField;

        private VentoClient.CardInformation_DTO[] CardsField;

        private string CityField;

        private System.Guid CustomerGroupIdField;

        private string CustomerGroupNameField;

        private int CustomerGroupNumberField;

        private VentoClient.DeviceToken_DTO[] DeviceTokensField;

        private string EMailField;

        private System.Nullable<System.Guid> EmailVerificationKeyField;

        private string FirstNameField;

        private string GenderShortNameField;

        private System.Guid IDField;

        private string IbanField;

        private System.Nullable<decimal> IdChargingSumField;

        private System.Nullable<System.Guid> ImageIdField;

        private string ImportStatusField;

        private bool IsIdentityProviderRegisteredField;

        private bool IsMailValidatedField;

        private System.Nullable<System.DateTime> LastLoginDateField;

        private string LastNameField;

        private string LastSelectedLanguageShortnameField;

        private System.Nullable<int> LastSelectedLanguage_IDField;

        private string MandateReferenceField;

        private System.Nullable<decimal> NutrientAlertLevelField;

        private string PINField;

        private string PasswordHashField;

        private VentoClient.PaymentType_DTO PaymentTypeField;

        private string PersonNumberField;

        private System.Nullable<System.DateTime> PhoneNumberAcceptedDateField;

        private System.Nullable<System.DateTime> PhoneNumberSendDateField;

        private string PhoneVerificationKeyField;

        private VentoClient.AllergenInformation_DTO[] RestrictedAllergensField;

        private VentoClient.PaymentType_DTO SecondaryPaymentTypeField;

        private VentoClient.Role_DTO[] SecurityRolesField;

        private int[] SelectedDeliveryLocationsField;

        private System.Nullable<System.Guid> SelectedRestaurantIdField;

        private System.Guid[] SelectedRestaurantsField;

        private System.Nullable<System.DateTime> SepaSignatureDateField;

        private System.Nullable<uint> SizeField;

        private System.Nullable<System.DateTime> TermsOfUseAcceptedDateField;

        private VentoClient.PaymentType_DTO[] TopUpPaymentTypesField;

        private string UserNameField;

        private System.Nullable<uint> WeightField;

        private System.Nullable<uint> WrongPINEntriesField;

        private string ZipField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AccountHolder
        {
            get
            {
                return this.AccountHolderField;
            }
            set
            {
                this.AccountHolderField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> ActivityFactor
        {
            get
            {
                return this.ActivityFactorField;
            }
            set
            {
                this.ActivityFactorField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public VentoClient.ActivityLevel_DTO ActivityLevel
        {
            get
            {
                return this.ActivityLevelField;
            }
            set
            {
                this.ActivityLevelField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.Guid> ActivityLevelId
        {
            get
            {
                return this.ActivityLevelIdField;
            }
            set
            {
                this.ActivityLevelIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AdditionalInfo1
        {
            get
            {
                return this.AdditionalInfo1Field;
            }
            set
            {
                this.AdditionalInfo1Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AdditionalInfo10
        {
            get
            {
                return this.AdditionalInfo10Field;
            }
            set
            {
                this.AdditionalInfo10Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AdditionalInfo2
        {
            get
            {
                return this.AdditionalInfo2Field;
            }
            set
            {
                this.AdditionalInfo2Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AdditionalInfo3
        {
            get
            {
                return this.AdditionalInfo3Field;
            }
            set
            {
                this.AdditionalInfo3Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AdditionalInfo4
        {
            get
            {
                return this.AdditionalInfo4Field;
            }
            set
            {
                this.AdditionalInfo4Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AdditionalInfo5
        {
            get
            {
                return this.AdditionalInfo5Field;
            }
            set
            {
                this.AdditionalInfo5Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AdditionalInfo6
        {
            get
            {
                return this.AdditionalInfo6Field;
            }
            set
            {
                this.AdditionalInfo6Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AdditionalInfo7
        {
            get
            {
                return this.AdditionalInfo7Field;
            }
            set
            {
                this.AdditionalInfo7Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AdditionalInfo8
        {
            get
            {
                return this.AdditionalInfo8Field;
            }
            set
            {
                this.AdditionalInfo8Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AdditionalInfo9
        {
            get
            {
                return this.AdditionalInfo9Field;
            }
            set
            {
                this.AdditionalInfo9Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Address
        {
            get
            {
                return this.AddressField;
            }
            set
            {
                this.AddressField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public VentoClient.Address_DTO AddressDto
        {
            get
            {
                return this.AddressDtoField;
            }
            set
            {
                this.AddressDtoField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Bic
        {
            get
            {
                return this.BicField;
            }
            set
            {
                this.BicField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> Birthday
        {
            get
            {
                return this.BirthdayField;
            }
            set
            {
                this.BirthdayField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> BreadUnits
        {
            get
            {
                return this.BreadUnitsField;
            }
            set
            {
                this.BreadUnitsField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public VentoClient.CardInformation_DTO[] Cards
        {
            get
            {
                return this.CardsField;
            }
            set
            {
                this.CardsField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string City
        {
            get
            {
                return this.CityField;
            }
            set
            {
                this.CityField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid CustomerGroupId
        {
            get
            {
                return this.CustomerGroupIdField;
            }
            set
            {
                this.CustomerGroupIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CustomerGroupName
        {
            get
            {
                return this.CustomerGroupNameField;
            }
            set
            {
                this.CustomerGroupNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int CustomerGroupNumber
        {
            get
            {
                return this.CustomerGroupNumberField;
            }
            set
            {
                this.CustomerGroupNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public VentoClient.DeviceToken_DTO[] DeviceTokens
        {
            get
            {
                return this.DeviceTokensField;
            }
            set
            {
                this.DeviceTokensField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EMail
        {
            get
            {
                return this.EMailField;
            }
            set
            {
                this.EMailField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.Guid> EmailVerificationKey
        {
            get
            {
                return this.EmailVerificationKeyField;
            }
            set
            {
                this.EmailVerificationKeyField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FirstName
        {
            get
            {
                return this.FirstNameField;
            }
            set
            {
                this.FirstNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string GenderShortName
        {
            get
            {
                return this.GenderShortNameField;
            }
            set
            {
                this.GenderShortNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid ID
        {
            get
            {
                return this.IDField;
            }
            set
            {
                this.IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Iban
        {
            get
            {
                return this.IbanField;
            }
            set
            {
                this.IbanField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> IdChargingSum
        {
            get
            {
                return this.IdChargingSumField;
            }
            set
            {
                this.IdChargingSumField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.Guid> ImageId
        {
            get
            {
                return this.ImageIdField;
            }
            set
            {
                this.ImageIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ImportStatus
        {
            get
            {
                return this.ImportStatusField;
            }
            set
            {
                this.ImportStatusField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsIdentityProviderRegistered
        {
            get
            {
                return this.IsIdentityProviderRegisteredField;
            }
            set
            {
                this.IsIdentityProviderRegisteredField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsMailValidated
        {
            get
            {
                return this.IsMailValidatedField;
            }
            set
            {
                this.IsMailValidatedField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> LastLoginDate
        {
            get
            {
                return this.LastLoginDateField;
            }
            set
            {
                this.LastLoginDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LastName
        {
            get
            {
                return this.LastNameField;
            }
            set
            {
                this.LastNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LastSelectedLanguageShortname
        {
            get
            {
                return this.LastSelectedLanguageShortnameField;
            }
            set
            {
                this.LastSelectedLanguageShortnameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> LastSelectedLanguage_ID
        {
            get
            {
                return this.LastSelectedLanguage_IDField;
            }
            set
            {
                this.LastSelectedLanguage_IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MandateReference
        {
            get
            {
                return this.MandateReferenceField;
            }
            set
            {
                this.MandateReferenceField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> NutrientAlertLevel
        {
            get
            {
                return this.NutrientAlertLevelField;
            }
            set
            {
                this.NutrientAlertLevelField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PIN
        {
            get
            {
                return this.PINField;
            }
            set
            {
                this.PINField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PasswordHash
        {
            get
            {
                return this.PasswordHashField;
            }
            set
            {
                this.PasswordHashField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public VentoClient.PaymentType_DTO PaymentType
        {
            get
            {
                return this.PaymentTypeField;
            }
            set
            {
                this.PaymentTypeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PersonNumber
        {
            get
            {
                return this.PersonNumberField;
            }
            set
            {
                this.PersonNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> PhoneNumberAcceptedDate
        {
            get
            {
                return this.PhoneNumberAcceptedDateField;
            }
            set
            {
                this.PhoneNumberAcceptedDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> PhoneNumberSendDate
        {
            get
            {
                return this.PhoneNumberSendDateField;
            }
            set
            {
                this.PhoneNumberSendDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PhoneVerificationKey
        {
            get
            {
                return this.PhoneVerificationKeyField;
            }
            set
            {
                this.PhoneVerificationKeyField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public VentoClient.AllergenInformation_DTO[] RestrictedAllergens
        {
            get
            {
                return this.RestrictedAllergensField;
            }
            set
            {
                this.RestrictedAllergensField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public VentoClient.PaymentType_DTO SecondaryPaymentType
        {
            get
            {
                return this.SecondaryPaymentTypeField;
            }
            set
            {
                this.SecondaryPaymentTypeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public VentoClient.Role_DTO[] SecurityRoles
        {
            get
            {
                return this.SecurityRolesField;
            }
            set
            {
                this.SecurityRolesField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int[] SelectedDeliveryLocations
        {
            get
            {
                return this.SelectedDeliveryLocationsField;
            }
            set
            {
                this.SelectedDeliveryLocationsField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.Guid> SelectedRestaurantId
        {
            get
            {
                return this.SelectedRestaurantIdField;
            }
            set
            {
                this.SelectedRestaurantIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid[] SelectedRestaurants
        {
            get
            {
                return this.SelectedRestaurantsField;
            }
            set
            {
                this.SelectedRestaurantsField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> SepaSignatureDate
        {
            get
            {
                return this.SepaSignatureDateField;
            }
            set
            {
                this.SepaSignatureDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<uint> Size
        {
            get
            {
                return this.SizeField;
            }
            set
            {
                this.SizeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> TermsOfUseAcceptedDate
        {
            get
            {
                return this.TermsOfUseAcceptedDateField;
            }
            set
            {
                this.TermsOfUseAcceptedDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public VentoClient.PaymentType_DTO[] TopUpPaymentTypes
        {
            get
            {
                return this.TopUpPaymentTypesField;
            }
            set
            {
                this.TopUpPaymentTypesField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName
        {
            get
            {
                return this.UserNameField;
            }
            set
            {
                this.UserNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<uint> Weight
        {
            get
            {
                return this.WeightField;
            }
            set
            {
                this.WeightField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<uint> WrongPINEntries
        {
            get
            {
                return this.WrongPINEntriesField;
            }
            set
            {
                this.WrongPINEntriesField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Zip
        {
            get
            {
                return this.ZipField;
            }
            set
            {
                this.ZipField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "ActivityLevel_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class ActivityLevel_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private System.Guid IDField;

        private string NameField;

        private byte[] PictureField;

        private string ShortNameField;

        private decimal ValueField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid ID
        {
            get
            {
                return this.IDField;
            }
            set
            {
                this.IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] Picture
        {
            get
            {
                return this.PictureField;
            }
            set
            {
                this.PictureField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ShortName
        {
            get
            {
                return this.ShortNameField;
            }
            set
            {
                this.ShortNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Value
        {
            get
            {
                return this.ValueField;
            }
            set
            {
                this.ValueField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "Address_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class Address_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string Person_CityField;

        private string Person_DoorField;

        private string Person_EmailField;

        private string Person_FirstNameField;

        private string Person_HouseNumberField;

        private string Person_LastNameField;

        private string Person_PhoneField;

        private string Person_StairField;

        private string Person_StreetField;

        private string Person_ZipField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Person_City
        {
            get
            {
                return this.Person_CityField;
            }
            set
            {
                this.Person_CityField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Person_Door
        {
            get
            {
                return this.Person_DoorField;
            }
            set
            {
                this.Person_DoorField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Person_Email
        {
            get
            {
                return this.Person_EmailField;
            }
            set
            {
                this.Person_EmailField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Person_FirstName
        {
            get
            {
                return this.Person_FirstNameField;
            }
            set
            {
                this.Person_FirstNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Person_HouseNumber
        {
            get
            {
                return this.Person_HouseNumberField;
            }
            set
            {
                this.Person_HouseNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Person_LastName
        {
            get
            {
                return this.Person_LastNameField;
            }
            set
            {
                this.Person_LastNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Person_Phone
        {
            get
            {
                return this.Person_PhoneField;
            }
            set
            {
                this.Person_PhoneField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Person_Stair
        {
            get
            {
                return this.Person_StairField;
            }
            set
            {
                this.Person_StairField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Person_Street
        {
            get
            {
                return this.Person_StreetField;
            }
            set
            {
                this.Person_StreetField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Person_Zip
        {
            get
            {
                return this.Person_ZipField;
            }
            set
            {
                this.Person_ZipField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "PaymentType_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class PaymentType_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private System.Guid IDField;

        private string NameField;

        private string ShortNameField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid ID
        {
            get
            {
                return this.IDField;
            }
            set
            {
                this.IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ShortName
        {
            get
            {
                return this.ShortNameField;
            }
            set
            {
                this.ShortNameField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "CardInformation_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class CardInformation_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private VentoClient.CardAutomaticRecharge_DTO CardAutomaticRechargeField;

        private string CardNumberField;

        private System.Guid IDField;

        private string ImportStatusField;

        private System.DateTime ValidFromField;

        private System.Nullable<System.DateTime> ValidUntilField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public VentoClient.CardAutomaticRecharge_DTO CardAutomaticRecharge
        {
            get
            {
                return this.CardAutomaticRechargeField;
            }
            set
            {
                this.CardAutomaticRechargeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CardNumber
        {
            get
            {
                return this.CardNumberField;
            }
            set
            {
                this.CardNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid ID
        {
            get
            {
                return this.IDField;
            }
            set
            {
                this.IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ImportStatus
        {
            get
            {
                return this.ImportStatusField;
            }
            set
            {
                this.ImportStatusField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ValidFrom
        {
            get
            {
                return this.ValidFromField;
            }
            set
            {
                this.ValidFromField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> ValidUntil
        {
            get
            {
                return this.ValidUntilField;
            }
            set
            {
                this.ValidUntilField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "DeviceToken_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class DeviceToken_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string DeviceIDField;

        private System.Guid PersonIDField;

        private string PlatformField;

        private string TokenField;

        private System.DateTime ValidFromField;

        private System.Nullable<System.DateTime> ValidUntilField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DeviceID
        {
            get
            {
                return this.DeviceIDField;
            }
            set
            {
                this.DeviceIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid PersonID
        {
            get
            {
                return this.PersonIDField;
            }
            set
            {
                this.PersonIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Platform
        {
            get
            {
                return this.PlatformField;
            }
            set
            {
                this.PlatformField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Token
        {
            get
            {
                return this.TokenField;
            }
            set
            {
                this.TokenField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ValidFrom
        {
            get
            {
                return this.ValidFromField;
            }
            set
            {
                this.ValidFromField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> ValidUntil
        {
            get
            {
                return this.ValidUntilField;
            }
            set
            {
                this.ValidUntilField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "Role_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class Role_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string DisplayNameField;

        private System.Guid IDField;

        private string NameField;

        private System.Guid RestaurantIdField;

        private string ShortNameField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DisplayName
        {
            get
            {
                return this.DisplayNameField;
            }
            set
            {
                this.DisplayNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid ID
        {
            get
            {
                return this.IDField;
            }
            set
            {
                this.IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid RestaurantId
        {
            get
            {
                return this.RestaurantIdField;
            }
            set
            {
                this.RestaurantIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ShortName
        {
            get
            {
                return this.ShortNameField;
            }
            set
            {
                this.ShortNameField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "CardAutomaticRecharge_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class CardAutomaticRecharge_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private System.Guid IdField;

        private string InitialTransactionIdField;

        private System.Nullable<System.DateTime> LastCheckDateField;

        private int MinimumSaldoInCentField;

        private string ParentTransactionIdField;

        private int RecurringStatusField;

        private int ReloadValueInCentField;

        private int RequestState_IDField;

        private System.Nullable<bool> isActiveField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string InitialTransactionId
        {
            get
            {
                return this.InitialTransactionIdField;
            }
            set
            {
                this.InitialTransactionIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> LastCheckDate
        {
            get
            {
                return this.LastCheckDateField;
            }
            set
            {
                this.LastCheckDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int MinimumSaldoInCent
        {
            get
            {
                return this.MinimumSaldoInCentField;
            }
            set
            {
                this.MinimumSaldoInCentField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ParentTransactionId
        {
            get
            {
                return this.ParentTransactionIdField;
            }
            set
            {
                this.ParentTransactionIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int RecurringStatus
        {
            get
            {
                return this.RecurringStatusField;
            }
            set
            {
                this.RecurringStatusField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ReloadValueInCent
        {
            get
            {
                return this.ReloadValueInCentField;
            }
            set
            {
                this.ReloadValueInCentField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int RequestState_ID
        {
            get
            {
                return this.RequestState_IDField;
            }
            set
            {
                this.RequestState_IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<bool> isActive
        {
            get
            {
                return this.isActiveField;
            }
            set
            {
                this.isActiveField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "CustomerGroupInformation_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class CustomerGroupInformation_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string AdditionalBonHeaderTextField;

        private int CustomerGroupNumberField;

        private System.Guid IDField;

        private System.Nullable<bool> IsBlePaymentAllowedField;

        private System.Nullable<bool> IsNegativeChipSaldoAllowedField;

        private System.Nullable<bool> IsOrderOnlyForEstimationField;

        private System.Nullable<bool> IsSubsidyToBeShownWithinVATTableOnReceiptField;

        private string IsTaxDetailToShowField;

        private System.Nullable<decimal> MaxSubsidyField;

        private System.Nullable<int> MinimumChipSaldoField;

        private string NameField;

        private string PaymentTypeShortNameField;

        private string SecondaryPaymentTypeShortNameField;

        private string ShortNameField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AdditionalBonHeaderText
        {
            get
            {
                return this.AdditionalBonHeaderTextField;
            }
            set
            {
                this.AdditionalBonHeaderTextField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int CustomerGroupNumber
        {
            get
            {
                return this.CustomerGroupNumberField;
            }
            set
            {
                this.CustomerGroupNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid ID
        {
            get
            {
                return this.IDField;
            }
            set
            {
                this.IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<bool> IsBlePaymentAllowed
        {
            get
            {
                return this.IsBlePaymentAllowedField;
            }
            set
            {
                this.IsBlePaymentAllowedField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<bool> IsNegativeChipSaldoAllowed
        {
            get
            {
                return this.IsNegativeChipSaldoAllowedField;
            }
            set
            {
                this.IsNegativeChipSaldoAllowedField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<bool> IsOrderOnlyForEstimation
        {
            get
            {
                return this.IsOrderOnlyForEstimationField;
            }
            set
            {
                this.IsOrderOnlyForEstimationField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<bool> IsSubsidyToBeShownWithinVATTableOnReceipt
        {
            get
            {
                return this.IsSubsidyToBeShownWithinVATTableOnReceiptField;
            }
            set
            {
                this.IsSubsidyToBeShownWithinVATTableOnReceiptField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string IsTaxDetailToShow
        {
            get
            {
                return this.IsTaxDetailToShowField;
            }
            set
            {
                this.IsTaxDetailToShowField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> MaxSubsidy
        {
            get
            {
                return this.MaxSubsidyField;
            }
            set
            {
                this.MaxSubsidyField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> MinimumChipSaldo
        {
            get
            {
                return this.MinimumChipSaldoField;
            }
            set
            {
                this.MinimumChipSaldoField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PaymentTypeShortName
        {
            get
            {
                return this.PaymentTypeShortNameField;
            }
            set
            {
                this.PaymentTypeShortNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SecondaryPaymentTypeShortName
        {
            get
            {
                return this.SecondaryPaymentTypeShortNameField;
            }
            set
            {
                this.SecondaryPaymentTypeShortNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ShortName
        {
            get
            {
                return this.ShortNameField;
            }
            set
            {
                this.ShortNameField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "ContactInformation_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class ContactInformation_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string DescriptionField;

        private int IDField;

        private string NameField;

        private string ReceiverAddressField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description
        {
            get
            {
                return this.DescriptionField;
            }
            set
            {
                this.DescriptionField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID
        {
            get
            {
                return this.IDField;
            }
            set
            {
                this.IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ReceiverAddress
        {
            get
            {
                return this.ReceiverAddressField;
            }
            set
            {
                this.ReceiverAddressField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "TopUp_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class TopUp_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private System.Guid CardIdField;

        private string DescriptionField;

        private System.Nullable<System.DateTime> ExecutionDateField;

        private System.Guid IDField;

        private System.Nullable<System.DateTime> SendDateField;

        private int ValueInCentField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid CardId
        {
            get
            {
                return this.CardIdField;
            }
            set
            {
                this.CardIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description
        {
            get
            {
                return this.DescriptionField;
            }
            set
            {
                this.DescriptionField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> ExecutionDate
        {
            get
            {
                return this.ExecutionDateField;
            }
            set
            {
                this.ExecutionDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid ID
        {
            get
            {
                return this.IDField;
            }
            set
            {
                this.IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> SendDate
        {
            get
            {
                return this.SendDateField;
            }
            set
            {
                this.SendDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ValueInCent
        {
            get
            {
                return this.ValueInCentField;
            }
            set
            {
                this.ValueInCentField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "FeedbackPersonAnswer_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class FeedbackPersonAnswer_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private System.Nullable<System.DateTime> AnswerDateField;

        private System.Nullable<System.Guid> Bonline_IDField;

        private int FeedbackProgramAnswerIdField;

        private int FeedbackProgramIdField;

        private VentoClient.File_DTO FileField;

        private string FreeTextField;

        private System.Nullable<int> StarRatingValueField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> AnswerDate
        {
            get
            {
                return this.AnswerDateField;
            }
            set
            {
                this.AnswerDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.Guid> Bonline_ID
        {
            get
            {
                return this.Bonline_IDField;
            }
            set
            {
                this.Bonline_IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int FeedbackProgramAnswerId
        {
            get
            {
                return this.FeedbackProgramAnswerIdField;
            }
            set
            {
                this.FeedbackProgramAnswerIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int FeedbackProgramId
        {
            get
            {
                return this.FeedbackProgramIdField;
            }
            set
            {
                this.FeedbackProgramIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public VentoClient.File_DTO File
        {
            get
            {
                return this.FileField;
            }
            set
            {
                this.FileField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FreeText
        {
            get
            {
                return this.FreeTextField;
            }
            set
            {
                this.FreeTextField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> StarRatingValue
        {
            get
            {
                return this.StarRatingValueField;
            }
            set
            {
                this.StarRatingValueField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "File_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class File_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private System.Data.Linq.Binary FileField;

        private VentoClient.FileFormat_CS FileFormatField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Data.Linq.Binary File
        {
            get
            {
                return this.FileField;
            }
            set
            {
                this.FileField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public VentoClient.FileFormat_CS FileFormat
        {
            get
            {
                return this.FileFormatField;
            }
            set
            {
                this.FileFormatField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "FileFormat_CS", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public enum FileFormat_CS : int
    {

        [System.Runtime.Serialization.EnumMemberAttribute()]
        MP4 = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        JPG = 1,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        PNG = 2,
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "TermState_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class TermState_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private System.DateTime ChangeDateField;

        private bool IsAcceptedField;

        private System.Guid PersonIdField;

        private int TermIdField;

        private string TermNameField;

        private string TermShortNameField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ChangeDate
        {
            get
            {
                return this.ChangeDateField;
            }
            set
            {
                this.ChangeDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsAccepted
        {
            get
            {
                return this.IsAcceptedField;
            }
            set
            {
                this.IsAcceptedField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid PersonId
        {
            get
            {
                return this.PersonIdField;
            }
            set
            {
                this.PersonIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int TermId
        {
            get
            {
                return this.TermIdField;
            }
            set
            {
                this.TermIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TermName
        {
            get
            {
                return this.TermNameField;
            }
            set
            {
                this.TermNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TermShortName
        {
            get
            {
                return this.TermShortNameField;
            }
            set
            {
                this.TermShortNameField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "Language_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class Language_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private int IDField;

        private string NameField;

        private string ShortNameField;

        private System.Data.Linq.Binary SymbolField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID
        {
            get
            {
                return this.IDField;
            }
            set
            {
                this.IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ShortName
        {
            get
            {
                return this.ShortNameField;
            }
            set
            {
                this.ShortNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Data.Linq.Binary Symbol
        {
            get
            {
                return this.SymbolField;
            }
            set
            {
                this.SymbolField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "Log_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class Log_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string ExceptionMessageField;

        private string HostnameField;

        private string LevelField;

        private string MessageField;

        private string MethodnameField;

        private int ModuleIdField;

        private System.Nullable<System.Guid> PersonIdField;

        private string StackTraceField;

        private System.Nullable<int> ThreadField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ExceptionMessage
        {
            get
            {
                return this.ExceptionMessageField;
            }
            set
            {
                this.ExceptionMessageField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Hostname
        {
            get
            {
                return this.HostnameField;
            }
            set
            {
                this.HostnameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Level
        {
            get
            {
                return this.LevelField;
            }
            set
            {
                this.LevelField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message
        {
            get
            {
                return this.MessageField;
            }
            set
            {
                this.MessageField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Methodname
        {
            get
            {
                return this.MethodnameField;
            }
            set
            {
                this.MethodnameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ModuleId
        {
            get
            {
                return this.ModuleIdField;
            }
            set
            {
                this.ModuleIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.Guid> PersonId
        {
            get
            {
                return this.PersonIdField;
            }
            set
            {
                this.PersonIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StackTrace
        {
            get
            {
                return this.StackTraceField;
            }
            set
            {
                this.StackTraceField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> Thread
        {
            get
            {
                return this.ThreadField;
            }
            set
            {
                this.ThreadField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "Cashpoint_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class Cashpoint_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string BleSignatureField;

        private System.Nullable<int> CashpointNumberField;

        private string CashpointTypeNameField;

        private string CashpointTypeShortNameField;

        private string HostnameField;

        private System.Guid IDField;

        private string IPField;

        private System.Nullable<System.DateTime> LastLogonDateField;

        private string MACField;

        private string NameField;

        private string RestaurantNameField;

        private string ShortNameField;

        private string SubnetField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BleSignature
        {
            get
            {
                return this.BleSignatureField;
            }
            set
            {
                this.BleSignatureField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> CashpointNumber
        {
            get
            {
                return this.CashpointNumberField;
            }
            set
            {
                this.CashpointNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CashpointTypeName
        {
            get
            {
                return this.CashpointTypeNameField;
            }
            set
            {
                this.CashpointTypeNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CashpointTypeShortName
        {
            get
            {
                return this.CashpointTypeShortNameField;
            }
            set
            {
                this.CashpointTypeShortNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Hostname
        {
            get
            {
                return this.HostnameField;
            }
            set
            {
                this.HostnameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid ID
        {
            get
            {
                return this.IDField;
            }
            set
            {
                this.IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string IP
        {
            get
            {
                return this.IPField;
            }
            set
            {
                this.IPField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> LastLogonDate
        {
            get
            {
                return this.LastLogonDateField;
            }
            set
            {
                this.LastLogonDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MAC
        {
            get
            {
                return this.MACField;
            }
            set
            {
                this.MACField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RestaurantName
        {
            get
            {
                return this.RestaurantNameField;
            }
            set
            {
                this.RestaurantNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ShortName
        {
            get
            {
                return this.ShortNameField;
            }
            set
            {
                this.ShortNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Subnet
        {
            get
            {
                return this.SubnetField;
            }
            set
            {
                this.SubnetField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "Branding_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class Branding_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private decimal ButtonRadiusField;

        private decimal ButtonShadowField;

        private VentoClient.BrandingColor_DTO[] ColorsField;

        private VentoClient.BrandingIcon_DTO[] IconsField;

        private VentoClient.BrandingImage_DTO[] ImagesField;

        private System.Guid RestaurantIdField;

        private VentoClient.BrandingSize_DTO[] SizesField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ButtonRadius
        {
            get
            {
                return this.ButtonRadiusField;
            }
            set
            {
                this.ButtonRadiusField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ButtonShadow
        {
            get
            {
                return this.ButtonShadowField;
            }
            set
            {
                this.ButtonShadowField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public VentoClient.BrandingColor_DTO[] Colors
        {
            get
            {
                return this.ColorsField;
            }
            set
            {
                this.ColorsField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public VentoClient.BrandingIcon_DTO[] Icons
        {
            get
            {
                return this.IconsField;
            }
            set
            {
                this.IconsField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public VentoClient.BrandingImage_DTO[] Images
        {
            get
            {
                return this.ImagesField;
            }
            set
            {
                this.ImagesField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid RestaurantId
        {
            get
            {
                return this.RestaurantIdField;
            }
            set
            {
                this.RestaurantIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public VentoClient.BrandingSize_DTO[] Sizes
        {
            get
            {
                return this.SizesField;
            }
            set
            {
                this.SizesField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "BrandingColor_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class BrandingColor_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string HexValueField;

        private int IDField;

        private string KeyField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string HexValue
        {
            get
            {
                return this.HexValueField;
            }
            set
            {
                this.HexValueField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID
        {
            get
            {
                return this.IDField;
            }
            set
            {
                this.IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Key
        {
            get
            {
                return this.KeyField;
            }
            set
            {
                this.KeyField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "BrandingIcon_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class BrandingIcon_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private int IDField;

        private string KeyField;

        private System.Nullable<System.DateTime> LastModificationDateField;

        private string UrlField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID
        {
            get
            {
                return this.IDField;
            }
            set
            {
                this.IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Key
        {
            get
            {
                return this.KeyField;
            }
            set
            {
                this.KeyField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> LastModificationDate
        {
            get
            {
                return this.LastModificationDateField;
            }
            set
            {
                this.LastModificationDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Url
        {
            get
            {
                return this.UrlField;
            }
            set
            {
                this.UrlField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "BrandingImage_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class BrandingImage_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private int IDField;

        private string KeyField;

        private System.Nullable<System.DateTime> LastModificationDateField;

        private string UrlField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID
        {
            get
            {
                return this.IDField;
            }
            set
            {
                this.IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Key
        {
            get
            {
                return this.KeyField;
            }
            set
            {
                this.KeyField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> LastModificationDate
        {
            get
            {
                return this.LastModificationDateField;
            }
            set
            {
                this.LastModificationDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Url
        {
            get
            {
                return this.UrlField;
            }
            set
            {
                this.UrlField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "BrandingSize_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class BrandingSize_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string KeyField;

        private decimal SizeField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Key
        {
            get
            {
                return this.KeyField;
            }
            set
            {
                this.KeyField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Size
        {
            get
            {
                return this.SizeField;
            }
            set
            {
                this.SizeField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "MMSERROR_CS", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public enum MMSERROR_CS : int
    {

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_NONE = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_RESTAURANT_NOT_FOUND = 1,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_OTHER = 2,
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "RatingValue_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public partial class RatingValue_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private int ArticleNumberField;

        private System.Nullable<decimal> AverageRatingValueField;

        private System.Nullable<int> MaxRatingValueField;

        private System.Nullable<int> MinRatingValueField;

        private System.Nullable<int> RatingCountField;

        private System.Guid RestaurantIdField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ArticleNumber
        {
            get
            {
                return this.ArticleNumberField;
            }
            set
            {
                this.ArticleNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> AverageRatingValue
        {
            get
            {
                return this.AverageRatingValueField;
            }
            set
            {
                this.AverageRatingValueField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> MaxRatingValue
        {
            get
            {
                return this.MaxRatingValueField;
            }
            set
            {
                this.MaxRatingValueField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> MinRatingValue
        {
            get
            {
                return this.MinRatingValueField;
            }
            set
            {
                this.MinRatingValueField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> RatingCount
        {
            get
            {
                return this.RatingCountField;
            }
            set
            {
                this.RatingCountField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid RestaurantId
        {
            get
            {
                return this.RestaurantIdField;
            }
            set
            {
                this.RestaurantIdField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "MASTERDATAERROR_CS", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.common")]
    public enum MASTERDATAERROR_CS : int
    {

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_NONE = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_RESTAURANT_NOT_FOUND = 1,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_OTHER = 2,
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName = "IVentopayCardService")]
public interface IVentopayCardService
{

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayCardService/CardTopUp", ReplyAction = "http://tempuri.org/IVentopayCardService/CardTopUpResponse")]
    VentoClient.CardStatusError_CS CardTopUp(int valueInCent, ulong personNumber, VentoClient.PaymentType_CS paymentType, System.Nullable<bool> ignoreMaximumSaldo);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayCardService/CardTopUp", ReplyAction = "http://tempuri.org/IVentopayCardService/CardTopUpResponse")]
    System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> CardTopUpAsync(int valueInCent, ulong personNumber, VentoClient.PaymentType_CS paymentType, System.Nullable<bool> ignoreMaximumSaldo);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayCardService/CardTopUpNew", ReplyAction = "http://tempuri.org/IVentopayCardService/CardTopUpNewResponse")]
    VentoClient.CardStatusError_CS CardTopUpNew(int valueInCent, ulong personNumber, VentoClient.PaymentType_CS paymentType, System.Guid restaurantId, System.Nullable<bool> ignoreMaximumSaldo);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayCardService/CardTopUpNew", ReplyAction = "http://tempuri.org/IVentopayCardService/CardTopUpNewResponse")]
    System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> CardTopUpNewAsync(int valueInCent, ulong personNumber, VentoClient.PaymentType_CS paymentType, System.Guid restaurantId, System.Nullable<bool> ignoreMaximumSaldo);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayCardService/GetBonlinesByBonNumber", ReplyAction = "http://tempuri.org/IVentopayCardService/GetBonlinesByBonNumberResponse")]
    GetBonlinesByBonNumberResponse GetBonlinesByBonNumber(GetBonlinesByBonNumberRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayCardService/GetBonlinesByBonNumber", ReplyAction = "http://tempuri.org/IVentopayCardService/GetBonlinesByBonNumberResponse")]
    System.Threading.Tasks.Task<GetBonlinesByBonNumberResponse> GetBonlinesByBonNumberAsync(GetBonlinesByBonNumberRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayCardService/GetBonsByPerson", ReplyAction = "http://tempuri.org/IVentopayCardService/GetBonsByPersonResponse")]
    GetBonsByPersonResponse GetBonsByPerson(GetBonsByPersonRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayCardService/GetBonsByPerson", ReplyAction = "http://tempuri.org/IVentopayCardService/GetBonsByPersonResponse")]
    System.Threading.Tasks.Task<GetBonsByPersonResponse> GetBonsByPersonAsync(GetBonsByPersonRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayCardService/GetLimtsByPerson", ReplyAction = "http://tempuri.org/IVentopayCardService/GetLimtsByPersonResponse")]
    GetLimtsByPersonResponse GetLimtsByPerson(GetLimtsByPersonRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayCardService/GetLimtsByPerson", ReplyAction = "http://tempuri.org/IVentopayCardService/GetLimtsByPersonResponse")]
    System.Threading.Tasks.Task<GetLimtsByPersonResponse> GetLimtsByPersonAsync(GetLimtsByPersonRequest request);
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "GetBonlinesByBonNumber", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class GetBonlinesByBonNumberRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string bonNumber;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string languageShortName;

    public GetBonlinesByBonNumberRequest()
    {
    }

    public GetBonlinesByBonNumberRequest(string bonNumber, string languageShortName)
    {
        this.bonNumber = bonNumber;
        this.languageShortName = languageShortName;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "GetBonlinesByBonNumberResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class GetBonlinesByBonNumberResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS GetBonlinesByBonNumberResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.Bonline_CS[] bonLines;

    public GetBonlinesByBonNumberResponse()
    {
    }

    public GetBonlinesByBonNumberResponse(VentoClient.CardStatusError_CS GetBonlinesByBonNumberResult, VentoClient.Bonline_CS[] bonLines)
    {
        this.GetBonlinesByBonNumberResult = GetBonlinesByBonNumberResult;
        this.bonLines = bonLines;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "GetBonsByPerson", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class GetBonsByPersonRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public ulong personNumber;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public System.DateTime fromDate;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public System.DateTime untilDate;

    public GetBonsByPersonRequest()
    {
    }

    public GetBonsByPersonRequest(ulong personNumber, System.DateTime fromDate, System.DateTime untilDate)
    {
        this.personNumber = personNumber;
        this.fromDate = fromDate;
        this.untilDate = untilDate;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "GetBonsByPersonResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class GetBonsByPersonResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS GetBonsByPersonResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.Bon_CS[] bons;

    public GetBonsByPersonResponse()
    {
    }

    public GetBonsByPersonResponse(VentoClient.CardStatusError_CS GetBonsByPersonResult, VentoClient.Bon_CS[] bons)
    {
        this.GetBonsByPersonResult = GetBonsByPersonResult;
        this.bons = bons;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "GetLimtsByPerson", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class GetLimtsByPersonRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public ulong personNumber;

    public GetLimtsByPersonRequest()
    {
    }

    public GetLimtsByPersonRequest(ulong personNumber)
    {
        this.personNumber = personNumber;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "GetLimtsByPersonResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class GetLimtsByPersonResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS GetLimtsByPersonResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public int remainingMonthSumInCent;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public int maximumLoadValueInCent;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 3)]
    public int currentSaldoInCent;

    public GetLimtsByPersonResponse()
    {
    }

    public GetLimtsByPersonResponse(VentoClient.CardStatusError_CS GetLimtsByPersonResult, int remainingMonthSumInCent, int maximumLoadValueInCent, int currentSaldoInCent)
    {
        this.GetLimtsByPersonResult = GetLimtsByPersonResult;
        this.remainingMonthSumInCent = remainingMonthSumInCent;
        this.maximumLoadValueInCent = maximumLoadValueInCent;
        this.currentSaldoInCent = currentSaldoInCent;
    }
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface IVentopayCardServiceChannel : IVentopayCardService, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class VentopayCardServiceClient : System.ServiceModel.ClientBase<IVentopayCardService>, IVentopayCardService
{

    public VentopayCardServiceClient()
    {
    }

    public VentopayCardServiceClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
    {
    }

    public VentopayCardServiceClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
    {
    }

    public VentopayCardServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
    {
    }

    public VentopayCardServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
    {
    }

    public VentoClient.CardStatusError_CS CardTopUp(int valueInCent, ulong personNumber, VentoClient.PaymentType_CS paymentType, System.Nullable<bool> ignoreMaximumSaldo)
    {
        return base.Channel.CardTopUp(valueInCent, personNumber, paymentType, ignoreMaximumSaldo);
    }

    public System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> CardTopUpAsync(int valueInCent, ulong personNumber, VentoClient.PaymentType_CS paymentType, System.Nullable<bool> ignoreMaximumSaldo)
    {
        return base.Channel.CardTopUpAsync(valueInCent, personNumber, paymentType, ignoreMaximumSaldo);
    }

    public VentoClient.CardStatusError_CS CardTopUpNew(int valueInCent, ulong personNumber, VentoClient.PaymentType_CS paymentType, System.Guid restaurantId, System.Nullable<bool> ignoreMaximumSaldo)
    {
        return base.Channel.CardTopUpNew(valueInCent, personNumber, paymentType, restaurantId, ignoreMaximumSaldo);
    }

    public System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> CardTopUpNewAsync(int valueInCent, ulong personNumber, VentoClient.PaymentType_CS paymentType, System.Guid restaurantId, System.Nullable<bool> ignoreMaximumSaldo)
    {
        return base.Channel.CardTopUpNewAsync(valueInCent, personNumber, paymentType, restaurantId, ignoreMaximumSaldo);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    GetBonlinesByBonNumberResponse IVentopayCardService.GetBonlinesByBonNumber(GetBonlinesByBonNumberRequest request)
    {
        return base.Channel.GetBonlinesByBonNumber(request);
    }

    public VentoClient.CardStatusError_CS GetBonlinesByBonNumber(string bonNumber, string languageShortName, out VentoClient.Bonline_CS[] bonLines)
    {
        GetBonlinesByBonNumberRequest inValue = new GetBonlinesByBonNumberRequest();
        inValue.bonNumber = bonNumber;
        inValue.languageShortName = languageShortName;
        GetBonlinesByBonNumberResponse retVal = ((IVentopayCardService)(this)).GetBonlinesByBonNumber(inValue);
        bonLines = retVal.bonLines;
        return retVal.GetBonlinesByBonNumberResult;
    }

    public System.Threading.Tasks.Task<GetBonlinesByBonNumberResponse> GetBonlinesByBonNumberAsync(GetBonlinesByBonNumberRequest request)
    {
        return base.Channel.GetBonlinesByBonNumberAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    GetBonsByPersonResponse IVentopayCardService.GetBonsByPerson(GetBonsByPersonRequest request)
    {
        return base.Channel.GetBonsByPerson(request);
    }

    public VentoClient.CardStatusError_CS GetBonsByPerson(ulong personNumber, System.DateTime fromDate, System.DateTime untilDate, out VentoClient.Bon_CS[] bons)
    {
        GetBonsByPersonRequest inValue = new GetBonsByPersonRequest();
        inValue.personNumber = personNumber;
        inValue.fromDate = fromDate;
        inValue.untilDate = untilDate;
        GetBonsByPersonResponse retVal = ((IVentopayCardService)(this)).GetBonsByPerson(inValue);
        bons = retVal.bons;
        return retVal.GetBonsByPersonResult;
    }

    public System.Threading.Tasks.Task<GetBonsByPersonResponse> GetBonsByPersonAsync(GetBonsByPersonRequest request)
    {
        return base.Channel.GetBonsByPersonAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    GetLimtsByPersonResponse IVentopayCardService.GetLimtsByPerson(GetLimtsByPersonRequest request)
    {
        return base.Channel.GetLimtsByPerson(request);
    }

    public VentoClient.CardStatusError_CS GetLimtsByPerson(ulong personNumber, out int remainingMonthSumInCent, out int maximumLoadValueInCent, out int currentSaldoInCent)
    {
        GetLimtsByPersonRequest inValue = new GetLimtsByPersonRequest();
        inValue.personNumber = personNumber;
        GetLimtsByPersonResponse retVal = ((IVentopayCardService)(this)).GetLimtsByPerson(inValue);
        remainingMonthSumInCent = retVal.remainingMonthSumInCent;
        maximumLoadValueInCent = retVal.maximumLoadValueInCent;
        currentSaldoInCent = retVal.currentSaldoInCent;
        return retVal.GetLimtsByPersonResult;
    }

    public System.Threading.Tasks.Task<GetLimtsByPersonResponse> GetLimtsByPersonAsync(GetLimtsByPersonRequest request)
    {
        return base.Channel.GetLimtsByPersonAsync(request);
    }
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName = "IVentopayMobileService")]
public interface IVentopayMobileService
{

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_CardTopUp", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_CardTopUpResponse")]
    VentoClient.CardStatusError_CS VpMob_CardTopUp(string restaurantId, int valueInCent, string cardId, VentoClient.PaymentType_CS paymentType, string description, System.Nullable<bool> ignoreMaximumSaldo);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_CardTopUp", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_CardTopUpResponse")]
    System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_CardTopUpAsync(string restaurantId, int valueInCent, string cardId, VentoClient.PaymentType_CS paymentType, string description, System.Nullable<bool> ignoreMaximumSaldo);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetBonlinesByBonNumber", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetBonlinesByBonNumberResponse")]
    VpMob_GetBonlinesByBonNumberResponse VpMob_GetBonlinesByBonNumber(VpMob_GetBonlinesByBonNumberRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetBonlinesByBonNumber", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetBonlinesByBonNumberResponse")]
    System.Threading.Tasks.Task<VpMob_GetBonlinesByBonNumberResponse> VpMob_GetBonlinesByBonNumberAsync(VpMob_GetBonlinesByBonNumberRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetBonsByPerson", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetBonsByPersonResponse")]
    VpMob_GetBonsByPersonResponse VpMob_GetBonsByPerson(VpMob_GetBonsByPersonRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetBonsByPerson", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetBonsByPersonResponse")]
    System.Threading.Tasks.Task<VpMob_GetBonsByPersonResponse> VpMob_GetBonsByPersonAsync(VpMob_GetBonsByPersonRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetLastBonByPerson", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetLastBonByPersonResponse")]
    VpMob_GetLastBonByPersonResponse VpMob_GetLastBonByPerson(VpMob_GetLastBonByPersonRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetLastBonByPerson", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetLastBonByPersonResponse")]
    System.Threading.Tasks.Task<VpMob_GetLastBonByPersonResponse> VpMob_GetLastBonByPersonAsync(VpMob_GetLastBonByPersonRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetLimtsByCard", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetLimtsByCardResponse")]
    VpMob_GetLimtsByCardResponse VpMob_GetLimtsByCard(VpMob_GetLimtsByCardRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetLimtsByCard", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetLimtsByCardResponse")]
    System.Threading.Tasks.Task<VpMob_GetLimtsByCardResponse> VpMob_GetLimtsByCardAsync(VpMob_GetLimtsByCardRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetBonusLevelForCard", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetBonusLevelForCardResponse")]
    VpMob_GetBonusLevelForCardResponse VpMob_GetBonusLevelForCard(VpMob_GetBonusLevelForCardRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetBonusLevelForCard", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetBonusLevelForCardResponse")]
    System.Threading.Tasks.Task<VpMob_GetBonusLevelForCardResponse> VpMob_GetBonusLevelForCardAsync(VpMob_GetBonusLevelForCardRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_ActivateBonus", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_ActivateBonusResponse")]
    VentoClient.CardStatusError_CS VpMob_ActivateBonus(string restaurantId, string syncId, string cardId, string bonusLevel_Id, System.DateTime activationDate, System.Nullable<decimal> remainingPoints);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_ActivateBonus", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_ActivateBonusResponse")]
    System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_ActivateBonusAsync(string restaurantId, string syncId, string cardId, string bonusLevel_Id, System.DateTime activationDate, System.Nullable<decimal> remainingPoints);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetRestaurant", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetRestaurantResponse")]
    VpMob_GetRestaurantResponse VpMob_GetRestaurant(VpMob_GetRestaurantRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetRestaurant", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetRestaurantResponse")]
    System.Threading.Tasks.Task<VpMob_GetRestaurantResponse> VpMob_GetRestaurantAsync(VpMob_GetRestaurantRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetNewsFeed", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetNewsFeedResponse")]
    VpMob_GetNewsFeedResponse VpMob_GetNewsFeed(VpMob_GetNewsFeedRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetNewsFeed", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetNewsFeedResponse")]
    System.Threading.Tasks.Task<VpMob_GetNewsFeedResponse> VpMob_GetNewsFeedAsync(VpMob_GetNewsFeedRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_UpdatePersonData", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_UpdatePersonDataResponse")]
    VentoClient.CardStatusError_CS VpMob_UpdatePersonData(string restaurantId, VentoClient.PersonInformation_DTO personInformationDto);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_UpdatePersonData", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_UpdatePersonDataResponse")]
    System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_UpdatePersonDataAsync(string restaurantId, VentoClient.PersonInformation_DTO personInformationDto);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetPersonInformation", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetPersonInformationResponse")]
    VpMob_GetPersonInformationResponse VpMob_GetPersonInformation(VpMob_GetPersonInformationRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetPersonInformation", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetPersonInformationResponse")]
    System.Threading.Tasks.Task<VpMob_GetPersonInformationResponse> VpMob_GetPersonInformationAsync(VpMob_GetPersonInformationRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_Login", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_LoginResponse")]
    VpMob_LoginResponse VpMob_Login(VpMob_LoginRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_Login", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_LoginResponse")]
    System.Threading.Tasks.Task<VpMob_LoginResponse> VpMob_LoginAsync(VpMob_LoginRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_ChangePassword", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_ChangePasswordResponse")]
    VentoClient.CardStatusError_CS VpMob_ChangePassword(string restaurantId, string personId, string UserName, string oldPasswordHash, string newPasswordHash);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_ChangePassword", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_ChangePasswordResponse")]
    System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_ChangePasswordAsync(string restaurantId, string personId, string UserName, string oldPasswordHash, string newPasswordHash);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetMenu", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetMenuResponse")]
    VpMob_GetMenuResponse VpMob_GetMenu(VpMob_GetMenuRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetMenu", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetMenuResponse")]
    System.Threading.Tasks.Task<VpMob_GetMenuResponse> VpMob_GetMenuAsync(VpMob_GetMenuRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_IsAlive", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_IsAliveResponse")]
    bool VpMob_IsAlive();

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_IsAlive", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_IsAliveResponse")]
    System.Threading.Tasks.Task<bool> VpMob_IsAliveAsync();

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_LoginByCardNumber", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_LoginByCardNumberResponse")]
    VpMob_LoginByCardNumberResponse VpMob_LoginByCardNumber(VpMob_LoginByCardNumberRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_LoginByCardNumber", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_LoginByCardNumberResponse")]
    System.Threading.Tasks.Task<VpMob_LoginByCardNumberResponse> VpMob_LoginByCardNumberAsync(VpMob_LoginByCardNumberRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetAllPersonInformation", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetAllPersonInformationResponse")]
    VpMob_GetAllPersonInformationResponse VpMob_GetAllPersonInformation(VpMob_GetAllPersonInformationRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetAllPersonInformation", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetAllPersonInformationResponse")]
    System.Threading.Tasks.Task<VpMob_GetAllPersonInformationResponse> VpMob_GetAllPersonInformationAsync(VpMob_GetAllPersonInformationRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetApplicationSetting", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetApplicationSettingResponse")]
    VpMob_GetApplicationSettingResponse VpMob_GetApplicationSetting(VpMob_GetApplicationSettingRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetApplicationSetting", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetApplicationSettingResponse")]
    System.Threading.Tasks.Task<VpMob_GetApplicationSettingResponse> VpMob_GetApplicationSettingAsync(VpMob_GetApplicationSettingRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetAllCustomergroupInformation", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetAllCustomergroupInformationRes" +
        "ponse")]
    VpMob_GetAllCustomergroupInformationResponse VpMob_GetAllCustomergroupInformation(VpMob_GetAllCustomergroupInformationRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetAllCustomergroupInformation", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetAllCustomergroupInformationRes" +
        "ponse")]
    System.Threading.Tasks.Task<VpMob_GetAllCustomergroupInformationResponse> VpMob_GetAllCustomergroupInformationAsync(VpMob_GetAllCustomergroupInformationRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_ResetPassword", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_ResetPasswordResponse")]
    VentoClient.CardStatusError_CS VpMob_ResetPassword(string restaurantId, string mailAddress, string userName);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_ResetPassword", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_ResetPasswordResponse")]
    System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_ResetPasswordAsync(string restaurantId, string mailAddress, string userName);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetNutrientInformation", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetNutrientInformationResponse")]
    VpMob_GetNutrientInformationResponse VpMob_GetNutrientInformation(VpMob_GetNutrientInformationRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetNutrientInformation", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetNutrientInformationResponse")]
    System.Threading.Tasks.Task<VpMob_GetNutrientInformationResponse> VpMob_GetNutrientInformationAsync(VpMob_GetNutrientInformationRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetBonlinesByPerson", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetBonlinesByPersonResponse")]
    VpMob_GetBonlinesByPersonResponse VpMob_GetBonlinesByPerson(VpMob_GetBonlinesByPersonRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetBonlinesByPerson", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetBonlinesByPersonResponse")]
    System.Threading.Tasks.Task<VpMob_GetBonlinesByPersonResponse> VpMob_GetBonlinesByPersonAsync(VpMob_GetBonlinesByPersonRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetContactInformation", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetContactInformationResponse")]
    VpMob_GetContactInformationResponse VpMob_GetContactInformation(VpMob_GetContactInformationRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetContactInformation", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetContactInformationResponse")]
    System.Threading.Tasks.Task<VpMob_GetContactInformationResponse> VpMob_GetContactInformationAsync(VpMob_GetContactInformationRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_SendMessage", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_SendMessageResponse")]
    VentoClient.CardStatusError_CS VpMob_SendMessage(string restaurantId, int contactInformationId, string subject, string message, string replyEmailAddress, string replyTelephoneNumber, string personId);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_SendMessage", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_SendMessageResponse")]
    System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_SendMessageAsync(string restaurantId, int contactInformationId, string subject, string message, string replyEmailAddress, string replyTelephoneNumber, string personId);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetActivityLevels", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetActivityLevelsResponse")]
    VpMob_GetActivityLevelsResponse VpMob_GetActivityLevels(VpMob_GetActivityLevelsRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetActivityLevels", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetActivityLevelsResponse")]
    System.Threading.Tasks.Task<VpMob_GetActivityLevelsResponse> VpMob_GetActivityLevelsAsync(VpMob_GetActivityLevelsRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetTopUps", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetTopUpsResponse")]
    VpMob_GetTopUpsResponse VpMob_GetTopUps(VpMob_GetTopUpsRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetTopUps", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetTopUpsResponse")]
    System.Threading.Tasks.Task<VpMob_GetTopUpsResponse> VpMob_GetTopUpsAsync(VpMob_GetTopUpsRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetPersonPicture", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetPersonPictureResponse")]
    VpMob_GetPersonPictureResponse VpMob_GetPersonPicture(VpMob_GetPersonPictureRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetPersonPicture", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetPersonPictureResponse")]
    System.Threading.Tasks.Task<VpMob_GetPersonPictureResponse> VpMob_GetPersonPictureAsync(VpMob_GetPersonPictureRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_SetPersonPicture", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_SetPersonPictureResponse")]
    VentoClient.CardStatusError_CS VpMob_SetPersonPicture(string restaurantId, string personId, System.Data.Linq.Binary picture);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_SetPersonPicture", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_SetPersonPictureResponse")]
    System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_SetPersonPictureAsync(string restaurantId, string personId, System.Data.Linq.Binary picture);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetAllergens", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetAllergensResponse")]
    VpMob_GetAllergensResponse VpMob_GetAllergens(VpMob_GetAllergensRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetAllergens", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetAllergensResponse")]
    System.Threading.Tasks.Task<VpMob_GetAllergensResponse> VpMob_GetAllergensAsync(VpMob_GetAllergensRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetAllergenLegend", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetAllergenLegendResponse")]
    VpMob_GetAllergenLegendResponse VpMob_GetAllergenLegend(VpMob_GetAllergenLegendRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetAllergenLegend", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetAllergenLegendResponse")]
    System.Threading.Tasks.Task<VpMob_GetAllergenLegendResponse> VpMob_GetAllergenLegendAsync(VpMob_GetAllergenLegendRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetFeedbackProgram", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetFeedbackProgramResponse")]
    VpMob_GetFeedbackProgramResponse VpMob_GetFeedbackProgram(VpMob_GetFeedbackProgramRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetFeedbackProgram", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetFeedbackProgramResponse")]
    System.Threading.Tasks.Task<VpMob_GetFeedbackProgramResponse> VpMob_GetFeedbackProgramAsync(VpMob_GetFeedbackProgramRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_SetFeedbackAnswer", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_SetFeedbackAnswerResponse")]
    VentoClient.CardStatusError_CS VpMob_SetFeedbackAnswer(string restaurantId, string cardId, VentoClient.FeedbackPersonAnswer_DTO[] answer);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_SetFeedbackAnswer", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_SetFeedbackAnswerResponse")]
    System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_SetFeedbackAnswerAsync(string restaurantId, string cardId, VentoClient.FeedbackPersonAnswer_DTO[] answer);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_MergeAccounts", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_MergeAccountsResponse")]
    VpMob_MergeAccountsResponse VpMob_MergeAccounts(VpMob_MergeAccountsRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_MergeAccounts", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_MergeAccountsResponse")]
    System.Threading.Tasks.Task<VpMob_MergeAccountsResponse> VpMob_MergeAccountsAsync(VpMob_MergeAccountsRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_ResetPersonToStandardGroup", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_ResetPersonToStandardGroupRespons" +
        "e")]
    VentoClient.CardStatusError_CS VpMob_ResetPersonToStandardGroup(string restaurantId, string personId);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_ResetPersonToStandardGroup", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_ResetPersonToStandardGroupRespons" +
        "e")]
    System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_ResetPersonToStandardGroupAsync(string restaurantId, string personId);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_UpdateBonline", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_UpdateBonlineResponse")]
    VentoClient.CardStatusError_CS VpMob_UpdateBonline(string restaurantId, VentoClient.Bonline_DTO bonline);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_UpdateBonline", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_UpdateBonlineResponse")]
    System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_UpdateBonlineAsync(string restaurantId, VentoClient.Bonline_DTO bonline);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetText", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetTextResponse")]
    VpMob_GetTextResponse VpMob_GetText(VpMob_GetTextRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetText", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetTextResponse")]
    System.Threading.Tasks.Task<VpMob_GetTextResponse> VpMob_GetTextAsync(VpMob_GetTextRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_ActivateNewsFeed", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_ActivateNewsFeedResponse")]
    VentoClient.CardStatusError_CS VpMob_ActivateNewsFeed(string restaurantId, int newsFeedId, string personId);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_ActivateNewsFeed", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_ActivateNewsFeedResponse")]
    System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_ActivateNewsFeedAsync(string restaurantId, int newsFeedId, string personId);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_ChangeTermsAcceptanceState", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_ChangeTermsAcceptanceStateRespons" +
        "e")]
    VentoClient.CardStatusError_CS VpMob_ChangeTermsAcceptanceState(string restaurantId, string personId, System.DateTime deviceDate, string termShortName, bool isAccepted);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_ChangeTermsAcceptanceState", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_ChangeTermsAcceptanceStateRespons" +
        "e")]
    System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_ChangeTermsAcceptanceStateAsync(string restaurantId, string personId, System.DateTime deviceDate, string termShortName, bool isAccepted);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetLastTermStatesByPerson", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetLastTermStatesByPersonResponse" +
        "")]
    VpMob_GetLastTermStatesByPersonResponse VpMob_GetLastTermStatesByPerson(VpMob_GetLastTermStatesByPersonRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetLastTermStatesByPerson", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetLastTermStatesByPersonResponse" +
        "")]
    System.Threading.Tasks.Task<VpMob_GetLastTermStatesByPersonResponse> VpMob_GetLastTermStatesByPersonAsync(VpMob_GetLastTermStatesByPersonRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_LoginByPersonNumber", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_LoginByPersonNumberResponse")]
    VpMob_LoginByPersonNumberResponse VpMob_LoginByPersonNumber(VpMob_LoginByPersonNumberRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_LoginByPersonNumber", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_LoginByPersonNumberResponse")]
    System.Threading.Tasks.Task<VpMob_LoginByPersonNumberResponse> VpMob_LoginByPersonNumberAsync(VpMob_LoginByPersonNumberRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetAvailableLanguages", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetAvailableLanguagesResponse")]
    VpMob_GetAvailableLanguagesResponse VpMob_GetAvailableLanguages(VpMob_GetAvailableLanguagesRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetAvailableLanguages", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetAvailableLanguagesResponse")]
    System.Threading.Tasks.Task<VpMob_GetAvailableLanguagesResponse> VpMob_GetAvailableLanguagesAsync(VpMob_GetAvailableLanguagesRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_ResendMailActivation", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_ResendMailActivationResponse")]
    VentoClient.CardStatusError_CS VpMob_ResendMailActivation(string restaurantId, string personId);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_ResendMailActivation", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_ResendMailActivationResponse")]
    System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_ResendMailActivationAsync(string restaurantId, string personId);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_LoginByAccountDetail", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_LoginByAccountDetailResponse")]
    VpMob_LoginByAccountDetailResponse VpMob_LoginByAccountDetail(VpMob_LoginByAccountDetailRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_LoginByAccountDetail", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_LoginByAccountDetailResponse")]
    System.Threading.Tasks.Task<VpMob_LoginByAccountDetailResponse> VpMob_LoginByAccountDetailAsync(VpMob_LoginByAccountDetailRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetBonlinesByPersonWithExcludedCa" +
        "shpoints", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetBonlinesByPersonWithExcludedCa" +
        "shpointsResponse")]
    VpMob_GetBonlinesByPersonWithExcludedCashpointsResponse VpMob_GetBonlinesByPersonWithExcludedCashpoints(VpMob_GetBonlinesByPersonWithExcludedCashpointsRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetBonlinesByPersonWithExcludedCa" +
        "shpoints", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetBonlinesByPersonWithExcludedCa" +
        "shpointsResponse")]
    System.Threading.Tasks.Task<VpMob_GetBonlinesByPersonWithExcludedCashpointsResponse> VpMob_GetBonlinesByPersonWithExcludedCashpointsAsync(VpMob_GetBonlinesByPersonWithExcludedCashpointsRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_ChangePin", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_ChangePinResponse")]
    VentoClient.CardStatusError_CS VpMob_ChangePin(string restaurantId, string personId, string newPin);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_ChangePin", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_ChangePinResponse")]
    System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_ChangePinAsync(string restaurantId, string personId, string newPin);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_Log", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_LogResponse")]
    VentoClient.CardStatusError_CS VpMob_Log(string restaurantId, VentoClient.Log_DTO logDto);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_Log", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_LogResponse")]
    System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_LogAsync(string restaurantId, VentoClient.Log_DTO logDto);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_PayoutSaldo", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_PayoutSaldoResponse")]
    VentoClient.CardStatusError_CS VpMob_PayoutSaldo(string restaurantId, string cardId, string iban, string bic, string lastname, int saldoInCent);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_PayoutSaldo", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_PayoutSaldoResponse")]
    System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_PayoutSaldoAsync(string restaurantId, string cardId, string iban, string bic, string lastname, int saldoInCent);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_DoPayment", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_DoPaymentResponse")]
    VpMob_DoPaymentResponse VpMob_DoPayment(VpMob_DoPaymentRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_DoPayment", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_DoPaymentResponse")]
    System.Threading.Tasks.Task<VpMob_DoPaymentResponse> VpMob_DoPaymentAsync(VpMob_DoPaymentRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_DoMobilePayment", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_DoMobilePaymentResponse")]
    VpMob_DoMobilePaymentResponse VpMob_DoMobilePayment(VpMob_DoMobilePaymentRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_DoMobilePayment", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_DoMobilePaymentResponse")]
    System.Threading.Tasks.Task<VpMob_DoMobilePaymentResponse> VpMob_DoMobilePaymentAsync(VpMob_DoMobilePaymentRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_DoAuthorization", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_DoAuthorizationResponse")]
    VpMob_DoAuthorizationResponse VpMob_DoAuthorization(VpMob_DoAuthorizationRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_DoAuthorization", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_DoAuthorizationResponse")]
    System.Threading.Tasks.Task<VpMob_DoAuthorizationResponse> VpMob_DoAuthorizationAsync(VpMob_DoAuthorizationRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_RegisterUser", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_RegisterUserResponse")]
    VentoClient.CardStatusError_CS VpMob_RegisterUser(string restaurantId, VentoClient.PersonInformation_DTO personInformationDto, System.Data.Linq.Binary personPicture);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_RegisterUser", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_RegisterUserResponse")]
    System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_RegisterUserAsync(string restaurantId, VentoClient.PersonInformation_DTO personInformationDto, System.Data.Linq.Binary personPicture);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetReportData", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetReportDataResponse")]
    VpMob_GetReportDataResponse VpMob_GetReportData(VpMob_GetReportDataRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetReportData", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetReportDataResponse")]
    System.Threading.Tasks.Task<VpMob_GetReportDataResponse> VpMob_GetReportDataAsync(VpMob_GetReportDataRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_NotifyModule", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_NotifyModuleResponse")]
    VpMob_NotifyModuleResponse VpMob_NotifyModule(VpMob_NotifyModuleRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_NotifyModule", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_NotifyModuleResponse")]
    System.Threading.Tasks.Task<VpMob_NotifyModuleResponse> VpMob_NotifyModuleAsync(VpMob_NotifyModuleRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_NotifyNewsfeed", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_NotifyNewsfeedResponse")]
    VpMob_NotifyNewsfeedResponse VpMob_NotifyNewsfeed(VpMob_NotifyNewsfeedRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_NotifyNewsfeed", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_NotifyNewsfeedResponse")]
    System.Threading.Tasks.Task<VpMob_NotifyNewsfeedResponse> VpMob_NotifyNewsfeedAsync(VpMob_NotifyNewsfeedRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_NotifyFeedback", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_NotifyFeedbackResponse")]
    VpMob_NotifyFeedbackResponse VpMob_NotifyFeedback(VpMob_NotifyFeedbackRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_NotifyFeedback", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_NotifyFeedbackResponse")]
    System.Threading.Tasks.Task<VpMob_NotifyFeedbackResponse> VpMob_NotifyFeedbackAsync(VpMob_NotifyFeedbackRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetBleEnabledCashpoints", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetBleEnabledCashpointsResponse")]
    VpMob_GetBleEnabledCashpointsResponse VpMob_GetBleEnabledCashpoints(VpMob_GetBleEnabledCashpointsRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetBleEnabledCashpoints", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetBleEnabledCashpointsResponse")]
    System.Threading.Tasks.Task<VpMob_GetBleEnabledCashpointsResponse> VpMob_GetBleEnabledCashpointsAsync(VpMob_GetBleEnabledCashpointsRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_EnablePayment", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_EnablePaymentResponse")]
    VentoClient.CardStatusError_CS VpMob_EnablePayment(string restaurantId, string cardId, string cashpointId);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_EnablePayment", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_EnablePaymentResponse")]
    System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_EnablePaymentAsync(string restaurantId, string cardId, string cashpointId);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_CancelPayment", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_CancelPaymentResponse")]
    VentoClient.CardStatusError_CS VpMob_CancelPayment(string restaurantId, string cardId, string cashpointId);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_CancelPayment", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_CancelPaymentResponse")]
    System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_CancelPaymentAsync(string restaurantId, string cardId, string cashpointId);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_IsPaymentActive", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_IsPaymentActiveResponse")]
    VpMob_IsPaymentActiveResponse VpMob_IsPaymentActive(VpMob_IsPaymentActiveRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_IsPaymentActive", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_IsPaymentActiveResponse")]
    System.Threading.Tasks.Task<VpMob_IsPaymentActiveResponse> VpMob_IsPaymentActiveAsync(VpMob_IsPaymentActiveRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetBonByBonId", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetBonByBonIdResponse")]
    VpMob_GetBonByBonIdResponse VpMob_GetBonByBonId(VpMob_GetBonByBonIdRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetBonByBonId", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetBonByBonIdResponse")]
    System.Threading.Tasks.Task<VpMob_GetBonByBonIdResponse> VpMob_GetBonByBonIdAsync(VpMob_GetBonByBonIdRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetBranding", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetBrandingResponse")]
    VpMob_GetBrandingResponse VpMob_GetBranding(VpMob_GetBrandingRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMobileService/VpMob_GetBranding", ReplyAction = "http://tempuri.org/IVentopayMobileService/VpMob_GetBrandingResponse")]
    System.Threading.Tasks.Task<VpMob_GetBrandingResponse> VpMob_GetBrandingAsync(VpMob_GetBrandingRequest request);
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetBonlinesByBonNumber", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetBonlinesByBonNumberRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string bonNumber;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public string languageShortName;

    public VpMob_GetBonlinesByBonNumberRequest()
    {
    }

    public VpMob_GetBonlinesByBonNumberRequest(string restaurantId, string bonNumber, string languageShortName)
    {
        this.restaurantId = restaurantId;
        this.bonNumber = bonNumber;
        this.languageShortName = languageShortName;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetBonlinesByBonNumberResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetBonlinesByBonNumberResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_GetBonlinesByBonNumberResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.Bonline_DTO[] bonLines;

    public VpMob_GetBonlinesByBonNumberResponse()
    {
    }

    public VpMob_GetBonlinesByBonNumberResponse(VentoClient.CardStatusError_CS VpMob_GetBonlinesByBonNumberResult, VentoClient.Bonline_DTO[] bonLines)
    {
        this.VpMob_GetBonlinesByBonNumberResult = VpMob_GetBonlinesByBonNumberResult;
        this.bonLines = bonLines;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetBonsByPerson", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetBonsByPersonRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string cardId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public System.Nullable<System.DateTime> fromDate;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 3)]
    public System.Nullable<System.DateTime> untilDate;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 4)]
    public System.Nullable<int> cashpointNumberFrom;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 5)]
    public System.Nullable<int> cashpointNumberUntil;

    public VpMob_GetBonsByPersonRequest()
    {
    }

    public VpMob_GetBonsByPersonRequest(string restaurantId, string cardId, System.Nullable<System.DateTime> fromDate, System.Nullable<System.DateTime> untilDate, System.Nullable<int> cashpointNumberFrom, System.Nullable<int> cashpointNumberUntil)
    {
        this.restaurantId = restaurantId;
        this.cardId = cardId;
        this.fromDate = fromDate;
        this.untilDate = untilDate;
        this.cashpointNumberFrom = cashpointNumberFrom;
        this.cashpointNumberUntil = cashpointNumberUntil;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetBonsByPersonResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetBonsByPersonResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_GetBonsByPersonResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.Bon_DTO[] bons;

    public VpMob_GetBonsByPersonResponse()
    {
    }

    public VpMob_GetBonsByPersonResponse(VentoClient.CardStatusError_CS VpMob_GetBonsByPersonResult, VentoClient.Bon_DTO[] bons)
    {
        this.VpMob_GetBonsByPersonResult = VpMob_GetBonsByPersonResult;
        this.bons = bons;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetLastBonByPerson", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetLastBonByPersonRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string cardId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public System.Nullable<System.DateTime> fromDate;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 3)]
    public System.Nullable<System.DateTime> untilDate;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 4)]
    public System.Nullable<VentoClient.BonType_CS> bonType;

    public VpMob_GetLastBonByPersonRequest()
    {
    }

    public VpMob_GetLastBonByPersonRequest(string restaurantId, string cardId, System.Nullable<System.DateTime> fromDate, System.Nullable<System.DateTime> untilDate, System.Nullable<VentoClient.BonType_CS> bonType)
    {
        this.restaurantId = restaurantId;
        this.cardId = cardId;
        this.fromDate = fromDate;
        this.untilDate = untilDate;
        this.bonType = bonType;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetLastBonByPersonResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetLastBonByPersonResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_GetLastBonByPersonResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.Bon_DTO bon;

    public VpMob_GetLastBonByPersonResponse()
    {
    }

    public VpMob_GetLastBonByPersonResponse(VentoClient.CardStatusError_CS VpMob_GetLastBonByPersonResult, VentoClient.Bon_DTO bon)
    {
        this.VpMob_GetLastBonByPersonResult = VpMob_GetLastBonByPersonResult;
        this.bon = bon;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetLimtsByCard", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetLimtsByCardRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string cardId;

    public VpMob_GetLimtsByCardRequest()
    {
    }

    public VpMob_GetLimtsByCardRequest(string restaurantId, string cardId)
    {
        this.restaurantId = restaurantId;
        this.cardId = cardId;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetLimtsByCardResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetLimtsByCardResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_GetLimtsByCardResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public int remainingMonthSumInCent;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public int consumedMonthSumInCent;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 3)]
    public int maximumLoadValueInCent;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 4)]
    public int currentSaldoInCent;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 5)]
    public bool isHceAllowed;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 6)]
    public bool isChargingAllowed;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 7)]
    public string cardPaymentType;

    public VpMob_GetLimtsByCardResponse()
    {
    }

    public VpMob_GetLimtsByCardResponse(VentoClient.CardStatusError_CS VpMob_GetLimtsByCardResult, int remainingMonthSumInCent, int consumedMonthSumInCent, int maximumLoadValueInCent, int currentSaldoInCent, bool isHceAllowed, bool isChargingAllowed, string cardPaymentType)
    {
        this.VpMob_GetLimtsByCardResult = VpMob_GetLimtsByCardResult;
        this.remainingMonthSumInCent = remainingMonthSumInCent;
        this.consumedMonthSumInCent = consumedMonthSumInCent;
        this.maximumLoadValueInCent = maximumLoadValueInCent;
        this.currentSaldoInCent = currentSaldoInCent;
        this.isHceAllowed = isHceAllowed;
        this.isChargingAllowed = isChargingAllowed;
        this.cardPaymentType = cardPaymentType;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetBonusLevelForCard", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetBonusLevelForCardRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string cardId;

    public VpMob_GetBonusLevelForCardRequest()
    {
    }

    public VpMob_GetBonusLevelForCardRequest(string restaurantId, string cardId)
    {
        this.restaurantId = restaurantId;
        this.cardId = cardId;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetBonusLevelForCardResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetBonusLevelForCardResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_GetBonusLevelForCardResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public vp.mocca.app.service.core.Interfaces.BonusValue_DTO[] bonusList;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public System.Guid responseCardId;

    public VpMob_GetBonusLevelForCardResponse()
    {
    }

    public VpMob_GetBonusLevelForCardResponse(VentoClient.CardStatusError_CS VpMob_GetBonusLevelForCardResult, vp.mocca.app.service.core.Interfaces.BonusValue_DTO[] bonusList, System.Guid responseCardId)
    {
        this.VpMob_GetBonusLevelForCardResult = VpMob_GetBonusLevelForCardResult;
        this.bonusList = bonusList;
        this.responseCardId = responseCardId;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetRestaurant", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetRestaurantRequest
{

    public VpMob_GetRestaurantRequest()
    {
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetRestaurantResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetRestaurantResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_GetRestaurantResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public vp.mocca.app.service.core.Interfaces.RestaurantConnection_DTO[] restConnections;

    public VpMob_GetRestaurantResponse()
    {
    }

    public VpMob_GetRestaurantResponse(VentoClient.CardStatusError_CS VpMob_GetRestaurantResult, vp.mocca.app.service.core.Interfaces.RestaurantConnection_DTO[] restConnections)
    {
        this.VpMob_GetRestaurantResult = VpMob_GetRestaurantResult;
        this.restConnections = restConnections;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetNewsFeed", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetNewsFeedRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string cardId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public string languageShortName;

    public VpMob_GetNewsFeedRequest()
    {
    }

    public VpMob_GetNewsFeedRequest(string restaurantId, string cardId, string languageShortName)
    {
        this.restaurantId = restaurantId;
        this.cardId = cardId;
        this.languageShortName = languageShortName;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetNewsFeedResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetNewsFeedResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_GetNewsFeedResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.NewsFeedItem_DTO[] newsFeedList;

    public VpMob_GetNewsFeedResponse()
    {
    }

    public VpMob_GetNewsFeedResponse(VentoClient.CardStatusError_CS VpMob_GetNewsFeedResult, VentoClient.NewsFeedItem_DTO[] newsFeedList)
    {
        this.VpMob_GetNewsFeedResult = VpMob_GetNewsFeedResult;
        this.newsFeedList = newsFeedList;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetPersonInformation", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetPersonInformationRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string cardId;

    public VpMob_GetPersonInformationRequest()
    {
    }

    public VpMob_GetPersonInformationRequest(string restaurantId, string cardId)
    {
        this.restaurantId = restaurantId;
        this.cardId = cardId;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetPersonInformationResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetPersonInformationResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_GetPersonInformationResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.PersonInformation_DTO personInformationDto;

    public VpMob_GetPersonInformationResponse()
    {
    }

    public VpMob_GetPersonInformationResponse(VentoClient.CardStatusError_CS VpMob_GetPersonInformationResult, VentoClient.PersonInformation_DTO personInformationDto)
    {
        this.VpMob_GetPersonInformationResult = VpMob_GetPersonInformationResult;
        this.personInformationDto = personInformationDto;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_Login", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_LoginRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string userName;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public string passwordHash;

    public VpMob_LoginRequest()
    {
    }

    public VpMob_LoginRequest(string restaurantId, string userName, string passwordHash)
    {
        this.restaurantId = restaurantId;
        this.userName = userName;
        this.passwordHash = passwordHash;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_LoginResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_LoginResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_LoginResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.PersonInformation_DTO personInformationDto;

    public VpMob_LoginResponse()
    {
    }

    public VpMob_LoginResponse(VentoClient.CardStatusError_CS VpMob_LoginResult, VentoClient.PersonInformation_DTO personInformationDto)
    {
        this.VpMob_LoginResult = VpMob_LoginResult;
        this.personInformationDto = personInformationDto;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetMenu", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetMenuRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    public VpMob_GetMenuRequest()
    {
    }

    public VpMob_GetMenuRequest(string restaurantId)
    {
        this.restaurantId = restaurantId;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetMenuResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetMenuResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_GetMenuResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string url;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public byte[] pdf;

    public VpMob_GetMenuResponse()
    {
    }

    public VpMob_GetMenuResponse(VentoClient.CardStatusError_CS VpMob_GetMenuResult, string url, byte[] pdf)
    {
        this.VpMob_GetMenuResult = VpMob_GetMenuResult;
        this.url = url;
        this.pdf = pdf;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_LoginByCardNumber", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_LoginByCardNumberRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string cardNumber;

    public VpMob_LoginByCardNumberRequest()
    {
    }

    public VpMob_LoginByCardNumberRequest(string restaurantId, string cardNumber)
    {
        this.restaurantId = restaurantId;
        this.cardNumber = cardNumber;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_LoginByCardNumberResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_LoginByCardNumberResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_LoginByCardNumberResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.PersonInformation_DTO personInformationDto;

    public VpMob_LoginByCardNumberResponse()
    {
    }

    public VpMob_LoginByCardNumberResponse(VentoClient.CardStatusError_CS VpMob_LoginByCardNumberResult, VentoClient.PersonInformation_DTO personInformationDto)
    {
        this.VpMob_LoginByCardNumberResult = VpMob_LoginByCardNumberResult;
        this.personInformationDto = personInformationDto;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetAllPersonInformation", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetAllPersonInformationRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string personId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public string filterText;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 3)]
    public int startRowIndex;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 4)]
    public int endRowIndex;

    public VpMob_GetAllPersonInformationRequest()
    {
    }

    public VpMob_GetAllPersonInformationRequest(string restaurantId, string personId, string filterText, int startRowIndex, int endRowIndex)
    {
        this.restaurantId = restaurantId;
        this.personId = personId;
        this.filterText = filterText;
        this.startRowIndex = startRowIndex;
        this.endRowIndex = endRowIndex;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetAllPersonInformationResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetAllPersonInformationResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_GetAllPersonInformationResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.PersonInformation_DTO[] personInformationDto;

    public VpMob_GetAllPersonInformationResponse()
    {
    }

    public VpMob_GetAllPersonInformationResponse(VentoClient.CardStatusError_CS VpMob_GetAllPersonInformationResult, VentoClient.PersonInformation_DTO[] personInformationDto)
    {
        this.VpMob_GetAllPersonInformationResult = VpMob_GetAllPersonInformationResult;
        this.personInformationDto = personInformationDto;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetApplicationSetting", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetApplicationSettingRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string key;

    public VpMob_GetApplicationSettingRequest()
    {
    }

    public VpMob_GetApplicationSettingRequest(string restaurantId, string key)
    {
        this.restaurantId = restaurantId;
        this.key = key;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetApplicationSettingResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetApplicationSettingResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_GetApplicationSettingResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string value;

    public VpMob_GetApplicationSettingResponse()
    {
    }

    public VpMob_GetApplicationSettingResponse(VentoClient.CardStatusError_CS VpMob_GetApplicationSettingResult, string value)
    {
        this.VpMob_GetApplicationSettingResult = VpMob_GetApplicationSettingResult;
        this.value = value;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetAllCustomergroupInformation", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetAllCustomergroupInformationRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string personId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public string filterText;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 3)]
    public System.Nullable<bool> getSystemEntries;

    public VpMob_GetAllCustomergroupInformationRequest()
    {
    }

    public VpMob_GetAllCustomergroupInformationRequest(string restaurantId, string personId, string filterText, System.Nullable<bool> getSystemEntries)
    {
        this.restaurantId = restaurantId;
        this.personId = personId;
        this.filterText = filterText;
        this.getSystemEntries = getSystemEntries;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetAllCustomergroupInformationResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetAllCustomergroupInformationResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_GetAllCustomergroupInformationResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.CustomerGroupInformation_DTO[] customerGroupInformationDto;

    public VpMob_GetAllCustomergroupInformationResponse()
    {
    }

    public VpMob_GetAllCustomergroupInformationResponse(VentoClient.CardStatusError_CS VpMob_GetAllCustomergroupInformationResult, VentoClient.CustomerGroupInformation_DTO[] customerGroupInformationDto)
    {
        this.VpMob_GetAllCustomergroupInformationResult = VpMob_GetAllCustomergroupInformationResult;
        this.customerGroupInformationDto = customerGroupInformationDto;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetNutrientInformation", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetNutrientInformationRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public System.DateTime dateFrom;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public System.Nullable<System.DateTime> dateUntil;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 3)]
    public string cardId;

    public VpMob_GetNutrientInformationRequest()
    {
    }

    public VpMob_GetNutrientInformationRequest(string restaurantId, System.DateTime dateFrom, System.Nullable<System.DateTime> dateUntil, string cardId)
    {
        this.restaurantId = restaurantId;
        this.dateFrom = dateFrom;
        this.dateUntil = dateUntil;
        this.cardId = cardId;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetNutrientInformationResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetNutrientInformationResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_GetNutrientInformationResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.NutrientInformation_DTO[] nutrientInformationDto;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public VentoClient.AllergenInformation_DTO[] allergenInfo;

    public VpMob_GetNutrientInformationResponse()
    {
    }

    public VpMob_GetNutrientInformationResponse(VentoClient.CardStatusError_CS VpMob_GetNutrientInformationResult, VentoClient.NutrientInformation_DTO[] nutrientInformationDto, VentoClient.AllergenInformation_DTO[] allergenInfo)
    {
        this.VpMob_GetNutrientInformationResult = VpMob_GetNutrientInformationResult;
        this.nutrientInformationDto = nutrientInformationDto;
        this.allergenInfo = allergenInfo;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetBonlinesByPerson", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetBonlinesByPersonRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string cardId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public System.DateTime dateFrom;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 3)]
    public System.DateTime dateUntil;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 4)]
    public string languageShortName;

    public VpMob_GetBonlinesByPersonRequest()
    {
    }

    public VpMob_GetBonlinesByPersonRequest(string restaurantId, string cardId, System.DateTime dateFrom, System.DateTime dateUntil, string languageShortName)
    {
        this.restaurantId = restaurantId;
        this.cardId = cardId;
        this.dateFrom = dateFrom;
        this.dateUntil = dateUntil;
        this.languageShortName = languageShortName;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetBonlinesByPersonResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetBonlinesByPersonResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_GetBonlinesByPersonResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.Bonline_DTO[] bonLines;

    public VpMob_GetBonlinesByPersonResponse()
    {
    }

    public VpMob_GetBonlinesByPersonResponse(VentoClient.CardStatusError_CS VpMob_GetBonlinesByPersonResult, VentoClient.Bonline_DTO[] bonLines)
    {
        this.VpMob_GetBonlinesByPersonResult = VpMob_GetBonlinesByPersonResult;
        this.bonLines = bonLines;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetContactInformation", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetContactInformationRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    public VpMob_GetContactInformationRequest()
    {
    }

    public VpMob_GetContactInformationRequest(string restaurantId)
    {
        this.restaurantId = restaurantId;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetContactInformationResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetContactInformationResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_GetContactInformationResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.ContactInformation_DTO[] contactInformations;

    public VpMob_GetContactInformationResponse()
    {
    }

    public VpMob_GetContactInformationResponse(VentoClient.CardStatusError_CS VpMob_GetContactInformationResult, VentoClient.ContactInformation_DTO[] contactInformations)
    {
        this.VpMob_GetContactInformationResult = VpMob_GetContactInformationResult;
        this.contactInformations = contactInformations;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetActivityLevels", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetActivityLevelsRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string activityLevelId;

    public VpMob_GetActivityLevelsRequest()
    {
    }

    public VpMob_GetActivityLevelsRequest(string restaurantId, string activityLevelId)
    {
        this.restaurantId = restaurantId;
        this.activityLevelId = activityLevelId;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetActivityLevelsResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetActivityLevelsResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_GetActivityLevelsResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.ActivityLevel_DTO[] activityLevelList;

    public VpMob_GetActivityLevelsResponse()
    {
    }

    public VpMob_GetActivityLevelsResponse(VentoClient.CardStatusError_CS VpMob_GetActivityLevelsResult, VentoClient.ActivityLevel_DTO[] activityLevelList)
    {
        this.VpMob_GetActivityLevelsResult = VpMob_GetActivityLevelsResult;
        this.activityLevelList = activityLevelList;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetTopUps", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetTopUpsRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string cardId;

    public VpMob_GetTopUpsRequest()
    {
    }

    public VpMob_GetTopUpsRequest(string restaurantId, string cardId)
    {
        this.restaurantId = restaurantId;
        this.cardId = cardId;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetTopUpsResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetTopUpsResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_GetTopUpsResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.TopUp_DTO[] topUpList;

    public VpMob_GetTopUpsResponse()
    {
    }

    public VpMob_GetTopUpsResponse(VentoClient.CardStatusError_CS VpMob_GetTopUpsResult, VentoClient.TopUp_DTO[] topUpList)
    {
        this.VpMob_GetTopUpsResult = VpMob_GetTopUpsResult;
        this.topUpList = topUpList;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetPersonPicture", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetPersonPictureRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string pictureId;

    public VpMob_GetPersonPictureRequest()
    {
    }

    public VpMob_GetPersonPictureRequest(string restaurantId, string pictureId)
    {
        this.restaurantId = restaurantId;
        this.pictureId = pictureId;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetPersonPictureResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetPersonPictureResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_GetPersonPictureResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public System.Data.Linq.Binary picture;

    public VpMob_GetPersonPictureResponse()
    {
    }

    public VpMob_GetPersonPictureResponse(VentoClient.CardStatusError_CS VpMob_GetPersonPictureResult, System.Data.Linq.Binary picture)
    {
        this.VpMob_GetPersonPictureResult = VpMob_GetPersonPictureResult;
        this.picture = picture;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetAllergens", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetAllergensRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public System.Nullable<int> allergenId;

    public VpMob_GetAllergensRequest()
    {
    }

    public VpMob_GetAllergensRequest(string restaurantId, System.Nullable<int> allergenId)
    {
        this.restaurantId = restaurantId;
        this.allergenId = allergenId;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetAllergensResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetAllergensResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_GetAllergensResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.AllergenInformation_DTO[] allergenList;

    public VpMob_GetAllergensResponse()
    {
    }

    public VpMob_GetAllergensResponse(VentoClient.CardStatusError_CS VpMob_GetAllergensResult, VentoClient.AllergenInformation_DTO[] allergenList)
    {
        this.VpMob_GetAllergensResult = VpMob_GetAllergensResult;
        this.allergenList = allergenList;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetAllergenLegend", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetAllergenLegendRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    public VpMob_GetAllergenLegendRequest()
    {
    }

    public VpMob_GetAllergenLegendRequest(string restaurantId)
    {
        this.restaurantId = restaurantId;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetAllergenLegendResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetAllergenLegendResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_GetAllergenLegendResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public System.Data.Linq.Binary legend;

    public VpMob_GetAllergenLegendResponse()
    {
    }

    public VpMob_GetAllergenLegendResponse(VentoClient.CardStatusError_CS VpMob_GetAllergenLegendResult, System.Data.Linq.Binary legend)
    {
        this.VpMob_GetAllergenLegendResult = VpMob_GetAllergenLegendResult;
        this.legend = legend;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetFeedbackProgram", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetFeedbackProgramRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string cardId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public string articleId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 3)]
    public string languageShortName;

    public VpMob_GetFeedbackProgramRequest()
    {
    }

    public VpMob_GetFeedbackProgramRequest(string restaurantId, string cardId, string articleId, string languageShortName)
    {
        this.restaurantId = restaurantId;
        this.cardId = cardId;
        this.articleId = articleId;
        this.languageShortName = languageShortName;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetFeedbackProgramResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetFeedbackProgramResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_GetFeedbackProgramResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.FeedbackProgram_DTO[] programm;

    public VpMob_GetFeedbackProgramResponse()
    {
    }

    public VpMob_GetFeedbackProgramResponse(VentoClient.CardStatusError_CS VpMob_GetFeedbackProgramResult, VentoClient.FeedbackProgram_DTO[] programm)
    {
        this.VpMob_GetFeedbackProgramResult = VpMob_GetFeedbackProgramResult;
        this.programm = programm;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_MergeAccounts", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_MergeAccountsRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string personId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public string personIdToBeMerged;

    public VpMob_MergeAccountsRequest()
    {
    }

    public VpMob_MergeAccountsRequest(string restaurantId, string personId, string personIdToBeMerged)
    {
        this.restaurantId = restaurantId;
        this.personId = personId;
        this.personIdToBeMerged = personIdToBeMerged;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_MergeAccountsResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_MergeAccountsResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_MergeAccountsResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.PersonInformation_DTO personInformationDto;

    public VpMob_MergeAccountsResponse()
    {
    }

    public VpMob_MergeAccountsResponse(VentoClient.CardStatusError_CS VpMob_MergeAccountsResult, VentoClient.PersonInformation_DTO personInformationDto)
    {
        this.VpMob_MergeAccountsResult = VpMob_MergeAccountsResult;
        this.personInformationDto = personInformationDto;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetText", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetTextRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string languageShortName;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public string cashPointTypeShortName;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 3)]
    public string textKey;

    public VpMob_GetTextRequest()
    {
    }

    public VpMob_GetTextRequest(string restaurantId, string languageShortName, string cashPointTypeShortName, string textKey)
    {
        this.restaurantId = restaurantId;
        this.languageShortName = languageShortName;
        this.cashPointTypeShortName = cashPointTypeShortName;
        this.textKey = textKey;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetTextResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetTextResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_GetTextResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public System.Collections.Generic.Dictionary<string, string> values;

    public VpMob_GetTextResponse()
    {
    }

    public VpMob_GetTextResponse(VentoClient.CardStatusError_CS VpMob_GetTextResult, System.Collections.Generic.Dictionary<string, string> values)
    {
        this.VpMob_GetTextResult = VpMob_GetTextResult;
        this.values = values;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetLastTermStatesByPerson", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetLastTermStatesByPersonRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string personId;

    public VpMob_GetLastTermStatesByPersonRequest()
    {
    }

    public VpMob_GetLastTermStatesByPersonRequest(string restaurantId, string personId)
    {
        this.restaurantId = restaurantId;
        this.personId = personId;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetLastTermStatesByPersonResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetLastTermStatesByPersonResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_GetLastTermStatesByPersonResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.TermState_DTO[] lastTermStates;

    public VpMob_GetLastTermStatesByPersonResponse()
    {
    }

    public VpMob_GetLastTermStatesByPersonResponse(VentoClient.CardStatusError_CS VpMob_GetLastTermStatesByPersonResult, VentoClient.TermState_DTO[] lastTermStates)
    {
        this.VpMob_GetLastTermStatesByPersonResult = VpMob_GetLastTermStatesByPersonResult;
        this.lastTermStates = lastTermStates;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_LoginByPersonNumber", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_LoginByPersonNumberRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string personNumber;

    public VpMob_LoginByPersonNumberRequest()
    {
    }

    public VpMob_LoginByPersonNumberRequest(string restaurantId, string personNumber)
    {
        this.restaurantId = restaurantId;
        this.personNumber = personNumber;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_LoginByPersonNumberResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_LoginByPersonNumberResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_LoginByPersonNumberResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.PersonInformation_DTO personInformationDto;

    public VpMob_LoginByPersonNumberResponse()
    {
    }

    public VpMob_LoginByPersonNumberResponse(VentoClient.CardStatusError_CS VpMob_LoginByPersonNumberResult, VentoClient.PersonInformation_DTO personInformationDto)
    {
        this.VpMob_LoginByPersonNumberResult = VpMob_LoginByPersonNumberResult;
        this.personInformationDto = personInformationDto;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetAvailableLanguages", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetAvailableLanguagesRequest
{

    public VpMob_GetAvailableLanguagesRequest()
    {
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetAvailableLanguagesResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetAvailableLanguagesResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_GetAvailableLanguagesResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.Language_DTO[] availableLanguages;

    public VpMob_GetAvailableLanguagesResponse()
    {
    }

    public VpMob_GetAvailableLanguagesResponse(VentoClient.CardStatusError_CS VpMob_GetAvailableLanguagesResult, VentoClient.Language_DTO[] availableLanguages)
    {
        this.VpMob_GetAvailableLanguagesResult = VpMob_GetAvailableLanguagesResult;
        this.availableLanguages = availableLanguages;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_LoginByAccountDetail", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_LoginByAccountDetailRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string personAccountDetailAttribute;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public string personAccountDetailAttributeValue;

    public VpMob_LoginByAccountDetailRequest()
    {
    }

    public VpMob_LoginByAccountDetailRequest(string restaurantId, string personAccountDetailAttribute, string personAccountDetailAttributeValue)
    {
        this.restaurantId = restaurantId;
        this.personAccountDetailAttribute = personAccountDetailAttribute;
        this.personAccountDetailAttributeValue = personAccountDetailAttributeValue;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_LoginByAccountDetailResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_LoginByAccountDetailResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_LoginByAccountDetailResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.PersonInformation_DTO personInformationDto;

    public VpMob_LoginByAccountDetailResponse()
    {
    }

    public VpMob_LoginByAccountDetailResponse(VentoClient.CardStatusError_CS VpMob_LoginByAccountDetailResult, VentoClient.PersonInformation_DTO personInformationDto)
    {
        this.VpMob_LoginByAccountDetailResult = VpMob_LoginByAccountDetailResult;
        this.personInformationDto = personInformationDto;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetBonlinesByPersonWithExcludedCashpoints", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetBonlinesByPersonWithExcludedCashpointsRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string cardId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public System.DateTime dateFrom;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 3)]
    public System.DateTime dateUntil;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 4)]
    public string languageShortName;

    public VpMob_GetBonlinesByPersonWithExcludedCashpointsRequest()
    {
    }

    public VpMob_GetBonlinesByPersonWithExcludedCashpointsRequest(string restaurantId, string cardId, System.DateTime dateFrom, System.DateTime dateUntil, string languageShortName)
    {
        this.restaurantId = restaurantId;
        this.cardId = cardId;
        this.dateFrom = dateFrom;
        this.dateUntil = dateUntil;
        this.languageShortName = languageShortName;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetBonlinesByPersonWithExcludedCashpointsResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetBonlinesByPersonWithExcludedCashpointsResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_GetBonlinesByPersonWithExcludedCashpointsResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.Bonline_DTO[] bonLines;

    public VpMob_GetBonlinesByPersonWithExcludedCashpointsResponse()
    {
    }

    public VpMob_GetBonlinesByPersonWithExcludedCashpointsResponse(VentoClient.CardStatusError_CS VpMob_GetBonlinesByPersonWithExcludedCashpointsResult, VentoClient.Bonline_DTO[] bonLines)
    {
        this.VpMob_GetBonlinesByPersonWithExcludedCashpointsResult = VpMob_GetBonlinesByPersonWithExcludedCashpointsResult;
        this.bonLines = bonLines;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_DoPayment", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_DoPaymentRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string cardId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public int amountInCent;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 3)]
    public string redirectUrl;

    public VpMob_DoPaymentRequest()
    {
    }

    public VpMob_DoPaymentRequest(string restaurantId, string cardId, int amountInCent, string redirectUrl)
    {
        this.restaurantId = restaurantId;
        this.cardId = cardId;
        this.amountInCent = amountInCent;
        this.redirectUrl = redirectUrl;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_DoPaymentResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_DoPaymentResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_DoPaymentResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string paymentUrl;

    public VpMob_DoPaymentResponse()
    {
    }

    public VpMob_DoPaymentResponse(VentoClient.CardStatusError_CS VpMob_DoPaymentResult, string paymentUrl)
    {
        this.VpMob_DoPaymentResult = VpMob_DoPaymentResult;
        this.paymentUrl = paymentUrl;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_DoMobilePayment", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_DoMobilePaymentRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string cardId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public int amountInCent;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 3)]
    public string redirectUrl;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 4)]
    public VentoClient.PaymentType_CS paymentType;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 5)]
    public string paymentToken;

    public VpMob_DoMobilePaymentRequest()
    {
    }

    public VpMob_DoMobilePaymentRequest(string restaurantId, string cardId, int amountInCent, string redirectUrl, VentoClient.PaymentType_CS paymentType, string paymentToken)
    {
        this.restaurantId = restaurantId;
        this.cardId = cardId;
        this.amountInCent = amountInCent;
        this.redirectUrl = redirectUrl;
        this.paymentType = paymentType;
        this.paymentToken = paymentToken;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_DoMobilePaymentResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_DoMobilePaymentResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_DoMobilePaymentResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string paymentUrl;

    public VpMob_DoMobilePaymentResponse()
    {
    }

    public VpMob_DoMobilePaymentResponse(VentoClient.CardStatusError_CS VpMob_DoMobilePaymentResult, string paymentUrl)
    {
        this.VpMob_DoMobilePaymentResult = VpMob_DoMobilePaymentResult;
        this.paymentUrl = paymentUrl;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_DoAuthorization", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_DoAuthorizationRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string cardId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public int reloadValueInCent;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 3)]
    public int minimumSaldoInCent;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 4)]
    public string redirectUrl;

    public VpMob_DoAuthorizationRequest()
    {
    }

    public VpMob_DoAuthorizationRequest(string restaurantId, string cardId, int reloadValueInCent, int minimumSaldoInCent, string redirectUrl)
    {
        this.restaurantId = restaurantId;
        this.cardId = cardId;
        this.reloadValueInCent = reloadValueInCent;
        this.minimumSaldoInCent = minimumSaldoInCent;
        this.redirectUrl = redirectUrl;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_DoAuthorizationResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_DoAuthorizationResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_DoAuthorizationResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string paymentUrl;

    public VpMob_DoAuthorizationResponse()
    {
    }

    public VpMob_DoAuthorizationResponse(VentoClient.CardStatusError_CS VpMob_DoAuthorizationResult, string paymentUrl)
    {
        this.VpMob_DoAuthorizationResult = VpMob_DoAuthorizationResult;
        this.paymentUrl = paymentUrl;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetReportData", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetReportDataRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string personId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public System.DateTime dateFrom;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 3)]
    public System.DateTime dateUntil;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 4)]
    public string languageShortName;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 5)]
    public System.Nullable<int> cashpointNumberFrom;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 6)]
    public System.Nullable<int> cashpointNumberUntil;

    public VpMob_GetReportDataRequest()
    {
    }

    public VpMob_GetReportDataRequest(string restaurantId, string personId, System.DateTime dateFrom, System.DateTime dateUntil, string languageShortName, System.Nullable<int> cashpointNumberFrom, System.Nullable<int> cashpointNumberUntil)
    {
        this.restaurantId = restaurantId;
        this.personId = personId;
        this.dateFrom = dateFrom;
        this.dateUntil = dateUntil;
        this.languageShortName = languageShortName;
        this.cashpointNumberFrom = cashpointNumberFrom;
        this.cashpointNumberUntil = cashpointNumberUntil;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetReportDataResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetReportDataResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_GetReportDataResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public byte[] binaryReport;

    public VpMob_GetReportDataResponse()
    {
    }

    public VpMob_GetReportDataResponse(VentoClient.CardStatusError_CS VpMob_GetReportDataResult, byte[] binaryReport)
    {
        this.VpMob_GetReportDataResult = VpMob_GetReportDataResult;
        this.binaryReport = binaryReport;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_NotifyModule", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_NotifyModuleRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string[] customerGroupNumbers;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public string title;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 3)]
    public string message;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 4)]
    public int moduleId;

    public VpMob_NotifyModuleRequest()
    {
    }

    public VpMob_NotifyModuleRequest(string restaurantId, string[] customerGroupNumbers, string title, string message, int moduleId)
    {
        this.restaurantId = restaurantId;
        this.customerGroupNumbers = customerGroupNumbers;
        this.title = title;
        this.message = message;
        this.moduleId = moduleId;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_NotifyModuleResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_NotifyModuleResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_NotifyModuleResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.DeviceToken_DTO[] deviceTokens;

    public VpMob_NotifyModuleResponse()
    {
    }

    public VpMob_NotifyModuleResponse(VentoClient.CardStatusError_CS VpMob_NotifyModuleResult, VentoClient.DeviceToken_DTO[] deviceTokens)
    {
        this.VpMob_NotifyModuleResult = VpMob_NotifyModuleResult;
        this.deviceTokens = deviceTokens;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_NotifyNewsfeed", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_NotifyNewsfeedRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string[] customerGroupNumbers;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public string title;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 3)]
    public string message;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 4)]
    public int newsfeedId;

    public VpMob_NotifyNewsfeedRequest()
    {
    }

    public VpMob_NotifyNewsfeedRequest(string restaurantId, string[] customerGroupNumbers, string title, string message, int newsfeedId)
    {
        this.restaurantId = restaurantId;
        this.customerGroupNumbers = customerGroupNumbers;
        this.title = title;
        this.message = message;
        this.newsfeedId = newsfeedId;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_NotifyNewsfeedResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_NotifyNewsfeedResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_NotifyNewsfeedResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.DeviceToken_DTO[] deviceTokens;

    public VpMob_NotifyNewsfeedResponse()
    {
    }

    public VpMob_NotifyNewsfeedResponse(VentoClient.CardStatusError_CS VpMob_NotifyNewsfeedResult, VentoClient.DeviceToken_DTO[] deviceTokens)
    {
        this.VpMob_NotifyNewsfeedResult = VpMob_NotifyNewsfeedResult;
        this.deviceTokens = deviceTokens;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_NotifyFeedback", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_NotifyFeedbackRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string[] customerGroupNumbers;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public string title;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 3)]
    public string message;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 4)]
    public int feedbackId;

    public VpMob_NotifyFeedbackRequest()
    {
    }

    public VpMob_NotifyFeedbackRequest(string restaurantId, string[] customerGroupNumbers, string title, string message, int feedbackId)
    {
        this.restaurantId = restaurantId;
        this.customerGroupNumbers = customerGroupNumbers;
        this.title = title;
        this.message = message;
        this.feedbackId = feedbackId;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_NotifyFeedbackResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_NotifyFeedbackResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_NotifyFeedbackResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.DeviceToken_DTO[] deviceTokens;

    public VpMob_NotifyFeedbackResponse()
    {
    }

    public VpMob_NotifyFeedbackResponse(VentoClient.CardStatusError_CS VpMob_NotifyFeedbackResult, VentoClient.DeviceToken_DTO[] deviceTokens)
    {
        this.VpMob_NotifyFeedbackResult = VpMob_NotifyFeedbackResult;
        this.deviceTokens = deviceTokens;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetBleEnabledCashpoints", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetBleEnabledCashpointsRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string cardId;

    public VpMob_GetBleEnabledCashpointsRequest()
    {
    }

    public VpMob_GetBleEnabledCashpointsRequest(string restaurantId, string cardId)
    {
        this.restaurantId = restaurantId;
        this.cardId = cardId;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetBleEnabledCashpointsResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetBleEnabledCashpointsResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_GetBleEnabledCashpointsResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.Cashpoint_DTO[] cashpoints;

    public VpMob_GetBleEnabledCashpointsResponse()
    {
    }

    public VpMob_GetBleEnabledCashpointsResponse(VentoClient.CardStatusError_CS VpMob_GetBleEnabledCashpointsResult, VentoClient.Cashpoint_DTO[] cashpoints)
    {
        this.VpMob_GetBleEnabledCashpointsResult = VpMob_GetBleEnabledCashpointsResult;
        this.cashpoints = cashpoints;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_IsPaymentActive", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_IsPaymentActiveRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string cardId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public string cashpointId;

    public VpMob_IsPaymentActiveRequest()
    {
    }

    public VpMob_IsPaymentActiveRequest(string restaurantId, string cardId, string cashpointId)
    {
        this.restaurantId = restaurantId;
        this.cardId = cardId;
        this.cashpointId = cashpointId;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_IsPaymentActiveResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_IsPaymentActiveResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_IsPaymentActiveResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public bool active;

    public VpMob_IsPaymentActiveResponse()
    {
    }

    public VpMob_IsPaymentActiveResponse(VentoClient.CardStatusError_CS VpMob_IsPaymentActiveResult, bool active)
    {
        this.VpMob_IsPaymentActiveResult = VpMob_IsPaymentActiveResult;
        this.active = active;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetBonByBonId", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetBonByBonIdRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string bonId;

    public VpMob_GetBonByBonIdRequest()
    {
    }

    public VpMob_GetBonByBonIdRequest(string restaurantId, string bonId)
    {
        this.restaurantId = restaurantId;
        this.bonId = bonId;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetBonByBonIdResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetBonByBonIdResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_GetBonByBonIdResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.Bon_DTO bon;

    public VpMob_GetBonByBonIdResponse()
    {
    }

    public VpMob_GetBonByBonIdResponse(VentoClient.CardStatusError_CS VpMob_GetBonByBonIdResult, VentoClient.Bon_DTO bon)
    {
        this.VpMob_GetBonByBonIdResult = VpMob_GetBonByBonIdResult;
        this.bon = bon;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetBranding", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetBrandingRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    public VpMob_GetBrandingRequest()
    {
    }

    public VpMob_GetBrandingRequest(string restaurantId)
    {
        this.restaurantId = restaurantId;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMob_GetBrandingResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMob_GetBrandingResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.CardStatusError_CS VpMob_GetBrandingResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.Branding_DTO branding;

    public VpMob_GetBrandingResponse()
    {
    }

    public VpMob_GetBrandingResponse(VentoClient.CardStatusError_CS VpMob_GetBrandingResult, VentoClient.Branding_DTO branding)
    {
        this.VpMob_GetBrandingResult = VpMob_GetBrandingResult;
        this.branding = branding;
    }
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface IVentopayMobileServiceChannel : IVentopayMobileService, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class VentopayMobileServiceClient : System.ServiceModel.ClientBase<IVentopayMobileService>, IVentopayMobileService
{

    public VentopayMobileServiceClient()
    {
    }

    public VentopayMobileServiceClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
    {
    }

    public VentopayMobileServiceClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
    {
    }

    public VentopayMobileServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
    {
    }

    public VentopayMobileServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
    {
    }

    public VentoClient.CardStatusError_CS VpMob_CardTopUp(string restaurantId, int valueInCent, string cardId, VentoClient.PaymentType_CS paymentType, string description, System.Nullable<bool> ignoreMaximumSaldo)
    {
        return base.Channel.VpMob_CardTopUp(restaurantId, valueInCent, cardId, paymentType, description, ignoreMaximumSaldo);
    }

    public System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_CardTopUpAsync(string restaurantId, int valueInCent, string cardId, VentoClient.PaymentType_CS paymentType, string description, System.Nullable<bool> ignoreMaximumSaldo)
    {
        return base.Channel.VpMob_CardTopUpAsync(restaurantId, valueInCent, cardId, paymentType, description, ignoreMaximumSaldo);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_GetBonlinesByBonNumberResponse IVentopayMobileService.VpMob_GetBonlinesByBonNumber(VpMob_GetBonlinesByBonNumberRequest request)
    {
        return base.Channel.VpMob_GetBonlinesByBonNumber(request);
    }

    public VentoClient.CardStatusError_CS VpMob_GetBonlinesByBonNumber(string restaurantId, string bonNumber, string languageShortName, out VentoClient.Bonline_DTO[] bonLines)
    {
        VpMob_GetBonlinesByBonNumberRequest inValue = new VpMob_GetBonlinesByBonNumberRequest();
        inValue.restaurantId = restaurantId;
        inValue.bonNumber = bonNumber;
        inValue.languageShortName = languageShortName;
        VpMob_GetBonlinesByBonNumberResponse retVal = ((IVentopayMobileService)(this)).VpMob_GetBonlinesByBonNumber(inValue);
        bonLines = retVal.bonLines;
        return retVal.VpMob_GetBonlinesByBonNumberResult;
    }

    public System.Threading.Tasks.Task<VpMob_GetBonlinesByBonNumberResponse> VpMob_GetBonlinesByBonNumberAsync(VpMob_GetBonlinesByBonNumberRequest request)
    {
        return base.Channel.VpMob_GetBonlinesByBonNumberAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_GetBonsByPersonResponse IVentopayMobileService.VpMob_GetBonsByPerson(VpMob_GetBonsByPersonRequest request)
    {
        return base.Channel.VpMob_GetBonsByPerson(request);
    }

    public VentoClient.CardStatusError_CS VpMob_GetBonsByPerson(string restaurantId, string cardId, System.Nullable<System.DateTime> fromDate, System.Nullable<System.DateTime> untilDate, System.Nullable<int> cashpointNumberFrom, System.Nullable<int> cashpointNumberUntil, out VentoClient.Bon_DTO[] bons)
    {
        VpMob_GetBonsByPersonRequest inValue = new VpMob_GetBonsByPersonRequest();
        inValue.restaurantId = restaurantId;
        inValue.cardId = cardId;
        inValue.fromDate = fromDate;
        inValue.untilDate = untilDate;
        inValue.cashpointNumberFrom = cashpointNumberFrom;
        inValue.cashpointNumberUntil = cashpointNumberUntil;
        VpMob_GetBonsByPersonResponse retVal = ((IVentopayMobileService)(this)).VpMob_GetBonsByPerson(inValue);
        bons = retVal.bons;
        return retVal.VpMob_GetBonsByPersonResult;
    }

    public System.Threading.Tasks.Task<VpMob_GetBonsByPersonResponse> VpMob_GetBonsByPersonAsync(VpMob_GetBonsByPersonRequest request)
    {
        return base.Channel.VpMob_GetBonsByPersonAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_GetLastBonByPersonResponse IVentopayMobileService.VpMob_GetLastBonByPerson(VpMob_GetLastBonByPersonRequest request)
    {
        return base.Channel.VpMob_GetLastBonByPerson(request);
    }

    public VentoClient.CardStatusError_CS VpMob_GetLastBonByPerson(string restaurantId, string cardId, System.Nullable<System.DateTime> fromDate, System.Nullable<System.DateTime> untilDate, System.Nullable<VentoClient.BonType_CS> bonType, out VentoClient.Bon_DTO bon)
    {
        VpMob_GetLastBonByPersonRequest inValue = new VpMob_GetLastBonByPersonRequest();
        inValue.restaurantId = restaurantId;
        inValue.cardId = cardId;
        inValue.fromDate = fromDate;
        inValue.untilDate = untilDate;
        inValue.bonType = bonType;
        VpMob_GetLastBonByPersonResponse retVal = ((IVentopayMobileService)(this)).VpMob_GetLastBonByPerson(inValue);
        bon = retVal.bon;
        return retVal.VpMob_GetLastBonByPersonResult;
    }

    public System.Threading.Tasks.Task<VpMob_GetLastBonByPersonResponse> VpMob_GetLastBonByPersonAsync(VpMob_GetLastBonByPersonRequest request)
    {
        return base.Channel.VpMob_GetLastBonByPersonAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_GetLimtsByCardResponse IVentopayMobileService.VpMob_GetLimtsByCard(VpMob_GetLimtsByCardRequest request)
    {
        return base.Channel.VpMob_GetLimtsByCard(request);
    }

    public VentoClient.CardStatusError_CS VpMob_GetLimtsByCard(string restaurantId, string cardId, out int remainingMonthSumInCent, out int consumedMonthSumInCent, out int maximumLoadValueInCent, out int currentSaldoInCent, out bool isHceAllowed, out bool isChargingAllowed, out string cardPaymentType)
    {
        VpMob_GetLimtsByCardRequest inValue = new VpMob_GetLimtsByCardRequest();
        inValue.restaurantId = restaurantId;
        inValue.cardId = cardId;
        VpMob_GetLimtsByCardResponse retVal = ((IVentopayMobileService)(this)).VpMob_GetLimtsByCard(inValue);
        remainingMonthSumInCent = retVal.remainingMonthSumInCent;
        consumedMonthSumInCent = retVal.consumedMonthSumInCent;
        maximumLoadValueInCent = retVal.maximumLoadValueInCent;
        currentSaldoInCent = retVal.currentSaldoInCent;
        isHceAllowed = retVal.isHceAllowed;
        isChargingAllowed = retVal.isChargingAllowed;
        cardPaymentType = retVal.cardPaymentType;
        return retVal.VpMob_GetLimtsByCardResult;
    }

    public System.Threading.Tasks.Task<VpMob_GetLimtsByCardResponse> VpMob_GetLimtsByCardAsync(VpMob_GetLimtsByCardRequest request)
    {
        return base.Channel.VpMob_GetLimtsByCardAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_GetBonusLevelForCardResponse IVentopayMobileService.VpMob_GetBonusLevelForCard(VpMob_GetBonusLevelForCardRequest request)
    {
        return base.Channel.VpMob_GetBonusLevelForCard(request);
    }

    public VentoClient.CardStatusError_CS VpMob_GetBonusLevelForCard(string restaurantId, string cardId, out vp.mocca.app.service.core.Interfaces.BonusValue_DTO[] bonusList, out System.Guid responseCardId)
    {
        VpMob_GetBonusLevelForCardRequest inValue = new VpMob_GetBonusLevelForCardRequest();
        inValue.restaurantId = restaurantId;
        inValue.cardId = cardId;
        VpMob_GetBonusLevelForCardResponse retVal = ((IVentopayMobileService)(this)).VpMob_GetBonusLevelForCard(inValue);
        bonusList = retVal.bonusList;
        responseCardId = retVal.responseCardId;
        return retVal.VpMob_GetBonusLevelForCardResult;
    }

    public System.Threading.Tasks.Task<VpMob_GetBonusLevelForCardResponse> VpMob_GetBonusLevelForCardAsync(VpMob_GetBonusLevelForCardRequest request)
    {
        return base.Channel.VpMob_GetBonusLevelForCardAsync(request);
    }

    public VentoClient.CardStatusError_CS VpMob_ActivateBonus(string restaurantId, string syncId, string cardId, string bonusLevel_Id, System.DateTime activationDate, System.Nullable<decimal> remainingPoints)
    {
        return base.Channel.VpMob_ActivateBonus(restaurantId, syncId, cardId, bonusLevel_Id, activationDate, remainingPoints);
    }

    public System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_ActivateBonusAsync(string restaurantId, string syncId, string cardId, string bonusLevel_Id, System.DateTime activationDate, System.Nullable<decimal> remainingPoints)
    {
        return base.Channel.VpMob_ActivateBonusAsync(restaurantId, syncId, cardId, bonusLevel_Id, activationDate, remainingPoints);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_GetRestaurantResponse IVentopayMobileService.VpMob_GetRestaurant(VpMob_GetRestaurantRequest request)
    {
        return base.Channel.VpMob_GetRestaurant(request);
    }

    public VentoClient.CardStatusError_CS VpMob_GetRestaurant(out vp.mocca.app.service.core.Interfaces.RestaurantConnection_DTO[] restConnections)
    {
        VpMob_GetRestaurantRequest inValue = new VpMob_GetRestaurantRequest();
        VpMob_GetRestaurantResponse retVal = ((IVentopayMobileService)(this)).VpMob_GetRestaurant(inValue);
        restConnections = retVal.restConnections;
        return retVal.VpMob_GetRestaurantResult;
    }

    public System.Threading.Tasks.Task<VpMob_GetRestaurantResponse> VpMob_GetRestaurantAsync(VpMob_GetRestaurantRequest request)
    {
        return base.Channel.VpMob_GetRestaurantAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_GetNewsFeedResponse IVentopayMobileService.VpMob_GetNewsFeed(VpMob_GetNewsFeedRequest request)
    {
        return base.Channel.VpMob_GetNewsFeed(request);
    }

    public VentoClient.CardStatusError_CS VpMob_GetNewsFeed(string restaurantId, string cardId, string languageShortName, out VentoClient.NewsFeedItem_DTO[] newsFeedList)
    {
        VpMob_GetNewsFeedRequest inValue = new VpMob_GetNewsFeedRequest();
        inValue.restaurantId = restaurantId;
        inValue.cardId = cardId;
        inValue.languageShortName = languageShortName;
        VpMob_GetNewsFeedResponse retVal = ((IVentopayMobileService)(this)).VpMob_GetNewsFeed(inValue);
        newsFeedList = retVal.newsFeedList;
        return retVal.VpMob_GetNewsFeedResult;
    }

    public System.Threading.Tasks.Task<VpMob_GetNewsFeedResponse> VpMob_GetNewsFeedAsync(VpMob_GetNewsFeedRequest request)
    {
        return base.Channel.VpMob_GetNewsFeedAsync(request);
    }

    public VentoClient.CardStatusError_CS VpMob_UpdatePersonData(string restaurantId, VentoClient.PersonInformation_DTO personInformationDto)
    {
        return base.Channel.VpMob_UpdatePersonData(restaurantId, personInformationDto);
    }

    public System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_UpdatePersonDataAsync(string restaurantId, VentoClient.PersonInformation_DTO personInformationDto)
    {
        return base.Channel.VpMob_UpdatePersonDataAsync(restaurantId, personInformationDto);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_GetPersonInformationResponse IVentopayMobileService.VpMob_GetPersonInformation(VpMob_GetPersonInformationRequest request)
    {
        return base.Channel.VpMob_GetPersonInformation(request);
    }

    public VentoClient.CardStatusError_CS VpMob_GetPersonInformation(string restaurantId, string cardId, out VentoClient.PersonInformation_DTO personInformationDto)
    {
        VpMob_GetPersonInformationRequest inValue = new VpMob_GetPersonInformationRequest();
        inValue.restaurantId = restaurantId;
        inValue.cardId = cardId;
        VpMob_GetPersonInformationResponse retVal = ((IVentopayMobileService)(this)).VpMob_GetPersonInformation(inValue);
        personInformationDto = retVal.personInformationDto;
        return retVal.VpMob_GetPersonInformationResult;
    }

    public System.Threading.Tasks.Task<VpMob_GetPersonInformationResponse> VpMob_GetPersonInformationAsync(VpMob_GetPersonInformationRequest request)
    {
        return base.Channel.VpMob_GetPersonInformationAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_LoginResponse IVentopayMobileService.VpMob_Login(VpMob_LoginRequest request)
    {
        return base.Channel.VpMob_Login(request);
    }

    public VentoClient.CardStatusError_CS VpMob_Login(string restaurantId, string userName, string passwordHash, out VentoClient.PersonInformation_DTO personInformationDto)
    {
        VpMob_LoginRequest inValue = new VpMob_LoginRequest();
        inValue.restaurantId = restaurantId;
        inValue.userName = userName;
        inValue.passwordHash = passwordHash;
        VpMob_LoginResponse retVal = ((IVentopayMobileService)(this)).VpMob_Login(inValue);
        personInformationDto = retVal.personInformationDto;
        return retVal.VpMob_LoginResult;
    }

    public System.Threading.Tasks.Task<VpMob_LoginResponse> VpMob_LoginAsync(VpMob_LoginRequest request)
    {
        return base.Channel.VpMob_LoginAsync(request);
    }

    public VentoClient.CardStatusError_CS VpMob_ChangePassword(string restaurantId, string personId, string UserName, string oldPasswordHash, string newPasswordHash)
    {
        return base.Channel.VpMob_ChangePassword(restaurantId, personId, UserName, oldPasswordHash, newPasswordHash);
    }

    public System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_ChangePasswordAsync(string restaurantId, string personId, string UserName, string oldPasswordHash, string newPasswordHash)
    {
        return base.Channel.VpMob_ChangePasswordAsync(restaurantId, personId, UserName, oldPasswordHash, newPasswordHash);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_GetMenuResponse IVentopayMobileService.VpMob_GetMenu(VpMob_GetMenuRequest request)
    {
        return base.Channel.VpMob_GetMenu(request);
    }

    public VentoClient.CardStatusError_CS VpMob_GetMenu(string restaurantId, out string url, out byte[] pdf)
    {
        VpMob_GetMenuRequest inValue = new VpMob_GetMenuRequest();
        inValue.restaurantId = restaurantId;
        VpMob_GetMenuResponse retVal = ((IVentopayMobileService)(this)).VpMob_GetMenu(inValue);
        url = retVal.url;
        pdf = retVal.pdf;
        return retVal.VpMob_GetMenuResult;
    }

    public System.Threading.Tasks.Task<VpMob_GetMenuResponse> VpMob_GetMenuAsync(VpMob_GetMenuRequest request)
    {
        return base.Channel.VpMob_GetMenuAsync(request);
    }

    public bool VpMob_IsAlive()
    {
        return base.Channel.VpMob_IsAlive();
    }

    public System.Threading.Tasks.Task<bool> VpMob_IsAliveAsync()
    {
        return base.Channel.VpMob_IsAliveAsync();
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_LoginByCardNumberResponse IVentopayMobileService.VpMob_LoginByCardNumber(VpMob_LoginByCardNumberRequest request)
    {
        return base.Channel.VpMob_LoginByCardNumber(request);
    }

    public VentoClient.CardStatusError_CS VpMob_LoginByCardNumber(string restaurantId, string cardNumber, out VentoClient.PersonInformation_DTO personInformationDto)
    {
        VpMob_LoginByCardNumberRequest inValue = new VpMob_LoginByCardNumberRequest();
        inValue.restaurantId = restaurantId;
        inValue.cardNumber = cardNumber;
        VpMob_LoginByCardNumberResponse retVal = ((IVentopayMobileService)(this)).VpMob_LoginByCardNumber(inValue);
        personInformationDto = retVal.personInformationDto;
        return retVal.VpMob_LoginByCardNumberResult;
    }

    public System.Threading.Tasks.Task<VpMob_LoginByCardNumberResponse> VpMob_LoginByCardNumberAsync(VpMob_LoginByCardNumberRequest request)
    {
        return base.Channel.VpMob_LoginByCardNumberAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_GetAllPersonInformationResponse IVentopayMobileService.VpMob_GetAllPersonInformation(VpMob_GetAllPersonInformationRequest request)
    {
        return base.Channel.VpMob_GetAllPersonInformation(request);
    }

    public VentoClient.CardStatusError_CS VpMob_GetAllPersonInformation(string restaurantId, string personId, string filterText, int startRowIndex, int endRowIndex, out VentoClient.PersonInformation_DTO[] personInformationDto)
    {
        VpMob_GetAllPersonInformationRequest inValue = new VpMob_GetAllPersonInformationRequest();
        inValue.restaurantId = restaurantId;
        inValue.personId = personId;
        inValue.filterText = filterText;
        inValue.startRowIndex = startRowIndex;
        inValue.endRowIndex = endRowIndex;
        VpMob_GetAllPersonInformationResponse retVal = ((IVentopayMobileService)(this)).VpMob_GetAllPersonInformation(inValue);
        personInformationDto = retVal.personInformationDto;
        return retVal.VpMob_GetAllPersonInformationResult;
    }

    public System.Threading.Tasks.Task<VpMob_GetAllPersonInformationResponse> VpMob_GetAllPersonInformationAsync(VpMob_GetAllPersonInformationRequest request)
    {
        return base.Channel.VpMob_GetAllPersonInformationAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_GetApplicationSettingResponse IVentopayMobileService.VpMob_GetApplicationSetting(VpMob_GetApplicationSettingRequest request)
    {
        return base.Channel.VpMob_GetApplicationSetting(request);
    }

    public VentoClient.CardStatusError_CS VpMob_GetApplicationSetting(string restaurantId, string key, out string value)
    {
        VpMob_GetApplicationSettingRequest inValue = new VpMob_GetApplicationSettingRequest();
        inValue.restaurantId = restaurantId;
        inValue.key = key;
        VpMob_GetApplicationSettingResponse retVal = ((IVentopayMobileService)(this)).VpMob_GetApplicationSetting(inValue);
        value = retVal.value;
        return retVal.VpMob_GetApplicationSettingResult;
    }

    public System.Threading.Tasks.Task<VpMob_GetApplicationSettingResponse> VpMob_GetApplicationSettingAsync(VpMob_GetApplicationSettingRequest request)
    {
        return base.Channel.VpMob_GetApplicationSettingAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_GetAllCustomergroupInformationResponse IVentopayMobileService.VpMob_GetAllCustomergroupInformation(VpMob_GetAllCustomergroupInformationRequest request)
    {
        return base.Channel.VpMob_GetAllCustomergroupInformation(request);
    }

    public VentoClient.CardStatusError_CS VpMob_GetAllCustomergroupInformation(string restaurantId, string personId, string filterText, System.Nullable<bool> getSystemEntries, out VentoClient.CustomerGroupInformation_DTO[] customerGroupInformationDto)
    {
        VpMob_GetAllCustomergroupInformationRequest inValue = new VpMob_GetAllCustomergroupInformationRequest();
        inValue.restaurantId = restaurantId;
        inValue.personId = personId;
        inValue.filterText = filterText;
        inValue.getSystemEntries = getSystemEntries;
        VpMob_GetAllCustomergroupInformationResponse retVal = ((IVentopayMobileService)(this)).VpMob_GetAllCustomergroupInformation(inValue);
        customerGroupInformationDto = retVal.customerGroupInformationDto;
        return retVal.VpMob_GetAllCustomergroupInformationResult;
    }

    public System.Threading.Tasks.Task<VpMob_GetAllCustomergroupInformationResponse> VpMob_GetAllCustomergroupInformationAsync(VpMob_GetAllCustomergroupInformationRequest request)
    {
        return base.Channel.VpMob_GetAllCustomergroupInformationAsync(request);
    }

    public VentoClient.CardStatusError_CS VpMob_ResetPassword(string restaurantId, string mailAddress, string userName)
    {
        return base.Channel.VpMob_ResetPassword(restaurantId, mailAddress, userName);
    }

    public System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_ResetPasswordAsync(string restaurantId, string mailAddress, string userName)
    {
        return base.Channel.VpMob_ResetPasswordAsync(restaurantId, mailAddress, userName);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_GetNutrientInformationResponse IVentopayMobileService.VpMob_GetNutrientInformation(VpMob_GetNutrientInformationRequest request)
    {
        return base.Channel.VpMob_GetNutrientInformation(request);
    }

    public VentoClient.CardStatusError_CS VpMob_GetNutrientInformation(string restaurantId, System.DateTime dateFrom, System.Nullable<System.DateTime> dateUntil, string cardId, out VentoClient.NutrientInformation_DTO[] nutrientInformationDto, out VentoClient.AllergenInformation_DTO[] allergenInfo)
    {
        VpMob_GetNutrientInformationRequest inValue = new VpMob_GetNutrientInformationRequest();
        inValue.restaurantId = restaurantId;
        inValue.dateFrom = dateFrom;
        inValue.dateUntil = dateUntil;
        inValue.cardId = cardId;
        VpMob_GetNutrientInformationResponse retVal = ((IVentopayMobileService)(this)).VpMob_GetNutrientInformation(inValue);
        nutrientInformationDto = retVal.nutrientInformationDto;
        allergenInfo = retVal.allergenInfo;
        return retVal.VpMob_GetNutrientInformationResult;
    }

    public System.Threading.Tasks.Task<VpMob_GetNutrientInformationResponse> VpMob_GetNutrientInformationAsync(VpMob_GetNutrientInformationRequest request)
    {
        return base.Channel.VpMob_GetNutrientInformationAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_GetBonlinesByPersonResponse IVentopayMobileService.VpMob_GetBonlinesByPerson(VpMob_GetBonlinesByPersonRequest request)
    {
        return base.Channel.VpMob_GetBonlinesByPerson(request);
    }

    public VentoClient.CardStatusError_CS VpMob_GetBonlinesByPerson(string restaurantId, string cardId, System.DateTime dateFrom, System.DateTime dateUntil, string languageShortName, out VentoClient.Bonline_DTO[] bonLines)
    {
        VpMob_GetBonlinesByPersonRequest inValue = new VpMob_GetBonlinesByPersonRequest();
        inValue.restaurantId = restaurantId;
        inValue.cardId = cardId;
        inValue.dateFrom = dateFrom;
        inValue.dateUntil = dateUntil;
        inValue.languageShortName = languageShortName;
        VpMob_GetBonlinesByPersonResponse retVal = ((IVentopayMobileService)(this)).VpMob_GetBonlinesByPerson(inValue);
        bonLines = retVal.bonLines;
        return retVal.VpMob_GetBonlinesByPersonResult;
    }

    public System.Threading.Tasks.Task<VpMob_GetBonlinesByPersonResponse> VpMob_GetBonlinesByPersonAsync(VpMob_GetBonlinesByPersonRequest request)
    {
        return base.Channel.VpMob_GetBonlinesByPersonAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_GetContactInformationResponse IVentopayMobileService.VpMob_GetContactInformation(VpMob_GetContactInformationRequest request)
    {
        return base.Channel.VpMob_GetContactInformation(request);
    }

    public VentoClient.CardStatusError_CS VpMob_GetContactInformation(string restaurantId, out VentoClient.ContactInformation_DTO[] contactInformations)
    {
        VpMob_GetContactInformationRequest inValue = new VpMob_GetContactInformationRequest();
        inValue.restaurantId = restaurantId;
        VpMob_GetContactInformationResponse retVal = ((IVentopayMobileService)(this)).VpMob_GetContactInformation(inValue);
        contactInformations = retVal.contactInformations;
        return retVal.VpMob_GetContactInformationResult;
    }

    public System.Threading.Tasks.Task<VpMob_GetContactInformationResponse> VpMob_GetContactInformationAsync(VpMob_GetContactInformationRequest request)
    {
        return base.Channel.VpMob_GetContactInformationAsync(request);
    }

    public VentoClient.CardStatusError_CS VpMob_SendMessage(string restaurantId, int contactInformationId, string subject, string message, string replyEmailAddress, string replyTelephoneNumber, string personId)
    {
        return base.Channel.VpMob_SendMessage(restaurantId, contactInformationId, subject, message, replyEmailAddress, replyTelephoneNumber, personId);
    }

    public System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_SendMessageAsync(string restaurantId, int contactInformationId, string subject, string message, string replyEmailAddress, string replyTelephoneNumber, string personId)
    {
        return base.Channel.VpMob_SendMessageAsync(restaurantId, contactInformationId, subject, message, replyEmailAddress, replyTelephoneNumber, personId);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_GetActivityLevelsResponse IVentopayMobileService.VpMob_GetActivityLevels(VpMob_GetActivityLevelsRequest request)
    {
        return base.Channel.VpMob_GetActivityLevels(request);
    }

    public VentoClient.CardStatusError_CS VpMob_GetActivityLevels(string restaurantId, string activityLevelId, out VentoClient.ActivityLevel_DTO[] activityLevelList)
    {
        VpMob_GetActivityLevelsRequest inValue = new VpMob_GetActivityLevelsRequest();
        inValue.restaurantId = restaurantId;
        inValue.activityLevelId = activityLevelId;
        VpMob_GetActivityLevelsResponse retVal = ((IVentopayMobileService)(this)).VpMob_GetActivityLevels(inValue);
        activityLevelList = retVal.activityLevelList;
        return retVal.VpMob_GetActivityLevelsResult;
    }

    public System.Threading.Tasks.Task<VpMob_GetActivityLevelsResponse> VpMob_GetActivityLevelsAsync(VpMob_GetActivityLevelsRequest request)
    {
        return base.Channel.VpMob_GetActivityLevelsAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_GetTopUpsResponse IVentopayMobileService.VpMob_GetTopUps(VpMob_GetTopUpsRequest request)
    {
        return base.Channel.VpMob_GetTopUps(request);
    }

    public VentoClient.CardStatusError_CS VpMob_GetTopUps(string restaurantId, string cardId, out VentoClient.TopUp_DTO[] topUpList)
    {
        VpMob_GetTopUpsRequest inValue = new VpMob_GetTopUpsRequest();
        inValue.restaurantId = restaurantId;
        inValue.cardId = cardId;
        VpMob_GetTopUpsResponse retVal = ((IVentopayMobileService)(this)).VpMob_GetTopUps(inValue);
        topUpList = retVal.topUpList;
        return retVal.VpMob_GetTopUpsResult;
    }

    public System.Threading.Tasks.Task<VpMob_GetTopUpsResponse> VpMob_GetTopUpsAsync(VpMob_GetTopUpsRequest request)
    {
        return base.Channel.VpMob_GetTopUpsAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_GetPersonPictureResponse IVentopayMobileService.VpMob_GetPersonPicture(VpMob_GetPersonPictureRequest request)
    {
        return base.Channel.VpMob_GetPersonPicture(request);
    }

    public VentoClient.CardStatusError_CS VpMob_GetPersonPicture(string restaurantId, string pictureId, out System.Data.Linq.Binary picture)
    {
        VpMob_GetPersonPictureRequest inValue = new VpMob_GetPersonPictureRequest();
        inValue.restaurantId = restaurantId;
        inValue.pictureId = pictureId;
        VpMob_GetPersonPictureResponse retVal = ((IVentopayMobileService)(this)).VpMob_GetPersonPicture(inValue);
        picture = retVal.picture;
        return retVal.VpMob_GetPersonPictureResult;
    }

    public System.Threading.Tasks.Task<VpMob_GetPersonPictureResponse> VpMob_GetPersonPictureAsync(VpMob_GetPersonPictureRequest request)
    {
        return base.Channel.VpMob_GetPersonPictureAsync(request);
    }

    public VentoClient.CardStatusError_CS VpMob_SetPersonPicture(string restaurantId, string personId, System.Data.Linq.Binary picture)
    {
        return base.Channel.VpMob_SetPersonPicture(restaurantId, personId, picture);
    }

    public System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_SetPersonPictureAsync(string restaurantId, string personId, System.Data.Linq.Binary picture)
    {
        return base.Channel.VpMob_SetPersonPictureAsync(restaurantId, personId, picture);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_GetAllergensResponse IVentopayMobileService.VpMob_GetAllergens(VpMob_GetAllergensRequest request)
    {
        return base.Channel.VpMob_GetAllergens(request);
    }

    public VentoClient.CardStatusError_CS VpMob_GetAllergens(string restaurantId, System.Nullable<int> allergenId, out VentoClient.AllergenInformation_DTO[] allergenList)
    {
        VpMob_GetAllergensRequest inValue = new VpMob_GetAllergensRequest();
        inValue.restaurantId = restaurantId;
        inValue.allergenId = allergenId;
        VpMob_GetAllergensResponse retVal = ((IVentopayMobileService)(this)).VpMob_GetAllergens(inValue);
        allergenList = retVal.allergenList;
        return retVal.VpMob_GetAllergensResult;
    }

    public System.Threading.Tasks.Task<VpMob_GetAllergensResponse> VpMob_GetAllergensAsync(VpMob_GetAllergensRequest request)
    {
        return base.Channel.VpMob_GetAllergensAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_GetAllergenLegendResponse IVentopayMobileService.VpMob_GetAllergenLegend(VpMob_GetAllergenLegendRequest request)
    {
        return base.Channel.VpMob_GetAllergenLegend(request);
    }

    public VentoClient.CardStatusError_CS VpMob_GetAllergenLegend(string restaurantId, out System.Data.Linq.Binary legend)
    {
        VpMob_GetAllergenLegendRequest inValue = new VpMob_GetAllergenLegendRequest();
        inValue.restaurantId = restaurantId;
        VpMob_GetAllergenLegendResponse retVal = ((IVentopayMobileService)(this)).VpMob_GetAllergenLegend(inValue);
        legend = retVal.legend;
        return retVal.VpMob_GetAllergenLegendResult;
    }

    public System.Threading.Tasks.Task<VpMob_GetAllergenLegendResponse> VpMob_GetAllergenLegendAsync(VpMob_GetAllergenLegendRequest request)
    {
        return base.Channel.VpMob_GetAllergenLegendAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_GetFeedbackProgramResponse IVentopayMobileService.VpMob_GetFeedbackProgram(VpMob_GetFeedbackProgramRequest request)
    {
        return base.Channel.VpMob_GetFeedbackProgram(request);
    }

    public VentoClient.CardStatusError_CS VpMob_GetFeedbackProgram(string restaurantId, string cardId, string articleId, string languageShortName, out VentoClient.FeedbackProgram_DTO[] programm)
    {
        VpMob_GetFeedbackProgramRequest inValue = new VpMob_GetFeedbackProgramRequest();
        inValue.restaurantId = restaurantId;
        inValue.cardId = cardId;
        inValue.articleId = articleId;
        inValue.languageShortName = languageShortName;
        VpMob_GetFeedbackProgramResponse retVal = ((IVentopayMobileService)(this)).VpMob_GetFeedbackProgram(inValue);
        programm = retVal.programm;
        return retVal.VpMob_GetFeedbackProgramResult;
    }

    public System.Threading.Tasks.Task<VpMob_GetFeedbackProgramResponse> VpMob_GetFeedbackProgramAsync(VpMob_GetFeedbackProgramRequest request)
    {
        return base.Channel.VpMob_GetFeedbackProgramAsync(request);
    }

    public VentoClient.CardStatusError_CS VpMob_SetFeedbackAnswer(string restaurantId, string cardId, VentoClient.FeedbackPersonAnswer_DTO[] answer)
    {
        return base.Channel.VpMob_SetFeedbackAnswer(restaurantId, cardId, answer);
    }

    public System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_SetFeedbackAnswerAsync(string restaurantId, string cardId, VentoClient.FeedbackPersonAnswer_DTO[] answer)
    {
        return base.Channel.VpMob_SetFeedbackAnswerAsync(restaurantId, cardId, answer);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_MergeAccountsResponse IVentopayMobileService.VpMob_MergeAccounts(VpMob_MergeAccountsRequest request)
    {
        return base.Channel.VpMob_MergeAccounts(request);
    }

    public VentoClient.CardStatusError_CS VpMob_MergeAccounts(string restaurantId, string personId, string personIdToBeMerged, out VentoClient.PersonInformation_DTO personInformationDto)
    {
        VpMob_MergeAccountsRequest inValue = new VpMob_MergeAccountsRequest();
        inValue.restaurantId = restaurantId;
        inValue.personId = personId;
        inValue.personIdToBeMerged = personIdToBeMerged;
        VpMob_MergeAccountsResponse retVal = ((IVentopayMobileService)(this)).VpMob_MergeAccounts(inValue);
        personInformationDto = retVal.personInformationDto;
        return retVal.VpMob_MergeAccountsResult;
    }

    public System.Threading.Tasks.Task<VpMob_MergeAccountsResponse> VpMob_MergeAccountsAsync(VpMob_MergeAccountsRequest request)
    {
        return base.Channel.VpMob_MergeAccountsAsync(request);
    }

    public VentoClient.CardStatusError_CS VpMob_ResetPersonToStandardGroup(string restaurantId, string personId)
    {
        return base.Channel.VpMob_ResetPersonToStandardGroup(restaurantId, personId);
    }

    public System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_ResetPersonToStandardGroupAsync(string restaurantId, string personId)
    {
        return base.Channel.VpMob_ResetPersonToStandardGroupAsync(restaurantId, personId);
    }

    public VentoClient.CardStatusError_CS VpMob_UpdateBonline(string restaurantId, VentoClient.Bonline_DTO bonline)
    {
        return base.Channel.VpMob_UpdateBonline(restaurantId, bonline);
    }

    public System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_UpdateBonlineAsync(string restaurantId, VentoClient.Bonline_DTO bonline)
    {
        return base.Channel.VpMob_UpdateBonlineAsync(restaurantId, bonline);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_GetTextResponse IVentopayMobileService.VpMob_GetText(VpMob_GetTextRequest request)
    {
        return base.Channel.VpMob_GetText(request);
    }

    public VentoClient.CardStatusError_CS VpMob_GetText(string restaurantId, string languageShortName, string cashPointTypeShortName, string textKey, out System.Collections.Generic.Dictionary<string, string> values)
    {
        VpMob_GetTextRequest inValue = new VpMob_GetTextRequest();
        inValue.restaurantId = restaurantId;
        inValue.languageShortName = languageShortName;
        inValue.cashPointTypeShortName = cashPointTypeShortName;
        inValue.textKey = textKey;
        VpMob_GetTextResponse retVal = ((IVentopayMobileService)(this)).VpMob_GetText(inValue);
        values = retVal.values;
        return retVal.VpMob_GetTextResult;
    }

    public System.Threading.Tasks.Task<VpMob_GetTextResponse> VpMob_GetTextAsync(VpMob_GetTextRequest request)
    {
        return base.Channel.VpMob_GetTextAsync(request);
    }

    public VentoClient.CardStatusError_CS VpMob_ActivateNewsFeed(string restaurantId, int newsFeedId, string personId)
    {
        return base.Channel.VpMob_ActivateNewsFeed(restaurantId, newsFeedId, personId);
    }

    public System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_ActivateNewsFeedAsync(string restaurantId, int newsFeedId, string personId)
    {
        return base.Channel.VpMob_ActivateNewsFeedAsync(restaurantId, newsFeedId, personId);
    }

    public VentoClient.CardStatusError_CS VpMob_ChangeTermsAcceptanceState(string restaurantId, string personId, System.DateTime deviceDate, string termShortName, bool isAccepted)
    {
        return base.Channel.VpMob_ChangeTermsAcceptanceState(restaurantId, personId, deviceDate, termShortName, isAccepted);
    }

    public System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_ChangeTermsAcceptanceStateAsync(string restaurantId, string personId, System.DateTime deviceDate, string termShortName, bool isAccepted)
    {
        return base.Channel.VpMob_ChangeTermsAcceptanceStateAsync(restaurantId, personId, deviceDate, termShortName, isAccepted);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_GetLastTermStatesByPersonResponse IVentopayMobileService.VpMob_GetLastTermStatesByPerson(VpMob_GetLastTermStatesByPersonRequest request)
    {
        return base.Channel.VpMob_GetLastTermStatesByPerson(request);
    }

    public VentoClient.CardStatusError_CS VpMob_GetLastTermStatesByPerson(string restaurantId, string personId, out VentoClient.TermState_DTO[] lastTermStates)
    {
        VpMob_GetLastTermStatesByPersonRequest inValue = new VpMob_GetLastTermStatesByPersonRequest();
        inValue.restaurantId = restaurantId;
        inValue.personId = personId;
        VpMob_GetLastTermStatesByPersonResponse retVal = ((IVentopayMobileService)(this)).VpMob_GetLastTermStatesByPerson(inValue);
        lastTermStates = retVal.lastTermStates;
        return retVal.VpMob_GetLastTermStatesByPersonResult;
    }

    public System.Threading.Tasks.Task<VpMob_GetLastTermStatesByPersonResponse> VpMob_GetLastTermStatesByPersonAsync(VpMob_GetLastTermStatesByPersonRequest request)
    {
        return base.Channel.VpMob_GetLastTermStatesByPersonAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_LoginByPersonNumberResponse IVentopayMobileService.VpMob_LoginByPersonNumber(VpMob_LoginByPersonNumberRequest request)
    {
        return base.Channel.VpMob_LoginByPersonNumber(request);
    }

    public VentoClient.CardStatusError_CS VpMob_LoginByPersonNumber(string restaurantId, string personNumber, out VentoClient.PersonInformation_DTO personInformationDto)
    {
        VpMob_LoginByPersonNumberRequest inValue = new VpMob_LoginByPersonNumberRequest();
        inValue.restaurantId = restaurantId;
        inValue.personNumber = personNumber;
        VpMob_LoginByPersonNumberResponse retVal = ((IVentopayMobileService)(this)).VpMob_LoginByPersonNumber(inValue);
        personInformationDto = retVal.personInformationDto;
        return retVal.VpMob_LoginByPersonNumberResult;
    }

    public System.Threading.Tasks.Task<VpMob_LoginByPersonNumberResponse> VpMob_LoginByPersonNumberAsync(VpMob_LoginByPersonNumberRequest request)
    {
        return base.Channel.VpMob_LoginByPersonNumberAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_GetAvailableLanguagesResponse IVentopayMobileService.VpMob_GetAvailableLanguages(VpMob_GetAvailableLanguagesRequest request)
    {
        return base.Channel.VpMob_GetAvailableLanguages(request);
    }

    public VentoClient.CardStatusError_CS VpMob_GetAvailableLanguages(out VentoClient.Language_DTO[] availableLanguages)
    {
        VpMob_GetAvailableLanguagesRequest inValue = new VpMob_GetAvailableLanguagesRequest();
        VpMob_GetAvailableLanguagesResponse retVal = ((IVentopayMobileService)(this)).VpMob_GetAvailableLanguages(inValue);
        availableLanguages = retVal.availableLanguages;
        return retVal.VpMob_GetAvailableLanguagesResult;
    }

    public System.Threading.Tasks.Task<VpMob_GetAvailableLanguagesResponse> VpMob_GetAvailableLanguagesAsync(VpMob_GetAvailableLanguagesRequest request)
    {
        return base.Channel.VpMob_GetAvailableLanguagesAsync(request);
    }

    public VentoClient.CardStatusError_CS VpMob_ResendMailActivation(string restaurantId, string personId)
    {
        return base.Channel.VpMob_ResendMailActivation(restaurantId, personId);
    }

    public System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_ResendMailActivationAsync(string restaurantId, string personId)
    {
        return base.Channel.VpMob_ResendMailActivationAsync(restaurantId, personId);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_LoginByAccountDetailResponse IVentopayMobileService.VpMob_LoginByAccountDetail(VpMob_LoginByAccountDetailRequest request)
    {
        return base.Channel.VpMob_LoginByAccountDetail(request);
    }

    public VentoClient.CardStatusError_CS VpMob_LoginByAccountDetail(string restaurantId, string personAccountDetailAttribute, string personAccountDetailAttributeValue, out VentoClient.PersonInformation_DTO personInformationDto)
    {
        VpMob_LoginByAccountDetailRequest inValue = new VpMob_LoginByAccountDetailRequest();
        inValue.restaurantId = restaurantId;
        inValue.personAccountDetailAttribute = personAccountDetailAttribute;
        inValue.personAccountDetailAttributeValue = personAccountDetailAttributeValue;
        VpMob_LoginByAccountDetailResponse retVal = ((IVentopayMobileService)(this)).VpMob_LoginByAccountDetail(inValue);
        personInformationDto = retVal.personInformationDto;
        return retVal.VpMob_LoginByAccountDetailResult;
    }

    public System.Threading.Tasks.Task<VpMob_LoginByAccountDetailResponse> VpMob_LoginByAccountDetailAsync(VpMob_LoginByAccountDetailRequest request)
    {
        return base.Channel.VpMob_LoginByAccountDetailAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_GetBonlinesByPersonWithExcludedCashpointsResponse IVentopayMobileService.VpMob_GetBonlinesByPersonWithExcludedCashpoints(VpMob_GetBonlinesByPersonWithExcludedCashpointsRequest request)
    {
        return base.Channel.VpMob_GetBonlinesByPersonWithExcludedCashpoints(request);
    }

    public VentoClient.CardStatusError_CS VpMob_GetBonlinesByPersonWithExcludedCashpoints(string restaurantId, string cardId, System.DateTime dateFrom, System.DateTime dateUntil, string languageShortName, out VentoClient.Bonline_DTO[] bonLines)
    {
        VpMob_GetBonlinesByPersonWithExcludedCashpointsRequest inValue = new VpMob_GetBonlinesByPersonWithExcludedCashpointsRequest();
        inValue.restaurantId = restaurantId;
        inValue.cardId = cardId;
        inValue.dateFrom = dateFrom;
        inValue.dateUntil = dateUntil;
        inValue.languageShortName = languageShortName;
        VpMob_GetBonlinesByPersonWithExcludedCashpointsResponse retVal = ((IVentopayMobileService)(this)).VpMob_GetBonlinesByPersonWithExcludedCashpoints(inValue);
        bonLines = retVal.bonLines;
        return retVal.VpMob_GetBonlinesByPersonWithExcludedCashpointsResult;
    }

    public System.Threading.Tasks.Task<VpMob_GetBonlinesByPersonWithExcludedCashpointsResponse> VpMob_GetBonlinesByPersonWithExcludedCashpointsAsync(VpMob_GetBonlinesByPersonWithExcludedCashpointsRequest request)
    {
        return base.Channel.VpMob_GetBonlinesByPersonWithExcludedCashpointsAsync(request);
    }

    public VentoClient.CardStatusError_CS VpMob_ChangePin(string restaurantId, string personId, string newPin)
    {
        return base.Channel.VpMob_ChangePin(restaurantId, personId, newPin);
    }

    public System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_ChangePinAsync(string restaurantId, string personId, string newPin)
    {
        return base.Channel.VpMob_ChangePinAsync(restaurantId, personId, newPin);
    }

    public VentoClient.CardStatusError_CS VpMob_Log(string restaurantId, VentoClient.Log_DTO logDto)
    {
        return base.Channel.VpMob_Log(restaurantId, logDto);
    }

    public System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_LogAsync(string restaurantId, VentoClient.Log_DTO logDto)
    {
        return base.Channel.VpMob_LogAsync(restaurantId, logDto);
    }

    public VentoClient.CardStatusError_CS VpMob_PayoutSaldo(string restaurantId, string cardId, string iban, string bic, string lastname, int saldoInCent)
    {
        return base.Channel.VpMob_PayoutSaldo(restaurantId, cardId, iban, bic, lastname, saldoInCent);
    }

    public System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_PayoutSaldoAsync(string restaurantId, string cardId, string iban, string bic, string lastname, int saldoInCent)
    {
        return base.Channel.VpMob_PayoutSaldoAsync(restaurantId, cardId, iban, bic, lastname, saldoInCent);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_DoPaymentResponse IVentopayMobileService.VpMob_DoPayment(VpMob_DoPaymentRequest request)
    {
        return base.Channel.VpMob_DoPayment(request);
    }

    public VentoClient.CardStatusError_CS VpMob_DoPayment(string restaurantId, string cardId, int amountInCent, string redirectUrl, out string paymentUrl)
    {
        VpMob_DoPaymentRequest inValue = new VpMob_DoPaymentRequest();
        inValue.restaurantId = restaurantId;
        inValue.cardId = cardId;
        inValue.amountInCent = amountInCent;
        inValue.redirectUrl = redirectUrl;
        VpMob_DoPaymentResponse retVal = ((IVentopayMobileService)(this)).VpMob_DoPayment(inValue);
        paymentUrl = retVal.paymentUrl;
        return retVal.VpMob_DoPaymentResult;
    }

    public System.Threading.Tasks.Task<VpMob_DoPaymentResponse> VpMob_DoPaymentAsync(VpMob_DoPaymentRequest request)
    {
        return base.Channel.VpMob_DoPaymentAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_DoMobilePaymentResponse IVentopayMobileService.VpMob_DoMobilePayment(VpMob_DoMobilePaymentRequest request)
    {
        return base.Channel.VpMob_DoMobilePayment(request);
    }

    public VentoClient.CardStatusError_CS VpMob_DoMobilePayment(string restaurantId, string cardId, int amountInCent, string redirectUrl, VentoClient.PaymentType_CS paymentType, string paymentToken, out string paymentUrl)
    {
        VpMob_DoMobilePaymentRequest inValue = new VpMob_DoMobilePaymentRequest();
        inValue.restaurantId = restaurantId;
        inValue.cardId = cardId;
        inValue.amountInCent = amountInCent;
        inValue.redirectUrl = redirectUrl;
        inValue.paymentType = paymentType;
        inValue.paymentToken = paymentToken;
        VpMob_DoMobilePaymentResponse retVal = ((IVentopayMobileService)(this)).VpMob_DoMobilePayment(inValue);
        paymentUrl = retVal.paymentUrl;
        return retVal.VpMob_DoMobilePaymentResult;
    }

    public System.Threading.Tasks.Task<VpMob_DoMobilePaymentResponse> VpMob_DoMobilePaymentAsync(VpMob_DoMobilePaymentRequest request)
    {
        return base.Channel.VpMob_DoMobilePaymentAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_DoAuthorizationResponse IVentopayMobileService.VpMob_DoAuthorization(VpMob_DoAuthorizationRequest request)
    {
        return base.Channel.VpMob_DoAuthorization(request);
    }

    public VentoClient.CardStatusError_CS VpMob_DoAuthorization(string restaurantId, string cardId, int reloadValueInCent, int minimumSaldoInCent, string redirectUrl, out string paymentUrl)
    {
        VpMob_DoAuthorizationRequest inValue = new VpMob_DoAuthorizationRequest();
        inValue.restaurantId = restaurantId;
        inValue.cardId = cardId;
        inValue.reloadValueInCent = reloadValueInCent;
        inValue.minimumSaldoInCent = minimumSaldoInCent;
        inValue.redirectUrl = redirectUrl;
        VpMob_DoAuthorizationResponse retVal = ((IVentopayMobileService)(this)).VpMob_DoAuthorization(inValue);
        paymentUrl = retVal.paymentUrl;
        return retVal.VpMob_DoAuthorizationResult;
    }

    public System.Threading.Tasks.Task<VpMob_DoAuthorizationResponse> VpMob_DoAuthorizationAsync(VpMob_DoAuthorizationRequest request)
    {
        return base.Channel.VpMob_DoAuthorizationAsync(request);
    }

    public VentoClient.CardStatusError_CS VpMob_RegisterUser(string restaurantId, VentoClient.PersonInformation_DTO personInformationDto, System.Data.Linq.Binary personPicture)
    {
        return base.Channel.VpMob_RegisterUser(restaurantId, personInformationDto, personPicture);
    }

    public System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_RegisterUserAsync(string restaurantId, VentoClient.PersonInformation_DTO personInformationDto, System.Data.Linq.Binary personPicture)
    {
        return base.Channel.VpMob_RegisterUserAsync(restaurantId, personInformationDto, personPicture);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_GetReportDataResponse IVentopayMobileService.VpMob_GetReportData(VpMob_GetReportDataRequest request)
    {
        return base.Channel.VpMob_GetReportData(request);
    }

    public VentoClient.CardStatusError_CS VpMob_GetReportData(string restaurantId, string personId, System.DateTime dateFrom, System.DateTime dateUntil, string languageShortName, System.Nullable<int> cashpointNumberFrom, System.Nullable<int> cashpointNumberUntil, out byte[] binaryReport)
    {
        VpMob_GetReportDataRequest inValue = new VpMob_GetReportDataRequest();
        inValue.restaurantId = restaurantId;
        inValue.personId = personId;
        inValue.dateFrom = dateFrom;
        inValue.dateUntil = dateUntil;
        inValue.languageShortName = languageShortName;
        inValue.cashpointNumberFrom = cashpointNumberFrom;
        inValue.cashpointNumberUntil = cashpointNumberUntil;
        VpMob_GetReportDataResponse retVal = ((IVentopayMobileService)(this)).VpMob_GetReportData(inValue);
        binaryReport = retVal.binaryReport;
        return retVal.VpMob_GetReportDataResult;
    }

    public System.Threading.Tasks.Task<VpMob_GetReportDataResponse> VpMob_GetReportDataAsync(VpMob_GetReportDataRequest request)
    {
        return base.Channel.VpMob_GetReportDataAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_NotifyModuleResponse IVentopayMobileService.VpMob_NotifyModule(VpMob_NotifyModuleRequest request)
    {
        return base.Channel.VpMob_NotifyModule(request);
    }

    public VentoClient.CardStatusError_CS VpMob_NotifyModule(string restaurantId, string[] customerGroupNumbers, string title, string message, int moduleId, out VentoClient.DeviceToken_DTO[] deviceTokens)
    {
        VpMob_NotifyModuleRequest inValue = new VpMob_NotifyModuleRequest();
        inValue.restaurantId = restaurantId;
        inValue.customerGroupNumbers = customerGroupNumbers;
        inValue.title = title;
        inValue.message = message;
        inValue.moduleId = moduleId;
        VpMob_NotifyModuleResponse retVal = ((IVentopayMobileService)(this)).VpMob_NotifyModule(inValue);
        deviceTokens = retVal.deviceTokens;
        return retVal.VpMob_NotifyModuleResult;
    }

    public System.Threading.Tasks.Task<VpMob_NotifyModuleResponse> VpMob_NotifyModuleAsync(VpMob_NotifyModuleRequest request)
    {
        return base.Channel.VpMob_NotifyModuleAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_NotifyNewsfeedResponse IVentopayMobileService.VpMob_NotifyNewsfeed(VpMob_NotifyNewsfeedRequest request)
    {
        return base.Channel.VpMob_NotifyNewsfeed(request);
    }

    public VentoClient.CardStatusError_CS VpMob_NotifyNewsfeed(string restaurantId, string[] customerGroupNumbers, string title, string message, int newsfeedId, out VentoClient.DeviceToken_DTO[] deviceTokens)
    {
        VpMob_NotifyNewsfeedRequest inValue = new VpMob_NotifyNewsfeedRequest();
        inValue.restaurantId = restaurantId;
        inValue.customerGroupNumbers = customerGroupNumbers;
        inValue.title = title;
        inValue.message = message;
        inValue.newsfeedId = newsfeedId;
        VpMob_NotifyNewsfeedResponse retVal = ((IVentopayMobileService)(this)).VpMob_NotifyNewsfeed(inValue);
        deviceTokens = retVal.deviceTokens;
        return retVal.VpMob_NotifyNewsfeedResult;
    }

    public System.Threading.Tasks.Task<VpMob_NotifyNewsfeedResponse> VpMob_NotifyNewsfeedAsync(VpMob_NotifyNewsfeedRequest request)
    {
        return base.Channel.VpMob_NotifyNewsfeedAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_NotifyFeedbackResponse IVentopayMobileService.VpMob_NotifyFeedback(VpMob_NotifyFeedbackRequest request)
    {
        return base.Channel.VpMob_NotifyFeedback(request);
    }

    public VentoClient.CardStatusError_CS VpMob_NotifyFeedback(string restaurantId, string[] customerGroupNumbers, string title, string message, int feedbackId, out VentoClient.DeviceToken_DTO[] deviceTokens)
    {
        VpMob_NotifyFeedbackRequest inValue = new VpMob_NotifyFeedbackRequest();
        inValue.restaurantId = restaurantId;
        inValue.customerGroupNumbers = customerGroupNumbers;
        inValue.title = title;
        inValue.message = message;
        inValue.feedbackId = feedbackId;
        VpMob_NotifyFeedbackResponse retVal = ((IVentopayMobileService)(this)).VpMob_NotifyFeedback(inValue);
        deviceTokens = retVal.deviceTokens;
        return retVal.VpMob_NotifyFeedbackResult;
    }

    public System.Threading.Tasks.Task<VpMob_NotifyFeedbackResponse> VpMob_NotifyFeedbackAsync(VpMob_NotifyFeedbackRequest request)
    {
        return base.Channel.VpMob_NotifyFeedbackAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_GetBleEnabledCashpointsResponse IVentopayMobileService.VpMob_GetBleEnabledCashpoints(VpMob_GetBleEnabledCashpointsRequest request)
    {
        return base.Channel.VpMob_GetBleEnabledCashpoints(request);
    }

    public VentoClient.CardStatusError_CS VpMob_GetBleEnabledCashpoints(string restaurantId, string cardId, out VentoClient.Cashpoint_DTO[] cashpoints)
    {
        VpMob_GetBleEnabledCashpointsRequest inValue = new VpMob_GetBleEnabledCashpointsRequest();
        inValue.restaurantId = restaurantId;
        inValue.cardId = cardId;
        VpMob_GetBleEnabledCashpointsResponse retVal = ((IVentopayMobileService)(this)).VpMob_GetBleEnabledCashpoints(inValue);
        cashpoints = retVal.cashpoints;
        return retVal.VpMob_GetBleEnabledCashpointsResult;
    }

    public System.Threading.Tasks.Task<VpMob_GetBleEnabledCashpointsResponse> VpMob_GetBleEnabledCashpointsAsync(VpMob_GetBleEnabledCashpointsRequest request)
    {
        return base.Channel.VpMob_GetBleEnabledCashpointsAsync(request);
    }

    public VentoClient.CardStatusError_CS VpMob_EnablePayment(string restaurantId, string cardId, string cashpointId)
    {
        return base.Channel.VpMob_EnablePayment(restaurantId, cardId, cashpointId);
    }

    public System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_EnablePaymentAsync(string restaurantId, string cardId, string cashpointId)
    {
        return base.Channel.VpMob_EnablePaymentAsync(restaurantId, cardId, cashpointId);
    }

    public VentoClient.CardStatusError_CS VpMob_CancelPayment(string restaurantId, string cardId, string cashpointId)
    {
        return base.Channel.VpMob_CancelPayment(restaurantId, cardId, cashpointId);
    }

    public System.Threading.Tasks.Task<VentoClient.CardStatusError_CS> VpMob_CancelPaymentAsync(string restaurantId, string cardId, string cashpointId)
    {
        return base.Channel.VpMob_CancelPaymentAsync(restaurantId, cardId, cashpointId);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_IsPaymentActiveResponse IVentopayMobileService.VpMob_IsPaymentActive(VpMob_IsPaymentActiveRequest request)
    {
        return base.Channel.VpMob_IsPaymentActive(request);
    }

    public VentoClient.CardStatusError_CS VpMob_IsPaymentActive(string restaurantId, string cardId, string cashpointId, out bool active)
    {
        VpMob_IsPaymentActiveRequest inValue = new VpMob_IsPaymentActiveRequest();
        inValue.restaurantId = restaurantId;
        inValue.cardId = cardId;
        inValue.cashpointId = cashpointId;
        VpMob_IsPaymentActiveResponse retVal = ((IVentopayMobileService)(this)).VpMob_IsPaymentActive(inValue);
        active = retVal.active;
        return retVal.VpMob_IsPaymentActiveResult;
    }

    public System.Threading.Tasks.Task<VpMob_IsPaymentActiveResponse> VpMob_IsPaymentActiveAsync(VpMob_IsPaymentActiveRequest request)
    {
        return base.Channel.VpMob_IsPaymentActiveAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_GetBonByBonIdResponse IVentopayMobileService.VpMob_GetBonByBonId(VpMob_GetBonByBonIdRequest request)
    {
        return base.Channel.VpMob_GetBonByBonId(request);
    }

    public VentoClient.CardStatusError_CS VpMob_GetBonByBonId(string restaurantId, string bonId, out VentoClient.Bon_DTO bon)
    {
        VpMob_GetBonByBonIdRequest inValue = new VpMob_GetBonByBonIdRequest();
        inValue.restaurantId = restaurantId;
        inValue.bonId = bonId;
        VpMob_GetBonByBonIdResponse retVal = ((IVentopayMobileService)(this)).VpMob_GetBonByBonId(inValue);
        bon = retVal.bon;
        return retVal.VpMob_GetBonByBonIdResult;
    }

    public System.Threading.Tasks.Task<VpMob_GetBonByBonIdResponse> VpMob_GetBonByBonIdAsync(VpMob_GetBonByBonIdRequest request)
    {
        return base.Channel.VpMob_GetBonByBonIdAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMob_GetBrandingResponse IVentopayMobileService.VpMob_GetBranding(VpMob_GetBrandingRequest request)
    {
        return base.Channel.VpMob_GetBranding(request);
    }

    public VentoClient.CardStatusError_CS VpMob_GetBranding(string restaurantId, out VentoClient.Branding_DTO branding)
    {
        VpMob_GetBrandingRequest inValue = new VpMob_GetBrandingRequest();
        inValue.restaurantId = restaurantId;
        VpMob_GetBrandingResponse retVal = ((IVentopayMobileService)(this)).VpMob_GetBranding(inValue);
        branding = retVal.branding;
        return retVal.VpMob_GetBrandingResult;
    }

    public System.Threading.Tasks.Task<VpMob_GetBrandingResponse> VpMob_GetBrandingAsync(VpMob_GetBrandingRequest request)
    {
        return base.Channel.VpMob_GetBrandingAsync(request);
    }
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName = "IVentopayTableRoutingService")]
public interface IVentopayTableRoutingService
{

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayTableRoutingService/GetTableInformation", ReplyAction = "http://tempuri.org/IVentopayTableRoutingService/GetTableInformationResponse")]
    GetTableInformationResponse GetTableInformation(GetTableInformationRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayTableRoutingService/GetTableInformation", ReplyAction = "http://tempuri.org/IVentopayTableRoutingService/GetTableInformationResponse")]
    System.Threading.Tasks.Task<GetTableInformationResponse> GetTableInformationAsync(GetTableInformationRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayTableRoutingService/GetTableInformationNew", ReplyAction = "http://tempuri.org/IVentopayTableRoutingService/GetTableInformationNewResponse")]
    GetTableInformationNewResponse GetTableInformationNew(GetTableInformationNewRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayTableRoutingService/GetTableInformationNew", ReplyAction = "http://tempuri.org/IVentopayTableRoutingService/GetTableInformationNewResponse")]
    System.Threading.Tasks.Task<GetTableInformationNewResponse> GetTableInformationNewAsync(GetTableInformationNewRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayTableRoutingService/GetTableAreas", ReplyAction = "http://tempuri.org/IVentopayTableRoutingService/GetTableAreasResponse")]
    GetTableAreasResponse GetTableAreas(GetTableAreasRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayTableRoutingService/GetTableAreas", ReplyAction = "http://tempuri.org/IVentopayTableRoutingService/GetTableAreasResponse")]
    System.Threading.Tasks.Task<GetTableAreasResponse> GetTableAreasAsync(GetTableAreasRequest request);
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "GetTableInformation", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class GetTableInformationRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public int tableArea;

    public GetTableInformationRequest()
    {
    }

    public GetTableInformationRequest(int tableArea)
    {
        this.tableArea = tableArea;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "GetTableInformationResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class GetTableInformationResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public vp.mocca.app.service.core.Interfaces.TableRoutingError GetTableInformationResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public vp.mocca.app.service.core.Interfaces.TableStatus tableStatus;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public int estimatedFreeChairs;

    public GetTableInformationResponse()
    {
    }

    public GetTableInformationResponse(vp.mocca.app.service.core.Interfaces.TableRoutingError GetTableInformationResult, vp.mocca.app.service.core.Interfaces.TableStatus tableStatus, int estimatedFreeChairs)
    {
        this.GetTableInformationResult = GetTableInformationResult;
        this.tableStatus = tableStatus;
        this.estimatedFreeChairs = estimatedFreeChairs;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "GetTableInformationNew", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class GetTableInformationNewRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public int tableArea;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public System.Guid restaurantId;

    public GetTableInformationNewRequest()
    {
    }

    public GetTableInformationNewRequest(int tableArea, System.Guid restaurantId)
    {
        this.tableArea = tableArea;
        this.restaurantId = restaurantId;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "GetTableInformationNewResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class GetTableInformationNewResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public vp.mocca.app.service.core.Interfaces.TableRoutingError GetTableInformationNewResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public vp.mocca.app.service.core.Interfaces.TableStatus tableStatus;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public int estimatedFreeChairs;

    public GetTableInformationNewResponse()
    {
    }

    public GetTableInformationNewResponse(vp.mocca.app.service.core.Interfaces.TableRoutingError GetTableInformationNewResult, vp.mocca.app.service.core.Interfaces.TableStatus tableStatus, int estimatedFreeChairs)
    {
        this.GetTableInformationNewResult = GetTableInformationNewResult;
        this.tableStatus = tableStatus;
        this.estimatedFreeChairs = estimatedFreeChairs;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "GetTableAreas", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class GetTableAreasRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    public GetTableAreasRequest()
    {
    }

    public GetTableAreasRequest(string restaurantId)
    {
        this.restaurantId = restaurantId;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "GetTableAreasResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class GetTableAreasResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public vp.mocca.app.service.core.Interfaces.TableRoutingError GetTableAreasResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public vp.mocca.app.service.core.Interfaces.TableStatus_DTO[] tableStatus;

    public GetTableAreasResponse()
    {
    }

    public GetTableAreasResponse(vp.mocca.app.service.core.Interfaces.TableRoutingError GetTableAreasResult, vp.mocca.app.service.core.Interfaces.TableStatus_DTO[] tableStatus)
    {
        this.GetTableAreasResult = GetTableAreasResult;
        this.tableStatus = tableStatus;
    }
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface IVentopayTableRoutingServiceChannel : IVentopayTableRoutingService, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class VentopayTableRoutingServiceClient : System.ServiceModel.ClientBase<IVentopayTableRoutingService>, IVentopayTableRoutingService
{

    public VentopayTableRoutingServiceClient()
    {
    }

    public VentopayTableRoutingServiceClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
    {
    }

    public VentopayTableRoutingServiceClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
    {
    }

    public VentopayTableRoutingServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
    {
    }

    public VentopayTableRoutingServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
    {
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    GetTableInformationResponse IVentopayTableRoutingService.GetTableInformation(GetTableInformationRequest request)
    {
        return base.Channel.GetTableInformation(request);
    }

    public vp.mocca.app.service.core.Interfaces.TableRoutingError GetTableInformation(int tableArea, out vp.mocca.app.service.core.Interfaces.TableStatus tableStatus, out int estimatedFreeChairs)
    {
        GetTableInformationRequest inValue = new GetTableInformationRequest();
        inValue.tableArea = tableArea;
        GetTableInformationResponse retVal = ((IVentopayTableRoutingService)(this)).GetTableInformation(inValue);
        tableStatus = retVal.tableStatus;
        estimatedFreeChairs = retVal.estimatedFreeChairs;
        return retVal.GetTableInformationResult;
    }

    public System.Threading.Tasks.Task<GetTableInformationResponse> GetTableInformationAsync(GetTableInformationRequest request)
    {
        return base.Channel.GetTableInformationAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    GetTableInformationNewResponse IVentopayTableRoutingService.GetTableInformationNew(GetTableInformationNewRequest request)
    {
        return base.Channel.GetTableInformationNew(request);
    }

    public vp.mocca.app.service.core.Interfaces.TableRoutingError GetTableInformationNew(int tableArea, System.Guid restaurantId, out vp.mocca.app.service.core.Interfaces.TableStatus tableStatus, out int estimatedFreeChairs)
    {
        GetTableInformationNewRequest inValue = new GetTableInformationNewRequest();
        inValue.tableArea = tableArea;
        inValue.restaurantId = restaurantId;
        GetTableInformationNewResponse retVal = ((IVentopayTableRoutingService)(this)).GetTableInformationNew(inValue);
        tableStatus = retVal.tableStatus;
        estimatedFreeChairs = retVal.estimatedFreeChairs;
        return retVal.GetTableInformationNewResult;
    }

    public System.Threading.Tasks.Task<GetTableInformationNewResponse> GetTableInformationNewAsync(GetTableInformationNewRequest request)
    {
        return base.Channel.GetTableInformationNewAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    GetTableAreasResponse IVentopayTableRoutingService.GetTableAreas(GetTableAreasRequest request)
    {
        return base.Channel.GetTableAreas(request);
    }

    public vp.mocca.app.service.core.Interfaces.TableRoutingError GetTableAreas(string restaurantId, out vp.mocca.app.service.core.Interfaces.TableStatus_DTO[] tableStatus)
    {
        GetTableAreasRequest inValue = new GetTableAreasRequest();
        inValue.restaurantId = restaurantId;
        GetTableAreasResponse retVal = ((IVentopayTableRoutingService)(this)).GetTableAreas(inValue);
        tableStatus = retVal.tableStatus;
        return retVal.GetTableAreasResult;
    }

    public System.Threading.Tasks.Task<GetTableAreasResponse> GetTableAreasAsync(GetTableAreasRequest request)
    {
        return base.Channel.GetTableAreasAsync(request);
    }
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName = "IVentopayFileService")]
public interface IVentopayFileService
{

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayService/IsAlive", ReplyAction = "http://tempuri.org/IVentopayService/IsAliveResponse")]
    bool IsAlive();

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayService/IsAlive", ReplyAction = "http://tempuri.org/IVentopayService/IsAliveResponse")]
    System.Threading.Tasks.Task<bool> IsAliveAsync();

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayFileService/GetFileListFromDirectory", ReplyAction = "http://tempuri.org/IVentopayFileService/GetFileListFromDirectoryResponse")]
    GetFileListFromDirectoryResponse GetFileListFromDirectory(GetFileListFromDirectoryRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayFileService/GetFileListFromDirectory", ReplyAction = "http://tempuri.org/IVentopayFileService/GetFileListFromDirectoryResponse")]
    System.Threading.Tasks.Task<GetFileListFromDirectoryResponse> GetFileListFromDirectoryAsync(GetFileListFromDirectoryRequest request);

    // CODEGEN: Generating message contract since the wrapper name (DownloadRequest) of message DownloadRequest does not match the default value (DownloadFile)
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayFileService/DownloadFile", ReplyAction = "http://tempuri.org/IVentopayFileService/DownloadFileResponse")]
    RemoteFileInfo DownloadFile(DownloadRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayFileService/DownloadFile", ReplyAction = "http://tempuri.org/IVentopayFileService/DownloadFileResponse")]
    System.Threading.Tasks.Task<RemoteFileInfo> DownloadFileAsync(DownloadRequest request);
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "GetFileListFromDirectory", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class GetFileListFromDirectoryRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string directory;

    public GetFileListFromDirectoryRequest()
    {
    }

    public GetFileListFromDirectoryRequest(string directory)
    {
        this.directory = directory;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "GetFileListFromDirectoryResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class GetFileListFromDirectoryResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string[] GetFileListFromDirectoryResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public string versionInfo;

    public GetFileListFromDirectoryResponse()
    {
    }

    public GetFileListFromDirectoryResponse(string[] GetFileListFromDirectoryResult, string versionInfo)
    {
        this.GetFileListFromDirectoryResult = GetFileListFromDirectoryResult;
        this.versionInfo = versionInfo;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.ServiceModel.MessageContractAttribute(WrapperName = "DownloadRequest", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class DownloadRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string FilePath;

    public DownloadRequest()
    {
    }

    public DownloadRequest(string FilePath)
    {
        this.FilePath = FilePath;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.ServiceModel.MessageContractAttribute(WrapperName = "RemoteFileInfo", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class RemoteFileInfo
{

    [System.ServiceModel.MessageHeaderAttribute(Namespace = "http://tempuri.org/")]
    public string FileName;

    [System.ServiceModel.MessageHeaderAttribute(Namespace = "http://tempuri.org/")]
    public long Length;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public System.IO.Stream FileByteStream;

    public RemoteFileInfo()
    {
    }

    public RemoteFileInfo(string FileName, long Length, System.IO.Stream FileByteStream)
    {
        this.FileName = FileName;
        this.Length = Length;
        this.FileByteStream = FileByteStream;
    }
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface IVentopayFileServiceChannel : IVentopayFileService, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class VentopayFileServiceClient : System.ServiceModel.ClientBase<IVentopayFileService>, IVentopayFileService
{

    public VentopayFileServiceClient()
    {
    }

    public VentopayFileServiceClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
    {
    }

    public VentopayFileServiceClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
    {
    }

    public VentopayFileServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
    {
    }

    public VentopayFileServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
    {
    }

    public bool IsAlive()
    {
        return base.Channel.IsAlive();
    }

    public System.Threading.Tasks.Task<bool> IsAliveAsync()
    {
        return base.Channel.IsAliveAsync();
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    GetFileListFromDirectoryResponse IVentopayFileService.GetFileListFromDirectory(GetFileListFromDirectoryRequest request)
    {
        return base.Channel.GetFileListFromDirectory(request);
    }

    public string[] GetFileListFromDirectory(string directory, out string versionInfo)
    {
        GetFileListFromDirectoryRequest inValue = new GetFileListFromDirectoryRequest();
        inValue.directory = directory;
        GetFileListFromDirectoryResponse retVal = ((IVentopayFileService)(this)).GetFileListFromDirectory(inValue);
        versionInfo = retVal.versionInfo;
        return retVal.GetFileListFromDirectoryResult;
    }

    public System.Threading.Tasks.Task<GetFileListFromDirectoryResponse> GetFileListFromDirectoryAsync(GetFileListFromDirectoryRequest request)
    {
        return base.Channel.GetFileListFromDirectoryAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    RemoteFileInfo IVentopayFileService.DownloadFile(DownloadRequest request)
    {
        return base.Channel.DownloadFile(request);
    }

    public string DownloadFile(string FilePath, out long Length, out System.IO.Stream FileByteStream)
    {
        DownloadRequest inValue = new DownloadRequest();
        inValue.FilePath = FilePath;
        RemoteFileInfo retVal = ((IVentopayFileService)(this)).DownloadFile(inValue);
        Length = retVal.Length;
        FileByteStream = retVal.FileByteStream;
        return retVal.FileName;
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    System.Threading.Tasks.Task<RemoteFileInfo> IVentopayFileService.DownloadFileAsync(DownloadRequest request)
    {
        return base.Channel.DownloadFileAsync(request);
    }

    public System.Threading.Tasks.Task<RemoteFileInfo> DownloadFileAsync(string FilePath)
    {
        DownloadRequest inValue = new DownloadRequest();
        inValue.FilePath = FilePath;
        return ((IVentopayFileService)(this)).DownloadFileAsync(inValue);
    }
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName = "IVentopayBonusService")]
public interface IVentopayBonusService
{

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayService/IsAlive", ReplyAction = "http://tempuri.org/IVentopayService/IsAliveResponse")]
    bool IsAlive();

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayService/IsAlive", ReplyAction = "http://tempuri.org/IVentopayService/IsAliveResponse")]
    System.Threading.Tasks.Task<bool> IsAliveAsync();

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayBonusService/GetBonusLevelForCard", ReplyAction = "http://tempuri.org/IVentopayBonusService/GetBonusLevelForCardResponse")]
    GetBonusLevelForCardResponse GetBonusLevelForCard(GetBonusLevelForCardRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayBonusService/GetBonusLevelForCard", ReplyAction = "http://tempuri.org/IVentopayBonusService/GetBonusLevelForCardResponse")]
    System.Threading.Tasks.Task<GetBonusLevelForCardResponse> GetBonusLevelForCardAsync(GetBonusLevelForCardRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayBonusService/GetBonusLevelForCardNew", ReplyAction = "http://tempuri.org/IVentopayBonusService/GetBonusLevelForCardNewResponse")]
    GetBonusLevelForCardNewResponse GetBonusLevelForCardNew(GetBonusLevelForCardNewRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayBonusService/GetBonusLevelForCardNew", ReplyAction = "http://tempuri.org/IVentopayBonusService/GetBonusLevelForCardNewResponse")]
    System.Threading.Tasks.Task<GetBonusLevelForCardNewResponse> GetBonusLevelForCardNewAsync(GetBonusLevelForCardNewRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayBonusService/ActivateBonus", ReplyAction = "http://tempuri.org/IVentopayBonusService/ActivateBonusResponse")]
    vp.mocca.app.service.core.Interfaces.BonusAppError ActivateBonus(string SyncId, string CardId, string BonusLevel_Id, System.DateTime ActivationDate, System.Nullable<decimal> remainingPoints);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayBonusService/ActivateBonus", ReplyAction = "http://tempuri.org/IVentopayBonusService/ActivateBonusResponse")]
    System.Threading.Tasks.Task<vp.mocca.app.service.core.Interfaces.BonusAppError> ActivateBonusAsync(string SyncId, string CardId, string BonusLevel_Id, System.DateTime ActivationDate, System.Nullable<decimal> remainingPoints);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayBonusService/ActivateBonusNew", ReplyAction = "http://tempuri.org/IVentopayBonusService/ActivateBonusNewResponse")]
    vp.mocca.app.service.core.Interfaces.BonusAppError ActivateBonusNew(string SyncId, string CardId, string BonusLevel_Id, System.DateTime ActivationDate, System.Guid RestaurantId, System.Nullable<decimal> remainingPoints);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayBonusService/ActivateBonusNew", ReplyAction = "http://tempuri.org/IVentopayBonusService/ActivateBonusNewResponse")]
    System.Threading.Tasks.Task<vp.mocca.app.service.core.Interfaces.BonusAppError> ActivateBonusNewAsync(string SyncId, string CardId, string BonusLevel_Id, System.DateTime ActivationDate, System.Guid RestaurantId, System.Nullable<decimal> remainingPoints);
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "GetBonusLevelForCard", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class GetBonusLevelForCardRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string CardId;

    public GetBonusLevelForCardRequest()
    {
    }

    public GetBonusLevelForCardRequest(string CardId)
    {
        this.CardId = CardId;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "GetBonusLevelForCardResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class GetBonusLevelForCardResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public vp.mocca.app.service.core.Interfaces.BonusAppError GetBonusLevelForCardResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public vp.mocca.app.service.core.Interfaces.BonusValue[] bonusList;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public System.Guid ResponseCardId;

    public GetBonusLevelForCardResponse()
    {
    }

    public GetBonusLevelForCardResponse(vp.mocca.app.service.core.Interfaces.BonusAppError GetBonusLevelForCardResult, vp.mocca.app.service.core.Interfaces.BonusValue[] bonusList, System.Guid ResponseCardId)
    {
        this.GetBonusLevelForCardResult = GetBonusLevelForCardResult;
        this.bonusList = bonusList;
        this.ResponseCardId = ResponseCardId;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "GetBonusLevelForCardNew", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class GetBonusLevelForCardNewRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string CardId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public System.Guid RestaurantId;

    public GetBonusLevelForCardNewRequest()
    {
    }

    public GetBonusLevelForCardNewRequest(string CardId, System.Guid RestaurantId)
    {
        this.CardId = CardId;
        this.RestaurantId = RestaurantId;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "GetBonusLevelForCardNewResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class GetBonusLevelForCardNewResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public vp.mocca.app.service.core.Interfaces.BonusAppError GetBonusLevelForCardNewResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public vp.mocca.app.service.core.Interfaces.BonusValue[] bonusList;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public System.Guid ResponseCardId;

    public GetBonusLevelForCardNewResponse()
    {
    }

    public GetBonusLevelForCardNewResponse(vp.mocca.app.service.core.Interfaces.BonusAppError GetBonusLevelForCardNewResult, vp.mocca.app.service.core.Interfaces.BonusValue[] bonusList, System.Guid ResponseCardId)
    {
        this.GetBonusLevelForCardNewResult = GetBonusLevelForCardNewResult;
        this.bonusList = bonusList;
        this.ResponseCardId = ResponseCardId;
    }
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface IVentopayBonusServiceChannel : IVentopayBonusService, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class VentopayBonusServiceClient : System.ServiceModel.ClientBase<IVentopayBonusService>, IVentopayBonusService
{

    public VentopayBonusServiceClient()
    {
    }

    public VentopayBonusServiceClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
    {
    }

    public VentopayBonusServiceClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
    {
    }

    public VentopayBonusServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
    {
    }

    public VentopayBonusServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
    {
    }

    public bool IsAlive()
    {
        return base.Channel.IsAlive();
    }

    public System.Threading.Tasks.Task<bool> IsAliveAsync()
    {
        return base.Channel.IsAliveAsync();
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    GetBonusLevelForCardResponse IVentopayBonusService.GetBonusLevelForCard(GetBonusLevelForCardRequest request)
    {
        return base.Channel.GetBonusLevelForCard(request);
    }

    public vp.mocca.app.service.core.Interfaces.BonusAppError GetBonusLevelForCard(string CardId, out vp.mocca.app.service.core.Interfaces.BonusValue[] bonusList, out System.Guid ResponseCardId)
    {
        GetBonusLevelForCardRequest inValue = new GetBonusLevelForCardRequest();
        inValue.CardId = CardId;
        GetBonusLevelForCardResponse retVal = ((IVentopayBonusService)(this)).GetBonusLevelForCard(inValue);
        bonusList = retVal.bonusList;
        ResponseCardId = retVal.ResponseCardId;
        return retVal.GetBonusLevelForCardResult;
    }

    public System.Threading.Tasks.Task<GetBonusLevelForCardResponse> GetBonusLevelForCardAsync(GetBonusLevelForCardRequest request)
    {
        return base.Channel.GetBonusLevelForCardAsync(request);
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    GetBonusLevelForCardNewResponse IVentopayBonusService.GetBonusLevelForCardNew(GetBonusLevelForCardNewRequest request)
    {
        return base.Channel.GetBonusLevelForCardNew(request);
    }

    public vp.mocca.app.service.core.Interfaces.BonusAppError GetBonusLevelForCardNew(string CardId, System.Guid RestaurantId, out vp.mocca.app.service.core.Interfaces.BonusValue[] bonusList, out System.Guid ResponseCardId)
    {
        GetBonusLevelForCardNewRequest inValue = new GetBonusLevelForCardNewRequest();
        inValue.CardId = CardId;
        inValue.RestaurantId = RestaurantId;
        GetBonusLevelForCardNewResponse retVal = ((IVentopayBonusService)(this)).GetBonusLevelForCardNew(inValue);
        bonusList = retVal.bonusList;
        ResponseCardId = retVal.ResponseCardId;
        return retVal.GetBonusLevelForCardNewResult;
    }

    public System.Threading.Tasks.Task<GetBonusLevelForCardNewResponse> GetBonusLevelForCardNewAsync(GetBonusLevelForCardNewRequest request)
    {
        return base.Channel.GetBonusLevelForCardNewAsync(request);
    }

    public vp.mocca.app.service.core.Interfaces.BonusAppError ActivateBonus(string SyncId, string CardId, string BonusLevel_Id, System.DateTime ActivationDate, System.Nullable<decimal> remainingPoints)
    {
        return base.Channel.ActivateBonus(SyncId, CardId, BonusLevel_Id, ActivationDate, remainingPoints);
    }

    public System.Threading.Tasks.Task<vp.mocca.app.service.core.Interfaces.BonusAppError> ActivateBonusAsync(string SyncId, string CardId, string BonusLevel_Id, System.DateTime ActivationDate, System.Nullable<decimal> remainingPoints)
    {
        return base.Channel.ActivateBonusAsync(SyncId, CardId, BonusLevel_Id, ActivationDate, remainingPoints);
    }

    public vp.mocca.app.service.core.Interfaces.BonusAppError ActivateBonusNew(string SyncId, string CardId, string BonusLevel_Id, System.DateTime ActivationDate, System.Guid RestaurantId, System.Nullable<decimal> remainingPoints)
    {
        return base.Channel.ActivateBonusNew(SyncId, CardId, BonusLevel_Id, ActivationDate, RestaurantId, remainingPoints);
    }

    public System.Threading.Tasks.Task<vp.mocca.app.service.core.Interfaces.BonusAppError> ActivateBonusNewAsync(string SyncId, string CardId, string BonusLevel_Id, System.DateTime ActivationDate, System.Guid RestaurantId, System.Nullable<decimal> remainingPoints)
    {
        return base.Channel.ActivateBonusNewAsync(SyncId, CardId, BonusLevel_Id, ActivationDate, RestaurantId, remainingPoints);
    }
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName = "IVentopayMMSService")]
public interface IVentopayMMSService
{

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMMSService/VpMms_GetFeedbackRating", ReplyAction = "http://tempuri.org/IVentopayMMSService/VpMms_GetFeedbackRatingResponse")]
    VpMms_GetFeedbackRatingResponse VpMms_GetFeedbackRating(VpMms_GetFeedbackRatingRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMMSService/VpMms_GetFeedbackRating", ReplyAction = "http://tempuri.org/IVentopayMMSService/VpMms_GetFeedbackRatingResponse")]
    System.Threading.Tasks.Task<VpMms_GetFeedbackRatingResponse> VpMms_GetFeedbackRatingAsync(VpMms_GetFeedbackRatingRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMMSService/VpMms_IsAlive", ReplyAction = "http://tempuri.org/IVentopayMMSService/VpMms_IsAliveResponse")]
    bool VpMms_IsAlive();

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMMSService/VpMms_IsAlive", ReplyAction = "http://tempuri.org/IVentopayMMSService/VpMms_IsAliveResponse")]
    System.Threading.Tasks.Task<bool> VpMms_IsAliveAsync();
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMms_GetFeedbackRating", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMms_GetFeedbackRatingRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public int articleNumber;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public System.Nullable<System.DateTime> fromDate;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 3)]
    public System.Nullable<System.DateTime> untilDate;

    public VpMms_GetFeedbackRatingRequest()
    {
    }

    public VpMms_GetFeedbackRatingRequest(string restaurantId, int articleNumber, System.Nullable<System.DateTime> fromDate, System.Nullable<System.DateTime> untilDate)
    {
        this.restaurantId = restaurantId;
        this.articleNumber = articleNumber;
        this.fromDate = fromDate;
        this.untilDate = untilDate;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMms_GetFeedbackRatingResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMms_GetFeedbackRatingResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.MMSERROR_CS VpMms_GetFeedbackRatingResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.RatingValue_DTO ratingDto;

    public VpMms_GetFeedbackRatingResponse()
    {
    }

    public VpMms_GetFeedbackRatingResponse(VentoClient.MMSERROR_CS VpMms_GetFeedbackRatingResult, VentoClient.RatingValue_DTO ratingDto)
    {
        this.VpMms_GetFeedbackRatingResult = VpMms_GetFeedbackRatingResult;
        this.ratingDto = ratingDto;
    }
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface IVentopayMMSServiceChannel : IVentopayMMSService, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class VentopayMMSServiceClient : System.ServiceModel.ClientBase<IVentopayMMSService>, IVentopayMMSService
{

    public VentopayMMSServiceClient()
    {
    }

    public VentopayMMSServiceClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
    {
    }

    public VentopayMMSServiceClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
    {
    }

    public VentopayMMSServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
    {
    }

    public VentopayMMSServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
    {
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMms_GetFeedbackRatingResponse IVentopayMMSService.VpMms_GetFeedbackRating(VpMms_GetFeedbackRatingRequest request)
    {
        return base.Channel.VpMms_GetFeedbackRating(request);
    }

    public VentoClient.MMSERROR_CS VpMms_GetFeedbackRating(string restaurantId, int articleNumber, System.Nullable<System.DateTime> fromDate, System.Nullable<System.DateTime> untilDate, out VentoClient.RatingValue_DTO ratingDto)
    {
        VpMms_GetFeedbackRatingRequest inValue = new VpMms_GetFeedbackRatingRequest();
        inValue.restaurantId = restaurantId;
        inValue.articleNumber = articleNumber;
        inValue.fromDate = fromDate;
        inValue.untilDate = untilDate;
        VpMms_GetFeedbackRatingResponse retVal = ((IVentopayMMSService)(this)).VpMms_GetFeedbackRating(inValue);
        ratingDto = retVal.ratingDto;
        return retVal.VpMms_GetFeedbackRatingResult;
    }

    public System.Threading.Tasks.Task<VpMms_GetFeedbackRatingResponse> VpMms_GetFeedbackRatingAsync(VpMms_GetFeedbackRatingRequest request)
    {
        return base.Channel.VpMms_GetFeedbackRatingAsync(request);
    }

    public bool VpMms_IsAlive()
    {
        return base.Channel.VpMms_IsAlive();
    }

    public System.Threading.Tasks.Task<bool> VpMms_IsAliveAsync()
    {
        return base.Channel.VpMms_IsAliveAsync();
    }
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName = "IVentopayMasterDataService")]
public interface IVentopayMasterDataService
{

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMasterDataService/VpMds_UpdatePerson", ReplyAction = "http://tempuri.org/IVentopayMasterDataService/VpMds_UpdatePersonResponse")]
    VpMds_UpdatePersonResponse VpMds_UpdatePerson(VpMds_UpdatePersonRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMasterDataService/VpMds_UpdatePerson", ReplyAction = "http://tempuri.org/IVentopayMasterDataService/VpMds_UpdatePersonResponse")]
    System.Threading.Tasks.Task<VpMds_UpdatePersonResponse> VpMds_UpdatePersonAsync(VpMds_UpdatePersonRequest request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMasterDataService/VpMds_IsAlive", ReplyAction = "http://tempuri.org/IVentopayMasterDataService/VpMds_IsAliveResponse")]
    bool VpMds_IsAlive();

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMasterDataService/VpMds_IsAlive", ReplyAction = "http://tempuri.org/IVentopayMasterDataService/VpMds_IsAliveResponse")]
    System.Threading.Tasks.Task<bool> VpMds_IsAliveAsync();

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMasterDataService/VpMds_GetMonthSums", ReplyAction = "http://tempuri.org/IVentopayMasterDataService/VpMds_GetMonthSumsResponse")]
    VpMds_GetMonthSumsResponse VpMds_GetMonthSums(VpMds_GetMonthSumsRequest request);

    // CODEGEN: Generating message contract since the operation has multiple return values.
    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IVentopayMasterDataService/VpMds_GetMonthSums", ReplyAction = "http://tempuri.org/IVentopayMasterDataService/VpMds_GetMonthSumsResponse")]
    System.Threading.Tasks.Task<VpMds_GetMonthSumsResponse> VpMds_GetMonthSumsAsync(VpMds_GetMonthSumsRequest request);
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMds_UpdatePerson", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMds_UpdatePersonRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.PersonInformation_DTO[] personList;

    public VpMds_UpdatePersonRequest()
    {
    }

    public VpMds_UpdatePersonRequest(string restaurantId, VentoClient.PersonInformation_DTO[] personList)
    {
        this.restaurantId = restaurantId;
        this.personList = personList;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMds_UpdatePersonResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMds_UpdatePersonResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.MASTERDATAERROR_CS VpMds_UpdatePersonResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.PersonInformation_DTO[] personStatusList;

    public VpMds_UpdatePersonResponse()
    {
    }

    public VpMds_UpdatePersonResponse(VentoClient.MASTERDATAERROR_CS VpMds_UpdatePersonResult, VentoClient.PersonInformation_DTO[] personStatusList)
    {
        this.VpMds_UpdatePersonResult = VpMds_UpdatePersonResult;
        this.personStatusList = personStatusList;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMds_GetMonthSums", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMds_GetMonthSumsRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public string restaurantId;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public System.DateTime fromDate;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 2)]
    public System.DateTime untilDate;

    public VpMds_GetMonthSumsRequest()
    {
    }

    public VpMds_GetMonthSumsRequest(string restaurantId, System.DateTime fromDate, System.DateTime untilDate)
    {
        this.restaurantId = restaurantId;
        this.fromDate = fromDate;
        this.untilDate = untilDate;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.MessageContractAttribute(WrapperName = "VpMds_GetMonthSumsResponse", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class VpMds_GetMonthSumsResponse
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    public VentoClient.MASTERDATAERROR_CS VpMds_GetMonthSumsResult;

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 1)]
    public VentoClient.PersonInformation_DTO[] persons;

    public VpMds_GetMonthSumsResponse()
    {
    }

    public VpMds_GetMonthSumsResponse(VentoClient.MASTERDATAERROR_CS VpMds_GetMonthSumsResult, VentoClient.PersonInformation_DTO[] persons)
    {
        this.VpMds_GetMonthSumsResult = VpMds_GetMonthSumsResult;
        this.persons = persons;
    }
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface IVentopayMasterDataServiceChannel : IVentopayMasterDataService, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class VentopayMasterDataServiceClient : System.ServiceModel.ClientBase<IVentopayMasterDataService>, IVentopayMasterDataService
{

    public VentopayMasterDataServiceClient()
    {
    }

    public VentopayMasterDataServiceClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
    {
    }

    public VentopayMasterDataServiceClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
    {
    }

    public VentopayMasterDataServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
    {
    }

    public VentopayMasterDataServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
    {
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMds_UpdatePersonResponse IVentopayMasterDataService.VpMds_UpdatePerson(VpMds_UpdatePersonRequest request)
    {
        return base.Channel.VpMds_UpdatePerson(request);
    }

    public VentoClient.MASTERDATAERROR_CS VpMds_UpdatePerson(string restaurantId, VentoClient.PersonInformation_DTO[] personList, out VentoClient.PersonInformation_DTO[] personStatusList)
    {
        VpMds_UpdatePersonRequest inValue = new VpMds_UpdatePersonRequest();
        inValue.restaurantId = restaurantId;
        inValue.personList = personList;
        VpMds_UpdatePersonResponse retVal = ((IVentopayMasterDataService)(this)).VpMds_UpdatePerson(inValue);
        personStatusList = retVal.personStatusList;
        return retVal.VpMds_UpdatePersonResult;
    }

    public System.Threading.Tasks.Task<VpMds_UpdatePersonResponse> VpMds_UpdatePersonAsync(VpMds_UpdatePersonRequest request)
    {
        return base.Channel.VpMds_UpdatePersonAsync(request);
    }

    public bool VpMds_IsAlive()
    {
        return base.Channel.VpMds_IsAlive();
    }

    public System.Threading.Tasks.Task<bool> VpMds_IsAliveAsync()
    {
        return base.Channel.VpMds_IsAliveAsync();
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    VpMds_GetMonthSumsResponse IVentopayMasterDataService.VpMds_GetMonthSums(VpMds_GetMonthSumsRequest request)
    {
        return base.Channel.VpMds_GetMonthSums(request);
    }

    public VentoClient.MASTERDATAERROR_CS VpMds_GetMonthSums(string restaurantId, System.DateTime fromDate, System.DateTime untilDate, out VentoClient.PersonInformation_DTO[] persons)
    {
        VpMds_GetMonthSumsRequest inValue = new VpMds_GetMonthSumsRequest();
        inValue.restaurantId = restaurantId;
        inValue.fromDate = fromDate;
        inValue.untilDate = untilDate;
        VpMds_GetMonthSumsResponse retVal = ((IVentopayMasterDataService)(this)).VpMds_GetMonthSums(inValue);
        persons = retVal.persons;
        return retVal.VpMds_GetMonthSumsResult;
    }

    public System.Threading.Tasks.Task<VpMds_GetMonthSumsResponse> VpMds_GetMonthSumsAsync(VpMds_GetMonthSumsRequest request)
    {
        return base.Channel.VpMds_GetMonthSumsAsync(request);
    }
}
namespace System.Data.Linq
{
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "Binary", Namespace = "http://schemas.datacontract.org/2004/07/System.Data.Linq")]
    public partial class Binary : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private byte[] BytesField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] Bytes
        {
            get
            {
                return this.BytesField;
            }
            set
            {
                this.BytesField = value;
            }
        }
    }
}
namespace vp.mocca.app.service.core.Interfaces
{
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "BonusValue_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.app.service.core.Interfaces")]
    public partial class BonusValue_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private vp.mocca.app.service.core.Interfaces.BonusLevel_DTO[] BonusLevelListField;

        private int BonusLogic_IDField;

        private string BonusProgramNameField;

        private int CustomerGroup_BonusLogic_IDField;

        private System.Guid CustomerGroup_IDField;

        private System.Nullable<System.DateTime> LastActivationDateField;

        private System.DateTime ReportingValidFromField;

        private System.DateTime ReportingValidUntilField;

        private System.DateTime ValidFromField;

        private System.DateTime ValidUntilField;

        private string ValueField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public vp.mocca.app.service.core.Interfaces.BonusLevel_DTO[] BonusLevelList
        {
            get
            {
                return this.BonusLevelListField;
            }
            set
            {
                this.BonusLevelListField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int BonusLogic_ID
        {
            get
            {
                return this.BonusLogic_IDField;
            }
            set
            {
                this.BonusLogic_IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BonusProgramName
        {
            get
            {
                return this.BonusProgramNameField;
            }
            set
            {
                this.BonusProgramNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int CustomerGroup_BonusLogic_ID
        {
            get
            {
                return this.CustomerGroup_BonusLogic_IDField;
            }
            set
            {
                this.CustomerGroup_BonusLogic_IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid CustomerGroup_ID
        {
            get
            {
                return this.CustomerGroup_IDField;
            }
            set
            {
                this.CustomerGroup_IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> LastActivationDate
        {
            get
            {
                return this.LastActivationDateField;
            }
            set
            {
                this.LastActivationDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ReportingValidFrom
        {
            get
            {
                return this.ReportingValidFromField;
            }
            set
            {
                this.ReportingValidFromField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ReportingValidUntil
        {
            get
            {
                return this.ReportingValidUntilField;
            }
            set
            {
                this.ReportingValidUntilField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ValidFrom
        {
            get
            {
                return this.ValidFromField;
            }
            set
            {
                this.ValidFromField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ValidUntil
        {
            get
            {
                return this.ValidUntilField;
            }
            set
            {
                this.ValidUntilField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Value
        {
            get
            {
                return this.ValueField;
            }
            set
            {
                this.ValueField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "BonusLevel_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.app.service.core.Interfaces")]
    public partial class BonusLevel_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private System.Nullable<System.DateTime> ActivationDateField;

        private string BonusLevelDescriptionField;

        private int BonusLevelIdField;

        private string BonusLevelNameField;

        private byte[] BonusLevelPictureField;

        private int[] FeedbackProgramIdsField;

        private VentoClient.FeedbackProgram_DTO[] FeedbackProgramsField;

        private bool IsActivationPossibleField;

        private string LowerLimitField;

        private string UpperLimitField;

        private System.Nullable<System.DateTime> UseDateField;

        private string VoucherCodeField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> ActivationDate
        {
            get
            {
                return this.ActivationDateField;
            }
            set
            {
                this.ActivationDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BonusLevelDescription
        {
            get
            {
                return this.BonusLevelDescriptionField;
            }
            set
            {
                this.BonusLevelDescriptionField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int BonusLevelId
        {
            get
            {
                return this.BonusLevelIdField;
            }
            set
            {
                this.BonusLevelIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BonusLevelName
        {
            get
            {
                return this.BonusLevelNameField;
            }
            set
            {
                this.BonusLevelNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] BonusLevelPicture
        {
            get
            {
                return this.BonusLevelPictureField;
            }
            set
            {
                this.BonusLevelPictureField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int[] FeedbackProgramIds
        {
            get
            {
                return this.FeedbackProgramIdsField;
            }
            set
            {
                this.FeedbackProgramIdsField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public VentoClient.FeedbackProgram_DTO[] FeedbackPrograms
        {
            get
            {
                return this.FeedbackProgramsField;
            }
            set
            {
                this.FeedbackProgramsField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsActivationPossible
        {
            get
            {
                return this.IsActivationPossibleField;
            }
            set
            {
                this.IsActivationPossibleField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LowerLimit
        {
            get
            {
                return this.LowerLimitField;
            }
            set
            {
                this.LowerLimitField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UpperLimit
        {
            get
            {
                return this.UpperLimitField;
            }
            set
            {
                this.UpperLimitField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> UseDate
        {
            get
            {
                return this.UseDateField;
            }
            set
            {
                this.UseDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string VoucherCode
        {
            get
            {
                return this.VoucherCodeField;
            }
            set
            {
                this.VoucherCodeField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "RestaurantConnection_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.app.service.core.Interfaces")]
    public partial class RestaurantConnection_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string AdvisorModuleUrlField;

        private string AppNameField;

        private VentoClient.Module_DTO[] AvailableModulesField;

        private System.Guid Cashpoint_IDField;

        private string CityField;

        private string ConnectionStringField;

        private string CountryCodeField;

        private string CurrencyShortNameField;

        private string CurrencyStringField;

        private string CurrencySymbolField;

        private string EMailField;

        private vp.mocca.app.service.core.Interfaces.AppModule[] EnabledModulesField;

        private string FaxNumberField;

        private int IDField;

        private string InfoField;

        private bool IsConnectionActiveField;

        private System.DateTime LastTextModificationDateField;

        private byte[] LogoField;

        private string NameField;

        private string PostalCodeField;

        private string ReportingConnectionStringField;

        private int RestaurantNumberField;

        private System.Guid Restaurant_IDField;

        private string SecondaryColorField;

        private string StreetField;

        private string TelephoneNumberField;

        private string UIDField;

        private string WebsiteUrlField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AdvisorModuleUrl
        {
            get
            {
                return this.AdvisorModuleUrlField;
            }
            set
            {
                this.AdvisorModuleUrlField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AppName
        {
            get
            {
                return this.AppNameField;
            }
            set
            {
                this.AppNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public VentoClient.Module_DTO[] AvailableModules
        {
            get
            {
                return this.AvailableModulesField;
            }
            set
            {
                this.AvailableModulesField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid Cashpoint_ID
        {
            get
            {
                return this.Cashpoint_IDField;
            }
            set
            {
                this.Cashpoint_IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string City
        {
            get
            {
                return this.CityField;
            }
            set
            {
                this.CityField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ConnectionString
        {
            get
            {
                return this.ConnectionStringField;
            }
            set
            {
                this.ConnectionStringField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CountryCode
        {
            get
            {
                return this.CountryCodeField;
            }
            set
            {
                this.CountryCodeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CurrencyShortName
        {
            get
            {
                return this.CurrencyShortNameField;
            }
            set
            {
                this.CurrencyShortNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CurrencyString
        {
            get
            {
                return this.CurrencyStringField;
            }
            set
            {
                this.CurrencyStringField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CurrencySymbol
        {
            get
            {
                return this.CurrencySymbolField;
            }
            set
            {
                this.CurrencySymbolField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EMail
        {
            get
            {
                return this.EMailField;
            }
            set
            {
                this.EMailField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public vp.mocca.app.service.core.Interfaces.AppModule[] EnabledModules
        {
            get
            {
                return this.EnabledModulesField;
            }
            set
            {
                this.EnabledModulesField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FaxNumber
        {
            get
            {
                return this.FaxNumberField;
            }
            set
            {
                this.FaxNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID
        {
            get
            {
                return this.IDField;
            }
            set
            {
                this.IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Info
        {
            get
            {
                return this.InfoField;
            }
            set
            {
                this.InfoField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsConnectionActive
        {
            get
            {
                return this.IsConnectionActiveField;
            }
            set
            {
                this.IsConnectionActiveField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime LastTextModificationDate
        {
            get
            {
                return this.LastTextModificationDateField;
            }
            set
            {
                this.LastTextModificationDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] Logo
        {
            get
            {
                return this.LogoField;
            }
            set
            {
                this.LogoField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PostalCode
        {
            get
            {
                return this.PostalCodeField;
            }
            set
            {
                this.PostalCodeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ReportingConnectionString
        {
            get
            {
                return this.ReportingConnectionStringField;
            }
            set
            {
                this.ReportingConnectionStringField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int RestaurantNumber
        {
            get
            {
                return this.RestaurantNumberField;
            }
            set
            {
                this.RestaurantNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid Restaurant_ID
        {
            get
            {
                return this.Restaurant_IDField;
            }
            set
            {
                this.Restaurant_IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SecondaryColor
        {
            get
            {
                return this.SecondaryColorField;
            }
            set
            {
                this.SecondaryColorField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Street
        {
            get
            {
                return this.StreetField;
            }
            set
            {
                this.StreetField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TelephoneNumber
        {
            get
            {
                return this.TelephoneNumberField;
            }
            set
            {
                this.TelephoneNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UID
        {
            get
            {
                return this.UIDField;
            }
            set
            {
                this.UIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string WebsiteUrl
        {
            get
            {
                return this.WebsiteUrlField;
            }
            set
            {
                this.WebsiteUrlField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "AppModule", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.app.service.core.Interfaces")]
    public enum AppModule : int
    {

        [System.Runtime.Serialization.EnumMemberAttribute()]
        MODULE_HOME = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        MODULE_SALDO = 1,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        MODULE_CHARGE_CARD = 2,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        MODULE_BONUS = 3,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        MODULE_SETTINGS = 4,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        MODULE_PREVIOUS_BONS = 5,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        MODULE_MENUPLAN = 6,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        MODULE_PAYMENT = 7,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        MODULE_NUTRIENT_INFORMATION = 8,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        MODULE_ASSISTENT = 9,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        MODULE_FEEDBACK = 10,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "TableRoutingError", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.app.service.core.Interfaces")]
    public enum TableRoutingError : int
    {

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_NONE = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_RESTAURANT_NOT_FOUND = 1,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_TABLE_AREA_NOT_FOUND = 2,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_TABLE_AREAS_NOT_FOUND = 3,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_OTHER = 4,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "TableStatus", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.app.service.core.Interfaces")]
    public enum TableStatus : int
    {

        [System.Runtime.Serialization.EnumMemberAttribute()]
        FREE = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ALMOSTFULL = 1,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        FULL = 2,
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "TableStatus_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.app.service.core.Interfaces")]
    public partial class TableStatus_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private vp.mocca.app.service.core.Interfaces.Area_DTO AreaInformationField;

        private int EstimatedFreeChairsField;

        private vp.mocca.app.service.core.Interfaces.TableStatus TableStatusField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public vp.mocca.app.service.core.Interfaces.Area_DTO AreaInformation
        {
            get
            {
                return this.AreaInformationField;
            }
            set
            {
                this.AreaInformationField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int EstimatedFreeChairs
        {
            get
            {
                return this.EstimatedFreeChairsField;
            }
            set
            {
                this.EstimatedFreeChairsField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public vp.mocca.app.service.core.Interfaces.TableStatus TableStatus
        {
            get
            {
                return this.TableStatusField;
            }
            set
            {
                this.TableStatusField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "Area_DTO", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.app.service.core.Interfaces")]
    public partial class Area_DTO : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private int AreaNumberField;

        private string NameField;

        private int PlaceCountField;

        private string ShortNameField;

        private System.TimeSpan TimePerEaterField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int AreaNumber
        {
            get
            {
                return this.AreaNumberField;
            }
            set
            {
                this.AreaNumberField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int PlaceCount
        {
            get
            {
                return this.PlaceCountField;
            }
            set
            {
                this.PlaceCountField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ShortName
        {
            get
            {
                return this.ShortNameField;
            }
            set
            {
                this.ShortNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.TimeSpan TimePerEater
        {
            get
            {
                return this.TimePerEaterField;
            }
            set
            {
                this.TimePerEaterField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "BonusAppError", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.app.service.core.Interfaces")]
    public enum BonusAppError : int
    {

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_NONE = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_CARDID_NOT_FOUND = 1,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_CARDID_INVALID = 2,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_TOO_LESS_TURNOVER = 3,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_INVALID_GUID = 4,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_OTHER = 5,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_NO_BONUS = 6,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR_NO_REPORTING = 7,
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "BonusValue", Namespace = "http://schemas.datacontract.org/2004/07/vp.mocca.app.service.core.Interfaces")]
    public partial class BonusValue : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private int BonusLogic_IDField;

        private int CustomerGroup_BonusLogic_IDField;

        private System.Guid CustomerGroup_IDField;

        private System.Nullable<System.DateTime> LastActivationDateField;

        private System.DateTime ReportingValidFromField;

        private System.DateTime ReportingValidUntilField;

        private System.DateTime ValidFromField;

        private System.DateTime ValidUntilField;

        private string ValueField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int BonusLogic_ID
        {
            get
            {
                return this.BonusLogic_IDField;
            }
            set
            {
                this.BonusLogic_IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int CustomerGroup_BonusLogic_ID
        {
            get
            {
                return this.CustomerGroup_BonusLogic_IDField;
            }
            set
            {
                this.CustomerGroup_BonusLogic_IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid CustomerGroup_ID
        {
            get
            {
                return this.CustomerGroup_IDField;
            }
            set
            {
                this.CustomerGroup_IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> LastActivationDate
        {
            get
            {
                return this.LastActivationDateField;
            }
            set
            {
                this.LastActivationDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ReportingValidFrom
        {
            get
            {
                return this.ReportingValidFromField;
            }
            set
            {
                this.ReportingValidFromField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ReportingValidUntil
        {
            get
            {
                return this.ReportingValidUntilField;
            }
            set
            {
                this.ReportingValidUntilField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ValidFrom
        {
            get
            {
                return this.ValidFromField;
            }
            set
            {
                this.ValidFromField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ValidUntil
        {
            get
            {
                return this.ValidUntilField;
            }
            set
            {
                this.ValidUntilField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Value
        {
            get
            {
                return this.ValueField;
            }
            set
            {
                this.ValueField = value;
            }
        }
    }

}

