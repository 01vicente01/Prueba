using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Acysa_Maneger.Formularios.Albaranes;


namespace Acysa_Maneger
{
    public partial class Frm_main : Form
    {
        
        public Frm_main()
        {
            InitializeComponent();
        }

        private void Frm_main_Load(object sender, EventArgs e)
        {

        }

        private void albaranesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Abre el formulario Albarán
            Form formulario = new Frm_Albaranes();
            formulario.MdiParent = this;
            formulario.Show();
        }

        private void presupuestoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void parteDeHorasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        
    }
}
