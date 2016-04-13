using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Test_grafiek
{
  class DBConnection
  {
    private MySqlConnection connection;
    private string server;
    private string database;
    private string uid;
    private string password;

    public DBConnection()
    {
      Initialize();
    }
    private void Initialize()
    {
      server = "localhost";
      database = "movedb";
      uid = "root";
      password = "Admin";
      string connectionString;
      connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

      connection = new MySqlConnection(connectionString);

    }
    private bool OpenConnection()
    {
      try
      {
        connection.Open();
        return true;
      }
      catch (MySqlException ex)
      {
        switch (ex.Number)
        {
          case 0:
            MessageBox.Show("Cannot connect to server");
            break;
          case 1045:
            MessageBox.Show("Invalid User/pass");
            break;
        }
        return false;
      }
    }
    private bool CloseConnection()
    {
      try
      {
        connection.Close();
        return true;
      }
      catch (MySqlException ex)
      {
        MessageBox.Show(ex.Message);
        return false;
      }

    }
    public void Insert()
    {

    }
    public void Update()
    {

    }
    public void Delete()
    {
      string query = "DELETE FROM tabel1 WHERE merk='nissan'";

      if (this.OpenConnection() == true)
      {
        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.ExecuteNonQuery();
        this.CloseConnection();
      }
    }
    public List<string>[] Select()
    {
      string query = "SELECT * FROM tabel1";

      //Create a list to store the result
      List<string>[] list = new List<string>[3];
      list[0] = new List<string>();
      list[1] = new List<string>();


      //Open connection
      if (this.OpenConnection() == true)
      {
        //Create Command
        MySqlCommand cmd = new MySqlCommand(query, connection);
        //Create a data reader and Execute the command
        MySqlDataReader dataReader = cmd.ExecuteReader();

        //Read the data and store them in the list
        while (dataReader.Read())
        {
          list[0].Add(dataReader["id"] + "");
          list[1].Add(dataReader["merk"] + "");

        }

        //close Data Reader
        dataReader.Close();

        //close Connection
        this.CloseConnection();

        //return list to be displayed
        return list;
      }
      else
      {
        return list;
      }
    }
    public int Count()
    {
      return 5;
    }
    public void Restore()
    {

    }
    public void Backup()
    {

    }


  } }