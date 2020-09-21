namespace NalogRobot
{
    partial class SyncForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyncForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbxStat = new System.Windows.Forms.ListBox();
            this.btnSync = new System.Windows.Forms.Button();
            this.chbDelete = new System.Windows.Forms.CheckBox();
            this.chbRemoveRecords = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 252);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(360, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Будет произведена синхронизация файлов в каталоге и базе данных";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 269);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(345, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Отсутствующие файлы в каталоге будут удалены из базы данных.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 287);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(531, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Отметьте \"Удалить файлы\", чтобы удалить из каталога файлы незарегистрированные в " +
    "базе данных.";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbxStat);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(560, 237);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Статистика";
            // 
            // lbxStat
            // 
            this.lbxStat.FormattingEnabled = true;
            this.lbxStat.Location = new System.Drawing.Point(6, 37);
            this.lbxStat.Name = "lbxStat";
            this.lbxStat.Size = new System.Drawing.Size(531, 173);
            this.lbxStat.TabIndex = 0;
            // 
            // btnSync
            // 
            this.btnSync.Location = new System.Drawing.Point(454, 318);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(118, 23);
            this.btnSync.TabIndex = 4;
            this.btnSync.Text = "Синхронизировать";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // chbDelete
            // 
            this.chbDelete.AutoSize = true;
            this.chbDelete.Location = new System.Drawing.Point(315, 322);
            this.chbDelete.Name = "chbDelete";
            this.chbDelete.Size = new System.Drawing.Size(106, 17);
            this.chbDelete.TabIndex = 5;
            this.chbDelete.Text = "Удалить файлы";
            this.chbDelete.UseVisualStyleBackColor = true;
            // 
            // chbRemoveRecords
            // 
            this.chbRemoveRecords.AutoSize = true;
            this.chbRemoveRecords.Location = new System.Drawing.Point(18, 322);
            this.chbRemoveRecords.Name = "chbRemoveRecords";
            this.chbRemoveRecords.Size = new System.Drawing.Size(181, 17);
            this.chbRemoveRecords.TabIndex = 6;
            this.chbRemoveRecords.Text = "Удалить пустые записи из БД";
            this.chbRemoveRecords.UseVisualStyleBackColor = true;
            // 
            // SyncForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.chbRemoveRecords);
            this.Controls.Add(this.chbDelete);
            this.Controls.Add(this.btnSync);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SyncForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Синхронизация";
            this.Load += new System.EventHandler(this.SyncForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lbxStat;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.CheckBox chbDelete;
        private System.Windows.Forms.CheckBox chbRemoveRecords;
    }
}