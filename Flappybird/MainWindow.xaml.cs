using System.Diagnostics;
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

        private List<Rectangle> toBeRemoved;

        private int flip;
        private int goingUp;
        public MainWindow()
        {
            InitializeComponent();
            lb_Bird.Focus();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(5);
            timer.Tick += Tick;
            tickCount = 0;
            fallSpeed = 1;
            flip = 0;
            goingUp = 0;
            timer.Start();
            
            
        }

        private void Tick(object? sender, EventArgs e)
        {
            if (flip > 0)
            {
                bird_rotation.Angle += 15;
                flip--;
            }

            if (Canvas.GetTop(lb_Bird) <= -10)
            {
                MessageBox.Show("Meghaltál", "Vesztes");
                timer.Stop();
            }
            else if (Canvas.GetTop(lb_Bird) >= 400)
            {
                MessageBox.Show("Meghaltál", "Vesztes");
                timer.Stop();
            }
            else
            {


            Canvas.SetTop(lb_Bird, Canvas.GetTop(lb_Bird) + fallSpeed);
            tickCount++;
            if (goingUp > 0)
            {
                Canvas.SetTop(lb_Bird, Canvas.GetTop(lb_Bird) - goingUp / 2);
                goingUp--;
            }
            if (tickCount % 4 == 0)
            {
                fallSpeed++;
            }

            if(tickCount % 60 == 0)
            {
                Create_Pipes();
            }
                toBeRemoved = new List<Rectangle>();
            foreach(Rectangle p in Pipe_path.Children)
            {
                    if(Canvas.GetLeft(p) < -45)
                    {
                        toBeRemoved.Add(p);
                    }
                Canvas.SetLeft(p,Canvas.GetLeft(p) - 5);
                if(Canvas.GetLeft(p) >= 10 && Canvas.GetLeft(p) <= 90)
                    {
                        if(Canvas.GetBottom(p) == 0) //lenti
                        {
                            //Trace.WriteLine(Canvas.GetTop(lb_Bird);
                            if ((p.Height) + Canvas.GetTop(lb_Bird) + lb_Bird.Height >= Pipe_path.ActualHeight)
                            {
                                MessageBox.Show("Meghaltál", "Vesztes");
                                //timer.Stop();

                            }
                        }
                        else //fenti
                        {
                            if (p.Height >= Canvas.GetTop(lb_Bird))
                            {
                                MessageBox.Show("Meghaltál", "Vesztes");
                                //timer.Stop();

                            }
                        }
                    }
            }
            foreach (Rectangle r in toBeRemoved)
                {
                    Pipe_path.Children.Remove(r);
                }
            }
        }

        private void lb_Bird_KeyDown(object sender, KeyEventArgs e)
        {
            Random rnd = new Random();
            if (e.Key == Key.Space)
            {
                goingUp = 20;
                fallSpeed = 1;

                if(rnd.Next(0,100) == 1)
                {
                    flip = 24;
                }

            }
        }

        private void Create_Pipes()
        {
            rnd = new Random();
            Rectangle pipeBottom = new Rectangle();
            Rectangle pipeTop = new Rectangle();

            int topPipeHeight = rnd.Next(30,180);
            int bottomPipeHeight = (int)((Pipe_path.ActualHeight - topPipeHeight) - lb_Bird.Height - rnd.Next(110,190));

            pipeBottom.Height = bottomPipeHeight;
            pipeTop.Height = topPipeHeight;

            pipeBottom.Width = 50;
            pipeTop.Width = 50;

            pipeBottom.Fill = Brushes.Red;
            pipeTop.Fill = Brushes.Red;

            Pipe_path.Children.Add(pipeBottom);
            Pipe_path.Children.Add(pipeTop);

            Canvas.SetLeft(pipeBottom,800);
            Canvas.SetLeft(pipeTop,800);

            Canvas.SetBottom(pipeBottom,0);
            

        }
    }
}