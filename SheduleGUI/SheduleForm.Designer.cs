namespace SheduleGUI
{
    partial class g_SheduleForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.g_bSaveAs = new System.Windows.Forms.Button();
            this.g_bGenerateShedule = new System.Windows.Forms.Button();
            this.g_dgvShedule = new System.Windows.Forms.DataGridView();
            this.g_tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.g_tsmiOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.g_dgvShedule)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.g_dgvShedule, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(733, 380);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 407F));
            this.tableLayoutPanel2.Controls.Add(this.g_bGenerateShedule, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.g_bSaveAs, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(727, 74);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // g_bSaveAs
            // 
            this.g_bSaveAs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.g_bSaveAs.Location = new System.Drawing.Point(10, 10);
            this.g_bSaveAs.Margin = new System.Windows.Forms.Padding(10);
            this.g_bSaveAs.Name = "g_bSaveAs";
            this.g_bSaveAs.Size = new System.Drawing.Size(140, 54);
            this.g_bSaveAs.TabIndex = 1;
            this.g_bSaveAs.Text = "Сохранить";
            this.g_bSaveAs.UseVisualStyleBackColor = true;
            this.g_bSaveAs.Click += new System.EventHandler(this.g_bSaveAs_Click);
            // 
            // g_bGenerateShedule
            // 
            this.g_bGenerateShedule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.g_bGenerateShedule.Location = new System.Drawing.Point(170, 10);
            this.g_bGenerateShedule.Margin = new System.Windows.Forms.Padding(10);
            this.g_bGenerateShedule.Name = "g_bGenerateShedule";
            this.g_bGenerateShedule.Size = new System.Drawing.Size(140, 54);
            this.g_bGenerateShedule.TabIndex = 0;
            this.g_bGenerateShedule.Text = "Сгенерировать расписание";
            this.g_bGenerateShedule.UseVisualStyleBackColor = true;
            this.g_bGenerateShedule.Visible = false;
            this.g_bGenerateShedule.Click += new System.EventHandler(this.g_bGenerateShedule_Click);
            // 
            // g_dgvShedule
            // 
            this.g_dgvShedule.AllowUserToAddRows = false;
            this.g_dgvShedule.AllowUserToDeleteRows = false;
            this.g_dgvShedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.g_dgvShedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.g_dgvShedule.Location = new System.Drawing.Point(3, 83);
            this.g_dgvShedule.Name = "g_dgvShedule";
            this.g_dgvShedule.Size = new System.Drawing.Size(727, 294);
            this.g_dgvShedule.TabIndex = 1;
            // 
            // g_tsmiFile
            // 
            this.g_tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.g_tsmiOpen});
            this.g_tsmiFile.Name = "g_tsmiFile";
            this.g_tsmiFile.Size = new System.Drawing.Size(48, 20);
            this.g_tsmiFile.Text = "Файл";
            // 
            // g_tsmiOpen
            // 
            this.g_tsmiOpen.Name = "g_tsmiOpen";
            this.g_tsmiOpen.Size = new System.Drawing.Size(121, 22);
            this.g_tsmiOpen.Text = "Открыть";
            this.g_tsmiOpen.Click += new System.EventHandler(this.g_tsmiOpen_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.g_tsmiFile});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(733, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // g_SheduleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 404);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "g_SheduleForm";
            this.Text = "Генератор расписания";
            this.Load += new System.EventHandler(this.g_SheduleForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.g_dgvShedule)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button g_bGenerateShedule;
        private System.Windows.Forms.Button g_bSaveAs;
        private System.Windows.Forms.DataGridView g_dgvShedule;
        private System.Windows.Forms.ToolStripMenuItem g_tsmiFile;
        private System.Windows.Forms.ToolStripMenuItem g_tsmiOpen;
        private System.Windows.Forms.MenuStrip menuStrip1;
    }
}

