using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DayZTradeCenter.UI.Web.Models
{
    public class CreateTradeViewModel : IValidatableObject
    {
        public IEnumerable<DomainModel.Entities.ItemViewModel> Have { get; set; }
        public IEnumerable<DomainModel.Entities.ItemViewModel> Want { get; set; }

        public bool IsHardcore { get; set; }
        public bool IsExperimental { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Have.Count() != Want.Count()) 
                yield break;
            
            if (Have.All(i => Want.Any(j => j.Id == i.Id)) &&
                Want.All(i => Have.Any(j => j.Id == i.Id)))
            {
                yield return new ValidationResult(
                    "It is not possible to create a trade for the same items.", new[] {"Have", "Want"});
            }
        }
    }
}