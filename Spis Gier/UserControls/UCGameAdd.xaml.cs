using Spis_Gier.Class;
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
    public partial class UCGameAdd : UserControl
    {
        private SGSQLite cSQLite;
        private MainWindow mWin;

        public UCGameAdd(MainWindow _mw)
        {
            InitializeComponent();

            cSQLite = new SGSQLite();

            mWin = _mw;

            cobxType.ItemsSource     = GamesType();
            cobxPlatform.ItemsSource = GamesPlatform();


            //SelectCBFill(cobxType, "SELECT * FROM type ORDER BY name ASC");
            //cobxType.SelectedIndex = 0;

            //SelectCBFill(cobxPlatform, "SELECT * FROM platform ORDER BY name ASC");
            //cobxPlatform.SelectedIndex = 0;

            //SelectCBFillSrv(cobxSrv, "SELECT * FROM srv ORDER BY name ASC");
            //cobxSrv.SelectedIndex = 0;

            //dpPremiere.SelectedDate = DateTime.Parse(DateTime.Now.ToString());


            // BindData();
        }


        public List<string> GamesType() // (ComboBox cb, string sql)
        {

            List<string> listComboBox = new List<string>();
            listComboBox.Add(" -- Wybierz -- ");

            DataSet ds = null;
            ds = cSQLite.Select("SELECT * FROM type ORDER BY name ASC");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                listComboBox.Add(ds.Tables[0].Rows[i].ItemArray[1].ToString());
            }

            return listComboBox;
        }

        public List<string> GamesPlatform() // (ComboBox cb, string sql)
        {

            List<string> listComboBox = new List<string>();
            listComboBox.Add(" -- Wybierz -- ");

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



            //listComboBox.Add("PC");
            //listComboBox.Add("XBOX 360");
            //listComboBox.Add("XBOX ONE");
            //listComboBox.Add("Switch");
            //listComboBox.Add("3DS");
            //listComboBox.Add("PS VITA");

            return listComboBox;
        }



        public void SelectCBFill(ComboBox cb, string sql)
        {
            //DataSet ds = null;
            // ds = gl.SelectCB(sql);

            // cb.ItemsSource = ds.Tables[0].DefaultView; // lub zamiast t mobze być 0
            // cb.DisplayMemberPath = "name";
            // cb.SelectedValuePath = "id";
            // cobxType.Items.Clear();
        }


        public void SelectCBFillSrv(ComboBox cb, string sql)
        {
            //DataSet ds = null;
            //ds = gl.SelectCB(sql);

            //DataTable dt = new DataTable();


            //cb.SelectedValuePath = "Key";
            //cb.DisplayMemberPath = "Value";
            //cb.Items.Clear();

            //List<string>[] tab;
            //tab = gl.SelComBox(sql);

            //cb.Items.Add(new KeyValuePair<int, string>(1, "Pudełkowe"));

            //for (int i = 0; i < tab[0].Count; i++)
            //{
            //    cb.Items.Add(new KeyValuePair<int, string>(Int32.Parse(tab[0][i]), tab[1][i]));
            //} // end for
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            string title = txtTitle.Text;
            string type = cobxType.Text; // Gatunek
            string platform = cobxPlatform.Text; // Platforma (PC, XBOX, PlayStetion, SWITCH)
            string edition = "";
            string srv = "";
            int dlc = 0;
            string description = "Brak"; // txtDescription.Text;
            int endGame = 0;

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
                //dpPremiere.SelectedDate = DateTime.Now.Date;
            }

            dateConvert = DateTime.Parse("0001-01-01");// DateTime.Parse(dpBuy.Text);
            dateBuy = dateConvert.ToString("yyyy-MM-dd");


            if (txtTitle.Text != "")
            {
                if (cSQLite.Insert(title, type, platform, edition, srv, dlc, description, endGame, datePremiere, dateBuy))
                {
                    //MessageBox.Show("Gra została dodana.");

                    UCGameList ucGL = new UCGameList(mWin);
                    mWin.GridMain.Children.Add(ucGL);

                }
                else
                {
                    MessageBox.Show("Wystąpił błąd!  Nie dodano gry.");
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

 




        //private void btnOK_Click(object sender, RoutedEventArgs e)
        //{
        //    //string title = txtTitle.Text;

        //    //string type = cobxType.Text; // Gatunek
        //    //string platform = cobxPlatform.Text; // Platforma
        //    //string edition = "box";
        //    //string srv = "";
        //    //int dlc = 0;
        //    //string description = txtDescription.Text;
        //    //int endGame;

        //    //string datePremiere, dateBuy;
        //    //DateTime dateConvert;


        //    //if (cobxSrv.Text != "Pudełkowe")
        //    //{
        //    //    edition = "dig";
        //    //    srv = cobxSrv.Text;

        //    //    // dig - cyfrowa
        //    //    // box - pudełkowa
        //    //}
        //    //else
        //    //{
        //    //    edition = "box";
        //    //    srv = "";
        //    //}

        //    //if (chbxEnd.IsChecked == true)
        //    //{ endGame = 1; }
        //    //else
        //    //{ endGame = 0; }


        //    //if (dpPremiere.SelectedDate != null)
        //    //{
        //    //    dateConvert = DateTime.Parse(dpPremiere.Text);
        //    //    //  datePremiere = dateConvert.ToString("yyyy-MM-dd");
        //    //}
        //    //else
        //    //{
        //    //    dateConvert = DateTime.Parse("0001-01-01");
        //    //    // datePremiere = dateConvert.ToString("yyyy-MM-dd");
        //    //    //dpPremiere.SelectedDate = DateTime.Now.Date;
        //    //}


        //    //// dateConvert = DateTime.Parse("0001-01-01");// DateTime.Parse(dpBuy.Text);
        //    //dateBuy = dateConvert.ToString("yyyy-MM-dd");

        //    //datePremiere = dateConvert.ToString("yyyy-MM-dd");

        //    //  MessageBox.Show(dateConvert.ToString("yyyy-MM-dd"));



        //    //if (txtTitle.Text != "")
        //    //{
        //    //    if (cSQLite.Insert(title, type, platform, edition, srv, dlc, description, endGame, datePremiere, dateBuy))
        //    //    {
        //    //        MessageBox.Show("Gra została dodana.");
        //    //        // Close();
        //    //    }
        //    //    else
        //    //    {
        //    //        MessageBox.Show("Wystąpił błąd!  Nie dodano gry.");
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    MessageBox.Show("Podaj tytuł gry.", "", MessageBoxButton.OK, MessageBoxImage.Information);
        //    //}














        //}


    }
}
