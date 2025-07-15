using System;
using System.Collections.Generic;

namespace BioGenom_test_keys.Models
{
    public partial class PersonalizedSet
    {
        public PersonalizedSet()
        {
            SupplementNutrients = new HashSet<SupplementNutrient>();
        }

        public int Id { get; set; }
        public int ReportId { get; set; }
        public string Name { get; set; } = null!;
        public string? Note { get; set; }
        public string? Description { get; set; }
        public int AlternativesCount { get; set; }

        public virtual NutritionReport Report { get; set; }
        public virtual ICollection<SupplementNutrient> SupplementNutrients { get; set; }
    }
}
