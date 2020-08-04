using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignmentThird
{
    class operatorsAndNumber
    {
        public enum Operator
        {
            add,
            sub,
            mul,
            div,

        }
        
       public bool IsDivMul(string s)
        {
            if (/*s.Equals("+") || s.Equals("-") ||*/ s.Equals("*") || s.Equals("/"))
            {
                return true;
            }
            return false;
        }
        //distinguish the operators
        public Operator CheckOperators(string s)
        {

            if (s.Equals("+"))
            {
                return Operator.add;
            }
            else if (s.Equals("-"))
            {
                return Operator.sub;
            }
            else if (s.Equals("*"))
            {
                return Operator.mul;
            }
            return Operator.div;
        }
        //calculate
        public int calculation(int num1, int num2, Operator o)
        {
            int result = 0;

            if (o.Equals(Operator.add))
            {
                result = num1 + num2;
               
            }
            else if (o.Equals(Operator.sub))
            {
                result = num1 - num2;
               
            }
            else if (o.Equals(Operator.mul))
            {
                result = num1 * num2;
               
            }
            else if (o.Equals(Operator.div))
            {
                try
                {
                    result = num1 / num2;
                    
                }

                catch (DivideByZeroException e)
                {
                    Console.WriteLine("The Error is  {0}", e);
                }
            }
            return result;
        }

    }
}
