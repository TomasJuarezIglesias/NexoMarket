<%@ Page Async = "true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConfirmarVenta.aspx.cs" Inherits="NexoMarket.NexoMarket.ConfirmarVenta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4">
        <div class="row justify-content-center">
            <div class="col-lg-8">

                <!-- Encabezado -->
                <div class="d-flex align-items-center mb-3">
                    <a href="Carrito.aspx" class="text-decoration-none me-2">
                        <i class="bi bi-arrow-left"></i> Volver al carrito
                    </a>
                </div>

                <!-- Card -->
                <div class="card shadow-sm">
                    <div class="card-body">
                        <h3 class="card-title mb-3">Confirmar datos de entrega</h3>
                        <p class="text-muted mb-4">Completá la dirección donde querés recibir tu pedido.</p>

                        <!-- Resumen (opcional) -->
                        <asp:Panel ID="pnlResumen" runat="server" CssClass="alert alert-light border mb-4" Visible="true">
                            <div class="d-flex justify-content-between align-items-center flex-wrap gap-2">
                                <span class="fw-semibold">Total del pedido:</span>
                                <span class="fs-4 text-success">$<asp:Label ID="lblTotal" runat="server" /></span>
                            </div>
                        </asp:Panel>

                        <!-- Mensajes de validación -->
                        <asp:ValidationSummary ID="vsErrores" runat="server"
                            CssClass="alert alert-danger"
                            HeaderText="Revisá estos campos:"
                            ValidationGroup="confirm" />

                        <!-- Layout 6 / 6 para alinear Número con Ciudad -->
                        <div class="row g-3">

                            <!-- COLUMNA IZQUIERDA -->
                            <div class="col-md-6">
                                <!-- Calle -->
                                <label for="txtCalle" class="form-label">Calle</label>
                                <asp:TextBox ID="txtCalle" runat="server" CssClass="form-control" MaxLength="100" />
                                <asp:RequiredFieldValidator ID="rfvCalle" runat="server"
                                    ControlToValidate="txtCalle"
                                    ErrorMessage="La calle es obligatoria."
                                    Display="Dynamic" CssClass="text-danger"
                                    ValidationGroup="confirm" />

                                <!-- Piso/Depto (opcional) -->
                                <div class="mt-3">
                                    <label for="txtPisoDepto" class="form-label">Piso / Depto (opcional)</label>
                                    <asp:TextBox ID="txtPisoDepto" runat="server" CssClass="form-control" MaxLength="20" />
                                </div>

                                <!-- Código Postal -->
                                <div class="mt-3">
                                    <label for="txtCP" class="form-label">Código Postal</label>
                                    <asp:TextBox ID="txtCP" runat="server" CssClass="form-control" MaxLength="10" />
                                    <asp:RequiredFieldValidator ID="rfvCP" runat="server"
                                        ControlToValidate="txtCP"
                                        ErrorMessage="El código postal es obligatorio."
                                        Display="Dynamic" CssClass="text-danger"
                                        ValidationGroup="confirm" />
                                    <asp:RegularExpressionValidator ID="revCP" runat="server"
                                        ControlToValidate="txtCP"
                                        ValidationExpression="^([A-Za-z]\d{4}[A-Za-z]{3}|\d{4,5})$"
                                        ErrorMessage="Ingresá un CP válido (e.g. 1704 o C1425ABC)."
                                        Display="Dynamic" CssClass="text-danger"
                                        ValidationGroup="confirm" />
                                </div>

                            </div>

                            <!-- COLUMNA DERECHA -->
                            <div class="col-md-6">
                                <!-- Número -->
                                <label for="txtNumero" class="form-label">Número</label>
                                <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control text-end" MaxLength="10" TextMode="Number" />
                                <asp:RequiredFieldValidator ID="rfvNumero" runat="server"
                                    ControlToValidate="txtNumero"
                                    ErrorMessage="El número es obligatorio."
                                    Display="Dynamic" CssClass="text-danger"
                                    ValidationGroup="confirm" />
                                <asp:RegularExpressionValidator ID="revNumero" runat="server"
                                    ControlToValidate="txtNumero"
                                    ValidationExpression="^\d{1,10}$"
                                    ErrorMessage="Ingresá solo números."
                                    Display="Dynamic" CssClass="text-danger"
                                    ValidationGroup="confirm" />

                                <!-- Ciudad -->
                                <div class="mt-3">
                                    <label for="txtCiudad" class="form-label">Ciudad</label>
                                    <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control" MaxLength="60" />
                                    <asp:RequiredFieldValidator ID="rfvCiudad" runat="server"
                                        ControlToValidate="txtCiudad"
                                        ErrorMessage="La ciudad es obligatoria."
                                        Display="Dynamic" CssClass="text-danger"
                                        ValidationGroup="confirm" />
                                </div>

                                <div class="mt-3">
                                    <label for="txtFechaEntrega" class="form-label">Fecha de entrega</label>
                                    <asp:TextBox ID="txtFechaEntrega" runat="server" CssClass="form-control" TextMode="Date" />
                                    <asp:RequiredFieldValidator ID="rfvFechaEntrega" runat="server"
                                        ControlToValidate="txtFechaEntrega"
                                        ErrorMessage="La fecha de entrega es obligatoria."
                                        Display="Dynamic" CssClass="text-danger"
                                        ValidationGroup="confirm" />
                                    <asp:CustomValidator ID="cvFechaEntrega" runat="server"
                                        ControlToValidate="txtFechaEntrega"
                                        ErrorMessage="La fecha debe estar dentro de los próximos 10 días."
                                        Display="Dynamic" CssClass="text-danger"
                                        ValidationGroup="confirm"
                                        ClientValidationFunction="validarFechaEntrega" />
                                </div>
                            </div>

                            <!-- Aclaraciones (ancho completo) -->
                            <div class="col-12">
                                <label for="txtAclaraciones" class="form-label">Aclaraciones para el repartidor (opcional)</label>
                                <asp:TextBox ID="txtAclaraciones" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="250" />
                                <div class="form-text">Ej: “Portón negro”, “timbre que no funciona”, “dejar en portería”.</div>
                            </div>
                        </div>

                        <!-- Acciones -->
                        <div class="d-flex justify-content-end mt-4">
                            <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar compra"
                                CssClass="btn btn-success"
                                UseSubmitBehavior="False"
                                ValidationGroup="confirm"
                                OnClick="btnConfirmar_Click"
                                OnClientClick="return confirmarCompra();" />
                        </div>

                    </div>
                </div>

            </div>
        </div>
    </div>

    <script type="text/javascript">
        function confirmarCompra() {
            if (typeof (Page_ClientValidate) === 'function') {
                if (!Page_ClientValidate('confirm')) return false;
            }

            alertify.confirm(
                'Confirmar compra',
                '¿Querés confirmar la compra con esta dirección?',
                function () {
                    __doPostBack('<%= btnConfirmar.UniqueID %>', '');
                },
                function () { }
            ).set('labels', { ok: 'Sí, confirmar', cancel: 'Cancelar' })
                .set('closable', false)
                .set('transition', 'pulse')
                .set('reverseButtons', true)
                .set('defaultFocus', 'cancel');

            return false;
        }

         function validarFechaEntrega(sender, args) {
            var fechaSeleccionada = args.Value;
            if (!fechaSeleccionada) {
                args.IsValid = false;
                return;
            }

            var hoy = new Date();
            var max = new Date();
            max.setDate(hoy.getDate() + 10);

            var seleccion = new Date(fechaSeleccionada);
            seleccion.setHours(0, 0, 0, 0);
            hoy.setHours(0, 0, 0, 0);
            max.setHours(0, 0, 0, 0);

            args.IsValid = (seleccion >= hoy && seleccion <= max);
        }
    </script>
</asp:Content>
