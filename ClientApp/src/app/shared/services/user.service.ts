import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Http, Headers, RequestOptions } from '@angular/http';

import { DoctorRegistration } from '../../shared/models/doctor.registration.interface'
import { ConfigService } from '../utils/config.service';
import { BaseService } from "./base.service";

import { Observable } from 'rxjs/Rx';
import { BehaviorSubject } from 'rxjs/Rx'; 
import { first } from 'rxjs/operator/first';
import { Appointment } from '../models/appointment.interface';
import { Feedback } from '../models/feedback.interface';
import { PatientHistory } from '../models/patient.history.interface';
import { PatientProfile } from '../models/patient.profile.interface';
import { Response } from '@angular/http/src/static_response';

@Injectable()
export class UserService extends BaseService {
  baseUrl: string = '';

  private loggedIn = false;
  private pages : number;

  constructor(private http: Http, private configService: ConfigService) {
    super();
    this.loggedIn = !!localStorage.getItem('auth_token');
    this.baseUrl = configService.getApiURI();
  }

  getLogginState() {
    if(this.loggedIn) {
      return true;
    }
    return false;
  }

  getUserId() {
    if(!!localStorage.getItem('user_id') == true) {
      return localStorage.getItem('user_id');
    }
    return null;
  }

  getDoctor(id : string) {
    if(this.loggedIn == false) {
      return this.http.get(this.baseUrl + "api/Doctors/" + id)
        .map(response => response.json())
        .catch(this.handleError);
    }
  }

  doctorRegister(firstName: string, lastName: string, email: string, password: string, phoneNumber: string, description : string, speciality: string, hospital: string, city: string, address: string): Observable<DoctorRegistration> {
    let body = JSON.stringify({ firstName, lastName , email, password, phoneNumber, description, speciality, hospital, city, address });
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });

    return this.http.post(this.baseUrl + "api/Doctors", body, options)
      .map(res => true)
      .catch(this.handleError);
    }

    editDoctorProfile(id : string, firstName: string, lastName: string, email: string, password: string, phoneNumber: string, description : string, speciality: string, hospital: string, city: string, address: string, appointments : Array<Appointment>, feedbacks : Array<Feedback>): Observable<DoctorRegistration> {
      let body = JSON.stringify({ firstName, lastName , email, password, phoneNumber, description, speciality, hospital, city, address, appointments, feedbacks });
      let headers = new Headers({ 'Content-Type': 'application/json' });
      let options = new RequestOptions({ headers: headers });
      
      return this.http.put(this.baseUrl + "api/Doctors/" + id, body, options)
        .map(res => true)
        .catch(this.handleError);
    }

    getDoctorsByFilter(name : string, hospital : string, speciality : string, city : string, skip : number, take : number) {
      let body = JSON.stringify({ name, hospital , speciality, city});
      let headers = new Headers({ 'Content-Type': 'application/json' });
      let options = new RequestOptions({ headers: headers });

      return this.http.put(this.baseUrl + "api/Doctors/page/" + skip + "/" + take, body, options)
        .map(response => response)
        .catch(this.handleError);
    }
    

    /* PATIENT */
    getPatient (id : string) {
      return this.http.get(this.baseUrl + "api/Patients/" + id)
        .map(response => response.json())
        .catch(this.handleError);
    }

    patientRegister(firstName: string, lastName: string, email: string, password: string, phoneNumber: string, city: string, birthdate: Date): Observable<DoctorRegistration> {
      let body = JSON.stringify({ firstName, lastName , email, password, phoneNumber, city, birthdate });
      let headers = new Headers({ 'Content-Type': 'application/json' });
      let options = new RequestOptions({ headers: headers });
  
      return this.http.post(this.baseUrl + "api/Patients", body, options)
        .map(res => true)
        .catch(this.handleError);
      }

    editPatientProfile(id, firstName: string, lastName: string, email: string, password: string, phoneNumber: string, city: string, birthdate: Date, appointments : Array<Appointment>, patientHistories : Array<PatientHistory>, feedbacks : Array<Feedback>): Observable<DoctorRegistration> {
      let body = JSON.stringify({ firstName, lastName , email, password, phoneNumber, city, birthdate, appointments, patientHistories, feedbacks });
      let headers = new Headers({ 'Content-Type': 'application/json' });
      let options = new RequestOptions({ headers: headers });
  
      return this.http.put(this.baseUrl + "api/Patients/" + id, body, options)
        .map(res => true)
        .catch(this.handleError);
    }

    addFeedback(description : string, rating : number) {
      
    }
}
