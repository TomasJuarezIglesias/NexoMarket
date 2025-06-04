using Newtonsoft.Json;
using NexoMarket.Business;
using NexoMarket.Entity;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace NexoMarket
{
    public partial class SiteMaster : MasterPage
    {
        public List<MenuEntity> MenusList;

        protected void Page_Load(object sender, EventArgs e)
        {
            var url = Request.Url.AbsolutePath.Replace("/NexoMarket/", "").ToLower();

            if (url.Contains("login"))
            {
                sidebar.Visible = false;
            }

            // Armado de sideBar
            if (Request.IsAuthenticated)
            {
                FormsIdentity identity = (FormsIdentity)HttpContext.Current.User.Identity;
                FormsAuthenticationTicket ticket = identity.Ticket;

                var userData = JsonConvert.DeserializeObject<UserAuthEntity>(ticket.UserData);
                MenusList = userData.AllowedMenues ?? new List<MenuEntity>();

                // Validación Ruta
                if (url != "inicio.aspx" && !MenusList.Exists(m => m.Url.ToLower() == url))
                {
                    Response.Redirect("Inicio.aspx");
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }


    }
}