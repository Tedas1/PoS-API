namespace PoS.Entities
{
    public class Tax
    {
        public Guid TaxId { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Percentage { get; set; }
    }
}
