namespace NalogRobot
{
    partial class FileForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileForm));
            this.dgvTaxGrid = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txbSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.taxBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.regNumDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tempFileDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.destFileDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.importStateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.updatedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaxGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.taxBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTaxGrid
            // 
            this.dgvTaxGrid.AllowUserToAddRows = false;
            this.dgvTaxGrid.AllowUserToOrderColumns = true;
            this.dgvTaxGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTaxGrid.AutoGenerateColumns = false;
            this.dgvTaxGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTaxGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.regNumDataGridViewTextBoxColumn,
            this.tempFileDataGridViewTextBoxColumn,
            this.destFileDataGridViewTextBoxColumn,
            this.importStateDataGridViewTextBoxColumn,
            this.updatedDataGridViewTextBoxColumn,
            this.createdDataGridViewTextBoxColumn});
            this.dgvTaxGrid.DataSource = this.taxBindingSource;
            this.dgvTaxGrid.Location = new System.Drawing.Point(12, 91);
            this.dgvTaxGrid.Name = "dgvTaxGrid";
            this.dgvTaxGrid.ReadOnly = true;
            this.dgvTaxGrid.Size = new System.Drawing.Size(760, 458);
            this.dgvTaxGrid.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.txbSearch);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(760, 63);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Поиск";
            // 
            // txbSearch
            // 
            this.txbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txbSearch.Location = new System.Drawing.Point(6, 25);
            this.txbSearch.Name = "txbSearch";
            this.txbSearch.Size = new System.Drawing.Size(658, 22);
            this.txbSearch.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(670, 25);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Найти";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // taxBindingSource
            // 
            this.taxBindingSource.DataSource = typeof(NalogRobot.Tax);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "№";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // regNumDataGridViewTextBoxColumn
            // 
            this.regNumDataGridViewTextBoxColumn.DataPropertyName = "RegNum";
            this.regNumDataGridViewTextBoxColumn.HeaderText = "Рег Номер";
            this.regNumDataGridViewTextBoxColumn.Name = "regNumDataGridViewTextBoxColumn";
            this.regNumDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tempFileDataGridViewTextBoxColumn
            // 
            this.tempFileDataGridViewTextBoxColumn.DataPropertyName = "TempFile";
            this.tempFileDataGridViewTextBoxColumn.HeaderText = "Врем. файл";
            this.tempFileDataGridViewTextBoxColumn.Name = "tempFileDataGridViewTextBoxColumn";
            this.tempFileDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // destFileDataGridViewTextBoxColumn
            // 
            this.destFileDataGridViewTextBoxColumn.DataPropertyName = "DestFile";
            this.destFileDataGridViewTextBoxColumn.HeaderText = "Имя файла ";
            this.destFileDataGridViewTextBoxColumn.Name = "destFileDataGridViewTextBoxColumn";
            this.destFileDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // importStateDataGridViewTextBoxColumn
            // 
            this.importStateDataGridViewTextBoxColumn.DataPropertyName = "ImportState";
            this.importStateDataGridViewTextBoxColumn.HeaderText = "Статус";
            this.importStateDataGridViewTextBoxColumn.Name = "importStateDataGridViewTextBoxColumn";
            this.importStateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // updatedDataGridViewTextBoxColumn
            // 
            this.updatedDataGridViewTextBoxColumn.DataPropertyName = "Updated";
            this.updatedDataGridViewTextBoxColumn.HeaderText = "Обновлен";
            this.updatedDataGridViewTextBoxColumn.Name = "updatedDataGridViewTextBoxColumn";
            this.updatedDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // createdDataGridViewTextBoxColumn
            // 
            this.createdDataGridViewTextBoxColumn.DataPropertyName = "Created";
            this.createdDataGridViewTextBoxColumn.HeaderText = "Создан";
            this.createdDataGridViewTextBoxColumn.Name = "createdDataGridViewTextBoxColumn";
            this.createdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // FileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvTaxGrid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FileForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Список файлов";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaxGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.taxBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTaxGrid;
        private System.Windows.Forms.BindingSource taxBindingSource;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txbSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn regNumDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tempFileDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn destFileDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn importStateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn updatedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdDataGridViewTextBoxColumn;
    }
}