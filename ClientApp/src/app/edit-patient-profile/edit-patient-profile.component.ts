import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { PatientProfile } from '../shared/models/patient.profile.interface';
import { UserService } from '../shared/services/user.service';

@Component({
  selector: 'app-edit-patient-profile',
  templateUrl: './edit-patient-profile.component.html',
  styleUrls: ['./edit-patient-profile.component.scss']
})
export class EditPatientProfileComponent implements OnInit {

  errors: string;  
    isRequesting: boolean;
    submitted: boolean = false;

    constructor(private userService: UserService, private router: Router) { }

    ngOnInit() {
    }

    editPatientProfile({ value, valid }: { value: PatientProfile, valid: boolean }) {
        console.log(value.firstName + " " + value.lastName + "-----")
        this.submitted = true;
        this.isRequesting = true;
        this.errors = '';
        
        if (valid) {
            this.userService.editPatientProfile(value.firstName,
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
                            this.router.navigate(['/edit-patient-profile'], { queryParams: { brandNew: true, email: value.email } });
                        }
                    },
                    errors => this.errors = errors);
        }
    }
}
