using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DALBase<T>
        where T : class
    {
        private static readonly string conStr;//连接字符串
        private static SqlConnection conn;//连接
        static DALBase()
        {
            conStr = ConfigurationManager.AppSettings["connectionString"].ToString();
            conn = new SqlConnection(conStr);
        }
        //获取数据库中某个表的所有数据
        public DataTable GetAll()
        {
            DataSet ds = new DataSet();
            string cmdTxt = String.Format("select * from {0};", typeof(T).Name);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmdTxt, conn);
            sqlDataAdapter.Fill(ds);
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }
        //根据id获取数据库中某个表的某条记录
        public DataRow GetById(int id)
        {
            DataSet ds = new DataSet();
            string cmdTxt = String.Format("select * from {0} where ID={1};", typeof(T).Name, id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmdTxt, conn);
            sqlDataAdapter.Fill(ds);
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            else
                return ds.Tables[0].Rows[0];
        }
        //执行Sql查询语句，获取数据
        public DataTable SqlQuery(string cmdTxt)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmdTxt, conn);
            sqlDataAdapter.Fill(ds);
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }
        //以事务方式执行非查询操作
        public bool Transaction(string cmdTxt)
        {
            bool ret;
            try
            {
                conn.Open();//打开连接
                SqlTransaction tran = conn.BeginTransaction();//开始事务
                SqlCommand command = new SqlCommand(cmdTxt, conn, tran);//初始化SqlCommand对象
                if (command.ExecuteNonQuery() > 0)//执行命令
                {
                    ret = true;
                    tran.Commit();
                }
                else
                {
                    ret = false;
                    tran.Rollback();
                }
            }
            catch
            {
                ret = false;
            }
            finally
            {
                conn.Close();//关闭连接
            }
            return ret;
        }
    }
}
