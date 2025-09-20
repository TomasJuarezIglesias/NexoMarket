<%@ Page Title="Carrito" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="NexoMarket.NexoMarket.Carrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">

        <!-- Mensaje si no hay productos -->
        <asp:Panel ID="pnlVacio" runat="server" Visible="true" CssClass="text-center mt-5">
            <h4 class="text-muted">Tu carrito está vacío 🛒</h4>
            <p>Agregá productos desde el listado para comenzar tu compra.</p>
        </asp:Panel>

        <!-- Listado de productos en el carrito -->
        <asp:Repeater ID="rptCarrito" runat="server">
            <HeaderTemplate>
                <div class="row justify-content-center">
            </HeaderTemplate>

            <ItemTemplate>
                <div class="col-md-6 col-lg-4 mb-4">
                    <div class="card shadow-sm h-100">
                        <img src='<%# Eval("ImagenUrl") %>' class="card-img-top" alt="Producto" style="height:200px; object-fit:contain;" />
                        <div class="card-body d-flex flex-column">
                            <h5 class="card-title"><%# Eval("Nombre") %></h5>
                            <p class="card-text text-muted"><%# Eval("Descripcion") %></p>
                            <p class="fw-bold text-success">$<%# Eval("Precio") %></p>

                            <div class="d-flex align-items-center mb-2">
                                <label class="me-2">Cantidad:</label>
                                <asp:TextBox ID="txtCantidad" runat="server" Text='<%# Eval("Cantidad") %>' CssClass="form-control text-center" style="width:60px;" />
                            </div>

                            <div class="mt-auto d-flex justify-content-between">
                                <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CommandName="Actualizar" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-outline-primary btn-sm" />
                                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-danger btn-sm" />
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>

            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>

        <!-- Total y botón de finalizar compra -->
        <asp:Panel ID="pnlTotal" runat="server" CssClass="d-flex justify-content-end align-items-center mt-4" Visible="false">
            <h4 class="me-4">Total: <span class="text-success">$<asp:Label ID="lblTotal" runat="server" /></span></h4>
            <asp:Button ID="btnFinalizarCompra" runat="server" Text="Finalizar Compra" CssClass="btn btn-primary btn-lg" />
        </asp:Panel>
    </div>
</asp:Content>
