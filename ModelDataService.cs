using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

using DataAccess;
using System.Data;
using System.Data.Common;
using AutoResourceRecoveryControlService.Models;

namespace AutoResourceRecoveryControlService
{
    public class ModelDataService
    {

        static string GetDB(bool RealData)
        {
            return "Server=tcp:kkpipk3ejg.database.windows.net,1433;Database=AutoScaleDB;User ID={0};Password={1};Trusted_Connection=False;Encrypt=True;Connection Timeout=30";
        }

        public static Machine GetMachine(string id, bool RealData)
        {
            string db = GetDB(RealData);

            Machine result = new Machine();

            IConnection con = new ConnectionLocator(db).connection;
            using (DbConnection dbCon = con.Connect())
            {
                try
                {
                    dbCon.Open();
                    using (DbDataAdapter adapter = con.factory.CreateDataAdapter())
                    {
                        StringBuilder sql = new StringBuilder();
                        sql.AppendFormat("EXEC GET_DETAIL_MACHINE @MACHINE_ID='{0}'", id);

                        adapter.SelectCommand = new CommandManager().CreateQueryCommand(dbCon, sql.ToString());

                        DataSet ds = new DataSet();
                        adapter.Fill(ds, "MS_MACHINE");

                        result.Name = "";

                        if (ds.Tables["MS_MACHINE"].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables["MS_MACHINE"].Rows)
                            {
                                result.Id = id;
                                result.Name = Convert.ToString(row["NAME"]);
                            }
                        }
                    }

                }
                catch
                {
                    return null;
                }
            }
            if (result.Name != "")
            {
                return result;
            }
            return null;
        }

