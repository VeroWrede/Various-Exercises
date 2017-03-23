using CarPricer.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPricer.Test
{

    public class CarPricerTest
    {
        [TestCase(5, 100, 97.5)]
        [TestCase(250, 100, 40)]

        public void TestApplyAgePenalty(int ageInMonths, int originalPrice, double expectedPrice)
        {
            var car = new Car
            {
                OriginalPrice = originalPrice,
                AgeInMonths = ageInMonths
            };
            var carPricer = new CarPricer();
            var price = carPricer.ApplyAgeDeduction(car, originalPrice);

            Assert.AreEqual(expectedPrice, price);
        }


        [TestCase(3, 100, 94)]
        [TestCase(7, 100, 90)]

        public void TestApplyCollisionPenalty(int numberOfCollsion, int originalPrice, double expectedPrice)
        {
            var car = new Car
            {
                OriginalPrice = originalPrice,
                NumberOfCollisions = numberOfCollsion
            };
            var carPricer = new CarPricer();
            var price = carPricer.ApplyCollisionPenalty(car, originalPrice);

            Assert.AreEqual(price, expectedPrice);
        }


        [TestCase(2, 100, 100)]
        [TestCase(5, 100, 75)]

        public void TestApplyPreviousOwnerDeduction(int numberOfPreviousOwners, int originalPrice, double expectedPrice)
        {
            var car = new Car
            {
                OriginalPrice = originalPrice,
                NumberOfPreviousOwners = numberOfPreviousOwners
            };
            var carPricer = new CarPricer();
            var price = carPricer.ApplyPreviousOwnerDeduction(car, originalPrice);

            Assert.AreEqual(price, expectedPrice);
        }


        [TestCase(0, 100, 110)]
        [TestCase(3, 100, 75)]

        public void TestApplyNoPreviousOwnerBonus(int numberOfPreviousOwners, int originalPrice, double expectedPrice)
        {
            var car = new Car
            {
                OriginalPrice = originalPrice,
                NumberOfPreviousOwners = numberOfPreviousOwners
            };
            var carPricer = new CarPricer();
            var price = carPricer.ApplyNoPreviousOwnerBonus(car, originalPrice);

            Assert.AreEqual(price, expectedPrice);
        }


        [TestCase(5000, 100, 99)]
        [TestCase(20000, 100, 70)]

        public void TestApplyMileageDeduction(int numberOfMiles, int originalPrice, double expectedPrice)
        {
            var car = new Car
            {
                OriginalPrice = originalPrice,
                NumberOfMiles = numberOfMiles
            };
            var carPricer = new CarPricer();
            var price = carPricer.ApplyMileageDeduction(car, originalPrice);

            Assert.AreEqual(price, expectedPrice);
        }


    }
}
