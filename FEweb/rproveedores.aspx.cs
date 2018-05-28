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
    public partial class rproveedores : System.Web.UI.Page
    {
        BLL.gestorProveedor _gestorProveedor = new BLL.gestorProveedor();
        List<BE.Proveedor> listaProveedor = new List<BE.Proveedor>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/reportes/reporteProveedores.rdlc");
                listaProveedor = _gestorProveedor.leer_proveedor();
                IEnumerable<BE.Proveedor> ie;
                ie = listaProveedor.AsQueryable();
                Microsoft.Reporting.WebForms.ReportDataSource datasource = new Microsoft.Reporting.WebForms.ReportDataSource("proveedor", ie);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
            }

        }
    }
}