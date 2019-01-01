using Neural_Ant_Colony_Classification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace SimpleNACC
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();            
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(delegate ()
            {
                ClassificationNAC nac = new ClassificationNAC(100, 1, 0.1);

                nac.InitializeColony(5, 10, 5, 5);                

                for (int i = 0; i < 500; i++)
                {
                    nac.SetInputOutput(new List<int> { 10, 10, 0, 20, 20 }, new List<int> { -1, 1, 1, 1, 1 });
                    for (int j = 0; j < 10; j++)
                    {                     
                        nac.RunIteration();
                    }
                    nac.SetInputOutput(new List<int> { 10, 30, 0, 0, 1 }, new List<int> { 1, 1, 1, 1, -1 });                  
                    for (int j = 0; j < 10; j++)
                    {                       
                        nac.RunIteration();
                    }                   
                }            
            }));

            thread.Start();
        }
    }
}
