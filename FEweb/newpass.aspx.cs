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
    public partial class newpass : System.Web.UI.Page
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
                bool integridad = false;
                integridad = (bool)_gestorIntegridad.validar();
                
               
                if  ((!integridad & (txtIdUsuario.Text == "adminMaster")))
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
                    try
                    {
                        if (lblEmail.Visible == false)
                        {
                            Page.Response.Redirect("login.aspx"); 
                        }
                        BE.Usuario usuario = new BE.Usuario();
                        usuario = _gestorUsuario.leer_usuario(txtIdUsuario.Text);
                        
                        if (usuario.Email == txtemail.Text)
                        {
                            //enviar mail

                            var fechaHora = "";
                            fechaHora = DateTimeOffset.UtcNow.UtcDateTime.ToString("u");
                            fechaHora = fechaHora.Replace(" ", "T");
                            Uri myuri = new Uri(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
                            var relativeUrl = VirtualPathUtility.ToAppRelative(new Uri(Context.Request.Url.PathAndQuery, UriKind.Relative).ToString());
                            relativeUrl = relativeUrl.ToString().Replace("~", "");
                            string hostname = myuri.ToString().Replace(relativeUrl, "");

                            string mailTO = usuario.Email;
                            string subject = "Recupero de Contraseña";
                            string body = "Acceda al siguiente link y Siga las instrucciones <p><p><p>" + hostname + "/changepass.aspx?usuario=" + usuario.idUsuario + "&token=" + usuario.Clave + "&o=" + fechaHora + "<p><p><p>  El enlace permanecerá activo por 2 horas desde su generación";

                            WSMail.WSMailSoapClient WSMail = new WSMail.WSMailSoapClient();
                            
                            try
                            {
                                WSMail.EnvioMail(mailTO, body, subject);

                                lblLogin.Text = "<h3>Se ha enviado un correo electrónico con la información solicitada.</h3> Acceda al sistema a través del enlace en el mismo y siga las instrucciones. </br> </br> Tenga en cuenta que el enlace dejará de funcionar 2 horas después de haberse generado.</br></br>";
                                txtemail.Visible = false;
                                txtIdUsuario.Visible = false;
                                lblEmail.Visible = false;
                                lblUsuario.Visible = false;
                            }
                            catch (Exception ex)
                            {
                                Exception ex2 = ex;
                                string errorMessage = string.Empty;
                                while (ex2 != null)
                                {
                                    errorMessage += ex2.ToString();
                                    ex2 = ex2.InnerException;
                                }
                                
                                Page.ClientScript.RegisterStartupScript(GetType(), "Mensaje de Usuario", "<script>alert('Error de Envío...');if(alert){ window.location='newpass.aspx';}</script>");
                                lblLogin.Text = ex.ToString();
                            }
                        }
                        else
                        {
                            lblLogin.Text = "Nombre de Usuario o Email incorrectos";
                        }
                    }
                    catch (Exception ex)
                    {
                        lblLogin.Text = "ERROR no controlado - Contacte al administrador del sistema";
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