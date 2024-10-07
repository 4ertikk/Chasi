namespace Chasi;

public partial class NewPage2 : ContentPage
{
    private CancellationTokenSource _cancellationTokenSource;
    private bool _isRunning = false;
    private TimeSpan _remainingTime;
    public NewPage2()
	{
		InitializeComponent();
        TimerLabel.Text = "00:00:00"; // Инициализация метки таймера
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (_isRunning)
            return;

        _isRunning = true;
        TimerLabel.Text = "00:00:00"; // Сброс метки таймера

        int hours = int.TryParse(HoursEntry.Text, out hours) ? hours : 0;
        int minutes = int.TryParse(MinutesEntry.Text, out minutes) ? minutes : 0;
        int seconds = int.TryParse(SecondsEntry.Text, out seconds) ? seconds : 0;

        _remainingTime = new TimeSpan(hours, minutes, seconds);
        TimerLabel.Text = _remainingTime.ToString();
        _cancellationTokenSource = new CancellationTokenSource();

        while (_remainingTime.TotalSeconds > 0 && !_cancellationTokenSource.Token.IsCancellationRequested)
        {
            await Task.Delay(1000);
            _remainingTime = _remainingTime.Add(TimeSpan.FromSeconds(-1));
            TimerLabel.Text = _remainingTime.ToString(@"hh\:mm\:ss");
        }

        _isRunning = false;
        if (!_cancellationTokenSource.Token.IsCancellationRequested)
        {
            await DisplayAlert("Таймер", "Время вышло!", "OK");
        }
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        if (_isRunning)
        {
            _cancellationTokenSource.Cancel();
            _isRunning = false;
        }
    }

    private void Button_Clicked_2(object sender, EventArgs e)
    {
        _cancellationTokenSource?.Cancel();
        _remainingTime = TimeSpan.Zero; // Завершение таймера
        TimerLabel.Text = "00:00:00"; // Обновление метки таймера
        _isRunning = false;
    }
}