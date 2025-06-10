using NexoMarket.Business;
using NexoMarket.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace NexoMarket.NexoMarket
{
    public partial class Producto : System.Web.UI.Page
    {
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
                var business = new BusinessProducto();
                var productos = await business.BuscarProductos();

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
    }
}
