<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="LimeSystems.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        //$(window).load(function () {
            
        //});
        var j = jQuery.noConflict();
        j(window).load(function () {
            j('.chosen-select').chosen();
            j('.chosen-select-deselect').chosen({ allow_single_deselect: true });
        })
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="btn-group">
        <asp:Button ID="UserBtn" CssClass="btn btn-primary" Text="Управление пользователями" runat="server" OnClick="UserBtn_Click" />
        <asp:Button ID="ProjectBtn" CssClass="btn" Text="Управление проектами" runat="server" OnClick="ProjectBtn_Click" />
        <asp:Button ID="TaskBtn" CssClass="btn" Text="Управление задачами" runat="server" OnClick="TaskBtn_Click" />
    </div>
    <br />
    <br />
    <br />
    <asp:MultiView ID="AdminContainer" runat="server" ActiveViewIndex="0">
        <asp:View ID="UserView" runat="server">
            <div class=" row form-group col-md-3">
                <div class="input-group">
                    <span class="input-group-addon label-success" style="color: white;">Действие</span>
                    <asp:DropDownList ID="UserViewAction" CssClass="form-control col-md-2 text-success" runat="server" AutoPostBack="true" OnSelectedIndexChanged="UserViewAction_SelectedIndexChanged">
                        <asp:ListItem Value="0" Text="Добавить пользователя"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Посмотреть пользователей" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <br />
            <br />
            <br />
            <asp:MultiView ID="UserViewContainer" runat="server" ActiveViewIndex="1">
                <asp:View ID="CreateUserView" runat="server">
                    <br />
                    <div class=" row col-md-4">
                        <div class="input-group">
                            <span class="input-group-addon">Логин</span>
                            <asp:TextBox ID="LoginTb" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorLogin" ValidationGroup="1" CssClass="text-danger" runat="server" ControlToValidate="LoginTb" ErrorMessage="Введите Логин"></asp:RequiredFieldValidator>
                        <br />
                        <div class="input-group">
                            <span class="input-group-addon">Имя</span>
                            <asp:TextBox ID="NameTb" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <br />
                        <div class="input-group">
                            <span class="input-group-addon">Фамилия</span>
                            <asp:TextBox ID="SurnameTb" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <br />
                        <div class="input-group">
                            <span class="input-group-addon">Пароль</span>
                            <asp:TextBox ID="PasswordTb" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword"  ValidationGroup="1" CssClass="text-danger" runat="server" ControlToValidate="PasswordTb" ErrorMessage="Введите Пароль"></asp:RequiredFieldValidator>
                        <br />
                        <div class="input-group">
                            <span class="input-group-addon">Уровень доступа</span>
                            <asp:DropDownList ID="RoleList" CssClass="form-control col-md-2 text-success" runat="server">
                                <asp:ListItem Value="Admin" Text="Администратор"></asp:ListItem>
                                <asp:ListItem Value="User" Text="Пользователь" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <br />
                        <div class="input-group">
                            <span class="input-group-addon">Активность</span>
                            <asp:DropDownList ID="StatusList" CssClass="form-control col-md-2 text-success" runat="server">
                                <asp:ListItem Value="0" Text="Не активен"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Активен" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <br />
                        <asp:Button ID="CreateUserBtn" CssClass="form-control btn-success" ValidationGroup="1" Text="Создать пользователя" runat="server" OnClick="CreateUserBtn_Click" />
                    </div>

                </asp:View>
                <asp:View ID="DisplayUserView" runat="server">
                    <br />
                    <div class=" row col-md-4">
                        <asp:GridView ID="UserGridView" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-bordered" Width="600px" AllowPaging="True" AllowSorting="True" DataSourceID="UserLDS" OnRowCommand="UserGridView_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="Login" HeaderText="Логин" ReadOnly="True"/>
                                <asp:BoundField DataField="Name" HeaderText="Имя" ReadOnly="True"/>
                                <asp:BoundField DataField="Surname" HeaderText="Фамилия" ReadOnly="True"/>
                                <asp:BoundField DataField="Password" HeaderText="Пароль" ReadOnly="True"/>
                                <asp:CheckBoxField DataField="Status" HeaderText="Активный" ReadOnly="True"/>
                                <asp:BoundField DataField="UserRole.Name" HeaderText="Права" ReadOnly="True"/>

                                <asp:CommandField HeaderText="Изменить активность" HeaderStyle-Width="10%" ShowHeader="True" ShowSelectButton="True" />

                            </Columns>
                        </asp:GridView>
                        <asp:LinqDataSource ID="UserLDS" runat="server" ContextTypeName="LimeSystems.DAL.Storage" OrderBy="Login, Status, UserRole" Select="new (Login, Name, Surname, Password, Status, UserRole)" TableName="Users">
                            <DeleteParameters>
                                <asp:Parameter Name="Login" />
                            </DeleteParameters>
                        </asp:LinqDataSource>
                    </div>
                </asp:View>
            </asp:MultiView>
        </asp:View>
        <asp:View ID="ProjectView" runat="server">
            <div class=" row form-group col-md-3">
                <div class="input-group">
                    <span class="input-group-addon label-success" style="color: white;">Действие</span>
                    <asp:DropDownList ID="ProjectViewAction" CssClass="form-control col-md-2 text-success" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ProjectViewAction_SelectedIndexChanged">
                        <asp:ListItem Value="0" Text="Добавить проект"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Список проектов" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <br />
            <br />
            <br />
            <asp:MultiView ID="ProjectContainer" runat="server" ActiveViewIndex="1">
                <asp:View ID="CreateProjectView" runat="server">
                    <div class=" row col-md-4">
                        <div class="input-group">
                            <span class="input-group-addon">Название</span>
                            <asp:TextBox ID="ProjNameTb" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="2" CssClass="text-danger" runat="server" ControlToValidate="ProjNameTb" ErrorMessage="Введите название"></asp:RequiredFieldValidator>
                      
                        <br />
                        <div class="input-group">
                            <span class="input-group-addon">Учасники</span>
                            <asp:ListBox SelectionMode="Multiple" ID="MemberList"  CssClass="chosen-select" runat="server">
                            </asp:ListBox>
                        </div>
                        <br />
                        <div class="input-group">
                            <span class="input-group-addon">Активность</span>
                            <asp:DropDownList ID="ProjStatus" CssClass="form-control col-md-2 text-success" runat="server">
                                <asp:ListItem Value="0" Text="Не активен"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Активен" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <br />
                        <asp:Button ID="ProjCreateBtn" CssClass="form-control btn-success" ValidationGroup="2" Text="Создать проект" runat="server" OnClick="ProjCreateBtn_Click" />
                    </div>
                </asp:View>
                <asp:View ID="DisplayProjectView" runat="server">
                     <br />
                    <div class=" row col-md-4">
                        <asp:GridView ID="ProjectGridview" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-bordered" Width="600px" AllowPaging="True" AllowSorting="True" DataSourceID="ProjLDS" OnRowCommand="ProjectGridview_RowCommand">
                            
                            <Columns>
                                <asp:BoundField DataField="ProjectCode" HeaderText="Код проекта" ReadOnly="True"  />
                                <asp:BoundField DataField="Name" HeaderText="Название" ReadOnly="True"  />
                                <asp:CheckBoxField DataField="Status" HeaderText="Активен" ReadOnly="True"  />
                               
                                <asp:CommandField HeaderText="Изменить активность" ShowHeader="True" ShowSelectButton="True" />
                            </Columns>
                            
                        </asp:GridView>
                        
                        
                        <asp:LinqDataSource ID="ProjLDS" runat="server" ContextTypeName="LimeSystems.DAL.Storage" EntityTypeName="" OrderBy="ProjectCode, ProjectCode, Status" Select="new (ProjectCode, Name, Status, Employees)" TableName="Projects">
                        </asp:LinqDataSource>
                        
                        
                    </div>
                </asp:View>
            </asp:MultiView>
        </asp:View>
        <asp:View ID="TaskView" runat="server">
            <div class=" row form-group col-md-3">
                <div class="input-group">
                    <span class="input-group-addon label-success" style="color: white;">Действие</span>
                    <asp:DropDownList ID="TaskViewAction" CssClass="form-control col-md-2 text-success" runat="server" AutoPostBack="true" OnSelectedIndexChanged="TaskViewAction_SelectedIndexChanged">
                        <asp:ListItem Value="0" Text="Добавить задачу"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Список задач" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <br />
            <br />
            <br />
            <asp:MultiView ID="TaskContainer" runat="server" ActiveViewIndex="1">
                <asp:View ID="CreateTaskView" runat="server">
                    Модуль в разработке
                    <br/>
                    <br/>
                    <div class=" row col-md-4">
                        <div class="input-group">
                            <span class="input-group-addon">Название</span>
                            <asp:TextBox ID="TaskNameTb" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="3" CssClass="text-danger" runat="server" ControlToValidate="TaskNameTb" ErrorMessage="Введите название"></asp:RequiredFieldValidator>
                        <br />
                        <div class="input-group">
                            <span class="input-group-addon">Описание</span>
                            <asp:TextBox ID="TaskDescription" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <br />
                        <div class="input-group">
                            <span class="input-group-addon">Проект</span>
                            <asp:DropDownList ID="ProjectListTb" CssClass="form-control col-md-2 text-success" runat="server">
                            </asp:DropDownList>
                        </div>
                        <br />
                        <div class="input-group">
                            <span class="input-group-addon">Дата начала</span>
                            <asp:TextBox ID="DateStartTb" TextMode="DateTime" CssClass="form-control date" runat="server"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="3" CssClass="text-danger" runat="server" ControlToValidate="DateStartTb" ErrorMessage="Введите Дату Начала"></asp:RequiredFieldValidator>
                        <br />
                        <div class="input-group">
                            <span class="input-group-addon">Дедлайн</span>
                            <asp:TextBox ID="DateDeadlineTb" TextMode="DateTime" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="3" CssClass="text-danger" runat="server" ControlToValidate="DateDeadlineTb" ErrorMessage="Введите Дату Дедлайна"></asp:RequiredFieldValidator>
                        
                        <br />
                        <div class="input-group">
                            <span class="input-group-addon">Активность</span>
                            <asp:DropDownList ID="TaskStatus" CssClass="form-control col-md-2 text-success" runat="server">
                                <asp:ListItem Value="0" Text="Не активен"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Активен" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <br />
                        <asp:Button ID="CreateTaskBtn" CssClass="form-control btn-success" ValidationGroup="3" Text="Создать Задачу" runat="server" OnClick="CreateTaskBtn_Click" />
                    </div>
                </asp:View>
                <asp:View ID="DisplayTaskView" runat="server">
                    TaskDisplay --- Модуль в разработке
                </asp:View>
            </asp:MultiView>
        </asp:View>
    </asp:MultiView>

</asp:Content>
