import { Component, OnInit } from '@angular/core';
import {AuthService} from '../shared/services/auth.service';
import {Router} from '@angular/router';

import { ConfigService } from '../shared/utils/config.service';
import { DoctorFilter } from '../shared/models/doctor.filter.interface';

@Component({
  selector: 'app-home-component',
  moduleId: module.id,
  templateUrl: './home-component.component.html',
  styleUrls: ['./home-component.component.scss'],
  providers: [AuthService]
})

export class HomeComponent implements OnInit {
  characters: string[];
  baseUrl = ''
 
  constructor(private authService: AuthService, private router: Router,private configService: ConfigService) {
    this.baseUrl = configService.getApiURI();
  }

  ngOnInit() {
      
  }

  getDoctorsByFilter({value, valid}: { value: DoctorFilter, valid: boolean }) {
    value.city = "";
    value.hospital = "";
    value.speciality = ""
    
    this.router.navigate(['/doctor-search'], { queryParams: { name: value.name }});
  }

}
