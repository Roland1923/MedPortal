import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';

import { UserService } from '../shared/services/user.service';
import { DoctorProfile } from '../shared/models/doctor.profile.interface'

@Component({
  selector: 'app-doctor-profile',
  templateUrl: './doctor-profile.component.html',
  styleUrls: ['./doctor-profile.component.scss']
})
export class DoctorProfileComponent implements OnInit {

    private doctorId : string;
    doctor : DoctorProfile;
    errors : string = '';

    constructor(private userService:UserService, private router: Router, private activateRoute : ActivatedRoute) { }

    ngOnInit() {
        /*this.activateRoute.params.subscribe(params => {
            this.doctorId = params['id'];
         });

        console.log(this.doctorId);

        if(this.doctorId != null) {
            this.userService.getDoctor(this.doctorId)
            .subscribe((doctor: DoctorProfile) => {
                this.doctor = doctor;
            },
            errors => this.errors = errors
            );
        }
        else {
            this.router.navigate(['/home']);
        }*/
    }



    displayComments(event) {
        if (document.getElementById("comments_");.style.display === "none") {
            document.getElementById('addFeedback_').style.display = "none";
            document.getElementById('appointments_').style.display = "none";
            document.getElementById("comments_").style.display = "block";
        } else {
            document.getElementById('comments_').style.display = "none";
            document.getElementById('addFeedback_').style.display = "none";
            document.getElementById('appointments_').style.display = "none";
        }
        console.log(document.getElementById('comments_').style.display);
    }

    addFeedback(event) {
        if(document.getElementById("addFeedback_").style.display === "none") {
            document.getElementById("comments_").style.display = "none";
            document.getElementById("appointments_").style.display = "none";
            document.getElementById("addFeedback_").style.display = "block";
        } else {
            document.getElementById('comments_').style.display = "none";
            document.getElementById('addFeedback_').style.display = "none";
            document.getElementById('appointments_').style.display = "none";
        }
    }

    checkAppointment(event) {
        if(document.getElementById("appointments_").style.display === "none") {
            document.getElementById("comments_").style.display = "none";
            document.getElementById("addFeedback_").style.display = "none";
            document.getElementById("appointments_").style.display = "block";
        } else {
            document.getElementById('comments_').style.display = "none";
            document.getElementById('addFeedback_').style.display = "none";
            document.getElementById('appointments_').style.display = "none";
        }
    }
}
