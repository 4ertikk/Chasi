namespace Chasi
{
    public partial class MainPage : ContentPage
    {
        [Obsolete]
        public MainPage()
        {
            InitializeComponent();
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                TimeLabel.Text = DateTime.Now.ToString("HH:mm:ss");
                DateLabel.Text = DateTime.Now.ToString("dd MMMM yyyy");
                return true; // возвращает true для непрерывного таймера
            });
        }


    }

}
