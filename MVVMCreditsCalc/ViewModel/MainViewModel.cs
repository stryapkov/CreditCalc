using GalaSoft.MvvmLight;
using System.ComponentModel;
using GalaSoft.MvvmLight.CommandWpf;

namespace MVVMCreditsCalc.ViewModel
{
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {
        Calculate calc = new Calculate();
        public RelayCommand CalculateCrediit { get; set; }
        public RelayCommand CalculateDel { get; set; }
        public RelayCommand CalculateSave { get; set; }
        public RelayCommand CalculatePrint { get; set; }
        public RelayCommand CalcOpenCsv { get; set; }
        public MainViewModel()
        {
            CalculatePrint=new RelayCommand(calc.PrintPages);
            CalculateCrediit=new RelayCommand(calc.CalculationClick);
            CalculateDel=new RelayCommand(calc.ClearClic);
            CalculateSave=new RelayCommand(calc.Save);
            CalcOpenCsv=new RelayCommand(calc.CalculateOpenCsv);
        }

        public Calculate Calc
        {
           get { return calc; }
            set
            {
                Set(ref calc, value);
                RaisePropertyChanged(nameof(Calc));
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

    }
}