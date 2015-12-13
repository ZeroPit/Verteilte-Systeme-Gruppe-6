namespace VerteilteSysteme
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.grbDisplayMandelbrot = new System.Windows.Forms.GroupBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnTest1 = new System.Windows.Forms.Button();
            this.grbSettings = new System.Windows.Forms.GroupBox();
            this.btnFindPosition = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.btnTest2 = new System.Windows.Forms.Button();
            this.lblOpenclTest = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnZoomUp = new System.Windows.Forms.Button();
            this.lblQuickZoom = new System.Windows.Forms.Label();
            this.btnZoomDown = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnStopZoom = new System.Windows.Forms.Button();
            this.btnStartZoom = new System.Windows.Forms.Button();
            this.nudZoom = new System.Windows.Forms.NumericUpDown();
            this.nudZoomSpeed = new System.Windows.Forms.NumericUpDown();
            this.lblZoomSpeed = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnJumpToPosition = new System.Windows.Forms.Button();
            this.lblZoom = new System.Windows.Forms.Label();
            this.lblPositionY = new System.Windows.Forms.Label();
            this.txtPositionY = new System.Windows.Forms.TextBox();
            this.lblPositionX = new System.Windows.Forms.Label();
            this.txtPositionX = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.plBack = new VerteilteSysteme.MyPannel();
            this.plFore = new VerteilteSysteme.MyPannel();
            this.grbDisplayMandelbrot.SuspendLayout();
            this.grbSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudZoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudZoomSpeed)).BeginInit();
            this.plBack.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbDisplayMandelbrot
            // 
            this.grbDisplayMandelbrot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbDisplayMandelbrot.Controls.Add(this.plBack);
            this.grbDisplayMandelbrot.Controls.Add(this.lblInfo);
            this.grbDisplayMandelbrot.Location = new System.Drawing.Point(12, 12);
            this.grbDisplayMandelbrot.Name = "grbDisplayMandelbrot";
            this.grbDisplayMandelbrot.Size = new System.Drawing.Size(599, 566);
            this.grbDisplayMandelbrot.TabIndex = 0;
            this.grbDisplayMandelbrot.TabStop = false;
            this.grbDisplayMandelbrot.Text = "Mandelbrot-Menge Anzeige";
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(6, 550);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(73, 13);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "Positions Info:";
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReset.Location = new System.Drawing.Point(6, 537);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(217, 23);
            this.btnReset.TabIndex = 23;
            this.btnReset.Text = "Zurücksetzen";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnTest1
            // 
            this.btnTest1.Location = new System.Drawing.Point(6, 404);
            this.btnTest1.Name = "btnTest1";
            this.btnTest1.Size = new System.Drawing.Size(105, 23);
            this.btnTest1.TabIndex = 19;
            this.btnTest1.Text = "Test 1";
            this.btnTest1.UseVisualStyleBackColor = true;
            this.btnTest1.Click += new System.EventHandler(this.btnTest1_Click);
            // 
            // grbSettings
            // 
            this.grbSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbSettings.Controls.Add(this.txtConsole);
            this.grbSettings.Controls.Add(this.btnFindPosition);
            this.grbSettings.Controls.Add(this.label10);
            this.grbSettings.Controls.Add(this.btnTest2);
            this.grbSettings.Controls.Add(this.lblOpenclTest);
            this.grbSettings.Controls.Add(this.label8);
            this.grbSettings.Controls.Add(this.btnZoomUp);
            this.grbSettings.Controls.Add(this.lblQuickZoom);
            this.grbSettings.Controls.Add(this.btnZoomDown);
            this.grbSettings.Controls.Add(this.label6);
            this.grbSettings.Controls.Add(this.btnStopZoom);
            this.grbSettings.Controls.Add(this.btnStartZoom);
            this.grbSettings.Controls.Add(this.nudZoom);
            this.grbSettings.Controls.Add(this.nudZoomSpeed);
            this.grbSettings.Controls.Add(this.lblZoomSpeed);
            this.grbSettings.Controls.Add(this.label4);
            this.grbSettings.Controls.Add(this.btnJumpToPosition);
            this.grbSettings.Controls.Add(this.lblZoom);
            this.grbSettings.Controls.Add(this.lblPositionY);
            this.grbSettings.Controls.Add(this.txtPositionY);
            this.grbSettings.Controls.Add(this.lblPositionX);
            this.grbSettings.Controls.Add(this.txtPositionX);
            this.grbSettings.Controls.Add(this.btnReset);
            this.grbSettings.Controls.Add(this.btnTest1);
            this.grbSettings.Location = new System.Drawing.Point(617, 12);
            this.grbSettings.Name = "grbSettings";
            this.grbSettings.Size = new System.Drawing.Size(229, 566);
            this.grbSettings.TabIndex = 1;
            this.grbSettings.TabStop = false;
            this.grbSettings.Text = "Einstellungen";
            // 
            // btnFindPosition
            // 
            this.btnFindPosition.FlatAppearance.BorderSize = 0;
            this.btnFindPosition.Image = global::VerteilteSysteme.Properties.Resources.Settings;
            this.btnFindPosition.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFindPosition.Location = new System.Drawing.Point(9, 103);
            this.btnFindPosition.Name = "btnFindPosition";
            this.btnFindPosition.Size = new System.Drawing.Size(214, 30);
            this.btnFindPosition.TabIndex = 4;
            this.btnFindPosition.Text = "X und Y Postion finden";
            this.btnFindPosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFindPosition.UseVisualStyleBackColor = true;
            this.btnFindPosition.Click += new System.EventHandler(this.btnFindPosition_Click);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 515);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(217, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "___________________________________";
            // 
            // btnTest2
            // 
            this.btnTest2.Location = new System.Drawing.Point(118, 404);
            this.btnTest2.Name = "btnTest2";
            this.btnTest2.Size = new System.Drawing.Size(105, 23);
            this.btnTest2.TabIndex = 20;
            this.btnTest2.Text = "Test 2";
            this.btnTest2.UseVisualStyleBackColor = true;
            this.btnTest2.Click += new System.EventHandler(this.btnTest2_Click);
            // 
            // lblOpenclTest
            // 
            this.lblOpenclTest.AutoSize = true;
            this.lblOpenclTest.Location = new System.Drawing.Point(6, 388);
            this.lblOpenclTest.Name = "lblOpenclTest";
            this.lblOpenclTest.Size = new System.Drawing.Size(133, 13);
            this.lblOpenclTest.TabIndex = 18;
            this.lblOpenclTest.Text = "OpenCL Test mittels CLoo:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 365);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(217, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "___________________________________";
            // 
            // btnZoomUp
            // 
            this.btnZoomUp.FlatAppearance.BorderSize = 0;
            this.btnZoomUp.Image = global::VerteilteSysteme.Properties.Resources.up;
            this.btnZoomUp.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnZoomUp.Location = new System.Drawing.Point(118, 332);
            this.btnZoomUp.Name = "btnZoomUp";
            this.btnZoomUp.Size = new System.Drawing.Size(105, 30);
            this.btnZoomUp.TabIndex = 16;
            this.btnZoomUp.Text = "Zoom up";
            this.btnZoomUp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnZoomUp.UseVisualStyleBackColor = true;
            this.btnZoomUp.Click += new System.EventHandler(this.btnZoomUp_Click);
            // 
            // lblQuickZoom
            // 
            this.lblQuickZoom.AutoSize = true;
            this.lblQuickZoom.Location = new System.Drawing.Point(6, 316);
            this.lblQuickZoom.Name = "lblQuickZoom";
            this.lblQuickZoom.Size = new System.Drawing.Size(117, 13);
            this.lblQuickZoom.TabIndex = 14;
            this.lblQuickZoom.Text = "Quick Zoom Änderung:";
            // 
            // btnZoomDown
            // 
            this.btnZoomDown.FlatAppearance.BorderSize = 0;
            this.btnZoomDown.Image = global::VerteilteSysteme.Properties.Resources.down;
            this.btnZoomDown.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnZoomDown.Location = new System.Drawing.Point(6, 332);
            this.btnZoomDown.Name = "btnZoomDown";
            this.btnZoomDown.Size = new System.Drawing.Size(105, 30);
            this.btnZoomDown.TabIndex = 15;
            this.btnZoomDown.Text = "Zoom down";
            this.btnZoomDown.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnZoomDown.UseVisualStyleBackColor = true;
            this.btnZoomDown.Click += new System.EventHandler(this.btnZoomDown_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 294);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(217, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "___________________________________";
            // 
            // btnStopZoom
            // 
            this.btnStopZoom.Location = new System.Drawing.Point(9, 268);
            this.btnStopZoom.Name = "btnStopZoom";
            this.btnStopZoom.Size = new System.Drawing.Size(214, 23);
            this.btnStopZoom.TabIndex = 12;
            this.btnStopZoom.Text = "Stopp Zoom";
            this.btnStopZoom.UseVisualStyleBackColor = true;
            this.btnStopZoom.Click += new System.EventHandler(this.btnStopZoom_Click);
            // 
            // btnStartZoom
            // 
            this.btnStartZoom.Location = new System.Drawing.Point(9, 239);
            this.btnStartZoom.Name = "btnStartZoom";
            this.btnStartZoom.Size = new System.Drawing.Size(214, 23);
            this.btnStartZoom.TabIndex = 11;
            this.btnStartZoom.Text = "Start Zoom";
            this.btnStartZoom.UseVisualStyleBackColor = true;
            this.btnStartZoom.Click += new System.EventHandler(this.btnStartZoom_Click);
            // 
            // nudZoom
            // 
            this.nudZoom.Location = new System.Drawing.Point(95, 139);
            this.nudZoom.Name = "nudZoom";
            this.nudZoom.Size = new System.Drawing.Size(128, 20);
            this.nudZoom.TabIndex = 6;
            this.nudZoom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudZoom.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // nudZoomSpeed
            // 
            this.nudZoomSpeed.Location = new System.Drawing.Point(95, 213);
            this.nudZoomSpeed.Name = "nudZoomSpeed";
            this.nudZoomSpeed.Size = new System.Drawing.Size(128, 20);
            this.nudZoomSpeed.TabIndex = 10;
            this.nudZoomSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblZoomSpeed
            // 
            this.lblZoomSpeed.AutoSize = true;
            this.lblZoomSpeed.Location = new System.Drawing.Point(6, 215);
            this.lblZoomSpeed.Name = "lblZoomSpeed";
            this.lblZoomSpeed.Size = new System.Drawing.Size(91, 13);
            this.lblZoomSpeed.TabIndex = 9;
            this.lblZoomSpeed.Text = "Speed des Zoom:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 190);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(217, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "___________________________________";
            // 
            // btnJumpToPosition
            // 
            this.btnJumpToPosition.Location = new System.Drawing.Point(9, 164);
            this.btnJumpToPosition.Name = "btnJumpToPosition";
            this.btnJumpToPosition.Size = new System.Drawing.Size(214, 23);
            this.btnJumpToPosition.TabIndex = 7;
            this.btnJumpToPosition.Text = "Zur Position springen";
            this.btnJumpToPosition.UseVisualStyleBackColor = true;
            this.btnJumpToPosition.Click += new System.EventHandler(this.btnJumpToPosition_Click);
            // 
            // lblZoom
            // 
            this.lblZoom.AutoSize = true;
            this.lblZoom.Location = new System.Drawing.Point(6, 141);
            this.lblZoom.Name = "lblZoom";
            this.lblZoom.Size = new System.Drawing.Size(37, 13);
            this.lblZoom.TabIndex = 5;
            this.lblZoom.Text = "Zoom:";
            // 
            // lblPositionY
            // 
            this.lblPositionY.AutoSize = true;
            this.lblPositionY.Location = new System.Drawing.Point(6, 61);
            this.lblPositionY.Name = "lblPositionY";
            this.lblPositionY.Size = new System.Drawing.Size(57, 13);
            this.lblPositionY.TabIndex = 2;
            this.lblPositionY.Text = "Y Position:";
            // 
            // txtPositionY
            // 
            this.txtPositionY.Location = new System.Drawing.Point(9, 77);
            this.txtPositionY.Name = "txtPositionY";
            this.txtPositionY.Size = new System.Drawing.Size(214, 20);
            this.txtPositionY.TabIndex = 3;
            // 
            // lblPositionX
            // 
            this.lblPositionX.AutoSize = true;
            this.lblPositionX.Location = new System.Drawing.Point(6, 22);
            this.lblPositionX.Name = "lblPositionX";
            this.lblPositionX.Size = new System.Drawing.Size(57, 13);
            this.lblPositionX.TabIndex = 0;
            this.lblPositionX.Text = "X Position:";
            // 
            // txtPositionX
            // 
            this.txtPositionX.Location = new System.Drawing.Point(9, 38);
            this.txtPositionX.Name = "txtPositionX";
            this.txtPositionX.Size = new System.Drawing.Size(214, 20);
            this.txtPositionX.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 581);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(400, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Powered by: Merlin, Nicklas und Peter für den Kurs Verteilte Systeme bei Prof. La" +
    "ng";
            // 
            // txtConsole
            // 
            this.txtConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConsole.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConsole.Location = new System.Drawing.Point(6, 433);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ReadOnly = true;
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConsole.Size = new System.Drawing.Size(214, 86);
            this.txtConsole.TabIndex = 24;
            // 
            // plBack
            // 
            this.plBack.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plBack.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.plBack.Controls.Add(this.plFore);
            this.plBack.Location = new System.Drawing.Point(9, 16);
            this.plBack.Margin = new System.Windows.Forms.Padding(0);
            this.plBack.Name = "plBack";
            this.plBack.Size = new System.Drawing.Size(587, 534);
            this.plBack.TabIndex = 5;
            // 
            // plFore
            // 
            this.plFore.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plFore.BackColor = System.Drawing.Color.Transparent;
            this.plFore.Location = new System.Drawing.Point(0, 0);
            this.plFore.Margin = new System.Windows.Forms.Padding(0);
            this.plFore.Name = "plFore";
            this.plFore.Size = new System.Drawing.Size(587, 534);
            this.plFore.TabIndex = 0;
            this.plFore.MouseDown += new System.Windows.Forms.MouseEventHandler(this.palOutput_MouseDown);
            this.plFore.MouseLeave += new System.EventHandler(this.palOutput_MouseLeave);
            this.plFore.MouseMove += new System.Windows.Forms.MouseEventHandler(this.palOutput_MouseMove);
            this.plFore.MouseUp += new System.Windows.Forms.MouseEventHandler(this.palOutput_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 596);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grbSettings);
            this.Controls.Add(this.grbDisplayMandelbrot);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Mandelbrot-Menge / Apfelmännchen Berechnung mittels OpenCl";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.grbDisplayMandelbrot.ResumeLayout(false);
            this.grbDisplayMandelbrot.PerformLayout();
            this.grbSettings.ResumeLayout(false);
            this.grbSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudZoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudZoomSpeed)).EndInit();
            this.plBack.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grbDisplayMandelbrot;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnTest1;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.GroupBox grbSettings;
        private System.Windows.Forms.Label lblZoom;
        private System.Windows.Forms.Label lblPositionY;
        private System.Windows.Forms.TextBox txtPositionY;
        private System.Windows.Forms.Label lblPositionX;
        private System.Windows.Forms.TextBox txtPositionX;
        private VerteilteSysteme.MyPannel plFore;
        private VerteilteSysteme.MyPannel plBack;
        private System.Windows.Forms.Button btnFindPosition;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnTest2;
        private System.Windows.Forms.Label lblOpenclTest;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnZoomUp;
        private System.Windows.Forms.Label lblQuickZoom;
        private System.Windows.Forms.Button btnZoomDown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnStopZoom;
        private System.Windows.Forms.Button btnStartZoom;
        private System.Windows.Forms.NumericUpDown nudZoom;
        private System.Windows.Forms.NumericUpDown nudZoomSpeed;
        private System.Windows.Forms.Label lblZoomSpeed;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnJumpToPosition;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtConsole;
    }
}

