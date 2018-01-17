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


    moreInfo(event, divID) {
        var x = document.getElementById(divID);
        if (x.style.display === "none") {
            x.style.display = "block";
        } else {
            x.style.display = "none";
        }
    }

    displayHistory() {
		if(document.getElementById("history_").style.display === "none") {
			document.getElementById("appointment_").style.display = "none";
			document.getElementById("history_").style.display = "block";
            document.getElementById("tab_content").style.disply = "block";
		} else {
			document.getElementById("history_").style.display = "none";
            document.getElementById("tab_content")
		}
	}

    displayAppointment() {
		if(document.getElementById("appointment_").style.display === "none") {
			document.getElementById("history_").style.display = "none";
			document.getElementById("appointment_").style.display = "block";
            document.getElementById("tab_content").style.disply = "block";

		} else {
			document.getElementById("appointment_").style.display = "none";
            document.getElementById("tab_content").style.disply = "none";
		}
	}

}
