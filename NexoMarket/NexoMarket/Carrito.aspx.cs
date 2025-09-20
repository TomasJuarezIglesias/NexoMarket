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

        protected void RepeaterProductos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (!int.TryParse(e.CommandArgument.ToString(), out int idProducto))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertifyRegistro", $"showError('No se encontro el producto');", true);
                return;
            }


            if (e.CommandName == "Actualizar")
            {



            }
            else if (e.CommandName == "Eliminar")
            {
                _productoBusiness.RemoveFromCart(idProducto);
                ScriptManager.RegisterStartupScript(this, GetType(), "alertifyRegistro", $"showSuccess('Producto eliminado correctamente');", true);
            }


            CargarCarrito();
        }
    }
}