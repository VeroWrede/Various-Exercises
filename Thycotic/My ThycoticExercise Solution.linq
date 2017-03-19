<Query Kind="Program" />

/*
 * You are tasked with writing an algorithm that determines the value of a used car, 
 * given several factors.
 * 
 *    AGE:    Given the number of months of how old the car is, reduce its value one-half 
 *            (0.5) percent.
 *            After 10 years, it's value cannot be reduced further by age. This is not 
 *            cumulative.
 *            
// *    MILES:    For every 1,000 miles on the car, reduce its value by one-fifth of a
 *              percent (0.2). Do not consider remaining miles. After 150,000 miles, it's 
 *              value cannot be reduced further by miles.
 *            
 *    PREVIOUS OWNER:    If the car has had more than 2 previous owners, reduce its value 
 *                       by twenty-five (25) percent. If the car has had no previous  
 *                       owners, add ten (10) percent of the FINAL car value at the end.
 *                    
 *    COLLISION:        For every reported collision the car has been in, remove two (2) 
 *                      percent of it's value up to five (5) collisions.
 *                    
 * 
 *    Each factor should be off of the result of the previous value in the order of
 *        1. AGE
 *        2. MILES
 *        3. PREVIOUS OWNER
 *        4. COLLISION
 *        
 *    E.g., Start with the current value of the car, then adjust for age, take that  
 *    result then adjust for miles, then collision, and finally previous owner. 
 *    Note that if previous owner, had a positive effect, then it should be applied 
 *    AFTER step 4. If a negative effect, then BEFORE step 4.
 */

void Main()
{
	var tests = new UnitTests();
	tests.CalculateCarValue();
}

namespace CarPricer
{
    public class Car
    {
        public decimal PurchaseValue { get; set; }
        public int AgeInMonths { get; set; }
        public int NumberOfMiles { get; set; }
        public int NumberOfPreviousOwners { get; set; }
        public int NumberOfCollisions { get; set; }
    }

    public class PriceDeterminator
    {
        public decimal DetermineCarPrice(Car car)
        {
			var price = car.PurchaseValue;
			price = AgePriceReduction(price, car.AgeInMonths);
			price = MilesPriceReduction(price, car.NumberOfMiles);
			price = OwnerPenalty(price, car.NumberOfPreviousOwners);
			price = CollisionPenalty(price, car.NumberOfCollisions);
			// adding 10% of price if there were no previous owners 
			if(car.NumberOfPreviousOwners == 0)
			{
				price *= 1.1m;
			}
			
			return price;
		}
		
		private decimal AgePriceReduction(decimal price, int ageInMonths)
		{
			const decimal monthlyReduction = 0.005m;
			//age based price reduction stops after 10 years of age
			var effectiveAgeInMonths = Math.Min(ageInMonths, 12*10); 
			return price * (1-(monthlyReduction * effectiveAgeInMonths));	
		}
		
		private decimal MilesPriceReduction(decimal price, int numberMiles)
		{
			const decimal milesReduction = 0.002m;
			// miles based price reduction stops after 150,000 miles
			var effectiveNumberMiles = Math.Min(numberMiles, 150000);
			return price * (1-(effectiveNumberMiles/1000m) *milesReduction);
		}
		
		private decimal OwnerPenalty(decimal price, int numberOwners)
		{
			if(numberOwners > 2)
			{
				return price * 0.75m;
			} 
			else 
			{
				return price;	
			}
		}
		
		private decimal CollisionPenalty(decimal price, int numberCollisions)
		{
			const decimal collisionReduction = 0.02m;
			// collision based price reduction stops after 5 collisions
			var effectiveNumberOfCollisions = Math.Min(5, numberCollisions);
			return price * (1- (effectiveNumberOfCollisions * collisionReduction));
		}
    }

    
    public class UnitTests
    {
        
        public void CalculateCarValue()
        {
            AssertCarValue(25313.40m, 35000m, 3 * 12, 50000,  1, 1);
            AssertCarValue(19688.20m, 35000m, 3 * 12, 150000, 1, 1);
            AssertCarValue(19688.20m, 35000m, 3 * 12, 250000, 1, 1);
            AssertCarValue(20090.00m, 35000m, 3 * 12, 250000, 1, 0);
            AssertCarValue(21657.02m, 35000m, 3 * 12, 250000, 0, 1);
        }

        private static void AssertCarValue(decimal expectValue, decimal purchaseValue, 
        int ageInMonths, int numberOfMiles, int numberOfPreviousOwners, int 
        numberOfCollisions)
        {
            Car car = new Car
                        {
                            AgeInMonths = ageInMonths,
                            NumberOfCollisions = numberOfCollisions,
                            NumberOfMiles = numberOfMiles,
                            NumberOfPreviousOwners = numberOfPreviousOwners,
                            PurchaseValue = purchaseValue
                        };
            PriceDeterminator priceDeterminator = new PriceDeterminator();
            var carPrice = priceDeterminator.DetermineCarPrice(car);
            Assert.AreEqual(expectValue, carPrice);
        }
    }
}