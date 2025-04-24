<%@ Page Title="Login" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="NexoMarket.NexoMarket.Login" %>

<asp:Content ID="Login" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5" style="max-width: 400px;">
        <h2 class="mb-4">Iniciar sesión</h2>

        <asp:ValidationSummary CssClass="text-danger" runat="server" />

        <div class="form-group mb-3">
            <label for="txtUsuario">Usuario</label>
            <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="valUsuario" runat="server" ControlToValidate="txtUsuario"
                ErrorMessage="El usuario es obligatorio" Display="Dynamic" CssClass="text-danger" />
        </div>

        <div class="form-group mb-3">
            <label for="txtPassword">Contraseña</label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="valPassword" runat="server" ControlToValidate="txtPassword"
                ErrorMessage="La contraseña es obligatoria" Display="Dynamic" CssClass="text-danger" />
        </div>

        <asp:Label ID="lblError" runat="server" CssClass="text-danger" />

        <asp:Button ID="btnLogin" runat="server" Text="Ingresar" CssClass="btn btn-primary mt-3"
            OnClick="btnLogin_Click" />
    </div>
</asp:Content>

