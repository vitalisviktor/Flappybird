using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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

        private List<Image> toBeRemoved;

        private int skip;
        private Stopwatch stopwatch;

        private int flip;
        private int goingUp;
        public MainWindow()
        {
            InitializeComponent();
            //stopwatch = new Stopwatch();
            tickCount = 0;
            fallSpeed = 1;
            flip = 0;
            goingUp = 0;



        }

        private void Tick(object? sender, EventArgs e)
        {
            tickCount++;
            lb_Bird.Visibility = Visibility.Visible;
            lb_Bird.Focus();







            if (flip > 0)
            {
                bird_rotation.Angle += 15;
                flip--;
            }

            if (Canvas.GetTop(lb_Bird) <= -10)
            {
                MessageBox.Show("Meghaltál", "Vesztes");

            }
            else if (Canvas.GetTop(lb_Bird) >= 400)
            {
                MessageBox.Show("Meghaltál", "Vesztes");

            }
            else
            {


                Canvas.SetTop(lb_Bird, Canvas.GetTop(lb_Bird) + fallSpeed);
                if (goingUp > 0)
                {
                    Canvas.SetTop(lb_Bird, Canvas.GetTop(lb_Bird) - goingUp / 2);
                    goingUp--;
                }
                if (tickCount % 4 == 0)
                {
                    fallSpeed++;
                }

                if (tickCount % 60 == 0)
                {
                    Create_Pipes();
                }
                toBeRemoved = new List<Image>();
                foreach (Image p in Pipe_path.Children)
                {
                    if (Canvas.GetLeft(p) < -45)
                    {
                        toBeRemoved.Add(p);
                    }
                    Canvas.SetLeft(p, Canvas.GetLeft(p) - 5);
                    if (Canvas.GetLeft(p) >= 10 && Canvas.GetLeft(p) <= 80)
                    {
                        if (Canvas.GetBottom(p) == 0) //lenti
                        {
                            //Trace.WriteLine(Canvas.GetTop(lb_Bird);
                            if ((p.Height) + Canvas.GetTop(lb_Bird) + lb_Bird.Height >= Pipe_path.ActualHeight + 15)
                            {
                                MessageBox.Show("Meghaltál", "Vesztes");


                            }
                        }
                        else //fenti
                        {
                            if (p.Height - 15 >= Canvas.GetTop(lb_Bird))
                            {
                                MessageBox.Show("Meghaltál", "Vesztes");


                            }
                        }
                    }
                }
                foreach (Image r in toBeRemoved)
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

                if (rnd.Next(0, 100) == 1)
                {
                    flip = 24;
                }

            }
        }


        private void Create_Pipes()
        {
            rnd = new Random();
            Image pipeBottom = new Image();
            Image pipeTop = new Image();

            pipeBottom.Source = new BitmapImage(new Uri("/pipe.png", UriKind.Relative));
            pipeTop.Source = new BitmapImage(new Uri("/pipe.png", UriKind.Relative));

            int topPipeHeight = rnd.Next(30, 180);
            int bottomPipeHeight = (int)((Pipe_path.ActualHeight - topPipeHeight) - lb_Bird.Height - rnd.Next(110, 190));

            pipeBottom.Height = bottomPipeHeight;
            pipeTop.Height = topPipeHeight;


            pipeBottom.Stretch = Stretch.Fill;
            pipeTop.Stretch = Stretch.Fill;

            pipeBottom.Width = 50;
            pipeTop.Width = 50;
            
            Pipe_path.Children.Add(pipeBottom);
            Pipe_path.Children.Add(pipeTop);

            Canvas.SetLeft(pipeBottom, 800);
            Canvas.SetLeft(pipeTop, 800);

             Canvas.SetBottom(pipeBottom, 0);

            

        }

        private void btn_start_Click(object sender, RoutedEventArgs e)
        {
            btn_start.Visibility = Visibility.Collapsed;
            txbl_GameTitle.Visibility = Visibility.Collapsed;
            CompositionTarget.Rendering += Tick;

        }
    }
}