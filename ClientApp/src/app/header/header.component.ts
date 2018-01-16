import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthComponent } from '../auth/auth.component';
import { UserService } from '../shared/services/user.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  showHeader: boolean = false;
  auth: AuthComponent;
  errors : string;

  constructor(private userService : UserService, private router: Router) { }

  ngOnInit() {
    this.router.events.subscribe(event => this.modifyHeader(event));
  }

  modifyHeader(location) {
    if (location.url != "/home"){
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
    this.auth.login();

    /* Check if autentification faild and show error */
    if(this.userService.getLogginState() == false) {
      this.errors = "Email sau parola incorecte";
    }
  }

}
