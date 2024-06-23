﻿namespace PropertyExchange.Presentation.API.Models.Property
{
    public class PropertyIncomeBreakOutVM
    {
        public int Id { get; set; }
        public required string PropertyID { get; set; }
        public required string IncomeType { get; set; }
        public string? Description { get; set; }
        public required decimal IncomeAmount { get; set; }
        public required string IncomeGeneratedOnDate { get; set; }
        public int IncomeMonth { get; set; }
        public int IncomeYear { get; set; }
        public string? CreatedDate { get; set; }
        public string? LastUpdatedDate { get; set; }
        public PropertyVM? Property { get; set; }
    }
}
