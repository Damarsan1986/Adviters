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
    public partial class bitacoras : System.Web.UI.Page
    {
        //string Cultura = SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString();
        string Cultura = "";
        public GestorBitacora _gestorBitacora = new GestorBitacora();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SesionActualWindows.SesionActual().ObtenerUsuarioActual() != null)
                {
                    Cultura = SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString();
                }

                //Traductor.TraducirControles(Page.Form.Controls, Cultura);
                LlenarTabla();
            }

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LlenarTabla();
        }
        protected void txtIdUsuario_TextChanged(object sender, EventArgs e)
        {
            lblInfo.Text = "";
            if (string.IsNullOrEmpty(txtIdUsuario.Text))
            {
                txtDescripcion.Text = "";
                txtFecD.Text = "";
                txtFecH.Text = "";
                txtDescripcion.Enabled = true;
                txtFecD.Enabled = true;
                txtFecH.Enabled = true;
            }
            else
            {
                txtDescripcion.Text = "";
                txtFecD.Text = "";
                txtFecH.Text = "";
                txtDescripcion.Enabled = false;
                txtFecD.Enabled = false;
                txtFecH.Enabled = false;
            }

        }
        protected void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            lblInfo.Text = "";
            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                txtIdUsuario.Text = "";
                txtFecD.Text = "";
                txtFecH.Text = "";
                txtIdUsuario.Enabled = true;
                txtFecD.Enabled = true;
                txtFecH.Enabled = true;
            }
            else
            {
                txtIdUsuario.Text = "";
                txtFecD.Text = "";
                txtFecH.Text = "";
                txtIdUsuario.Enabled = false;
                txtFecD.Enabled = false;
                txtFecH.Enabled = false;
            }

        }
        protected void LlenarTabla()
        {
            GridView1.DataSource = null;
            GridView1.AutoGenerateColumns = false;
            GridView1.DataSource = _gestorBitacora.leer_bitacora();
            GridView1.DataBind();
        }
        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdUsuario.Text) && string.IsNullOrEmpty(txtDescripcion.Text) && string.IsNullOrEmpty(txtFecD.Text) && string.IsNullOrEmpty(txtFecH.Text))
            {
                GridView1.DataSource = null;
                GridView1.DataSource = _gestorBitacora.leer_bitacora();
                GridView1.DataBind();
            }
            else
            {
                if (string.IsNullOrEmpty(txtIdUsuario.Text) && string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    if (string.IsNullOrEmpty(txtFecD.Text) || string.IsNullOrEmpty(txtFecH.Text))
                    {
                        lblInfo.Text =  Traductor.Mensaje("ERR123",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                    }
                    else
                    {
                        if ((txtFecD.BackColor != System.Drawing.Color.Empty) || (txtFecH.BackColor != System.Drawing.Color.Empty))
                        {
                            lblInfo.Text = Traductor.Mensaje("ERR170", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                        }
                        else
                        {
                            GridView1.DataSource = null;
                            GridView1.DataSource = _gestorBitacora.leer_bitacora_fecha(Convert.ToDateTime(txtFecD.Text), Convert.ToDateTime(txtFecH.Text));
                            GridView1.DataBind();
                        }
                    }
                }

                else
                {
                    if (string.IsNullOrEmpty(txtIdUsuario.Text))
                    {
                        GridView1.DataSource = null;
                        GridView1.DataSource = _gestorBitacora.leer_bitacora_descripcion(txtDescripcion.Text);
                        GridView1.DataBind();
                    }
                    else
                    {
                        GridView1.DataSource = null;
                        GridView1.DataSource = _gestorBitacora.leer_bitacora(txtIdUsuario.Text);
                        GridView1.DataBind();
                    }
                }
                    
            }
        }
        protected void btnMigrar_Click(object sender, EventArgs e)
        {
            if ( txtFecD.Text != String.Empty)
            {
                if ((txtFecD.BackColor != System.Drawing.Color.Empty))
                {
                    lblInfo.Text = Traductor.Mensaje("ERR170", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                }
                else
                {
                    DateTime fechaD = Convert.ToDateTime(txtFecD.Text);
                    _gestorBitacora.bitacora_migrar(fechaD);
                    lblInfo.Text = Traductor.Mensaje("ERR124", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                    GridView1.DataSource = _gestorBitacora.leer_bitacora();
                    GridView1.DataBind();
                }
            }
            else
            {
                lblInfo.Text = Traductor.Mensaje("ERR125",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
            }
        }
        protected void btnDesmigrar_Click(object sender, EventArgs e)
        {
            if ((txtFecD.Text != String.Empty)&&(txtFecH.Text != String.Empty))
            {
                if ((txtFecD.BackColor != System.Drawing.Color.Empty) || (txtFecH.BackColor != System.Drawing.Color.Empty))
                {
                    lblInfo.Text = Traductor.Mensaje("ERR170", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                }
                else
                {
                    DateTime fechaD = Convert.ToDateTime(txtFecD.Text);
                    DateTime fechaH = Convert.ToDateTime(txtFecH.Text);
                    _gestorBitacora.bitacora_desmigrar(fechaD, fechaH);
                    lblInfo.Text = Traductor.Mensaje("ERR126", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                    GridView1.DataSource = _gestorBitacora.leer_bitacora();
                    GridView1.DataBind();
                }
            }
            else
            {
                lblInfo.Text = Traductor.Mensaje("ERR127",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
            }
        }
    }
}