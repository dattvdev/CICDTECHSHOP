using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Core.Models
{
    public class Collaborator
    {
        [Key]
        public Guid? ID { get; set; }
        public string? UserID { get; set; }
        public User? User { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [CustomStartDateValidation]
        public DateTime? StartDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [CustomEndDateValidation("StartDate")]
        public DateTime? EndDate { get; set; }
        [AllowNull]
        public Guid? InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }
    }

    public class CustomStartDateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime startDate = Convert.ToDateTime(value);
            if (startDate < DateTime.Today)
            {
                return new ValidationResult("Start Date cannot be earlier than today.");
            }
            return ValidationResult.Success;
        }
    }

    public class CustomEndDateValidation : ValidationAttribute
    {
        private readonly string _startDatePropertyName;

        public CustomEndDateValidation(string startDatePropertyName)
        {
            _startDatePropertyName = startDatePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var endDate = Convert.ToDateTime(value);
            var startDate = (DateTime?)validationContext.ObjectType
                                .GetProperty(_startDatePropertyName)?
                                .GetValue(validationContext.ObjectInstance);

            if (startDate.HasValue && endDate < startDate.Value)
            {
                return new ValidationResult("End Date cannot be earlier than Start Date.");
            }
            return ValidationResult.Success;
        }
    }
}
