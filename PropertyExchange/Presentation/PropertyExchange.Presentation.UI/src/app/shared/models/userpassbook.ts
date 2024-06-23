export class UserPassbook {
    UserEmail: string;
    UserPhoneNumber: string;
    PropertyID: string;
    TxnType: string;
    TxnNumber: string;
    Quantity: number;
    Price: number;
    TotalTxnValue: number;
    OrderDateTime: string;
    IsCurrentHolding: boolean;
}

export class UserHolding {
    UserEmail: string;
    UserPhoneNumber: string;
    PropertyID: string;
    PropertyName: string;
    Quantity: number;
    Price: number;
    IsActive: boolean;
}