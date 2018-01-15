import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { DoctorProfile } from '../shared/models/doctor.profile.interface'
import { UpdateDoctor } from '../shared/models/update.doctor.interface'
import { UserService } from '../shared/services/user.service';

@Component({
    selector: 'app-edit-doctor-profile',
    templateUrl: './edit-doctor-profile.component.html',
    styleUrls: ['./edit-doctor-profile.component.scss']
})
export class EditDoctorProfileComponent implements OnInit {

    errors: string;  
    isRequesting: boolean;
    submitted: boolean = false;
    id : string = 'a9e458f0-8692-437a-983b-d0e6860ac849';
    doctor: DoctorProfile;

    constructor(private userService:UserService, private router: Router) { }

    ngOnInit() {
        this.getDoctor();
    }

    private getDoctor() {
        this.userService.getDoctor(this.id)
        .subscribe((doctor: DoctorProfile) => {
            this.doctor = doctor;
        },
        errors => {
            this.errors = errors;
        });
    }
    
    editDoctorProfile({ value, valid }: { value: UpdateDoctor, valid: boolean }) {
        this.submitted = true;
        this.isRequesting = true;
        this.errors = '';

        if(value.password != value.passwordConfirmation) {
            this.errors = "Parolele nu coincid";
        }
        else {
            if (valid) {
                this.userService.editDoctorProfile(this.id,
                    value.firstName,
                    value.lastName,
                    value.email,
                    value.password,
                    value.phoneNumber,
                    value.description,
                    value.speciality,
                    value.hospital,
                    value.city,
                    value.address,
                    this.doctor.appointments,
                    this.doctor.feedbacks)
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
