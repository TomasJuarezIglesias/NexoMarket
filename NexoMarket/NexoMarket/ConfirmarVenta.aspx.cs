using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using NexoMarket.Business;
using NexoMarket.Data;
using NexoMarket.Entity;

namespace NexoMarket.NexoMarket
{
    public partial class ConfirmarVenta : System.Web.UI.Page
    {
        public ConfirmarVenta()
        {
            
        }
        VentaBusiness _ventaBusiness = new Business.VentaBusiness();
        ProductoBusiness _productoBusiness = new ProductoBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {         
            decimal total = _ventaBusiness.CalcularTotal();     
            lblTotal.Text = total.ToString("N2");
        }
        
        protected async void btnConfirmar_Click(object sender, EventArgs e)
        {
            var Venta = new VentaEntity();
            
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            UserAuthEntity user = JsonConvert.DeserializeObject<UserAuthEntity>(ticket.UserData);

            Venta.Fecha = DateTime.Now;
            Venta.Total = _ventaBusiness.CalcularTotal();
            Venta.Id_Usuario = user.Id;
            bool response =  await _ventaBusiness.AgregarVenta(Venta);
            if (!response)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertifyRegistro", $"showError('No se pudo registrar la compra');", true);
                return;
            }

            _productoBusiness.EmptyCart();
            Session["ShowMsgC"] = true;
            Response.Redirect("Producto.aspx");        
        }
    }
}