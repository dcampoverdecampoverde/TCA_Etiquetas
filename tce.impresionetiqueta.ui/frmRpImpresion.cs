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
    public partial class frmRpImpresion : Form
    {
        List<clsEtiquedaDto> listaEtiquetdaData = new List<clsEtiquedaDto>();
        public frmRpImpresion()
        {
            InitializeComponent();
            
        }

        public frmRpImpresion(List<clsEtiquedaDto> lista)
        {
            InitializeComponent();
            listaEtiquetdaData = lista;
        }

        private void frmRpImpresion_Load(object sender, EventArgs e)
        {
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            bsFuenteDatos.DataSource = listaEtiquetdaData;
            this.reportViewer1.RefreshReport();
        }
    }
}
