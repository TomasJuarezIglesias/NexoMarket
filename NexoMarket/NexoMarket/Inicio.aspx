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

            .modern-table {
    width: 80%;
    margin: 20px auto;
    border-collapse: collapse;
    font-family: 'Segoe UI', sans-serif;
    box-shadow: 0 4px 12px rgba(0,0,0,0.1);
    border-radius: 10px;
    overflow: hidden;
}

.modern-table th, .modern-table td {
    padding: 12px 16px;
    text-align: center;
}

.table-header th {
    background-color: #0c427e;
    color: #fff;
    font-weight: 600;
    font-size: 1rem;
}

.table-row td {
    background-color: #ffffff;
    color: #333;
    font-size: 0.95rem;
    border-bottom: 1px solid #eaeaea;
}

.table-row-alt td {
    background-color: #f6f8fb;
    color: #333;
    border-bottom: 1px solid #eaeaea;
}

.modern-table tr:hover td {
    background-color: #e4ebf7;
    transition: background-color 0.3s ease;
}

        }
    </style>

    <!-- Sección Logo -->
    <div class="section">
        <img src="../Assets/Images/LogoNexoMarketRecortado.png" style="max-width: 700px;" />
    </div>

    <!-- Sección Mapa -->


    <!-- Sección Publicidad (si aplica) -->
    <% if (Session["User"] != null && Session["User"].ToString() == "Cliente Visitante")
        { %>
    <div class="section">
        <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d4022.246050290747!2d-58.40758623118068!3d-34.77098943653516!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x95bcd2fb5371a9e3%3A0xb37fec36914a63ad!2sUniversidad%20Abierta%20Interamericana%20-%20UAI%20Lomas!5e0!3m2!1ses!2sar!4v1758461679147!5m2!1ses!2sar" width="600" height="450" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>
    </div>
    <div class="ad-container">
        <asp:AdRotator ID="AdRotatorPromo"
            runat="server"
            CssClass="adrotator"
            AdvertisementFile="~/Utils/AnunciosRotator.xml"
            KeywordFilter="supermercado"
            Target="_blank" />
    </div>
    <% } %>

    <% if (Session["User"] != null && Session["User"].ToString() == "Web Master")
        { %>
    <div class="section">
        <!-- Sección GridView -->
        <div class="grid-container">
          <%--  <asp:Button runat="server" Text="Ver Estadisticas" OnClick="Unnamed1_Click"></asp:Button>--%>
            <asp:GridView ID="gvTopProductos" runat="server" AutoGenerateColumns="False"
                CssClass="modern-table"
                HeaderStyle-CssClass="table-header"
                RowStyle-CssClass="table-row"
                AlternatingRowStyle-CssClass="table-row-alt"
                BorderWidth="0"
                GridLines="None">
                <Columns>
                    <asp:BoundField DataField="Orden" HeaderText="Orden" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Facturacion" HeaderText="Facturación"
                        DataFormatString="{0:N2}" HtmlEncode="False" />
                    <asp:BoundField DataField="Porcentaje" HeaderText="% Sobre Total"
                        DataFormatString="{0:N2} %" HtmlEncode="False" />
                </Columns>
            </asp:GridView>
        </div>
        <% } %>
</asp:Content>
