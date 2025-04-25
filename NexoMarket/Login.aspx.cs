using Newtonsoft.Json;
using NexoMarket.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NexoMarket.NexoMarket
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly UserRepository _userRepository;
        public Login()
        {
            _userRepository = new UserRepository();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/NexoMarket/Inicio.aspx");
            }
        }

        protected async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsuario.Text.Trim();
            string password = txtPassword.Text.Trim();

            lblError.Visible = false;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                lblError.Text = "Usuario o contraseña incorrectos.";
                lblError.Visible = true;
                return;
            }

            var user = await _userRepository.Login(username, password);

            if (user is null)
            {
                lblError.Text = "Credenciales Incorrectas";
                lblError.Visible = true;
                return;
            }

            var userData = JsonConvert.SerializeObject(new
            {
                user.Id,
                user.Username
            });


            var ticket = new FormsAuthenticationTicket(
                1,
                username,
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                false,
                userData,
                FormsAuthentication.FormsCookiePath
            );

            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true
            };

            Response.Cookies.Add(authCookie);

            Response.Redirect("~/NexoMarket/Inicio.aspx");
        }
    }
}