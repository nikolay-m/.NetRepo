<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LimeSystems.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery.validate.unobtrusive.bootstrap.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="vcenter">
        <div class="row">
            <asp:Panel ID="LoginPanel" CssClass="col-md-offset-3 col-md-6" runat="server">
                <asp:Label runat="server" CssClass="form-control label-primary text-capitalize text-center" Text="Система управления проектами" ForeColor="White"></asp:Label>
                </br>
                <div class="input-group">
                    <asp:Label ID="LoginLb" CssClass="input-group-addon" runat="server" Text="Логин"></asp:Label>
                    <asp:TextBox ID="LoginTb" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="input-group">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorLogin" CssClass="text-danger" runat="server" ControlToValidate="LoginTb" ErrorMessage="Введите логин"></asp:RequiredFieldValidator>
                </div>
            <div class="input-group">
                <asp:Label ID="PasswordLb" CssClass="input-group-addon" runat="server" Text="Пароль"></asp:Label>
                <asp:TextBox ID="PasswordTb" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
                <div class="input-group">
                    <asp:RequiredFieldValidator  ID="RequiredFieldValidatorPass" CssClass="text-danger" runat="server" ControlToValidate="PasswordTb" ErrorMessage="Введите пароль"></asp:RequiredFieldValidator>
                </div>
                <asp:Label ID="ErroMessage" CssClass="text-danger" runat="server"></asp:Label>
                <div class="checkbox">
                    <label>
                        <asp:CheckBox ID="StayInChb" CssClass="checkbox" runat="server" />
                        Запомнить меня
                    </label>
                </div>
                <asp:Button ID="LoginBtn" CssClass="form-control" runat="server" Text="Выполнить вход" OnClick="ValidateUser" />
            </asp:Panel>
        </div>
    </div>
</asp:Content>
