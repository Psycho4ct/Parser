using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace CompilerLab
{
    /// <summary>
    /// Interaction logic for CloseFileWindow.xaml
    /// </summary>
    public partial class CloseFileWindow : Window
    {
        public CloseFileWindow()
        {
            InitializeComponent();
        }
        private void SaveButton(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;  
            this.Close();
        }

        public bool IsCanceled = false;
        public bool IsClosed = false;
        private void CancelButton(object sender, RoutedEventArgs e)
        {
            IsCanceled = true;
            this.Close();       
        }
        private void CloseFileWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            IsClosed = true;
        }
    }
}
