using NexoMarket.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NexoMarket.Forms
{
    public partial class BitacoraEventos : System.Web.UI.Page
    {
        private readonly BusinessBitacora _businessBitacora;

        public BitacoraEventos()
        {
            _businessBitacora = new BusinessBitacora();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var eventos = Task.Run(() => _businessBitacora.BuscarEventosBitacora()).Result;
                DataBitacora.DataSource = eventos;
                DataBitacora.DataBind();
            }
        }

        protected void DataBitacora_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var eventos = Task.Run(() => _businessBitacora.BuscarEventosBitacora()).Result;
            DataBitacora.PageIndex = e.NewPageIndex;
            DataBitacora.DataSource = eventos; // Volvé a enlazar tus datos
            DataBitacora.DataBind();

        }
    }
}