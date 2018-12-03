using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Supervisor
{
    public partial class administrarCapacitaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Capacitacion cap = new Capacitacion();

            gvCapacitaciones.DataSource = cap.ListarCapacitaciones();
            gvCapacitaciones.DataBind();
        }
    }
}