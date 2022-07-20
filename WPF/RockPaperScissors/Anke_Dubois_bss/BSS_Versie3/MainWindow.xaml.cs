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
using Microsoft.VisualBasic;

namespace BSS_Versie3
{
    /// ---------------------------------------------------
    /// Author:         Dubois Anke, 1ProK
    /// Create Date:    22/12/2020
    /// Description:    Age of Empires-project
    /// ---------------------------------------------------
    /// <summary>
    /// Versie 3 Blad, steen schaar aangepast naar Age of Empires volgens de extra opdracht.
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

        #region Methodes timer
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
                uitkomstComputer.Source = new BitmapImage(new Uri(@"img\clock.png", UriKind.RelativeOrAbsolute));
                uitkomstSpeler.Source = new BitmapImage(new Uri(@"img\clock.png", UriKind.RelativeOrAbsolute));
            }
        }
        #endregion

        #region Clickevents
        private int spelerRidder;
        private void btnKnight_Click(object sender, RoutedEventArgs e)
        {

            // Methode oproepen om comp randomn keuze te laten maken
            KeuzeComp();

            // Juiste afbeelding printen.
            uitkomstSpeler.Source = new BitmapImage(new Uri(@"img\knight.png", UriKind.RelativeOrAbsolute));

            // Aanpassen buttons
            btnImageRidder.Source = new BitmapImage(new Uri(@"img\knightclick.png", UriKind.RelativeOrAbsolute));
            btnImageBoogschutter.Source = new BitmapImage(new Uri(@"img\archer.png", UriKind.RelativeOrAbsolute));
            btnImageZwaardvechter.Source = new BitmapImage(new Uri(@"img\spearman.png", UriKind.RelativeOrAbsolute));
            btnImageSpeervechter.Source = new BitmapImage(new Uri(@"img\swordsman.png", UriKind.RelativeOrAbsolute));

            // Op basis van de keuze van de computer, namelijk 0,1,2 en andere wint de speler, computer of is er een draw (gelijkspel)
            if (keuze == 0)
            {
                Draw();
            }
            else if (keuze == 1)
            {
                SpelerWint();
            }
            else if (keuze == 2)
            {
                Draw();
            }
            else
            {
                ComputerWint();
            }

            // Aantal ridder die de speler heeft gekozen gaat met 1 omhoog en wordt geprint op de juiste plaats
            spelerRidder++;
            aantalRidderSpeler.Content = "Ridder: " + (spelerRidder);

            // reset aantal secondes terug naar 4, om die timer correct te laten lopen
            aantalSeconde = 4;
        }

        private int spelerBoogschutter;
        private void btnArcher_Click(object sender, RoutedEventArgs e)
        {
            KeuzeComp();

            uitkomstSpeler.Source = new BitmapImage(new Uri("/img/archer.png", UriKind.RelativeOrAbsolute));

            btnImageRidder.Source = new BitmapImage(new Uri(@"img\knight.png", UriKind.RelativeOrAbsolute));
            btnImageBoogschutter.Source = new BitmapImage(new Uri(@"img\archerclick.png", UriKind.RelativeOrAbsolute));
            btnImageZwaardvechter.Source = new BitmapImage(new Uri(@"img\spearman.png", UriKind.RelativeOrAbsolute));
            btnImageSpeervechter.Source = new BitmapImage(new Uri(@"img\swordsman.png", UriKind.RelativeOrAbsolute));

            if (keuze == 0)
            {
                ComputerWint();
            }
            else if (keuze == 1)
            {
                Draw();
            }
            else if (keuze == 2)
            {
                SpelerWint();
            }
            else
            {
                Draw();
            }

            spelerBoogschutter++;
            aantalBoogschutterSpeler.Content = "Boogschutter: " + (spelerBoogschutter);

            aantalSeconde = 4;
        }


        private int spelerZwaardvechter;
        private void btnSwordsman_Click(object sender, RoutedEventArgs e)
        {
            KeuzeComp();

            uitkomstSpeler.Source = new BitmapImage(new Uri("/img/swordsman.png", UriKind.RelativeOrAbsolute));

            btnImageRidder.Source = new BitmapImage(new Uri(@"img\knight.png", UriKind.RelativeOrAbsolute));
            btnImageBoogschutter.Source = new BitmapImage(new Uri(@"img\archer.png", UriKind.RelativeOrAbsolute));
            btnImageZwaardvechter.Source = new BitmapImage(new Uri(@"img\spearmanclick.png", UriKind.RelativeOrAbsolute));
            btnImageSpeervechter.Source = new BitmapImage(new Uri(@"img\swordsman.png", UriKind.RelativeOrAbsolute));

            if (keuze == 0)
            {
                Draw();
            }
            else if (keuze == 1)
            {
                ComputerWint();
            }
            else if (keuze == 2)
            {
                Draw();
            }
            else
            {
                SpelerWint();
            }

            spelerZwaardvechter++;
            aantalZwaardvechterSpeler.Content = "Zwaardvechter: " + (spelerZwaardvechter);

            aantalSeconde = 4;
        }

        private int spelerSpeervechter;

        private void btnSpearman_Click(object sender, RoutedEventArgs e)
        {
            KeuzeComp();

            uitkomstSpeler.Source = new BitmapImage(new Uri("/img/spearman.png", UriKind.RelativeOrAbsolute));

            btnImageRidder.Source = new BitmapImage(new Uri(@"img\knight.png", UriKind.RelativeOrAbsolute));
            btnImageBoogschutter.Source = new BitmapImage(new Uri(@"img\archer.png", UriKind.RelativeOrAbsolute));
            btnImageZwaardvechter.Source = new BitmapImage(new Uri(@"img\spearman.png", UriKind.RelativeOrAbsolute));
            btnImageSpeervechter.Source = new BitmapImage(new Uri(@"img\swordsmanclick.png", UriKind.RelativeOrAbsolute));

            if (keuze == 0)
            {
                SpelerWint();
            }
            else if (keuze == 1)
            {
                Draw();
            }
            else if (keuze == 2)
            {
                ComputerWint();
            }
            else
            {
                Draw();
            }

            spelerSpeervechter++;
            aantalSpeerwerperspeler.Content = "Speervechter" + (spelerSpeervechter);

            aantalSeconde = 4;
        }
        #endregion

        #region Randomn generator computer
        // Methode om computer random te laten kiezen tussen blad steen en schaar, door waardes toe te kennen van 0 tot 3
        private int keuze;
        private int compRidder = 0;
        private int compBoogschutter = 0;
        private int compZwaardvechter = 0;
        private int compSpeervechter = 0;
        private void KeuzeComp()
        {
            Random random = new Random();
            keuze = random.Next(0, 4);

            if (keuze == 0)
            {
                uitkomstComputer.Source = new BitmapImage(new Uri("/img/knight.png", UriKind.RelativeOrAbsolute));
                compRidder++;
                aantalRidderComp.Content = "Ridder: " + (compRidder);
            }
            else if (keuze == 1)
            {
                uitkomstComputer.Source = new BitmapImage(new Uri("/img/archer.png", UriKind.RelativeOrAbsolute));

                compBoogschutter++;
                aantalBoogschutterComp.Content = "Boogschutter: " + (compBoogschutter);
            }
            else if (keuze == 2)
            {
                uitkomstComputer.Source = new BitmapImage(new Uri("/img/swordsman.png", UriKind.RelativeOrAbsolute));
                compZwaardvechter++;
                aantalZwaardvechterComp.Content = "Zwaardvechter: " + (compZwaardvechter);
            }
            else
            {
                uitkomstComputer.Source = new BitmapImage(new Uri("/img/spearman.png", UriKind.RelativeOrAbsolute));

                compSpeervechter++;
                aantalSpeerwerperComp.Content = "Speervechter: " + (compSpeervechter);
            }
        }
        #endregion

        #region Spelmethods
        // Methode als speler wint
        private int tellerSpeler = 0;
        private void SpelerWint()
        {
            // print de winnaar
            lblMessage.Content = "De speler wint!";

            // verander achtergrondkleuren
            stackSpeler.Background = Brushes.LightGreen;
            stackComputer.Background = Brushes.Coral;

            // Berekening nieuwe score

            tellerSpeler++;

            //print nieuwe score
            txtSpelerAantal.Text = Convert.ToString(tellerSpeler);

            //Afbeeldingen voor timer die verloopt
            TimerAfgelopen();

            // check de eindscore
            Eindscore();
        }

        // Methode als computer wint
        private int tellerComp;
        private void ComputerWint()

        {

            lblMessage.Content = "De computer wint!";

            stackComputer.Background = Brushes.LightGreen;
            stackSpeler.Background = Brushes.Coral;

            tellerComp++;

            txtCompAantal.Text = Convert.ToString(tellerComp);

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
        #endregion

        #region Afloop spel
        // Methode om de eindscore te berekenen en messagebox
        private int eindScore = 10;
        private void Eindscore()
        {
            // Aanmaak nieuwe string om in de Messagebox te displayen.
            string berichtMessageBox;

            // Spel bereikt einde als de score van de speler OF als de score van de computer gelijk is aan de eindscore, namelijk 10.
            if (tellerSpeler == eindScore || tellerComp == eindScore)
            {
                //timer stoppen 
                dispatcher.Stop();

                // Print score op het scorebord
                UpdateUitslagen();

                if (tellerSpeler == eindScore)
                {
                    berichtMessageBox = "Proficiat! Jij bent de winnaar!";

                }
                else
                {
                    berichtMessageBox = "Helaas, de computer wint!";
                }
                MessageBoxResult antwoord = MessageBox.Show("Wil je opnieuw spelen?", $"{berichtMessageBox}", MessageBoxButton.YesNo);

                // Als de gebruiker "yes" aanduid in de messagebox, dan wordt het spel gereset, anders wordt het spel afgesloten
                if (antwoord == MessageBoxResult.Yes)
                {
                    txtSpelerAantal.Text = "0";
                    tellerSpeler = 0;
                    txtCompAantal.Text = "0";
                    tellerComp = 0;
                    uitkomstComputer.Source = null;
                    uitkomstSpeler.Source = null;
                    lblMessage.Content = "Welkom terug!";
                    dispatcher.Start();
                    spelerRidder = 0;
                    spelerBoogschutter = 0;
                    spelerSpeervechter = 0;
                    spelerZwaardvechter = 0;
                    compRidder = 0;
                    compBoogschutter = 0;
                    compZwaardvechter = 0;
                    compSpeervechter = 0;
                    stackComputer.Background = Brushes.LightGray;
                    stackSpeler.Background = Brushes.LightGray;
                    btnImageRidder.Source = new BitmapImage(new Uri(@"img\knight.png", UriKind.RelativeOrAbsolute));
                    btnImageBoogschutter.Source = new BitmapImage(new Uri(@"img\archer.png", UriKind.RelativeOrAbsolute));
                    btnImageZwaardvechter.Source = new BitmapImage(new Uri(@"img\spearman.png", UriKind.RelativeOrAbsolute));
                    btnImageSpeervechter.Source = new BitmapImage(new Uri(@"img\swordsman.png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    Application.Current.Shutdown();
                }
            }
        }
        #endregion

        #region Uitslagen
        private string scorebord;
        private string defaultValue;
        private string tempNaamSpeler;
        private string message = "Geef je naam.";
        private string title = "Naam speler";

        // methode om de uitslagen te berekenen en af te printen.
        private void UpdateUitslagen()
        {
            // Aanmaak inputbox om naam van de speler op te vragen.
            string naamSpeler = Microsoft.VisualBasic.Interaction.InputBox(message, title, defaultValue, -1, -1);

            // Als de naam van de speler leeg blijft wordt er anonieme speler gebruikt.
            if (naamSpeler == "")
            {
                naamSpeler = "Anoniem";
            }
            // We onthouden de ingegeven naam en gebruiken die als de default ingevulde waarde binnen de inputbox
            tempNaamSpeler = naamSpeler;
            defaultValue = tempNaamSpeler;


            string tempScorebord = $"{naamSpeler} - {tellerSpeler} - Computer - {tellerComp} - ({DateTime.Now.ToString("HH:mm:ss")}) \n";
            scorebord = tempScorebord + scorebord;

            lblScorebord.Content = scorebord;
        }
        #endregion

        #region MouseEnter
        private void btnImageRidder_MouseEnter(object sender, MouseEventArgs e)
        {
            btnImageRidder.Source = new BitmapImage(new Uri(@"img\knighthover.png", UriKind.RelativeOrAbsolute));
        }

        private void btnImageBoogschutter_MouseEnter(object sender, MouseEventArgs e)
        {
            btnImageBoogschutter.Source = new BitmapImage(new Uri(@"img\archerhover.png", UriKind.RelativeOrAbsolute));
        }

        private void btnImageZwaardvechter_MouseEnter(object sender, MouseEventArgs e)
        {
            btnImageZwaardvechter.Source = new BitmapImage(new Uri(@"img\swordmanhover.png", UriKind.RelativeOrAbsolute));
        }

        private void btnImageSpeervechter_MouseEnter(object sender, MouseEventArgs e)
        {
            btnImageSpeervechter.Source = new BitmapImage(new Uri(@"img\spearmanhover.png", UriKind.RelativeOrAbsolute));
        }

        #endregion

        #region MouseLeave
        private void btnImageRidder_MouseLeave(object sender, MouseEventArgs e)
        {
            btnImageRidder.Source = new BitmapImage(new Uri(@"img\knight.png", UriKind.RelativeOrAbsolute));
        }

        private void btnImageBoogschutter_MouseLeave(object sender, MouseEventArgs e)
        {
            btnImageBoogschutter.Source = new BitmapImage(new Uri(@"img\archer.png", UriKind.RelativeOrAbsolute));
        }

        private void btnImageZwaardvechter_MouseLeave(object sender, MouseEventArgs e)
        {
            btnImageZwaardvechter.Source = new BitmapImage(new Uri(@"img\swordsman.png", UriKind.RelativeOrAbsolute));
        }

        private void btnImageSpeervechter_MouseLeave(object sender, MouseEventArgs e)
        {
            btnImageSpeervechter.Source = new BitmapImage(new Uri(@"img\spearman.png", UriKind.RelativeOrAbsolute));
        }
        #endregion
    }
}

        
