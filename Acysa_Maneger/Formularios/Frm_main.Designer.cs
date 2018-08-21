namespace Acysa_Maneger
{
    partial class Frm_main
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_main));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.herramientasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.albaranesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.presupuestoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parteDeHorasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.herramientasToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(989, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // herramientasToolStripMenuItem
            // 
            this.herramientasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.albaranesToolStripMenuItem,
            this.presupuestoToolStripMenuItem,
            this.parteDeHorasToolStripMenuItem});
            this.herramientasToolStripMenuItem.Name = "herramientasToolStripMenuItem";
            this.herramientasToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.herramientasToolStripMenuItem.Text = "Herramientas";
            // 
            // albaranesToolStripMenuItem
            // 
            this.albaranesToolStripMenuItem.Name = "albaranesToolStripMenuItem";
            this.albaranesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.albaranesToolStripMenuItem.Text = "Albaranes";
            this.albaranesToolStripMenuItem.Click += new System.EventHandler(this.albaranesToolStripMenuItem_Click);
            // 
            // presupuestoToolStripMenuItem
            // 
            this.presupuestoToolStripMenuItem.Name = "presupuestoToolStripMenuItem";
            this.presupuestoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.presupuestoToolStripMenuItem.Text = "Presupuesto";
            this.presupuestoToolStripMenuItem.Click += new System.EventHandler(this.presupuestoToolStripMenuItem_Click);
            // 
            // parteDeHorasToolStripMenuItem
            // 
            this.parteDeHorasToolStripMenuItem.Name = "parteDeHorasToolStripMenuItem";
            this.parteDeHorasToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.parteDeHorasToolStripMenuItem.Text = "Parte de horas";
            this.parteDeHorasToolStripMenuItem.Click += new System.EventHandler(this.parteDeHorasToolStripMenuItem_Click);
            // 
            // Frm_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 550);
            this.Controls.Add(this.menuStrip1);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Frm_main";
            this.Text = "Acysa Maneger";
            this.TransparencyKey = System.Drawing.Color.Yellow;
            this.Load += new System.EventHandler(this.Frm_main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem herramientasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem albaranesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem presupuestoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parteDeHorasToolStripMenuItem;
    }
}

