using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seguridad;
using BLL;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace FE
{
    public partial class adminbd : System.Web.UI.Page
    {
        GestorDVV _gestorDVV = new GestorDVV();
        GestorBitacora _gestorBitacora = new GestorBitacora();
        GestorUsuario _gestorUsuario = new GestorUsuario();
        GestorIntegridad _gestorIntegridad = new GestorIntegridad();
        GestorIntegridadBLL _gestorIntegridadBLL = new GestorIntegridadBLL();
        GestorBackup _gestorBackup = new GestorBackup();
        
        string Cultura = "";
        
        BE.bitacora bitacora = new BE.bitacora();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SesionActualWindows.SesionActual().ObtenerUsuarioActual() != null)
            {
                Cultura = SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString();
            }
            //Traductor.TraducirControles(Page.Form.Controls, Cultura);
            lblInfo.Text = "";
        }
        protected void btnBackUp_Click(object sender, EventArgs e)
        {
            BE.BackUp bkup = new BE.BackUp();

            bkup.nombre = "bkup_db_";
            bkup.fechaHora = DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString() + DateTime.Today.Day.ToString() + DateTime.Now.Hour.ToString("D2") + DateTime.Now.Minute.ToString("D2") + DateTime.Now.Second.ToString("D2");
            bkup.directorio = @"C:\bkups\";

            try
            {
                if (!Directory.Exists(bkup.directorio))
                {
                    Directory.CreateDirectory(bkup.directorio);
                }
            }
            catch (Exception)
            {
                lblInfo.Text = "DIR ERROR";
            }

            try
            {
                if (_gestorBackup.escribir_backup(bkup))
                {
                    lblInfo.Text = Traductor.Mensaje("ERR106", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());

                    bitacora.idUsuario = SesionActualWindows.SesionActual().ObtenerUsuarioActual().idUsuario.ToString();
                    bitacora.descripcion = "Punto de Respaldo Correcto";
                    _gestorBitacora.escribir_bitacora(bitacora);
                }
                else
                {
                    bitacora.idUsuario = SesionActualWindows.SesionActual().ObtenerUsuarioActual().idUsuario.ToString();
                    bitacora.descripcion = "Intento de Backup Erróneo";
                    _gestorBitacora.escribir_bitacora(bitacora);
                    lblInfo.Text = Traductor.Mensaje("ERR107", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                }
            }
            catch (Exception)
            {

                lblInfo.Text = "BD - BKUP ERROR";
            }

        }
        protected void btnRestore_Click(object sender, EventArgs e)
        {
            
            if (FileUpload1.HasFile)
            {
                FileInfo FI = new FileInfo(FileUpload1.FileName);

                byte[] documentContent = FileUpload1.FileBytes;

                string NombreArchivo = FI.Name;
                string ExtensionArchivo = FI.Extension;
                string Ruta = FI.FullName;
                string RutaArchivo = FI.DirectoryName;
                int largo = FI.Name.Length;
                int limite = largo - 4 - 8;


                if (ExtensionArchivo == ".bak")
                {
                    BE.BackUp bkup = new BE.BackUp();

                    if (NombreArchivo == "")
                    {
                        lblInfo.Text = Traductor.Mensaje("ERR108",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString()); ;
                    }
                    else
                    {
                        bkup.nombre = NombreArchivo.Substring(0, 8);
                        bkup.fechaHora = NombreArchivo.Substring(8, limite);
                        bkup.directorio = "C:\\bkups\\";

                        try
                        {
                            if (_gestorBackup.escribir_restore(bkup))
                            {
                                lblInfo.Text = Traductor.Mensaje("ERR109", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                                bitacora.idUsuario = SesionActualWindows.SesionActual().ObtenerUsuarioActual().idUsuario.ToString();
                                bitacora.descripcion = "Recovery Correcto";
                                _gestorBitacora.escribir_bitacora(bitacora);
                            }
                            else
                            {
                                lblInfo.Text = Traductor.Mensaje("ERR110", SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                                bitacora.idUsuario = SesionActualWindows.SesionActual().ObtenerUsuarioActual().idUsuario.ToString();
                                bitacora.descripcion = "No se pudo hacer el recovery";
                                _gestorBitacora.escribir_bitacora(bitacora);
                            }
                        }
                        catch (Exception)
                        {
                            lblInfo.Text = "BD - Restore Error";
                        }
                        
                    }
                }
                else
                {
                    lblInfo.Text = Traductor.Mensaje("ERR111",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString());
                }
            }
            else
            {
                lblInfo.Text = Traductor.Mensaje("ERR112",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString()); 
            }
            

      

        }
        protected void btnRecalcularDVV_Click(object sender, EventArgs e)
        {
            _gestorIntegridad.recalcularDVV();
            _gestorIntegridadBLL.recalcularDVV();

            lblInfo.Text = Traductor.Mensaje("ERR114",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString()); 
        }

        protected void btnRecalcularDVH_Click(object sender, EventArgs e)
        {
            _gestorIntegridad.recalcular_t_Bitacora("corregir");
            _gestorIntegridad.recalcular_t_Usuario("corregir");
            _gestorIntegridad.recalcular_t_Cultura("corregir");
            _gestorIntegridad.recalcular_t_Mensaje("corregir");
            _gestorIntegridad.recalcular_t_Menu("corregir");
            _gestorIntegridad.recalcular_t_Permisos("corregir");
            _gestorIntegridad.recalcular_t_PermisoPermiso("corregir");
            _gestorIntegridadBLL.recalcular_t_Moneda("corregir");
            _gestorIntegridadBLL.recalcular_t_Movimiento("corregir");
            _gestorIntegridadBLL.recalcular_t_tipoCambio("corregir");
            _gestorIntegridadBLL.recalcular_t_Cliente("corregir");
            _gestorIntegridadBLL.recalcular_t_Comprobante("corregir");
            _gestorIntegridadBLL.recalcular_t_Consumidor("corregir");
            _gestorIntegridadBLL.recalcular_t_DComprobante("corregir");
            _gestorIntegridadBLL.recalcular_t_MovCustomer("corregir");
            _gestorIntegridadBLL.recalcular_t_MovEmpresa("corregir");
            _gestorIntegridadBLL.recalcular_t_Producto("corregir");
            _gestorIntegridadBLL.recalcular_t_Proveedor("corregir");

            lblInfo.Text = Traductor.Mensaje("ERR113",SesionActualWindows.SesionActual().ObtenerUsuarioActual().Cultura.ToString()); 
        }
    }


}