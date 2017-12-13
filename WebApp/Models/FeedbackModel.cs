using Core.Entities;

namespace WebApp.Models

{
    public class FeedbackModel
    {
        public string Description { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public int Rating { get; set; }

    }
}