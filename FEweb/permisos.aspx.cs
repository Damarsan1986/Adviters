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
    public partial class permisos : System.Web.UI.Page
    {
        public GestorBitacora _gestorBitacora = new GestorBitacora();
        public GestorMensaje _gestormensaje = new GestorMensaje();
        public GestorUsuario _gestorUsuario = new GestorUsuario();
        public GestorCultura _gestorCultura = new GestorCultura();
        public GestorPermiso _gestorPermiso = new GestorPermiso();
        public GestorPermisoCompuesto _gestorPermisoCompuesto = new GestorPermisoCompuesto();
        public GestorIntegridad _gestorIntegridad = new GestorIntegridad();
        //string Cultura = SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString();
        string Cultura = "";
        public int totalItemSeleccionados = 0;
        public int totalItemSeleccionadosHijos = 0;
        public bool borraMasivo = false;
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
                phAltaPermiso.Visible = false;
                phGuardaPermisos.Visible = false;
                phListaHijos.Visible = false;
                phBotonera.Visible = true;
                phListaPermisos.Visible = true;
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e) 
        {
            GridView1.PageIndex = e.NewPageIndex;
            LlenarTablaPermisos();
        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            LlenarTablaP_Hijos();
        }

        protected void LlenarTabla()
        {
            LlenarTablaPermisos();
            LlenarTablaP_Hijos();
        }
        protected void LlenarTablaPermisos()
        {
            GridView1.DataSource = null;
            GridView1.AutoGenerateColumns = false;
            GridView1.DataSource = _gestorPermiso.leer_permiso();
            GridView1.DataBind();
        }

        protected void LlenarTablaP_Hijos()
        {
            GridView2.DataSource = null;
            GridView2.AutoGenerateColumns = false;
            GridView2.DataSource = _gestorPermiso.leer_permiso();
            GridView2.DataBind();
        }


        protected void LlenarListas(BE.PermisoBase excluirPermiso)
        {
            if (excluirPermiso==null)
            {
                lblInfo.Text = "ERROR";
            }
            else
            {           
                foreach (GridViewRow fila in GridView2.Rows)
                {
                     ((CheckBox)fila.FindControl("chkSeleccionar")).Checked = false;
                }     
          
                GestorPermisoCompuesto _gestorPermisoCompuesto = new GestorPermisoCompuesto();
                List<BE.PermisoBase> listaPermisos = _gestorPermiso.leer_permiso();
                
                foreach (BE.PermisoBase permi in listaPermisos)
                {
                    if (permi.Nombre != excluirPermiso.Nombre)
                    {

                        if (!excluirPermiso.esAccion)
                        {
                            BE.PermisoFiltro _permisoFiltro = new BE.PermisoFiltro();
                            _permisoFiltro.Nombre = excluirPermiso.Nombre;
                            BE.PermisoBase UnPermiso = _gestorPermiso.leer_UnPermiso(_permisoFiltro);
                            BE.PermisoCompuesto PermisoHijo = UnPermiso as BE.PermisoCompuesto;
                            foreach (BE.PermisoBase pH in PermisoHijo.listaHijos)
                            {
                                foreach (GridViewRow fila in GridView2.Rows)
                                {
                                    if (fila.Cells[1].Text == pH.Nombre)
                                    {
                                        CheckBox chkPrueba = fila.FindControl("chkSeleccionar") as CheckBox;

                                        ((CheckBox)fila.FindControl("chkSeleccionar")).Checked = true;

                                        CheckBox chkElim = fila.FindControl("chkSeleccionar") as CheckBox;
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }
        protected void LimpiarCampos()
        {
            txtDescripcion.Text = "";
            txtNombre.Text = "";
            chkesAccion.Checked = false;

        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            phAltaPermiso.Visible = true;
            phListaHijos.Visible = true;
            phGuardaPermisos.Visible = true;
            phBotonera.Visible = false;
            phListaPermisos.Visible = false;
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            phAltaPermiso.Visible = false;
            phListaHijos.Visible = false;
            phGuardaPermisos.Visible = false;
            phBotonera.Visible = true;
            phListaPermisos.Visible = true;
            lblMensaje.Text = "";

        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtNombre.Text) && !String.IsNullOrEmpty(txtDescripcion.Text)) 
            {
                try
                {
                    BE.PermisoBase entidad;
                    if (chkesAccion.Checked)
                    {
                        entidad = new BE.PermisoSimple();
                    }
                    else
                    {
                        entidad = new BE.PermisoCompuesto();
                    }
                
                    entidad.Nombre = txtNombre.Text;
                    entidad.Descripcion = txtDescripcion.Text;
                    entidad.esAccion = chkesAccion.Checked;

                    if (!entidad.esAccion)
                    {
                        BE.PermisoCompuesto nuevoPermiso = entidad as BE.PermisoCompuesto;

                        for (int i = 0; i <= (GridView2.Rows.Count - 1); i++)
                        {
                            CheckBox chkElim = GridView2.Rows[i].FindControl("chkSeleccionar") as CheckBox;
                            if (chkElim.Checked)
                            {
                                BE.PermisoBase pBase = new BE.PermisoSimple();

                                pBase.Nombre = GridView2.Rows[i].Cells[1].ToString();
                                pBase.Nombre = GridView2.Rows[i].Cells[1].Text.ToString();

                                pBase.Descripcion = GridView2.Rows[i].Cells[2].ToString();
                                pBase.Descripcion = GridView2.Rows[i].Cells[2].Text.ToString();
                                

                                CheckBox accion = GridView2.Rows[i].Cells[3].Controls[0] as CheckBox;
                                pBase.esAccion = accion.Checked;

                                nuevoPermiso.listaHijos.Add(pBase);
                            }
                        }
                        entidad = nuevoPermiso;
                    }
            
                    if (_gestorPermiso.escribir_permiso(entidad))
                    {
                        LimpiarCampos();
                        LlenarTabla();
                        lblMensaje.Text = Traductor.Mensaje("ERR152",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                        //_gestorIntegridad.recalcular_t_Permisos("corregir");
                        //_gestorIntegridad.recalcular_t_PermisoPermiso("corregir");
                        //_gestorIntegridad.recalcularDVV();
                    }
                    else
                    {
                        lblMensaje.Text = Traductor.Mensaje("ERR153",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
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
                lblInfo.Text = Traductor.Mensaje("ERR141",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
            }
        }
   
        protected void GridView1_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {
            string NombrePermiso = GridView1.Rows[e.RowIndex].Cells[2].Text;

            int permEnUso = _gestorUsuario.leer_ususarioUsaPermiso(NombrePermiso);
            if (permEnUso == 0)
            {
                _gestorPermiso.eliminar_permiso(NombrePermiso);
                lblInfo.Text = Traductor.Mensaje("ERR154",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                if (!borraMasivo)
                {
                    LlenarTabla();
                }
            }
            else
            {
                lblInfo.Text = Traductor.Mensaje("ERR155",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
            }
        }
        protected void GridView1_RowUpdating(Object sender, GridViewUpdateEventArgs e)
        {

            //ver desde aca
            BE.PermisoFiltro pFiltro = new BE.PermisoFiltro();
            pFiltro.Nombre = (e.Keys[0].ToString());
            BE.PermisoBase miPermiso = _gestorPermiso.leer_UnPermiso(pFiltro);

            if (e.NewValues[0] == null || e.NewValues[1] == null)
            {
                lblInfo.Text = Traductor.Mensaje("ERR141",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
            }
            else
            {
                //DMS

                if (!miPermiso.esAccion)
                {
                    BE.PermisoCompuesto nuevoPermiso = miPermiso as BE.PermisoCompuesto;
                    
                    nuevoPermiso.listaHijos.Clear();

                    for (int i = 0; i <= (GridView2.Rows.Count - 1); i++)
                    {
                        CheckBox chkSeleccionar = GridView2.Rows[i].FindControl("chkSeleccionar") as CheckBox;

                        if (chkSeleccionar.Checked)
                        {
                            BE.PermisoBase pBase = new BE.PermisoSimple();

                            pBase.Nombre = GridView2.Rows[i].Cells[1].Text.ToString();
                            pBase.Descripcion = GridView2.Rows[i].Cells[2].Text.ToString();

                            CheckBox accion = GridView2.Rows[i].Cells[3].Controls[0] as CheckBox;
                            pBase.esAccion = accion.Checked;

                            nuevoPermiso.listaHijos.Add(pBase);
                        }
                    }
                    miPermiso = nuevoPermiso;
                }

                miPermiso.Descripcion = e.NewValues[0].ToString();
                
                CheckBox accionPermiso = GridView1.Rows[e.RowIndex].Cells[4].Controls[0] as CheckBox;
                miPermiso.esAccion = accionPermiso.Checked;

                _gestorPermiso.escribir_permiso(miPermiso);
                GridView1.EditIndex = -1;
                LimpiarCampos();
                lblMensaje.Text = Traductor.Mensaje("ERR156",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                LlenarTabla();
                phListaHijos.Visible = false;
            }
     

        
        }
        protected void GridView1_RowEditing(Object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;

            string nombre = GridView1.Rows[e.NewEditIndex].Cells[2].Text.ToString();
            BE.PermisoFiltro filtro = new BE.PermisoFiltro();
            filtro.Nombre = nombre;
            BE.PermisoBase entidad = _gestorPermiso.leer_UnPermiso(filtro);

            if (entidad.esAccion)
            {
                phListaHijos.Visible = false;
            }
            else
            {
                phListaHijos.Visible = true;
                LlenarListas(entidad);

            }
            LlenarTablaPermisos();
            lblInfo.Text = "";

        }
        protected void GridView1_RowCancelingEdit(Object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            LlenarTabla();
            
            lblInfo.Text = "";

            phListaHijos.Visible = false;

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
           phListaHijos.Visible = false;
        
        }
        protected void chk_OnCheckedChanged(Object sender, EventArgs e)
        {
            //Recorrer las filas del GridView...
            lblInfo.Text = "";
        
            for (int i = 0; i==(GridView1.Rows.Count - 1);i++)
            {
                CheckBox chkElim = GridView1.Rows[i].FindControl("chkEliminar") as CheckBox;
                if (chkElim.Checked)
                {
                    totalItemSeleccionados += 1;
                }
            }
        }

        protected void GridView2_OnCheckedChanged(Object sender, EventArgs e)
        {
            //Recorrer las filas del GridView...
            lblInfo.Text = "";

            for (int i = 0; i == (GridView2.Rows.Count - 1); i++)
            {
                CheckBox chkElim = GridView2.Rows[i].FindControl("chkSeleccionar") as CheckBox;
                if (chkElim.Checked)
                {
                    totalItemSeleccionadosHijos += 1;
                }
            }
        }
        protected void AgegarHijos()
        {
            //Desarrollado en Guardar, revisar para extraer...

        }
    }
}