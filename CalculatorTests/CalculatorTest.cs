using System;
using NUnit.Framework;
using QueryCalculator.Calculator;

namespace CalculatorTests
{
    public class Tests
    {
        private Calculator calculator = new CalculatorImpl();
        
    [Test]
    public void testCalculatorSingleDivision() {

        Assert.AreSame("10", calculator.calculate("100/10"));
    }

    [Test]
    public void calculateNestedExpressions() {
        Assert.AreSame("75000", calculator.calculate("(100+200) * (50+200)"));
    }

    [Test]
    public void calculateNestedNestedExpressions() {
        Assert.AreSame("75000", calculator.calculate("(100+(50*4)) * (50+(100+100))"));
    }

    [Test]
    public void testCalculatorMultiDivision() {

        Assert.AreSame("0.1", calculator.calculate("100/10/100"));
    }

    [Test]
    public void testCalculatorMultiDivisionAndMultiplication() {

        Assert.AreSame("100.0", calculator.calculate("100/10/100*1000"));
    }

    [Test]
    public void testCalculatorMultiDivisionMultiplicationAndAddition() {

        Assert.AreSame("2100.0", calculator.calculate("100/10/100*1000+2000"));
    }

    [Test]
    public void testCalculatorMultiSwapped() {
        Assert.AreSame("8700", calculator.calculate("200+100+400+200*200/5"));
    }

    [Test]
    public void testCalculatorMultiSwappedMixed() {
        Assert.AreSame("940", calculator.calculate("200+100+200/5+400+200"));
    }

    [Test]
    public void testCalculatorMultiSwappedMixedMixed() {
        Assert.AreSame("940", calculator.calculate("200+100+200/5+400+200"));
    }

    [Test]
    public void testCalculatorSubstration() {
        Assert.AreSame("0", calculator.calculate("100-100"));
    }

    [Test]
    public void testCalculatorSubstrationDivisionMultiplicationAndAddition() {
        Assert.AreSame("-7300", calculator.calculate("100-100 * 400 / 5 + 600"));
    }

    [Test]
    public void testPowerOf() {
        Assert.AreSame("4", calculator.calculate("2^2"));
    }

    [Test]
    public void veryLargeValues() {
        Assert.AreSame("99999999998999900000000001", calculator.calculate("99999999999*999999999999999"));
    }

    [Test]
    public void veryVeryLargeValues() {
        Assert.AreSame(new Integer(299999), new Integer(calculator.calculate("100.0^99999").Length));
    }
        
    }
}