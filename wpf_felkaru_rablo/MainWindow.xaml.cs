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
using System.Security.Cryptography;
using System.Windows.Threading;
using System.Diagnostics.SymbolStore;

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
        private int spinTicks = 0;
        private int symbolCount = 10;
        private string[] reel;
        private int reel1Position = 0;
        private int reel2Position = 0;
        private int reel3Position = 0;

        private DispatcherTimer timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += Timer_Tick;
            BuildReel();
        }
        private void BuildReel()
        {
            reel = new string[symbolCount];
            for (int i = 0; i < symbolCount; i++)
            {
                reel[i] = symbols[random.Next(symbols.Length)];
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            spinTicks++;
            reel1Position++;
            reel2Position++;
            reel3Position++;
            if (reel1Position >= symbolCount) reel1Position = 0;
            if (reel2Position >= symbolCount) reel2Position = 0;
            if (reel3Position >= symbolCount) reel3Position = 0;

            int reel1TopIndex = reel1Position + 1;
            int reel1BottomIndex = reel1Position - 1;
            int reel2TopIndex = reel2Position + 1;
            int reel2BottomIndex = reel2Position - 1;
            int reel3TopIndex = reel3Position + 1;
            int reel3BottomIndex = reel3Position - 1;

            image1.Source = new BitmapImage(new Uri($"assets/{reel[reel1Position]}.png", UriKind.Relative));
            image2.Source = new BitmapImage(new Uri($"assets/{reel[reel2Position]}.png", UriKind.Relative));
            image3.Source = new BitmapImage(new Uri($"assets/{reel[reel3Position]}.png", UriKind.Relative));

            if (reel1BottomIndex < 0) reel1BottomIndex = symbolCount - 1;
            if (reel1TopIndex >= symbolCount) reel1TopIndex = 0;
            if (reel2BottomIndex < 0) reel2BottomIndex = symbolCount - 1;
            if (reel2TopIndex >= symbolCount) reel2TopIndex = 0;
            if (reel3BottomIndex < 0) reel3BottomIndex = symbolCount - 1;
            if (reel3TopIndex >= symbolCount) reel3TopIndex = 0;


            image1Top.Source = new BitmapImage(new Uri($"assets/{reel[reel1TopIndex]}.png", UriKind.Relative));
            image1Bottom.Source = new BitmapImage(new Uri($"assets/{reel[reel1BottomIndex]}.png", UriKind.Relative));

            image2Top.Source = new BitmapImage(new Uri($"assets/{reel[reel2TopIndex]}.png", UriKind.Relative));
            image2Bottom.Source = new BitmapImage(new Uri($"assets/{reel[reel2BottomIndex]}.png", UriKind.Relative));

            image3Top.Source = new BitmapImage(new Uri($"assets/{reel[reel3TopIndex]}.png", UriKind.Relative));
            image3Bottom.Source = new BitmapImage(new Uri($"assets/{reel[reel3BottomIndex]}.png", UriKind.Relative));


            if (spinTicks >= 20)
            {
                timer.Stop();
                lasnTick();
            }
        }

        private void spinButton_Click(object sender, RoutedEventArgs e)
        {
            if (balance < bet)
            {
                balanceTextBox.Text = "Nincs elég pénz a téthez!";
                return;
            }
            spinButton.IsEnabled = false;
            balance -= bet;
            spinTicks = 0;
            timer.Start();
        }
        private void lasnTick ()
        {
            int index1 = random.Next(symbols.Length);
            int index2 = random.Next(symbols.Length);
            int index3 = random.Next(symbols.Length);
            if (index1 == index2 && index2 == index3)
            {
                win = true;
                balance += bet * 10;
                resultsTextBox.Text = "Nyertél!";
            }
            else if (index1 == index2 || index2 == index3 || index1 == index3)
            {
                win = true;
                balance += bet * 2;
                resultsTextBox.Text = "Részleges nyeremény!";
            }
            else
            {
                win = false;
                resultsTextBox.Text = "Vesztettél!";
            }
            image1.Source = new BitmapImage(new Uri($"assets/{symbols[index1]}.png", UriKind.Relative));
            image2.Source = new BitmapImage(new Uri($"assets/{symbols[index2]}.png", UriKind.Relative));
            image3.Source = new BitmapImage(new Uri($"assets/{symbols[index3]}.png", UriKind.Relative));
            balanceTextBox.Text = $"{balance}";
            spinButton.IsEnabled = true;
        }
    }
}