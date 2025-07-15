using System;
using System.Collections.Generic;

namespace BioGenom_test_keys.Models
{
    public partial class SupplementNutrient
    {
        public int Id { get; set; }
        public int SetId { get; set; }
        public string Name { get; set; } = null!;
        public decimal FromSet { get; set; }
        public decimal FromNutrition { get; set; }
        public string Unit { get; set; } = null!;

        public virtual PersonalizedSet Set { get; set; }
    }
}
