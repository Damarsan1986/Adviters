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
using System.Runtime;

namespace FE
{
    public partial class Productos : System.Web.UI.Page
    {
        public GestorBitacora _gestorBitacora = new GestorBitacora();
        public GestorMensaje _gestormensaje = new GestorMensaje();
        public BLL.gestorProducto _gestorProducto = new BLL.gestorProducto();
        //string Cultura = SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString();
        string Cultura = "";
        public validacion _check = new validacion();

        public int totalItemSeleccionados = 0;
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
                LimpiarCampos();
                LlenarTabla();
                LlenarListas();
                phAlta.Visible = false;
                phBotonera.Visible = true;
                phLista.Visible = true;
                
            }
            else
            {
                if (this.FileUpload1.HasFile)
                {
                    FileInfo FI = new FileInfo(FileUpload1.FileName);

                    string NombreArchivo = FI.Name;
                    string ext = FI.Extension;
                    string Ruta = FI.FullName;
                    string RutaArchivo = FI.DirectoryName;

                    lblimg.Text = NombreArchivo;

                    if (ext == ".jpg" || ext == ".JPG" || ext == ".png" || ext == ".PNG" || ext == ".gif" || ext == ".GIF" || ext == ".bmp" || ext == ".BMP")
                    {
                        FileUpload1.SaveAs(Server.MapPath("./productos/") + FileUpload1.FileName);
                        imgProducto.ImageUrl = @"~//productos//" + FileUpload1.FileName;
                    }
                    else
                    {
                       lblInfo.Text = Traductor.Mensaje("ERR157",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                    }
                }
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
            GridView1.DataSource = _gestorProducto.leer_producto();
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
            txtIdProducto.Enabled = false;
            txtCategoria.Text = "";
            txtDescripcion.Text = "";
            txtMarca.Text = "";
            //txtPicture.Text = "";
            txtPrecio.Text = "";
            txtStockMaximo.Text = "";
            txtStockMinimo.Text = "";
            txtTipo.Text = "";
            txtTitulo.Text = "";
            txtIdProducto.Text = "";
            imgProducto.ImageUrl = "";

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
        public static byte[] StrToByteArray(string str)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            return encoding.GetBytes(str);
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtTitulo.Text) && !String.IsNullOrEmpty(txtMarca.Text) && !String.IsNullOrEmpty(txtTipo.Text) && !String.IsNullOrEmpty(txtCategoria.Text) && !String.IsNullOrEmpty(txtPrecio.Text))
            
            {
                if (!string.IsNullOrEmpty(lblimg.Text))
                {
                    if ((txtStockMaximo.BackColor != System.Drawing.Color.Empty) || (txtStockMinimo.BackColor != System.Drawing.Color.Empty) || (txtPrecio.BackColor != System.Drawing.Color.Empty))
                    {
                        lblInfo.Text = Traductor.Mensaje("ERR170", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                    }
                    else
                    {
                        try
                        {
                            BE.Producto Producto = new BE.Producto();
                            Producto.idProducto = 0;
                            Producto.tituloProducto = txtTitulo.Text;
                            Producto.categoria = txtCategoria.Text;
                            Producto.Descripcion = txtDescripcion.Text;
                            Producto.marca = txtMarca.Text;
                            Producto.Precio = Convert.ToDouble(txtPrecio.Text);
                            Producto.StockMaximo = Convert.ToInt16(txtStockMaximo.Text);
                            Producto.stockMinimo = Convert.ToInt16(txtStockMinimo.Text);
                            Producto.tipoProducto = txtTipo.Text;
                            Producto.picture = lblimg.Text;

                            Producto.productoDVH = "1";

                            _gestorProducto.insertar_producto(Producto);
                            {
                                LlenarTabla();
                                LimpiarCampos();
                                lblInfo.Text = Traductor.Mensaje("ERR158", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
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
                    lblInfo.Text = Traductor.Mensaje("ERR159", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                }
            }
            else
            {
                lblInfo.Text = Traductor.Mensaje("ERR141",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
            }
        }
        
        protected void GridView1_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {
            _gestorProducto.eliminar_producto(GridView1.Rows[e.RowIndex].Cells[2].Text);
            if (!borraMasivo)
            {
                LlenarTabla();
            }
        }
        protected void GridView1_RowUpdating(Object sender, GridViewUpdateEventArgs e)
        {

            BE.Producto miProducto = _gestorProducto.leer_producto(Convert.ToInt32(e.Keys[0].ToString()));

            if (e.NewValues[0] == null || e.NewValues[1] == null || e.NewValues[2] == null || e.NewValues[3] == null)
            {
                lblInfo.Text = Traductor.Mensaje("ERR141",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
            }
            else
            {

               
                miProducto.tituloProducto = e.NewValues[0].ToString();
                miProducto.Descripcion = e.NewValues[1].ToString();
                miProducto.tipoProducto = e.NewValues[2].ToString();
                miProducto.marca = e.NewValues[3].ToString();
                miProducto.categoria = e.NewValues[4].ToString();


                bool chequeoFormato = true;

                if (!_check.ValidarNumerico(e.NewValues[5].ToString()))
                {
                    chequeoFormato = false;
                }
                else
                {
                    miProducto.Precio = Convert.ToDouble(e.NewValues[5].ToString());
                }
                if (!_check.ValidarNumerico(e.NewValues[6].ToString()))
                {
                    chequeoFormato = false;
                }
                else
                {
                    miProducto.StockMaximo = Convert.ToInt16(e.NewValues[6].ToString());
                }
                if (!_check.ValidarNumerico(e.NewValues[7].ToString()))
                {
                    chequeoFormato = false;
                }
                else
                {
                    miProducto.stockMinimo = Convert.ToInt16(e.NewValues[7].ToString());
                }
                if (chequeoFormato)
                {
                    _gestorProducto.insertar_producto(miProducto);
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