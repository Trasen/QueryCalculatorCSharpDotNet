using System;
using NUnit.Framework;
using QueryCalculator.Calculator;

namespace CalculatorTests
{
    public class CalculationTest
    {
        
        [Test]
        public void NotNested()
        {
            NestedCalculation nestedCalculation = new NestedCalculation("1+1");
            Assert.AreEqual("1+1" , nestedCalculation.getCalcualtion());
        }
        
        private static NestedCalculation NestedCalculation(String query)
        {
            return new NestedCalculation(query);
        }

        [Test]
        public void Nested()
        {
            NestedCalculation nestedCalculation = new NestedCalculation("(1+1)");
            Assert.AreEqual("1+1" , nestedCalculation.getCalcualtion());
        }
        
        [Test]
        public void OpenNotClosed()
        {
            Assert.Throws<YouNeedToFinishNestedCalculationExpression>( () => new NestedCalculation("(1+1"));
        }
        
        [Test]
        public void NullNotAccepted()
        {
            Assert.Throws<NullReferenceException>(() => new NestedCalculation(null));
        }
    }
}