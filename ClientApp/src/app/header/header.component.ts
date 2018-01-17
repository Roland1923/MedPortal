import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthComponent } from '../auth/auth.component';
import { Subscription } from 'rxjs';
import { AuthService } from '../shared/services/auth.service';
import { AuthModel } from '../shared/models/auth.model.interface';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  providers : [AuthService]
})
export class HeaderComponent implements OnInit {
  errors: string;  
  isRequesting: boolean;
  submitted: boolean = false;

  showHeader: boolean = false;
  private postStream$: Subscription;
  private email_login: string;
  private password_login: string;
  
  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {
  }

 /* modifyHeader() {
    if(this.userService.getLogginState()) {
      this.showHeader = true;
    }
    else {
      this.showHeader = false;
    }
  }*/

  headerLogin({ value, valid }: { value: AuthModel, valid: boolean }) {
    this.submitted = true;
    this.isRequesting = true;
    this.errors = '';

    if (valid) {
        this.authService.login$(value.email,
            value.password)
            .finally(() => this.isRequesting = false)
            .subscribe(
                result => {
                    if (result) {
                        this.router.navigate(['/home']);
                    }
                },
                errors => this.errors = errors);
    }
    /*if(this.userService.getLogginState() == false) {
      this.errors = "Email sau parola incorecte";
    }*/
  }
}

//scripturile jos, css-urile sus