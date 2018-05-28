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
    public partial class monedas : System.Web.UI.Page
    {
        public GestorBitacora _gestorBitacora = new GestorBitacora();
        public GestorMensaje _gestormensaje = new GestorMensaje();
        public BLL.gestorProveedor _gestorProveedor = new BLL.gestorProveedor();
        string Cultura = "";
        public int totalItemSeleccionados = 0;
        public bool borraMasivo = false;
        public validacion _check = new validacion();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SesionActualWindows.SesionActual().ObtenerUsuarioActual() != null)
                {
                    Cultura = SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString();
                }
                LimpiarCampos();
                LlenarTabla();
                LlenarListas();
                phAlta.Visible = false;
                phBotonera.Visible = true;
                phLista.Visible = true;
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
            GridView1.DataSource = _gestorProveedor.leer_proveedor();
            GridView1.DataBind();
        }

        protected void LlenarListas()
        {
            //lstCultura.DataSource = _gestorCultura.leer_cultura();
            //lstCultura.DataTextField = "descripcion";
            //lstCultura.DataValueField = "idCultura";
            //lstCultura.DataBind();

        }
        protected void LimpiarCampos()
        {
            txtIdProveedor.Enabled = false;
            txtCP.Text = "";
            txtDomicilio.Text = "";
            txtIdProveedor.Text = "";
            txtLocalidad.Text = "";
            txtPais.Text = "";
            txtProvincia.Text = "";
            txtRazonSocial.Text = "";
            txtSFI.Text = "";
            UCMail.Text = "";
            UCCuit.Text = "";


        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            phAlta.Visible = true;
            phBotonera.Visible = false;
            phLista.Visible = false;

            WSDivisas.WSDivisasSoapClient Divisas = new WSDivisas.WSDivisasSoapClient();
            
            BE.TipoCambio tCambio = new BE.TipoCambio();
           
            //DMS -- recupero la cotizacion, tendria que recuperarlo como XML y parsearlo a la clase, insertar en la tabla - 
            Divisas.RecuperarCotizacion("USD");

            

        }
        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            phAlta.Visible = false;
            phBotonera.Visible = true;
            phLista.Visible = true;
            lblInfo.Text = "";

        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtRazonSocial.Text) && !String.IsNullOrEmpty(txtDomicilio.Text) && !String.IsNullOrEmpty(txtCP.Text) && !String.IsNullOrEmpty(txtPais.Text) && !String.IsNullOrEmpty(UCMail.Text) && !String.IsNullOrEmpty(txtProvincia.Text))
            {
                if ((UCCuit.BackColor != System.Drawing.Color.Empty) || (UCMail.BackColor != System.Drawing.Color.Empty) || (txtSFI.BackColor != System.Drawing.Color.Empty))
                {
                    lblInfo.Text = Traductor.Mensaje("ERR170", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                }
                else
                {
                    try
                    {
                        BE.Proveedor Proveedor = new BE.Proveedor();
                        Proveedor.idProveedor = 0;
                        Proveedor.razonSocial = txtRazonSocial.Text;
                        Proveedor.domicilio = txtDomicilio.Text;
                        string cuit = UCCuit.Text;
                        cuit = cuit.Replace("-", "");
                        Proveedor.cuit = Convert.ToInt64(cuit);
                        Proveedor.Email = UCMail.Text;
                        Proveedor.localidad = txtLocalidad.Text;
                        Proveedor.provincia = txtProvincia.Text;
                        Proveedor.pais = txtPais.Text;
                        Proveedor.SFI = Convert.ToInt16(txtSFI.Text);
                        Proveedor.CP = txtCP.Text;
                        Proveedor.fechaAlta = DateTime.Today;
                        Proveedor.proveedorDVH = "1";

                        _gestorProveedor.insertar_proveedor(Proveedor);
                        {
                            LlenarTabla();
                            LimpiarCampos();
                            lblInfo.Text = Traductor.Mensaje("ERR160", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                        }

                    }
                    catch (SeguridadException ex)
                    {
                        lblInfo.Text = Traductor.Mensaje("ERR138", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                    }
                    catch (Exception ex)
                    {
                        lblInfo.Text = ex.Message;
                    }
                }
            }
            else
            {
                lblInfo.Text = Traductor.Mensaje("ERR141",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
            }
        }
        
        protected void GridView1_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {
            _gestorProveedor.eliminar_proveedor(GridView1.Rows[e.RowIndex].Cells[2].Text);
            if (!borraMasivo)
            {
                LlenarTabla();
            }
        }
        protected void GridView1_RowUpdating(Object sender, GridViewUpdateEventArgs e)
        {

            BE.Proveedor miProveedor = _gestorProveedor.leer_proveedor(Convert.ToInt32(e.Keys[0].ToString()));

            if (e.NewValues[0] == null || e.NewValues[1] == null || e.NewValues[2] == null || e.NewValues[3] == null)
            {
                lblInfo.Text = Traductor.Mensaje("ERR141",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
            }
            else
            {
                bool chequeoFormato = true;

                if (!_check.ValidarCuit(e.NewValues[1].ToString()))
                {
                    chequeoFormato = false;
                }
                else
                {
                    string cuit = e.NewValues[1].ToString();
                    cuit = cuit.Replace("-", "");
                    miProveedor.cuit = Convert.ToInt64(cuit);
                }
                if (!_check.ValidarEMail(e.NewValues[2].ToString()))
                {
                    chequeoFormato = false;
                }
                if (!_check.ValidarTextoNumEspacio(e.NewValues[4].ToString()))
                {
                    chequeoFormato = false;
                }
                if (!_check.ValidarTextoNumEspacio(e.NewValues[5].ToString()))
                {
                    chequeoFormato = false;
                }
                if (!_check.ValidarTextoNumEspacio(e.NewValues[6].ToString()))
                {
                    chequeoFormato = false;
                }
                if (!_check.ValidarTextoNumEspacio(e.NewValues[7].ToString()))
                {
                    chequeoFormato = false;
                }
                if (!_check.ValidarNumerico(e.NewValues[8].ToString()))
                {
                    chequeoFormato = false;
                }
                else
                {
                    miProveedor.SFI = Convert.ToInt16(e.NewValues[8].ToString());
                }


                miProveedor.razonSocial = e.NewValues[0].ToString();
                miProveedor.Email = e.NewValues[2].ToString();
                miProveedor.domicilio = e.NewValues[3].ToString();
                miProveedor.localidad = e.NewValues[4].ToString();
                miProveedor.provincia = e.NewValues[5].ToString();
                miProveedor.pais = e.NewValues[6].ToString();
                miProveedor.CP = e.NewValues[7].ToString();
                

                if (chequeoFormato)
                {
                _gestorProveedor.insertar_proveedor(miProveedor);
                GridView1.EditIndex = -1;
                lblInfo.Text = "";
                LlenarTabla();
                }
                else
                {
                    lblInfo.Text = Traductor.Mensaje("ERR169", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());

                }
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


    }
}