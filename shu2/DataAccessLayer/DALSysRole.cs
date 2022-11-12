using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
   
    public class DALSysRole:DALBase<SysRole>
    {
        //增
        public bool Add(SysRole r)
        {
            string cmdTxt;
            if (r.IsActive)
                cmdTxt = string.Format("insert into SysRole values('{0}',1);", r.RoleName);
            else
                cmdTxt = string.Format("insert into SysRole values('{0}',0);", r.RoleName);
            return base.Transaction(cmdTxt);
        }
        //删
        public bool Delete(int id)
        {
            string cmdTxt = String.Format("delete from User_Role where RoleID={0};", id);
            cmdTxt += String.Format("delete from SysRole where ID={0};", id);
            return base.Transaction(cmdTxt);
        }    
        //改
        public bool Update(SysRole r,string menus)
        {
            string cmdTxt;
            string[] _menus = menus.Split(',');
            if (r.IsActive)
                cmdTxt = String.Format("update SysRole set RoleName = '{0}', " +
                    "IsActive = 1 " +
                    "where ID = {1};",
                    r.RoleName,
                    r.ID);
            else
                cmdTxt = String.Format("update SysRole set RoleName = '{0}', " +
                    "IsActive = 0 " +
                    "where ID = {1};",
                    r.RoleName,
                    r.ID);
            cmdTxt += String.Format("delete from Role_Menu where RoleID={0};", r.ID);
            foreach(string s in _menus)
            {
                if (s != "")
                {
                    cmdTxt += String.Format("insert into Role_Menu values({0}, {1});", r.ID, s);
                }
            }
            return base.Transaction(cmdTxt);
        }
        //获取所有新角色
        public new List<SysRole> GetAll()
        {
            List<SysRole> ret = new List<SysRole>();
            DataTable dt = base.GetAll();
            if (dt.Rows[0]["ID"].ToString() == "")
            {
                return ret;
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {

                    ret.Add(
                        new SysRole
                        {
                            ID = Convert.ToInt32(dr["ID"].ToString()),
                            RoleName = dr["RoleName"].ToString().Trim(),
                            IsActive = Convert.ToBoolean(dr["IsActive"])
                        }
                    );
                }
                return ret;
            }
            
        }
        //查
        public new SysRole GetById(int id)
        {
            DataRow dr = base.GetById(id);
            if (dr == null)
                return null;
            else
            {
                SysRole r = new SysRole();
                r.ID = id;
                r.RoleName = dr["RoleName"].ToString().Trim();
                r.IsActive = Convert.ToBoolean(dr["IsActive"]);
                return r;
            }
        }
    }
}
