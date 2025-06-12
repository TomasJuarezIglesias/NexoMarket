<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Async="true" CodeBehind="Producto.aspx.cs" Inherits="NexoMarket.NexoMarket.Producto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .producto-grid {
            display: flex;
            flex-wrap: wrap;
            gap: 20px;
        }

        .producto-card {
            width: 200px;
            border: 1px solid #ccc;
            border-radius: 8px;
            padding: 10px;
            text-align: center;
            background-color: #fdfdfd;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }

        .producto-card img {
            width: 100%;
            height: 150px;
            object-fit: cover;
            border-radius: 4px;
        }

        .producto-nombre {
            font-weight: bold;
            margin-top: 10px;
        }

        .producto-precio {
            color: green;
            font-size: 1.1em;
            margin-top: 5px;
            margin-bottom: 5px;
        }

        .producto-descripcion {
            font-size: 0.9em;
            margin: 5px 0;
        }
    </style>

    <h1 style="text-align:center" class="mb-4">Nexo Market</h1>
    <div class="producto-grid">
        
        <asp:Repeater ID="RepeaterProductos" runat="server" OnItemCommand="RepeaterProductos_ItemCommand">

            <ItemTemplate>
                <div class="producto-card">
                    <img src='<%# Eval("ImagenBase64") %>' alt="Imagen producto" />
                    <div class="producto-nombre"><%# Eval("Nombre") %></div>
                    <div class="producto-descripcion"><%# Eval("Descripcion") %></div>
                    <div class="producto-precio">$<%# Eval("Precio", "{0:N2}") %></div>
                    <div class="producto-precio">
                        <input type="number" id="Cantidad" runat="server" min="1" max="10" value="1" class="form-control" style="width: 60px; display: inline-block;" />
                    </div>
                    <asp:Button ID="BtnAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary"
                        CommandName="Agregar"
                        CommandArgument='<%# Eval("Nombre") %>' />
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
