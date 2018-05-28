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
    public partial class mensajes : System.Web.UI.Page
    {
        public GestorBitacora _gestorBitacora = new GestorBitacora();
        public GestorMensaje _gestormensaje = new GestorMensaje();
        public GestorCultura _gestorCultura = new GestorCultura();
        public int totalItemSeleccionados = 0;
        public bool borraMasivo = false;
        //string Cultura = SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString();
        string Cultura = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Traductor.TraducirControles(Page.Form.Controls, Cultura);   
                if (SesionActualWindows.SesionActual().ObtenerUsuarioActual() != null)
                {
                    Cultura = SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString();
                }

                LlenarListas();
                LlenarTabla();
                
                phAltaMensaje.Visible = false;
                phBotonera.Visible = true;
                phListaMensaje.Visible = true;
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e) 
        {
            GridView1.PageIndex = e.NewPageIndex;
            LlenarTabla();
        }

        protected void LlenarTabla()
        {
            GridView1.DataSource = null;
            GridView1.AutoGenerateColumns = false;
            GridView1.DataSource = _gestormensaje.leer_mensaje_complejo(lstCulturaModelo.SelectedValue.ToString(), lstCulturaEdicion.SelectedValue.ToString());
            GridView1.DataBind();
        }

        protected void LlenarListas()
        {
            lstCultura.DataSource = _gestorCultura.leer_cultura();
            lstCultura.DataTextField = "descripcion";
            lstCultura.DataValueField = "idCultura";
            lstCultura.DataBind();

            lstCulturaModelo.DataSource = _gestorCultura.leer_cultura();
            lstCulturaModelo.DataTextField = "descripcion";
            lstCulturaModelo.DataValueField = "idCultura";
            lstCulturaModelo.SelectedIndex = 1;
            lstCulturaModelo.DataBind();

            lstCulturaEdicion.DataSource = _gestorCultura.leer_cultura();
            lstCulturaEdicion.DataTextField = "descripcion";
            lstCulturaEdicion.DataValueField = "idCultura";
            lstCulturaEdicion.SelectedIndex = 2;
            lstCulturaEdicion.DataBind();
        }
        protected void LimpiarCampos()
        {
            txtDescripcion.Text = "";
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            phAltaMensaje.Visible = true;
            phBotonera.Visible = false;
            phListaMensaje.Visible = false;
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            phAltaMensaje.Visible = false;
            phBotonera.Visible = true;
            phListaMensaje.Visible = true;
            lblMensaje.Text = "";

        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtDescripcion.Text))
            {
                    try
                    {

                        BE.Mensaje mensaje = new BE.Mensaje();
                        mensaje.idMensaje = txtIdMensaje.Text;
                        mensaje.descripcion = txtDescripcion.Text;
                        mensaje.Cultura = new System.Globalization.CultureInfo(lstCultura.SelectedValue.ToString());

                        BE.Mensaje nuevoMensaje = new BE.Mensaje();
                        List<BE.Cultura> ListaCultura = _gestorCultura.leer_cultura();

                        
                        if (nuevoMensaje.idMensaje != "")
                        {
                            foreach (BE.Cultura X in ListaCultura)
                            {

                                if (mensaje.Cultura == X.idCultura)
                                {
                                    nuevoMensaje.descripcion = txtDescripcion.Text;
                                }
                                else
                                {
                                    WSGoogle.WSGoogleSoapClient trd = new WSGoogle.WSGoogleSoapClient(); 
                                    nuevoMensaje.descripcion = trd.TraducirTexto(txtDescripcion.Text, mensaje.Cultura.ToString().Substring(0,2)+"|"+ X.idCultura.ToString().Substring(0,2));
                                }
                                nuevoMensaje.Cultura = X.idCultura;
                                nuevoMensaje.idMensaje = txtIdMensaje.Text;

                                _gestormensaje.escribir_mensaje(nuevoMensaje);
                            }
                            LlenarTabla();
                            LimpiarCampos();
                            lblMensaje.Text = Traductor.Mensaje("ERR147",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                        }
              
                    }
                    catch (SeguridadException ex)
                    {
                        lblMensaje.Text = Traductor.TraducirMensage(ex.CodigError, ex.Message);
                    }
                    catch (Exception ex)
                    {
                        lblMensaje.Text = ex.Message;
                    }
                }

            else
            {
                lblMensaje.Text = Traductor.Mensaje("ERR141",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
            }
        }


        protected void GridView1_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {
            _gestormensaje.eliminar_mensaje(GridView1.Rows[e.RowIndex].Cells[2].Text);
            if (!borraMasivo)
            {
                LlenarTabla();
            }
        }
        protected void GridView1_RowUpdating(Object sender, GridViewUpdateEventArgs e)
        {

            BE.Mensaje miMensaje = _gestormensaje.leer_mensaje(e.Keys[0].ToString(),lstCulturaEdicion.SelectedValue.ToString());

            if (e.NewValues[0] == null)
            {
                lblInfo.Text = Traductor.Mensaje("ERR141",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
            }
            else
            {
                miMensaje.descripcion = e.NewValues[0].ToString();
                
                _gestormensaje.escribir_mensaje(miMensaje);
                GridView1.EditIndex = -1;
                LimpiarCampos();
                lblMensaje.Text = Traductor.Mensaje("ERR146",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                lblInfo.Text = "";
                LlenarTabla();
            }
        
        }
        protected void GridView1_RowEditing(Object sender, GridViewEditEventArgs e)
        {
            //Todas los campos excepto dropdownlist
            GridView1.EditIndex = e.NewEditIndex;
            LlenarTabla();
            lblInfo.Text = "";

        }
        protected void GridView1_RowCancelingEdit(Object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            LlenarTabla();
            lblInfo.Text = "";
        }
        protected void btnQuitarSeleccionados_Click(Object sender, EventArgs e)
        {
            //Recorrer las filas del GridView...
        
           for (int i = 0; i<=(GridView1.Rows.Count - 1);i++)
            {
                CheckBox chkElim = GridView1.Rows[i].FindControl("chkEliminar") as CheckBox;
                if (chkElim.Checked)
                {
                    borraMasivo = true;
                    GridView1.DeleteRow(i);
                }
                
            }
           borraMasivo = false;
           LlenarTabla();
        
        }
        protected void chk_OnCheckedChanged(Object sender, EventArgs e)
        {
            //Recorrer las filas del GridView...
         
            //Quita el mensaje de información si lo hubiera...
            lblInfo.Text = "";
        
            for (int i = 0; i==(GridView1.Rows.Count - 1);i++)
            {
                //CheckBox CheckBoxElim = (CheckBox)(GridView1.Rows(i).FindControl("chkEliminar");
                CheckBox chkElim = GridView1.Rows[i].FindControl("chkEliminar") as CheckBox;
                if (chkElim.Checked)
                {
                    totalItemSeleccionados += 1;
                }
            }
        }

        protected void lstCultura_Changed(object sender, EventArgs e)
        {
            LlenarTabla();
        }


    }
}