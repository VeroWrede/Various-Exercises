using CarPricer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarPricer
{
    public interface ICarPricer
    {
        double DetermineCarPrice(Car car);
    }
    public class CarPricer : ICarPricer
    {
        private const int MILEAGE_THRESHOLD = 150000;
        private const double MILEAGE_PENALTY_PERCENTAGE = 0.002;
        private const int MILEAGE_PENALTY_RATE = 1000;

        private const int AGE_THRESHOLD = 12 * 10;
        private const double AGE_PENALTY_PERCENTAGE = 0.005;

        private const int OWNER_THRESHOLD = 2;
        private const double OWNER_PENALTY = 0.25;

        private const double NEW_CAR_BONUS = 0.1;

        private const int COLLISION_THRESHOLD = 5;
        private const double COLLISION_PENALTY_PERCENTAGE = 0.02;


        public double DetermineCarPrice(Car car)
        {
            var priceRules = GetPriceRules();

            return this.ApplyPriceRules(car, priceRules);
        }

        private double ApplyPriceRules(Car car, IEnumerable<PriceRule> priceRules)
        {
            return priceRules.Aggregate(car.OriginalPrice, (acc, rule) => rule(car, acc));
        }

        private IEnumerable<PriceRule> GetPriceRules()
        {
            return new List<PriceRule>
            {
                ApplyAgeDeduction,
                ApplyMileageDeduction,
                ApplyPreviousOwnerDeduction,
                ApplyCollisionPenalty,
                ApplyNoPreviousOwnerBonus
            };
        }

        public double ApplyMileageDeduction(Car car, double price)
        {
            var mileageConsidered = Math.Min(MILEAGE_THRESHOLD, car.NumberOfMiles);

            var mileageDeduction = price * ((mileageConsidered / MILEAGE_PENALTY_RATE) * MILEAGE_PENALTY_PERCENTAGE);

            return price - mileageDeduction;
        }

        public double ApplyPreviousOwnerDeduction(Car car, double price)
        {
            double previousOwnerPenalty = 0;
            if (car.NumberOfPreviousOwners > OWNER_THRESHOLD)
            {
                previousOwnerPenalty = price * OWNER_PENALTY;
            }
            return price - previousOwnerPenalty;
        }

        public double ApplyNoPreviousOwnerBonus(Car car, double price)
        {
            double NoPreviousOwnerBonus = 0; 
            if (car.NumberOfPreviousOwners == 0)
            {
                NoPreviousOwnerBonus = price * NEW_CAR_BONUS;
            }
            return price + NoPreviousOwnerBonus;
        }

        public double ApplyCollisionPenalty(Car car, double price)
        {
            var collisionsConsidered = Math.Min(COLLISION_THRESHOLD, car.NumberOfCollisions);

            var collisionDeduction = price * (collisionsConsidered * COLLISION_PENALTY_PERCENTAGE);

            return price - collisionDeduction;
        }

        public double ApplyAgeDeduction(Car car, double price)
        {
            var ageConsidered = Math.Min(AGE_THRESHOLD, car.AgeInMonths);

            var ageDeduction = price * (ageConsidered * AGE_PENALTY_PERCENTAGE);

            return price - ageDeduction;

        }

        private delegate double PriceRule(Car car, double price);
    }
}
