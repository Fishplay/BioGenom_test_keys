using System;
using System.Collections.Generic;

namespace BioGenom_test_keys.Models
{
    public partial class CurrentMetric
    {
        public int Id { get; set; }
        public int ReportId { get; set; }
        public string Name { get; set; } = null!;
        public decimal CurrentValue { get; set; }
        public string NormValue { get; set; } = null!;
        public string Unit { get; set; } = null!;

        public virtual NutritionReport Report { get; set; }
    }
}
