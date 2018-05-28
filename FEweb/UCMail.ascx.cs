using System; 
using System.Collections.Generic; 
using System.Web; 
using System.Web.UI; 
using System.Web.UI.WebControls; 
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace FE
{
    public partial class UCMail : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        public string Text {
            set { TextBox1.Text = value; }
            get { return TextBox1.Text; }
        }
        public System.Drawing.Color BackColor
        {
            set { TextBox1.BackColor = value; }
            get { return TextBox1.BackColor; }
        }

        //UC para validar que el dato ingresado sea numerico, si no lo es pinta el fondo de rojo.
        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (!IsValidEmail(TextBox1.Text.Trim()))
            {
                this.TextBox1.BackColor = System.Drawing.Color.IndianRed;
            }
            else
            {
                this.TextBox1.BackColor = System.Drawing.Color.Empty;
            }
                return;
        }

        //Metodo publico que evalua la estructura del dato ingresado.
 
        bool invalid = false;

        public bool IsValidEmail(string strIn)
        {
            invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            try {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper,
                                    RegexOptions.None, TimeSpan.FromMilliseconds(200));
                }
                catch (RegexMatchTimeoutException) {
                    return false;
                }

            if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format.
                try 
                {
                    return Regex.IsMatch(strIn,
                        @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                        RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                }
                catch (RegexMatchTimeoutException) 
                {
                    return false;
                }
        }

        private string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException) {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }
    }
   }
    
    
