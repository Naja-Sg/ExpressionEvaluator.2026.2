using Backend;

var infix1 = "4*(5+6-(8/2^3)-7)-1";
Console.WriteLine($"Infix = {infix1}, Result = {ExpressionEvaluator.Evaluate(infix1):N5}"); // 11. Test with integers

var infix2 = "144^(1/2)";
Console.WriteLine($"Infix = {infix2}, Result = {ExpressionEvaluator.Evaluate(infix2):N5}"); // 12. Test with numbers of more than 1 digit

var infix3 = "2.5+20*(10.65-8/3)";
Console.WriteLine($"Infix = {infix3}, Result = {ExpressionEvaluator.Evaluate(infix3):N5}"); // 162.16667. Test with decimals


