using Newtonsoft.Json;
using NexoMarket.Business;
using NexoMarket.Entity;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;

namespace NexoMarket.NexoMarket
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly BusinessUser _businessUser;
        private readonly BusinessBitacora _businessBitacora;
        private readonly BusinessMenu _businessMenu;

        public Login()
        {
            _businessUser = new BusinessUser();
            _businessBitacora = new BusinessBitacora();
            _businessMenu = new BusinessMenu();
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
            if (response.Mensaje == "Credenciales Incorrectas")
            {
                // Crear el diccionario si no existe
                if (Session["Intentos"] == null)
                {
                    Dictionary<string, int> intentos = new Dictionary<string, int>();
                    intentos.Add(username, 1);
                    Session["Intentos"] = intentos;
                }
                else
                {
                    // Recuperar y actualizar el diccionario
                    var intentos = (Dictionary<string, int>)Session["Intentos"];

                    if (intentos.ContainsKey(username))
                        intentos[username]++;
                    else
                        intentos[username] = 1;

                    Session["Intentos"] = intentos;

                    if (intentos[username] >= 3)
                    {
                        await _businessUser.BlockUser(username);
                        lblError.Text = "Usuario Bloqueado";
                        lblError.Visible = true;
                        return;
                    }
                }
            }

            if (!response.Ok)
            {
                lblError.Text = response.Mensaje;
                lblError.Visible = true;
                return;
            }

            // Limpio los intentos almacenados
            Session.Remove("Intentos");

            var allowedMenues = _businessMenu.GetMenusByUser(response.Data.Id);

            var userData = JsonConvert.SerializeObject(new UserAuthEntity
            {
                Id = response.Data.Id,
                Username = response.Data.Username,
                Rol = response.Data.Rol.Nombre,
                AllowedMenues = allowedMenues
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

            await _businessBitacora.GuardarEventoBitacora("Inicio de Sesion", response.Data.Id);

            Response.Redirect("~/NexoMarket/Inicio.aspx");
        }

       
    }
}