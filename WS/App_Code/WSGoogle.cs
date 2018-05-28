using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

[WebService(Namespace = "AccesoGoogle")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]

public class WSGoogle : System.Web.Services.WebService
{
    public WSGoogle()
    {
        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string TraducirTexto(string input, string languagePair)
    {
        try
        {
            string url = String.Format("http://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", input, languagePair);
            System.Net.WebClient webClient = new System.Net.WebClient();
            webClient.Encoding = System.Text.Encoding.UTF8;

            string result = webClient.DownloadString(url);
            string text = getBetween(result, "TRANSLATED_TEXT='", "';");
            return text;
        }
        catch (Exception)
        {
            return input;

        }

    }
    [WebMethod]
    public string ConvertirValor(string input, string languagePair)
    {
        try
        {
            string url = String.Format("http://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", input, languagePair);
            System.Net.WebClient webClient = new System.Net.WebClient();
            webClient.Encoding = System.Text.Encoding.UTF8;

            string result = webClient.DownloadString(url);
            string text = getBetween(result, "TRANSLATED_TEXT='", "';");
            return text;
        }
        catch (Exception)
        {
            return input;

        }

    }
    [WebMethod]
    public string RecuperarValor(string moneda)
    {
        try
        {
            string url = String.Format("https://www.cronista.com/MercadosOnline/moneda.html?id={0}", moneda);
            System.Net.WebClient webClient = new System.Net.WebClient();
            webClient.Encoding = System.Text.Encoding.UTF8;

            string result = webClient.DownloadString(url);
            string text = getBetween(result, "TRANSLATED_TEXT='", "';");
            return text;
        }
        catch (Exception)
        {
            return moneda;

        }

    }
    public static string getBetween(string strSource, string strStart, string strEnd)
    {
        int Start, End;
        if (strSource.Contains(strStart) && strSource.Contains(strEnd))
        {
            Start = strSource.IndexOf(strStart, 0) + strStart.Length;
            End = strSource.IndexOf(strEnd, Start);
            return strSource.Substring(Start, End - Start);
        }
        else
        {
            return "";
        }
    }
    
}