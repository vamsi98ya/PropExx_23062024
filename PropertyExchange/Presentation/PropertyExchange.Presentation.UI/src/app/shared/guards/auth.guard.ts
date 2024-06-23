import { Injectable } from '@angular/core';
import { Router, CanActivate} from '@angular/router';
import { AuthService } from '../services/auth.service';



@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
    constructor(
        private router: Router,
        private authService: AuthService
    ) {}

    canActivate() {
        const currentUser = this.authService.currentUserValue;
        if (currentUser) {
            // authorised so return true
            return true;
        }

        // not logged in so redirect to login page with the return url
        this.router.navigate(['/AuthenticateUser/Login']);
        return false;
    }
}