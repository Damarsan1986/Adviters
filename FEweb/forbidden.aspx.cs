using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seguridad;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace FE
{
    public partial class forbidden : System.Web.UI.Page
    {
        //string Cultura = SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Traductor.TraducirControles(Page.Form.Controls, Cultura);
                lblDenegado.Text = Traductor.Mensaje("ERR142", "es-AR");
                lblAcceda.Text = Traductor.Mensaje("ERR143", "es-AR");
                lblModuloForbidden.Text = Traductor.Mensaje("ERR144", "es-AR");
            }
        }
    }
}