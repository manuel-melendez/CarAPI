using CarAPi.Entities;

namespace CarAPi.Repositories
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> getAllCarsAsync();
        Task<Car> getCarByIdAsync(int  carId);
        Task AddCarAsync(Car car);
        Task UpdateCarAsync(Car car);
        void DeleteCarAsync(Car car);
        Task<bool> SaveChangesAsync();

    }
}
