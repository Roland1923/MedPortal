import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { UserService } from '../shared/services/user.service';
import { DoctorProfile } from '../shared/models/doctor.profile.interface';
import { DoctorFilter } from '../shared/models/doctor.filter.interface';

@Component({
  selector: 'app-doctor-search',
  templateUrl: './doctor-search.component.html',
  styleUrls: ['./doctor-search.component.scss']
})
export class DoctorSearchComponent implements OnInit {
  errors : string = '';
  submitted = true;
  isRequesting = true;

  doctorsList : Array<DoctorProfile>;
  doctor : DoctorProfile;
  userId : string = '40626b74-1af9-4314-b3fc-2c5a7ff8b9c0';

  constructor(private userService:UserService, private router: Router) { 
    this.doctorsList = new Array<DoctorProfile>();
  }

  ngOnInit() {

  }

  getDoctorsByFilter({ value, valid }: { value: DoctorFilter, valid: boolean }) {
    this.submitted = true;
    this.isRequesting = true;
    this.errors = '';
    
    if (valid) {
      this.userService.getDoctorsByFilter(value.name,
          value.hospital,
          value.speciality,
          value.city,
          value.skip,
          10)
          .subscribe((doctors: Array<DoctorProfile>) => {
              this.doctorsList = doctors;
          },
          errors => this.errors = errors
          );
    }
  }

}
