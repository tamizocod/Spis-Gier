using Spis_Gier.Class;
using Spis_Gier.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

using Spis_Gier.UserControls;

namespace Spis_Gier.UserControls
{
    public partial class UCGamesList : UserControl
    {
        private SGSQLite cSQLite;

        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;

        private MainWindow mWin;

        // public MainWindow aaa;

        public UCGamesList(MainWindow _mw)
        {
            InitializeComponent();

            cSQLite = new SGSQLite();
            mWin = _mw;
 
            SelectGames();

        }

        // wyświetlanie listy gier
        public void SelectGames()
        {
            List<Games> row = new List<Games>();

            DataSet ds = null;
            ds = cSQLite.Select("SELECT * FROM games ORDER BY title ASC");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string id       = ds.Tables[0].Rows[i].ItemArray[0].ToString();
                string title    = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                string type     = ds.Tables[0].Rows[i].ItemArray[2].ToString();
                string platform = ds.Tables[0].Rows[i].ItemArray[3].ToString();
                string edition  = ds.Tables[0].Rows[i].ItemArray[4].ToString();
                string srv;   //  = ds.Tables[0].Rows[i].ItemArray[4].ToString()
                string datePremiere;
                string endGame = ds.Tables[0].Rows[i].ItemArray[6].ToString();


                if (edition == "box")
                { srv = "Pudełkowe"; }
                else
                { srv = ds.Tables[0].Rows[i].ItemArray[5].ToString(); }

                DateTime dateConvert = DateTime.Parse(ds.Tables[0].Rows[i].ItemArray[9].ToString());
                datePremiere = dateConvert.ToString("dd.MM.yyyy");

                row.Add(new Games() { Lp = i + 1, Title = title, Type = type, Platform = platform, Edition = srv, DatePremiere = datePremiere, EndGame = endGame, Id = id });

            } // end for
            gamesList.ItemsSource = row;

        }// end list


        private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {

            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                gamesList.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            gamesList.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));




        }


        public class SortAdorner : Adorner
        {
            private static Geometry ascGeometry =
                Geometry.Parse("M 0 4 L 3.5 0 L 7 4 Z");

            private static Geometry descGeometry =
                Geometry.Parse("M 0 0 L 3.5 4 L 7 0 Z");

            public ListSortDirection Direction { get; private set; }

            public SortAdorner(UIElement element, ListSortDirection dir)
                : base(element)
            {
                this.Direction = dir;
            }

            protected override void OnRender(DrawingContext drawingContext)
            {
                base.OnRender(drawingContext);

                if (AdornedElement.RenderSize.Width < 20)
                    return;

                TranslateTransform transform = new TranslateTransform
                    (
                        AdornedElement.RenderSize.Width - 15,
                        (AdornedElement.RenderSize.Height - 5) / 2
                    );
                drawingContext.PushTransform(transform);

                Geometry geometry = ascGeometry;
                if (this.Direction == ListSortDirection.Descending)
                    geometry = descGeometry;
                drawingContext.DrawGeometry(Brushes.Black, null, geometry);

                drawingContext.Pop();
            }
        }

       // UserControl ucWin = null;
        public void GamesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //string aaa = (gamesList.SelectedIndex = gamesList.Items.Count ).ToString();
            dynamic selectedItem = gamesList.SelectedItems[0];
            //selectedItem.Id
            // MessageBox.Show(selectedItem.Id);


            UCGameEdit ucGE = new UCGameEdit(mWin, selectedItem.Id);

            mWin.GridMain.Children.Add(ucGE);

           // _current.PanelMain.Children.Add(u22);

            //_current = f;
            // UserControl2 u2 = new UserControl2();
            // _current.panelMain.Controls.Add(u2);

            //  this.Hide();

            // UserControl2 u2 = new UserControl2();

            // MainForm f1 = new MainForm();
            // f1.panelMain.Controls.Add(u2)







            //UserControl usc = null;
            // aaa.GridMain.Children.Clear();


            // MainWindow
            // aaa.GridMain.Children.Clear();

            //ucWin = new UCGameAdd();
            //       GridMain.Children.Add(ucWin);

            // ucWin = new UCGameEdit(selectedItem.Id);
            // aaa.GridMain.Children.Add(ucWin);
            //  GridMain.Children.Add(ucWin);
            //aaa.GridMain.Children.Add(ucWin);

            //  aaa.t ttt();
        }
    }
}
