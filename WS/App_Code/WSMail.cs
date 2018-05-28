using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Net.Mail;

[WebService(Namespace = "EnvioMail")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]

public class WSMail : System.Web.Services.WebService
{
    public WSMail()
    {
        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod]


    public string EnvioMail(string mailTO, string body, string subject)
    {

        MailMessage mail = new MailMessage();
        mail.To.Add(mailTO);
        mail.From = new MailAddress("info@adviters.com", "Adviters - SUPPORT", System.Text.Encoding.UTF8);
        mail.Subject = subject;
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        mail.Body = body;
        mail.BodyEncoding = System.Text.Encoding.UTF8;
        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.High;
        SmtpClient client = new SmtpClient();
        client.UseDefaultCredentials = false;
        client.EnableSsl = true;
        client.Credentials = new System.Net.NetworkCredential("info@adviters.com", "Info2016!"); 
        client.Port = 587;
        client.Host = "p3plcpnl0394.prod.phx3.secureserver.net";
        client.Timeout = 30000;

        try
        {
            client.Send(mail);
            return "OK + body: " + body;
        }
        catch (Exception ex)
        {
            return "Error " + ex;
        }

    }
    
}