using System;
using System.Text.Json.Serialization;

namespace LoanCalculator.Application.Calculation.Dtos
{
    public class QuoteSubmissionPostModel
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int Term { get; set; }
        public double InterestRate { get; set; }
        public double LoanAmount { get; set; }
        [JsonIgnore]
        public DateTime LoanDate { get; set; }
        [JsonIgnore]
        public DateTime StartDate { get; set; }
    }

}
