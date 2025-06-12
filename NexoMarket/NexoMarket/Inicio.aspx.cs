using Newtonsoft.Json;
using NexoMarket.Entity;
using System;
using System.Web;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;

namespace NexoMarket.Forms
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MostrarRol();
        }

        public void MostrarRol()
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

            UserAuthEntity user = JsonConvert.DeserializeObject<UserAuthEntity>(ticket.UserData);

            string title = $"Bienvenido {user.Username}";
            string message = $"Tu rol en el sistema es: {user.Rol}";

            ScriptManager.RegisterStartupScript(this, GetType(), "alertifyRegistro", $"alertify.alert('{title}', '{message}');", true);         
        }
    }
}