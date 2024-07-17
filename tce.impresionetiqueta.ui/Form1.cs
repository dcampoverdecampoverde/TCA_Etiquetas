using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IronXL;
using Microsoft.Reporting.WinForms;
using Spire.Barcode;
using tce.impresionetiqueta.models.clases;


namespace tce.impresionetiqueta.ui
{
    public partial class Form1 : Form
    {
        private List<clsEtiquedaDto> listadoEtiquetas = new List<clsEtiquedaDto>();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDatos.Rows.Count > 0)
                {
                    listadoEtiquetas.Clear();
                    var indiceSerial = 1;
                    foreach (DataGridViewRow dr in dgvDatos.Rows)
                    {
                        if (dr.Cells[0].Value != null)
                        {
                            clsEtiquedaDto item = new clsEtiquedaDto();
                            item.modeloProducto = dr.Cells[2].Value.ToString();
                            item.contenido = dr.Cells[6].Value.ToString();
                            item.descripcionProducto = dr.Cells[1].Value.ToString();
                            item.codigoEAN = dr.Cells[5].Value.ToString();
                            item.serialProducto = new string('0', (10 - indiceSerial.ToString().Length)) + indiceSerial;
                            item.paisOrigen = dr.Cells[7].Value.ToString();
                            #region Generacion de Etioqueda EAN
                            if (!string.IsNullOrWhiteSpace(item.codigoEAN))
                            {
                                BarcodeSettings settings = new BarcodeSettings();
                                settings.Type = BarCodeType.EAN13;
                                settings.Data = item.codigoEAN;
                                //settings.UseChecksum = CheckSumMode.ForceEnable;
                                settings.ShowTextOnBottom = true;
                                settings.TextAlignment = StringAlignment.Center;
                                BarCodeGenerator generator = new BarCodeGenerator(settings);
                                Image image = generator.GenerateImage();
                                image.Save("EAN-13.png", System.Drawing.Imaging.ImageFormat.Png);
                                item.imagenEtiqueta = File.ReadAllBytes("EAN-13.png");
                                File.Delete("EAN-13.png");
                                //using (var ms = new MemoryStream())
                                //{
                                //    image.Save(ms, image.RawFormat);
                                //    item.imagenEtiqueta = ms.ToArray();
                                //}
                                listadoEtiquetas.Add(item);
                            }
                            #endregion

                            indiceSerial++;
                        }
                    }

                    frmRpImpresion frmRpImpresion = new frmRpImpresion(listadoEtiquetas);
                    frmRpImpresion.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No hay data para generar etiquetas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                

                //image.Save("EAN-13.png", System.Drawing.Imaging.ImageFormat.Png);



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error); //custom messageBox to show error
            }
        }

        private async Task<DataTable> ReadExcel(string fileName)
        {
            return await Task.Run(() =>
            {
                WorkBook workbook = WorkBook.Load(fileName);
                WorkSheet sheet = workbook.DefaultWorkSheet;
                return sheet.ToDataTable(true);
            });
            
        }

        private async void btnSeleccionar_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog file = new OpenFileDialog();
                if (file.ShowDialog() == DialogResult.OK)
                {
                    btnSeleccionar.Enabled = false;
                    string fileExt = Path.GetExtension(file.FileName); //get the file extension
                    if (fileExt.CompareTo(".xls") == 0 || fileExt.CompareTo(".xlsx") == 0)
                    {
                        try
                        {
                            DataTable dtExcel = await ReadExcel(file.FileName); //read excel file
                            //dgvDatos.Visible = true;
                            dgvDatos.DataSource = dtExcel;
                            btnSeleccionar.Enabled = true;
                        }
                        catch (Exception ex)
                        {
                            btnSeleccionar.Enabled = true;
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                    else
                    {
                        btnSeleccionar.Enabled = true;
                        MessageBox.Show("Please choose .xls or .xlsx file only.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); //custom messageBox to show error
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
