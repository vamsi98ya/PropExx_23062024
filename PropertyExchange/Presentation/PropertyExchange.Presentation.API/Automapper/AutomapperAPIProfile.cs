using AutoMapper;
using PropertyExchange.Core.Domain.Models.Property;
using PropertyExchange.Core.Domain.Models.User;
using PropertyExchange.Presentation.API.Models.Property;
using PropertyExchange.Presentation.API.Models.Tenant;
using PropertyExchange.Presentation.API.Models.User;
using peCore = PropertyExchange.Core.Domain.Models;
using peVM = PropertyExchange.Presentation.API.Models;

namespace PropertyExchange.Presentation.API.Automapper
{
    public class AutomapperAPIProfile:Profile
    {
        public AutomapperAPIProfile()
        {
            //USER
            CreateMap<UserRegistrationLoginModel, UserRegistrationLoginVM>().ReverseMap();
            CreateMap<UserModel, UserVM>().ReverseMap();
            CreateMap<UserChangePasswordModel, UserChangePasswordVM>().ReverseMap();
            CreateMap<UserFundDetailsModel, UserFundDetailsVM>().ReverseMap();
            CreateMap<UserPassbookModel, UserPassbookVM>().ReverseMap();
            CreateMap<UserHoldingModel, UserHoldingVM>().ReverseMap();


            //PROPERTY
            CreateMap<PropertyModel, PropertyVM>().ReverseMap();
            CreateMap<PropertyAddressDetailsModel, PropertyAddressDetailsVM>().ReverseMap();
            CreateMap<PropertyPurchaseOrSaleExpensesBreakOutModel, PropertyPurchaseOrSaleExpensesBreakOutVM>().ReverseMap();
            CreateMap<PropertyPassbookModel, PropertyPassbookVM>().ReverseMap();
            CreateMap<PropertyIncomeBreakOutModel, PropertyIncomeBreakOutVM>().ReverseMap();
            CreateMap<PropertyExpensesBreakOutModel, PropertyExpensesBreakOutVM>().ReverseMap();
            CreateMap<PropertyValuationMetricsModel, PropertyValuationMetricsVM>().ReverseMap();
            CreateMap<PropertyProjectedValuationMetricsModel, PropertyProjectedValuationMetricsVM>().ReverseMap();
            CreateMap<PropertyTradeRecordsModel, PropertyTradeRecordsVM>().ReverseMap();

            //TENANT
            CreateMap<TenantModel, TenantVM>().ReverseMap();
            CreateMap<TenantLeaseDetailsModel, TenantLeaseDetailsVM>().ReverseMap();
            CreateMap<TenantRentPaymentDetailsModel, TenantRentPaymentDetailsVM>().ReverseMap();
        }
    }
}
