using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;



//using IronXL;
using Microsoft.Reporting.WinForms;
using Spire.Barcode;
using tce.impresionetiqueta.models.clases;



namespace tce.impresionetiqueta.ui
{
    public partial class Form1 : Form
    {
        List<clsEtiquedaDto> listadoEtiquetasInvalidas = new List<clsEtiquedaDto>();
        private List<clsEtiquedaDto> listadoEtiquetas = new List<clsEtiquedaDto>();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            try
            {
                listadoEtiquetasInvalidas.Clear();
                if (dgvDatos.Rows.Count > 0)
                {
                    listadoEtiquetas.Clear();
                    var indiceSerial = 0;
                    foreach (DataGridViewRow dr in dgvDatos.Rows)
                    {
                        if(indiceSerial > 0)
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
                                #region Valido si el Codigo EAN es valido
                                var eanValido = validateGTIN(item.codigoEAN);
                                if(eanValido == false)
                                {
                                    listadoEtiquetasInvalidas.Add(item);
                                }
                                else
                                {
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
                                }
                                #endregion


                            }
                        }
                        indiceSerial++;
                    }

                    //Si se encuentran etiquetas invalidas, aqui se las debe mostrar
                    if (listadoEtiquetasInvalidas.Count > 0)
                    {
                        frmTrEtiquetasInvalidas frm = new frmTrEtiquetasInvalidas(ref listadoEtiquetasInvalidas);
                        frm.Show();
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
                //WorkBook workbook = WorkBook.Load(fileName);
                //WorkSheet sheet = workbook.DefaultWorkSheet;
                //return sheet.ToDataTable(true);

                FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
                IExcelDataReader excelReader;

                //1. Reading Excel file
                if (Path.GetExtension(fileName).ToUpper() == ".XLS")
                {
                    //1.1 Reading from a binary Excel file ('97-2003 format; *.xls)
                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else
                {
                    //1.2 Reading from a OpenXml Excel file (2007 format; *.xlsx)
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }

                //2. DataSet - The result of each spreadsheet will be created in the result.Tables
                DataSet result = excelReader.AsDataSet();

                //3. DataSet - Create column names from first row
                DataTable dt = result.Tables[0];


                return dt;
            });
            
        }

        public Boolean validateGTIN(string gtin)
        {
            string tmpGTIN = gtin;
            if (tmpGTIN.Length < 13)
            {
                Console.Write("GTIN code is invalid (should be at least 13 digits long)");
                return false;
            }
            else if (tmpGTIN.Length == 13)
            {
                tmpGTIN = "0" + gtin;
            }
            // Now that you have a GTIN with 14 digits, you can check the checksum
            Boolean IsValid = false;
            int Sum = 0;
            int EvenSum = 0;
            int CurrentDigit = 0;
            for (int pos = 0; pos <= 12; ++pos)
            {
                Int32.TryParse(tmpGTIN[pos].ToString(), out CurrentDigit);
                if (pos % 2 == 0)
                {
                    EvenSum += CurrentDigit;
                }
                else
                {
                    Sum += CurrentDigit;
                }
            }
            Sum += 3 * EvenSum;
            Int32.TryParse(tmpGTIN[13].ToString(), out CurrentDigit);
            IsValid = ((10 - (Sum % 10)) % 10) == CurrentDigit;
            if (!IsValid)
            {
                Console.Write("GTIN code is invalid (wrong checksum)");
            }
            return IsValid;
        }

        

        private async void btnSeleccionar_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog file = new OpenFileDialog();
                if (file.ShowDialog() == DialogResult.OK)
                {
                    btnSeleccionar.Enabled = false;
                    btnProcesar.Enabled = false;
                    string fileExt = Path.GetExtension(file.FileName); //get the file extension
                    if (fileExt.CompareTo(".xls") == 0 || fileExt.CompareTo(".xlsx") == 0)
                    {
                        try
                        {
                            DataTable dtExcel = await ReadExcel(file.FileName); //read excel file
                            //dgvDatos.Visible = true;
                            dgvDatos.DataSource = dtExcel;
                            btnSeleccionar.Enabled = true;
                            btnProcesar.Enabled = true;
                        }
                        catch (Exception ex)
                        {
                            btnSeleccionar.Enabled = true;
                            btnProcesar.Enabled = true;
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                    else
                    {
                        btnSeleccionar.Enabled = true;
                        btnProcesar.Enabled = true;
                        MessageBox.Show("Por favor seleccione un archivo con extension .xls o .xlsx unicamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); //custom messageBox to show error
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
