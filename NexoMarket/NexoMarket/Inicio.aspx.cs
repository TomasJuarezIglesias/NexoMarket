using Newtonsoft.Json;
using NexoMarket.Entity;
using System;
using System.Web;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.Services;

namespace NexoMarket.Forms
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static UserAuthEntity GetRolUsuario()
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

            UserAuthEntity user = JsonConvert.DeserializeObject<UserAuthEntity>(ticket.UserData);
            return user;                
        }
    }
}