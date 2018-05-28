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
    public partial class rproductos : System.Web.UI.Page
    {
        BLL.gestorProducto _gestorProducto = new BLL.gestorProducto();
        List<BE.Producto> listaProducto = new List<BE.Producto>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/reportes/reporteProductos.rdlc");
                listaProducto =  _gestorProducto.leer_producto();
                IEnumerable<BE.Producto> ie;
                ie = listaProducto.AsQueryable();
                Microsoft.Reporting.WebForms.ReportDataSource datasource = new Microsoft.Reporting.WebForms.ReportDataSource("productos", ie);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
            }

        }
    }
}