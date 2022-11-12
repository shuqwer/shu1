using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DALSysUser : DALBase<SysUser>
    {
        //增
        public bool Add(SysUser u)
        {
            string cmdTxt;
            if (u.IsActive)
                cmdTxt = String.Format("insert into SysUser values('{0}','{1}',1);", u.UserName, u.Password);
            else
                cmdTxt = String.Format("insert into SysUser values('{0}','{1}',0);", u.UserName, u.Password);
            return base.Transaction(cmdTxt);
        }
        //删
        public bool Delete(int id)
        {
            string cmdTxt = String.Format("delete from User_Role where UserID={0};", id);
            cmdTxt += String.Format("delete from SysUser where ID={0};", id);
            return base.Transaction(cmdTxt);
        }
        //改
        public bool Update(SysUser u, string roles)
        {
            string cmdTxt;
            string[] _roles = roles.Split(',');//"1"  "2"  "3"
            if (u.IsActive)
                cmdTxt = String.Format("update SysUser set UserName = '{0}', " +
                    "Password = '{1}', " +
                    "IsActive = 1 " +
                    "where ID = {2};",
                    u.UserName,
                    u.Password,
                    u.ID);
            else
                cmdTxt = String.Format("update SysUser set UserName = '{0}', " +
                    "Password = '{1}', " +
                    "IsActive = 0 " +
                    "where ID = {2};",
                    u.UserName,
                    u.Password,
                    u.ID);
            cmdTxt += String.Format("delete from User_Role where UserID={0};", u.ID);
            foreach (string s in _roles)//"1","2","3"
            {
                if (s != "")
                {
                    cmdTxt += String.Format("insert into User_Role values({0}, {1});", u.ID, s);
                }
            }
            return base.Transaction(cmdTxt);
        }
        //获取所有用户
        public new List<SysUser> GetAll()
        {
            List<SysUser> ret = new List<SysUser>();
            DataTable dt = base.GetAll();
            foreach (DataRow dr in dt.Rows)
            {
                ret.Add(
                    new SysUser
                    {
                        ID = Convert.ToInt32(dr["ID"].ToString()),
                        UserName = dr["UserName"].ToString().Trim(),
                        Password = dr["Password"].ToString().Trim(),
                        IsActive = Convert.ToBoolean(dr["IsActive"])
                    }
                );
            }
            return ret;
        }
        //根据id获取用户
        public new SysUser GetById(int id)
        {
            DataRow dr = base.GetById(id);
            if (dr == null)
                return null;
            else
            {
                SysUser u = new SysUser();
                u.ID = id;
                u.UserName = dr["UserName"].ToString().Trim();
                u.Password = dr["Password"].ToString().Trim();
                u.IsActive = Convert.ToBoolean(dr["IsActive"]);
                return u;
            }
        }
    }
}
