using System;
using System.Collections.Generic;
using System.Data;
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

namespace PrivacyDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CreateDataTable();
        }

        private void CreateDataTable()
        {
            // 1 dataset aanmaken
            DataSet ds = new DataSet("Privacy Demo");

            // 2 datatable aanmaken
            DataTable dt = new DataTable("Demo table");

            // 3 columns aanmaken
            DataColumn id = new DataColumn("Id",typeof(int));
            DataColumn firstName = new DataColumn("FirstName", typeof(string));
            DataColumn lastName = new DataColumn("LastName", typeof(string));

            // 4 koppeling datatabel
            dt.Columns.Add(id);
            dt.Columns.Add(firstName);
            dt.Columns.Add(lastName);

            // 5 primary key instellen
            DataColumn[] primaryKey = {id};
            dt.PrimaryKey = primaryKey;

            // 6 toevoegen van rijen
            dt.Rows.Add(1, "Sander", "De Puydt");
            dt.Rows.Add(2, "Anke", "Dubois");
            dt.Rows.Add(3, "Jeffrey", "Lampaert");

            // 7 gebruik datatable
            ds.Tables.Add(dt);
            DataGridData.ItemsSource = dt.DefaultView;
        }
    }
}
