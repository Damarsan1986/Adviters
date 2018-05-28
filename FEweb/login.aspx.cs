using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seguridad;
using BLL;

namespace FE
{
    public partial class login : System.Web.UI.Page
    {
        public GestorBitacora _gestorBitacora = new GestorBitacora();
        public GestorIntegridad _gestorIntegridad = new GestorIntegridad();
        public GestorIntegridadBLL _gestorIntegridadBLL = new GestorIntegridadBLL();
        public GestorMensaje _gestormensaje = new GestorMensaje();
        public GestorCultura _gestorCultura = new GestorCultura();
       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SesionActualWindows.SesionActual().ObtenerUsuarioActual() != null)
            {
               
                Page.Response.Redirect("main.aspx");
                
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e) 
        {
            try
            {
                bool integridad = false;
                bool integridadBLL = false;

                    integridad = _gestorIntegridad.validar();
                    integridadBLL = _gestorIntegridadBLL.validar();

                    if ((!integridad || !integridadBLL)  & (txtIdUsuario.Text == "adminMaster"))
                    {
                        lblLogin.Text = Traductor.Mensaje("ERR100", "es-AR");
                        
                        integridad = true;
                        integridadBLL = true;
                    }

                    if (!integridad || !integridadBLL)
                    {
                        lblLogin.Text = _gestormensaje.leer_mensaje("ERR101", "es-AR").descripcion;
                    }
                    else
                    {
                        try
                        {
                            BE.Usuario usuario = new BE.Usuario();
                            usuario.idUsuario = txtIdUsuario.Text;
                            usuario.Clave = txtClave.Text;


                            ResultadoAutenticacion resultado = SesionActualWindows.SesionActual().Iniciar(usuario);

                            switch (resultado)
                            {
                                case ResultadoAutenticacion.UsuarioContingencia:
                                    lblLogin.Text = Traductor.Mensaje("ERR102", "es-AR");
                                    Page.Response.Redirect("bitacoras.aspx");
                                    break;
                                case ResultadoAutenticacion.UsuarioValido:
                                     //Si el usuario es válido, recupero la cultura y lo guardo en la sesión. Luego redirecciono a la pagina principal del sistema
                                    lblLogin.Text = "";
                                    BE.Cultura cultura = new BE.Cultura();

                                    //validio si la cultura recuperada es valida, sino por defecto asigno español Argentina
                                    if (SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura != null)
                                    {
                                        cultura.idCultura = SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura;
                                    }
                                    else
                                    { 
                                        cultura = _gestorCultura.leer_cultura("es-AR"); 
                                    }

                                    DateTime fechaD = Convert.ToDateTime(DateTime.Now.AddDays(-15));
                                    _gestorBitacora.bitacora_migrar(fechaD);

                                    if (txtIdUsuario.Text == "adminMaster")
                                    {
                                        Page.Response.Redirect("bitacoras.aspx");
                                    }
                                    else
                                    {
                                        Page.Response.Redirect("main.aspx");
                                    }
                                    break;

                                case ResultadoAutenticacion.UsuarioInvalido:
                                    lblLogin.Text = Traductor.Mensaje("ERR103", "es-AR");
                                    break;

                                case ResultadoAutenticacion.UsuarioBloqueado:
                                    lblLogin.Text = Traductor.Mensaje("ERR104", "es-AR");
                                    break;
                            }
                        }
                        catch (Exception)
                        {
                            lblLogin.Text = Traductor.Mensaje("ERR105", "es-AR");
                        }
                    }
            }
               
            catch (Exception)
                {
                    if (txtIdUsuario.Text == "adminMaster" && txtClave.Text == "Init753951!")
                    {
                        GestorPermiso _gestorPermiso = new GestorPermiso();
                        BE.Usuario usuario = new BE.Usuario();
                        usuario.idUsuario = txtIdUsuario.Text;
                        usuario.Clave = txtClave.Text;
                        usuario.Cultura = new System.Globalization.CultureInfo("es-AR");

                        BE.PermisoFiltro permiso = new BE.PermisoFiltro();
                        permiso.Nombre = "ADM MASTER";
                        usuario.Perfil = _gestorPermiso.leer_UnPermiso(permiso);
                        

                        ResultadoAutenticacion resultado = SesionActualWindows.SesionActual().IniciarContingencia(usuario);

                        Page.Response.Redirect("bitacoras.aspx");
                    }
                    else
                    {
                        lblLogin.Text = "ERROR BD - Contacte al administrador del sistema";
                    }
                }
        }
    }
}