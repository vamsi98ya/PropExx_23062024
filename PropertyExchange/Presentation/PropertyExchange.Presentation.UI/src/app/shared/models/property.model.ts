import { TenantLeaseDetails, TenantRentPaymentDetailsVM } from "./tenant.model";

export class Property {
    Id: number;
    PropertyID: string;
    Name: string;
    Description: string;
    Location: string;
    PropertyType: string;
    HoldingType:string;
    OverallAreaInSft: number;
    IsActive: boolean;
    IsAvailableForInvesting: boolean;
    InitialTotalValuation: number;
    InitialTokenPrice: number;
    InitialTotalNumberOfTokens: number;
    InitialAvailableNumberOfTokens: number;
    InitialTotalTokensValuation: number;
    CurrentTotalValuation: number;
    CurrentTokenPrice: number;
    CurrentTotalNumberOfTokens: number;
    CurrentAvailableNumberOfTokens: number;
    CurrentTotalTokensValuation: number;
    CreatedDate: string;
    LastUpdatedDate: string;

    AddressDetails: PropertyAddressDetails;
    PurchaseOrSaleExpensesBreakOut: PropertyPurchaseOrSaleExpensesBreakOutVM[];
    Passbook: PropertyPassbook[];
    TradeRecords: PropertyTradeRecords[];
    IncomeBreakOut: PropertyIncomeBreakOut[];
    ExpensesBreakOut: PropertyExpenseBreakOut[];
    ValuationMetrics: PropertyValuationMetrics[];
    ProjectedValuationMetrics: PropertyProjectedValuationMetrics[];
    TenantDetails: TenantDetails[];
}

export class PropertyAddressDetails {
    Id:number;
    PropertyID: string;
    AddressLine1: string;
    AddressLine2: string;
    AddressLine3: string;
    Landmark: string;
    City: string;
    State: string;
    Country: string;
    PINCode: number;
    CreatedDate: string
    LastUpdatedDate: string;
    Property: Property;
}

export class PropertyPurchaseOrSaleExpensesBreakOutVM {
    Id:number;
    PropertyID: string;
    PurchaseOrSale: string;
    ExpenseType: string;
    Description: string;
    ExpenseAmount: number;
    ExpenseIncurredOnDate: string;
    ExpenseMonth: number;
    ExpenseYear: number;
    CreatedDate: string;
    LastUpdatedDate: string;
    Property: Property;
}

export class PropertyPassbook
{
    Id:number;
    PropertyID: string;
    TxnNumber : string; 
    TxnType: string;
    Quantity: number;
    Price: number;
    TotalTxnValue:number;
    OrderDateTime: string;
    CreatedDate: string;
    LastUpdatedDate: string;
    Property: Property;
}

export class PropertyTradeRecords
{
    Id:number;
    PropertyID: string;
    CurrentTokenPrice: number;
    CurrentTotalNumberOfTokens: number;
    CurrentAvailableNumberOfTokens: number;
    CurrentTotalTokensValuation: string;
    CreatedDate: string;
    LastUpdatedDate: string;
    Property: Property;
}

export class PropertyIncomeBreakOut
{
    Id:number;
    PropertyID: string;
    IncomeType: string;
    Description: string;
    IncomeAmount: number;
    IncomeGeneratedOnDate: string;
    IncomeMonth: number;
    IncomeYear: number;
    CreatedDate: string;
    LastUpdatedDate: string;
    Property: Property;
}

export class PropertyExpenseBreakOut
{
    Id:number;
    PropertyID: string;
    ExpenseType: string;
    Description: string;
    ExpenseAmount: number;
    ExpenseIncurredOnDate: string;
    ExpenseMonth: number;
    ExpenseYear: number;
    CreatedDate: string;
    LastUpdatedDate: string;
    Property: Property;
}

export class PropertyValuationMetrics{
    Id:number;
    PropertyID: string;
    PricePerSft: number;
    TotalSft: number;
    Month: number; 
    Year: number; 
    CreatedDate: string;
    LastUpdatedDate: string;
    Property: Property;
}

export class PropertyProjectedValuationMetrics{
    Id:number;
    PropertyID: string;
    PricePerSft: number;
    TotalSft: number;
    Month: number; 
    Year: number; 
    CreatedDate: string;
    LastUpdatedDate: string;
    Property: Property;
}

export class TenantDetails{
    Id:number;
    PropertyID: string;
    TenantID: string;
    Name: string;
    Description: string;
    Email: string;
    PhoneNumber: string;
    Country: string;
    IsActive: boolean;
    TenancyType: string;
    CreatedDate: string;
    LastUpdatedDate: string;
    Property: Property;
    LeaseDetails: TenantLeaseDetails[];
    RentPaymentDetails: TenantRentPaymentDetailsVM[];
}