        public static List<Machine> GetMachines(bool RealData)
        {
            string db = GetDB(RealData);

            List<Machine> MachineList = new List<Machine>();

            IConnection con = new ConnectionLocator(db).connection;
            using (DbConnection dbCon = con.Connect())
            {
                try
                {
                    dbCon.Open();
                    using (DbDataAdapter adapter = con.factory.CreateDataAdapter())
                    {
                        StringBuilder sql = new StringBuilder();
                        sql.AppendFormat("EXEC GET_LIST_MACHINE");

                        adapter.SelectCommand = new CommandManager().CreateQueryCommand(dbCon, sql.ToString());

                        DataSet ds = new DataSet();
                        adapter.Fill(ds, "MS_MACHINE_LIST");

                        if (ds.Tables["MS_MACHINE_LIST"].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables["MS_MACHINE_LIST"].Rows)
                            {
                                if (Convert.ToString(row["FLG0"]) != "0")
                                {
                                    Machine machine = new Machine();
                                    machine.Id = Convert.ToString(row["MACHINE_ID"]);
                                    machine.Name = Convert.ToString(row["MACHINE_NAME"]);
                                    MachineList.Add(machine);
                                }
                            }
                        }
                    }
                }
                catch
                {
                    return null;
                }
            }
            if (MachineList.Count() > 0) return MachineList;
            return null;
        }

        public static PointExchange GetPointExchange(string id, bool RealData)
        {
            string db = GetDB(RealData);

            PointExchange result = new PointExchange();

            IConnection con = new ConnectionLocator(db).connection;
            using (DbConnection dbCon = con.Connect())
            {
                try
                {
                    dbCon.Open();
                    using (DbDataAdapter adapter = con.factory.CreateDataAdapter())
                    {
                        StringBuilder sql = new StringBuilder();
                        sql.AppendFormat("EXEC MC_GET_DETAIL_POINT_EXCHANGE @ID='{0}'", id);

                        adapter.SelectCommand = new CommandManager().CreateQueryCommand(dbCon, sql.ToString());

                        DataSet ds = new DataSet();
                        adapter.Fill(ds, "TBL_POINT_EXCHANGE");


                        if (ds.Tables["TBL_POINT_EXCHANGE"].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables["TBL_POINT_EXCHANGE"].Rows)
                            {
                                result.Id = id;
                                result.RecodeDate = Convert.ToDateTime(row["RECODE_DATE"]);
                                result.PublishedTime = Convert.ToInt32(row["PUBLISHED_TIME"]);
                                result.MemberNo = Convert.ToString(row["MEMBER_NO"]);
                                result.Point = Convert.ToInt32(row["POINT"]);
                                result.Machine = new Machine();
                                result.Machine.Id = Convert.ToString(row["MACHINE_ID"]);
                                result.Machine.Name = Convert.ToString(row["MACHINE_NAME"]);
                            }
                        }
                    }

                }
                catch
                {
                    return null;
                }
            }
            if ((result.MemberNo != null) && (result.MemberNo != ""))
            {
                return result;
            }
            return null;
        }

        public static List<PointExchange> GetPointExchanges(string MemberNo, bool RealData)
        {
            string db = GetDB(RealData);

            List<PointExchange> result = new List<PointExchange>();

            IConnection con = new ConnectionLocator(db).connection;
            using (DbConnection dbCon = con.Connect())
            {
                try
                {
                    dbCon.Open();
                    using (DbDataAdapter adapter = con.factory.CreateDataAdapter())
                    {
                        StringBuilder sql = new StringBuilder();
                        sql.AppendFormat("EXEC MC_GetPointExchangesByMemberNo @MEMBER_NO='{0}'", MemberNo);

                        adapter.SelectCommand = new CommandManager().CreateQueryCommand(dbCon, sql.ToString());

                        DataSet ds = new DataSet();
                        adapter.Fill(ds, "MC_GetPointExchangesByMemberNo");


                        if (ds.Tables["MC_GetPointExchangesByMemberNo"].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables["MC_GetPointExchangesByMemberNo"].Rows)
                            {
                                PointExchange d = new PointExchange();

                                d.Id = Convert.ToString(row["ID"]); ;
                                d.RecodeDate = Convert.ToDateTime(row["RECODE_DATE"]);
                                d.MemberNo = Convert.ToString(row["MEMBER_NO"]);
                                d.Point = Convert.ToInt32(row["POINT"]);
                                d.Machine = new Machine();
                                d.Machine.Id = Convert.ToString(row["MACHINE_ID"]);
                                d.Machine.Name = Convert.ToString(row["MACHINE_NAME"]);
                                d.PublishedTime = Convert.ToInt32(row["PUBLISHED_TIME"]);
                                result.Add(d);
                            }
                        }
                    }

                }
                catch
                {
                    return null;
                }
            }
            if (result.Count > 0)
            {
                return result;
            }
            return null;

        }

        public static List<AutoScale> GetAutoScales(string MemberNo, bool RealData)
        {
            string db = GetDB(RealData);

            List<AutoScale> result = new List<AutoScale>();

            IConnection con = new ConnectionLocator(db).connection;
            using (DbConnection dbCon = con.Connect())
            {
                try
                {
                    dbCon.Open();
                    using (DbDataAdapter adapter = con.factory.CreateDataAdapter())
                    {
                        StringBuilder sql = new StringBuilder();
                        sql.AppendFormat("EXEC MC_GetAutoScalesByMemberNo @MEMBER_NO='{0}'", MemberNo);

                        adapter.SelectCommand = new CommandManager().CreateQueryCommand(dbCon, sql.ToString());

                        DataSet ds = new DataSet();
                        adapter.Fill(ds, "MC_GetAutoScalesByMemberNo");


                        if (ds.Tables["MC_GetAutoScalesByMemberNo"].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables["MC_GetAutoScalesByMemberNo"].Rows)
                            {
                                AutoScale d = new AutoScale();

                                d.Id = Convert.ToString(row["ID"]); ;
                                d.RecodeDate = Convert.ToDateTime(row["RECODE_DATE"]);
                                d.MemberNo = Convert.ToString(row["MEMBER_NO"]);
                                d.Scale = Convert.ToDouble(row["SCALE"]);
                                d.Point = Convert.ToInt32(row["POINT"]);
                                d.TotalPoint = Convert.ToInt32(row["TOTAL_POINT"]);
                                d.Unit = Convert.ToString(row["UNIT"]);
                                d.Machine = new Machine();
                                d.Machine.Id = Convert.ToString(row["MACHINE_ID"]);
                                d.Machine.Name = Convert.ToString(row["MACHINE_NAME"]);
                                d.Item = new Item();
                                d.Item.Id = Convert.ToString(row["ITEM_CODE"]);
                                d.Item.Name = Convert.ToString(row["ITEM_NAME"]);
                                d.WeightCheck = Convert.ToString(row["WEIGHT_CHECK"]);
                                d.TimeCheck = Convert.ToString(row["TIME_CHECK"]);
                                result.Add(d);
                            }
                        }
                    }

                }
                catch
                {
                    return null;
                }
            }
            if (result.Count > 0)
            {
                return result;
            }
            return null;
        }

        public static Member GetMember(string MemberNo, bool RealData)
        {
            string db = GetDB(RealData);

            IConnection con = new ConnectionLocator(db).connection;
            using (DbConnection dbCon = con.Connect())
            {
                try
                {
                    dbCon.Open();
                    using (DbDataAdapter adapter = con.factory.CreateDataAdapter())
                    {
                        StringBuilder sql = new StringBuilder();
                        sql.AppendFormat("EXEC GET_DETAIL_MEMBER @MEMBER_NO='{0}'", MemberNo);

                        adapter.SelectCommand = new CommandManager().CreateQueryCommand(dbCon, sql.ToString());

                        DataSet ds = new DataSet();
                        adapter.Fill(ds, "GET_DETAIL_MEMBER");

                        Member result = new Member();

                        if (ds.Tables["GET_DETAIL_MEMBER"].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables["GET_DETAIL_MEMBER"].Rows)
                            {


                                result.MemberNo = Convert.ToString(row["MEMBER_NO"]); ;
                                result.Point = Convert.ToInt32(row["POINT"]);
                                result.ElsePoint = result.Point - Convert.ToInt32(row["TOTAL_POINT"]);
                            }
                        }
                        return result;
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        public static int MembersCardChanged(string OldMemberNo, string NewMemberNo, bool RealData)
        {
            string db = GetDB(RealData);
            int result = 0;

            using (DbConnection dbCon = new ConnectionLocator(db).connection.Connect())
            {
                try
                {
                    dbCon.Open();
                    DbTransaction Trans = dbCon.BeginTransaction();

                    using (DbCommand cmd = new CommandManager().CreateProcedureCommand(dbCon, "MC_MEMBERS_CARD_CHANGED"))
                    {
                        cmd.Transaction = Trans;
                        DbParameter param;

                        param = cmd.CreateParameter();
                        param.ParameterName = "@OLD_MEMBER_NO";
                        param.DbType = DbType.String;
                        param.Direction = ParameterDirection.Input;
                        param.Size = 32;
                        cmd.Parameters.Add(param);

                        param = cmd.CreateParameter();
                        param.ParameterName = "@NEW_MEMBER_NO";
                        param.DbType = DbType.String;
                        param.Direction = ParameterDirection.Input;
                        param.Size = 32;
                        cmd.Parameters.Add(param);

                        param = cmd.CreateParameter();
                        param.ParameterName = "@RESULT";
                        param.DbType = DbType.Int16;
                        param.Direction = ParameterDirection.Output;
                        param.Size = 4;
                        cmd.Parameters.Add(param);
                        
                        cmd.Parameters["@OLD_MEMBER_NO"].Value = OldMemberNo;
                        cmd.Parameters["@NEW_MEMBER_NO"].Value = NewMemberNo;
                        cmd.ExecuteNonQuery();

                        result = Convert.ToInt32(cmd.Parameters["@RESULT"].Value);

                        if (result == 1)
                        {
                            Trans.Commit();
                            return result;
                        }
                        else
                        {
                            Trans.Rollback();
                            return result;
                        }
                    }
                }
                catch(Exception error)
                {
                    return result;
                }
            }


        }
    }
}
