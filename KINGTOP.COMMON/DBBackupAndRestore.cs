using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace KingTop.Common
{
    public class DBBackupAndRestore
    {
        static void Main(string[] args)
        {
        }

        public string GetDataBaseName()
        {
            string connStr = KingTop.Common.SQLHelper.ConnectionStringLocalTransaction;
            string[] arrConn = connStr.Split(';');
            for (int i = 0; i < arrConn.Length; i++)
            {
                string[] itemArr = arrConn[i].Split('=');

                if (itemArr[0].ToLower() == "database")
                {
                    return itemArr[1];
                }

            }

            return "";
        }

        public int BackupDB(string[] bakFile)
        {
            int re = 0;
            string dbName = GetDataBaseName();
            //bool bak1 = BackUpDb(dbName, bakFile[1], bakSetName);
            bool bak1 = DataBackupConfigDB(bakFile[1], dbName);
            if (bak1)
            {
                FileInfo file = new FileInfo(bakFile[1]);
                bak1 = file.Exists;
                if (bak1)
                {
                    Utils.XmlUpdate(System.Web.HttpContext.Current.Server.MapPath("~/SysAdmin/Configuraion/DataBaseManage.config"), "DataBaseManageConfig/IsSameServer", "", "1");
                    re = 1;
                }
            }
            if (!bak1)
            {
                bak1 = DataBackupConfigDB(bakFile[0], dbName);
                if (!bak1)
                {
                    re = 3;
                }
                else
                {
                    re = 2;
                    Utils.XmlUpdate(System.Web.HttpContext.Current.Server.MapPath("~/SysAdmin/Configuraion/DataBaseManage.config"), "DataBaseManageConfig/IsSameServer", "", "0");
                }
            }

            return re;
        }
        
        /// <summary>        
        /// 备份数据库
        /// </summary>          
        /// <param name="backupFolder">备份文件路径</param>         
        /// <returns></returns>          
        public bool DataBackupConfigDB(string backupPath, string dbName)
        {
            //获取配置文件中sql数据库名   
            string sql;
            //创建连接对象            
            SqlConnection conn = new SqlConnection(SQLHelper.ConnectionStringLocalTransaction);
            conn.Open();//打开数据库连接

            #region 删除逻辑备份设备，但不会删掉备份的数据库文件          
            //string procname = "sp_dropdevice";
            //SqlCommand sqlcmd1 = new SqlCommand(procname, conn);
            //sqlcmd1.CommandType = CommandType.StoredProcedure;
            //SqlParameter sqlpar = new SqlParameter();
            //sqlpar = sqlcmd1.Parameters.Add("@logicalname", SqlDbType.VarChar, 20);
            //sqlpar.Direction = ParameterDirection.Input;
            //sqlpar.Value = dbName;
            //try//如果逻辑设备不存在，略去错误            
            //{
            //    sqlcmd1.ExecuteNonQuery();
            //}
            //catch
            //{
            //    //MessageBox.Show("错误的备份文件目录");
            //}
            #endregion

            #region 创建逻辑备份设备  
            //procname = "sp_addumpdevice";
            //SqlCommand sqlcmd2 = new SqlCommand(procname, conn);
            //sqlcmd2.CommandType = CommandType.StoredProcedure;
            //sqlpar = sqlcmd2.Parameters.Add("@devtype", SqlDbType.VarChar, 20);
            //sqlpar.Direction = ParameterDirection.Input;
            //sqlpar.Value = "disk";
            //sqlpar = sqlcmd2.Parameters.Add("@logicalname", SqlDbType.VarChar, 20);//逻辑设备名  
            //sqlpar.Direction = ParameterDirection.Input;
            //sqlpar.Value = dbName;
            //sqlpar = sqlcmd2.Parameters.Add("@physicalname", SqlDbType.NVarChar, 260);//物理设备名  
            //sqlpar.Direction = ParameterDirection.Input;
            //sqlpar.Value = backupPath;
            //try
            //{
            //    int i = sqlcmd2.ExecuteNonQuery();
            //}
            //catch (Exception err)
            //{
            //    string str = err.Message;
            //}
            #endregion

            //备份数据库到指定的数据库文件(完全备份)  
            sql = "BACKUP DATABASE [" + dbName + "] TO DISK='" + backupPath + "' WITH INIT";
            //System.Web.HttpContext.Current.Response.Write(sql);
            //System.Web.HttpContext.Current.Response.End();
            SqlCommand sqlcmd3 = new SqlCommand(sql, conn);
            sqlcmd3.CommandType = CommandType.Text;
            try
            {
                sqlcmd3.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                string str = err.Message;
                conn.Close();
                return false;
            }
            conn.Close();//关闭数据库连接         
            return true;
        }

        private string GetConnection(string PhyFilePath)
        {
            string connStr = KingTop.Common.SQLHelper.ConnectionStringLocalTransaction;
            string dbName = string.Empty;
            string dbServer = string.Empty;
            string[] arrConn = connStr.Split(';');
            for (int i = 0; i < arrConn.Length; i++)
            {
                string[] itemArr = arrConn[i].Split('=');

                if (itemArr[0].ToLower() == "database")
                {
                    dbName= itemArr[1];
                }
                else if (itemArr[0].ToLower() == "server")
                {
                    dbServer = itemArr[1];
                }

            }

            string dbUserName = Utils.XmlRead(PhyFilePath, "DataBaseManageConfig/UserName", "");//数据库连接用户名
            string dbPassword = Utils.XmlRead(PhyFilePath, "DataBaseManageConfig/Password", "");//数据库连接密码

            string connStr1 = "server=" + dbServer + ";database=" + dbName + ";uid=" + dbUserName + ";pwd=" + dbPassword + "";

            return connStr1;
        }

        /// <summary>     
        /// 还原数据库文件   
        /// </summary>   
        /// <param name="dbFile">数据库备份文件（含路径）</param>   
        /// <returns></returns>   
        public bool RestoreDB(string dbFile)
        {
            //sql数据库名  
            string dbName = GetDataBaseName();

            string FilePath = "~/SysAdmin/Configuraion/DataBaseManage.config";
            string PhyFilePath = System.Web.HttpContext.Current.Server.MapPath(FilePath);
            //备份bak文件所在数据库服务器文件夹路径
            string bakFilePath = Utils.XmlRead(PhyFilePath, "DataBaseManageConfig/BakFilePath", "");
            //数据库和网站程序是否同一台机器 0=否，1=是
            string isSameServer = Utils.XmlRead(PhyFilePath, "DataBaseManageConfig/IsSameServer", "");
            //是否其他用户进行还原数据库 0=否，1=是 
            string isUseOtherUser = Utils.XmlRead(PhyFilePath, "DataBaseManageConfig/IsUseOtherUser", "");

            //创建连接对象  
            SqlConnection conn;
            //设置的是其他还原用户
            if (Utils.ParseInt(isUseOtherUser,0) == 1)
            {
                conn = new SqlConnection(GetConnection(PhyFilePath));
            }
            else
            {
                conn = new SqlConnection(SQLHelper.ConnectionStringLocalTransaction);
            }

            //数据库与网站文件是同一台服务器
            if (Utils.ParseInt(isSameServer, 0) == 1)
            {
                dbFile = System.Web.HttpContext.Current.Server.MapPath("/dbbackup/") + dbFile;
            }
            else
            {
                dbFile = bakFilePath + "\\" + dbFile;
                dbFile = dbFile.Replace("\\\\", "\\");
            }

            //还原指定的数据库文件  
            string sql = string.Format(@"

USE MASTER
ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
RESTORE DATABASE [{0}] FROM DISK = N'{1}' with replace
ALTER DATABASE [{0}] SET MULTI_USER WITH ROLLBACK IMMEDIATE
", dbName, dbFile);

            SqlCommand sqlcmd = new SqlCommand(sql, conn);
            sqlcmd.CommandType = CommandType.Text;
            
            try {
                conn.Open();
                sqlcmd.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                string str = err.Message;
                conn.Close();
                return false;
            }
            conn.Close();//关闭数据库连接  
            conn.Dispose();

            try
            {
                //防止上面将数据库连接关闭后，导致网站暂时性出错
                conn = new SqlConnection(SQLHelper.ConnectionStringLocalTransaction);
                conn.Open();
                conn.Close();
                conn.Dispose();
            }
            catch { 
                conn.Close();
                conn.Dispose();
            }
            return true;
        }
        
    }
}