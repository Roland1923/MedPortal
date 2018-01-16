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
  }

}
