using Spis_Gier.Class;
using Spis_Gier.Models;
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

namespace Spis_Gier.UserControls
{
    public partial class UCGameEdit : UserControl
    {

        private SGSQLite cSQLite;  
        private string id;
        private MainWindow mWin;

        public UCGameEdit(MainWindow _mw, string _id)
        {
            InitializeComponent();
            cSQLite = new SGSQLite();

            id = _id;
            mWin = _mw;
   
            cobxType.ItemsSource = GamesType();
            cobxPlatform.ItemsSource = GamesPlatform();
            
            SelectGames(id);

        }

        // wyświetlanie listy gier
        public void SelectGames(string idTab)
        {

            List<Games> row = new List<Games>();

            int endGame;
            DateTime dateConvert;

            DataSet ds = null;
            ds = cSQLite.Select("SELECT * FROM games WHERE id = '"+idTab+"' ");

            txtTitle.Text = ds.Tables[0].Rows[0].ItemArray[1].ToString();
            cobxType.Text = ds.Tables[0].Rows[0].ItemArray[2].ToString();
            cobxPlatform.Text = ds.Tables[0].Rows[0].ItemArray[3].ToString();

            endGame = int.Parse(ds.Tables[0].Rows[0].ItemArray[6].ToString());
            if (endGame == 0)
            { chbxEnd.IsChecked = false; }
            else
            { chbxEnd.IsChecked = true; }

            dateConvert = DateTime.Parse(ds.Tables[0].Rows[0].ItemArray[9].ToString());
            dpPremiere.Text = ds.Tables[0].Rows[0].ItemArray[9].ToString();

        }// end list





        public List<string> GamesType() // (ComboBox cb, string sql)
        {

            List<string> listComboBox = new List<string>();
           // listComboBox.Add(" -- Wybierz -- ");

            DataSet ds = null;
            ds = cSQLite.Select("SELECT * FROM type ORDER BY name ASC");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                listComboBox.Add(ds.Tables[0].Rows[i].ItemArray[1].ToString());
            }

            //listComboBox.Add("Przygodowa");
            //listComboBox.Add("Akcji");
            //listComboBox.Add("Strzelanka");
            //listComboBox.Add("Strategiczna");
            //listComboBox.Add("RPG");
            //listComboBox.Add("Platformowa");

            return listComboBox;
        }

        public List<string> GamesPlatform() // (ComboBox cb, string sql)
        {

            List<string> listComboBox = new List<string>();
           // listComboBox.Add(" -- Wybierz -- ");

            DataSet ds = null;
            ds = cSQLite.Select("SELECT * FROM platform ORDER BY name ASC");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                listComboBox.Add(ds.Tables[0].Rows[i].ItemArray[1].ToString());
            } 

            //listComboBox.Add("PC - Pudełko");
            //listComboBox.Add("PC - Battle.net");
            //listComboBox.Add("PC - Origin");
            //listComboBox.Add("PC - Uplay");
            //listComboBox.Add("PC - GOG");
            //listComboBox.Add("XBOX ONE - Pudełko");
            //listComboBox.Add("XBOX ONE - Xbox Live Gold");
            //listComboBox.Add("XBOX 360 - Pudełko");
            //listComboBox.Add("SWITCH - Pudełko");
            //listComboBox.Add("SWITCH - Nintendo eShop");
            //listComboBox.Add("3DS - Pudełko");
            //listComboBox.Add("3DS - Nintendo eShop");
            //listComboBox.Add("PlayStation - PlayStation Store");

            return listComboBox;
        }




        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            string title = txtTitle.Text;
            string type = cobxType.Text; // Gatunek
            string platform = cobxPlatform.Text; // Platforma 
            string edition = "";
            string srv = "";
            int dlc = 0;
            string description = "Brak"; // txtDescription.Text;
            int endGame;

            string datePremiere, dateBuy;
            DateTime dateConvert;

            if (chbxEnd.IsChecked == true)
            { endGame = 1; }
            else
            { endGame = 0; }

            if (dpPremiere.SelectedDate != null)
            {
                dateConvert = DateTime.Parse(dpPremiere.Text);
                datePremiere = dateConvert.ToString("yyyy-MM-dd");
            }
            else
            {
                dateConvert = DateTime.Parse("0001-01-01");
                datePremiere = dateConvert.ToString("yyyy-MM-dd");
            }

            dateConvert = DateTime.Parse("0001-01-01");// DateTime.Parse(dpBuy.Text);
            dateBuy = dateConvert.ToString("yyyy-MM-dd");


            if (txtTitle.Text != "")
            {
                if (cSQLite.Update(title, type, platform, edition, srv, dlc, description, endGame, datePremiere, dateBuy, id))
                {
                   // MessageBox.Show("Zmiany zostały zapisane.");

                    UCGameList ucGL = new UCGameList(mWin);
                    mWin.GridMain.Children.Add(ucGL);
                }
                else
                {
                    MessageBox.Show("Wystąpił błąd zapisu!.");
                }
            }
            else
            {
                MessageBox.Show("Podaj tytuł gry.", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }




        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {

            UCGameList ucGL = new UCGameList(mWin);
            mWin.GridMain.Children.Add(ucGL);

        }



    }
}
