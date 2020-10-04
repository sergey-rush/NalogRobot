using System;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Data;

namespace NalogRobot
{
    public abstract class Data
    {
        protected string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["Sqlite"].ConnectionString; }
        }

        private static Data _instance;

        public static Data Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DataProvider();
                }

                return _instance;
            }
        }

        public abstract List<Tax> GetTaxList(string term, int limit, long sessionId);
        public abstract Tax GetTaxById(int id);
        public abstract int CountByRegNum(string regNum, ImportState importState);
        public abstract int InsertTax(Tax tax);
        public abstract bool UpdateTax(Tax tax);
        public abstract List<Session> GetSessions();
        public abstract int DeleteTaxListBySessionId(long sessionId);
        public abstract List<Session> GroupByDestFile();
        public abstract int DeleteTaxByDestFile(string destFile);
        public abstract void FinalizeSync(bool removeEmptyRecords);
        public abstract Tax GetTaxByRegNum(string regNum);

        // RegNum, TempFile, DestFile, ImportState, Updated, Created
        protected virtual Tax GetTaxFromReader(IDataReader reader)
        {
            Tax tax = new Tax()
            {
                Id = reader.GetInt32(0),
                RegNum = reader.GetString(1),
                ImportState = (ImportState)reader.GetInt32(4),
                Created = reader.GetDateTime(6),
                SessionId = reader.GetInt64(7)
            };

            if (reader[2] != DBNull.Value)
            {
                tax.TempFile = reader.GetString(2);
            }

            if (reader[3] != DBNull.Value)
            {
                tax.DestFile = reader.GetString(3);
            }

            if (reader[5] != DBNull.Value)
            {
                tax.Updated = reader.GetDateTime(5);
            }

            return tax;
        }

        /// <summary>
        /// Returns a collection of Tax objects with the data read from the input DataReader
        /// </summary>
        protected virtual List<Tax> GetTaxListFromReader(IDataReader reader)
        {
            List<Tax> taxList = new List<Tax>();
            while (reader.Read())
                taxList.Add(GetTaxFromReader(reader));
            return taxList;
        }
    }
}