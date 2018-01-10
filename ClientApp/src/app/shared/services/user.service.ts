import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Http, Headers, RequestOptions } from '@angular/http';

import { DoctorRegistration } from '../../shared/models/doctor.registration.interface'
import { ConfigService } from '../utils/config.service';
import {BaseService} from "./base.service";

import { Observable } from 'rxjs/Rx';
import { BehaviorSubject } from 'rxjs/Rx'; 

@Injectable()
export class UserService extends BaseService {

  baseUrl: string = '';

  // Observable navItem source
  private _authNavStatusSource = new BehaviorSubject<boolean>(false);
  // Observable navItem stream
  authNavStatus$ = this._authNavStatusSource.asObservable();

  private loggedIn = false;

  constructor(private http: Http, private configService: ConfigService) {
    super();
    this.loggedIn = !!localStorage.getItem('auth_token');
    // ?? not sure if this the best way to broadcast the status but seems to resolve issue on page refresh where auth status is lost in
    // header component resulting in authed user nav links disappearing despite the fact user is still logged in
    this._authNavStatusSource.next(this.loggedIn);
    this.baseUrl = configService.getApiURI();
  }

  doctorRegister(firstName: string, lastName: string, email: string, password: string, phoneNumber: string, speciality: string, hospital: string, city: string, address: string): Observable<DoctorRegistration> {
    //console.log(firstName + " " + lastName);
    let body = JSON.stringify({ firstName, lastName , email, password, phoneNumber, speciality, hospital, city, address });
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    
    
    console.log('body is ' + body);
    console.log('headers is ' + headers);

    return this.http.post(this.baseUrl + "api/Doctors", body, options)
      .map(res => true)
      .catch(this.handleError);
    }

}
