<%@ Page Async="true" Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="NexoMarket.Forms.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .section {
            width: 100%;
            display: flex;
            justify-content: center;
            margin: 20px 0;
            flex-direction: column;
            align-items: center;
        }

        .ad-container {
            width: 100%;
            max-width: 1200px;
            height: 200px;
            margin: 20px auto;
            display: flex;
            align-items: center;
            background: #f8f8f8;
            border-radius: 12px;
            overflow: hidden;
            box-shadow: 0 6px 20px rgba(0,0,0,.15);
        }

        .ad-container a {
            position: relative;
            display: block;
            height: 100%;
            width: 100%;
            text-align: left;
        }

        .ad-container img {
            width: 100%;
            height: 100%;
            display: block;
        }

        .ad-container a::after {
            content: attr(data-text);
            position: absolute;
            right: 20px;
            top: 50%;
            transform: translateY(-50%);
            font-size: 1.2rem;
            font-weight: 700;
            color: #333;
        }

        .grid-container {
            width: 80%;
            margin: 20px auto;
        }
    </style>

    <!-- Sección Logo -->
    <div class="section">
        <img src="../Assets/Images/LogoNexoMarketRecortado.png" style="max-width: 700px;" />
    </div>

    <!-- Sección Mapa -->
    <div class="section">
        <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d4022.246050290747!2d-58.40758623118068!3d-34.77098943653516!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x95bcd2fb5371a9e3%3A0xb37fec36914a63ad!2sUniversidad%20Abierta%20Interamericana%20-%20UAI%20Lomas!5e0!3m2!1ses!2sar!4v1758461679147!5m2!1ses!2sar" width="600" height="450" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>
    </div>

    <!-- Sección Publicidad (si aplica) -->
    <% if (Session["User"] != null && Session["User"].ToString() == "Cliente Visitante") { %>
        <div class="ad-container">
            <asp:AdRotator ID="AdRotatorPromo"
                runat="server"
                CssClass="adrotator"
                AdvertisementFile="~/Utils/AnunciosRotator.xml"
                KeywordFilter="supermercado"
                Target="_blank" />
        </div>
    <% } %>

    <!-- Sección GridView -->
    <div class="grid-container">
        <asp:Button runat="server" Text="Ver Estadisticas" OnClick="Unnamed1_Click"></asp:Button>
        <asp:GridView ID="gvTopProductos" runat="server" 
            AutoGenerateColumns="true" 
            CssClass="table table-bordered"
            HeaderStyle-BackColor="#f1f1f1"
            HeaderStyle-ForeColor="Black"
            BorderWidth="1"
            BorderColor="Gray"
            GridLines="Both">
        </asp:GridView>
    </div>
</asp:Content>
