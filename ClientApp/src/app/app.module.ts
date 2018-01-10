import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms'; 
import { HttpModule, XHRBackend } from '@angular/http';

import { routing } from './app-routing.module';

import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { DoctorRegisterComponent } from './doctor-register/doctor-register.component';
import { UserService } from './shared/services/user.service';
import { HttpClientModule } from '@angular/common/http';

import { ConfigService } from './shared/utils/config.service';

@NgModule({
  declarations: [
    AppComponent,
    DoctorRegisterComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot([
      {path:'', component: AppComponent},
      {path:'doctor-register', component:DoctorRegisterComponent}
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
