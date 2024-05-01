namespace VehicleSystem.Models
{
    public class Schedule
    {
        public DateTime PickUpDate { get; set; }
        public DateTime DropOffDate { get; set; }

        public Schedule(DateTime pickUpDate, DateTime dropOffDate)
        {
            PickUpDate = pickUpDate;
            DropOffDate = dropOffDate;
        }

        public bool Overlaps(Schedule other)
        {
            return !(this.DropOffDate<=other.PickUpDate ||this.PickUpDate<=other.DropOffDate);
        }
    }
}
