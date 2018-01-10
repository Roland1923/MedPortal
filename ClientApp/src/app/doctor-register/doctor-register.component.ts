import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { DoctorRegistration } from '../shared/models/doctor.registration.interface'
import { UserService } from '../shared/services/user.service';

@Component({
  selector: 'app-doctor-register',
  templateUrl: './doctor-register.component.html',
  styleUrls: ['./doctor-register.component.scss']
})
export class DoctorRegisterComponent implements OnInit {

  errors: string;  
  isRequesting: boolean;
  submitted: boolean = false;

  constructor(private userService:UserService, private router: Router) { }

  ngOnInit() {

  }

  registerDoctor({ value, valid }: { value: DoctorRegistration, valid: boolean }) {
    this.submitted = true;
    this.isRequesting = true;
    this.errors = '';
    console.log("firstname: " + value.firstName)
    console.log("lastname: " + value.lastName)
    if (valid) {
        this.userService.doctorRegister(value.firstName,
            value.lastName,
            value.email,
            value.password,
            value.phoneNumber,
            value.speciality,
            value.hospital,
            value.city,
            value.address)
            .finally(() => this.isRequesting = false)
            .subscribe(
                result => {
                    if (result) {
                        this.router.navigate(['/doctor-register'], { queryParams: { brandNew: true, email: value.email } });
                    }
                },
                errors => this.errors = errors);
    }
}

}