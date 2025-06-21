<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BackupRestore.aspx.cs" Inherits="NexoMarket.NexoMarket.BackupRestore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid py-5">

        <!-- Título principal -->
        <div class="row justify-content-center mb-5">
            <div class="col-12 col-md-8 text-center">
                <h2 class="fw-bold mb-2">Backup y Restore de Base de Datos</h2>
                <p class="text-muted">Gestione sus copias de seguridad y restauraciones de forma segura</p>
            </div>
        </div>

        <!-- Contenedor central -->
        <div class="row justify-content-center">
            <div class="col-12 col-md-6">

                <!-- Sección Backup -->
                <div class="card shadow-sm rounded-4 mb-4 w-100 border-0">
                    <div class="card-header text-white text-center rounded-top-4 fw-semibold" style="background-color: #0d6efd;">
                        <i class="fas fa-database me-2"></i> Realizar Backup
                    </div>
                    <div class="card-body d-flex flex-column align-items-center">
                        <asp:Button ID="btnBackup" runat="server" CssClass="btn btn-primary w-100 fw-semibold py-2" Text="Generar Backup" OnClick="btnBackup_Click" />
                    </div>
                </div>

                <!-- Sección Restore -->
                <div class="card shadow-sm rounded-4 w-100 border-0">
                    <div class="card-header text-white text-center rounded-top-4 fw-semibold" style="background-color: #0d6efd;">
                        <i class="fas fa-upload me-2"></i> Restaurar Base de Datos
                    </div>
                    <div class="card-body d-flex flex-column align-items-center">
                        <div class="mb-3 w-100 d-flex flex-column align-items-center">
                            <label class="form-label fw-semibold">Seleccionar archivo <code>.bak</code></label>
                            <asp:FileUpload ID="fileUploadRestore" runat="server" CssClass="form-control" />
                        </div>
                        <asp:Button ID="btnRestore" runat="server" CssClass="btn btn-primary w-100 fw-semibold py-2" Text="Restaurar Backup" OnClick="btnRestore_Click" />
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
