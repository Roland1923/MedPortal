using System;
using System.Collections.Generic;
using Core.Entities;

namespace Core.IRepositories
{
    public interface IAppointmentRepository
    { 
        void AddAppointment(Appointment patient);
        void EditAppointment(Appointment patient);
        void DeleteAppointment(Guid id);
        IReadOnlyCollection<Appointment> GetAllAppointments();
        Appointment GetAppointmentById(Guid id);
    }
}
