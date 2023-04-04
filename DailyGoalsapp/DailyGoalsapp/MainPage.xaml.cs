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
using System.Windows.Shapes;
using System.IO;
using Path = System.IO.Path;

namespace DailyGoalsapp
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Window
    {
        public class GoalItem
        {
            public string GoalText { get; set; }
            public bool IsChecked { get; set; }
        }

        public MainPage()
        {
            InitializeComponent();
            CheckFileAndSetVisibility();
        }

        private void CheckFileAndSetVisibility()
        {
            string projectFolder = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\"));
            string filePath = System.IO.Path.Combine(projectFolder, "Goals", "MyGoals.txt");

            string directoryPath = System.IO.Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (File.Exists(filePath))
            {
                string content = File.ReadAllText(filePath);
                if (string.IsNullOrWhiteSpace(content))
                {
                    GoalsNow.Visibility = Visibility.Hidden;
                    AddnewGoal.Visibility = Visibility.Hidden;
                    DeleteGoal.Visibility = Visibility.Hidden;
                    AllTheGoals.Visibility = Visibility.Hidden;
                    MessageNoGoal.Visibility = Visibility.Visible;
                    AddAGoal.Visibility = Visibility.Visible;
                }
                else
                {
                    GoalsNow.Visibility = Visibility.Visible;
                    AddnewGoal.Visibility = Visibility.Visible;
                    DeleteGoal.Visibility = Visibility.Visible;
                    AllTheGoals.Visibility = Visibility.Visible;
                    MessageNoGoal.Visibility = Visibility.Hidden;
                    AddAGoal.Visibility = Visibility.Hidden;

                    // Load goals from the file and display them
                    string[] lines = File.ReadAllLines(filePath);
                    List<GoalItem> goalItems = new List<GoalItem>();
                    for (int i = 0; i < lines.Length; i += 3)
                    {
                        if (i + 1 < lines.Length)
                        {
                            string goal = lines[i].Substring("Goals: ".Length).Trim();
                            string time = lines[i + 1].Substring("Time: ".Length).Trim();
                            goalItems.Add(new GoalItem { GoalText = $"- {goal} ({time})", IsChecked = false });
                        }
                    }
                    AllTheGoals.ItemsSource = goalItems;
                }
            }
            else
            {
                // Create the file if it doesn't exist.
                File.WriteAllText(filePath, string.Empty);
                GoalsNow.Visibility = Visibility.Hidden;
                AddnewGoal.Visibility = Visibility.Hidden;
                DeleteGoal.Visibility = Visibility.Hidden;
                AllTheGoals.Visibility = Visibility.Hidden;
                MessageNoGoal.Visibility = Visibility.Visible;
                AddAGoal.Visibility = Visibility.Visible;
            }
        }



        private void AddAGoal_Click(object sender, RoutedEventArgs e)
        {
            AddGoal mainWindow = new AddGoal();
            mainWindow.Show();
            this.Close();
        }

        private void DeleteGoal_Click(object sender, RoutedEventArgs e)
        {
            DeleteGoal mainWindow = new DeleteGoal();
            mainWindow.Show();
            this.Close();
        }

        private void AddnewGoal_Click(object sender, RoutedEventArgs e)
        {
            AddGoal mainWindow = new AddGoal();
            mainWindow.Show();
            this.Close();
        }
    }
}
