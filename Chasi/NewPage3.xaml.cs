using Microsoft.Maui.ApplicationModel;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Chasi;

public partial class NewPage3 : ContentPage
{
    private Timer _timer;
    private string _selectedDay;
    public NewPage3()
	{
		InitializeComponent();
        _timer = new Timer(1000); // Проверяем каждую секунду
        _timer.Elapsed += CheckAlarm;
        _timer.Start();
    }

    private void OnSetAlarmClicked(object sender, EventArgs e)
    {
        if (this.FindByName<StackLayout>("daysPanel") == null)
        {
            DisplayAlert("Ошибка", "Панель дней не найдена", "OK");
            return;
        }
        // Находим выбранный день из радиокнопок
        foreach (var child in this.FindByName<StackLayout>("daysPanel").Children)
        {
            if (child is RadioButton radioButton && radioButton.IsChecked)
            {
                _selectedDay = radioButton.Value.ToString();
                break;
            }
        }

        if (!string.IsNullOrEmpty(_selectedDay))
        {
            DisplayAlert("Будильник установлен",
                         $"Будильник установлен на {_selectedDay} в {timePicker.Time}",
                         "OK");
        }
        else
        {
            DisplayAlert("Ошибка", "Выберите день недели", "OK");
        }
    }

    private void CheckAlarm(object sender, ElapsedEventArgs e)
    {
        if (!string.IsNullOrEmpty(_selectedDay))
        {
            DayOfWeek selectedDayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), _selectedDay);
            DateTime now = DateTime.Now;
            TimeSpan selectedTime = timePicker.Time;

            // Проверка, совпадают ли день недели и время
            if (now.DayOfWeek == selectedDayOfWeek &&
                now.TimeOfDay.Hours == selectedTime.Hours &&
                now.TimeOfDay.Minutes == selectedTime.Minutes)
            {
                _timer.Stop(); // Остановить таймер после срабатывания
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await DisplayAlert("Будильник!", "Время вставать!", "OK");
                });
            }
        }
    }
}