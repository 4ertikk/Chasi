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
        _timer = new Timer(1000); // ��������� ������ �������
        _timer.Elapsed += CheckAlarm;
        _timer.Start();
    }

    private void OnSetAlarmClicked(object sender, EventArgs e)
    {
        if (this.FindByName<StackLayout>("daysPanel") == null)
        {
            DisplayAlert("������", "������ ���� �� �������", "OK");
            return;
        }
        // ������� ��������� ���� �� �����������
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
            DisplayAlert("��������� ����������",
                         $"��������� ���������� �� {_selectedDay} � {timePicker.Time}",
                         "OK");
        }
        else
        {
            DisplayAlert("������", "�������� ���� ������", "OK");
        }
    }

    private void CheckAlarm(object sender, ElapsedEventArgs e)
    {
        if (!string.IsNullOrEmpty(_selectedDay))
        {
            DayOfWeek selectedDayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), _selectedDay);
            DateTime now = DateTime.Now;
            TimeSpan selectedTime = timePicker.Time;

            // ��������, ��������� �� ���� ������ � �����
            if (now.DayOfWeek == selectedDayOfWeek &&
                now.TimeOfDay.Hours == selectedTime.Hours &&
                now.TimeOfDay.Minutes == selectedTime.Minutes)
            {
                _timer.Stop(); // ���������� ������ ����� ������������
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await DisplayAlert("���������!", "����� ��������!", "OK");
                });
            }
        }
    }
}