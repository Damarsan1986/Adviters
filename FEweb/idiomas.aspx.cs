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
    public partial class idiomas : System.Web.UI.Page
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
                if (SesionActualWindows.SesionActual().ObtenerUsuarioActual() != null)
                {
                    Cultura = SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString();
                }
                //Traductor.TraducirControles(Page.Form.Controls, Cultura);
                LlenarTabla();
                LlenarListas();
                phAltaIdioma.Visible = false;
                phBotonera.Visible = true;
                phListaIdioma.Visible = true;
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
            GridView1.DataSource = _gestorCultura.leer_cultura();
            GridView1.DataBind();
        }

        protected void LlenarListas()
        {
            lstCultura.DataSource = (System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.SpecificCultures));
            lstCultura.DataTextField = "name";
            //lstCultura.DataValueField = "idCultura";
            lstCultura.DataBind();

        }
        protected void LimpiarCampos()
        {
            txtDescripcion.Text = "";
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            phAltaIdioma.Visible = true;
            phBotonera.Visible = false;
            phListaIdioma.Visible = false;
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            phAltaIdioma.Visible = false;
            phBotonera.Visible = true;
            phListaIdioma.Visible = true;
            lblMensaje.Text = "";

        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtDescripcion.Text))
            {
                    try
                    {

                        BE.Cultura cultura = new BE.Cultura();
                        cultura.Descripcion= txtDescripcion.Text;
                        cultura.idCultura = new System.Globalization.CultureInfo(lstCultura.SelectedValue.ToString());



                        if (_gestorCultura.escribir_cultura(cultura))
                        {
                            _gestormensaje.escribir_mensaje("es-AR", cultura.idCultura.ToString());

                            LlenarTabla();
                            LimpiarCampos();
                            lblMensaje.Text = Traductor.Mensaje("ERR145",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
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
            _gestorCultura.borrar_cultura(GridView1.Rows[e.RowIndex].Cells[2].Text);
            if (!borraMasivo)
            {
                LlenarTabla();
            }
        }
        protected void GridView1_RowUpdating(Object sender, GridViewUpdateEventArgs e)
        {

            BE.Cultura miCultura = _gestorCultura.leer_cultura(e.Keys[0].ToString());

            if (e.NewValues[0] == null)
            {
                lblInfo.Text = Traductor.Mensaje("ERR141",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
            }
            else
            {
                miCultura.Descripcion = e.NewValues[0].ToString();
                
                _gestorCultura.escribir_cultura(miCultura);
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


    }
}