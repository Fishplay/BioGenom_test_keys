using System;
using System.Collections.Generic;

namespace BioGenom_test_keys.Models
{
    public partial class NutritionReport
    {
        public NutritionReport()
        {
            CurrentMetrics = new HashSet<CurrentMetric>();
            PersonalizedSets = new HashSet<PersonalizedSet>();
        }

        public int Id { get; set; }
        public int ReducedCount { get; set; }
        public int SufficientCount { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<CurrentMetric> CurrentMetrics { get; set; }
        public virtual ICollection<PersonalizedSet> PersonalizedSets { get; set; }
    }
}
