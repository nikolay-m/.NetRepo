using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using LimeSystems.DAL;


namespace LimeSystems
{
    public partial class Login : System.Web.UI.Page
    {

        protected void ValidateUser(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
               
                String pass = CryptoProvider.GetMD5Hash(PasswordTb.Text);
                try
                {
                    using (Storage db = new Storage())
                    {
                        var currentUser = db.Users.Where(u => u.Login == LoginTb.Text && u.Password == pass && u.Status == true).Select(u => new {Login=u.Login,Role=u.UserRole.Name}).FirstOrDefault();


                        if (currentUser != null)
                        {
                            Session["ActiveUser"] = currentUser.Login;
                            Session["UserRole"] = currentUser.Role;

                            FormsAuthentication.RedirectFromLoginPage(LoginTb.Text, StayInChb.Checked);
                        }
                        else
                        {
                            ErroMessage.Text = "Не правильный логин или пароль";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Loger.Log(Response, ex);
                }
            }    
        }

        protected void Page_Load(object sender, EventArgs e)
        {


        }
    }
}