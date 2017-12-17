﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Feedback
    {
        [Key]
        public Guid FeedbackId { get; private set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; private set; }
        public Guid PatientId { get; private set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; private set; }
        public Guid DoctorId { get; private set; }
        public string Description { get; private set; }
        public int Rating { get; private set; }

        private Feedback() { }

        public static Feedback Create(string description, Patient patient, Doctor doctor, int rating)
        {
            var instance = new Feedback { FeedbackId = Guid.NewGuid() };
            instance.Update(description, patient, doctor, rating);
            return instance;
        }

        public void Update(string description, Patient patient, Doctor doctor, int rating)
        {
            Description = description;
            Patient = patient;
            Doctor = doctor;
            Rating = rating;
        }
    }
}
