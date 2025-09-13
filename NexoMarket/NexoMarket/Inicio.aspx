<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="NexoMarket.Forms.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
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

        

                .ad-container img {
                    height: 100%;
                    width: 50%; /* imagen solo en la izquierda */
                    object-fit: cover; /* recorta sin deformar */
                    display: block;
                }

                .ad-container a {
    position: relative;
    display: block;
    height: 100%;
    width: 100%;
    text-align: left;
}

.ad-container a::after {
    content: attr(data-text);
    position: absolute;
    right: 20px;
    top: 50%;
    transform: translateY(-50%);
    font-size: 1.2rem;
    font-weight: bold;
    color: #333;
}

.ad-container a{
  position:relative; display:block; height:100%; width:100%; text-align:left;
}
.ad-container a::after{
  content: attr(data-text);
  position:absolute; right:20px; top:50%; transform:translateY(-50%);
  font-size:1.2rem; font-weight:700; color:#333;
}
.ad-container img{
  height:100%; width:50%; object-fit:cover; display:block;
}

    </style>
    <div style="display: flex; justify-content: center; flex-direction: column; align-items: center; height: 95vh;">
        <img src="../Assets/Images/LogoNexoMarket.png" style="max-width: 700px;" />


    <% if (Session["User"] != null && Session["User"].ToString() == "Cliente Visitante") { %>
        <div class="ad-container">
         <asp:AdRotator ID="AdRotatorPromo"
            runat="server"
            CssClass="adrotator"
            AdvertisementFile="~/Utils/AnunciosRotator.xml"
            KeywordFilter="supermercado" 
            target ="_blank"
                OnAdCreated="AdRotatorPromo_AdCreated"/>
            
           </div>
        </div>
<% } %>
</asp:Content>