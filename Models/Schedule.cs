namespace VehicleSystem.Models
{
    public class Schedule
    {
        public DateTime PickUpDate { get; set; }
        public DateTime DropOffDate { get; set; }

        public bool Overlaps(Schedule other)
        {
            return !(this.DropOffDate <= other.PickUpDate || this.PickUpDate <= other.DropOffDate);
        }
    }
}
