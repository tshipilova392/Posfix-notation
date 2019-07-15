using Arithmetic_Expression;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postfix_notation
{
    class Program
    {
        static void Main(string[] args)
        {
            ArithmeticExpression a = new ArithmeticExpression("(1 + 2) * 4");
            Console.WriteLine(a.PostfixNotation);
            Console.WriteLine(a.Value);
            Console.ReadLine();
        }
    }   
}
