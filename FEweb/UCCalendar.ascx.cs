using System; 
using System.Collections.Generic; 
using System.Web; 
using System.Web.UI; 
using System.Web.UI.WebControls; 
using System.Text;

namespace FE
{
    public partial class UCCalendar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string Text
        {
            set { TextBox1.Text = value; }
            get { return TextBox1.Text; }
        }
        public bool Enabled
        {
            set { TextBox1.Enabled = value; }
            get { return TextBox1.Enabled; }
        }

        public System.Drawing.Color BackColor
        {
            set { TextBox1.BackColor = value; }
            get { return TextBox1.BackColor; }
        }

        //UC para validar que el dato ingresado sea numerico, si no lo es pinta el fondo de rojo.
        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBox1.Text))
            {
                this.TextBox1.BackColor = System.Drawing.Color.Empty;
            }
            else
            {


                if (!ValidarFormato(TextBox1.Text))
                {
                    this.TextBox1.BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    this.TextBox1.BackColor = System.Drawing.Color.Empty;

                }
            }
                return;
        }

        //Metodo publico que evalua si el valor ingresado es numero o no.
        public bool ValidarFormato(object Expression) 
        {
            DateTime dt1 = DateTime.Now;
            DateTime dt2 = dt1;
            var culture = System.Globalization.CultureInfo.CreateSpecificCulture("es-AR");
            var styles = System.Globalization.DateTimeStyles.None;

            bool fechaValida = DateTime.TryParse(Convert.ToString(Expression), culture, styles, out dt1);
            
            return fechaValida;

        }
   }
    
    
}