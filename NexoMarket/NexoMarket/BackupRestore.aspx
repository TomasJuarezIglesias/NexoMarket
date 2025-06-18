<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BackupRestore.aspx.cs" Inherits="NexoMarket.NexoMarket.BackupRestore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid py-5">

        <!-- Título general sin fondo -->
        <div class="row justify-content-center mb-5">
            <div class="col-12 col-md-8">
                <div class="text-center">
                    <h2 class="m-0">Backup y Restore de Base de Datos</h2>
                </div>
            </div>
        </div>

        <!-- Sección de tarjetas -->
        <div class="row justify-content-center">
            <div class="col-12 col-md-6">

                <!-- Backup -->
                <div class="card shadow-sm rounded-4 mb-4 w-100">
                    <div class="card-header text-white text-center rounded-top-4" style="background-color: #0d6efd;">
                        Realizar Backup
                    </div>
                    <div class="card-body d-flex flex-column align-items-center">
                        <asp:Button ID="btnBackup" runat="server" CssClass="btn btn-primary w-100" Text="Generar Backup" OnClick="btnBackup_Click"></asp:Button>
                    </div>
                </div>

                <!-- Restore -->
                <div class="card shadow-sm rounded-4 w-100">
                    <div class="card-header text-white text-center rounded-top-4" style="background-color: #0d6efd;">
                        Restaurar Base de Datos
                    </div>
                    <div class="card-body d-flex flex-column align-items-center">
                        <div class="mb-3 w-100">
                            <label class="form-label">Seleccionar archivo .bak</label>
                            <asp:FileUpload ID="fileUploadRestore" runat="server" CssClass="form-control" />
                        </div>
                        <asp:Button ID="btnRestore" runat="server" CssClass="btn btn-primary w-100" Text="Restaurar Backup" OnClick="btnRestore_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
