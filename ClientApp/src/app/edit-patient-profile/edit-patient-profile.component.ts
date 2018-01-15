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
    id : string = '40ac4cd1-5aca-4878-bed2-0b8d0985ccd9';
    patient : PatientProfile;

    constructor(private userService: UserService, private router: Router) { }

    ngOnInit() {
        this.getPatient();
    }

    private getPatient() {
        this.userService.getDoctor(this.id)
        .subscribe((patient: PatientProfile) => {
            this.patient = patient;
        },
        errors => {
            this.errors = errors;
        });
    }

    editPatientProfile({ value, valid }: { value: UpdatePatient, valid: boolean }) {
        this.submitted = true;
        this.isRequesting = true;
        this.errors = '';
        
        if(value.password != value.passwordConfirmation) {
            this.errors = "Parolele nu coincid";
        }
        else {
            if (valid) {
                this.userService.editPatientProfile(this.id,
                    value.firstName,
                    value.lastName,
                    value.email,
                    value.password,
                    value.phoneNumber,
                    value.city,
                    value.birthdate,
                    this.patient.appointments,
                    this.patient.patientHistories,
                    this.patient.feedbacks)
                    .finally(() => this.isRequesting = false)
                    .subscribe(
                        result => {
                            if (result) {
                                this.router.navigate(['/home']);
                            }
                        },
                        errors => this.errors = errors);
            }
        }
    }
}
