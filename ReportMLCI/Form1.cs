using Microsoft.Reporting.WinForms;
using System.Drawing.Printing;
using ReportMLCI.Helper;
using ReportMLCI.Models;
using ReportMLCI.Services;

namespace ReportMLCI
{
    public partial class Form1 : Form
    {
        #region Fields & Properties
        private List<DataDebitur> _dataDebitur = new List<DataDebitur>();
        #endregion

        #region Form Constructor & Initialization
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedItemChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Report 1");
            comboBox1.Items.Add("Report Lampiran 1");
            comboBox1.Items.Add("Report Lampiran 2");
            comboBox1.Items.Add("Report Clause MLCI");
            comboBox1.Items.Add("Report List CAM Approve Date");
            comboBox1.Items.Add("Report List Rejected CAM");
            comboBox1.Items.Add("Report History Payment");
            comboBox1.Items.Add("Report Early Termination");
            comboBox1.Items.Add("Report Monthly Summary Approved By CMO");
            comboBox1.Items.Add("Report History of BPKB Submission by Dealer/ Showroom");
            comboBox1.Items.Add("Report Summary CAM MOU Saran MCA");
            comboBox1.Items.Add("Report List Approved Cam By Cars Condition");
            comboBox1.Items.Add("Report Customer Overdue BPKB");
            comboBox1.Items.Add("Report List of Sales");
            comboBox1.Items.Add("Report List of Supplier");
            comboBox1.Items.Add("Report List of Model");
            comboBox1.Items.Add("Report List of Insurance Premi");
            comboBox1.Items.Add("Report List of Insurance TPL");
            comboBox1.Items.Add("Report High Risk Customer APU PPT");
            comboBox1.Items.Add("Report Blacklist");
            comboBox1.Items.Add("Report Detail SID Checking");
            comboBox1.Items.Add("CF_FD_PO_TC");
        }
        #endregion

        #region Event Handlers & Control Listeners
        private void ComboBox1_SelectedItemChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                LoadReport(comboBox1.SelectedItem.ToString()!);
            }
        }

        private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            if (e.ReportPath.Contains("SubReport_TtdDebitur"))
            {
                e.DataSources.Add(new ReportDataSource("DataDebitur", _dataDebitur));
            }
        }
        #endregion

        #region Report Loading & Orientation Handling
        private void LoadReport(string reportName)
        {
            string fileName = ReportPathHelper.GetReportFileName(reportName);
            string reportPath = Path.Combine(Application.StartupPath, "Reports", fileName);

            if (!File.Exists(reportPath))
            {
                MessageBox.Show("File tidak ditemukan:\n" + reportPath);
                return;
            }

            reportViewer1.LocalReport.ReportPath = reportPath;
            reportViewer1.LocalReport.DataSources.Clear();

            reportViewer1.LocalReport.SubreportProcessing -= LocalReport_SubreportProcessing;
            reportViewer1.LocalReport.SubreportProcessing += LocalReport_SubreportProcessing;

            // Load DataSources & Parameters from ReportDummyDataService
            var reportData = ReportDummyDataService.GetReportData(reportName);
            _dataDebitur = reportData.DataDebitur;

            foreach (var ds in reportData.DataSources)
            {
                reportViewer1.LocalReport.DataSources.Add(ds);
            }

            if (reportData.Parameters.Count > 0)
            {
                reportViewer1.LocalReport.SetParameters(reportData.Parameters);
            }

            // Automatic Page Orientation (Landscape vs Portrait) & Print Layout settings
            var defaultSettings = reportViewer1.LocalReport.GetDefaultPageSettings();
            var pageSettings = new PageSettings
            {
                Landscape = defaultSettings.IsLandscape,
                Margins = defaultSettings.Margins,
                PaperSize = defaultSettings.PaperSize
            };
            reportViewer1.SetPageSettings(pageSettings);
            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.PageWidth;
            reportViewer1.RefreshReport();
        }
        #endregion
    }
}
