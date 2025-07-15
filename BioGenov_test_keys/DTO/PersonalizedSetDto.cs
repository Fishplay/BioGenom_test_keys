namespace BioGenom_test_keys.DTO
{
    public class PersonalizedSetDto
    {
        public string Name { get; set; }
        public string Note { get; set; }
        public string Description { get; set; }
        public int AlternativesCount { get; set; }
        public List<SupplementNutrientDto> Nutrients { get; set; }
    }
}