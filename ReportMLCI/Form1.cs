using Microsoft.Data.SqlClient;
using Microsoft.Reporting.WinForms;
using static ReportMLCI.Helper.HandleNull;
using static ReportMLCI.Form1;

namespace ReportMLCI
{
    public partial class Form1 : Form
    {
        private List<DataDebitur> _dataDebitur;
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
        }

        private void ComboBox1_SelectedItemChanged(object sender, EventArgs e)
        {
            LoadReport(comboBox1.SelectedItem.ToString());
        }

        private void LoadReport(string reportName)
        {
            string fileName = "";

            switch (reportName)
            {
                case "Report 1":
                    fileName = "Report1.rdlc";
                    break;
                case "Report Lampiran 1":
                    fileName = "Report_Lampiran1.rdlc";
                    break;
                case "Report Lampiran 2":
                    fileName = "Report_Lampiran2.rdlc";
                    break;
                case "Report Clause MLCI":
                    fileName = "Report_ClauseMLCI.rdlc";
                    break;
            }

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

            if (reportName == "Report Lampiran 2")
            {
                // HEADER (Single Row)
                var dataLampiran2 = new List<DataLampiran2>
                {
                    new DataLampiran2
                    {
                        Np = "1512345678",
                        ItemBrand = "Toyota Kijang Innova G/AT Minibus",
                        YearCondition = new DateTime(2015, 1, 1),
                        NamaKreditur = "PT MITSUI LEASING CAPITAL INDONESIA",
                        AlamatKreditur = "JL. GATOT SUBROTO KAV. 42, JAKARTA SELATAN",
                        NorekKreditur = "09928737882",
                        NameDebitur = "Septian Dwi Rahmanza",
                        City = "Jakarta Pusat",

                        InterestFlatRate = "5%",
                        InterestEffectiveRate = "6%",
                        InstallmentPaymentMethod = "Cicilan",
                        Currency = "IDR",
                        InstallmentPaymentTiming = "Bulanan",
                        DueDateDescription = "Setiap tanggal 10",

                        InsuranceCompanyName = "PT. ASURANSI Raksa",
                        PolicyDuration = "12 Bulan",
                        PremiumPaymentMethod = "Cicilan Bulanan",
                        CoverageDetail = "All Risk",

                        PaymentDate = new DateTime(2023, 1, 4),

                        // Table 1
                        ItemPrice = 300000000,
                        DownpaymentAmount = 50000000,
                        MonthlyInstallmentAmount = 7987000,
                        PaymentDurationMonths = 36,
                        TotalPayableAmount = 287500000,

                        // Table 2
                        InsurancePremiumAmount = 8000000,
                        SurveyAdminFeeAmount = 2000000,
                        ProvincialFeeAmount = 3000000,
                        NotaryFeeAmount = 150000,
                        FiduciaryRegistrationFeeAmount = 350000,
                        OtherFeeAmount = 0,
                        TotalFeeAmount = 13500000
                    }
                };


                // TABLE 3 (MULTI ROW)
                var coverageList = new List<CoverageDetail>
                {
                    new CoverageDetail { Year = 1, CoverageAmount = 300000000, ThirdPartyLiabilityAmount = 10000000, CoverageType = "CP" },
                    new CoverageDetail { Year = 2, CoverageAmount = 255000000, ThirdPartyLiabilityAmount = 10000000, CoverageType = "CP" },
                    new CoverageDetail { Year = 3, CoverageAmount = 225000000, ThirdPartyLiabilityAmount = 10000000, CoverageType = "CP" }
                };

                reportViewer1.LocalReport.DataSources.Clear();

                // Header
                reportViewer1.LocalReport.DataSources.Add(
                    new ReportDataSource("DataSet1", dataLampiran2)
                );

                // Table 3
                reportViewer1.LocalReport.DataSources.Add(
                    new ReportDataSource("DataSet2", coverageList)
                );

                _dataDebitur = new List<DataDebitur>
                {
                    new DataDebitur { Name = "Septian Dwi Rahmanza" }
                };
            }
            else if (reportName == "Report Clause MLCI")
            {
                var clauseData = GetClauseData();

                reportViewer1.LocalReport.DataSources.Clear();

                reportViewer1.LocalReport.DataSources.Add(
                    new ReportDataSource("DataSetClause", clauseData)
                );
                reportViewer1.LocalReport.DataSources.Add(
                   new ReportDataSource("DataSetClauseDetail", clauseData)
               );
            }
            else
            {
                // Dummy Report 1
                var data = new List<Produk>
                {
                    new Produk { ID = 1, Nama = "Produk A", Jumlah = 10 },
                    new Produk { ID = 2, Nama = "Produk B", Jumlah = 20 }
                };

                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(
                    new ReportDataSource("DataSet1", data)
                );
            }

            reportViewer1.RefreshReport();
        }
        private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            if (e.ReportPath.Contains("SubReport_TtdDebitur"))
            {
                e.DataSources.Add(
                    new ReportDataSource("DataDebitur", _dataDebitur)
                );
            }
        }
        private List<ClauseData> GetClauseData()
        {
            var result = new List<ClauseData>();

            string connectionString ="Server=(localdb)\\MSSQLLocalDB;Database=ReportMLCI;Trusted_Connection=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
                                   SELECT 
                                    mhc.ClauseHeaderCode,
                                    mhc.ClauseHeaderTitle,
                                    mhc.ClauseHeaderDescription,
                                    mhc.IsActive AS IsActiveHeader,

                                    mc.Id,
                                    mc.ClauseCode,
                                    mc.ClauseTitle,
                                    mc.ClauseContent,
                                    mc.IsActive AS IsActiveClause,

                                    mcd.ClauseSubCode,
                                    mcd.ClauseSubTitle,
                                    mcd.ClauseSubContent,
                                    mcd.MasterClauseId,
                                    mcd.IsActive AS IsActiveClauseSub,

                                    mcsd.SubDetailCode,
                                    mcsd.SubDetailTitle,
                                    mcsd.SubDetailContent,
                                    mcsd.MasterClauseDetailId,
                                    mcsd.IsActive AS IsActiveSubDetail

                                FROM MasterClauses mc
                                INNER JOIN MasterHeaderClauses mhc on mhc.Id = mc.MasterHeaderClauseId
                                LEFT JOIN MasterClausesDetails mcd ON mc.Id = mcd.MasterClauseId
                                LEFT JOIN MasterClausesSubDetails mcsd ON mcsd.MasterClauseDetailId = mcd.Id
                                ORDER BY 
                                    TRY_CAST(REPLACE(mc.ClauseCode, 'PASAL ', '') AS INT),

                                    COALESCE(TRY_CAST(PARSENAME(mcd.ClauseSubCode, 2) AS INT), 999999) * 1000 +
                                    COALESCE(TRY_CAST(PARSENAME(mcd.ClauseSubCode, 1) AS INT), 999999),

                                    CASE 
                                        WHEN mcsd.SubDetailCode = '(i)' THEN 1
                                        WHEN mcsd.SubDetailCode = '(ii)' THEN 2
                                        WHEN mcsd.SubDetailCode = '(iii)' THEN 3
                                        WHEN mcsd.SubDetailCode = '(iv)' THEN 4
                                        WHEN mcsd.SubDetailCode = '(v)' THEN 5
                                        WHEN mcsd.SubDetailCode = '(vi)' THEN 6
                                        WHEN mcsd.SubDetailCode = '(vii)' THEN 7
                                        WHEN mcsd.SubDetailCode = '(viii)' THEN 8
                                        WHEN mcsd.SubDetailCode = '(ix)' THEN 9
                                        WHEN mcsd.SubDetailCode = '(x)' THEN 10

                                        WHEN mcsd.SubDetailCode LIKE '[a-z])'
                                            THEN ASCII(LEFT(mcsd.SubDetailCode,1)) + 100

                                        WHEN mcsd.SubDetailCode LIKE '[a-z].'
                                            THEN ASCII(LEFT(mcsd.SubDetailCode,1)) + 200

                                        ELSE 999999
                                    END
                            ";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Handle nullable MasterClauseId
                        Guid? masterClauseId = null;
                        if (!reader.IsDBNull(reader.GetOrdinal("MasterClauseId")))
                        {
                            masterClauseId = Guid.Parse(reader["MasterClauseId"].ToString());
                        }

                        result.Add(new ClauseData
                        {

                            ClauseCode = reader["ClauseCode"].ToString(),
                            ClauseTitle = reader["ClauseTitle"].ToString(),
                            ClauseContent = reader["ClauseContent"].ToString(),
                            IsActiveClause = nlBool(reader, "IsActiveClause"),
                            ClauseSubCode = nlString(reader, "ClauseSubCode"),
                            ClauseSubTitle = nlString(reader, "ClauseSubTitle"),
                            ClauseSubContent = nlString(reader, "ClauseSubContent"),
                            IsActiveClauseSub = nlBool(reader, "IsActiveClauseSub"),
                            SubDetailCode = nlString(reader, "SubDetailCode"),
                            SubDetailTitle = nlString(reader, "SubDetailTitle"),
                            SubDetailContent = nlString(reader, "SubDetailContent"),
                            MasterClauseDetailId = nlString(reader, "MasterClauseDetailId"),
                            IsActiveSubDetail = nlBool(reader, "IsActiveSubDetail"),
                            ClauseHeaderCode = reader["ClauseHeaderCode"].ToString(),
                            ClauseHeaderTitle = reader["ClauseHeaderTitle"].ToString(),
                            ClauseHeaderDescription = reader["ClauseHeaderDescription"].ToString(),
                            IsActiveHeader = Convert.ToBoolean(reader["IsActiveHeader"]),
                            MasterClauseId = masterClauseId
                        });
                    }
                }
            }

            return result;
        }
        public class Produk
        {
            public int ID { get; set; }
            public string Nama { get; set; }
            public int Jumlah { get; set; }
        }
        public class DataLampiran2
        {
            // Table 1
            public decimal ItemPrice { get; set; }
            public decimal DownpaymentAmount { get; set; }
            public decimal MonthlyInstallmentAmount { get; set; }
            public decimal PaymentDurationMonths { get; set; }
            public decimal TotalPayableAmount { get; set; }

            // Table 2
            public decimal InsurancePremiumAmount { get; set; }
            public decimal SurveyAdminFeeAmount { get; set; }
            public decimal ProvincialFeeAmount { get; set; }
            public decimal NotaryFeeAmount { get; set; }
            public decimal FiduciaryRegistrationFeeAmount { get; set; }
            public decimal OtherFeeAmount { get; set; }
            public decimal TotalFeeAmount { get; set; }
                
            // Info
            public string Np { get; set; }
            public string ItemBrand { get; set; }
            public DateTime YearCondition { get; set; }
            public string NamaKreditur { get; set; }
            public string AlamatKreditur { get; set; }
            public string NorekKreditur { get; set; }
            public string NameDebitur { get; set; }
            public string City { get; set; }

            public string InterestFlatRate { get; set; }
            public string InterestEffectiveRate { get; set; }
            public string InstallmentPaymentMethod { get; set; }
            public string Currency { get; set; }
            public string InstallmentPaymentTiming { get; set; }
            public string DueDateDescription { get; set; }

            public string InsuranceCompanyName { get; set; }
            public string PolicyDuration { get; set; }
            public string PremiumPaymentMethod { get; set; }
            public string CoverageDetail { get; set; }

            public DateTime PaymentDate { get; set; }
        }
        public class CoverageDetail
        {
            public int Year { get; set; }
            public decimal CoverageAmount { get; set; }
            public decimal ThirdPartyLiabilityAmount { get; set; }
            public string CoverageType { get; set; }
        }

        public class DataDebitur
        {
            public string Name { get; set; }
        }

        public class ClauseData
        {
            public string ClauseHeaderCode { get; set; }
            public string ClauseHeaderTitle { get; set; }
            public string ClauseHeaderDescription { get; set; }
            public bool IsActiveHeader { get; set; }

            public string ClauseCode { get; set; }
            public string ClauseTitle { get; set; }
            public string ClauseContent { get; set; }
            public bool IsActiveClause { get; set; }

            public string ClauseSubCode { get; set; }
            public string ClauseSubTitle { get; set; }
            public string ClauseSubContent { get; set; }
            public Guid? MasterClauseId { get; set; }
            public bool IsActiveClauseSub { get; set; }

            public string SubDetailCode { get; set; }
            public string SubDetailTitle { get; set; }
            public string SubDetailContent { get; set; }
            public string MasterClauseDetailId { get; set; }
            public bool IsActiveSubDetail { get; set; }

        }

    }

}
