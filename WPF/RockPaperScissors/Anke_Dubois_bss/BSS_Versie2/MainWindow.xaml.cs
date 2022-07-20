using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace BSS_Versie2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private DispatcherTimer dispatcher;
        private int aantalSeconde = 4;

        public MainWindow()
        {
            InitializeComponent();


            // Aanmaak timer
            dispatcher = new DispatcherTimer();
            dispatcher.Interval = new TimeSpan(0, 0, 1);

            dispatcher.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcher.Start();            
        }
        
        // methode om de timer te laten aftellen van 3 - 0
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            aantalSeconde--;

            //Wanneer het aantal seconden op 0 staat dan wint cpu en score cpu gaat met 1 omhoog

            if (aantalSeconde == 0)
            {
                dispatcher.Stop();
                ComputerWint();

                aantalSeconde = 3;
                dispatcher.Start();
            }
            lblTimer.Content = aantalSeconde;
        }

        // methode wanneer de timer 0 bereikt
        private void TimerAfgelopen()
        {
            if (aantalSeconde == 0)
            {
                lblMessage.Content = "De timer is afgelopen!";
                uitkomstComputer.Source = new BitmapImage(new Uri("/img/timer.png", UriKind.RelativeOrAbsolute));
                uitkomstSpeler.Source = new BitmapImage(new Uri("/img/timer.png", UriKind.RelativeOrAbsolute));
            }
        }

        //Clickevents
        private void btnBlad_Click(object sender, RoutedEventArgs e)
        {

     
            // Schrijf keuze speler en verander kleur border

            uitkomstSpeler.Source = new BitmapImage(new Uri("/img/Blad.png", UriKind.RelativeOrAbsolute));
            btnBlad.BorderBrush = Brushes.Black;
            btnSteen.BorderBrush = Brushes.LightGray;
            btnSchaar.BorderBrush = Brushes.LightGray;

            // Laat computer randomn waarde genereren
            KeuzeComp();

            // Bepalen van winnaar en printen naar applicatie
            if (keuze == 0)
            {
                Draw();
            }
            else if (keuze == 1)
            {
                SpelerWint();
            }
            else
            {
                ComputerWint();
            }

            // Reset de timer(seconden)
            aantalSeconde = 4;

        
        }

        private void btnSteen_Click(object sender, RoutedEventArgs e)
        {
       
            uitkomstSpeler.Source = new BitmapImage(new Uri("/img/Steen.png", UriKind.RelativeOrAbsolute));
            btnBlad.BorderBrush = Brushes.LightGray;
            btnSteen.BorderBrush = Brushes.Black;
            btnSchaar.BorderBrush = Brushes.LightGray;

            KeuzeComp();

            if (keuze == 1)
            {
                Draw();
            }
            else if (keuze == 0)
            {
                SpelerWint();
            }
            else
            {
                ComputerWint();
            }

            aantalSeconde = 4;
        }

        private void btnSchaar_Click(object sender, RoutedEventArgs e)
        {
     
            uitkomstSpeler.Source = new BitmapImage(new Uri("/img/Schaar.png", UriKind.RelativeOrAbsolute));
            btnBlad.BorderBrush = Brushes.LightGray;
            btnSteen.BorderBrush = Brushes.LightGray;
            btnSchaar.BorderBrush = Brushes.Black;

            KeuzeComp();

            if (keuze == 0)
            {
                SpelerWint();
            }
            else if (keuze == 1)
            {
                ComputerWint();
            }
            else
            {
                Draw();
            }
        }

        // Methode om computer random te laten kiezen tussen blad steen en schaar, door waardes toe te kennen van 0 tot 3
        private int keuze;
        private void KeuzeComp()
        {
            Random random = new Random();
            keuze = random.Next(0, 3);

            if (keuze == 0)
            {
                uitkomstComputer.Source = new BitmapImage(new Uri("/img/Blad.png", UriKind.RelativeOrAbsolute));
 
            }
            else if (keuze == 1)
            {
                uitkomstComputer.Source = new BitmapImage(new Uri("/img/Steen.png", UriKind.RelativeOrAbsolute));
        
            }
            else 
            {
                uitkomstComputer.Source = new BitmapImage(new Uri("/img/Schaar.png", UriKind.RelativeOrAbsolute));
            }
        }

        // Methode als speler wint
        private int tellerspeler = 0;
        private void SpelerWint()
        {
            // print de winnaar
            lblMessage.Content = "De speler wint!";

            // verander achtergrondkleuren
            stackSpeler.Background = Brushes.LightGreen;
            stackComputer.Background = Brushes.Coral;

            // Berekening nieuwe score

            tellerspeler++;

            //print nieuwe score
            txtSpelerAantal.Text = Convert.ToString(tellerspeler);

            //Afbeeldingen voor timer die verloopt
            TimerAfgelopen();

            // check de eindscore
            Eindscore();
        }

        // Methode als computer wint
        private int tellercomp;
        private void ComputerWint()

        {

            lblMessage.Content = "De computer wint!";

            stackComputer.Background = Brushes.LightGreen;
            stackSpeler.Background = Brushes.Coral;

            tellercomp++;
            
            txtCompAantal.Text = Convert.ToString(tellercomp);

            TimerAfgelopen();
            Eindscore();
        }

        // Methode als er gelijkspel is
        private void Draw()
        {
            lblMessage.Content = "Gelijkspel, niemand wint!";

            stackComputer.Background = Brushes.LightGray;
            stackSpeler.Background = Brushes.LightGray;

            TimerAfgelopen();
        }


        // Methode om de eindscore te berekenen en messagebox
        private int eindScore = 10;
        private void Eindscore()
        {
            string berichtMessageBox;

            if (tellerspeler == eindScore ||tellercomp == eindScore)
            {
                dispatcher.Stop();

                if (tellerspeler == eindScore)
                {
                    berichtMessageBox = "Proficiat! Jij bent de winnaar!";

                }
                else
                {
                    berichtMessageBox = "Helaas, de computer wint!";
                }
                MessageBoxResult antwoord = MessageBox.Show("Wil je opnieuw spelen?", $"{berichtMessageBox}", MessageBoxButton.YesNo);

                if (antwoord == MessageBoxResult.Yes)
                {
                   
                
                    txtSpelerAantal.Text = "0";
                    tellerspeler = 0;              
                    txtCompAantal.Text = "0";     
                    tellercomp = 0;
                    uitkomstComputer.Source = null;
                    uitkomstSpeler.Source = null;
                    btnBlad.BorderBrush = Brushes.LightGray;
                    btnSteen.BorderBrush = Brushes.LightGray;
                    btnSchaar.BorderBrush = Brushes.LightGray;
                    lblMessage.Content = "Welkom terug!";
                    dispatcher.Start();
                }
                else
                {
                    Application.Current.Shutdown();
                }
            }
        }

        private void ImgBlad_MouseEnter(object sender, MouseEventArgs e)
        {
            ImgBlad.Source = new BitmapImage(new Uri("/img/Blad2.png", UriKind.RelativeOrAbsolute));
        }

        private void ImgSteen_MouseEnter(object sender, MouseEventArgs e)
        {
            ImgSteen.Source = new BitmapImage(new Uri("/img/Steen2.png", UriKind.RelativeOrAbsolute));
        }

        private void ImgSchaar_MouseEnter(object sender, MouseEventArgs e)
        {
            ImgSchaar.Source = new BitmapImage(new Uri("/img/Schaar2.png", UriKind.RelativeOrAbsolute));
        }

        private void ImgBlad_MouseLeave(object sender, MouseEventArgs e)
        {
            ImgBlad.Source = new BitmapImage(new Uri("/img/Blad.png", UriKind.RelativeOrAbsolute));
        }

        private void ImgSteen_MouseLeave(object sender, MouseEventArgs e)
        {
            ImgSteen.Source = new BitmapImage(new Uri("/img/Steen.png", UriKind.RelativeOrAbsolute));
        }

        private void ImgSchaar_MouseLeave(object sender, MouseEventArgs e)
        {
            ImgSchaar.Source = new BitmapImage(new Uri("/img/Schaar.png", UriKind.RelativeOrAbsolute));
        }
    }
}
