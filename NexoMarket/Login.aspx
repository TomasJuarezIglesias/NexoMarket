<%@ Page Title="Login" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="NexoMarket.NexoMarket.Login" %>

<asp:Content ID="Login" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-center align-items-center w-100" style="min-height: 93vh;">
        <div class="card shadow-lg p-4" style="width: 100%; max-width: 380px; border-radius: 0.75rem;">
            <img src="../Assets/Images/LogoNexoMarket.jpg" 
                    style="height: 150px; width: 100%; object-fit: cover; margin-bottom: 10px;" />

            

            <!-- Usuario -->
            <div class="form-group mb-3">
                <label for="txtUsuario" class="form-label">Usuario</label>

                <div class="input-group">
                    <span class="input-group-text"><i class="bi bi-person-fill"></i></span>
                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" />
                </div>

                <asp:RequiredFieldValidator ID="valUsuario" runat="server" ControlToValidate="txtUsuario"
                    ErrorMessage="El usuario es obligatorio" Display="Dynamic"
                    CssClass="form-text text-danger mt-1" />
            </div>

            <!-- Contraseña -->
            <div class="form-group mb-3">
                <label for="txtPassword" class="form-label">Contraseña</label>

                <div class="input-group">
                    <span class="input-group-text"><i class="bi bi-lock-fill"></i></span>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" />
                </div>

                <asp:RequiredFieldValidator ID="valPassword" runat="server" ControlToValidate="txtPassword"
                    ErrorMessage="La contraseña es obligatoria" Display="Dynamic"
                    CssClass="form-text text-danger mt-1" />
            </div>

            <asp:Label ID="lblError" runat="server" Visible="false" CssClass="alert alert-danger mt-3 d-block"/>

            <!-- Botón -->
            <div class="d-flex justify-content-center">
                <asp:Button ID="btnLogin" runat="server" Text="Ingresar"
                    CssClass="btn btn-primary mt-4 w-100" OnClick="btnLogin_Click" />
            </div>
        </div>
    </div>





</asp:Content>

