import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { UserService } from '../shared/services/user.service';
import { DoctorProfile } from '../shared/models/doctor.profile.interface';
import { DoctorFilter } from '../shared/models/doctor.filter.interface';
import { Response } from '@angular/http/src/static_response';
import { DoctorProfileComponent } from '../doctor-profile/doctor-profile.component';

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
  numberPages : number;
  numbers : Array<number>;

  constructor(private userService:UserService, private router: Router) { 
    this.doctorsList = new Array<DoctorProfile>();
    this.numbers = new Array<number>();
  }

  ngOnInit() {

  }

  getDoctorsByFilter({ value, valid }: { value: DoctorFilter, valid: boolean }, skip : number) {
    this.submitted = true;
    this.isRequesting = true;
    this.errors = '';

    this.doctorsList = [];
    this.numbers = [];
    this.numberPages = 0;

    if (valid) {
      this.userService.getDoctorsByFilter(value.name,
          value.hospital,
          value.speciality,
          value.city,
          skip * 10,
          10)
          .subscribe((response : Response) => {
            this.doctorsList = response.json();
            this.numberPages = +response.headers.get('x-inlinecount');
            
            console.log(this.numberPages);
            
            this.numbers = [];
            for(let i = 0; i < this.numberPages / 10; i++)
            {
              this.numbers.push(i+1);
            }

            if(this.doctorsList.length == 0) {
              this.numbers = [];
            }
        },
        errors => this.errors = errors
        );
    }
  }

  redirectToDoctorProfile(id) {
    
  }
}
