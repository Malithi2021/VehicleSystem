namespace VehicleSystem.Models
{
    public class Schedule(DateTime pickUpDate, DateTime dropOffDate)
    {
        public DateTime PickUpDate { get; set; } = pickUpDate;
        public DateTime DropOffDate { get; set; } = dropOffDate;

        public bool Overlaps(Schedule other)
        {
            return !(this.DropOffDate<=other.PickUpDate ||this.PickUpDate<=other.DropOffDate);
        }
    }
}
