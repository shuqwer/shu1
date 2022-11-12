using DataAccessLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class BLLSysMenu
    {
        //显示
        public List<SysMenu> GetAll()
        {
            DALSysMenu menuService = new DALSysMenu();
            return menuService.GetAll();
        }
        //删除
        public bool Delete(int id)
        {
            DALSysMenu menuService = new DALSysMenu();
            return menuService.Delete(id);
        }
        //增
        public bool Add(SysMenu m)
        {
            DALSysMenu menuService = new DALSysMenu();
            return menuService.Add(m);
        }
        //查
        public SysMenu GetRoleById(int id)
        {
            DALSysMenu menuService = new DALSysMenu();
            return menuService.GetById(id);
        }
        //改
        public bool Updata(SysMenu m)
        {
            DALSysMenu menuService = new DALSysMenu();
            return menuService.Update(m);
        }
        
    }
}
