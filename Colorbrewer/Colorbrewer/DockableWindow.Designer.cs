namespace Colorbrewer
{
    partial class DockableWindow
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnLocate = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CboLayers = new System.Windows.Forms.ComboBox();
            this.CboRenderers = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblClasses = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.CboColors = new System.Windows.Forms.ComboBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.CboFields = new System.Windows.Forms.ComboBox();
            this.lblField = new System.Windows.Forms.Label();
            this.lblRenderers = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtFilePath);
            this.groupBox1.Controls.Add(this.btnLocate);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(7, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(388, 51);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File Locator";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(6, 18);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(295, 23);
            this.txtFilePath.TabIndex = 2;
            this.txtFilePath.TextChanged += new System.EventHandler(this.txtFilePath_TextChanged);
            // 
            // btnLocate
            // 
            this.btnLocate.Location = new System.Drawing.Point(307, 15);
            this.btnLocate.Name = "btnLocate";
            this.btnLocate.Size = new System.Drawing.Size(75, 27);
            this.btnLocate.TabIndex = 1;
            this.btnLocate.Text = "Locate";
            this.btnLocate.UseVisualStyleBackColor = true;
            this.btnLocate.Click += new System.EventHandler(this.btnLocate_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(3, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(88, 19);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Colorbrewer";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CboLayers);
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(7, 79);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(388, 53);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Layer Selection";
            // 
            // CboLayers
            // 
            this.CboLayers.FormattingEnabled = true;
            this.CboLayers.Location = new System.Drawing.Point(6, 20);
            this.CboLayers.Name = "CboLayers";
            this.CboLayers.Size = new System.Drawing.Size(376, 23);
            this.CboLayers.TabIndex = 0;
            // 
            // CboRenderers
            // 
            this.CboRenderers.FormattingEnabled = true;
            this.CboRenderers.Location = new System.Drawing.Point(63, 48);
            this.CboRenderers.Name = "CboRenderers";
            this.CboRenderers.Size = new System.Drawing.Size(319, 23);
            this.CboRenderers.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblClasses);
            this.groupBox3.Controls.Add(this.comboBox1);
            this.groupBox3.Controls.Add(this.CboColors);
            this.groupBox3.Controls.Add(this.lblColor);
            this.groupBox3.Controls.Add(this.CboFields);
            this.groupBox3.Controls.Add(this.lblField);
            this.groupBox3.Controls.Add(this.lblRenderers);
            this.groupBox3.Controls.Add(this.CboRenderers);
            this.groupBox3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(7, 138);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(388, 141);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Symbology";
            // 
            // lblClasses
            // 
            this.lblClasses.AutoSize = true;
            this.lblClasses.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClasses.Location = new System.Drawing.Point(6, 112);
            this.lblClasses.Name = "lblClasses";
            this.lblClasses.Size = new System.Drawing.Size(43, 13);
            this.lblClasses.TabIndex = 6;
            this.lblClasses.Text = "Classes";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(63, 107);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(319, 23);
            this.comboBox1.TabIndex = 4;
            // 
            // CboColors
            // 
            this.CboColors.FormattingEnabled = true;
            this.CboColors.Location = new System.Drawing.Point(63, 78);
            this.CboColors.Name = "CboColors";
            this.CboColors.Size = new System.Drawing.Size(319, 23);
            this.CboColors.TabIndex = 5;
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColor.Location = new System.Drawing.Point(6, 83);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(32, 13);
            this.lblColor.TabIndex = 4;
            this.lblColor.Text = "Color";
            // 
            // CboFields
            // 
            this.CboFields.FormattingEnabled = true;
            this.CboFields.Location = new System.Drawing.Point(63, 19);
            this.CboFields.Name = "CboFields";
            this.CboFields.Size = new System.Drawing.Size(319, 23);
            this.CboFields.TabIndex = 4;
            this.CboFields.SelectedIndexChanged += new System.EventHandler(this.CboFields_SelectedIndexChanged);
            // 
            // lblField
            // 
            this.lblField.AutoSize = true;
            this.lblField.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblField.Location = new System.Drawing.Point(6, 24);
            this.lblField.Name = "lblField";
            this.lblField.Size = new System.Drawing.Size(30, 13);
            this.lblField.TabIndex = 4;
            this.lblField.Text = "Field";
            // 
            // lblRenderers
            // 
            this.lblRenderers.AutoSize = true;
            this.lblRenderers.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenderers.Location = new System.Drawing.Point(6, 53);
            this.lblRenderers.Name = "lblRenderers";
            this.lblRenderers.Size = new System.Drawing.Size(51, 13);
            this.lblRenderers.TabIndex = 2;
            this.lblRenderers.Text = "Renderer";
            // 
            // DockableWindow
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "DockableWindow";
            this.Size = new System.Drawing.Size(402, 285);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLocate;
        private System.Windows.Forms.Label lblTitle;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox CboLayers;
        private System.Windows.Forms.ComboBox CboRenderers;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox CboFields;
        private System.Windows.Forms.Label lblField;
        private System.Windows.Forms.Label lblRenderers;
        private System.Windows.Forms.ComboBox CboColors;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Label lblClasses;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}
