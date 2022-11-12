using DataAccessLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class BLLSysRole
    {
        //增
        public bool Add(SysRole r)
        {
            DALSysRole roleService = new DALSysRole();
            return roleService.Add(r);
        }
        
        //删
        public bool Delete(int id)
        {
            DALSysRole roleService = new DALSysRole();
            return roleService.Delete(id);
        }
        //改
        public bool Update(SysRole r,string menus)
        {
            DALSysRole roleService = new DALSysRole();
            return roleService.Update(r,menus);
        }
        //查
        public SysRole GetRoleById(int id)
        {
            DALSysRole roleService = new DALSysRole();
            return roleService.GetById(id);
        }
        public List<SysRole> GetAll()
        {
            DALSysRole roleService = new DALSysRole();
            return roleService.GetAll();
        }
        public List<SysMenu> GetMenusByRoleId(int id)
        {
            DALRole_Menu rmService = new DALRole_Menu();
            return rmService.GetAllMenusByRoleId(id);
        }
    }
}
