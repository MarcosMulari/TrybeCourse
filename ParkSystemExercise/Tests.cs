using System;
using Car;
using System.Collections.Generic;

namespace Tests
{
    public static class ParkingTests
    {
        public static void GenerateTestData(List<Car.Car> parkedCars)
        {
            parkedCars.Clear();
            
            parkedCars.Add(new Car.Car
            {
                Id = Guid.Parse("0b7b8f57-e21c-4a4d-905b-810141bb7c53"),
                Plate = "ABC-1209",
                IsParked = false,
                Entry = DateTime.Parse("07/04/2025 13:33"),
                Exit = DateTime.Parse("07/04/2025 13:46")
            });

            AddTestCar(parkedCars, "XYZ-9876", 5);
            AddTestCar(parkedCars, "DEF-5432", 3);
            AddTestCar(parkedCars, "GHI-1098", 2);
            AddTestCar(parkedCars, "JKL-7654", 10);
            AddTestCar(parkedCars, "MNO-3210", 19);
        }

        private static void AddTestCar(List<Car.Car> parkedCars, string plate, int hoursParked)
        {
            parkedCars.Add(new Car.Car
            {
                Id = Guid.NewGuid(),
                Plate = plate,
                IsParked = true,
                Entry = DateTime.Now.AddHours(-hoursParked),
                Exit = DateTime.MinValue
            });
        }
    }
}