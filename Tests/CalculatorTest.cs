using System;
using NUnit.Framework;
using QueryCalculator.Calculator;

namespace CalculatorTests
{
    public class Tests
    {
        private Calculator calculator = new CalculatorImpl();

        [Test]
        public void SingleDivision()
        {
            Assert.AreEqual("10", calculator.calculate("100/10"));
        }

        [Test]
        public void Addition()
        {
            Assert.AreEqual("10", calculator.calculate("5+5"));
        }

        [Test]
        public void Subtraction()
        {
            Assert.AreEqual("0", calculator.calculate("5-5"));
        }

        [Test]
        public void PowerOf()
        {
            Assert.AreEqual("4", calculator.calculate("2^2"));
        }

        [Test]
        public void Multiplication()
        {
            Assert.AreEqual("4", calculator.calculate("2*2"));
        }
        
        [Test]
        public void SquareRoot()
        {
            Assert.AreEqual("2", calculator.calculate("√4"));
        }

        [Test]
        public void SquareRootLarge()
        {
            Assert.AreEqual("28.2842712474619", calculator.calculate("√800"));
        }
        
        [Test]
        public void SquareRootLargeCombined()
        {
            Assert.AreEqual("35.9911100134464", calculator.calculate("√(500+6) * 8 / 5"));
        }
        
        [Test]
        public void SquareRootLargeCombinedReversed()
        {
            Assert.AreEqual("35.9911100134464", calculator.calculate("8 / 5 * √(500+6)"));
        }

        [Test]
        public void testCalculatorRemoveUnspportedSigns()
        {
            Assert.AreEqual("10", calculator.calculate("100hgkjölkrty/10asdbgfhölk"));
        }


        [Test]
        public void NestedExpressions()
        {
            Assert.AreEqual("75000", calculator.calculate("(100+200) * (50+200)"));
        }

        [Test]
        public void NestedNestedExpressions()
        {
            Assert.AreEqual("75000", calculator.calculate("(100+(50*4)) * (50+(100+100))"));
        }

        [Test]
        public void rMultiDivision()
        {
            Assert.AreEqual("0.1", calculator.calculate("100/10/100"));
        }

        [Test]
        public void MultiDivisionAndMultiplication()
        {
            Assert.AreEqual("100.0", calculator.calculate("100/10/100*1000"));
        }

        [Test]
        public void MultiDivisionMultiplicationAndAddition()
        {
            Assert.AreEqual("2100.0", calculator.calculate("100/10/100*1000+2000"));
        }

        [Test]
        public void MultiSwapped()
        {
            Assert.AreEqual("8700", calculator.calculate("200+100+400+200*200/5"));
        }

        [Test]
        public void MultiSwappedMixed()
        {
            Assert.AreEqual("940", calculator.calculate("200+100+200/5+400+200"));
        }

        [Test]
        public void MultiSwappedMixedMixed()
        {
            Assert.AreEqual("940", calculator.calculate("200+100+200/5+400+200"));
        }

        [Test]
        public void SubstrationDivisionMultiplicationAndAddition()
        {
            Assert.AreEqual("-7300", calculator.calculate("100-100 * 400 / 5 + 600"));
        }

        [Test]
        public void veryLargeValues()
        {
            Assert.AreEqual("99999999998999900000000001", calculator.calculate("99999999999*999999999999999"));
        }

        // [Test] Need to find a proper replacement for BigDecimal from Java, which uses margin math with string replacements to do insane number sizes.
        // public void veryVeryLargeValues() {
        //     Assert.AreEqual(new Integer(299999), new Integer(calculator.calculate("100.0^99999").Length));
        // }
    }
}