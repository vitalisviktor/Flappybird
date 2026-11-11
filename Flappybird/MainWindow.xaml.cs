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

        private int flip;
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
            flip = 0;
            goingUp = 0;
            timer.Start();
            Create_Pipes();
            
        }

        private void Tick(object? sender, EventArgs e)
        {
            if (flip > 0)
            {
                bird_rotation.Angle += 30;
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
                Canvas.SetTop(lb_Bird, Canvas.GetTop(lb_Bird) - goingUp);
                goingUp--;
            }
            if (tickCount % 10 == 0)
            {
                fallSpeed++;
            }

            if(tickCount % 30 == 0)
            {
                Create_Pipes();
            }

            foreach(Rectangle p in Pipe_path.Children)
            {
                
                Canvas.SetLeft(p,Canvas.GetLeft(p) - 5);
                if(Canvas.GetLeft(p) > 50 && Canvas.GetLeft(p) < 95)
                    {
                        if(Canvas.GetBottom(p) == 0) //lenti
                        {
                            //Trace.WriteLine(Canvas.GetTop(lb_Bird));
                            if(p.Height + lb_Bird.Height >= Canvas.GetTop(lb_Bird) && false)
                            {
                                MessageBox.Show("Meghaltál", "Vesztes");
                                timer.Stop();

                            }
                        }
                        else //fenti
                        {
                            Trace.WriteLine(p.Height);
                            Trace.WriteLine(Canvas.GetTop(lb_Bird));
                            Trace.WriteLine(lb_Bird.ActualHeight);
                            if ((p.Height) + Canvas.GetTop(lb_Bird) + lb_Bird.Height >= 412.5)
                            {
                                MessageBox.Show("Meghaltál", "Vesztes");
                                timer.Stop();

                            }
                        }
                    }
            }
            }
        }

        private void lb_Bird_KeyDown(object sender, KeyEventArgs e)
        {
            Random rnd = new Random();
            if (e.Key == Key.Space)
            {
                goingUp = 10;
                fallSpeed = 1;

                if(rnd.Next(0,100) == 1)
                {
                    flip = 12;
                }

            }
        }

        private void Create_Pipes()
        {
            rnd = new Random();
            Rectangle pipeBottom = new Rectangle();
            Rectangle pipeTop = new Rectangle();

            int topPipeHeight = rnd.Next(30,180);
            int bottomPipeHeight = (int)((450 - topPipeHeight) - lb_Bird.Height - rnd.Next(110,230));

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