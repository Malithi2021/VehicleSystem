using VehicleSystem.Models;

namespace VehicleSystem.Interfaces
{
    public interface IOverlappable
    {
        public bool Overlaps(Schedule other);

    }
}
