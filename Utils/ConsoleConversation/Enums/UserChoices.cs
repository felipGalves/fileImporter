using System.ComponentModel;

namespace fileImporter.Utils.ConsoleConversation.Enums
{
    public enum UserChoices
    {
        [Description("Customer Listing")]
        CustomerListing = 1,
        [Description("Register New Customer")]
        RegisterNewCustomer = 2,
        [Description("Import Customers")]
        ImportCustomers = 3,
        [Description("Export Customers")]
        ExportCustomers = 4,
        [Description("Delete Customer")]
        DeleteCustomer = 5,
        [Description("Delete All Customers")]
        DeleteAllCustomers = 6,
        [Description("Clear History")]
        ClearHistory = 7
    }
}