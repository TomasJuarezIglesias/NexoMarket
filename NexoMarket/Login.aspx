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
                    CssClass="form-text text-danger mt-1" ValidationGroup="LoginGroup" />
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
                    CssClass="form-text text-danger mt-1" ValidationGroup="LoginGroup" />
            </div>

            <asp:Label ID="lblError" runat="server" Visible="false" CssClass="alert alert-danger mt-3 d-block" />

            <!-- Botón de Login -->
            <div class="d-flex justify-content-center">
                <asp:Button ID="btnLogin" runat="server" Text="Ingresar"
                    CssClass="btn btn-primary mt-4 w-100"
                    OnClick="btnLogin_Click" ValidationGroup="LoginGroup" />
            </div>

            <!-- Registro -->
            <div class="d-flex flex-column align-items-center mt-4 pt-3 border-top border-2 border-light-subtle">
                <label>¿No tienes una cuenta?</label>
                <button class="btn btn-primary mt-4 w-100" data-bs-toggle="modal" data-bs-target="#registroModal" type="button">Registrarse</button>
            </div>
        </div>

        <!-- Modal de Registro -->
        <div class="modal fade" id="registroModal" tabindex="-1" aria-labelledby="registroModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content p-4 shadow rounded-4 d-flex align-items-center">
                    <h3 class="mb-4 text-center border-bottom pb-4">Regístrate en Nexo-Market</h3>

                    <!-- Usuario -->
                    <div class="mb-3">
                        <label for="usuario-register" class="form-label">Nombre de Usuario</label>
                        <asp:TextBox ID="usuario_register" runat="server" CssClass="form-control" placeholder="Ej. Juan123" />
                        <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ControlToValidate="usuario_register"
                            ErrorMessage="El nombre de usuario es obligatorio." CssClass="text-danger" Display="Dynamic" ValidationGroup="RegisterGroup" />
                    </div>

                    <!-- Nombre -->
                    <div class="mb-3">
                        <label for="nombre-register" class="form-label">Nombre</label>
                        <asp:TextBox ID="nombre_register" runat="server" CssClass="form-control" placeholder="Ej. Juan" />
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="nombre_register"
                            ErrorMessage="El nombre es obligatorio." CssClass="text-danger" Display="Dynamic" ValidationGroup="RegisterGroup" />
                    </div>

                    <!-- Apellido -->
                    <div class="mb-3">
                        <label for="apellido-register" class="form-label">Apellido</label>
                        <asp:TextBox ID="apellido_register" runat="server" CssClass="form-control" placeholder="Ej. Perez" />
                        <asp:RequiredFieldValidator ID="rfvApellido" runat="server" ControlToValidate="apellido_register"
                            ErrorMessage="El apellido es obligatorio." CssClass="text-danger" Display="Dynamic" ValidationGroup="RegisterGroup" />
                    </div>

                    <!-- Contraseña -->
                    <div class="mb-3">
                        <label for="password-register" class="form-label">Contraseña</label>
                        <asp:TextBox ID="password_register" runat="server" TextMode="Password" CssClass="form-control" placeholder="********" />
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="password_register"
                            ErrorMessage="La contraseña es obligatoria." CssClass="text-danger" Display="Dynamic" ValidationGroup="RegisterGroup" />
                    </div>

                    <!-- Repetir Contraseña -->
                    <div class="mb-3">
                        <label for="repassword-register" class="form-label">Repetir Contraseña</label>
                        <asp:TextBox ID="repassword_register" runat="server" TextMode="Password" CssClass="form-control" placeholder="********" />
                        <asp:RequiredFieldValidator ID="rfvRepassword" runat="server" ControlToValidate="repassword_register"
                            ErrorMessage="Debe repetir la contraseña." CssClass="text-danger" Display="Dynamic" ValidationGroup="RegisterGroup" />
                        <asp:CompareValidator ID="cvPasswords" runat="server" ControlToCompare="password_register" ControlToValidate="repassword_register"
                            ErrorMessage="Las contraseñas no coinciden." CssClass="text-danger" Display="Dynamic" ValidationGroup="RegisterGroup" />
                    </div>

                    <!-- Botón de Registro -->
                    <asp:Button ID="btnRegistrarse" runat="server"
                        CssClass="btn btn-primary w-100 mt-3"
                        Text="Registrarse"
                        OnClick="btn_registrarse"
                        ValidationGroup="RegisterGroup" />

                    <!-- Mensajes -->
                    <asp:Label ID="LabelErrorRegister" runat="server" Visible="false" CssClass="alert alert-danger mt-3 d-block" />
                </div>
            </div>
        </div>
    </div>

    <!-- Modal ErroresDV -->
    <div class="modal fade" id="modalErroresDV" tabindex="-1" aria-labelledby="erroresModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-lg">
            <div class="modal-content shadow rounded-4">
                <div class="modal-header bg-danger text-white rounded-top-4">
                    <h4 class="modal-title w-100 text-center" id="erroresModalLabel">⚠️ Vulneración en la base de datos</h4>
                </div>
                <div class="modal-body px-4 py-3">
                    <p class="text-center text-muted mb-4">
                        Se han detectado inconsistencias en los registros. A continuación se detallan:
                    </p>
                    <asp:GridView ID="gvErrores" runat="server" CssClass="table table-bordered table-hover text-center" />
                </div>

                <div class="modal-footer justify-content-center border-0 pb-4 gap-3 flex-wrap">
                    <div class="d-grid" style="min-width: 240px;">
                        <asp:Button ID="btnRecomponer" runat="server" Text="Recomponer DV"
                            CssClass="btn btn-danger btn-lg w-100 shadow-sm rounded-pill" OnClick="btnRecomponer_Click" />
                    </div>
                    <div class="d-grid" style="min-width: 240px;">
                        <asp:Button ID="btnRestaurar" runat="server" Text="Restaurar Base"
                            CssClass="btn btn-outline-danger btn-lg w-100 shadow-sm rounded-pill" />
                    </div>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
