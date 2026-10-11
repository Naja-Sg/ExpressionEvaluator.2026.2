using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection.Metadata;

namespace Backend;

public static class ExpressionEvaluator
{
    public static double Evaluate(string infix) => EvalutePostfix(ToPostfix(infix));

    private static string ToPostfix(string infix)
    {
        var posfix = string.Empty;
        var stack = new Stack<char>();
        var accumulator = string.Empty;

        foreach (var item in infix)
        {
            if ((item >= '0' && item <= '9') || item == '.') { //here, I build numbers that have more than 1 digit or are decimals

                accumulator += item;
            }

            else if (IsOperator(item))
            {

                if (accumulator != string.Empty)
                {
                    posfix += accumulator + " "; //add to potstfix the number saved in accumulator
                    accumulator = string.Empty; //reset the accumulator to empty
                }

                if (item == ')')
                {
                    var ope = stack.Pop();
                    while(ope != '(')
                    {
                        posfix += ope + " ";
                        ope = stack.Pop();
                    }
                }
                else
                {
                    if (stack.Count == 0)
                    {
                        stack.Push(item);
                    }
                    else
                    {
                        if (PriorityInfix(item) > PriorityStack(stack.Peek()))
                        {
                            stack.Push(item);
                        }
                        else
                        {
                            posfix += stack.Pop() + " ";
                            stack.Push(item);
                        }
                    }
                }
            }
        }

        if (accumulator != string.Empty)
        {
            posfix += accumulator + " ";
        }

        do
        {
            posfix += stack.Pop() + " "; //edited this to add a space to differentiate characters
        } while (stack.Count != 0);
        return posfix.Trim(); //This trim visually removes the extra space at the end of the concatenation that we used before to do the differentiation (not the spaces in between)
    }

    private static int PriorityStack(char op) => op switch
    {
        '^' => 3,
        '*' => 2,
        '/' => 2,
        '+' => 1,
        '-' => 1,
        '(' => 0,
        _ => throw new Exception("Invalid expression."),
    };

    private static int PriorityInfix(char op) => op switch
    {
        '^' => 4,
        '*' => 2,
        '/' => 2,
        '+' => 1,
        '-' => 1,
        '(' => 5,
        _ => throw new Exception("Invalid expression."),
    };

    private static bool IsOperator(char item) => item == '^' || item == '*' || item == '/' || item == '+' || item == '-' || item == '(' || item == ')';

    private static double EvalutePostfix(string postfix)
    {
        var stack = new Stack<double>();
        var sepparators = postfix.Split(' ');

        foreach (var sepparator in sepparators)
        {
            if (sepparator.Length == 1 && IsOperator(sepparator[0])) // take the first character in Sepparators list
            {
                var ope2 = stack.Pop();
                var ope1 = stack.Pop();
                stack.Push(Calculate(ope1, ope2, sepparator[0]));
            }
            else
            {
                stack.Push(double.Parse(sepparator, CultureInfo.InvariantCulture)); //cast the string to number and add . as decimal sepparator
            }
        }
        return stack.Pop();
    }

    private static double Calculate(double ope1, double ope2, char item) => item switch
    {
        '*' => ope1 * ope2,
        '/' => ope1 / ope2,
        '+' => ope1 + ope2,
        '-' => ope1 - ope2,
        '^' => Math.Pow(ope1, ope2),
        _ => throw new Exception("Invalid expression."),
    };
}
