import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Router, CanActivate } from '@angular/router';
import { ConfigService } from '../utils/config.service';
import{Http,HttpModule} from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { map, distinctUntilChanged, debounceTime, catchError } from 'rxjs/operators'

import { AuthBearer } from '../models/auth.interface';
import { AuthComponent } from "../../auth/auth.component";


@Injectable()
export class AuthService implements CanActivate{

    private tokeyKey = "token";
    baseUrl = '';

    constructor(private http: HttpClient,private configService: ConfigService ,private router: Router) {
        this.baseUrl = configService.getApiURI();
     }

    public canActivate() {
        if (this.checkLogin()) {
            return true;
        } else {
            this.router.navigate(['login']);
            return false;
        }
    }
   

    public login$(email: string, password: string) {
        let body = JSON.stringify({email, password});
       
        let header = new HttpHeaders().set('Content-Type', 'application/json');
        let options = { headers: header };
        console.log(body);
        
        

        return this.http.put<AuthBearer>(this.baseUrl +"api/Account/doctorAccount", body, options).pipe(
            debounceTime(200),
            distinctUntilChanged(),
            map(
                res => {
                    let result = res;
                    if (result.state && result.state == 1 && result.data && result.data.accessToken) {
                        localStorage.setItem('user_id',result.data.user_id);
                        sessionStorage.setItem(this.tokeyKey,result.data.accessToken)
                    }
                    return result;
                }
            ),

            catchError(this.handleError<AuthBearer>("login"))
        )
    }

    public authGet$(url) {
        let header = this.initAuthHeaders();
        let options = { headers: header };
        return this.http.get<any>(url, options).pipe(
            debounceTime(200),
            distinctUntilChanged(),
            catchError(this.handleError<any>("authGet")));
    }

    public checkLogin(): boolean {
        let token = sessionStorage.getItem(this.tokeyKey);
        return token != null;
    }

    public getUserInfo$() {
        return this.authGet$("/api/TokenAuth");
    }

    public authPost$(url: string, body: any) {
        let headers = this.initAuthHeaders();

        return this.http.post(url, body, { headers: headers }).pipe(
            debounceTime(200),
            distinctUntilChanged(),
            catchError(this.handleError("authPost"))
        )
    }

    private getLocalToken(): string {
        return sessionStorage.getItem(this.tokeyKey);
    }

    private initAuthHeaders(): HttpHeaders {
        let token = this.getLocalToken();
        if (token == null) throw "No token";

        let headers = new HttpHeaders()
            .set('Content-Type', 'application/json')
            .set("Authorization", "Bearer " + token);
        return headers;
    }

    private handleError<T>(operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {
            console.error(`${operation} error: ${error.message}`);
            return of(result as T);
        };
    }

    public logout()
    {
        console.log("dasDasdasdasdasd");
        localStorage.clear();
        sessionStorage.clear();
        this.router.navigate(['/home']);
    }

}
