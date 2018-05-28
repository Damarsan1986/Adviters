using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seguridad;
using System.Net.Mail;

namespace FE
{
    public partial class logoff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SesionActualWindows.SesionActual().Cerrar();
                Page.Response.Redirect("login.aspx");
            }
            catch (Exception)
            {
                lblLogin.Text = "Error al Cerrar la Sesión, contacte al Administrador";
                
            }
            
            
        }

    }
}