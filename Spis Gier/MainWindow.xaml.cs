using Spis_Gier.Class;
using Spis_Gier.Models;
using Spis_Gier.UserControls;
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

namespace Spis_Gier
{
    public partial class MainWindow : Window
    {
        private SGSQLite cSQLite;

        UserControl ucWin = null;
  
        public MainWindow()
        {
            InitializeComponent();

            cSQLite = new SGSQLite();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;


            string quantityOfGamesDB    = cSQLite.IloscRow("SELECT COUNT(*) as id from games");
            string quantityOfEndGamesDB = cSQLite.IloscRow("SELECT COUNT(*) as id from games WHERE endGame ='1'");
               

            quantityOfGames.Content = "Ilość: " + quantityOfEndGamesDB + " /" + quantityOfGamesDB;

            //SELECT  count(*), id, typ, DATA FROM `punkty`
           // WHERE(DATA >= date_sub(NOW(), INTERVAL 3 HOUR))
              //GROUP BY id
            //ORDER BY count(id) ASC LIMIT 3

            // cSQLite = new SGSQLite();
            // cobxSort.SelectedIndex = 0;

            // numberOfGames.Text = "Ilość gier: " + gl.IloscRow("SELECT COUNT(*) as id from games");
            //numberOfEndGames.Text = "Ukończone: " + gl.IloscRow("SELECT COUNT(*) as id from games WHERE endGame ='1'");


             ucWin = new UCGameList(this);
             GridMain.Children.Add(ucWin);
        }


        //private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        //{
        //    ButtonCloseMenu.Visibility = Visibility.Visible;
        //    ButtonOpenMenu.Visibility = Visibility.Collapsed;
        //}

        //private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        //{
        //    ButtonCloseMenu.Visibility = Visibility.Collapsed;
        //    ButtonOpenMenu.Visibility = Visibility.Visible;
        //}


        private void ButtonCloseApp_Click(object sender, RoutedEventArgs e)
        {
            //ButtonCloseMenu.Visibility = Visibility.Collapsed;
            //ButtonOpenMenu.Visibility = Visibility.Visible;
            Application.Current.Shutdown();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }


        //private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    //UserControl usc = null;
        //    GridMain.Children.Clear();

        //    switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
        //    {

        //        case "ItemGamesList":
        //            ucWin = new UCGameList(this);
        //            GridMain.Children.Add(ucWin);
        //            break;

        //        case "ItemGamesAdd":
        //            ucWin = new UCGameAdd(this);
        //            GridMain.Children.Add(ucWin);
        //            break;

        //        //case "ItemGamesEdit":
        //        //   // ucWin = new UCGameEdit(this);
        //        //   // GridMain.Children.Add(ucWin);
        //        //    break;

        //        //default:
        //        //    // usc = new DefaultUserControl();
        //        //    //   GridMain.Children.Add(usc);

        //        //  //  ucWin = new UCGameAdd();
        //        //  //  GridMain.Children.Add(ucWin);
        //        //    break;
        //    }



        //}

     
    }
}