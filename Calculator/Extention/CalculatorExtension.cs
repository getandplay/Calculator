using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Data;

namespace Calculator
{
    static class CalculatorExtension
    {
        private static string[] _operators = new[] { "+", "-", "*", "/" };
        public static bool IsOperator(this string value)
        {
            return _operators.Contains(value);
        }

        public static double GetValue(this string formula)
        {
            double result = 0;
            formula = GetTheValidatedFormula(formula);
            try
            {
                result = Convert.ToDouble(new DataTable().Compute(formula, null));
            }
            catch (SyntaxErrorException e)
            {
                // log here
            }
            return double.IsInfinity(result) ? 0 : result;
        }

        public static bool IsEndWithOperator(this string formula)
        {
            return _operators.Any(x => formula.EndsWith(x));
        }

        public static bool HasOperator(this string formula)
        {
            return _operators.Any(x => formula.Contains(x));
        }

        private static string GetTheValidatedFormula(string formula)
        {
            if (formula.IsEndWithOperator())
            {
                formula = formula.Substring(0, formula.Length - 1);
            }
            return formula;
        }
    }
}
