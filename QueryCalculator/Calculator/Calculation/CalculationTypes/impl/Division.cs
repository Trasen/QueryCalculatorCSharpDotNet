﻿namespace QueryCalculator.Calculator.Calculations
{
    class Division : CalculationType
    {
        public Division() : base('/', 10)
        {
           
        }

        protected override decimal calculate(decimal num1, decimal num2)
        {
            return num1 / num2;
        }
    }
}