<%@ Page Title="BitacoraEventos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BitacoraEventos.aspx.cs" Inherits="NexoMarket.Forms.BitacoraEventos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="display: flex; justify-content: center; flex-direction : column; margin-bottom:80px">
        <h1>Bitacora Eventos</h1>
            <asp:GridView ID="DataBitacora" runat="server" Height="276px"  Width="80%" CssClass="modern-gridview" AllowPaging="True" OnPageIndexChanging="DataBitacora_PageIndexChanging">
            </asp:GridView>
        
    </div>
         &nbsp;
</asp:Content>
