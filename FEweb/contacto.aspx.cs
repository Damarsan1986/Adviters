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
    public partial class contacto : System.Web.UI.Page
    {
        public GestorBitacora _gestorBitacora = new GestorBitacora();
        public GestorIntegridad _gestorIntegridad = new GestorIntegridad();
        public GestorMensaje _gestormensaje = new GestorMensaje();
        public GestorUsuario _gestorUsuario = new GestorUsuario(); 

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {

            string mailTO = "daniel.marcelo.sanchez@gmail.com";
            string subject = "Mensaje desde el Sitio";
            string body = "El siguiente mensaje ha sido escrito por " + txtNombre.Text + "<p><p><p> " + txtMensaje.Text + " <p><p><p> sus datos son --> Mail:" + txtEmail.Text + " Tel:" + txtTelefono.Text ;

            WSMail.WSMailSoapClient WSMail = new WSMail.WSMailSoapClient();

            try
            {
                WSMail.EnvioMail(mailTO, body, subject);
                Page.ClientScript.RegisterStartupScript(GetType(), "Mensaje de Usuario", "<script>alert('Mensaje enviado con éxito, lo contactaremos a la brevedad');if(alert){ window.location='contacto.aspx';}</script>");
            }
            catch (Exception)
            {

                Page.ClientScript.RegisterStartupScript(GetType(), "Mensaje de Usuario", "<script>alert('Error de Envío, escribanos a info@Adviters.com');if(alert){ window.location='contacto.aspx';}</script>");
            }

        }
    }
}