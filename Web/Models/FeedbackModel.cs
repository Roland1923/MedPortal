using Core.Entities;

namespace Web.Models

{
    public class FeedbackModel
    {
        public string Description { get; set; }
        public Patient CurrentPacient { get; set; }
        public Doctor CurrentDoctor { get; set; }
        public string Rating { get; set; }

    }
}