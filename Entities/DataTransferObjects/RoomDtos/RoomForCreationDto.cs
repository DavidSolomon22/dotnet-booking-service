namespace Entities.DataTransferObjects.RoomDtos
{
    public class RoomForCreationDto
    {
        public string Number { get; set; }
        public bool HasWhiteboard { get; set; }
        public bool HasProjector { get; set; }
    }
}
