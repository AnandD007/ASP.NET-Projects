namespace CustomerWebApp.Api.Models
{
    public static class ConstantStatusMessages
    {
        public static string DataRetrievedSuccessfully = "Data Retrieval Successful";
        public static string AttorneyDoesNotExist = "Attorney with this ID does not Exist";
        public static string AttorneyAlreadyExists = "Attorney with the same ID already Exists";
        public static string DataAddedSuccessfully = "Data Added Successfully";
        public static string DataUpdatedSuccessfully = "Data Updated Successfully";
        public static string DataDeletedSuccessfully = "Data Deleted Successfully";
        public static string MatterDoesNotExist = "Matter with this ID does not Exist";
        public static string MatterAlreadyExists = "Matter with the same ID already Exists";
        public static string ClientDoesNotExist = "Client with this ID does not Exist";
        public static string ClientAlreadyExists = "Client with the same ID already Exists";
        public static string NoMatchingJurisdiction = "This Attorney is invalid for this Matter's Jurisdiction";
        public static string MattersByClientsNotFound = "Clients don't have any previous Matters";
        public static string MattersByClientNotFound = "This Client doesn't have any previous Matters";
        public static string InvoiceDoesNotExist = "Invoice with this ID does not Exist";
        public static string InvoicesByMattersNotFound = "Matters don't have any previous Invoices";
        public static string InvoicesByMatterNotFound = "This Matter doesn't have any previous Invoices";
        public static string InvoiceAlreadyExists = "Invoice with the same ID already Exists";
        public static string BillingFetched = "Last Week's Billing Amount Retrieval Successful";
    }
}
