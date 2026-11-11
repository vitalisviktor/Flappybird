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
        private int tickCount;
        private int fallSpeed;

        private int goingUp;
        public MainWindow()
        {
            InitializeComponent();
            lb_Bird.Focus();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += Tick;
            tickCount = 0;
            fallSpeed = 1;
            goingUp = 0;
            timer.Start();
            
        }

        private void Tick(object? sender, EventArgs e)
        {
            Canvas.SetTop(lb_Bird, Canvas.GetTop(lb_Bird) + fallSpeed);
            tickCount++;
            if (goingUp > 0)
            {
                Canvas.SetTop(lb_Bird, Canvas.GetTop(lb_Bird) - goingUp);
                goingUp--;
            }
            if (tickCount % 20 == 0)
            {
                fallSpeed++;
            }
        }

        private void lb_Bird_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                goingUp = 10;
                fallSpeed = 1;

            }
        }

        private void Create_Pipe()
        {
            rnd = new Random();
            Image pipeBottom = new Image();
            Image pipeTop = new Image();

            int topPipeHeight = rnd.Next(50,200);
            int bottomPipeHeight = topPipeHeight - rnd.Next(50, 100);

            pipeBottom.Height = bottomPipeHeight;
            pipeTop.Height = topPipeHeight;
        }
    }
}