using Newtonsoft.Json;
using NexoMarket.Business;
using NexoMarket.Entity;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace NexoMarket.NexoMarket
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly BusinessUser _businessUser;
        private readonly BusinessBitacora _businessBitacora;
        private readonly BusinessMenu _businessMenu;
        private readonly BusinessDigitoVerificador _businessDigitoVerificador;

        public Login()
        {
            _businessUser = new BusinessUser();
            _businessBitacora = new BusinessBitacora();
            _businessMenu = new BusinessMenu();
            _businessDigitoVerificador = new BusinessDigitoVerificador();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/NexoMarket/Inicio.aspx");
            }
            if (IsPostBack)
            {
                if (LabelErrorRegister.Visible)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "abrirModal", "$('#registroModal').modal('show');", true);
                }
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

            var validationResponse = await _businessDigitoVerificador.Verificar();

            if (!validationResponse.Ok)
            {
                if (response.Data.Rol.Nombre != "Web Master")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertifyRegistro", "alertify.alert('Sistema no disponible', 'El sistema no se encuentra disponible en este momento. Por favor, contacte al administrador para más información.');", true);
                    return;
                }

                gvErrores.DataSource = validationResponse.Data;
                gvErrores.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "abrirModalErrores","new bootstrap.Modal(document.getElementById('modalErroresDV')).show();",true);
                return;
            }

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

        protected async void btn_registrarse(object sender, EventArgs e)
        {
            if (password_register.Text != repassword_register.Text)
            {
                LabelErrorRegister.Text = "Las contraseñas no coinciden";
                LabelErrorRegister.Visible = true;
                return;
            }

            UserCreateEntity user = new UserCreateEntity()
            {
                Id_Rol = 2, //Siempre Cliente
                Nombre = nombre_register.Text,
                Apellido = apellido_register.Text,
                Username = usuario_register.Text,
                Password = password_register.Text,
            };
            bool resultado = await _businessUser.CreateUser(user);
            if (resultado)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertifyRegistro", "alertify.success('¡Usuario Registrado!');", true);
                usuario_register.Text = "";
                nombre_register.Text = "";
                apellido_register.Text = "";
                password_register.Text = "";
                repassword_register.Text = "";
                return;
            }
        }

        protected async void btnRecomponer_Click(object sender, EventArgs e)
        {
            await _businessDigitoVerificador.Recomponer();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertifyRegistro", "alertify.success('Digito verificador recalculado correctamente');", true);
        }
    }
}