namespace ExtraMaterialControl
{
    partial class ExtraMaterialCtrl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.radButtonExtraMaterial = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonExtraMaterial)).BeginInit();
            this.SuspendLayout();
            // 
            // radButtonExtraMaterial
            // 
            this.radButtonExtraMaterial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.radButtonExtraMaterial.Location = new System.Drawing.Point(0, 0);
            this.radButtonExtraMaterial.Name = "radButtonExtraMaterial";
            this.radButtonExtraMaterial.Size = new System.Drawing.Size(100, 31);
            this.radButtonExtraMaterial.TabIndex = 1;
            this.radButtonExtraMaterial.Text = "חומר נוסף";
            this.radButtonExtraMaterial.Click += new System.EventHandler(this.radButtonExtraMaterial_Click);
            // 
            // ExtraMaterialCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radButtonExtraMaterial);
            this.Name = "ExtraMaterialCtrl";
            this.Size = new System.Drawing.Size(100, 31);
            ((System.ComponentModel.ISupportInitialize)(this.radButtonExtraMaterial)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadButton radButtonExtraMaterial;


    }
}
