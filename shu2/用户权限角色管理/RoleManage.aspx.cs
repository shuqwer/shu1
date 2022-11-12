using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;

namespace 用户权限角色管理
{
    public partial class RoleManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DisplayAllRoles();
                InitiateRoles();
            }
        }
        private void DisplayAllRoles()
        {
            List<dynamic> lst = new List<dynamic>();
            BLLSysRole rService = new BLLSysRole();
            List<SysRole> lstr = rService.GetAll();
            foreach (SysRole r in lstr)
            {
                List<SysMenu> lstm = rService.GetMenusByRoleId(r.ID);
                string menus = "";
                foreach(SysMenu m in lstm)//登录，查看
                    menus += m.MenuName + ",";
                if (menus.EndsWith(","))
                    menus = menus.Substring(0, menus.Length - 1);
                var roler = new { ID = r.ID, RoleName = r.RoleName, IsActive = r.IsActive, Menus =menus};
                lst.Add(roler);
            }
            gvRole.DataSource = lst;
            gvRole.DataBind();
            //BLLSysRole r = new BLLSysRole();
            //List<SysRole> lst = r.GetAll();
            //gvRole.DataSource = lst;
            //gvRole.DataBind();
        }

        private void InitiateRoles()
        {
            BLLSysMenu mService = new BLLSysMenu();
            List<SysMenu> lstm = mService.GetAll();
            foreach (SysMenu m in lstm)
            {
                cblModMenus.Items.Add(new ListItem { Text = m.MenuName, Value = m.ID.ToString(), Selected = false });
            }
        }
        //增
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (tbRoleName.Text.Trim() == "")
                return;
            BLLSysRole rService = new BLLSysRole();
            SysRole role = new SysRole();
            role.RoleName = tbRoleName.Text.Trim();
            role.IsActive = rbRoleActive.Checked;
            rService.Add(role);
            DisplayAllRoles();
        }
        protected void gvRole_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(e.Values["ID"]);
            BLLSysRole rService = new BLLSysRole();
            rService.Delete(id);
            DisplayAllRoles();
        }
        protected void gvRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            string menus = "";
            string[] _menus;
            List<SysMenu> lstm;
            lblID.Text = gvRole.Rows[gvRole.SelectedIndex].Cells[1].Text.Trim();
            TBModRoleName.Text = gvRole.Rows[gvRole.SelectedIndex].Cells[2].Text.Trim();
            rbModRoleActive.Checked = (gvRole.Rows[gvRole.SelectedIndex].Cells[3].Controls[0] as CheckBox).Checked;
            rbModRoleDisActive.Checked = !rbModRoleActive.Checked;
            BLLSysRole rService = new BLLSysRole();
            lstm = rService.GetMenusByRoleId(Convert.ToInt32(lblID.Text));
            foreach(SysMenu m in lstm)
            {
                menus += m.ID + ",";
            }
            if (menus.EndsWith(","))
                menus = menus.Substring(0, menus.Length - 1);
            _menus = menus.Split(',');
            foreach(ListItem li in cblModMenus.Items)
            {
                li.Selected = false;
            }
            foreach(string s in _menus)
            {
                foreach(ListItem li in cblModMenus.Items)
                {
                    if (li.Value == s)
                    {
                        li.Selected =true;
                        break;
                    }
                }
            }
        }
        //修改
        protected void btnMod_Click(object sender, EventArgs e)
        {
            BLLSysRole rService = new BLLSysRole();
            try
            {
                string menus = "";
                foreach (ListItem li in cblModMenus.Items)
                {
                    if (li.Selected)
                    {
                        menus += li.Value + ",";
                    }
                }
                if (menus.EndsWith(","))
                    menus = menus.Substring(0, menus.Length - 1);
                SysRole role = rService.GetRoleById(Convert.ToInt32(lblID.Text));
                if (role != null)
                {
                    role.RoleName = TBModRoleName.Text.Trim();
                    role.IsActive = rbModRoleActive.Checked;
                    rService.Update(role,menus);

                    DisplayAllRoles();
                }
            }
            catch { }
        }
    }
}