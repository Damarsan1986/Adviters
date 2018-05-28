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
    public partial class ventapuntos : System.Web.UI.Page
    {
        public GestorBitacora _gestorBitacora = new GestorBitacora();
        public GestorMensaje _gestormensaje = new GestorMensaje();
        public BLL.gestorComprobante _gestorComprobante = new BLL.gestorComprobante();
        public BLL.gestorCliente _gestorCliente = new BLL.gestorCliente();
        public BLL.gestorMoneda _gestorMoneda = new BLL.gestorMoneda();
        public BLL.gestorTIpoCambio _gestorTipoCambio = new BLL.gestorTIpoCambio();
        public BLL.gestorD_Comprobante _gestorD_Comprobante = new BLL.gestorD_Comprobante();
        public BLL.gestorMovEmpresa _gestorMovEmpresa = new BLL.gestorMovEmpresa();
        //string Cultura = SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString();
        string Cultura = "";



        public int totalItemSeleccionados = 0;
        public bool borraMasivo = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SesionActualWindows.SesionActual().ObtenerUsuarioActual() != null)
                {
                    Cultura = SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString();
                    LimpiarCampos();
                    LlenarTabla();
                    LlenarListas();
                    phAlta.Visible = false;
                    phBotonera.Visible = true;
                    phLista.Visible = true;
                }
                //Traductor.TraducirControles(Page.Form.Controls, Cultura);   

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
            GridView1.DataSource = _gestorComprobante.leer_comprobante();
            GridView1.DataBind();
        }

        protected void LlenarListas()
        {
            lstCliente.DataSource = _gestorCliente.leer_cliente();
            lstCliente.DataTextField = "razonSocial";
            lstCliente.DataValueField = "idCliente";
            lstCliente.DataBind();

            lstMoneda.DataSource = _gestorMoneda.leer_moneda();
            lstMoneda.DataTextField = "descripcionCorta";
            lstMoneda.DataValueField = "idMoneda";
            lstMoneda.DataBind();

        }
        protected void LimpiarCampos()
        {
            txtCantPuntos.Text = "";
            txtPrecio.Text = "";
            txtPrecio.Enabled = false;


        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            phAlta.Visible = true;
            phBotonera.Visible = false;
            phLista.Visible = false;
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
            if (!String.IsNullOrEmpty(txtPrecio.Text) && !String.IsNullOrEmpty(txtCantPuntos.Text) && (txtPrecio.Text != "0"))
            {
                    try
                    {
                        DateTime fechaHoraOperacion = DateTime.Now;

                        BE.Comprobante Venta = new BE.Comprobante();
                        Venta.idComprobante = 0;
                        Venta.idCliente = Convert.ToInt16(lstCliente.SelectedValue.ToString());
                        Venta.idConsumidor = 0;
                        Venta.idOperador = SesionActualWindows.SesionActual().ObtenerUsuarioActual().idUsuario.ToString();
                        Venta.monedaOperacion = Convert.ToInt16(lstMoneda.SelectedValue.ToString());
                        Venta.descOperacion = "Venta de Puntos";
                        Venta.fechaHora = fechaHoraOperacion;
                        Venta.comprobanteDVH = "1";

                        string retorno = _gestorComprobante.insertar_comprobante(Venta);
                        if (retorno=="1")
                        {
                            BE.Comprobante comp = _gestorComprobante.leer_comprobante(Venta);

                            BE.D_Comprobante Dcomp = new BE.D_Comprobante();
                            Dcomp.idComprobante = comp.idComprobante;
                            Dcomp.idD_Comprobante = 0;
                            Dcomp.idProducto = 0;
                            Dcomp.cantidad = Convert.ToInt16(txtCantPuntos.Text);
                            Dcomp.pUnitario = Convert.ToDouble(txtPrecio.Text);
                            Dcomp.dComprobanteDVH = "1";

                            string retornoD = _gestorD_Comprobante.insertar_D_Comprobante(Dcomp);

                            if (retornoD=="1")
                            {
                                BE.MovEmpresa movEmpresa = new BE.MovEmpresa();
                                movEmpresa.idEmpresa = comp.idCliente;
                                movEmpresa.idComprobante = comp.idComprobante;
                                movEmpresa.cantidad = Dcomp.cantidad;
                                movEmpresa.accion = "I";
                                movEmpresa.fechaHora = fechaHoraOperacion;
                                movEmpresa.observaciones = "venta realizada por " + comp.idOperador;
                                movEmpresa.movEmpresaDVH = "1";

                                string retornoMov =  _gestorMovEmpresa.insertar_mov_empresa(movEmpresa);

                                if (retornoMov=="1")
                                {
                                    LlenarTabla();
                                    LimpiarCampos();
                                    lblInfo.Text = Traductor.Mensaje("ERR162",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                                }
                                else
                                {
                                    lblInfo.Text = Traductor.Mensaje("ERR118",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                                }
                            }
                            else
                            {
                                lblInfo.Text = Traductor.Mensaje("ERR119",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                            }
                        }
                        else
                        {
                            lblInfo.Text = Traductor.Mensaje("ERR120",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                        }
                    }
                    catch (SeguridadException ex)
                    {
                        lblInfo.Text = Traductor.Mensaje("ERR138",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                    }
                    catch (Exception ex)
                    {
                        lblInfo.Text = ex.Message;
                    }
            }
            else
            {
                lblInfo.Text = Traductor.Mensaje("ERR141",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
            }
        }
        
        protected void btnImprimirSeleccionados_Click(Object sender, EventArgs e)
        {
            //Recorrer las filas del GridView...
                
        }

        protected void btnCalcularPrecio_Click(Object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lstMoneda.SelectedValue.ToString()) || (string.IsNullOrEmpty(txtCantPuntos.Text)))
            {
                lblInfo.Text = Traductor.Mensaje("ERR163",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
            }
            else
            {
                if ((txtCantPuntos.BackColor != System.Drawing.Color.Empty))
                {
                    lblInfo.Text = Traductor.Mensaje("ERR170", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                }
                else
                {

                    int moneda = Convert.ToInt16(lstMoneda.SelectedValue.ToString());
                    double puntos = Convert.ToDouble(txtCantPuntos.Text);

                    txtPrecio.Text = (_gestorTipoCambio.calcular_precio(puntos, moneda)).ToString();
                    if (string.Equals(txtPrecio.Text, "0"))
                    {
                        lblInfo.Text = Traductor.Mensaje("ERR164", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                    }
                    else
                    {
                        lblInfo.Text = "";
                    }
                }
            }
            
               
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