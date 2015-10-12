using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LimeSystems
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ActiveUser"] != null)
            {
                WelcomeUser.Text ="Пользователь: "+ Session["ActiveUser"].ToString();
                WelcomeUser.Visible = true;

                if (Session["UserRole"] != null && Session["UserRole"].ToString() == "Admin")
                {
                    AdminLink.Text = "Панель администратора";
                    AdminLink.Visible = true;
                }
            }
            
        }

        protected void LogoutBtn_LoggedOut(object sender, EventArgs e)
        {
            Session.Abandon();
            WelcomeUser.Text = String.Empty;
            WelcomeUser.Visible = false;
            AdminLink.Visible = false;
        }
    }
}