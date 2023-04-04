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
using System.Windows.Threading;
using System.Globalization;
using System.ComponentModel;

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

            public TimeSpan TimeLimit { get; set; }
        }
        private List<bool> checkmarkStates; 

        private DispatcherTimer timer;
        public MainPage()
        {
            InitializeComponent();

            this.Closing += Window_Closing; 

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);

            // Subscribe to the Tick event to update the clock
            timer.Tick += Timer_Tick;

            // Start the timer
            timer.Start();


            // Load goals from the file and display them
            CheckFileAndSetVisibility();
            LoadCheckmarkStates();
        }

        private void UpdateComboBoxGoals()
        {
            GoalsToFocusMode.ItemsSource = AllTheGoals.ItemsSource;
            GoalsToFocusMode.DisplayMemberPath = "GoalText";
            GoalsToFocusMode.SelectedIndex = 0;
        }

        private void LoadCheckmarkStates()
        {
            string projectFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\"));
            string filePath = Path.Combine(projectFolder, "Goals", "CheckmarkStates.txt");

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                if (lines.Length > 0 && DateTime.TryParse(lines[0], out DateTime savedDate) && savedDate.Date == DateTime.Now.Date)
                {
                    List<GoalItem> goalItems = AllTheGoals.ItemsSource != null
                            ? (AllTheGoals.ItemsSource as IEnumerable<GoalItem>).ToList()
                            : new List<GoalItem>();
                    if (goalItems != null)
                    {
                        goalItems = goalItems.ToList();
                        bool[] checkmarkStates = lines.Skip(1).Select(line => bool.Parse(line)).ToArray();

                        for (int i = 0; i < Math.Min(checkmarkStates.Length, goalItems.Count); i++)
                        {
                            goalItems[i].IsChecked = checkmarkStates[i];
                        }
                    }
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Get the current date and time
            DateTime currentTime = DateTime.Now;

            // Update the text of the clock label
            CurrentTime.Text = currentTime.ToString("hh:mm tt");

            // Check if we need to reset the checkmarks
            string projectFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\"));
            string filePath = Path.Combine(projectFolder, "Goals", "CheckmarkStates.txt");

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                DateTime savedDate = DateTime.ParseExact(lines[0], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime currentDate = DateTime.Now.Date;

                if (savedDate != currentDate)
                {
                    for (int i = 0; i < checkmarkStates.Count; i++)
                    {
                        checkmarkStates[i] = false;
                        ((GoalItem)AllTheGoals.Items[i]).IsChecked = false;
                    }
                }
            }
        }
        private void UpdateVisibility()
        {
            bool allChecked = AllTheGoals.Items.Cast<GoalItem>().All(item => item.IsChecked);

            if (allChecked)
            {
                GoalsNow.Visibility = Visibility.Hidden;
                AllTheGoals.Visibility = Visibility.Hidden;
                AddAGoal.Visibility = Visibility.Hidden;
                AddnewGoal.Visibility = Visibility.Hidden;
                DeleteGoal.Visibility = Visibility.Hidden;

                MessageNoGoal.Text = "Congrats for being productive today! \t Clean up for next day? :)";
                MessageNoGoal.VerticalAlignment = VerticalAlignment.Center;
                MessageNoGoal.Visibility = Visibility.Visible;
                restartforToday.Visibility = Visibility.Visible;
            }
            else
            {
                GoalsNow.Visibility = Visibility.Visible;
                AllTheGoals.Visibility = Visibility.Visible;
                AddAGoal.Visibility = Visibility.Visible;
                AddnewGoal.Visibility = Visibility.Visible;
                DeleteGoal.Visibility = Visibility.Visible;
                restartforToday.Visibility = Visibility.Hidden;
                MessageNoGoal.Visibility = Visibility.Hidden;
                restartforToday.Visibility = Visibility.Hidden;
            }
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
                    UpdateVisibility();
                    UpdateComboBoxGoals();
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

            LoadCheckmarkStates(); // Move this call here
        }



        private void Window_Closing(object sender, CancelEventArgs e)
        {
            string projectFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\"));
            string filePath = Path.Combine(projectFolder, "Goals", "CheckmarkStates.txt");

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(DateTime.Now.Date.ToString("yyyy-MM-dd"));

            foreach (var item in AllTheGoals.Items)
            {
                bool isChecked = ((GoalItem)item).IsChecked;
                sb.AppendLine(isChecked.ToString());
            }

            File.WriteAllText(filePath, sb.ToString());
        }

        private void CheckBox_Changed(object sender, RoutedEventArgs e)
        {
            UpdateVisibility();
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

        private void GoToFocusMode_Click(object sender, RoutedEventArgs e)
        {
            if (GoalsToFocusMode.SelectedItem is GoalItem selectedGoal)
            {
                FocusMode focusModeWindow = new FocusMode(selectedGoal);
                focusModeWindow.Show();
                this.Close();
            }
        }

        private void restartforToday_Click(object sender, RoutedEventArgs e)
        {
            string projectFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\"));
            string goalsFilePath = Path.Combine(projectFolder, "Goals", "MyGoals.txt");
            string completedGoalsFilePath = Path.Combine(projectFolder, "Goals", "CompletedGoals.txt");

            // Read the current goals
            string[] lines = File.ReadAllLines(goalsFilePath);

            // Prepare the completed goals data
            StringBuilder completedGoalsData = new StringBuilder();
            completedGoalsData.AppendLine(DateTime.Now.ToString("yyyy-MM-dd"));
            for (int i = 0; i < lines.Length; i += 3)
            {
                if (i + 1 < lines.Length)
                {
                    string goal = lines[i].Substring("Goals: ".Length).Trim();
                    string time = lines[i + 1].Substring("Time: ".Length).Trim();
                    completedGoalsData.AppendLine($"{goal} ({time})");
                }
            }

            // Append the completed goals for the day to the CompletedGoals.txt file
            File.AppendAllText(completedGoalsFilePath, completedGoalsData.ToString());

            // Clear the current goals
            File.WriteAllText(goalsFilePath, string.Empty);

            // Refresh the visibility of UI elements
            CheckFileAndSetVisibility();

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
