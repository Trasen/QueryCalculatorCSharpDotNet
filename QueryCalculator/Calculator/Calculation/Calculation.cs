using System;
using CalculatorTests;

namespace QueryCalculator.Calculator.Calculations
{
    public interface Calculation
    {
        Calculation run(CalculationType type);
        String getResult();
    }
}