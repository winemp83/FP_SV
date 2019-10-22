using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using StundenVerwaltung.Model;

namespace StundenVerwaltung.Services
{
    public class SqliteDataProvider : IDataProvider
    {
        public const string FileName = "Personal.db";
        public string DataPath { get; private set; }
        protected string ConnectionString { get; set; }
        private DialogService ds = new DialogService();

        public SqliteDataProvider() {
            DataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FileName);
            SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder();
            builder.DataSource = DataPath;
            ConnectionString = builder.ToString();
            if (!File.Exists(DataPath)) {
                SQLiteConnection.CreateFile(DataPath);
                string sqlCreateTable = @"CREATE TABLE Personal (
Id varchar(40) Primary Key,
MId varchar(40) Not Null,
Name varchar(40)cv  Not Null,
VName varchar(40) Not Null,
Stunden varchar(40),
IstUnterrichtet bool)";
                ExeNonQueryCommand(sqlCreateTable);
            }
        }

        public bool Delete(IPersonal personal)
        {
            string sqlDelete = $@"DELETE FROM Personal WHERE Id='{personal.ID}'";
            return ExeNonQueryCommand(sqlDelete);
        }
        public List<IPersonal> GetAllPersonal()
        {
            var list = new List<IPersonal>();
            try {
                SQLiteConnection conn = new SQLiteConnection(ConnectionString);
                conn.Open();
                string sqlSelect = $@"SELECT * FROM Personal";
                SQLiteCommand cmd = new SQLiteCommand(sqlSelect, conn);
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                var dataTable = new DataTable();
                da.Fill(dataTable);
                foreach(DataRow row in dataTable.Rows) {
                    var personal = new Personal();
                    personal.ID = row["Id"].ToString();
                    personal.MID = row["MId"].ToString();
                    personal.Name = row["Name"].ToString();
                    personal.VName = row["VName"].ToString();
                    personal.Stunden = row["Stunden"] != null ? Convert.ToDouble(row["Stunden"]) : 0.0f;
                    personal.IstUnterrichtet = row["IstUnterrichtet"] != null ? (bool)(row["IstUnterrichtet"]) : false;
                    list.Add(personal);
                }
            }catch(Exception ex) {
                ds.Exception(ex);
            }
            return list;
        }
        public IPersonal GetPersonalById(string id)
        {
            Personal personal = null;
            try{
                SQLiteConnection conn = new SQLiteConnection(ConnectionString);
                conn.Open();
                string sqlSelect = $@"SELECT * FROM Personal WHERE Id='{id}'";
                SQLiteCommand cmd = new SQLiteCommand(sqlSelect, conn);
                SQLiteDataReader dr = cmd.ExecuteReader();
                if(dr.Read()){
                    personal.ID = dr["Id"].ToString();
                    personal.MID = dr["MId"].ToString();
                    personal.Name = dr["Name"].ToString();
                    personal.VName = dr["VName"].ToString();
                    personal.Stunden = dr["Stunden"] != null ? Convert.ToDouble(dr["Stunden"]) : 0.0f;
                    personal.IstUnterrichtet = dr["IstUnterrichtet"] != null ? (bool)(dr["IstUnterrichtet"]) : false;
                }
            }catch (Exception ex){
                ds.Exception(ex);
            }
            return personal;
        }
        public bool Insert(IPersonal personal)
        {
            string sqlInsert = $@"INSERT INTO Personal VALUES('{personal.ID}', '{personal.MID}', '{personal.Name}', '{personal.VName}', '{personal.Stunden.ToString()}', '{personal.IstUnterrichtet}')";
            return ExeNonQueryCommand(sqlInsert);
        }
        public bool Update(IPersonal personal)
        {
            string sqlUpdate = $@"UPDATE Personal SET MId='{personal.MID}', Name='{personal.Name}', VName='{personal.VName}', Stunden='{personal.Stunden.ToString()}', IstUnterrichtet='{personal.IstUnterrichtet}' WHERE Id='{personal.ID}'";
            return ExeNonQueryCommand(sqlUpdate);
        }

        private bool ExeNonQueryCommand(string sqlCommand) {
            bool isSuccess = false;
            try {
                SQLiteConnection conn = new SQLiteConnection(ConnectionString);
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(sqlCommand, conn);
                isSuccess = cmd.ExecuteNonQuery() > 0 ? true : false;
            }catch(Exception ex) {
                ds.Exception(ex);
            }
            return isSuccess;
        }
    }
}
