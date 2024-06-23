using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PropertyExchange.Presentation.API.Models.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyExchange.Core.Domain.Models.Property
{
    public class PropertyTradeRecordsModel
    {
        public int Id { get; set; }
        public required string PropertyID { get; set; }
        public decimal CurrentTokenPrice { get; set; }
        public int CurrentTotalNumberOfTokens { get; set; }
        public int CurrentAvailableNumberOfTokens { get; set; }
        public int CurrentTotalTokensValuation { get; set; }
        public string? CreatedDate { get; set; }
        public string? LastUpdatedDate { get; set; }
        public PropertyModel? Property { get; set; }
    }
}
