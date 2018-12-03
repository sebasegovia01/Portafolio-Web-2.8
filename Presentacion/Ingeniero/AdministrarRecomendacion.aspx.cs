using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;

namespace Presentacion.Ingeniero
{
    public partial class AdministrarRecomendacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DetalleEvaluacion det = new DetalleEvaluacion();

            gvLista.DataSource = det.ListaDetalle();
            gvLista.DataBind();
            gvLista.Visible = true;
        }
    }
}