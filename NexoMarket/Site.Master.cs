using Newtonsoft.Json;
using NexoMarket.Business;
using NexoMarket.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace NexoMarket
{
    public partial class SiteMaster : MasterPage
    {
        public List<MenuEntity> MenusList;
        private readonly BusinessBitacora _businessBitacora;

        public SiteMaster()
        {
            MenusList = new List<MenuEntity>();
            _businessBitacora = new BusinessBitacora();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            var url = Request.Url.AbsolutePath.Replace("/NexoMarket/", "").Replace(".aspx","").ToLower();

            if (url.Contains("login"))
            {
                sidebar.Visible = false;
            }

            // Armado de sideBar
            if (Request.IsAuthenticated)
            {
                var userData = GetUserAuthenticated();
                MenusList = userData.AllowedMenues ?? new List<MenuEntity>();

                // Validación Ruta
                if ((url != "inicio.aspx" && url != "inicio") && !MenusList.Exists(m => m.Url.Replace(".aspx","").ToLower() == url))
                {
                    Response.Redirect("Inicio.aspx");
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            var user = GetUserAuthenticated();

            // Ejecuión en segundo plano
            Task.Run(() => _businessBitacora.GuardarEventoBitacora("Logout", user.Id));

            FormsAuthentication.SignOut();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }

        private UserAuthEntity GetUserAuthenticated()
        {
            FormsIdentity identity = (FormsIdentity)HttpContext.Current.User.Identity;
            FormsAuthenticationTicket ticket = identity.Ticket;

            return JsonConvert.DeserializeObject<UserAuthEntity>(ticket.UserData);
        }

    }
}