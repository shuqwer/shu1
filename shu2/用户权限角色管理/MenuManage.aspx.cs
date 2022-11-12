using BusinessLogicLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 用户权限角色管理
{
    public partial class MenuManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DisplayAllMenus();
            }
        }
        public void DisplayAllMenus()
        {
            BLLSysMenu m = new BLLSysMenu();
            List<SysMenu> lst = m.GetAll();
            gvMenu.DataSource = lst;
            gvMenu.DataBind();
        }

        protected void gvMenu_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(e.Values["ID"]);
            BLLSysMenu mService = new BLLSysMenu();
            mService.Delete(id);
            DisplayAllMenus();
        }

        protected void gvMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblID.Text = gvMenu.Rows[gvMenu.SelectedIndex].Cells[1].Text.Trim();
            tbModMenuName.Text = gvMenu.Rows[gvMenu.SelectedIndex].Cells[2].Text.Trim();
            rbModMenuActive.Checked = (gvMenu.Rows[gvMenu.SelectedIndex].Cells[3].Controls[0] as CheckBox).Checked;
            rbModMenuDisActive.Checked = !rbModMenuActive.Checked;
        }

        protected void btMod_Click(object sender, EventArgs e)
        {
            BLLSysMenu mService = new BLLSysMenu();
            try
            {
                SysMenu menu = mService.GetRoleById(Convert.ToInt32(lblID.Text));
                if (menu != null)
                {
                    menu.MenuName = tbModMenuName.Text.Trim();
                    menu.IsActive = rbModMenuActive.Checked;
                    mService.Updata(menu);
                    DisplayAllMenus();
                }
            }
            catch { }
        }

        protected void btAdd_Click(object sender, EventArgs e)
        {
            if (tbMenuName.Text.Trim() == "")
            {
                return;
            }
            BLLSysMenu mService = new BLLSysMenu();
            SysMenu menu = new SysMenu();
            menu.MenuName = tbMenuName.Text.Trim();
            mService.Add(menu);
            DisplayAllMenus();
        }
    }
}