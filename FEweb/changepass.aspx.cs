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
    public partial class changepass : System.Web.UI.Page
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
            try
            {
                string idUsuario = Request["usuario"];
                string token = Request["token"];
                string fecha = Request["o"];

                WSConversorFecha.WSConversorFechaSoapClient WSConversorFecha = new WSConversorFecha.WSConversorFechaSoapClient();
                
                DateTimeOffset fechaLimite1 = (Convert.ToDateTime(fecha.Replace("T", " ")).AddHours(-2));

                DateTime fechaLim = WSConversorFecha.ConvertirFecha(fecha);
                
                DateTimeOffset fechaLimite = fechaLim;


                fechaLimite = fechaLimite.UtcDateTime;


                bool integridad = false;
                integridad = (bool)_gestorIntegridad.validar();
                
               
                if  ((!integridad & (idUsuario == "adminMaster")))
                {
                    lblLogin.Text = "Error de Integridad, revise y restaure el sistema desde el último punto bueno conocido";
                   
                    integridad = true;
                }

                if (!integridad) 
                    {
                        lblLogin.Text = "Error de Integridad.  Contacte al administrador del sistema";
                    
                    }
                else
                {
                    if (txtclave.Text == txtclave2.Text)
                    {
                        if (txtclave.Text.Length < 6)
                        {
                            lblLogin.Text = "La Clave debe tener mínimo 6 caracteres.";
                        }
                        else
                        {
                            try
                            {

                                BE.Usuario usuario = new BE.Usuario();
                                usuario = _gestorUsuario.leer_usuario(idUsuario);

                                if (usuario.Clave == token)
                                {
                                    if (fechaLimite <= DateTimeOffset.UtcNow.UtcDateTime)
                                    {
                                        usuario.Clave = txtclave.Text;
                                        _gestorUsuario.escribir_usuario(usuario);
                                        lblLogin.Text = "Contraseña cambiada con éxito";
                                        Page.Response.Redirect("login.aspx");
                                    }
                                    else
                                    {
                                        lblLogin.Text = "Token expirado, debe volver a generar su acceso temporal";
                                    }
                                }
                                else
                                {
                                    lblLogin.Text = "Token Inválido, debe volver a generar su acceso temporal";
                                }
                            }
                            catch (Exception ex)
                            {
                                lblLogin.Text = "ERROR no controlado - Contacte al administrador del sistema";
                            }
                        }
                    }
                    else
                    {
                        lblLogin.Text = "Las contraseñas ingresadas no coinciden";
                    }
                }
            }
               
        catch (Exception ex)
                {
                    lblLogin.Text = "ERROR no controlado - Contacte al administrador del sistema";
                }
        }
    }
}