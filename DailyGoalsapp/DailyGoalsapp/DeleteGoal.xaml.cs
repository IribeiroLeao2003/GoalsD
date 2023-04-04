using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DailyGoalsapp
{
    public partial class DeleteGoal : Window
    {
        private List<GoalItem> goalItems;

        public DeleteGoal()
        {
            InitializeComponent();

            goalItems = new List<GoalItem>();

            LoadGoals();
        }

        private void LoadGoals()
        {
            string projectFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\"));
            string filePath = Path.Combine(projectFolder, "Goals", "MyGoals.txt");

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                for (int i = 0; i < lines.Length; i += 3)
                {
                    if (i + 1 < lines.Length)
                    {
                        string goal = lines[i].Substring("Goals: ".Length).Trim();
                        string time = lines[i + 1].Substring("Time: ".Length).Trim();

                        GoalItem goalItem = new GoalItem
                        {
                            GoalText = $"- {goal} ({time})"
                        };

                        goalItems.Add(goalItem);
                    }
                }

                GoalListBox.ItemsSource = goalItems;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            GoalItem goalItem = (GoalItem)button.DataContext;

            goalItems.Remove(goalItem);
            GoalListBox.ItemsSource = null;
            GoalListBox.ItemsSource = goalItems;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string projectFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\"));
            string filePath = Path.Combine(projectFolder, "Goals", "MyGoals.txt");

            StringBuilder sb = new StringBuilder();
            foreach (GoalItem goalItem in goalItems)
            {
                string goal = goalItem.GoalText.Substring(2, goalItem.GoalText.LastIndexOf("(") - 3);
                string time = goalItem.GoalText.Substring(goalItem.GoalText.LastIndexOf("(") + 1, goalItem.GoalText.LastIndexOf(")") - goalItem.GoalText.LastIndexOf("(") - 1);

                sb.AppendLine($"Goals: {goal}");
                sb.AppendLine($"Time: {time}");
                sb.AppendLine();
            }

            File.WriteAllText(filePath, sb.ToString());

            MessageBox.Show("Goals have been updated.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            
            
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }

    public class GoalItem
    {
        public string GoalText { get; set; }
        public bool IsChecked { get; set; }
    }
}
