using Newtonsoft.Json;
using NexoMarket.Data.Dtos;
using NexoMarket.Data.Repository;
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
        private readonly MenuRepository _menuRepository;

        public SiteMaster()
        {
            _menuRepository = new MenuRepository();
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

                var userData = JsonConvert.DeserializeObject<UserDto>(ticket.UserData);
                int userId = userData.Id;

                MenusList = _menuRepository.GetMenusByUser(userId);
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