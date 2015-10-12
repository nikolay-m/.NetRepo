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
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!this.Page.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }

            if (Session["UserRole"] == null || Session["UserRole"].ToString() == "User")
            {
                Response.Redirect("Home.aspx");
            }

        }

        protected void UserBtn_Click(object sender, EventArgs e)
        {

            UserBtn.CssClass = "btn btn-primary";
            ProjectBtn.CssClass = "btn";
            TaskBtn.CssClass = "btn";
            AdminContainer.ActiveViewIndex = 0;

            ResetUserCreateForm();


        }

        protected void ProjectBtn_Click(object sender, EventArgs e)
        {
            UserBtn.CssClass = "btn";
            ProjectBtn.CssClass = "btn btn-primary";
            TaskBtn.CssClass = "btn";
            AdminContainer.ActiveViewIndex = 1;


        }

        protected void TaskBtn_Click(object sender, EventArgs e)
        {
            UserBtn.CssClass = "btn";
            ProjectBtn.CssClass = "btn";
            TaskBtn.CssClass = "btn  btn-primary";
            AdminContainer.ActiveViewIndex = 2;
        }

        protected void UserViewAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UserViewAction.SelectedValue == "0")
            {
                UserViewContainer.ActiveViewIndex = 0;
            }
            else if (UserViewAction.SelectedValue == "1")
            {
                UserViewContainer.ActiveViewIndex = 1;
            }
        }

        protected void ProjectViewAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ProjectViewAction.SelectedValue == "0")
            {
                ProjectContainer.ActiveViewIndex = 0;

                try
                {
                    using (Storage db = new Storage())
                    {
                        var actUsers = db.Users.Where(u => u.Status == true).Select(u => u.Login).ToList();

                        MemberList.DataSource = actUsers;
                        MemberList.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    Loger.Log(Response, ex);
                }

            }
            else if (ProjectViewAction.SelectedValue == "1")
            {
                ProjectContainer.ActiveViewIndex = 1;
            }

        }

        protected void TaskViewAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TaskViewAction.SelectedValue == "0")
            {
                TaskContainer.ActiveViewIndex = 0;

                try
                {
                    using (Storage db = new Storage())
                    {
                        var projects = db.Projects.Select(p=>new {Value=p.ProjectCode, Text=p.Name}).ToList();

                        foreach (var pr in projects) {
                            ProjectListTb.Items.Add(new ListItem(pr.Text, pr.Value.ToString()));
                        }

                        ProjectListTb.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    Loger.Log(Response, ex);
                }
            }
            else if (TaskViewAction.SelectedValue == "1")
            {
                TaskContainer.ActiveViewIndex = 1;
            }

        }

        protected void CreateUserBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    using (Storage db = new Storage())
                    {
                        if (!db.Users.Where(u => u.Login == LoginTb.Text).Any())
                        {
                            DAL.User new_user = new User();

                            new_user.Login = LoginTb.Text;
                            new_user.Name = (string.IsNullOrEmpty(NameTb.Text) || string.IsNullOrWhiteSpace(NameTb.Text)) ? null : NameTb.Text;
                            new_user.Surname = (string.IsNullOrEmpty(SurnameTb.Text) || string.IsNullOrWhiteSpace(SurnameTb.Text)) ? null : SurnameTb.Text;
                            new_user.Password = CryptoProvider.GetMD5Hash(PasswordTb.Text);
                            new_user.Status = StatusList.SelectedValue == "0" ? false : true;
                            new_user.UserRole = db.Roles.Where(r => r.Name == RoleList.SelectedValue).First();

                            db.Users.Add(new_user);
                            db.SaveChanges();

                            ResetUserCreateForm();
                            UserGridView.DataBind();
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Пользователь был успешно добавлен')", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Пользователь с таким логином уже существует')", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Loger.Log(Response, ex);
                }
            }

        }

        protected void UserGridView_SelectedIndexChanged(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string Login = UserGridView.Rows[rowIndex].Cells[0].Text;


                using (Storage db = new Storage())
                {

                    DAL.User cu = db.Users.Include("UserRole").Where(u => u.Login == Login).FirstOrDefault();

                    if (cu != null)
                    {

                        cu.Status = (cu.Status) ? false : true;
                        db.SaveChanges();
                    }

                    UserGridView.DataBind();
                }

            }
            catch (Exception ex)
            {
                Loger.Log(Response, ex);
            }
        }

        protected void ProjCreateBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                try
                {
                    using (Storage db = new Storage())
                    {

                        Project p = new Project();

                        p.Name = ProjNameTb.Text;
                        p.Status = ProjStatus.SelectedValue == "0" ? false : true;

                        foreach (ListItem li in MemberList.Items)
                        {
                            if (li.Selected)
                            {
                                DAL.User su = db.Users.Where(u => u.Login == li.Value).Select(u => u).FirstOrDefault();
                                p.Employees.Add(su);
                            }
                        }

                        db.Projects.Add(p);
                        db.SaveChanges();

                        ResetProjectCreateForm();
                        ProjectGridview.DataBind();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Проект был успешно добавлен')", true);

                    }

                }
                catch (Exception ex)
                {
                    Loger.Log(Response, ex);
                }
            }

        }

        protected void ProjectGridview_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int projId = Convert.ToInt32(ProjectGridview.Rows[rowIndex].Cells[0].Text);


                using (Storage db = new Storage())
                {

                    DAL.Project proj = db.Projects.Include("Employees").Include("Tasks").Where(p => p.ProjectCode == projId).FirstOrDefault();

                    if (proj != null)
                    {

                        proj.Status = (proj.Status) ? false : true;
                        db.SaveChanges();
                    }

                    ProjectGridview.DataBind();
                }

            }
            catch (Exception ex)
            {
                Loger.Log(Response, ex);
            }
        }


        protected void ResetUserCreateForm()
        {
            LoginTb.Text = string.Empty;
            NameTb.Text = string.Empty;
            SurnameTb.Text = string.Empty;
            PasswordTb.Text = string.Empty;
        }

        protected void ResetProjectCreateForm()
        {
            ProjNameTb.Text = string.Empty;
            foreach (ListItem li in MemberList.Items)
            {
                if (li.Selected)
                {
                    li.Selected = false;
                }
            }
        }

        protected void UserGridViewBind()
        {
            try
            {
                using (Storage db = new Storage())
                {
                    var users = db.Users.Select(u => new
                    {
                        Login = u.Login,
                        Name = u.Name,
                        Surname = u.Surname,
                        Password = u.Password,
                        Role = u.UserRole.Name,
                        Status = u.Status
                    }).ToList();

                    UserGridView.DataSource = users;
                    UserGridView.DataBind();
                }
            }
            catch (Exception ex)
            {
                Loger.Log(Response, ex);
            }
        }

        protected void CreateTaskBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid) {
                
                Task new_task = new Task();

                new_task.Name = TaskNameTb.Text;
                
               
            }
        }
    }
}