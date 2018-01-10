import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { DoctorProfile} from '../shared/models/doctor.profile.interface'
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

    constructor(private userService:UserService, private router: Router) { }

    ngOnInit() {
    }

    editDoctorProfile({ value, valid }: { value: DoctorProfile, valid: boolean }) {
        console.log(value.firstName + " " + value.lastName)
        this.submitted = true;
        this.isRequesting = true;
        this.errors = '';
        
        if (valid) {
            this.userService.editDoctorProfile(value.firstName,
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
                            this.router.navigate(['/edit-doctor-profile'], { queryParams: { brandNew: true, email: value.email } });
                        }
                    },
                    errors => this.errors = errors);
        }
    }
}
