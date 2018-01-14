import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { PatientRegistration } from '../shared/models/patient.registration.interface'
import { UserService } from '../shared/services/user.service';
import { error } from 'selenium-webdriver';

@Component({
  selector: 'app-patient-register',
  templateUrl: './patient-register.component.html',
  styleUrls: ['./patient-register.component.scss']
})
export class PatientRegisterComponent implements OnInit {

  errors: string;  
  isRequesting: boolean;
  submitted: boolean = false;

  constructor(private userService:UserService, private router: Router) { }

  ngOnInit() {

  }

  patientRegister({ value, valid }: { value: PatientRegistration, valid: boolean }) {
    this.submitted = true;
    this.isRequesting = true;
    this.errors = '';
    console.log(value);
    if (valid) {
        this.userService.patientRegister(value.firstName,
            value.lastName,
            value.email,
            value.password,
            value.phoneNumber,
            value.city,
            value.birthdate)
            .finally(() => this.isRequesting = false)
            .subscribe(
                result => {
                    if (result) {
                        this.router.navigate(['/edit-patient-profile']);
                    }
                },
                errors => {
                  this.errors = "<i class=\"fa fa-close\">" + errors + "<\\i>";
                  console.log(errors);
              });
    }
  }

  expandRegisterForm(event) {
    var button = document.getElementById("register-btn");
    if (event.target.checked) {
      document.getElementById('donorDiv').style.display = 'none';
      button.classList.remove('btn-danger');
      button.classList.add('btn-info');
    }
    else {
      document.getElementById('donorDiv').style.display = 'block';
      button.classList.remove('btn-info');
      button.classList.add('btn-danger');
    }
  }
}
