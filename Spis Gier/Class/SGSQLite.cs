using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite;
using System.Data;
using System.Windows.Controls;
using System.Windows;


namespace Spis_Gier.Class
{
    class SGSQLite
    {

        // Obiekty SQLite :
        SQLiteConnection con;
        SQLiteCommand com;
        SQLiteDataReader dRead;
        SQLiteDataAdapter dAdap;
        DataSet dSet;


        public SGSQLite()
        {
            con = new SQLiteConnection("Data Source=dbGames.db;Version=3;");

            // createNewDatabase();
            // connectToDatabase();

            // createTable();
            // fillTable();
            // delTable();
        }

        // Creates an empty database file
        void createNewDatabase()
        {
            SQLiteConnection.CreateFile("dbGames.db");
        }

        // Creates a table named 'games' 
        void createTable()
        {
            OpenConSQLite();
            string sql = " CREATE TABLE games( id  INTEGER   PRIMARY KEY AUTOINCREMENT,    title  VARCHAR(100),    type   VARCHAR(20),    platform     VARCHAR(20),    edition   VARCHAR(20), srv VARCHAR(25),    endGame  INTEGER,  datePremiere DATE, dateBuy  DATE  )";

            com = new SQLiteCommand(sql, con);
            com.ExecuteNonQuery();

            con.Close();
        }

        // Creates a table named 'games' 
        //void delTable()
        //{
        //    OpenConSQLite();
        //    string sql = " DROP TABLE games_t ";

        //    // = "CREATE TABLE games(id integer PRIMARY KEY AUTOINCREMENT, title VARCHAR (100), type VARCHAR(20), platform VARCHAR(20), edition VARCHAR(20), endGame BOOLEAN, datePremiere DATE, dateBuy DATE)";
        //    com = new SQLiteCommand(sql, con);
        //    com.ExecuteNonQuery();

        //    con.Close();
        //}


        // Inserts some values in the highscores table.
        //void fillTable()
        //{
        //    string sql;
        //    OpenConSQLite();

        //    sql = "insert into games_t (id, title) values (1, 'Gra 01')";
        //    com = new SQLiteCommand(sql, con);
        //    com.ExecuteNonQuery();

        //    sql = "insert into games_t (id, title) values (2, 'Gra 043y587234785')";
        //    com = new SQLiteCommand(sql, con);
        //    com.ExecuteNonQuery();

        //    sql = "insert into games_t (id, title) values (3, 'Gra 0dsgfaghf')";
        //    com = new SQLiteCommand(sql, con);
        //    com.ExecuteNonQuery();

        //    con.Close();
        //}


        // Creates a connection with our database file.
        //void connectToDatabase()
        //{
        //    return con = new SQLiteConnection("Data Source=SpisGier.db;Version=3;");
        //}


        // Otwarte połączenie do bazy danych
        public bool OpenConSQLite()
        {
            try
            {
                con.Open();
                return true;
            }
            catch (SQLiteException ex)
            {
                return false;
            }
        }


        // Select: Wyświetlanie Listy 
        public DataSet Select(string sql)
        {
            try
            {
                DataSet dSet;
                dSet = new DataSet();
                dAdap = new SQLiteDataAdapter();
                OpenConSQLite();
                com = new SQLiteCommand(sql, con);

                dAdap.SelectCommand = com;
                dAdap.Fill(dSet);

                con.Close();
                return dSet;
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
                return null;
            }
        }


        //    //List<string>[] list = null;

        //    //// Tworzenie listy, aby zapisać wynik
        //    //list = new List<string>[9];
        //    //list[0] = new List<string>();
        //    //list[1] = new List<string>();
        //    //list[2] = new List<string>();
        //    //list[3] = new List<string>();
        //    //list[4] = new List<string>();
        //    //list[5] = new List<string>();
        //    //list[6] = new List<string>();
        //    //list[7] = new List<string>();
        //    //list[8] = new List<string>();

        //    //// Open connection
        //    //if (OpenConSQLite() == true)
        //    //{
        //    //    com = con.CreateCommand();
        //    //    com.CommandText = query;
        //    //    dRead = com.ExecuteReader();

        //    //    while (dRead.Read())
        //    //    {
        //    //        list[0].Add(dRead["id"].ToString());
        //    //        list[1].Add(dRead["title"].ToString());
        //    //        list[2].Add(dRead["type"].ToString());
        //    //        list[3].Add(dRead["platform"].ToString());
        //    //        list[4].Add(dRead["edition"].ToString());
        //    //        list[5].Add(dRead["srv"].ToString());
        //    //        list[6].Add(dRead["endGame"].ToString());
        //    //        list[7].Add(dRead["datePremiere"].ToString());
        //    //        list[8].Add(dRead["dateBuy"].ToString());
        //    //    }

