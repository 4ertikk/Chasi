using System.Timers;
using Timer = System.Timers.Timer;

namespace Chasi;

public partial class NewPage1 : ContentPage
{
    private Timer _timer;
    private TimeSpan _elapsedTime;
    private bool _isRunning;
    private List<string> _laps;
    public NewPage1()
	{
		InitializeComponent();
        _timer = new Timer(1000);
        _timer.Elapsed += OnTimedEvent;
        _laps = new List<string>();
    }

    private void OnStartClicked(object sender, EventArgs e)
    {
        if (!_isRunning)
        {
            _timer.Start();
            _isRunning = true;
            StartButton.IsEnabled = false;
            StopButton.IsEnabled = true;
            ResetButton.IsEnabled = false;
            LapButton.IsEnabled = true;
        }   
    }

    private void OnStopClicked(object sender, EventArgs e)
    {
        if (_isRunning)
        {
            _timer.Stop();
            _isRunning = false;
            StartButton.IsEnabled = true;
            StopButton.IsEnabled = false;
            ResetButton.IsEnabled = true;
            LapButton.IsEnabled = false;
        }
    }

    private void OnResetClicked(object sender, EventArgs e)
    {
        _timer.Stop();
        _isRunning = false;
        _elapsedTime = TimeSpan.Zero;
        TimerLabel.Text = "00:00:00";
        LapsListView.ItemsSource = null;
        _laps.Clear();
        StartButton.IsEnabled = true;
        StopButton.IsEnabled = false;
        ResetButton.IsEnabled = false;
        LapButton.IsEnabled = false;
    }

    private void OnLapClicked(object sender, EventArgs e)
    {
        if (_isRunning)
        {
            _laps.Add(TimerLabel.Text);
            LapsListView.ItemsSource = null; // Обновляем источник данных
            LapsListView.ItemsSource = _laps; // Позволяем списку отобразить новые данные
        }
    }

    [Obsolete]
    private void OnTimedEvent(object sender, ElapsedEventArgs e)
    {
        _elapsedTime = _elapsedTime.Add(TimeSpan.FromSeconds(1));
        Device.BeginInvokeOnMainThread(() =>
        {
            TimerLabel.Text = _elapsedTime.ToString(@"hh\:mm\:ss");
        });
    }
}