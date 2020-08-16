using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
//using Microsoft.Data.Sqlite;
using System.Data.SQLite;

namespace Member3
{
    class DBConnection
    {
        public static SQLiteConnection connect()
        {
            var conString = new SQLiteConnectionStringBuilder();
            conString.DataSource = "./member1.db";

            SQLiteConnection connection = new SQLiteConnection(conString.ConnectionString);
            return connection;
        }
    }
}
