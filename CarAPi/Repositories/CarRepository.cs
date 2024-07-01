using CarAPi.Data;
using CarAPi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarAPi.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;
        public CarRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Car>> getAllCarsAsync()
        {
            
            return  await _context.Cars.ToListAsync();
        }

        public async Task<Car> getCarByIdAsync(int carId)
        {
            return await _context.Cars.FirstOrDefaultAsync(c => c.Id == carId);
        }

        public async Task AddCarAsync(Car car)
        {
            _context.Cars.Add(car);
        }

        public async Task UpdateCarAsync(Car car)
        {
            _context.Entry(car).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public void DeleteCarAsync(Car car)
        {
            _context.Cars.Remove(car);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }


    }
}
