using NexoMarket.Business;
using NexoMarket.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace NexoMarket.NexoMarket
{
    public partial class Carrito : System.Web.UI.Page
    {
        private readonly ProductoBusiness _productoBusiness;

        public Carrito()
        {
            _productoBusiness = new ProductoBusiness();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCarrito();
            }
        }

        private void CargarCarrito()
        {
            var products = _productoBusiness.GetProductsFromCart();

            var productosConBase64 = products.Select(p => new
            {
                Producto = new
                {
                    p.Product.Id,
                    p.Product.Nombre,
                    p.Product.Descripcion,
                    p.Product.Precio,
                    p.Product.Stock,
                    ImagenBase64 = p.Product.Imagen != null
                        ? "data:image/jpeg;base64," + Convert.ToBase64String(p.Product.Imagen)
                        : "https://via.placeholder.com/200x150?text=Sin+Imagen",
                },
                p.Cantidad
            }).ToList();

            if (productosConBase64.Any())
            {
                pnlVacio.Visible = false;
                pnlTotal.Visible = true;

                RepeaterProductos.DataSource = productosConBase64;
                RepeaterProductos.DataBind();

                decimal total = productosConBase64.Sum(p => p.Producto.Precio * p.Cantidad);
                lblTotal.Text = total.ToString("N2");
            }
            else
            {
                pnlVacio.Visible = true;
                pnlTotal.Visible = false;

                RepeaterProductos.DataSource = null;
                RepeaterProductos.DataBind();
            }
        }

        protected async void RepeaterProductos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (!int.TryParse(e.CommandArgument.ToString(), out int idProducto))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertifyRegistro", $"showError('No se encontró el producto');", true);
                return;
            }

            if (e.CommandName == "Editar")
            {
                var txtCantidad = (HtmlInputGenericControl)e.Item.FindControl("txtCantidad");
                var btnEditar = (Button)e.Item.FindControl("btnEditar");
                var btnEliminar = (Button)e.Item.FindControl("btnEliminar");

                var btnGuardar = (Button)e.Item.FindControl("btnGuardar");
                var btnCancelar = (Button)e.Item.FindControl("btnCancelar");

                txtCantidad.Attributes.Remove("readonly");

                btnEditar.Visible = false;
                btnEliminar.Visible = false;

                btnGuardar.Visible = true;
                btnCancelar.Visible = true;

                return;
            }
            else if (e.CommandName == "Guardar")
            {
                var txtCantidad = (HtmlInputGenericControl)e.Item.FindControl("txtCantidad");
                if (!int.TryParse(txtCantidad.Value, out int nuevaCantidad) || nuevaCantidad <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertifyRegistro", $"showError('La cantidad debe ser mayor a 0');", true);
                    return;
                }

                if (nuevaCantidad > 99)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertifyRegistro", $"showError('La cantidad debe ser menor a 30');", true);
                    return;
                }

                bool hasStock = await _productoBusiness.HasStock(idProducto, nuevaCantidad);

                if (!hasStock)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertifyRegistro", $"showError('No Hay Stock');", true);
                    return;
                }

                _productoBusiness.UpdateQuantity(idProducto, nuevaCantidad);

                ScriptManager.RegisterStartupScript(this, GetType(), "alertifyRegistro", $"showSuccess('Cantidad actualizada');", true);
            }
            else if (e.CommandName == "Eliminar")
            {
                _productoBusiness.RemoveFromCart(idProducto);
                ScriptManager.RegisterStartupScript(this, GetType(), "alertifyRegistro", $"showSuccess('Producto eliminado correctamente');", true);
            }

            CargarCarrito();
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            // Redireccion a siguiente pagina para planificar entrega y despues hacer confirmación de pedido
            // En la confirmación antes de ir a hacer los cambios a la db, se tiene que checkear la disponibilidad de stock
        }

        protected void btnVaciarCarrito_Click(object sender, EventArgs e)
        {
            _productoBusiness.EmptyCart();
            CargarCarrito();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertifyRegistro", $"showSuccess('Carrito vaciado correctamente');", true);
        }
    }
}