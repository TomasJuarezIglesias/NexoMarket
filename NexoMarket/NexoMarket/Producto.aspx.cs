using NexoMarket.Business;
using NexoMarket.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace NexoMarket.NexoMarket
{
    public partial class Producto : System.Web.UI.Page
    {
        private readonly ProductoBusiness _productoBusiness;

        public Producto()
        {
            _productoBusiness = new ProductoBusiness();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
            }
        }

        private async void CargarProductos()
        {
            try
            {
                var productos = await _productoBusiness.BuscarProductos();

                var productosConBase64 = productos.Select(p => new
                {
                    p.Id,
                    p.Nombre,
                    p.Descripcion,
                    p.Precio,
                    p.Stock,
                    ImagenBase64 = p.Imagen != null
                        ? "data:image/jpeg;base64," + Convert.ToBase64String(p.Imagen)
                        : "https://via.placeholder.com/200x150?text=Sin+Imagen"
                }).ToList();

                RepeaterProductos.DataSource = productosConBase64;
                RepeaterProductos.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<h2 style='color:red'>ERROR: " + ex.Message + "</h2>");
                Response.End();
            }
        }

        protected async void RepeaterProductos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Agregar")
            {
                if(!int.TryParse(e.CommandArgument.ToString(), out int idProducto))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertifyRegistro", $"showError('No se encontro el producto');", true);
                    return;
                }

                var cantidadControl = (HtmlInputGenericControl)e.Item.FindControl("Cantidad");
                int cantidad = int.Parse(cantidadControl.Value);

                bool hasStock = await _productoBusiness.HasStock(idProducto, cantidad);

                if(!hasStock)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertifyRegistro", $"showError('No Hay Stock');", true);
                    return;
                }

                var producto = await _productoBusiness.GetById(idProducto);

                _productoBusiness.AddToCart(producto, cantidad);

                cantidadControl.Value = "1";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertifyRegistro", $"showSuccess('Agregado {cantidad} unidad/es de {producto.Nombre}');", true);
            }
        }
    }
}
