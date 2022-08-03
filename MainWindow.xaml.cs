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

    public class buttonCollection
    {
        public string Name { get; set; }
        public Button buttonItem { get; set; }

        public buttonCollection(string name, Button item)
        {
            this.Name = name;
            this.buttonItem = item;
        }
    }

    public partial class MainWindow : Window
    {
        private bool turnScrollerOn = false;

        public string? tText { get; set; }

        public char? tTextC { get; set; }

        private Style? myStyle;

        private int Index = 0;

        private MainWindow obj;

        private bool grid;

        private int TempNum { get; set; } = 95;

        private double WaterFlow { get; set; } = 2;

        private Button addButton;

        private Data userObjData;

        Dictionary<Button, Data> users = new Dictionary<Button, Data>();

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

            try
            {
                buttonCollection[] list = new buttonCollection[]
                {
                    new buttonCollection("Netflix", Netflix),
                    new buttonCollection("YouTube", YouTube),
                    new buttonCollection("Instagram", Instagram),
                    new buttonCollection("Spotify", Spotify),
                    new buttonCollection("Facebook", Facebook),
                    new buttonCollection("Discord", Discord),
                    new buttonCollection("HBO Max", HBO_Max),
                    new buttonCollection("TikTok", TikTok),
                    new buttonCollection("Prime Video", Prime_Video),
                    new buttonCollection("Hulu", Hulu),
                    new buttonCollection("Apple Music", Apple_Music),
                    new buttonCollection("Reddit", Reddit),
                };

                ComboBoxApps.ItemsSource = list;
                ComboBoxApps.DisplayMemberPath = "Name";
                ComboBoxApps.SelectedValuePath = "buttonItem";
            }
            catch { }

        }

        //changes the value of time
        void timer_Tick(object sender, EventArgs e)
        {
            Time.Content = DateTime.Now.ToString("t");
        }

        //increase what's in temperature
        private void IncreaseTemp_Click(object sender, RoutedEventArgs e)
        {
            TempNum++;
            Temp.Content = TempNum.ToString() + "°F";
        }

        //decrease temp value
        private void DecreaseTemp_Click(object sender, RoutedEventArgs e)
        {
            TempNum--;
            Temp.Content = TempNum.ToString() + "°F";
            
        }

        //updates value of waterflow when slider is changed
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            WaterFlow = (double)e.NewValue < 1 ? 0 : (double)e.NewValue;
            //Temp.Content=ScrollVal.HorizontalOffset;
        }

        //adds app to active shower
        private void Button_ClickAdd(object sender, RoutedEventArgs e)
        {
            ScrollVal.Visibility = Visibility.Collapsed;
            AddAppGrid.Visibility = Visibility.Visible;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            addButton = (Button)ComboBoxApps.SelectedValue;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Button buttonF = (Button)sender;
            ScrollVal.Visibility = Visibility.Collapsed;
            Panel.Visibility = Visibility.Visible;
            browserView.Source = new Uri(buttonF.Content.ToString());
            //browserView.CoreWebView2.Navigate(buttonF.Content.ToString());
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            ScrollVal.Visibility = Visibility.Visible;
            AddAppGrid.Visibility = Visibility.Collapsed;
            AppIsAlreadyAdded.Text = "";
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (addButton.Visibility == Visibility.Visible)
            {
                AppIsAlreadyAdded.Text = "App Already Added";
            }
            else
            {
                Grid.SetColumn(addButton, Grid.GetColumn(BPlus));
                Grid.SetRow(addButton, Grid.GetRow(BPlus));
                checkActiveAdd(Settings_Page, true);
                checkActiveAdd(BPlus, false);
                addButton.Visibility = Visibility.Visible;
                ScrollVal.Visibility = Visibility.Visible;
                AddAppGrid.Visibility = Visibility.Collapsed;
                AppIsAlreadyAdded.Text = "";
            }
        }

        private void checkActiveAdd(Button newB, bool icon)
        {
            if ((Grid.GetColumn(newB) % 3) < 2)
            {
                Grid.SetColumn(newB, Grid.GetColumn(newB) + 1);
            }
            else if ((Grid.GetColumn(newB) % 3) == 2)
            {
                if (Grid.GetRow(newB) == 0)
                {
                    Grid.SetColumn(newB, Grid.GetColumn(newB) - 2);
                    Grid.SetRow(newB, 1);
                }
                else
                {
                    if (icon)
                    {
                        AppViewer.ColumnDefinitions.Add(new ColumnDefinition()
                        { Width = AppViewer.ColumnDefinitions[0].Width });
                        AppViewer.ColumnDefinitions.Add(new ColumnDefinition()
                        { Width = AppViewer.ColumnDefinitions[0].Width });
                        AppViewer.ColumnDefinitions.Add(new ColumnDefinition()
                        { Width = AppViewer.ColumnDefinitions[0].Width });
                    }

                    Grid.SetColumn(newB, Grid.GetColumn(newB) + 1);
                    Grid.SetRow(newB, 0);
                }
            }
        }

        //goes back to user screen
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Panel.Visibility = Visibility.Collapsed;
            Main.Visibility = Visibility.Visible;
            ActiveGrid.Visibility = Visibility.Collapsed;
        }

        //adds user to userScreen
        private void NewUser_Click(object sender, RoutedEventArgs e)
        {

            normalGrid.Visibility = Visibility.Collapsed;
            AddUser.Visibility = Visibility.Visible;

        }

        //when confirm is pressed for adding a user
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (textReturn.Text != "")
            {
                var userData = new Data();

                var newB = new Button
                {
                    Style = myStyle
                };

                string text = textReturn.Text.Replace(" ", "");

                try
                {

                    tText = textReturn.Text;

                    newB.Name = text;
                    userData.user_Name = tText;

                    users.Add(newB, userData);

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

        //when cancel is pressed for adding a user
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddUser.Visibility = Visibility.Collapsed;
            normalGrid.Visibility = Visibility.Visible;
            textReturn.Text = "";
            addBlock.Text = "Enter Name Here:";

        }

        //checks the grid when trying to add a user to userscreen
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

        //when the user icon is pressed, this happens (moves to active shower screen)
        public void newButtonClick(object sender, RoutedEventArgs e)
        {
            Button newButton = (Button)sender;
            Save.Visibility = Visibility.Visible;
            ActiveGrid.Visibility = Visibility.Visible;
            Main.Visibility = Visibility.Collapsed;
            users.TryGetValue(newButton, out userObjData);
            TempNum = userObjData.temperature;
            WaterFlow = userObjData.waterFlow;
            Temp.Content = userObjData.temperature + "°F";
            WaterFlowVal.Value = userObjData.waterFlow;
            AtlBlock.Text = userObjData.user_Name;

        }



        private void Guest_Click(object sender, RoutedEventArgs e)
        {
            ActiveGrid.Visibility = Visibility.Visible;
            Main.Visibility = Visibility.Collapsed;
            TempNum = 95;
            WaterFlow = 2;
            Temp.Content = TempNum + "°F";
            WaterFlowVal.Value = WaterFlow;
            Save.Visibility = Visibility.Collapsed;
            AtlBlock.Text = "Guest";
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            grid = false;
            EditingGrid.Visibility = Visibility.Visible;
            Main.Visibility = Visibility.Collapsed;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            browserView.GoBack();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Panel.Visibility = Visibility.Collapsed;
            ScrollVal.Visibility = Visibility.Visible;
            browserView.Source = new Uri("https://blank.org/");
        }

        private void Settings_Page_Click(object sender, RoutedEventArgs e)
        {
            grid = true;
            EditingGrid.Visibility = Visibility.Visible;
            ActiveGrid.Visibility = Visibility.Collapsed;
        }

        private void BackButton_Copy_OnClick(object sender, RoutedEventArgs e)
        {
            if (grid)
            {
                EditingGrid.Visibility = Visibility.Collapsed;
                ActiveGrid.Visibility = Visibility.Visible;
            }
            else
            {
                EditingGrid.Visibility = Visibility.Collapsed;
                Main.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            userObjData.temperature = TempNum;
            userObjData.waterFlow = WaterFlowVal.Value;
        }
    }
}

