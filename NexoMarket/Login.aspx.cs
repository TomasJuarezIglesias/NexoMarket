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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("Inicio.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUsuario.Text.Trim();
            string pass = txtPassword.Text;

            if (user == "" && pass == "")
            {
                lblError.Text = "Usuario o contraseña incorrectos.";
                return;
            }

            FormsAuthentication.SetAuthCookie(user, false);
            Response.Redirect("~/NexoMarket/Inicio.aspx");
        }
    }
}