namespace tce.impresionetiqueta.ui
{
    partial class frmTrEtiquetasInvalidas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colDescripcionProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colModeloProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCodigoEan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDescripcionProducto,
            this.colModeloProducto,
            this.colCodigoEan});
            this.dataGridView1.Location = new System.Drawing.Point(12, 65);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(982, 380);
            this.dataGridView1.TabIndex = 0;
            // 
            // colDescripcionProducto
            // 
            this.colDescripcionProducto.DataPropertyName = "descripcionProducto";
            this.colDescripcionProducto.HeaderText = "Descripcion Producto";
            this.colDescripcionProducto.MinimumWidth = 8;
            this.colDescripcionProducto.Name = "colDescripcionProducto";
            this.colDescripcionProducto.ReadOnly = true;
            this.colDescripcionProducto.Width = 300;
            // 
            // colModeloProducto
            // 
            this.colModeloProducto.DataPropertyName = "modeloProducto";
            this.colModeloProducto.HeaderText = "Modelo Producto";
            this.colModeloProducto.MinimumWidth = 8;
            this.colModeloProducto.Name = "colModeloProducto";
            this.colModeloProducto.ReadOnly = true;
            this.colModeloProducto.Width = 150;
            // 
            // colCodigoEan
            // 
            this.colCodigoEan.DataPropertyName = "codigoEAN";
            this.colCodigoEan.HeaderText = "EAN - 13 Invalida";
            this.colCodigoEan.MinimumWidth = 8;
            this.colCodigoEan.Name = "colCodigoEan";
            this.colCodigoEan.ReadOnly = true;
            this.colCodigoEan.Width = 200;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(568, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "Se han detectado codigos EAN-13 invalidos para su posterior revision";
            // 
            // frmTrEtiquetasInvalidas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 457);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.MaximizeBox = false;
            this.Name = "frmTrEtiquetasInvalidas";
            this.Text = "Etiquetas Invalidas";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescripcionProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn colModeloProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCodigoEan;
        private System.Windows.Forms.Label label1;
    }
}