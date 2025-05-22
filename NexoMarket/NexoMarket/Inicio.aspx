<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="NexoMarket.Forms.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="display: flex; justify-content: center; margin-bottom:80px">
        <h1>Nexo Market</h1>
    </div>
    <div style="display: flex; justify-content: center" >
         <img src="../Assets/Images/LogoNexoMarket.jpg" style="height: 500px; width:500px"/>
    </div>

    <script>
        $(document).ready(() => {
            $.ajax({
                type: "POST",
                url: "<%= ResolveUrl("Inicio.aspx/GetRolUsuario") %>",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    const user = response.d
                    alertify.alert(`Bienvenido ${user.Username}`, `Tu rol en el sistema es: ${user.Rol}`);
                },
                error: function (xhr, status, error) {
                    alertify.error("Error al obtener el rol");
                }
            });
        });
    </script>
</asp:Content>


