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
using System.Windows.Threading;
namespace DailyGoalsapp
{
    /// <summary>
    /// Interaction logic for FocusMode.xaml
    /// </summary>
    using System.Windows.Threading; // Add this line at the top

    public partial class FocusMode : Window
    {
        private readonly DispatcherTimer _timer;
        private readonly DateTime _startTime;
        private readonly TimeSpan _timeLimit;

        public FocusMode(MainPage.GoalItem goalItem)
        {
            InitializeComponent();

            _startTime = DateTime.Now;
            _timeLimit = goalItem.TimeLimit;
            CurrentTime.Text = _startTime.ToString("hh:mm tt");
            TimeLeft.Text = _timeLimit.ToString(@"hh\:mm\:ss");
            Goal_to_complete.Text = goalItem.GoalText;

            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsedTime = DateTime.Now - _startTime;

            if (elapsedTime >= _timeLimit)
            {
                _timer.Stop();
                TimeFinished.Visibility = Visibility.Visible;
                TimeLeft.Text = "00:00:00";
            }
            else
            {
                TimeSpan timeRemaining = _timeLimit - elapsedTime;
                TimeLeft.Text = timeRemaining.ToString(@"hh\:mm\:ss");
            }
        }
    }
}
