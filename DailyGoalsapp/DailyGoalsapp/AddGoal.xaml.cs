using System;
using System.Collections.Generic;
using System.IO;
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
using Path = System.IO.Path;

namespace DailyGoalsapp
{
    /// <summary>
    /// Interaction logic for AddGoal.xaml
    /// </summary>
    public partial class AddGoal : Window
    {
        public AddGoal()
        {
            InitializeComponent();
        }

        private void AddTheGoal_Click(object sender, RoutedEventArgs e)
        {
            // Get user input
            string goalName = GoalName.Text.Trim();
            string howLong = HowLong.Text.Trim();

            // Check if both fields are filled
            if (!string.IsNullOrEmpty(goalName) && !string.IsNullOrEmpty(howLong))
            {
                try
                {
                    // Get the file path
                    string projectFolder = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\"));
                    string filePath = System.IO.Path.Combine(projectFolder, "Goals", "MyGoals.txt");

                    // Write user input to the text file
                    using (StreamWriter sw = File.AppendText(filePath))
                    {
                        sw.WriteLine($"Goals: {goalName}");
                        sw.WriteLine($"Time: {howLong}");
                        sw.WriteLine(); // Add an empty line to separate entries
                    }

                    // Navigate back to the main page
                    MainPage mainPage = new MainPage();
                    mainPage.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please fill in both fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
