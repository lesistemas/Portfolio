namespace ProductInventoryChecker.Application.Interfaces
{
    public interface IProductAvailabilityService
    {
        void CheckAllProductQuantities(string shopUrl);
    }
}
