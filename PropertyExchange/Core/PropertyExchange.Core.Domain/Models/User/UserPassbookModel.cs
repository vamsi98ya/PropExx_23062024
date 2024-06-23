using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PropertyExchange.Presentation.API.Models.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyExchange.Core.Domain.Models.User
{
    public class UserPassbookModel
    {
        public int Id { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPhoneNumber { get; set; }
        public required string PropertyID { get; set; }
        public required string TxnNumber { get; set; }
        public required string TxnType { get; set; }
        public required int Quantity { get; set; }
        public required decimal Price { get; set; }
        public required decimal TotalTxnValue { get; set; }
        public required string OrderDateTime { get; set; }
        public bool IsCurrentHolding { get; set; }
        public string? CreatedDate { get; set; }
        public string? LastUpdatedDate { get; set; }
        public UserModel? UserDetails { get; set; }
        public PropertyModel? Property { get; set; }

    }
}
