import { Appointment } from "./appointment.interface";
import { Feedback } from "./feedback.interface";

export interface DoctorProfile {
    id : string;
    firstName : string;
    lastName : string;
    email : string;
    password : string;
    phoneNumber : string;
    description : string;
    speciality : string;
    hospital : string;
    city : string;
    address : string;
    
    appointments: Array<Appointment>;
    feedbacks: Array<Feedback>;
}
