using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace SFKSubEditor
{
    /// <summary>
    /// Interaction logic for ChoiceEncodingWindow.xaml
    /// </summary>
    public partial class ChoiceEncodingWindow : Window
    {
        private String fileName;
        public ChoiceEncodingWindow(String fileName)
        {
            InitializeComponent();
            this.fileName = fileName;
            // sometimes the name of the czech subtitles end with cz. and in the case the encoding should be Eastern Europe
            if (fileName.Contains("cz."))
                encodingChooser.SelectedIndex = 0;
            else
                encodingChooser.SelectedIndex = 1;
            displayFileInEncoding();
            encodingChooser.Focus();
        }
        /// <summary>
        /// When the selection is changed, it reads again the text from the file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void encodingChooser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            displayFileInEncoding();
        }
        /// <summary>
        /// Read some lines from the file in the choosen encoding and display them.
        /// </summary>
        private void displayFileInEncoding()
        {
            Encoding fileEncoding = getActualEncoding();
            using (StreamReader sr = new StreamReader(fileName, fileEncoding))
            {
                textFromFile.Text="";
                String input;
                int i = 0;
                while ((input = sr.ReadLine()) != null && i < 20)
                {
                    textFromFile.Text += input + "\n";
                    i++;
                }
            }
        }
        /// <summary>
        /// Gets the current item in the listBox and makes an encoding from it.
        /// </summary>
        /// <returns>Encoding according to select item in the listBox.</returns>
        public Encoding getActualEncoding()
        {
            Encoding fileEncoding = Encoding.Default;
            switch (encodingChooser.SelectedIndex)
            {
                case 0: // Eastern Europe
                    fileEncoding = Encoding.GetEncoding(1250);
                    break;
                case 1: // Western Europe
                    fileEncoding = Encoding.GetEncoding(1252);
                    break;
                case 2:
                    fileEncoding = Encoding.UTF8;
                    break;
                case 3:
                    fileEncoding = Encoding.Unicode;
                    break;
            }
            return (fileEncoding);
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (encodingChooser.SelectedItem == null)
            {
                MessageBox.Show("You have to choose an encoding.","Error",MessageBoxButton.OK, MessageBoxImage.Error);
            } else
                this.DialogResult = true;
        }

        private void encodingChooser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.DialogResult = true;
            }
        }

        private void encodingChooser_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
