namespace PropertyExchange.Presentation.API.Models.Property
{
    public class PropertyExpensesBreakOutModel
    {
        public int Id { get; set; }
        public required string PropertyID { get; set; }
        public required string ExpenseType { get; set; }
        public string? Description { get; set; }
        public required decimal ExpenseAmount { get; set; }
        public required string ExpenseIncurredOnDate { get; set; }
        public int ExpenseMonth { get; set; }
        public int ExpenseYear { get; set; }
        public required string CreatedDate { get; set; }
        public string? LastUpdatedDate { get; set; }
        public required PropertyModel Property { get; set; }
    }
}
