using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;

namespace Calculator.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand TheCommand { get; private set; } 

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

        public string CurrentFormula { get; set; } = "1290";

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            TheCommand = new RelayCommand(ActionTest);
        }

        private void ActionTest()
        {
            Formula = "hello world";
        }
    }
}