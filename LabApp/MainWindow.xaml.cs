using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace LabApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private XMLDataHandler myXMLDataHandler = new XMLDataHandler();
        private ErrorCode myErrorCode = new ErrorCode(); 


        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void btSaveErrorcode_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("The New command was invoked");
        }


        public string _errorCode;

        public string SearchErrorCode
        {
            get { return _errorCode; }
            set { _errorCode = value; ValidatErrorCode(); OnPropertyChanged("SearchErrorCode"); }
        }

        private void ValidatErrorCode()
        {

            if (_errorCode.Length == 2 )
            {
                if(_errorCode.Substring(1, 1) != ":")
                {
                    _errorCode = _errorCode.Substring(0, 1) + ":" + _errorCode.Substring(1, 1);
                }
            }
            else if (_errorCode.Length == 5)
            {
                GetErrorInformation();
            }
            else if (_errorCode.Length > 5)
            {
                _errorCode = _errorCode.Substring(0, 5);

            }
        }

        private void GetErrorInformation()
        {
            myErrorCode.Code = _errorCode;

            myXMLDataHandler.XMLWriteErrorCode(myErrorCode.Code);

            myXMLDataHandler.XmlReaderErrorCode(myErrorCode.Code);
        }

        public MainWindow()
        {
            InitializeComponent();
           

            this.DataContext = this;
        }
    }
}
