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
    public partial class rmovcustomer : System.Web.UI.Page
    {
        BE.Usuario usuario = SesionActualWindows.SesionActual().ObtenerUsuarioActual();
        BLL.gestorMovCustomer _gestorMovCustomer = new BLL.gestorMovCustomer();
        BLL.gestorConsumidor _gestorConsumidor = new BLL.gestorConsumidor();
        string Cultura = "";
        List<BE.MovCustomer> listaMovCustomer = new List<BE.MovCustomer>();
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                if (SesionActualWindows.SesionActual().ObtenerUsuarioActual() != null)
                {
                    Cultura = SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString();
                    BE.Consumidor consumidor = new BE.Consumidor();
                    if (usuario.Perfil.Nombre == "CONSUMIDOR")
                    {
                        consumidor = _gestorConsumidor.leer_Consumidor_DNI(usuario.idUsuario.ToString());
                    }
                    ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/reportes/reporteMovCus.rdlc");
                    listaMovCustomer = _gestorMovCustomer.leer_mov_Customer(consumidor.idCliente.ToString(), consumidor.idConsumidor.ToString());
                    IEnumerable<BE.MovCustomer> ie;
                    ie = listaMovCustomer.AsQueryable();
                    Microsoft.Reporting.WebForms.ReportDataSource datasource = new Microsoft.Reporting.WebForms.ReportDataSource("movCustomer", ie);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                }

            }

        }
    }
}