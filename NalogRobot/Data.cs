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

        public abstract List<Tax> GetTaxList(string term);
        public abstract Tax GetTaxById(int id);
        public abstract int CountTaxs();
        public abstract int InsertTax(Tax tax);
        public abstract bool UpdateTax(Tax tax);

        // RegNum, TempFile, DestFile, ImportState, Updated, Created
        protected virtual Tax GetTaxFromReader(IDataReader reader)
        {
            Tax tax = new Tax()
            {
                Id = reader.GetInt32(0),
                RegNum = reader.GetString(1),
                ImportState = (ImportState)reader.GetInt32(4),
                Created = reader.GetDateTime(6)
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