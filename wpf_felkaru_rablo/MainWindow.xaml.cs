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
using System.Security.Cryptography;

namespace wpf_felkaru_rablo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int balance = 100;
        private int bet = 10;
        private string[] symbols = { "cherry", "lemon", "orange", "bell", "grape", "seven", "star", "watermelon" };
        private Random random = new Random();
        private bool win = false;
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
            int index1 = random.Next(symbols.Length);
            int index2 = random.Next(symbols.Length);
            int index3 = random.Next(symbols.Length);
            if (index1 == index2 && index2 == index3)
            {
                win = true;
                balance += bet * 10;
            }
            image1.Source = new BitmapImage(new Uri($"assets/{symbols[index1]}.png", UriKind.Relative));
            image2.Source = new BitmapImage(new Uri($"assets/{symbols[index2]}.png", UriKind.Relative));
            image3.Source = new BitmapImage(new Uri($"assets/{symbols[index3]}.png", UriKind.Relative));
            balanceTextBox.Text = $"{balance}";
        }
    }
}