        //    //    // Close 
        //    //    dRead.Close();
        //    //    con.Close();

        //    //    return list;
        //    //}
        //    //else
        //    //{
        //    //    return list;
        //    //}

        //} // end Select




        // Ilosc: Wyświetlanie liczby wierszy   
        public string IloscRow(string query)
        {
            // query = "SELECT * from games ";
            string liczba;

            OpenConSQLite();

            com = con.CreateCommand();
            com.CommandText = query;
            dRead = com.ExecuteReader();

            dRead.Read();
            liczba = dRead["id"].ToString();

            dRead.Close();
            con.Close();

            return liczba;
        }


        // Insert:  dodawanie nowego rekordu z grą
        public bool Insert(string title, string type, string platform, string edition, string srv, int dlc, string description, int endGame, string datePremiere, string dateBuy)
        {
            try
            {
                // Open connection
                if (OpenConSQLite() == true)
                {
                    com = con.CreateCommand();
                    com.CommandText = "INSERT INTO games (id, title, type, platform, edition, srv, dlc, description, endGame, datePremiere, dateBuy ) VALUES (NULL, '" + title + "', '" + type + "', '" + platform + "', '" + edition + "', '" + srv + "', '" + dlc + "', '" + description + "','" + endGame + "', '" + datePremiere + "', '" + dateBuy + "');";
                    com.ExecuteNonQuery();

                    // Close connection
                    con.Close();

                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (SQLiteException ex)
            {
                return false;  // ex.Message;
            }

        }


        // Insert:  dodawanie nowego rekordu z grą
        public bool Update(string title, string type, string platform, string edition, string srv, int dlc, string description, int endGame, string datePremiere, string dateBuy, string id)
        {
            try
            {
                // Open connection
                if (OpenConSQLite() == true)
                {
                    com = con.CreateCommand();
                    com.CommandText = "UPDATE games SET title = '" + title + "' , type = '" + type + "', platform = '" + platform + "', edition ='" + edition + "', srv = '" + srv + "', dlc = '" + dlc + "', description =  '" + description + "', endGame = '" + endGame + "', datePremiere = '" + datePremiere + "', dateBuy = '" + dateBuy + "'  WHERE id = '"+id+"'";
                    com.ExecuteNonQuery();

                    // Close connection
                    con.Close();

                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (SQLiteException ex)
            {
                return false;  // ex.Message;
            }

        }


        // Delete games
        public bool Delete(string tab, string id)
        {
            if (OpenConSQLite() == true)
            {
                com = con.CreateCommand();
                com.CommandText = "DELETE FROM " + tab + " WHERE id = " + id + " ";
                com.ExecuteNonQuery();
                con.Close();

                return true;
            }
            else { return false; }
        }


        //================== ComboBox ========================================
        // Select: Wyświetlanie Listy 
        public DataSet SelectCB(string sql)
        {
            try
            {

                dSet  = new DataSet();
                dAdap = new SQLiteDataAdapter();
                com   = new SQLiteCommand(sql, con);

                con.Open();
                dAdap.SelectCommand = com;
                dAdap.Fill(dSet);

                con.Close();
                return dSet;

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
                return null;
            }
        }


        public void SetComBox(ComboBox cb, string type, string sql)
        {
            // Open connection
            if (OpenConSQLite() == true)
            {
                com = con.CreateCommand();
                com.CommandText = sql;
                dRead = com.ExecuteReader();

                int i = 0;
                cb.SelectedIndex = 0;

                while (dRead.Read())
                {
                    if (type == dRead["name"].ToString())
                    {
                        cb.SelectedIndex = i;
                        //  MessageBox.Show("Id: " + i + ", typ: " + dRead["name"].ToString());
                        break;
                    }
                    else { i++; }
                    //i++;
                }
                // Close 
                dRead.Close();
                con.Close();
            }
        } // end Select

      

        public void GetStats(out int numRows)
        {
            try
            {
                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"SELECT COUNT(*) as cnt from User";
                    cmd.CommandType = System.Data.CommandType.Text;

                    SQLiteDataReader reader;
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        numRows = Convert.ToInt32(reader["cnt"]);
                        return;
                    }

                    numRows = 0;
                }
            }
            catch (Exception exc)
            {
               // DoException(exc);
                numRows = 0;
            }
        }

    }
}
