import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { BehaviorSubject, Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class SessionTimeoutService {
  private timeoutDuration = 30 * 60 * 1000; // 30 minutes
  private timer: any;
  private lastActivityTime: number;
  private logoutSubject = new BehaviorSubject<boolean>(false);

  constructor(private router: Router, private cookieService:CookieService) {
    this.resetTimer();
    this.setupListeners();
  }

  private resetTimer(): void {
    clearTimeout(this.timer);
    this.lastActivityTime = Date.now();
    this.timer = setTimeout(() => this.logout(), this.timeoutDuration);
  }

  private setupListeners(): void {
    window.addEventListener('mousemove', () => this.resetTimer());
    window.addEventListener('keydown', () => this.resetTimer());
  }

  private logout(): void {
    // Perform logout actions
    localStorage.removeItem('currentUser');
    localStorage.removeItem('properties');
    this.cookieService.deleteAll('currentUser');
    this.router.navigate(['/AuthenticateUser/Login']);
    this.logoutSubject.next(true);
  }

  public getLogoutObservable(): Observable<boolean> {
    return this.logoutSubject.asObservable();
  }
}
