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
    public partial class consumidores : System.Web.UI.Page
    {
        public GestorBitacora _gestorBitacora = new GestorBitacora();
        public GestorMensaje _gestormensaje = new GestorMensaje();
        public BLL.gestorConsumidor _gestorConsumidor = new BLL.gestorConsumidor();
        public BLL.gestorCliente _gestorCliente = new BLL.gestorCliente();
        BE.Usuario usuario = SesionActualWindows.SesionActual().ObtenerUsuarioActual();
        public validacion _check = new validacion();
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

            if (usuario.Perfil.Nombre == "CLIENTE")
            {
                BE.Cliente cliente = _gestorCliente.leer_cliente_CUIT(usuario.idUsuario.ToString());
                GridView1.DataSource = _gestorConsumidor.leer_Consumidor(cliente.idCliente);
            }
            else
            {
                if (usuario.Perfil.Nombre == "CONSUMIDOR")
                {
                    BE.Consumidor consumidor = _gestorConsumidor.leer_Consumidor_DNI(usuario.idUsuario.ToString());
                    List<BE.Consumidor> listaConsumidor = new List<BE.Consumidor>();
                    listaConsumidor.Add(consumidor);
                    GridView1.DataSource = listaConsumidor;
                }

                else
                {
                    GridView1.DataSource = _gestorConsumidor.leer_Consumidor();
                }
            }
            GridView1.DataBind();
        }

        protected void LlenarListas()
        {
            if (usuario.Perfil.Nombre == "CLIENTE")
            {
                BE.Cliente cliente = _gestorCliente.leer_cliente_CUIT(usuario.idUsuario.ToString());
                List<BE.Cliente> listaCliente = new List<BE.Cliente>();
                listaCliente.Add(_gestorCliente.leer_cliente(cliente.idCliente.ToString()));
                lstEmpresa.DataSource = listaCliente;
            }
            else
            {
                lstEmpresa.DataSource = _gestorCliente.leer_cliente();
            }
            lstEmpresa.DataTextField = "razonSocial";
            lstEmpresa.DataValueField = "idCliente";
            lstEmpresa.DataBind();


        }
        protected void LimpiarCampos()
        {
            txtIdConsumidor.Enabled = false;
            txtCP.Text = "";
            txtDomicilio.Text = "";
            txtIdConsumidor.Text = "";
            txtLocalidad.Text = "";
            txtPais.Text = "";
            txtProvincia.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            UCMail.Text = "";
            txtDNI.Text = "";

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
            if (!String.IsNullOrEmpty(txtNombre.Text) && !String.IsNullOrEmpty(txtApellido.Text) && !String.IsNullOrEmpty(txtDomicilio.Text) && !String.IsNullOrEmpty(txtCP.Text) && !String.IsNullOrEmpty(txtPais.Text) && !String.IsNullOrEmpty(UCMail.Text) && !String.IsNullOrEmpty(txtProvincia.Text))
            {
                if ((txtDNI.BackColor != System.Drawing.Color.Empty) || (UCMail.BackColor != System.Drawing.Color.Empty))
                {
                    lblInfo.Text = Traductor.Mensaje("ERR170", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                }
                else
                {
                    try
                    {
                        BE.Consumidor Consumidor = new BE.Consumidor();
                        Consumidor.idCliente = Convert.ToInt32(lstEmpresa.SelectedItem.Value.ToString());
                        Consumidor.idConsumidor = 0;
                        Consumidor.Nombre = txtNombre.Text;
                        Consumidor.Apellido = txtApellido.Text;
                        Consumidor.domicilio = txtDomicilio.Text;
                        Consumidor.dni = txtDNI.Text;
                        Consumidor.Email = UCMail.Text;
                        Consumidor.localidad = txtLocalidad.Text;
                        Consumidor.provincia = txtProvincia.Text;
                        Consumidor.pais = txtPais.Text;
                        Consumidor.CP = txtCP.Text;
                        Consumidor.fechaAlta = DateTime.Today;
                        Consumidor.consumidorDVH = "1";

                        _gestorConsumidor.insertar_Consumidor(Consumidor);
                        {
                            LlenarTabla();
                            LimpiarCampos();
                            lblInfo.Text = "Consumidor guardado correctamente";
                        }

                    }
                    catch (SeguridadException ex)
                    {
                        lblInfo.Text = Traductor.TraducirMensage(ex.CodigError, ex.Message);
                    }
                    catch (Exception ex)
                    {
                        lblInfo.Text = ex.Message;
                    }
                }
            }
            else
            {
                lblInfo.Text = "Todos los campos son mandatorios";
            }
        }
        
        protected void GridView1_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {
            _gestorConsumidor.eliminar_Consumidor(GridView1.Rows[e.RowIndex].Cells[2].Text, GridView1.Rows[e.RowIndex].Cells[3].Text);
            if (!borraMasivo)
            {
                LlenarTabla();
            }
        }
        protected void GridView1_RowUpdating(Object sender, GridViewUpdateEventArgs e)
        {

            BE.Consumidor miConsumidor = _gestorConsumidor.leer_Consumidor(Convert.ToInt32(e.Keys[0].ToString()), Convert.ToInt32(e.Keys[1].ToString()));

            if (e.NewValues[0] == null || e.NewValues[1] == null || e.NewValues[2] == null || e.NewValues[3] == null)
            {
                lblInfo.Text = Traductor.Mensaje("ERR141",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
            }
            else
            {
                bool chequeoFormato = true;

                //if (!_check.ValidarNoNumerico(e.NewValues[0].ToString()))
                //{
                //    chequeoFormato = false;
                //}
                //if (!_check.ValidarNoNumerico(e.NewValues[1].ToString()))
                //{
                //    chequeoFormato = false;
                //}
                //if (!_check.ValidarNumerico(e.NewValues[2].ToString()))
                //{
                //    chequeoFormato = false;
                //}
                if (!_check.ValidarEMail(e.NewValues[0].ToString())) //mail 
                {
                    chequeoFormato = false;
                }

                if (!_check.ValidarTextoNumEspacio(e.NewValues[2].ToString()))
                {
                    chequeoFormato = false;
                }
                if (!_check.ValidarTextoNumEspacio(e.NewValues[3].ToString()))
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


                miConsumidor.Email = e.NewValues[0].ToString();
                miConsumidor.domicilio = e.NewValues[1].ToString();
                miConsumidor.localidad = e.NewValues[2].ToString();
                miConsumidor.provincia = e.NewValues[3].ToString();
                miConsumidor.pais = e.NewValues[4].ToString();
                miConsumidor.CP = e.NewValues[5].ToString();

                if (chequeoFormato)
                {    
                    _gestorConsumidor.insertar_Consumidor(miConsumidor);
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