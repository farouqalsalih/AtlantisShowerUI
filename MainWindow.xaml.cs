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

        public MainWindow()
        {
            InitializeComponent();
            myStyle = FindResource("newUserS") as Style;
            this.DataContext = this;

        }

        private void NewUser_Click(object sender, RoutedEventArgs e)
        {

            normalGrid.Visibility = Visibility.Collapsed;
            AddUser.Visibility = Visibility.Visible;

        }

        //private ActiveShower objActiveShower;

        public void newButtonClick(object sender, RoutedEventArgs e)
        {
            Main.Visibility = Visibility.Collapsed;
            ViewControl.NavigationService.Content = new ActiveShower();
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
