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
    public partial class container : System.Web.UI.MasterPage
    {
        public GestorBitacora _gestorBitacora = new GestorBitacora();
        public GestorMensaje _gestormensaje = new GestorMensaje();
        public GestorMenu _gestorMenu = new GestorMenu();
        public GestorCultura _gestorCultura = new GestorCultura();
        

        protected void Page_Load(object sender, EventArgs e)
        {

             
                //Valido que no sea un retorno a la página para evitar volver a cargar los menúes.
                if (!this.IsPostBack)
                {

                    //valido que tenga haya establecida una sesion valida, sino lo redirecciono al login.
                    if (SesionActualWindows.SesionActual().ObtenerUsuarioActual() != null)

                    {

                        var relativeUrl = VirtualPathUtility.ToAppRelative(new Uri(Context.Request.Url.PathAndQuery, UriKind.Relative).ToString());

                        if (relativeUrl != "~/main.aspx")
                        {
        
                            if (relativeUrl != "~/forbidden.aspx")
                            {

                                if (SesionActualWindows.SesionActual().TienePermisoPara(relativeUrl))
                                {
                                    PopulateMenues();
                                }
                                else
                                {
                                    Page.Response.Redirect("forbidden.aspx");
                                }
                            }
                        }
                        else
                        {
                            PopulateMenues();
                        }
                        string Cultura = SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString();
                        Traductor.TraducirControles(Page.Form.Controls, Cultura); 

                    }
                    else 
                    { 
                        Page.Response.Redirect("login.aspx"); 
                    }

                }
        }

        public void PopulateMenues()
        {
            PopulateMenuSistema(_gestorMenu.leer_parentMenu(0), 0, null, Path.GetFileName(Request.Url.AbsolutePath));
            PopulateMenuUsuario();
        }
        public void PopulateMenuUsuario()
        {
            MenuItem menuUsuario = new MenuItem
            {
                Value = "menuUsuario",
                Text = SesionActualWindows.SesionActual().ObtenerUsuarioActual().idUsuario.ToString(),
                NavigateUrl = "javascript:;"
            };
            MenuUsuario.Items.Add(menuUsuario);

            MenuItem subMenuUsuario = new MenuItem
            {
                Value = "menuPerfil",
                Text = "Perfil",
                NavigateUrl = "~/perfil.aspx"
            };
            menuUsuario.ChildItems.Add(subMenuUsuario);
            
            subMenuUsuario = new MenuItem
            {
                Value = "menuSesion",
                Text = "Cerrar Sesión",
                NavigateUrl = "~/logoff.aspx"
            };
            menuUsuario.ChildItems.Add(subMenuUsuario);

            MenuItem menuCultura = new MenuItem
            {
                Value = "menuCultura",
                Text = SesionActualWindows.SesionActual().ObtenerCulturaActual().ToString(),
                NavigateUrl = "javascript:;"
            };
            MenuUsuario.Items.Add(menuCultura);


            int i = 0; 
            foreach (BE.Cultura cultura in _gestorCultura.leer_cultura())
            {
                i += 1;
                MenuItem subMenuCultura = new MenuItem
                {
                    Value = i+"menuCultura",
                    Text = cultura.idCultura.ToString()
                };
                menuCultura.ChildItems.Add(subMenuCultura);
                
                
            }
        }
        public void PopulateMenuSistema(List<BE.Menu> listaMenu, int parentMenuId, MenuItem parentMenuItem, string currentPage)
        {
            foreach (BE.Menu item in listaMenu)
            {
                MenuItem menuItem = new MenuItem
                {
                    Value = item.menuId.ToString(),
                    Text = item.titulo.ToString(),
                    NavigateUrl = item.url.ToString(),
                    Selected = item.url.ToString().EndsWith(currentPage, StringComparison.CurrentCultureIgnoreCase)
                };

                    if (parentMenuId == 0)
                    {
                        MenuSistema.Items.Add(menuItem);
                        List<BE.Menu> listaHijos = _gestorMenu.leer_parentMenu(Int32.Parse(menuItem.Value));
                        PopulateMenuSistema(listaHijos, int.Parse(menuItem.Value), menuItem, "");
                    }
                    else
                    {
                        if (SesionActualWindows.SesionActual().TienePermisoPara(menuItem.NavigateUrl))
                        {
                            parentMenuItem.ChildItems.Add(menuItem);
                        }
                    }
            }
        }
        protected void trd_click(object sender, MenuEventArgs e)
        {
            BE.Usuario usuario = new BE.Usuario();
            usuario = SesionActualWindows.SesionActual().ObtenerUsuarioActual();
            usuario.Cultura = new System.Globalization.CultureInfo(e.Item.Text);
            SesionActualWindows.SesionActual().EstablecerCulturaActual(usuario);
            Traductor.TraducirControles(Page.Form.Controls, e.Item.Text);            
        }
    }
}