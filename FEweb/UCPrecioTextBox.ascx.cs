using System; 
using System.Collections.Generic; 
using System.Web; 
using System.Web.UI; 
using System.Web.UI.WebControls; 
using System.Text;

namespace FE
{
    public partial class UCPrecioTextBox : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public bool Enabled
        {
            set { TextBox1.Enabled = value; }
            get { return TextBox1.Enabled; }
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
            if (!ValidaNumerico(TextBox1.Text.Trim()))
            {   
                this.TextBox1.BackColor = System.Drawing.Color.Red;
                //TextBox1.Text =  Format(Numero, "Currency");
                
                
            }
            else
            {
                this.TextBox1.BackColor = System.Drawing.Color.Empty;
                decimal valor = Convert.ToDecimal(TextBox1.Text);

                string valFormat = string.Format("{0:#,#.00}", valor);
                TextBox1.Text = valFormat;

            }
                return;
        }

        //Metodo publico que evalua si el valor ingresado es numero o no.
        public bool ValidaNumerico(object Expression) 
        {
            bool isNum; 
            double retNum;
            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles. Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
   }
    
    
}