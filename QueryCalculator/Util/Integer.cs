namespace CalculatorTests
{
   public class Integer
    {
        private int integer;

        public Integer(int integer)
        {
            this.integer = integer;
        }

        public int get()
        {
            return this.integer;
        }

        public override bool Equals(object obj)
        {
            
            Integer value = (Integer) obj;
            if (value.integer == this.integer)
            {
                return true;
            }

            return false;
        }
    }
}