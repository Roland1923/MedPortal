import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

export class AppComponent implements OnInit {
  showHeader: boolean = true;
  constructor(private router: Router) {}
 
  ngOnInit() {
   // listenging to routing navigation event
   this.router.events.subscribe(event => this.modifyHeader(event));
  }
 
  modifyHeader(location) {
    if (location.url != "/home")
    {
        this.showHeader = false;
    } else {
        this.showHeader = true;
    }
 }
 }