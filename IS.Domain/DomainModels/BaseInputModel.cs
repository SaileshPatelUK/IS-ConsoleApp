namespace IS.Domain.DomainModels
{
    public partial class BaseInputModel
    {
        public string Id { get; set; } = new Guid().ToString();
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public string? FaceDirection { get; set; }
        public int EventType { get; set; }

    }

    public partial class BaseInputModel : IBaseInput
    {
        public void Trim()
        {
            FaceDirection = FaceDirection?.Trim();
        }

        public string GetId()
        {
            return Id;
        }
    }
}
