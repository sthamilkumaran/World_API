namespace WorldAPI.Models
{
    public class States
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Population { get; set; }

        // country,State database relationship connection 
        public int CountryId { get; set; }

        public Country Country { get; set; }
    }
}
