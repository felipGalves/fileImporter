using System.ComponentModel;

namespace fileImporter.Utils.ConsoleConversation.Enums
{
    public enum UserChoices
    {
        [Description("Customer Listing")]
        CustomerListing = 1,
        [Description("Filter Customers")]
        FilterCustomers = 2,
        [Description("Register New Customer")]
        RegisterNewCustomer = 3,
        [Description("Import Customers")]
        ImportCustomers = 4,
        [Description("Export Customers")]
        ExportCustomers = 5,
        [Description("Delete Customer")]
        DeleteCustomer = 6,
        [Description("Delete All Customers")]
        DeleteAllCustomers = 7,
        [Description("Clear History")]
        ClearHistory = 8,
    }
}