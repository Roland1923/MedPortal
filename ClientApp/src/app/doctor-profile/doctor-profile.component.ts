import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-doctor-profile',
  templateUrl: './doctor-profile.component.html',
  styleUrls: ['./doctor-profile.component.scss']
})
export class DoctorProfileComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  displayComments(event) {
      if (document.getElementById("comments_").style.display === "none") {
          document.getElementById("addFeedback_").style.display = "none";
          document.getElementById('comments_').style.display = "block";
      } else {
          document.getElementById('comments_').style.display = "none";
      }
  }

  addFeedback(event) {
      if(document.getElementById("addFeedback_").style.display === "none") {
          document.getElementById("comments_").style.display = "none";
          document.getElementById("addFeedback_").style.display = "block";
      } else {
          document.getElementById("addFeedback_").style.display = "none";
      }
  }



}
