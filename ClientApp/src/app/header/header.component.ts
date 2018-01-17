import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthComponent } from '../auth/auth.component';
import { UserService } from '../shared/services/user.service';
import { Subscription } from 'rxjs';
import { AuthService } from '../shared/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  providers : [AuthService]
})
export class HeaderComponent implements OnInit {
  showHeader: boolean = false;
  auth: AuthComponent;
  errors : string;
  private postStream$: Subscription;
  private email_login: string;
  private password_login: string;
  
  constructor(private authService: AuthService,private userService : UserService, private router: Router) { }

  ngOnInit() {
    this.router.events.subscribe(event => this.modifyHeader(event));
  }

  modifyHeader(location) {
    if (location.url == "/home"){
      this.showHeader = true;
    } 
    else {
      this.showHeader = false;
    }
    // if(this.userService.getLogginState()) {
    //   this.showHeader = true;
    // }
    // else {
    //   this.showHeader = false;
    // }
  }

  headerLogin() {

      if (this.postStream$) { this.postStream$.unsubscribe }
   
      this.postStream$ = this.authService.login$(this.email_login, this.password_login).subscribe(
          result => {
              if (result.state == 1) {
                  this.router.navigate(["home"]);
              } else {
                  alert(result.msg);
              }
          }
      )
  
    if(this.userService.getLogginState() == false) {
      this.errors = "Email sau parola incorecte";
    }
  }
}

//scripturile jos, css-urile sus