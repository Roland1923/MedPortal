import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { PatientProfile } from '../shared/models/patient.profile.interface';
import { UpdatePatient } from '../shared/models/update.patient.interface';
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

    patient : PatientProfile;

    constructor(private userService: UserService, private router: Router) { }

    ngOnInit() {
        this.getPatient();
    }

    private getPatient() {
        this.userService.getDoctor("77dfb343-ff30-4c6b-9ae6-2acece49befd")
        .subscribe((patient: PatientProfile) => {
            this.patient = patient;
        },
        error => {
            this.errors = error;
        });
    }

    editPatientProfile({ value, valid }: { value: UpdatePatient, valid: boolean }) {
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
                            this.router.navigate(['/edit-patient-profile']);
                        }
                    },
                    errors => this.errors = errors);
        }
    }
}
