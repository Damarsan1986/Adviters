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
    public partial class perfil : System.Web.UI.Page
    {
        public GestorBitacora _gestorBitacora = new GestorBitacora();
        public GestorUsuario _gestorUsuario = new GestorUsuario();
        public GestorCultura _gestorCultura = new GestorCultura();
        //string Cultura = SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString();
        string Cultura = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (SesionActualWindows.SesionActual().ObtenerUsuarioActual() != null)
                {
                    Cultura = SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString();
                    LlenarListas();
                    BE.Usuario user = new BE.Usuario();
                    user = _gestorUsuario.leer_usuario(SesionActualWindows.SesionActual().ObtenerUsuarioActual().idUsuario.ToString());
                    txtUsuario.Text = user.idUsuario;
                    txtNombre.Text = user.Nombre;
                    txtApellido.Text = user.apellido;
                    txtEmail.Text = user.Email;
                    lstCultura.Text = user.Cultura.ToString();
                    txtUsuario.Enabled = false;
                }

                //Traductor.TraducirControles(Page.Form.Controls, Cultura);   

            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtClave.Text))
            {

                if (txtClave.Text.Length < 6)
                {
                    lblinfo.Text = Traductor.Mensaje("ERR148",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                }
                else
                {
                    if (txtClave.Text == txtClave2.Text)
                    {
                        if (!string.IsNullOrEmpty(txtApellido.Text) && !string.IsNullOrEmpty(txtNombre.Text) && !string.IsNullOrEmpty(txtEmail.Text))
                        {
                            BE.Usuario user = new BE.Usuario();
                            user = _gestorUsuario.leer_usuario(txtUsuario.Text);
                            user.Nombre = txtNombre.Text;
                            user.apellido = txtApellido.Text;
                            user.Clave = txtClave.Text;
                            user.Email = txtEmail.Text;
                            user.Cultura = new System.Globalization.CultureInfo(lstCultura.SelectedValue.ToString());
                            _gestorUsuario.escribir_usuario(user);
                            lblinfo.Text = Traductor.Mensaje("ERR149",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                            Page.Response.Redirect("main.aspx");
                        }
                        else
                        {
                            lblinfo.Text = Traductor.Mensaje("ERR141",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                        }
                    }
                    else
                    {
                        lblinfo.Text = Traductor.Mensaje("ERR150",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                    }
                }
            }
            else
            {
                lblinfo.Text = Traductor.Mensaje("ERR151",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
            }
         
        }
    
        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            LimpiarCapos();
            Page.Response.Redirect("main.aspx");
        }

        protected void LimpiarCapos()
        {
            txtUsuario.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEmail.Text = "";
            txtClave.Text = "";
            txtClave2.Text = "";
        }
        protected void LlenarListas()
        {
            lstCultura.DataSource = _gestorCultura.leer_cultura();
            lstCultura.DataTextField = "descripcion";
            lstCultura.DataValueField = "idCultura";
            lstCultura.DataBind();
        }
    }
}