using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;

namespace wpf_felkaru_rablo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int balance = 100;
        private int bet = 10;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void spinButton_Click(object sender, RoutedEventArgs e)
        {
            if (balance < bet)
            {
                balanceTextBox.Text = "Nincs elég pénz a téthez!";
                return;
            }
            balance -= bet;
            balanceTextBox.Text = $"{balance}";
            image1.Source = new BitmapImage(new Uri($"assets/cherry.png", UriKind.Relative));
        }
    }
}