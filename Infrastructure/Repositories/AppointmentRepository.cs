using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.IRepositories;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly IDatabaseService _databaseService;

        public AppointmentRepository(IDatabaseService appointmnetService)
        {
            _databaseService = appointmnetService;
        }

        public void AddAppointment(Appointment appointment)
        {
            _databaseService.Appointments.Add(appointment);
            _databaseService.SaveChanges();
        }

        public void EditAppointment(Appointment appointment)
        {
            _databaseService.Appointments.Update(appointment);
            _databaseService.SaveChanges();
        }

        public void DeleteAppointment(Guid id)
        {
            var appointment = GetAppointmentById(id);
            _databaseService.Appointments.Remove(appointment);
            _databaseService.SaveChanges();
        }

        public Appointment GetAppointmentById(Guid id)
        {
            return _databaseService.Appointments.FirstOrDefault(t => t.AppointmentId == id);
        }

        public IReadOnlyCollection<Appointment> GetAllAppointments()
        {
            return _databaseService.Appointments.ToList();
        }
    }
}
