using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DayZTradeCenter.UI.Web.Models
{
    public class CreateTradeViewModel : IValidatableObject
    {
        [Display(Name = "Have")]
        public int HaveId { get; set; }

        [Display(Name = "Want")]
        public int WantId { get; set; }

        public SelectList Items { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (HaveId == WantId)
            {
                yield return new ValidationResult(
                    "It is not possible to create a trade for the same item.", new[] { "HaveId", "WantId" });
            }
        }
    }
}