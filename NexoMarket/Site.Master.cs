using Newtonsoft.Json;
using NexoMarket.Business;
using NexoMarket.Entity.Dtos;
using NexoMarket.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NexoMarket
{
    public partial class SiteMaster : MasterPage
    {
        public List<MenuDto> MenusList = new List<MenuDto>();
        private readonly BusinessMenu _businessMenu;

        public SiteMaster()
        {
            _businessMenu = new BusinessMenu();
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.Url.AbsolutePath.ToLower().Contains("login"))
            {
                sidebar.Visible = false;
            }

            if (Request.IsAuthenticated)
            {
                FormsIdentity identity = (FormsIdentity)HttpContext.Current.User.Identity;
                FormsAuthenticationTicket ticket = identity.Ticket;

                var userData = JsonConvert.DeserializeObject<UserAuth>(ticket.UserData);
                int userId = userData.Id;

                MenusList = _businessMenu.GetMenusByUser(userId);
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