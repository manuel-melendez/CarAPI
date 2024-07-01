using CarAPi.Entities;
using CarAPi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CarAPi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/cars")]
    public class CarsController : ControllerBase
    {
        private readonly ICarRepository _carRepository;
        private readonly ILogger<CarsController> _logger;

        public CarsController(ICarRepository carRepository, ILogger<CarsController> logger)
        {
            _carRepository = carRepository ?? throw new ArgumentNullException(nameof(carRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            var cars = await _carRepository.getAllCarsAsync();

            return Ok(cars);
        }

        [HttpGet("{carId}")]
        public async Task<ActionResult<Car>> getCarById(int carId)
        {
            var car = await _carRepository.getCarByIdAsync(carId);
            if(car == null)
            {
                _logger.LogInformation($"Car with id {carId} not found");
                return NotFound();
            }

            return Ok(car);
        }

        [HttpPost]
        public async Task<ActionResult> PostCar(Car car)
        {
            await _carRepository.AddCarAsync(car);
            await _carRepository.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{carId}")]
        public async Task<ActionResult> UpdateCar(int carId, Car car)
        {
            var carToUpdate = await _carRepository.getCarByIdAsync(carId);

            if(carToUpdate == null)
            {
                _logger.LogInformation($"Car with car id {carId} not found.");
                return NotFound();
            }

            // before using automapper
            carToUpdate.Model = car.Model;
            carToUpdate.Year = car.Year;
            carToUpdate.Make = car.Make;
            carToUpdate.Mileage = car.Mileage;
            
            await _carRepository.UpdateCarAsync(carToUpdate);

            return NoContent();
        }

        [HttpDelete("{carId}")]
        public async Task<ActionResult> DeleteCar(int carId)
        {
            var carToDelete = await _carRepository.getCarByIdAsync(carId);
            if (carToDelete == null)
            {
                _logger.LogInformation($"Car with id {carId} not found.");
                return NotFound();
            }

            _carRepository.DeleteCarAsync(carToDelete);
            await _carRepository.SaveChangesAsync();

            return NoContent();
        }
    
    }
}
