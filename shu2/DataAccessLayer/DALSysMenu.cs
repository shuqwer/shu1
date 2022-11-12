using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DALSysMenu : DALBase<SysMenu>
    {
        //增
        public bool Add(SysMenu m)
        {
            string cmdTxt;
            if (m.IsActive)
                cmdTxt = string.Format("insert into SysMenu values('{0}',1);",m.MenuName);
            else
                cmdTxt = string.Format("insert into SysMenu values('{0}',0);", m.MenuName);          
                return base.Transaction(cmdTxt);
        }

        //删
        public bool Delete(int id)
        {
            string cmdTxt = String.Format("delete from User_Menu where UserID={0};", id);
            cmdTxt += String.Format("delete from SysMenu where ID={0};", id);
            return base.Transaction(cmdTxt);
        }

        //改
        public bool Update(SysMenu m)
        {
            string cmdTxt;
            if(m.IsActive)
                cmdTxt = String.Format("update SysMenu set MenuName = '{0}', IsActive = 1 where ID = {1};", m.MenuName, m.ID);
            else
                cmdTxt = String.Format("update SysMenu set MenuName = '{0}', IsActive = 0 where ID = {1};", m.MenuName, m.ID);
           return base.Transaction(cmdTxt);
        }
        //查
        public new SysMenu GetById( int id)
        {
            DataRow dr = base.GetById(id);
            if (dr == null)
                return null;
            else
            {
                SysMenu m = new SysMenu();
                m.ID = id;
                m.MenuName = dr["MenuName"].ToString().Trim();
                m.IsActive = Convert.ToBoolean(dr["IsActive"]);
                return m;
            }
        }

        //获取所有菜单选项
        public new List<SysMenu> GetAll()
        {
            List<SysMenu> ret = new List<SysMenu>();
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
                        new SysMenu
                        {
                            ID=Convert.ToInt32(dr["ID"].ToString()),
                            MenuName=dr["MenuName"].ToString().Trim(),
                            IsActive=Convert.ToBoolean(dr["IsActive"])
                        }
                     );                 
                }
                return ret;
            }

        }
    }
}
