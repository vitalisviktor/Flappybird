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
using System.Windows.Threading;

namespace Flappybird
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private DispatcherTimer timer;
        private Random rnd;
        public MainWindow()
        {
            InitializeComponent();
            lb_Bird.Focus();
            timer = new DispatcherTimer();
            rnd = new Random();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += Tick;
            timer.Start();
            
        }

        private void Tick(object? sender, EventArgs e)
        {
            Canvas.SetTop(lb_Bird, Canvas.GetTop(lb_Bird) + 3);
        }

        private void lb_Bird_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                Canvas.SetTop(lb_Bird, Canvas.GetTop(lb_Bird) - 50);

            }
        }
    }
}