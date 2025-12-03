namespace ProiectSlotMachine
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private OpenTK.GLControl GlControl1;
        private System.Windows.Forms.Button btnTrageSlot;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.NumericUpDown numericCicluriSlot;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.btnTrageSlot = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.GlControl1 = new OpenTK.GLControl();
            this.numericCicluriSlot = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericCicluriSlot)).BeginInit();
            this.SuspendLayout();

            // Setarea dimensiunii formularului pentru a face loc controalelor
            this.ClientSize = new System.Drawing.Size(900, 580);
            this.Text = "Slot Machine (OpenGL)";

            // -------------------------------------------------------------
            // 1. GlControl1 (Zona de Desenare OpenGL)
            // Ocupă majoritatea spațiului de sus
            // -------------------------------------------------------------
            this.GlControl1.BackColor = System.Drawing.Color.DimGray;
            this.GlControl1.Location = new System.Drawing.Point(10, 10);
            this.GlControl1.Name = "GlControl1";
            this.GlControl1.Size = new System.Drawing.Size(880, 480);
            this.GlControl1.TabIndex = 0;
            this.GlControl1.VSync = true;

            // -------------------------------------------------------------
            // 2. btnTrageSlot (Buton)
            // Poziționat sub GlControl1, stânga
            // -------------------------------------------------------------
            this.btnTrageSlot.BackColor = System.Drawing.Color.Green;
            this.btnTrageSlot.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrageSlot.ForeColor = System.Drawing.Color.White;
            this.btnTrageSlot.Location = new System.Drawing.Point(30, 510);
            this.btnTrageSlot.Name = "btnTrageSlot";
            this.btnTrageSlot.Size = new System.Drawing.Size(150, 50);
            this.btnTrageSlot.TabIndex = 2;
            this.btnTrageSlot.Text = "Trage Slotul!";
            this.btnTrageSlot.UseVisualStyleBackColor = false;

            // -------------------------------------------------------------
            // 3. numericCicluriSlot (Selector Cicluri)
            // Poziționat lângă butonul de tragere
            // -------------------------------------------------------------
            this.numericCicluriSlot.Location = new System.Drawing.Point(190, 525);
            this.numericCicluriSlot.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            this.numericCicluriSlot.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            this.numericCicluriSlot.Value = new decimal(new int[] { 50, 0, 0, 0 });
            this.numericCicluriSlot.Name = "numericCicluriSlot";
            this.numericCicluriSlot.Size = new System.Drawing.Size(80, 27);
            this.numericCicluriSlot.TabIndex = 4;
            this.numericCicluriSlot.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            // -------------------------------------------------------------
            // 4. labelStatus (Status Câștig)
            // Poziționat în dreapta, mare și centrat
            // -------------------------------------------------------------
            this.labelStatus.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.ForeColor = System.Drawing.Color.DarkRed; // O culoare puternică pentru text
            this.labelStatus.Location = new System.Drawing.Point(300, 498);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(580, 70);
            this.labelStatus.TabIndex = 3;
            this.labelStatus.Text = "Așteptare...";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // Adăugarea controalelor la formular
            this.Controls.Add(this.numericCicluriSlot);
            this.Controls.Add(this.btnTrageSlot);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.GlControl1);

            ((System.ComponentModel.ISupportInitialize)(this.numericCicluriSlot)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}