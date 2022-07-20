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

namespace BSS_Versie1
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
            
        }

       // Clickevents
        private void btnBlad_Click(object sender, RoutedEventArgs e)
        {
            // Schrijf keuze speler
            uitkomstSpeler.Text = "Blad";

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
        }

        private void btnSteen_Click(object sender, RoutedEventArgs e)
        {
            uitkomstSpeler.Text = "Steen";
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
        }

        private void btnSchaar_Click(object sender, RoutedEventArgs e)
        {
            uitkomstSpeler.Text = "Schaar";
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
        public void KeuzeComp()
        {
            Random random = new Random();
            keuze = random.Next(0,3);

            if (keuze == 0)
            {             
                uitkomstComputer.Text = "Blad";             
            }
            else if (keuze == 1)
            {
                uitkomstComputer.Text = "Steen";
            }
            else 
            {
                uitkomstComputer.Text = "Schaar";               
            }
        }

        // Methode als speler wint
        public void SpelerWint()
        {
            // print de winnaar
            lblMessage.Content = "De speler wint!";

            // verander achtergrondkleuren
            stackSpeler.Background = Brushes.LightGreen;
            stackComputer.Background = Brushes.Coral;

            // Berekening nieuwe score
            int tellerspeler = Convert.ToInt32(txtSpelerAantal.Text);
            int tempteller = tellerspeler + 1;

            //print nieuwe score
            txtSpelerAantal.Text = Convert.ToString(tempteller);
        }

        // Methode als computer wint
        public void ComputerWint()
        {
            lblMessage.Content = "De computer wint!";

            stackComputer.Background = Brushes.LightGreen;
            stackSpeler.Background = Brushes.Coral;

            int tellercomputer = Convert.ToInt32(txtCompAantal.Text);
            int tempteller = tellercomputer + 1;

            txtCompAantal.Text = Convert.ToString(tempteller);
        }

        // Methode als er gelijkspel is
        public void Draw()
        {
            lblMessage.Content = "Gelijkspel, niemand wint!";

            stackComputer.Background = Brushes.LightGray;
            stackSpeler.Background = Brushes.LightGray;
        }
    }
}
