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
    public partial class canjes : System.Web.UI.Page
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
        public BLL.gestorProducto _gestorProducto = new BLL.gestorProducto();
        public GestorUsuario _gestorUsuario = new GestorUsuario();
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
                    Carrito();
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

        public int CalcularStock()
        {
            BE.Usuario usuario = SesionActualWindows.SesionActual().ObtenerUsuarioActual();
            if (usuario.Perfil.Nombre == "CONSUMIDOR")
            {
                BE.Consumidor consumidor = new BE.Consumidor();
                consumidor = _gestorConsumidor.leer_Consumidor_DNI(usuario.idUsuario.ToString());
                if (string.IsNullOrEmpty(consumidor.idConsumidor.ToString()))
                {
                    lblCant.Text = Traductor.Mensaje("ERR128",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                }
                else
                {
                    int totalPuntos = _gestorMovCustomer.calcular_stock_Customer(consumidor.idCliente, consumidor.idConsumidor);
                    int totalCarrito = 0;
                    int totalCantidad = 0;
                    int cantidad = 0;
                    int pUnitario=0;

                    DataTable miCarrito = Session["datos"] as DataTable;
                    foreach (DataRow row in miCarrito.Rows)
                    {
                        cantidad = Convert.ToInt16(row["Cantidad"].ToString());
                        pUnitario = Convert.ToInt32(row["Precio"].ToString());
                        totalCarrito += cantidad * pUnitario;
                        totalCantidad += cantidad;
                    }

                    int subTotal = totalPuntos - totalCarrito;

                    if (subTotal == 0)
                    {
                        lblCant.Text = totalCantidad.ToString() + Traductor.Mensaje("ERR129", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString()) + subTotal.ToString() + Traductor.Mensaje("ERR130", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                        return 0;
                    }
                    else
                    {
                        lblCant.Text = totalCantidad.ToString() + Traductor.Mensaje("ERR129", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString()) + string.Format("{0:#,#}", subTotal.ToString()) + Traductor.Mensaje("ERR130", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                        return subTotal;
                    }
                }
                return 0;
            }
            return 0;
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
            GridView1.DataSource = _gestorProducto.leer_producto();
            GridView1.DataBind();

            GridView2.DataSource = null;
            GridView2.AutoGenerateColumns = false;
            DataTable miCarrito = Session["datos"] as DataTable;
            GridView2.DataSource = miCarrito;
            GridView2.DataBind();

        }

        public DataTable Carrito()
        {
            DataTable miCarrito = new DataTable();

            miCarrito.Columns.Add("idProducto");
            miCarrito.Columns.Add("TituloProducto");
            miCarrito.Columns.Add("Cantidad");
            miCarrito.Columns.Add("Descripcion");
            miCarrito.Columns.Add("Precio");
            
            Session["datos"] = miCarrito;
            
            return miCarrito;

        }
        protected void LlenarListas()
        {

        }

        protected void LimpiarCampos()
        {

        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            phAlta.Visible = true;
            phBotonera.Visible = false;
            phLista.Visible = false;
            DataTable miCarrito = Session["datos"] as DataTable;
            if (miCarrito.Rows.Count == 0)
            {
                lblInfo.Text = Traductor.Mensaje("ERR131",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
            }
            else
            {
                BE.Consumidor consumidor = _gestorConsumidor.leer_Consumidor_DNI(SesionActualWindows.SesionActual().ObtenerUsuarioActual().idUsuario.ToString()) ;

                lblInfo.Text = Traductor.Mensaje("ERR132", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString()) + consumidor.domicilio + " - " + consumidor.localidad + Traductor.Mensaje("ERR133", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "Ver")
            //{
            //    lblInfo.Text = "Funciono" + e.CommandArgument.ToString();
            //}

        }
        protected void GridView1_RowEditing(Object sender, GridViewEditEventArgs e)
        {
            //Todas los campos excepto dropdownlist
            GridView1.EditIndex = e.NewEditIndex;
            GridView1.Columns[0].HeaderText = "Cantidad";
            LlenarTabla();
            lblInfo.Text = "";

        }

        protected void GridView1_RowUpdating(Object sender, GridViewUpdateEventArgs e)
        {
            UCCantGrid cant = GridView1.Rows[e.RowIndex].FindControl("txtCantidad") as UCCantGrid;

            if (string.IsNullOrEmpty(cant.Text))
            {
                lblInfo.Text = Traductor.Mensaje("ERR134", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
            }
            else
            {
                DataTable miCarrito = Session["datos"] as DataTable;
                DataRow row = miCarrito.NewRow();

                
                int subTotalPuntos = CalcularStock();
                if (subTotalPuntos >= (Convert.ToInt32(cant.Text) * Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells[7].Text.ToString())))
                {

                    row["idProducto"] = GridView1.Rows[e.RowIndex].Cells[1].Text.ToString();
                    row["TituloProducto"] = GridView1.Rows[e.RowIndex].Cells[2].Text.ToString();

                    row["Cantidad"] = cant.Text.ToString();
                    row["Descripcion"] = GridView1.Rows[e.RowIndex].Cells[3].Text.ToString();
                    row["Precio"] = GridView1.Rows[e.RowIndex].Cells[7].Text.ToString();

                    miCarrito.Rows.Add(row);
                    Session["datos"] = miCarrito;

                    GridView1.EditIndex = -1;
                    GridView1.Columns[0].HeaderText = "";
                    lblInfo.Text = "";
                    LlenarTabla();
                }
                else
                {
                    lblInfo.Text = Traductor.Mensaje("ERR135", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                }

            }
            CalcularStock();

        }
        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            
            phAlta.Visible = false;
            phBotonera.Visible = true;
            phLista.Visible = true;
            lblInfo.Text = "";
            CalcularStock();
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            btnConfirmar.Visible = true;
            DataTable miCarrito = Session["datos"] as DataTable;
            if (miCarrito.Rows.Count>=1)
                
            {
                
                    try
                    {
                        WSMail.WSMailSoapClient WSMail = new WSMail.WSMailSoapClient();
                        BE.Usuario usr = new BE.Usuario();
                        usr = _gestorUsuario.leer_usuario(SesionActualWindows.SesionActual().ObtenerUsuarioActual().idUsuario.ToString());
                        string idComprobante = "";
                        string mailTO = usr.Email;
                        string subject = "Confirmación de Canje";
                        string body = "";
                        string detallePedido = "";

                        BE.Consumidor consumidor = _gestorConsumidor.leer_Consumidor_DNI(SesionActualWindows.SesionActual().ObtenerUsuarioActual().idUsuario.ToString()) ;

                        DateTime fechaHoraOperacion = DateTime.Now;
                        int total = 0;

                        BE.Comprobante Venta = new BE.Comprobante();
                        Venta.idComprobante = 0;
                        Venta.idCliente = consumidor.idCliente;
                        Venta.idConsumidor = consumidor.idConsumidor;
                        Venta.idOperador = consumidor.idConsumidor.ToString();
                        Venta.monedaOperacion = 0;
                        Venta.descOperacion = "Asignación de Puntos";
                        Venta.fechaHora = fechaHoraOperacion;
                        Venta.comprobanteDVH = "1";

                        string retorno = _gestorComprobante.insertar_comprobante(Venta);
                        if (retorno == "1")
                        {
                            detallePedido = "<p>___________";
                            foreach (DataRow dr in miCarrito.Rows)
                            {
                                BE.Comprobante comp = _gestorComprobante.leer_comprobante(Venta);

                                BE.D_Comprobante Dcomp = new BE.D_Comprobante();
                                Dcomp.idComprobante = comp.idComprobante;
                                idComprobante = comp.idComprobante.ToString();
                                Dcomp.idD_Comprobante = 0;
                                Dcomp.idProducto = Convert.ToInt16(dr["idProducto"].ToString());
                                Dcomp.cantidad = Convert.ToInt16(dr["Cantidad"].ToString());
                                Dcomp.pUnitario = Convert.ToInt32(dr["Precio"].ToString());
                                Dcomp.dComprobanteDVH = "1";
                                total += Convert.ToInt32(Dcomp.pUnitario * Dcomp.cantidad);
                                string retornoD = _gestorD_Comprobante.insertar_D_Comprobante(Dcomp);

                                BE.Producto prod = _gestorProducto.leer_producto(Dcomp.idProducto);
                                detallePedido = detallePedido + "<p><p><p> Producto: " + prod.idProducto + " - " + prod.tituloProducto + " || Cantidad: " + Dcomp.cantidad + " || Valor Unitario: " + Dcomp.pUnitario + " || SubTotal: " + Convert.ToInt32(Dcomp.pUnitario * Dcomp.cantidad);  


                            }
                            detallePedido = detallePedido + "<p><p><p> Importe Total: " + total + "<p><p><p><p>";
                            detallePedido = detallePedido + "___________<p><p><p><p>";
                            

                                    BE.MovCustomer movCustomer = new BE.MovCustomer();
                                    movCustomer.idCliente = Venta.idCliente;
                                    movCustomer.idCustomer = Venta.idConsumidor;
                                    movCustomer.idComprobante = Venta.idComprobante;
                                    movCustomer.cantidad = total*-1;
                                    movCustomer.accion = "E";
                                    movCustomer.fechaHora = fechaHoraOperacion;
                                    movCustomer.observaciones = "Canje realizado por " + Venta.idOperador;
                                    movCustomer.movCustomerDVH = "1";

                                    string retornoMovCustomer = _gestorMovCustomer.insertar_mov_Customer(movCustomer);


                                    if (retornoMovCustomer == "1")
                                    {
                                        LlenarTabla();
                                        LimpiarCampos();
                                        lblInfo.Text = "<p><p><p> Canje realizado correctamente.  <p><p><p> En instantes recibirá un email de confirmación con el número de Pedido.";
                                        miCarrito = new DataTable();
                                        Session["datos"] = miCarrito;

                                        body = "Estimado/a " + usr.Nombre + "  <p><p><p>" + "Muchas gracias por su orden del catálogo de Adviters. Su número de comprobante de canje es el " + idComprobante  + ".";
                                        body = body + detallePedido;
                                        body = body + "<p><p><p>  El pedido será enviado a la siguiente dirección: " + consumidor.domicilio + " - " + consumidor.localidad + " dentro de los próximos 20 días hábiles.  <p><p><p>" + "<p><p><p><p> Por cualquier duda puede contactarse a info@Adviters.com.  <p><p><p>" + "Atentamente <p><p><p>" + "El equipo de Adviters.";
                                        WSMail.EnvioMail(mailTO, body, subject);
                                        btnConfirmar.Visible = false;
                                    }
                                    else
                                    {
                                        lblInfo.Text = Traductor.Mensaje("ERR136", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                                    }


                        }
                        else
                        {
                            lblInfo.Text = Traductor.Mensaje("ERR137", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
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
            else
            {
                lblInfo.Text = Traductor.Mensaje("ERR139", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
            
            }
            LlenarTabla();
        }
        protected void GridView1_RowCancelingEdit(Object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GridView1.Columns[0].HeaderText = "";
            LlenarTabla();
            lblInfo.Text = "";
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

        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            DataTable miCarrito = Session["datos"] as DataTable;
            DataRow row = miCarrito.NewRow();

            row["idProducto"] = GridView2.Rows[e.RowIndex].Cells[1].Text.ToString();
            row["TituloProducto"] = GridView2.Rows[e.RowIndex].Cells[2].Text.ToString();

            row["Cantidad"] = GridView2.Rows[e.RowIndex].Cells[3].Text.ToString();
            row["Descripcion"] = GridView2.Rows[e.RowIndex].Cells[4].Text.ToString();
            row["Precio"] = GridView2.Rows[e.RowIndex].Cells[5].Text.ToString();

            miCarrito.Rows.RemoveAt(e.RowIndex);
            Session["datos"] = miCarrito;

            LlenarTabla();

        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            LlenarTabla();
        }
    }
}