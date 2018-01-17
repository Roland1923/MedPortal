import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';
import { AuthService } from '../shared/services/auth.service';

@Component({
    selector: 'app-auth',
    templateUrl: '../header/header.component.html',
    styleUrls: ['../header/header.component.scss'],
    providers: [AuthService]
  })
export class AuthComponent implements OnDestroy {

    private email: string;
    private password: string;
    private postStream$: Subscription;

    constructor(private authService: AuthService, private router: Router) { }

    login() {
        if (this.postStream$) { this.postStream$.unsubscribe }
     
        this.postStream$ = this.authService.login$(this.email, this.password).subscribe(
            result => {
                if (result.state == 1) {
                    this.router.navigate(["home"]);
                } else {
                    alert(result.msg);
                }
            }
        )
    }

    logout() {
        this.authService.logout();
    }

    ngOnDestroy() {
         if(this.postStream$){this.postStream$.unsubscribe()}
     }
}
