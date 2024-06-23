namespace PropertyExchange.Presentation.API.Models.Property
{
    public class PropertyPurchaseOrSaleExpensesBreakOutVM
    {
        public int Id { get; set; }
        public required string PropertyID { get; set; }
        public required string PurchaseOrSale { get; set; }
        public required string ExpenseType { get; set; }
        public string? Description { get; set; }
        public required decimal ExpenseAmount { get; set; }
        public required string ExpenseIncurredOnDate { get; set; }
        public int ExpenseMonth { get; set; }
        public int ExpenseYear { get; set; }
        public string? CreatedDate { get; set; }
        public string? LastUpdatedDate { get; set; }
        public PropertyVM? Property { get; set; }
    }
}
