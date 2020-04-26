namespace Entities.DataTransferObjects.RoomDtos
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public bool HasWhiteboard { get; set; }
        public bool HasProjector { get; set; }
    }
}
