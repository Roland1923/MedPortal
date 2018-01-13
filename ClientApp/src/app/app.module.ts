import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms'; 
import { HttpModule, XHRBackend } from '@angular/http';

import { routing } from './app-routing.module';

import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { UserService } from './shared/services/user.service';
import { HttpClientModule } from '@angular/common/http';

import { ConfigService } from './shared/utils/config.service';

import { HomeComponent } from './home-component/home-component.component';
import { DoctorRegisterComponent } from './doctor-register/doctor-register.component';
import { EditDoctorProfileComponent } from './edit-doctor-profile/edit-doctor-profile.component';
import { EditPatientProfileComponent } from './edit-patient-profile/edit-patient-profile.component';
import { AuthComponent } from './auth/auth.component';
import { AuthService } from './shared/services/auth.service';


@NgModule({
  declarations: [
    AppComponent,
    DoctorRegisterComponent,
    EditDoctorProfileComponent,
    EditPatientProfileComponent,
    HomeComponent,
    AuthComponent
    
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot([
      {path:'', component: AppComponent},
      {path:'home', component:HomeComponent},
      {path:'doctor-register', component:DoctorRegisterComponent},
      {path:'edit-doctor-profile', component:EditDoctorProfileComponent},
      {path:'edit-patient-profile', component:EditPatientProfileComponent},
      {path:'login',component:AuthComponent}
    ]),
    HttpClientModule,
    FormsModule,
    HttpModule,
    routing
  ],
  providers: [UserService, ConfigService],
  bootstrap: [AppComponent]
})
export class AppModule { }
