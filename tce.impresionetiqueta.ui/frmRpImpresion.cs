using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
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
            lGenerarReportePDF("rptImpresionEtiqueta.rdlc", new List<ReportDataSource> { new ReportDataSource("dsEtiquetas", listaEtiquetdaData) });
            this.Hide();
        }

        private void lGenerarReportePDF(string tsnombreReporte, List<ReportDataSource> toListReportDataSource, List<ReportParameter> toListReportParameter = null)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                ReportViewer poReportViewer = new ReportViewer();
                poReportViewer.ProcessingMode = ProcessingMode.Local;
                poReportViewer.LocalReport.ReportPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\" + tsnombreReporte;
                foreach (ReportDataSource poReportDataSource in toListReportDataSource)
                    poReportViewer.LocalReport.DataSources.Add(poReportDataSource);
                if (toListReportParameter != null)
                {
                    foreach (ReportParameter poReportParameter in toListReportParameter)
                        poReportViewer.LocalReport.SetParameters(poReportParameter);
                }
                byte[] Bytes = poReportViewer.LocalReport.Render(format: "PDF", deviceInfo: "");
                string path = lgetFullPath(".pdf");
                using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
                    stream.Write(Bytes, 0, Bytes.Length);
                System.Diagnostics.Process.Start(path);
                this.Cursor = Cursors.Default;
            }
            catch { this.Cursor = Cursors.Default; }
            this.Cursor = Cursors.Default;
        }

        private string lgetFullPath(string extension)
        {
            string pstempDirectory = Path.GetFullPath(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()));
            Directory.CreateDirectory(pstempDirectory);
            return pstempDirectory + "\\POP_" + Guid.NewGuid().ToString() + extension;
        }
    }
}
