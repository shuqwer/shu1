using DataAccessLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogicLayer
{
    public class BLLSysUser
    {
        //增
        public bool Add(SysUser u)
        {
            DALSysUser userService = new DALSysUser();
            return userService.Add(u);
        }
        //删
        public bool Delete(int id)
        {
            DALSysUser userService = new DALSysUser();
            return userService.Delete(id);
        }
        //改
        public bool Update(SysUser u, string roles)
        {
            DALSysUser userService = new DALSysUser();
            return userService.Update(u, roles);
        }
        //查
        public SysUser GetUserById(int id)
        {
            DALSysUser userService = new DALSysUser();
            return userService.GetById(id);
        }

        public List<SysUser> GetAll()
        {
            DALSysUser userService = new DALSysUser();
            return userService.GetAll();
        }
        public List<SysRole> GetRolesByUserId(int id)
        {
            DALUser_Role urService = new DALUser_Role();
            return urService.GetAllRolesByUserId(id);
        }
    }
}
