using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
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

using System.Windows.Threading;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///


    public partial class MainWindow : Window
    {
        private bool turnScrollerOn = false;

        public string? tText { get; set; }

        public char? tTextC { get; set; }

        private Style? myStyle;

        private int Index = 0;

        private MainWindow obj;

        IDictionary<int, Data> users = new Dictionary<int, Data>();

        private int TempNum { get; set; } = 95;
        private double WaterFlow { get; set; } = 2;
        private Data userObjData;


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

        private void Button_ClickAdd(object sender, RoutedEventArgs e)
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
            Main.Visibility = Visibility.Visible;
            ActiveGrid.Visibility = Visibility.Collapsed;
        }

        public MainWindow()
        {
            InitializeComponent();
            myStyle = FindResource("newUserS") as Style;
            this.DataContext = this;
            Temp.Content = TempNum.ToString() + Temp.Content;
            System.Windows.Threading.DispatcherTimer liveTime = new System.Windows.Threading.DispatcherTimer();
            liveTime.Interval = TimeSpan.FromSeconds(0);
            liveTime.Tick += timer_Tick;
            liveTime.Start();
            WaterFlowVal.Value = WaterFlow;

        }

        private void NewUser_Click(object sender, RoutedEventArgs e)
        {

            normalGrid.Visibility = Visibility.Collapsed;
            AddUser.Visibility = Visibility.Visible;

        }

        //private ActiveShower objActiveShower;

        public void newButtonClick(object sender, RoutedEventArgs e)
        {
            ActiveGrid.Visibility = Visibility.Visible;
            Main.Visibility = Visibility.Collapsed;
        }

        public void checkGrid(Button oButton, bool icon)
        {
            if ((Grid.GetColumn(oButton) % 5) < 3)
            {
                Grid.SetColumn(oButton, Grid.GetColumn(oButton) + 1);
            }
            else if ((Grid.GetColumn(oButton) % 5) == 3)
            {
                if (Grid.GetRow(oButton) == 0)
                {
                    Grid.SetColumn(oButton, Grid.GetColumn(oButton) - 2);
                    Grid.SetRow(oButton, 1);
                }
                else
                {
                    if (icon)
                    {
                        UserProfiles.ColumnDefinitions.Add(new ColumnDefinition()
                        { Width = UserProfiles.ColumnDefinitions[0].Width });
                        UserProfiles.ColumnDefinitions.Add(new ColumnDefinition()
                        { Width = UserProfiles.ColumnDefinitions[1].Width });
                        UserProfiles.ColumnDefinitions.Add(new ColumnDefinition()
                        { Width = UserProfiles.ColumnDefinitions[1].Width });
                        UserProfiles.ColumnDefinitions.Add(new ColumnDefinition()
                        { Width = UserProfiles.ColumnDefinitions[1].Width });
                        UserProfiles.ColumnDefinitions.Add(new ColumnDefinition()
                        { Width = UserProfiles.ColumnDefinitions[0].Width });

                        if (!turnScrollerOn)
                        {
                            UserScroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                        }
                    }

                    Grid.SetColumn(oButton, Grid.GetColumn(oButton) + 3);
                    Grid.SetRow(oButton, 0);
                }
            }
        }

        private void Guest_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {

        }

        //private void UserScroll_OnScrollChanged(object sender, ScrollChangedEventArgs e)
        //{
        //    ScrollViewer objScrollViewer = (ScrollViewer)sender;

        //    throw new NotImplementedException();
        //}


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (textReturn.Text != "")
            {
                var userData = new Data();

                var newB = new Button
                {
                    Style = myStyle
                };

                try
                {

                    tText = textReturn.Text;

                    newB.Name = tText;
                    userData.user_Name = tText;

                    users.Add(Index, userData);
                    ++Index;

                    tTextC = tText[0];

                }
                catch (Exception ex)
                {
                }

                newB.Margin = new Thickness(0, 0, 0, 45);

                newB.Click += new RoutedEventHandler(newButtonClick);

                UserProfiles.Children.Add(newB);
                Grid.SetRow(newB, Grid.GetRow(NewUser));
                Grid.SetColumn(newB, Grid.GetColumn(NewUser));


                checkGrid(Settings, true);
                checkGrid(Guest, false);
                checkGrid(NewUser, false);

                AddUser.Visibility = Visibility.Collapsed;
                normalGrid.Visibility = Visibility.Visible;
                textReturn.Text = "";
                addBlock.Text = "Enter Name Here:";

            }
            else
            {
                addBlock.Text = "At least One Character:";
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddUser.Visibility = Visibility.Collapsed;
            normalGrid.Visibility = Visibility.Visible;
            textReturn.Text = "";
            addBlock.Text = "Enter Name Here:";

        }


    }
}
