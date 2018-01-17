import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-patient-profile',
  templateUrl: './patient-profile.component.html',
  styleUrls: ['./patient-profile.component.scss']
})
export class PatientProfileComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }


    displayHistory(event) {
        if (document.getElementById("history_").style.display === "none") {
            document.getElementById('history_').style.display = "block";
        } else {
            document.getElementById('history_').style.display = "none";
        }
    }

    moreInfo(event, divID) {
        var x = document.getElementById(divID);
        if (x.style.display === "none") {
            x.style.display = "block";
        } else {
            x.style.display = "none";
        }
        console.log(divID);
    }

}
