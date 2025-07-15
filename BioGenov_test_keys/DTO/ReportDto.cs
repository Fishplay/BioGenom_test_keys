using BioGenom_test_keys.Models;


namespace BioGenom_test_keys.DTO
{
    public class ReportDto
    {
        public int Id { get; set; }
        public int ReducedCount { get; set; }
        public int SufficientCount { get; set; }
        public string Status { get; set; }
        public List<MetricDto> CurrentMetrics { get; set; }
        public PersonalizedSetDto PersonalizedSet { get; set; }
    }
}
