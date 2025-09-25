using Newtonsoft.Json;
using NexoMarket.Business;
using NexoMarket.Entity;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace NexoMarket.Forms
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MostrarRol();
        }
        ProductoBusiness _businessProducto = new ProductoBusiness();
        public void MostrarRol()
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

            UserAuthEntity user = JsonConvert.DeserializeObject<UserAuthEntity>(ticket.UserData);

            string title = $"Bienvenido {user.Username}";
            string message = $"Tu rol en el sistema es: {user.Rol}";
            Session["User"] = user.Rol;
            ScriptManager.RegisterStartupScript(this, GetType(), "alertifyRegistro", $"alertify.alert('{title}', '{message}');", true);         
        }

        public async Task EjecutarWebServiceAsync()
        {
            EstadisticaService ws = new EstadisticaService();
            var resultado = await _businessProducto.GetTop5();
            var dg_result = ws.GetTopProductos(resultado);
            gvTopProductos.DataSource = dg_result;
            gvTopProductos.DataBind();
        }

        protected async void Unnamed1_Click(object sender, EventArgs e)
        {
            await EjecutarWebServiceAsync();
        }
    }
}