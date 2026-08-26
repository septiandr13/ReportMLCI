namespace ReportMLCI
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            comboBox1 = new ComboBox();
            panelTop = new Panel();
            label1 = new Label();
            panelTop.SuspendLayout();
            SuspendLayout();
            // 
            // reportViewer1
            // 
            reportViewer1.Dock = DockStyle.Fill;
            reportViewer1.Location = new Point(0, 60);
            reportViewer1.Name = "reportViewer1";
            reportViewer1.ServerReport.BearerToken = null;
            reportViewer1.Size = new Size(1511, 840);
            reportViewer1.TabIndex = 0;
            reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // comboBox1
            // 
            comboBox1.Dock = DockStyle.Fill;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Location = new Point(130, 10);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(1071, 33);
            comboBox1.TabIndex = 0;
            // 
            // panelTop
            // 
            panelTop.Controls.Add(comboBox1);
            panelTop.Controls.Add(label1);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Padding = new Padding(10);
            panelTop.Size = new Size(1211, 60);
            panelTop.TabIndex = 1;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Left;
            label1.Location = new Point(10, 10);
            label1.Name = "label1";
            label1.Size = new Size(120, 40);
            label1.TabIndex = 1;
            label1.Text = "Pilih Report:";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Form1
            // 
            AccessibleName = "";
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1511, 900);
            Controls.Add(reportViewer1);
            Controls.Add(panelTop);
            ForeColor = SystemColors.ActiveCaptionText;
            Name = "Form1";
            Text = "MLCI Report";
            CenterToScreen();
            using (var bitmap = new Bitmap("Properties/icon_report.png"))
            {
                Icon = Icon.FromHandle(bitmap.GetHicon());
            }
            panelTop.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private ComboBox comboBox1;
        private Panel panelTop;
        private Label label1;
    }
}