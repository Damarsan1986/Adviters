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
using System.Reflection;


namespace FE
{
    public partial class asignaciones : System.Web.UI.Page
    {
        public GestorBitacora _gestorBitacora = new GestorBitacora();
        public GestorMensaje _gestormensaje = new GestorMensaje();
        public BLL.gestorComprobante _gestorComprobante = new BLL.gestorComprobante();
        public BLL.gestorCliente _gestorCliente = new BLL.gestorCliente();
        public BLL.gestorMoneda _gestorMoneda = new BLL.gestorMoneda();
        public BLL.gestorTIpoCambio _gestorTipoCambio = new BLL.gestorTIpoCambio();
        public BLL.gestorD_Comprobante _gestorD_Comprobante = new BLL.gestorD_Comprobante();
        public BLL.gestorMovEmpresa _gestorMovEmpresa = new BLL.gestorMovEmpresa();
        public BLL.gestorMovCustomer _gestorMovCustomer = new BLL.gestorMovCustomer();
        public BLL.gestorConsumidor _gestorConsumidor = new BLL.gestorConsumidor();
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
                    CalcularStock();
                }

                //Traductor.TraducirControles(Page.Form.Controls, Cultura);

            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e) 
        {
            GridView1.PageIndex = e.NewPageIndex;
            LlenarTabla();
        }

        protected void CalcularStock()
        {
            BE.Usuario usuario = SesionActualWindows.SesionActual().ObtenerUsuarioActual();
            if (usuario.Perfil.Nombre == "CLIENTE")
            {
                BE.Cliente cliente = new BE.Cliente();
                cliente = _gestorCliente.leer_cliente_CUIT(usuario.idUsuario.ToString());
                if (string.IsNullOrEmpty(cliente.idCliente.ToString()))
                {
                    lblCant.Text = Traductor.Mensaje("ERR115",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                }
                else
                {
                    lblCant.Text = string.Format("{0:#,#}", _gestorMovEmpresa.calcular_stock_empresa(cliente.idCliente)).ToString() + Traductor.Mensaje("Puntos",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                }
            }
        }
        protected void LlenarTabla()
        {


            GridView1.DataSource = null;
            GridView1.AutoGenerateColumns = false;
            BE.Usuario usuario = SesionActualWindows.SesionActual().ObtenerUsuarioActual();
            if (usuario.Perfil.Nombre == "CLIENTE")
            {
                BE.Cliente cliente = _gestorCliente.leer_cliente_CUIT(usuario.idUsuario.ToString());
                GridView1.DataSource = _gestorMovCustomer.leer_mov_Customer(cliente.idCliente.ToString());
            }                                           
            else
            {
                GridView1.DataSource = _gestorMovCustomer.leer_mov_Customer();
            }
            GridView1.DataBind();
        }

        protected void LlenarListas()
        {
            DataTable DTconsumidores = new DataTable();
            BE.Usuario usuario = SesionActualWindows.SesionActual().ObtenerUsuarioActual();
            if (usuario.Perfil.Nombre == "CLIENTE")
            {
                BE.Cliente cliente = _gestorCliente.leer_cliente_CUIT(usuario.idUsuario.ToString());
                List<BE.Cliente> listaCliente = new List<BE.Cliente>();
                listaCliente.Add(_gestorCliente.leer_cliente(cliente.idCliente.ToString()));
                lstCliente.DataSource = listaCliente;
                DTconsumidores = ToDataTable(_gestorConsumidor.leer_Consumidor(cliente.idCliente));
            }
            else
            {
                lstCliente.DataSource = _gestorCliente.leer_cliente();
                DTconsumidores = ToDataTable(_gestorConsumidor.leer_Consumidor());
            }
            lstCliente.DataTextField = "razonSocial";
            lstCliente.DataValueField = "idCliente";
            lstCliente.DataBind();

            DTconsumidores.Columns.Add("NombreApellido", typeof(string), "Nombre + ' ' + Apellido");

            lstConsumidor.DataSource = DTconsumidores;
            lstConsumidor.DataTextField = "NombreApellido";
            lstConsumidor.DataValueField = "idConsumidor";
            lstConsumidor.DataBind();
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public String NombreApellido(string nombre, string apellido)
        {
            return nombre + " " + apellido; 
        }
        protected void LimpiarCampos()
        {
            txtCantPuntos.Text = "";
            txtMotivo.Text = "";
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
            if (!String.IsNullOrEmpty(txtMotivo.Text) && !String.IsNullOrEmpty(txtCantPuntos.Text) )
            {
                if ((txtCantPuntos.BackColor != System.Drawing.Color.Empty))
                {
                    lblInfo.Text = Traductor.Mensaje("ERR170", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                }
                else
                {

                    int idcliente = Convert.ToInt16(lstCliente.SelectedValue.ToString());
                    int idConsumidor = Convert.ToInt16(lstConsumidor.SelectedValue.ToString());
                    if (_gestorMovEmpresa.calcular_stock_empresa(idcliente) > Convert.ToInt16(txtCantPuntos.Text))
                    {
                        try
                        {
                            DateTime fechaHoraOperacion = DateTime.Now;

                            BE.Comprobante Venta = new BE.Comprobante();
                            Venta.idComprobante = 0;
                            Venta.idCliente = idcliente;
                            Venta.idConsumidor = idConsumidor;
                            Venta.idOperador = SesionActualWindows.SesionActual().ObtenerUsuarioActual().idUsuario.ToString();
                            Venta.monedaOperacion = 0;
                            Venta.descOperacion = "Asignación de Puntos";
                            Venta.fechaHora = fechaHoraOperacion;
                            Venta.comprobanteDVH = "1";

                            string retorno = _gestorComprobante.insertar_comprobante(Venta);
                            if (retorno == "1")
                            {
                                BE.Comprobante comp = _gestorComprobante.leer_comprobante(Venta);

                                BE.D_Comprobante Dcomp = new BE.D_Comprobante();
                                Dcomp.idComprobante = comp.idComprobante;
                                Dcomp.idD_Comprobante = 0;
                                Dcomp.idProducto = 0;
                                Dcomp.cantidad = Convert.ToInt16(txtCantPuntos.Text);
                                Dcomp.pUnitario = 0;
                                Dcomp.dComprobanteDVH = "1";

                                string retornoD = _gestorD_Comprobante.insertar_D_Comprobante(Dcomp);

                                if (retornoD == "1")
                                {
                                    BE.MovEmpresa movEmpresa = new BE.MovEmpresa();
                                    movEmpresa.idEmpresa = comp.idCliente;
                                    movEmpresa.idComprobante = comp.idComprobante;
                                    movEmpresa.cantidad = Dcomp.cantidad * -1;
                                    movEmpresa.accion = "E";
                                    movEmpresa.fechaHora = fechaHoraOperacion;
                                    movEmpresa.observaciones = "Asignación realizada por " + comp.idOperador + " - " + txtMotivo.Text;
                                    movEmpresa.movEmpresaDVH = "1";

                                    string retornoMov = _gestorMovEmpresa.insertar_mov_empresa(movEmpresa);

                                    if (retornoMov == "1")
                                    {

                                        BE.MovCustomer movCustomer = new BE.MovCustomer();
                                        movCustomer.idCliente = idcliente;
                                        movCustomer.idCustomer = idConsumidor;
                                        movCustomer.idComprobante = comp.idComprobante;
                                        movCustomer.cantidad = Convert.ToInt16(txtCantPuntos.Text);
                                        movCustomer.accion = "I";
                                        movCustomer.fechaHora = fechaHoraOperacion;
                                        movCustomer.observaciones = "Asignación realizada por " + Venta.idOperador + " - " + txtMotivo.Text;
                                        movCustomer.movCustomerDVH = "1";

                                        string retornoMovCustomer = _gestorMovCustomer.insertar_mov_Customer(movCustomer);


                                        if (retornoMovCustomer == "1")
                                        {
                                            LlenarTabla();
                                            LimpiarCampos();
                                            lblInfo.Text = Traductor.Mensaje("ERR116", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                                        }
                                        else
                                        {
                                            lblInfo.Text = Traductor.Mensaje("ERR117", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                                        }
                                    }

                                    else
                                    {
                                        lblInfo.Text = Traductor.Mensaje("ERR118", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                                    }
                                }
                                else
                                {
                                    lblInfo.Text = Traductor.Mensaje("ERR119", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                                }
                            }
                            else
                            {
                                lblInfo.Text = Traductor.Mensaje("ERR120", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
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
                    else
                    {
                        lblInfo.Text = Traductor.Mensaje("ERR121", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                    }
                }
            }
            else
            {
                lblInfo.Text = Traductor.Mensaje("ERR122",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
            }
            CalcularStock();

        }
        
        protected void btnImprimirSeleccionados_Click(Object sender, EventArgs e)
        {
            //Recorrer las filas del GridView...
                
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