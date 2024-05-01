using VehicleSystem.Models;

namespace VehicleSystem.Interfaces
{
    public interface IOverlappable
    {
        bool Overlaps(Schedule other);

    }
}
