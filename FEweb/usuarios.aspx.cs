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
    public partial class usuarios : System.Web.UI.Page
    {
        public GestorBitacora _gestorBitacora = new GestorBitacora();
        public GestorMensaje _gestormensaje = new GestorMensaje();
        public GestorUsuario _gestorUsuario = new GestorUsuario();
        public GestorCultura _gestorCultura = new GestorCultura();
        public GestorPermiso _gestorPermiso = new GestorPermiso();
        public BLL.gestorCliente _gestorCliente = new BLL.gestorCliente();
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
                    LlenarTabla();
                    LlenarListas();
                    phAltaUsuario.Visible = false;
                    phBotonera.Visible = true;
                    phListaUsuarios.Visible = true;
                    phListaPermisos.Visible = false;
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
            BE.Usuario usuario = SesionActualWindows.SesionActual().ObtenerUsuarioActual();
            if (usuario.Perfil.Nombre == "CLIENTE")
            {
                BE.Cliente cliente = _gestorCliente.leer_cliente_CUIT(usuario.idUsuario.ToString());
                GridView1.DataSource = _gestorUsuario.leer_usuario_Empresa(cliente.idCliente.ToString());
            }                                           
            else
            {
                if (usuario.Perfil.Nombre == "ADM MASTER")
                {
                    List<BE.Usuario> listaUsuario = new List<BE.Usuario>() ;
                    listaUsuario.Add(_gestorUsuario.leer_usuario("admin"));

                    GridView1.DataSource = listaUsuario;
                }
                else
                {
                    GridView1.DataSource = _gestorUsuario.leer_usuario();
                }
            }
            GridView1.DataBind();
        }

        protected void LlenarListas()
        {
            lstCultura.DataSource = _gestorCultura.leer_cultura();
            lstCultura.DataTextField = "descripcion";
            lstCultura.DataValueField = "idCultura";
            lstCultura.DataBind();

            BE.Usuario usuario = SesionActualWindows.SesionActual().ObtenerUsuarioActual();
            if (usuario.Perfil.Nombre == "CLIENTE")
            {
                BE.Cliente cliente = _gestorCliente.leer_cliente_CUIT(usuario.idUsuario.ToString());
                BE.PermisoFiltro perm = new BE.PermisoFiltro();
                perm.Nombre = "CONSUMIDOR";
                lstPerfil.DataSource = _gestorPermiso.leer_permiso(perm);
            }
            else
            {
                lstPerfil.DataSource = _gestorPermiso.leer_permiso();
            }
            lstPerfil.DataTextField = "Descripcion";
            lstPerfil.DataValueField = "Nombre";
            lstPerfil.DataBind();


        }
        protected void LimpiarCampos()
        {
            txtIdUsuario.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtClave.Text = "";
            txtClaveRepetir.Text = "";
            txtEmail.Text = "";
            chkBloqueado.Checked = false;

        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            phAltaUsuario.Visible = true;
            phBotonera.Visible = false;
            phListaUsuarios.Visible = false;
            phListaPermisos.Visible = false;
        }
        protected void btnEliminarSeleccionados_Click(object sender, EventArgs e)
        {
            ;
        }
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            phAltaUsuario.Visible = false;
            phBotonera.Visible = true;
            phListaUsuarios.Visible = true;
            lblMensaje.Text = "";
            phListaPermisos.Visible = false;

        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtIdUsuario.Text) && !String.IsNullOrEmpty(txtNombre.Text) &&  !String.IsNullOrEmpty(txtApellido.Text) && !String.IsNullOrEmpty(txtClave.Text) && !String.IsNullOrEmpty(txtEmail.Text)) 
            {
                if (txtClave.Text == txtClaveRepetir.Text)
                {
                    if (txtClave.Text.Length < 6)
                    {
                        lblMensaje.Text = Traductor.Mensaje("ERR148",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                    }
                    else
                    {
                        try
                        {
                            BE.Usuario usuario = new BE.Usuario();
                            usuario.idUsuario = txtIdUsuario.Text;
                            usuario.Nombre = txtNombre.Text;
                            usuario.apellido = txtApellido.Text;
                            usuario.Clave = txtClave.Text;
                            usuario.Email = txtEmail.Text;
                            usuario.Bloqueado = chkBloqueado.Checked;
                            usuario.IntentosFallidos = 0;
                            usuario.Cultura = new System.Globalization.CultureInfo(lstCultura.SelectedValue.ToString());
                            usuario.Perfil = new BE.PermisoSimple();
                            usuario.Perfil.Nombre = lstPerfil.SelectedItem.Value.ToString();
                            usuario.usuarioDVH = "1";

                            if (_gestorUsuario.escribir_usuario(usuario))
                            {
                                LlenarTabla();
                                LimpiarCampos();
                                lblMensaje.Text = Traductor.Mensaje("ERR161",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
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
                }
                 else
                {
                    lblMensaje.Text = Traductor.Mensaje("ERR150",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                }
            }
            else
            {
                lblMensaje.Text = Traductor.Mensaje("ERR141",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
            }
        }
        
        protected void GridView1_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {
            _gestorUsuario.eliminar_usuario(GridView1.Rows[e.RowIndex].Cells[2].Text);
            if (!borraMasivo)
            {
                LlenarTabla();
            }
            phListaPermisos.Visible = false;
        }
        protected void GridView1_RowUpdating(Object sender, GridViewUpdateEventArgs e)
        {

            BE.Usuario miUsuario = _gestorUsuario.leer_usuario(e.Keys[0].ToString());

            if (e.NewValues[0] == null || e.NewValues[1] == null || e.NewValues[2] == null || e.NewValues[3] == null)
            {
                lblInfo.Text = Traductor.Mensaje("ERR141",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
            }
            else
            {

            
                bool chequeoFormato = true;
                if (!_check.ValidarNoNumerico(e.NewValues[0].ToString()) || !_check.ValidarTextoNumEspacio(e.NewValues[0].ToString()))
                {
                    chequeoFormato = false;
                }
                if (!_check.ValidarNoNumerico(e.NewValues[1].ToString()) || !_check.ValidarTextoNumEspacio(e.NewValues[1].ToString()))
                {
                    chequeoFormato = false;
                }
                if (!_check.ValidarEMail(e.NewValues[2].ToString()))
                {
                    chequeoFormato = false;
                }
                if (!_check.ValidarNumerico(e.NewValues[3].ToString()))
                {
                    chequeoFormato = false;
                }
                else
                {
                    miUsuario.IntentosFallidos = Convert.ToByte(e.NewValues[3].ToString());
                }

                
                miUsuario.Nombre = e.NewValues[0].ToString();
                miUsuario.apellido = e.NewValues[1].ToString();
                miUsuario.Email = e.NewValues[2].ToString();

                CheckBox bloqueo = GridView1.Rows[e.RowIndex].Cells[9].Controls[0] as CheckBox;
                miUsuario.Bloqueado = bloqueo.Checked;
                

                DropDownList combo = GridView1.Rows[e.RowIndex].FindControl("lstPerfilTabla") as DropDownList;
                miUsuario.Perfil = new BE.PermisoSimple();
                miUsuario.Perfil.Nombre = combo.SelectedItem.Value.ToString();

                DropDownList comboCultura = GridView1.Rows[e.RowIndex].FindControl("lstCultura") as DropDownList;
                miUsuario.Cultura = new System.Globalization.CultureInfo(comboCultura.SelectedValue.ToString());

                if (chequeoFormato)
                {
                    _gestorUsuario.escribir_usuario(miUsuario);
                    GridView1.EditIndex = -1;
                    lblInfo.Text = "";
                    lblMensaje.Text = "";
                    LlenarTabla();
                }
                else
                {
                    lblMensaje.Text = Traductor.Mensaje("ERR169", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());

                }
            }
        
        }
        protected void GridView1_RowEditing(Object sender, GridViewEditEventArgs e)
        {
            //Todas los campos excepto dropdownlist
            GridView1.EditIndex = e.NewEditIndex;
            LlenarTabla();
            lblInfo.Text = "";

            //Perfil
            DropDownList combo = GridView1.Rows[e.NewEditIndex].FindControl("lstPerfilTabla") as DropDownList;
            
            string usr = GridView1.DataKeys[e.NewEditIndex].Value.ToString();
            BE.Usuario usuario = _gestorUsuario.leer_usuario(usr);

            if (combo != null)
            {

                BE.Usuario usuario1 = SesionActualWindows.SesionActual().ObtenerUsuarioActual();
                if (usuario1.Perfil.Nombre == "CLIENTE")
                {
                    BE.Cliente cliente = _gestorCliente.leer_cliente_CUIT(usuario1.idUsuario.ToString());
                    BE.PermisoFiltro perm = new BE.PermisoFiltro();
                    perm.Nombre = "CONSUMIDOR";
                    combo.DataSource = _gestorPermiso.leer_permiso(perm);
                }
                else
                {
                    combo.DataSource = _gestorPermiso.leer_permiso();
                }
                combo.DataTextField = "Descripcion";
                combo.DataValueField = "Nombre";
                combo.DataBind();
                combo.SelectedValue = Convert.ToString(usuario.Perfil.Nombre);
            }

            //Cultura
            DropDownList comboCultura = GridView1.Rows[e.NewEditIndex].FindControl("lstCultura") as DropDownList;

            if (combo != null)
            {
                comboCultura.DataSource = _gestorCultura.leer_cultura();
                comboCultura.DataTextField = "descripcion";
                comboCultura.DataValueField = "idCultura";
                comboCultura.DataBind();
                comboCultura.SelectedValue = Convert.ToString(usuario.Cultura);
            }


        }
        protected void GridView1_RowCancelingEdit(Object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            LlenarTabla();
            lblInfo.Text = "";
            phListaPermisos.Visible = false;
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
        
            for (int i = 0; i<=(GridView1.Rows.Count - 1);i++)
            {
                //CheckBox CheckBoxElim = (CheckBox)(GridView1.Rows(i).FindControl("chkEliminar");
                CheckBox chkElim = GridView1.Rows[i].FindControl("chkEliminar") as CheckBox;
                if (chkElim.Checked)
                {

                    totalItemSeleccionados += 1;
                    if (totalItemSeleccionados ==1)
                    {
                        string pFiltro = "";
                        Label texto = GridView1.Rows[i].FindControl("lblPerfilTabla") as Label;
                        if (texto != null)
                        {
                            pFiltro = texto.Text;
                        }
                        else
                        {
                            DropDownList combo = GridView1.Rows[i].FindControl("lstPerfilTabla") as DropDownList;
                            pFiltro = combo.Text;
                        }
                        if (pFiltro != "")
                        {
                            MostrarPermisos(pFiltro);
                        }
                    }
                }
            }
            if (totalItemSeleccionados !=1)
            {
                phListaPermisos.Visible = false;
            }
        }

        protected void lstPerfil_TextChanged(object sender, EventArgs e)
        {
            string pFiltro = lstPerfil.Text;
            MostrarPermisos(pFiltro);
        }

        protected void MostrarPermisos(string pFiltro)
        {
            BE.PermisoFiltro filtro = new BE.PermisoFiltro();
            filtro.Nombre = pFiltro;
            BE.PermisoBase entidad = _gestorPermiso.leer_UnPermiso(filtro);

            phListaPermisos.Visible = true;

            if (entidad.esAccion)
            {
                GridView2.DataSource = null;
                GridView2.AutoGenerateColumns = false;
                GridView2.DataSource = _gestorPermiso.leer_permiso(filtro);
                GridView2.DataBind();
            }
            else
            {
                GridView2.DataSource = null;
                GridView2.AutoGenerateColumns = false;
                GridView2.DataSource = _gestorPermiso.leer_permiso();
                GridView2.DataBind();
                LlenarListaPermiso(entidad);
            }
        }

        protected void LlenarTablaP_Hijos()
        {
            GridView2.DataSource = null;
            GridView2.AutoGenerateColumns = false;
            GridView2.DataSource = _gestorPermiso.leer_permiso();
            GridView2.DataBind();
        }

        protected void LlenarListaPermiso(BE.PermisoBase excluirPermiso)
        {
            if (excluirPermiso == null)
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

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            LlenarTablaP_Hijos();
        }
    }

}