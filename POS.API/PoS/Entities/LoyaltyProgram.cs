namespace PoS.Entities
{
    public class LoyaltyProgram
    {
        public Guid ProgramId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int PointsAcquired { get; set; }
    }
}
