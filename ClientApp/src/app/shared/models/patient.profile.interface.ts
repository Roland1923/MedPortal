import { Appointment } from "./appointment.interface";
import { Feedback } from "./feedback.interface";
import { PatientHistory } from "./patient.history.interface";

export interface PatientProfile {
    firstName : string;
    lastName : string;
    email : string;
    password : string;
    phoneNumber : string;
    city : string;
    birthdate : string;

    appointments : Array<Appointment>;
    feedbacks : Array<Feedback>;
    patientHistories : Array<PatientHistory>;
}
