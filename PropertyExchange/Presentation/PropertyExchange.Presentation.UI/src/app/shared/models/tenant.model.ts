import { Property } from "./property.model";

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

export class TenantLeaseDetails{
    Id:number;
    PropertyID: string;
    TenantID: string;
    LeaseAdvanceAmount: number;
    LeaseStartDate: string;
    LeaseEndDate: string;
    LeaseTenureInMonths: number;
    RentPerSft: number;
    TotalAreaInSft: number;
    EscalationInPercentage: number;
    EscalationTenure: number;
    CreatedDate: string;
    LastUpdatedDate: string;
    Tenant: TenantDetails;
}

export class TenantRentPaymentDetailsVM{
    Id:number;
    PropertyID: string;
    TenantID: string;
    ExpenseType: string;
    Description: string;
    ExpenseAmount: number;
    ExpenseIncurredOnDate: string;
    ExpenseMonth: number;
    ExpenseYear: number;
    CreatedDate: string;
    LastUpdatedDate: string;
    Tenant: TenantDetails;
}