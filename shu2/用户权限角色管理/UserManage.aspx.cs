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
    public partial class UserManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DisplayAllUsers();
                InitiateMenus();
            }
        }
        //显示内容
        private void DisplayAllUsers()
        {
            List<dynamic> lst = new List<dynamic>();
            BLLSysUser uService = new BLLSysUser();
            List<SysUser> lstu = uService.GetAll();
            foreach (SysUser u in lstu)
            {
                List<SysRole> lstr = uService.GetRolesByUserId(u.ID);
                string roles = "";
                foreach (SysRole r in lstr)//校领导，教师
                    roles += r.RoleName + ",";//校领导，教师，
                if (roles.EndsWith(","))
                    roles = roles.Substring(0, roles.Length - 1);
                var user = new { ID = u.ID, Name = u.UserName, Password = u.Password, IsActive = u.IsActive, Roles = roles };
                lst.Add(user);
            }
            gvUser.DataSource = lst;
            gvUser.DataBind();
        }
        //显示菜单选项
        private void InitiateMenus()
        {
            BLLSysRole rService = new BLLSysRole();
            List<SysRole> lstr = rService.GetAll();
            foreach (SysRole r in lstr)
            {
                cblModRoles.Items.Add(new ListItem { Text = r.RoleName, Value = r.ID.ToString(), Selected = false });
            }
        }
        //删除记录
        protected void gvUser_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(e.Values["ID"]);
            BLLSysUser uService = new BLLSysUser();
            uService.Delete(id);
            DisplayAllUsers();
        }
        //选择记录
        protected void gvUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            string roles = "";
            string[] _roles;
            List<SysRole> lstr;
            lblID.Text = gvUser.Rows[gvUser.SelectedIndex].Cells[1].Text.Trim();
            TBModUserName.Text = gvUser.Rows[gvUser.SelectedIndex].Cells[2].Text.Trim();
            TBModUserPwd.Text = gvUser.Rows[gvUser.SelectedIndex].Cells[3].Text.Trim();
            rbModUserActive.Checked = (gvUser.Rows[gvUser.SelectedIndex].Cells[4].Controls[0] as CheckBox).Checked;
            rbModUserDisActive.Checked = !rbModUserActive.Checked;
            BLLSysUser uService = new BLLSysUser();
            lstr = uService.GetRolesByUserId(Convert.ToInt32(lblID.Text));
            foreach (SysRole r in lstr)
            {
                roles += r.ID + ",";//1,2,
            }
            if (roles.EndsWith(","))
                roles = roles.Substring(0, roles.Length - 1);//1,2
            _roles = roles.Split(',');//"1"   "2"
            foreach (ListItem li in cblModRoles.Items)
            {
                li.Selected = false;
            }
            foreach (string s in _roles)
            {
                foreach (ListItem li in cblModRoles.Items)
                {
                    if (li.Value == s)
                    {
                        li.Selected = true;
                        break;
                    }
                }
            }
        }
        //更改记录
        protected void btnMod_Click(object sender, EventArgs e)
        {
            BLLSysUser uService = new BLLSysUser();
            try
            {
                string roles = "";
                foreach (ListItem li in cblModRoles.Items)
                {
                    if (li.Selected)
                    {
                        roles += li.Value + ",";
                    }
                }
                if (roles.EndsWith(","))
                    roles = roles.Substring(0, roles.Length - 1);
                SysUser user = uService.GetUserById(Convert.ToInt32(lblID.Text));
                if (user != null)
                {
                    user.UserName = TBModUserName.Text.Trim();
                    user.Password = TBModUserPwd.Text.Trim();
                    user.IsActive = rbModUserActive.Checked;
                    uService.Update(user, roles);
                    DisplayAllUsers();
                }
            }
            catch { }
        }
        //新增角色
        protected void btnAdd_Click(object sender, EventArgs e)
        {

            if (tbUserName.Text.Trim() == "" || tbUserPwd.Text.Trim() == "")
                return;
            BLLSysUser uService = new BLLSysUser();
            SysUser user = new SysUser();
            user.UserName = tbUserName.Text.Trim();
            user.Password = tbUserPwd.Text.Trim();
            user.IsActive = rbUserActive.Checked;
            uService.Add(user);
            DisplayAllUsers();
        }
    }
}