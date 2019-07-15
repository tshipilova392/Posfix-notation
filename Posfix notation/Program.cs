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
            ArithmeticExpression a = new ArithmeticExpression("(1 + 2) * 4/6");
            Console.WriteLine(a.PostfixNotation);
            Console.WriteLine(a.Value);
            Console.ReadLine();
        }
    }

    public class ArithmeticExpression
    {
        public string InfixNotation { get; private set; }
        public string PostfixNotation { get { return CreatePostfixNotation(); } private set { } }
        public double Value { get { return CalculateValueOfExpression(); } private set { } }

        private Dictionary<string, Dictionary<string, int>> operatorMatrix =
            new Dictionary<string, Dictionary<string, int>>()
            {
                ["|"] = new Dictionary<string, int>()
                {
                    ["|"] = 4,
                    ["+"] = 1,
                    ["-"] = 1,
                    ["*"] = 1,
                    ["/"] = 1,
                    ["("] = 1,
                    [")"] = 5
                },
                ["+"] = new Dictionary<string, int>()
                {
                    ["|"] = 2,
                    ["+"] = 2,
                    ["-"] = 2,
                    ["*"] = 1,
                    ["/"] = 1,
                    ["("] = 1,
                    [")"] = 2
                },
                ["-"] = new Dictionary<string, int>()
                {
                    ["|"] = 2,
                    ["+"] = 2,
                    ["-"] = 2,
                    ["*"] = 1,
                    ["/"] = 1,
                    ["("] = 1,
                    [")"] = 2
                },
                ["*"] = new Dictionary<string, int>()
                {
                    ["|"] = 2,
                    ["+"] = 2,
                    ["-"] = 2,
                    ["*"] = 2,
                    ["/"] = 2,
                    ["("] = 1,
                    [")"] = 2
                },
                ["/"] = new Dictionary<string, int>()
                {
                    ["|"] = 2,
                    ["+"] = 2,
                    ["-"] = 2,
                    ["*"] = 2,
                    ["/"] = 2,
                    ["("] = 1,
                    [")"] = 2
                },
                ["("] = new Dictionary<string, int>()
                {
                    ["|"] = 5,
                    ["+"] = 1,
                    ["-"] = 1,
                    ["*"] = 1,
                    ["/"] = 1,
                    ["("] = 1,
                    [")"] = 3
                }
            };
        private Stack<string> postfixNotationStack = new Stack<string>();
        private Stack<string> operatorsStack = new Stack<string>();

            
        public ArithmeticExpression(string expression)
        {
            InfixNotation = expression;
        }

        private string CreatePostfixNotation()
        {
            char[] operators = new char[] { '|', '+', '-', '*', '/', '(', ')' };
            operatorsStack.Push("|");

            InfixNotation = InfixNotation.Replace(" ", string.Empty);
            InfixNotation = InfixNotation + "|";
            int position = 0;
            while (position<InfixNotation.Length)
            {
                int length = InfixNotation.IndexOfAny(operators,position) - position;
                if (length == 0)
                    length = 1;//in case next block is operator
                string block = InfixNotation.Substring(position, length);
                position += length;

                AddBlockToStacks(block);                
            }

            StringBuilder result = new StringBuilder();
            Stack<string> tmpStack = new Stack<string>(postfixNotationStack);
            foreach (var n in tmpStack)
            {
                result.Append(n);
                result.Append(" ");
            }

            return result.ToString();
        }

        private void AddBlockToStacks(string block)
        {
            if (int.TryParse(block, out int n))
            {
                postfixNotationStack.Push(block);
            }
            else
            {
                string s = operatorsStack.Peek();
                int i = operatorMatrix[s][block];
                switch (i)
                {
                    case 1:
                        operatorsStack.Push(block);
                        break;
                    case 2:
                        postfixNotationStack.Push(operatorsStack.Pop());
                        AddBlockToStacks(block);
                        break;
                    case 3:
                        operatorsStack.Pop();
                        break;
                    case 4:
                        break;
                    case 5:
                        throw new ArgumentException();
                    default:
                        break;
                };
            }
        }

        private double CalculateValueOfExpression()
        {
            Stack<string> reversePostfixStack = new Stack<string>(postfixNotationStack);
            Stack<double> operandsStack = new Stack<double>();

            while (reversePostfixStack.Count!=0)
            {
                string s = reversePostfixStack.Pop();
                if (int.TryParse(s, out int n))
                {
                    operandsStack.Push(n);
                }
                else
                {
                    double operand2 = operandsStack.Pop();
                    double operand1 = operandsStack.Pop();
                    double result = CalculateOperation(operand1, operand2, s);
                    operandsStack.Push(result);
                }
            }
            
            return operandsStack.Pop();
        }

        private double CalculateOperation(double operand1,double operand2,string operation)
        {
            switch (operation)
            {
                case "+":
                    return operand1 + operand2;
                case "-":
                    return operand1 - operand2;
                case "*":
                    return operand1 * operand2;
                case "/":
                    return operand1 / operand2;
                default:
                    break;
            }
            return 0;
        }
    }
}
