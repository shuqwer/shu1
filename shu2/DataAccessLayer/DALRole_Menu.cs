using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DALRole_Menu: DALBase<Role_Menu>
    {
        //根据角色ID获取所有的菜单信息
        public List<SysMenu> GetAllMenusByRoleId(int id)
        {
            List<SysMenu> ret = new List<SysMenu>();
            DALSysMenu r = new DALSysMenu();
            DataTable dt = base.GetAll();
            foreach (DataRow dr in dt.Rows)
            {
                if (id == Convert.ToInt32(dr["RoleID"].ToString()))
                {
                    ret.Add(r.GetById(Convert.ToInt32(dr["MenuID"].ToString())));
                }
            }
            return ret;
        }
    }
}
