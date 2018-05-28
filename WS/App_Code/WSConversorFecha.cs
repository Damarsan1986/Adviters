using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Descripción breve de WSConversorFecha
/// </summary>
[WebService(Namespace = "WSConversorFecha")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]
public class WSConversorFecha : System.Web.Services.WebService {

    public WSConversorFecha () {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod]
    public DateTime ConvertirFecha(string fecha)    
    {
        return (Convert.ToDateTime(fecha.Replace("T", " ")).AddHours(-2));
    }
    
}
