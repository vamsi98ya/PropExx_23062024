import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  constructor(public authService:AuthService,
    private router: Router) { }

  ngOnInit(): void {
  }

  customLogOut(){
    this.authService.logout();
    window.localStorage.clear();
  }

}
