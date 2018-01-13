import { Component, OnInit } from '@angular/core';
import {AuthService} from '../shared/services/auth.service';
import {Router} from '@angular/router';
import { ConfigService } from '../shared/utils/config.service';
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
      if (this.authService.checkLogin()) {
          this.authService.authGet$(this.baseUrl + "api/Auth/GetStaff").subscribe(
              characters => this.characters = characters
          );
      } else {
          this.router.navigate(["login"]);
      }

  }
}
