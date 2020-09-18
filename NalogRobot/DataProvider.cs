using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace NalogRobot
{
    public class DataProvider: Data
    {
        private SQLiteConnection cn;
        public DataProvider()
        {
            cn = new SQLiteConnection(ConnectionString);
            cn.Open();
        }

        public override List<Tax> GetTaxList(string term, int limit, long sessionId)
        {
            SQLiteCommand cmd = new SQLiteCommand("SELECT Id, RegNum, TempFile, DestFile, ImportState, Updated, Created, SessionId FROM tax WHERE SessionId = @SessionId AND RegNum LIKE @RegNum ORDER BY Id LIMIT @Limit;", cn);
            cmd.Parameters.Add("@RegNum", DbType.String).Value = $"%{term}%";
            cmd.Parameters.Add("@SessionId", DbType.UInt64).Value = sessionId;
            cmd.Parameters.Add("@Limit", DbType.Int32).Value = limit;
            return GetTaxListFromReader(cmd.ExecuteReader());
        }

        public override Tax GetTaxById(int id)
        {
            SQLiteCommand cmd = new SQLiteCommand("SELECT Id, FileName, Created FROM Tax ORDER BY Created DESC LIMIT 100;", cn);
            return GetTaxFromReader(cmd.ExecuteReader());
        }

        public override int CountTaxs()
        {
            SQLiteCommand cmd = new SQLiteCommand("SELECT COUNT(*) FROM Tax;", cn);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public override int InsertTax(Tax tax)
        {
            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO tax (RegNum, ImportState, Created, SessionId) VALUES (@RegNum, @ImportState, @Created, @SessionId);", cn);
            cmd.Parameters.Add("@RegNum", DbType.String).Value = tax.RegNum;
            cmd.Parameters.Add("@ImportState", DbType.Int32).Value = (int)tax.ImportState;
            cmd.Parameters.Add("@Created", DbType.String).Value = tax.Created;
            cmd.Parameters.Add("@SessionId", DbType.UInt64).Value = tax.SessionId;
            cmd.ExecuteNonQuery();
            return (int)cn.LastInsertRowId;
        }

        public override bool UpdateTax(Tax tax)
        {
            SQLiteCommand cmd = new SQLiteCommand("UPDATE Tax SET TempFile = @TempFile, DestFile = @DestFile, ImportState = @ImportState, Updated = @Updated WHERE Id = @Id;", cn);
            cmd.Parameters.Add("@Id", DbType.Int32).Value = tax.Id;
            cmd.Parameters.Add("@TempFile", DbType.String).Value = tax.TempFile;
            cmd.Parameters.Add("@DestFile", DbType.String).Value = tax.DestFile;
            cmd.Parameters.Add("@ImportState", DbType.Int32).Value = (int)tax.ImportState;
            cmd.Parameters.Add("@Updated", DbType.String).Value = tax.Updated;
            var retVal = cmd.ExecuteNonQuery();
            return retVal == 1;
        }

        public override int DeleteTaxListBySessionId(long sessionId)
        {
            SQLiteCommand cmd = new SQLiteCommand("DELETE FROM tax WHERE SessionId = @SessionId;", cn);
            
            cmd.Parameters.Add("@SessionId", DbType.UInt64).Value = sessionId;
            cmd.ExecuteNonQuery();
            return cmd.ExecuteNonQuery();
        }

        public override List<Session> GetSessions()
        {
            List<Session> sessions = new List<Session>();
            SQLiteCommand cmd = new SQLiteCommand("SELECT DISTINCT SessionId FROM tax;", cn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Session session = new Session();
                session.SessionId = reader.GetInt64(0);
                session.Name = new DateTime(session.SessionId).ToString("F");
                sessions.Add(session);
            }
            return sessions;
        }
    }
}