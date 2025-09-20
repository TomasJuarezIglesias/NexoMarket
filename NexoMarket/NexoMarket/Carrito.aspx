<%@ Page Async="true" Title="Carrito" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="NexoMarket.NexoMarket.Carrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex flex-column" style="height: calc(100vh - 60px);">

        <!-- Mensaje si no hay productos -->
        <asp:Panel ID="pnlVacio" runat="server" Visible="false" CssClass="text-center mt-5">
            <h4 class="text-muted">Tu carrito está vacío 🛒</h4>
            <p>Agregá productos desde el listado para comenzar tu compra.</p>
        </asp:Panel>

        <!-- Listado de productos -->
        <div class="flex-grow-1 overflow-auto pe-2">
            <asp:Repeater ID="RepeaterProductos" runat="server" OnItemCommand="RepeaterProductos_ItemCommand">
                <HeaderTemplate>
                    <div class="d-flex flex-column gap-3">
                </HeaderTemplate>

                <ItemTemplate>
                    <div class="card shadow-sm p-3 d-flex flex-row align-items-center">
                        <!-- Imagen -->
                        <div style="width: 120px;" class="me-3 text-center">
                            <img src='<%# Eval("Producto.ImagenBase64") %>'
                                alt="Producto"
                                class="img-fluid rounded"
                                style="max-height: 100px; object-fit: contain;" />
                        </div>

                        <!-- Datos del producto -->
                        <div class="flex-grow-1">
                            <h5 class="mb-1 fw-bold"><%# Eval("Producto.Nombre") %></h5>
                            <p class="text-muted mb-2"><%# Eval("Producto.Descripcion") %></p>

                            <div class="d-flex align-items-center mb-2">
                                <span class="fw-bold text-success me-3">
                                    $<%# Eval("Producto.Precio", "{0:N2}") %></span><label class="me-2">Cantidad:</label>

                                <input type="number"
                                    id="txtCantidad" 
                                    runat="server"
                                    class="form-control text-center"
                                    style="width: 70px;"
                                    value='<%# Eval("Cantidad") %>'
                                    min="1"
                                    max="99"
                                    readonly="readonly" />
                            </div>

                            <p class="fw-bold mb-0">
                                Subtotal: 
                                <span class="text-primary">
                                    $<%# (Convert.ToDecimal(Eval("Producto.Precio")) * Convert.ToInt32(Eval("Cantidad"))).ToString("N2") %></span></p>
                        </div>

                        <!-- Botones -->
                        <div class="ms-3 d-flex flex-column gap-2">
                            <asp:Button ID="btnEditar" runat="server" Text="Actualizar"
                                CssClass="btn btn-outline-primary btn-sm"
                                CommandName="Editar"
                                CommandArgument='<%# Eval("Producto.Id") %>'
                                UseSubmitBehavior="False" />

                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar"
                                CssClass="btn btn-success btn-sm"
                                CommandName="Guardar"
                                CommandArgument='<%# Eval("Producto.Id") %>'
                                UseSubmitBehavior="False" Visible="false" />

                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar"
                                CssClass="btn btn-danger btn-sm"
                                CommandName="Eliminar"
                                CommandArgument='<%# Eval("Producto.Id") %>'
                                UseSubmitBehavior="False" />
                        </div>
                    </div>
                </ItemTemplate>

                <FooterTemplate>
                    </div>
                </FooterTemplate>
            </asp:Repeater>
        </div>

        <!-- Total y Acciones -->
        <asp:Panel ID="pnlTotal" runat="server" CssClass="bg-light border-top shadow-sm p-3 mt-3" Visible="false">
            <div class="d-flex justify-content-between align-items-center flex-wrap gap-3">
                <h4 class="mb-0">
                    Total: <span class="text-success">$<asp:Label ID="lblTotal" runat="server" /></span>
                </h4>
                <div class="d-flex gap-2">
                    <asp:Button ID="btnVaciarCarrito" runat="server" Text="Vaciar carrito"
                        CssClass="btn btn-outline-danger btn-lg"
                        UseSubmitBehavior="False"
                        OnClick="btnVaciarCarrito_Click"
                        OnClientClick="confirmVaciarCarrito(); return false;" />
                    <asp:Button ID="btnFinalizarCompra" runat="server" Text="Finalizar pedido" 
                        CssClass="btn btn-success btn-lg" 
                        UseSubmitBehavior="False" OnClick="btnFinalizarCompra_Click" />
                </div>
            </div>
        </asp:Panel>
    </div>

    <script>
        function confirmVaciarCarrito() {
            alertify.confirm(
                'Vaciar carrito',
                '¿Estás seguro de que querés eliminar todos los productos del carrito?',
                function () {
                    // Si confirma, postback al botón real
                    __doPostBack('<%= btnVaciarCarrito.UniqueID %>', '');
                },
                () => { }
            ).set('labels', { ok: 'Sí, vaciar', cancel: 'Cancelar' })
                .set('closable', false)
                .set('transition', 'pulse')
                .set('reverseButtons', true)
                .set('defaultFocus', 'cancel');
        }

    </script>

</asp:Content>
