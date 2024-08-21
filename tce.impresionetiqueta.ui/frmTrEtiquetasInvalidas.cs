using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tce.impresionetiqueta.models.clases;

namespace tce.impresionetiqueta.ui
{
    public partial class frmTrEtiquetasInvalidas : Form
    {
        public frmTrEtiquetasInvalidas(ref List<clsEtiquedaDto> listadoEtiquetasInvalidas)
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = listadoEtiquetasInvalidas;
        }
    }
}
