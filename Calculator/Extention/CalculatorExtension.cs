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
            catch (SyntaxErrorException)
            {
                // log here
            }
            catch (DivideByZeroException)
            {
                //log here
            }
            return double.IsInfinity(result)||double.IsNaN(result) ? 0 : result;
        }

        public static bool EndWithOperator(this string formula)
        {
            return _operators.Any(x => formula.EndsWith(x));
        }

        public static bool HasOperator(this string formula)
        {
            return _operators.Any(x => formula.Contains(x));
        }

        /// <summary>
        /// Check if a dot can be added at the end of formula
        /// </summary>
        /// <param name="formula"></param>
        /// <returns></returns>
        public static bool CanAddDot(this string formula)
        {
            const string dot = ".";
            if (!formula.HasOperator())
            {
                return !formula.Contains(dot);
            }
            else
            {
                var rightPart = formula.GetRightPartOfFormula();
                if (string.IsNullOrEmpty(rightPart)) return true;
                var currentValue = rightPart.Substring(1);
                return !currentValue.Contains(dot);
            }
        }

        public static string GetRightPartOfFormula(this string formula)
        {
            var result = string.Empty;
            foreach (var @operator in _operators)
            {
                if (formula.Contains(@operator))
                {
                    var operatorIndex = formula.IndexOf(@operator);
                    // get the value at the right of operator
                    var rightPart = formula.Substring(operatorIndex);
                    // if rightPart length < 2 that means only operator(like 5+) and do not have any meaning in this fomula.
                    return rightPart.Length < 2 ? string.Empty : rightPart;
                }
            }
            return result;
        }

        public static bool IsValidatedFormula(this string formula)
        {
            return formula.HasOperator() && !formula.EndWithOperator();
        }

        private static string GetTheValidatedFormula(string formula)
        {
            if (formula.EndWithOperator())
            {
                formula = formula.Substring(0, formula.Length - 1);
            }
            return formula;
        }
    }
}
