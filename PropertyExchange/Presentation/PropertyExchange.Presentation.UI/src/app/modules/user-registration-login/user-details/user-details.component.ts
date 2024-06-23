import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from 'express';
import { Credentials } from 'src/app/shared/models/credentials.model';
import { RegExpModel } from 'src/app/shared/models/regularExprssion.model';
import { UserDetails, UserRegistrationLogin } from 'src/app/shared/models/userregistrationlogin.model';
import { AuthService } from 'src/app/shared/services/auth.service';
import { UserService } from '../../../shared/services/user.service';
import { MustMatch } from '../_helpers/must-match.validator';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrl: './user-details.component.css'
})
export class UserDetailsComponent {
 
  constructor() {}

  ngOnInit(): void {
    
  }

}
