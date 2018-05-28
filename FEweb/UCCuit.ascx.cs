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
    public partial class UCCuit : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        public string Text
        {
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
            if (!validarCuit(TextBox1.Text.Trim()))
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
        private bool validarCuit(string Cuit)
        {
            Regex rg = new Regex("[A-Z_a-z]");
            Cuit = Cuit.Replace("-", "");
            if (rg.IsMatch(Cuit))
                return false;
            if (Cuit.Length != 11)
                return false;
            char[] cuitArray = Cuit.ToCharArray();
            double sum = 0;
            int bint = 0;
            int j = 7;
            for (int i = 5, c = 0; c != 10; i--, c++)
            {
                if (i >= 2)
                    sum += (Char.GetNumericValue(cuitArray[c]) * i);
                else
                    bint = 1;
                if (bint == 1 && j >= 2)
                {
                    sum += (Char.GetNumericValue(cuitArray[c]) * j);
                    j--;
                }
            }

            if ((cuitArray.Length - (sum % 11)) == Char.GetNumericValue(cuitArray[cuitArray.Length - 1]))
                return true;
            return false;
        }

    }
}
    
    
