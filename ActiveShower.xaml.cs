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

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for ActiveShower.xaml
    /// </summary>
    public partial class ActiveShower : Page
    {
        private int TempNum { get; set; } = 95;
        private double WaterFlow { get; set; } = 2;
        private Data userObjData;


        public ActiveShower()
        {
            InitializeComponent();
            Temp.Content = TempNum.ToString() + Temp.Content;
            System.Windows.Threading.DispatcherTimer liveTime = new System.Windows.Threading.DispatcherTimer();
            liveTime.Interval = TimeSpan.FromSeconds(0);
            liveTime.Tick += timer_Tick;
            liveTime.Start();
            WaterFlowVal.Value = WaterFlow;

        }

        void timer_Tick(object sender, EventArgs e)
        {
            Time.Content = DateTime.Now.ToString("t");
        }

        private void IncreaseTemp_Click(object sender, RoutedEventArgs e)
        {
            TempNum++;
            Temp.Content = TempNum.ToString() + "°F";

            //BPlus.Content = Grid.GetColumn(BPlus);

            //Panel.Visibility = Visibility.Visible;
            //Browser.Navigate(new Uri("http://stackoverflow.com"));
        }

        private void DecreaseTemp_Click(object sender, RoutedEventArgs e)
        {
            TempNum--;
            Temp.Content = TempNum.ToString() + "°F";
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            WaterFlow = (double)e.NewValue < 1 ? 0 : (double)e.NewValue;
            //Temp.Content=ScrollVal.HorizontalOffset;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var newB = new Button();

            AppViewer.Children.Add(newB);
            Grid.SetRow(newB, Grid.GetRow(BPlus));
            Grid.SetColumn(newB, Grid.GetColumn(BPlus));
            newB.Content = 1;


            if ((Grid.GetColumn(BPlus) + 1) < 3)
            {
                Grid.SetColumn(BPlus, Grid.GetColumn(BPlus) + 1);
            }
            else
            {

                if (Grid.GetRow(BPlus) == 0)
                {
                    Grid.SetRow(BPlus, 1);
                    if (Grid.GetColumn(BPlus) == 2)
                    {
                        Grid.SetColumn(BPlus, Grid.GetColumn(BPlus) - 2);
                    }
                }
                else
                {
                    Grid.SetRow(BPlus, 0);

                    var added = new ColumnDefinition();
                    added.Width = AppViewer.ColumnDefinitions[0].Width;

                    AppViewer.ColumnDefinitions.Add(added);
                    Grid.SetColumn(BPlus, Grid.GetColumn(BPlus) + 1);

                }
            }


            //hello.RowDefinitions[1]
            //hello.ColumnDefinitions.Add(new ColumnDefinition());
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            //objMainWindow.Main.Visibility = Visibility.Visible;
            ViewControl.NavigationService.GoBack();
        }
    }


}
