using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DALUser_Role : DALBase<User_Role>
    {
        //根据用户ID获取所有的角色信息
        public List<SysRole> GetAllRolesByUserId(int id)
        {
            List<SysRole> ret = new List<SysRole>();
            DALSysRole r = new DALSysRole();
            DataTable dt = base.GetAll();
            foreach (DataRow dr in dt.Rows)
            {
                if (id == Convert.ToInt32(dr["UserID"].ToString()))
                {
                    ret.Add(r.GetById(Convert.ToInt32(dr["RoleID"].ToString())));
                }
            }
            return ret;
        }
    }
}
