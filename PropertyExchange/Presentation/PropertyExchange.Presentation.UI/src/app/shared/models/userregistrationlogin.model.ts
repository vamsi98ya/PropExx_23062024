export class UserRegistrationLogin{
    UserEmail: string;
    UserPhoneNumber:string;
    UserPassword:string;
}

export class UserDetails{
   UserEmail : string;
   UserPhoneNumber : string;
    Name : string;
    DOB : string;
    Gender : string;
   SecondaryEmail : string;
   SecondaryPhoneNumber : string;
   Occupation : string;
   IncomeRange : string;
   MaritalStatus : string;
   FatherName : string;
   AvailableMoneyForInvesting:number;
}

export class ChangePassword{
    UserEmail: string;
    UserPhoneNumber:string;
    OldPassword:string;
    NewPassword:string
}