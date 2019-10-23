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
            if (value.IsOperator())
            {
                //if the value is operator and there is a operator in CurrentFormula. Calculate the value of CurrentFormula
                if (CurrentFormula.HasOperator())
                {
                    var result = CurrentFormula.GetValue();
                    //set Formula
                    Formula = CurrentFormula;
                    //set CurrentFormula
                    CurrentFormula = $"{result.ToString()}{value}";
                }
                else CurrentFormula = $"{CurrentFormula}{value}";
            }
            else if (CurrentFormula == "0")
            {
                CurrentFormula = value;
            }
            else CurrentFormula = $"{CurrentFormula}{value}";
        }

        /// <summary>
        /// Clear the Formula
        /// </summary>
        private void ClearFormula()
        {
            CurrentFormula = "0";
            Formula = "";
        }

        /// <summary>
        /// Delete the Formula
        /// </summary>
        private void DeleteFormula()
        {
            if (CurrentFormula == "0") return;
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
            Formula = CurrentFormula;
            CurrentFormula = result.ToString();
        }
    }
}