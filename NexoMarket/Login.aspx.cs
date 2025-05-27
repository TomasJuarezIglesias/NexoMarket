using Newtonsoft.Json;
using NexoMarket.Business;
using NexoMarket.Entity.Dtos;
using NexoMarket.Entity.Entities;
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
        private readonly BusinessUser _businessUser;
        private readonly BusinessBitacora _businessBitacora;

        public Login()
        {
            _businessUser = new BusinessUser();
            _businessBitacora = new BusinessBitacora();
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

            var response = await _businessUser.Login(username, password);

            //Si escribio erroneamente la contraseña
            if(response.Mensaje == "Credenciales Incorrectas")
            {
                Session["Intentos"] = 
            }

            if (!response.Ok)
            {
                lblError.Text = response.Mensaje;
                lblError.Visible = true;
                return;
            }

            var userData = JsonConvert.SerializeObject(new UserAuth
            {
                Id = response.Data.Id,
                Username = response.Data.Username,
                Rol = response.Data.Rol.Nombre
            });

            Session.Remove("Intentos");

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
            await _businessBitacora.GuardarEventoBitacora("Inicio de Sesion", response.Data.Id);
            Response.Redirect("~/NexoMarket/Inicio.aspx");
        }
    }
}