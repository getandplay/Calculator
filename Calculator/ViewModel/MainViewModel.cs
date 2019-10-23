using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;

namespace Calculator.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand<string> EditFomulaCommand { get; private set; }

        public RelayCommand ClearCommand { get; private set; }

        public RelayCommand DeleteCommand { get; private set; }

        public RelayCommand CalculateCommand { get; private set; }

        public string Formula
        {
            get => _formula;
            set
            {
                _formula = value;
                RaisePropertyChanged("Formula");
            }
        }
        private string _formula;

        public string CurrentFormula
        {
            get => _currentFormula;
            set
            {
                _currentFormula = value;
                RaisePropertyChanged("CurrentFormula");
            }
        }

        private string _currentFormula = "0";

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            EditFomulaCommand = new RelayCommand<string>(EditFormula);

            ClearCommand = new RelayCommand(ClearFormula);

            DeleteCommand = new RelayCommand(DeleteFormula);

            CalculateCommand = new RelayCommand(Calculate);
        }

        /// <summary>
        /// Edit the currentFormula
        /// </summary>
        /// <param name="value"></param>
        private void EditFormula(string value)
        {
            const string dot = ".";
            if (value.IsOperator())
            {
                EditFormulaWhenOperatorInput(value);
            }
            else if (_isCalculatedWithoutOperator)
            {
                EditFormulaWhenCalculatedWithOutOperator(value);
            }
            else if (CurrentFormula == "0" && value != dot)
            {
                CurrentFormula = value;
            }
            // Add a '.' to formula should be careful and checked
            else if (value != dot || CurrentFormula.CanAddDot())
            {
                if (value == dot && CurrentFormula.EndWithOperator())
                {
                    CurrentFormula = $"{CurrentFormula}0{value}";
                }
                else CurrentFormula = $"{CurrentFormula}{value}";
            }
        }

        private void EditFormulaWhenOperatorInput(string value)
        {
            //if the value is operator and there is a operator in CurrentFormula. Calculate the value of CurrentFormula
            if (CurrentFormula.HasOperator())
            {
                var result = CurrentFormula.GetValue();
                //set Formula
                Formula = GetFormula(Formula, CurrentFormula);
                //set CurrentFormula
                CurrentFormula = $"{result.ToString()}{value}";
            }
            else CurrentFormula = $"{CurrentFormula}{value}";
            //set the flag false
            _isCalculatedWithoutOperator = false;
        }

        private void EditFormulaWhenCalculatedWithOutOperator(string value)
        {
            if (value == ".") CurrentFormula = $"0{value}";
            else CurrentFormula = value;
            Formula = string.Empty;
            _isCalculatedWithoutOperator = false;
        }

        /// <summary>
        /// Get correct formula values
        /// </summary>
        /// <param name="formula"></param>
        /// <param name="currentFormula"></param>
        /// <returns></returns>
        private string GetFormula(string formula, string currentFormula)
        {
            //if the formula is empty, return the current formula as the formula record
            if (string.IsNullOrEmpty(formula))
            {
                return currentFormula.EndWithOperator() ? currentFormula.Substring(0, currentFormula.Length - 1) : currentFormula;
            }
            var rightPart = currentFormula.GetRightPartOfFormula();
            return $"{formula}{rightPart}";
        }

        /// <summary>
        /// Clear the Formula
        /// </summary>
        private void ClearFormula()
        {
            CurrentFormula = "0";
            Formula = string.Empty;
        }

        /// <summary>
        /// Delete the Formula
        /// </summary>
        private void DeleteFormula()
        {
            if (CurrentFormula == "0" || _isCalculatedWithoutOperator) return;
            if (CurrentFormula.Length == 1)
            {
                CurrentFormula = "0";
                return;
            }
            else
            {
                CurrentFormula = CurrentFormula.Substring(0, CurrentFormula.Length - 1);
            }
        }

        /// <summary>
        /// calculate the value of current formula without next operator
        /// </summary>
        private void Calculate()
        {
            var result = CurrentFormula.GetValue();
            Formula = GetFormula(Formula, CurrentFormula);
            CurrentFormula = result.ToString();
            _isCalculatedWithoutOperator = true;
        }

        private bool _isCalculatedWithoutOperator = false;
    }
}