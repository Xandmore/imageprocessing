using Microsoft.Win32;
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

namespace Binarization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string ImageFilter { get; } = Utils.GetImageFilter();
        private BinaryzatorBase Binaryzator { get; set; } = new BinaryzatorType1();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = ImageFilter;

            if (dialog.ShowDialog() == true)
            {
                FilePathTextBox.Text = dialog.FileName;
            }

            Binaryzator.Image = new BitmapImage(new Uri(dialog.FileName));
            Image.Source = Binaryzator.Image;
        }        
    }
}
