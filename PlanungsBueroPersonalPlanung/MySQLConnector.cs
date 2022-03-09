using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace PlanungsBueroPersonalPlanung
{
    public class MySQLConnector
    {

        private MySqlConnection dbVerbindung;

        //const string errorMsg = "Konnte keine DB-Verbindung aufbauen: ";
        public MySQLConnector(string dbname)
        {
            this._DBName = dbname;
        }


        private string _DBHost = "localhost12";

        public string DBHost
        {
            get { return _DBHost; }
            set { _DBHost = value; }
        }

        private string _DBUser = "root";

        public string DBUser
        {
            get { return _DBUser; }
            set { _DBUser = value; }
        }

        private string _DBPassword = "";
            
        public string DBPassword
        {
            get { return _DBPassword; }
            set { _DBPassword = value; }
        }

        private string _DBName = "";  

        public string DBName
        {
            get { return _DBName; }
            set { _DBName = value; }
        }

        private string ConnectionString
        {
            get
            {
                var stringBuilder = new MySqlConnectionStringBuilder();
                stringBuilder.Server = _DBHost;
                stringBuilder.UserID = _DBUser;
                stringBuilder.Password = _DBPassword;
                stringBuilder.Database = _DBName;

                return stringBuilder.ConnectionString;
            }
        }


        private MySqlCommand OpenConnection()
        {
            
            MySqlCommand mySQLComm = new MySqlCommand();

            dbVerbindung = new MySqlConnection(this.ConnectionString);
           
                dbVerbindung.Open();
                mySQLComm.Connection = dbVerbindung;
                
         
            return mySQLComm;

        }

        public void CloseConnection()
        {
            dbVerbindung.Close();
        }

        public string TestConnection()
        {
            String msg = "DB-Verbindung war erfolgreich";
            String sql_request = "show tables;";
            try
            {
                MySqlCommand mySQLComm = this.OpenConnection();
                mySQLComm.CommandText = sql_request;
                mySQLComm.ExecuteReader();
                this.CloseConnection();
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }

        public int executeNonQuery(String dml)
        {
            int retValue = -1;
         
                MySqlCommand sqlcommand = this.OpenConnection();
                sqlcommand.CommandText = dml;
                retValue = sqlcommand.ExecuteNonQuery();
                this.CloseConnection();
           
            return retValue;
        }

        public MySqlDataReader executeQuery(String sql)
        {
            MySqlDataReader dataReader = null;
            
            
                MySqlCommand sqlcommand = this.OpenConnection();
                sqlcommand.CommandText = sql;
                dataReader = sqlcommand.ExecuteReader();
     
            
                
            
           
            return dataReader;
        }




    }
}
