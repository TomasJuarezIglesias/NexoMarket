<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Async="true" CodeBehind="Producto.aspx.cs" Inherits="NexoMarket.NexoMarket.Producto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .producto-scroll-wrapper {
            flex: 1;
            overflow-y: auto;
            overflow-x: auto;
            padding-right: 10px;
        }

        .producto-grid {
            display: flex;
            flex-wrap: wrap;
            gap: 20px;
            justify-content: flex-start;
            max-width: 1400px;
            margin: auto;
        }

        .producto-card {
            width: 200px;
            height: 430px;
            display: flex;
            flex-direction: column;
            justify-content: space-between;
            border: 1px solid #dee2e6;
            border-radius: 12px;
            padding: 12px;
            text-align: center;
            background-color: #ffffff;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.05);
            transition: transform 0.2s ease, box-shadow 0.2s ease;
            flex-shrink: 0;
        }

            .producto-card:hover {
                transform: translateY(-4px);
                box-shadow: 0 6px 14px rgba(0, 0, 0, 0.15);
            }

            .producto-card img {
                width: 100%;
                height: 120px;
                object-fit: contain;
                border-radius: 8px;
                background: #f8f9fa;
                margin-bottom: 10px;
            }

        .producto-nombre {
            font-weight: 600;
            font-size: 1.05rem;
            margin-bottom: 5px;
            min-height: 40px;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .producto-descripcion {
            font-size: 0.9rem;
            color: #6c757d;
            margin: 6px 0;
            flex-grow: 1;
            overflow: hidden;
        }

        .producto-precio {
            color: #28a745;
            font-weight: 600;
            font-size: 1.1em;
            margin: 6px 0;
        }

        .producto-cantidad {
            margin-bottom: 10px;
        }

            .producto-cantidad input {
                width: 60px;
                text-align: center;
            }

        .producto-boton {
            margin-top: auto;
        }
    </style>


    <div class="d-flex flex-column" style="height: calc(96vh - 32px); overflow: hidden;">
        <h2 class="text-center mb-3">Listado de Productos</h2>

        <div class="producto-scroll-wrapper">
            <div class="producto-grid">
                <asp:Repeater ID="RepeaterProductos" runat="server" OnItemCommand="RepeaterProductos_ItemCommand">
                    <ItemTemplate>
                        <div class="producto-card">
                            <img src='<%# Eval("ImagenBase64") %>' alt="Imagen producto" />
                            <div class="producto-nombre"><%# Eval("Nombre") %></div>
                            <div class="producto-descripcion"><%# Eval("Descripcion") %></div>
                            <div class="producto-precio">$<%# Eval("Precio", "{0:N2}") %></div>
                            <div class="producto-cantidad">
                                <input type="number" id="Cantidad" runat="server" min="1" max="10" value="1" class="form-control d-inline-block" />
                            </div>
                            <div class="producto-boton">
                                <asp:Button ID="BtnAgregar" runat="server" Text="Agregar" CssClass="btn btn-primary btn-sm w-100"
                                    CommandName="Agregar"
                                    CommandArgument='<%# Eval("Nombre") %>' />
                            </div>
                        </div>
                    </ItemTemplate>

                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>
