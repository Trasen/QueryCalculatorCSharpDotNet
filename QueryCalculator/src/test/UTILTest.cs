using System;
using System.Text;
using NUnit.Framework;
using QueryCalculator.Calculator;

namespace CalculatorTests
{
    public class UtilTest
    {
        private  Util util = new Util();
        
  

    [Test]
    public void replaceIndexFromTomString()
    {
        
        StringBuilder stringBuilder = util.ReplacaseIndexFromTomInString(5, 10, "(100+(50*4))*(50+(100+100))", "200");
        System.Console.WriteLine(stringBuilder);
        
        Assert.AreEqual("(100+200)*(50+(100+100))", stringBuilder.ToString());
    }
        
    }
}