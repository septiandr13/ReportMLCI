using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Reporting.WinForms;
using ReportMLCI.Models;
using static ReportMLCI.Helper.HandleNull;

namespace ReportMLCI.Services
{
    public class ReportDataResult
    {
        public List<ReportDataSource> DataSources { get; set; } = new List<ReportDataSource>();
        public List<ReportParameter> Parameters { get; set; } = new List<ReportParameter>();
        public List<DataDebitur> DataDebitur { get; set; } = new List<DataDebitur>();
    }

    public static class ReportDummyDataService
    {
        public static ReportDataResult GetReportData(string reportName)
        {
            var result = new ReportDataResult();

            if (reportName == "Report Lampiran 2")
            {
                var dataLampiran2 = new List<DataLampiran2>
                {
                    new DataLampiran2
                    {
                        Np = "1512345678", ItemBrand = "Toyota Kijang Innova G/AT Minibus", YearCondition = new DateTime(2015, 1, 1),
                        NamaKreditur = "PT MITSUI LEASING CAPITAL INDONESIA", AlamatKreditur = "JL. GATOT SUBROTO KAV. 42, JAKARTA SELATAN",
                        NorekKreditur = "09928737882", NameDebitur = "Septian Dwi Rahmanza", City = "Jakarta Pusat",
                        InterestFlatRate = "5%", InterestEffectiveRate = "6%", InstallmentPaymentMethod = "Cicilan",
                        Currency = "IDR", InstallmentPaymentTiming = "Bulanan", DueDateDescription = "Setiap tanggal 10",
                        InsuranceCompanyName = "PT. ASURANSI Raksa", PolicyDuration = "12 Bulan", PremiumPaymentMethod = "Cicilan Bulanan",
                        CoverageDetail = "All Risk", PaymentDate = new DateTime(2023, 1, 4),
                        ItemPrice = 300000000, DownpaymentAmount = 50000000, MonthlyInstallmentAmount = 7987000,
                        PaymentDurationMonths = 36, TotalPayableAmount = 287500000, InsurancePremiumAmount = 8000000,
                        SurveyAdminFeeAmount = 2000000, ProvincialFeeAmount = 3000000, NotaryFeeAmount = 150000,
                        FiduciaryRegistrationFeeAmount = 350000, OtherFeeAmount = 0, TotalFeeAmount = 13500000
                    }
                };

                var coverageList = new List<CoverageDetail>
                {
                    new CoverageDetail { Year = 1, CoverageAmount = 300000000, ThirdPartyLiabilityAmount = 10000000, CoverageType = "CP" },
                    new CoverageDetail { Year = 2, CoverageAmount = 255000000, ThirdPartyLiabilityAmount = 10000000, CoverageType = "CP" },
                    new CoverageDetail { Year = 3, CoverageAmount = 225000000, ThirdPartyLiabilityAmount = 10000000, CoverageType = "CP" }
                };

                result.DataSources.Add(new ReportDataSource("DataSet1", dataLampiran2));
                result.DataSources.Add(new ReportDataSource("DataSet2", coverageList));
                result.DataDebitur = new List<DataDebitur> { new DataDebitur { Name = "Septian Dwi Rahmanza" } };
            }
            else if (reportName == "Report Clause MLCI")
            {
                var clauseData = GetClauseData();
                result.DataSources.Add(new ReportDataSource("DataSetClause", clauseData));
                result.DataSources.Add(new ReportDataSource("DataSetClauseDetail", clauseData));
            }
            else if (reportName == "Report Monthly Summary Approved By CMO")
            {
                var monthlySummaryData = new List<CMOProductivityData>
                {
                    new CMOProductivityData { BranchName = "Kelapa Gading", No = "1", CMOName = "Cavell Sika Budiman", Total = "0", AvgDay = "0" },
                    new CMOProductivityData { BranchName = "Kelapa Gading", No = "2", CMOName = "Gunawan Rusandi", Total = "0", AvgDay = "0" }
                };
                result.DataSources.Add(new ReportDataSource("DataSet1", monthlySummaryData));
            }
            else if (reportName == "Report History of BPKB Submission by Dealer/ Showroom")
            {
                var bpkbData = new List<BPKBSubmissionData>
                {
                    new BPKBSubmissionData { No = "1", DealerShowroomName = "AUTOCAR JAYA", CustomerName = "JOHN DOE", ContractNo = "2023000001", Branch = "Kelapa Gading", DisbursementDate = "01/08/2023", ReceivedDateCA = "05/08/2023", Status = "RECEIVED", Remark = "-" },
                    new BPKBSubmissionData { No = "2", DealerShowroomName = "AUTOCAR JAYA", CustomerName = "JANE SMITH", ContractNo = "2023000002", Branch = "Kelapa Gading", DisbursementDate = "02/08/2023", ReceivedDateCA = "06/08/2023", Status = "PENDING", Remark = "Follow up required" }
                };
                result.DataSources.Add(new ReportDataSource("DataSet1", bpkbData));
            }
            else if (reportName == "Report Summary CAM MOU Saran MCA")
            {
                var headerData = new List<SummaryCAMMOUHeaderData> { new SummaryCAMMOUHeaderData { CustomerName = "PT ABCD", Address = "Jl fghji", EstablishedDate = "01-Jan-2015", NPWPID = "09213712312738999", ContactNumber = "021-555555 / +62 888 888 888", Business = "Mining Contractor/ Mining Concession Owner", CustomerStatus = "New", CustomerGrading = "1A", CustomerRating = "1", CustomerAssignedCategory = "Growth", Watchlist = "Not Listed", FinanceScheme = "Installment Financing", Branch = "Kelapa Gading", AuthorizedCapital = "IDR 10.000.000.000", PaidInCapital = "IDR 2.500.000.000" } };
                var shareholderData = new List<ShareholderData> { new ShareholderData { No = "1", NameInIDCard = "JOHN DOE", Status = "Individual", Position = "President Director", ShareholderPercentage = "60%", TotalShares = "600", NominalShares = "600.000.000", BO = "Yes", Signer = "Yes" }, new ShareholderData { No = "2", NameInIDCard = "JANE DOE", Status = "Individual", Position = "Commissioner", ShareholderPercentage = "40%", TotalShares = "400", NominalShares = "400.000.000", BO = "No", Signer = "No" } };
                var deedData = new List<DeedData> { new DeedData { No = "1", StateGazetteNo = "AHU-12345", DeedNo = "10", DateOfDeed = "15/01/2015", NotaryName = "Budi Santoso, SH", CertificateMinistry = "AHU-00123.AH.01.01.2015" } };
                var signerData = new List<SignerData> { new SignerData { Name = "JOHN DOE", Position = "President Director", EndTermOffice = "15/01/2028" } };
                var mgmtData = new List<ManagementDetailData> { new ManagementDetailData { No = "1", NameInIDCard = "JOHN DOE", IDCard = "3171010101010001", AddressInID = "Jl. Sudirman No. 1, Jakarta", City = "Jakarta Selatan", Image = "View" } };
                var outstandingData = new List<OutstandingObligorData> { new OutstandingObligorData { Category = "Active contract (disbursed)", Unit = "5", Amount = "10.000.000.000" }, new OutstandingObligorData { Category = "Undisbursed", Unit = "2", Amount = "5.000.000.000" }, new OutstandingObligorData { Category = "Propose (unapproved)", Unit = "3", Amount = "10.000.000.000" }, new OutstandingObligorData { Category = "Total", Unit = "10", Amount = "25.000.000.000" } };
                var eddData = new List<EDDQuestionnaireData> { new EDDQuestionnaireData { No = "1", Questionnaire = "Has the BM / Department Head Business Unit ensured that the prospective customer is not involved in money laundering activities?", YesNo = "Yes" }, new EDDQuestionnaireData { No = "2", Questionnaire = "Has the BM / Department Head Business Unit confirmed that the source of income originates from legal sources?", YesNo = "Yes" }, new EDDQuestionnaireData { No = "3", Questionnaire = "Has the BM / Department Head Business Unit confirmed that the prospective customer's business is not related to illegal activities?", YesNo = "Yes" }, new EDDQuestionnaireData { No = "4", Questionnaire = "Has the BM / Department Head Business Unit confirmed that all documents are in accordance with actual condition?", YesNo = "Yes" }, new EDDQuestionnaireData { No = "5", Questionnaire = "Has the BM / Department Head Business Unit conducted checking and ensured no negative news?", YesNo = "Yes" } };
                var autoDevData = new List<DeviationData> { new DeviationData { No = "1", DeviationText = "LTV exceeds 75% standard limit" } };
                var manualDevData = new List<DeviationData> { new DeviationData { No = "1", DeviationText = "Approval from Risk Committee required for Grace Period > 1 month" } };
                var finData = new List<FinancialData> { new FinancialData { LineItem = "TOTAL ASSET", Year1 = "150.000.000.000", Year2 = "175.000.000.000" }, new FinancialData { LineItem = "TOTAL LIABILITIES", Year1 = "90.000.000.000", Year2 = "100.000.000.000" }, new FinancialData { LineItem = "TOTAL EQUITY", Year1 = "60.000.000.000", Year2 = "75.000.000.000" }, new FinancialData { LineItem = "Net Profit / (Loss) for the Year", Year1 = "12.000.000.000", Year2 = "15.000.000.000" } };
                var finProjData = new List<FinancialProjectionData> { new FinancialProjectionData { LineItem = "Net Profit / (Loss) Projection", Projection = "18.000.000.000" } };
                var bankSummaryData = new List<BankSummaryData> { new BankSummaryData { MonthYear = "Jan-2025", BeginningBalance = "5.000.000.000", Debet = "20.000.000.000", Credit = "18.000.000.000", EndingBalance = "7.000.000.000", Notes = "Normal" } };
                var tcDocData = new List<TermConditionDocData> { new TermConditionDocData { No = "1", DocumentName = "KTP Customer", PriorTo = "TC", Check = "Yes", Waived = "No", PromiseDate = "-", ExpiredDate = "-", Notes = "Valid", View = "View" } };
                var loanRatioData = new List<LoanRatioData> { new LoanRatioData { Category = "PROPOSE", No = "1", ApplicationNo = "23182604001", BrandType = "Hino / 130 HDL 6.4 PS", UnitYear = "2026", Financing = "IF", Branch = "KG", InstalmentMonth = "8.750.000", MarketPrice = "1.000.000.000", Outstanding = "800.000.000", LoanRatioPercent = "80%" } };

                result.DataSources.Add(new ReportDataSource("DataSet1", headerData));
                result.DataSources.Add(new ReportDataSource("DataSetShareholder", shareholderData));
                result.DataSources.Add(new ReportDataSource("DataSetDeed", deedData));
                result.DataSources.Add(new ReportDataSource("DataSetSigner", signerData));
                result.DataSources.Add(new ReportDataSource("DataSetManagementDetail", mgmtData));
                result.DataSources.Add(new ReportDataSource("DataSetOutstanding", outstandingData));
                result.DataSources.Add(new ReportDataSource("DataSetEDD", eddData));
                result.DataSources.Add(new ReportDataSource("DataSetAutoDeviation", autoDevData));
                result.DataSources.Add(new ReportDataSource("DataSetManualDeviation", manualDevData));
                result.DataSources.Add(new ReportDataSource("DataSetFinancial", finData));
                result.DataSources.Add(new ReportDataSource("DataSetFinancialProjection", finProjData));
                result.DataSources.Add(new ReportDataSource("DataSetBankStatement", new List<BankStatementData>()));
                result.DataSources.Add(new ReportDataSource("DataSetBankSummary", bankSummaryData));
                result.DataSources.Add(new ReportDataSource("DataSetTermConditionDoc", tcDocData));
                result.DataSources.Add(new ReportDataSource("DataSetLoanRatio", loanRatioData));

                result.Parameters.AddRange(new[] {
                    new ReportParameter("PrintDate", DateTime.Now.ToString("dd/MM/yyyy")),
                    new ReportParameter("PrintBy", "System Admin"),
                    new ReportParameter("SalesOfficer", "Anto Rakso"),
                    new ReportParameter("CreditAnalyst", "Sinto Harjo"),
                    new ReportParameter("PurposeOfFinance", "Investment"),
                    new ReportParameter("FinanceScheme", "Installment Financing"),
                    new ReportParameter("Branch", "Kelapa Gading")
                });
            }
            else if (reportName == "Report List Approved Cam By Cars Condition")
            {
                var listCamData = new List<ListCamByCarConditionData>
                {
                    new ListCamByCarConditionData { ApprovalDate = "24-10-2025", NewNonTruckUnit = 0, NewNonTruckNetFinance = 0, NewTruckUnit = 0, NewTruckNetFinance = 0, UsedNonTruckUnit = 1, UsedNonTruckNetFinance = 800000000, UsedTruckUnit = 0, UsedTruckNetFinance = 0 },
                    new ListCamByCarConditionData { ApprovalDate = "27-10-2025", NewNonTruckUnit = 1, NewNonTruckNetFinance = 400000000, NewTruckUnit = 1, NewTruckNetFinance = 1200000000, UsedNonTruckUnit = 0, UsedNonTruckNetFinance = 0, UsedTruckUnit = 0, UsedTruckNetFinance = 0 },
                    new ListCamByCarConditionData { ApprovalDate = "11-11-2025", NewNonTruckUnit = 0, NewNonTruckNetFinance = 0, NewTruckUnit = 1, NewTruckNetFinance = 500293650, UsedNonTruckUnit = 0, UsedNonTruckNetFinance = 0, UsedTruckUnit = 0, UsedTruckNetFinance = 0 },
                    new ListCamByCarConditionData { ApprovalDate = "19-12-2025", NewNonTruckUnit = 1, NewNonTruckNetFinance = 240000000, NewTruckUnit = 0, NewTruckNetFinance = 0, UsedNonTruckUnit = 0, UsedNonTruckNetFinance = 0, UsedTruckUnit = 0, UsedTruckNetFinance = 0 },
                    new ListCamByCarConditionData { ApprovalDate = "22-12-2025", NewNonTruckUnit = 0, NewNonTruckNetFinance = 0, NewTruckUnit = 0, NewTruckNetFinance = 0, UsedNonTruckUnit = 5, UsedNonTruckNetFinance = 1165000000, UsedTruckUnit = 0, UsedTruckNetFinance = 0 },
                    new ListCamByCarConditionData { ApprovalDate = "24-12-2025", NewNonTruckUnit = 0, NewNonTruckNetFinance = 0, NewTruckUnit = 2, NewTruckNetFinance = 720000000, UsedNonTruckUnit = 0, UsedNonTruckNetFinance = 0, UsedTruckUnit = 0, UsedTruckNetFinance = 0 },
                     new ListCamByCarConditionData { ApprovalDate = "24-10-2025", NewNonTruckUnit = 0, NewNonTruckNetFinance = 0, NewTruckUnit = 0, NewTruckNetFinance = 0, UsedNonTruckUnit = 1, UsedNonTruckNetFinance = 800000000, UsedTruckUnit = 0, UsedTruckNetFinance = 0 },
                    new ListCamByCarConditionData { ApprovalDate = "27-10-2025", NewNonTruckUnit = 1, NewNonTruckNetFinance = 400000000, NewTruckUnit = 1, NewTruckNetFinance = 1200000000, UsedNonTruckUnit = 0, UsedNonTruckNetFinance = 0, UsedTruckUnit = 0, UsedTruckNetFinance = 0 },
                    new ListCamByCarConditionData { ApprovalDate = "11-11-2025", NewNonTruckUnit = 0, NewNonTruckNetFinance = 0, NewTruckUnit = 1, NewTruckNetFinance = 500293650, UsedNonTruckUnit = 0, UsedNonTruckNetFinance = 0, UsedTruckUnit = 0, UsedTruckNetFinance = 0 },
                    new ListCamByCarConditionData { ApprovalDate = "19-12-2025", NewNonTruckUnit = 1, NewNonTruckNetFinance = 240000000, NewTruckUnit = 0, NewTruckNetFinance = 0, UsedNonTruckUnit = 0, UsedNonTruckNetFinance = 0, UsedTruckUnit = 0, UsedTruckNetFinance = 0 },
                    new ListCamByCarConditionData { ApprovalDate = "22-12-2025", NewNonTruckUnit = 0, NewNonTruckNetFinance = 0, NewTruckUnit = 0, NewTruckNetFinance = 0, UsedNonTruckUnit = 5, UsedNonTruckNetFinance = 1165000000, UsedTruckUnit = 0, UsedTruckNetFinance = 0 },
                    new ListCamByCarConditionData { ApprovalDate = "24-12-2025", NewNonTruckUnit = 0, NewNonTruckNetFinance = 0, NewTruckUnit = 2, NewTruckNetFinance = 720000000, UsedNonTruckUnit = 0, UsedNonTruckNetFinance = 0, UsedTruckUnit = 0, UsedTruckNetFinance = 0 },
                    new ListCamByCarConditionData { ApprovalDate = "07-01-2026", NewNonTruckUnit = 0, NewNonTruckNetFinance = 0, NewTruckUnit = 0, NewTruckNetFinance = 0, UsedNonTruckUnit = 3, UsedNonTruckNetFinance = 664000000, UsedTruckUnit = 0, UsedTruckNetFinance = 0 },
                    new ListCamByCarConditionData { ApprovalDate = "12-01-2026", NewNonTruckUnit = 0, NewNonTruckNetFinance = 0, NewTruckUnit = 0, NewTruckNetFinance = 0, UsedNonTruckUnit = 0, UsedNonTruckNetFinance = 0, UsedTruckUnit = 1, UsedTruckNetFinance = 280000000 },
                    new ListCamByCarConditionData { ApprovalDate = "22-01-2026", NewNonTruckUnit = 2, NewNonTruckNetFinance = 440000000, NewTruckUnit = 0, NewTruckNetFinance = 0, UsedNonTruckUnit = 1, UsedNonTruckNetFinance = 225000000, UsedTruckUnit = 1, UsedTruckNetFinance = 225000000 },
                    new ListCamByCarConditionData { ApprovalDate = "27-01-2026", NewNonTruckUnit = 1, NewNonTruckNetFinance = 80000000, NewTruckUnit = 1, NewTruckNetFinance = 517840000, UsedNonTruckUnit = 0, UsedNonTruckNetFinance = 0, UsedTruckUnit = 0, UsedTruckNetFinance = 0 }
                };

                var conditionSummary = new List<ConditionSummaryData>
                {
                    new ConditionSummaryData { Condition = "New", Unit = 80, UnitPercent = "66%", NetFinance = 29523691650m, NetFinancePercent = "75%" },
                    new ConditionSummaryData { Condition = "Used", Unit = 42, UnitPercent = "34%", NetFinance = 9726850000m, NetFinancePercent = "25%" },
                    new ConditionSummaryData { Condition = "Total", Unit = 122, UnitPercent = "100%", NetFinance = 39250541650m, NetFinancePercent = "100%" }
                };

                var vehicleTypeSummary = new List<VehicleTypeSummaryData>
                {
                    new VehicleTypeSummaryData { VehicleType = "Non Truck", Unit = 86, UnitPercent = "70%", NetFinance = 27916600000m, NetFinancePercent = "71%" },
                    new VehicleTypeSummaryData { VehicleType = "Truck", Unit = 36, UnitPercent = "30%", NetFinance = 11333941650m, NetFinancePercent = "29%" },
                    new VehicleTypeSummaryData { VehicleType = "Total", Unit = 122, UnitPercent = "100%", NetFinance = 39250541650m, NetFinancePercent = "100%" }
                };

                result.DataSources.Add(new ReportDataSource("DataSet1", listCamData));
                result.DataSources.Add(new ReportDataSource("DataSetSummaryCondition", conditionSummary));
                result.DataSources.Add(new ReportDataSource("DataSetSummaryVehicleType", vehicleTypeSummary));

                result.Parameters.AddRange(new[] {
                    new ReportParameter("StartDate", "01-09-2025"),
                    new ReportParameter("EndDate", "02-09-2026"),
                    new ReportParameter("Branch", "Kelapa Gading")
                });
            }
            else if (reportName == "Report Customer Overdue BPKB")
            {
                var overdueList = new List<CustomerOverdueBPKBData>
                {
                    new CustomerOverdueBPKBData { CustomerName = "ALFA KARSA PERSADA, PT.", Address = "KUTA RAYA NO. 7, RT. 017/007, KELAPA GADING BARAT, KELAPA GADING", Status = "Active", UpdateBy = "N/A", LastUpdate = "N/A" },
                    new CustomerOverdueBPKBData { CustomerName = "TURANGGA PRANADITA, PT.", Address = "SANGATA BLOK G 19 NO. 4, RT. 013/013, JATIWARINGIN, PONDOKGEDE", Status = "Active", UpdateBy = "N/A", LastUpdate = "N/A" },
                    new CustomerOverdueBPKBData { CustomerName = "LE & EN INDOCHACON UTAMA, PT.", Address = "LIMUS PRATAMA REGENCY C/5, RT. 001/010, LIMUSNUNGGAL, CILEUNGSI", Status = "Active", UpdateBy = "N/A", LastUpdate = "N/A" },
                    new CustomerOverdueBPKBData { CustomerName = "SANTOSA AGRINDO, PT.", Address = "PERMAI VII BX 5/7 PAMULANG PERMAI, RT. 004/012, PAMULANG BARAT, PAMULANG", Status = "Active", UpdateBy = "N/A", LastUpdate = "N/A" },
                    new CustomerOverdueBPKBData { CustomerName = "RAHMAT", Address = "PERUMAHAN MENTENG BINTARO, JL. TEUKU UMAR FB NO. 13, RT. 002/012, PONDOK RANJI, CIPUTAT TIMUR", Status = "Active", UpdateBy = "N/A", LastUpdate = "N/A" },
                    new CustomerOverdueBPKBData { CustomerName = "MASSINDO KARYA PRIMA, PT.", Address = "TAMAN HOLIS INDAH C.3 NO.28, RT. 006/006, CIGONDEWAH RAHAYU, BANDUNG KULON", Status = "Active", UpdateBy = "N/A", LastUpdate = "N/A" },
                    new CustomerOverdueBPKBData { CustomerName = "PANCARAN INDONESIA, PT.", Address = "CENGKEH NO. 22, RT. 007/007, PINANGSIA, TAMAN SARI", Status = "Active", UpdateBy = "N/A", LastUpdate = "N/A" },
                    new CustomerOverdueBPKBData { CustomerName = "BINTANG MANDIRI CENDRAWASIH, PT.", Address = "KAPUK UTARA II NO. 12, RT. 001/003, KAPUK MUARA, PENJARINGAN", Status = "Active", UpdateBy = "N/A", LastUpdate = "N/A" },
                    new CustomerOverdueBPKBData { CustomerName = "NATURAL PERSADA MANDIRI, PT.", Address = "DE PARK CLUSTER BRASSIA BLOK D9 NO.5, RT. 003/009, LENGKONG KULON, PAGEDANGAN", Status = "Active", UpdateBy = "N/A", LastUpdate = "N/A" },
                    new CustomerOverdueBPKBData { CustomerName = "MASTERPANCANG PONDASI, PT.", Address = "APT. WINDSOR TWR SIGNATURE UNIT 2081, RT. 001/002, KEMBANGAN SELATAN, KEMBANGAN", Status = "Active", UpdateBy = "N/A", LastUpdate = "N/A" }
                };

                result.DataSources.Add(new ReportDataSource("DataSet1", overdueList));

                result.Parameters.AddRange(new[] {
                    new ReportParameter("Branch", ""),
                    new ReportParameter("Customer", "")
                });
            }
            else if (reportName == "Report List of Sales")
            {
                var listSales = new List<ListofSales>
                {
                    new ListofSales { SalesNo = "410001", Name = "HARTONO", Address = "GATOT SUBROTO IV, RT.01/RW.01 KEPATIHAN, KALIWATES, JEMBER"},
                    new ListofSales { SalesNo = "410003", Name = " LIRIYANTO, SE", Address = "LOSARI KIDUL, LOSARI BREBES"},
                    new ListofSales { SalesNo = "410004", Name = "A A NGR EDDY BERATHA", Address = "MUDING INDAH I/1 KEROBOKAN"},
                    new ListofSales { SalesNo = "410005", Name = " EDDY SUSILA SURYADI", Address = "WAHIDIN NO 41 DENPASAR BR/LINK TEGAL LINGGAH, TEGAL LINGGAH, PEMECUTAN,\r\nDENPASAR BARAT"}
                };

                result.DataSources.Add(new ReportDataSource("DataSet1", listSales));

                result.Parameters.AddRange(new[]
                {
                    new ReportParameter("Branch", "")
                });
            }
            else if (reportName == "Report List of Supplier")
            {
                var listSupplier = new List<ListofSupplier>
                {
                    new ListofSupplier { Name = "MITSUI LEASING CAPITAL INDONESIA, PT.", NickName = "MLCI HO", Address = "PERMATA PLAZA LT. 11, RUANG 1106, JL. M.H. THAMRIN KAV. 57", City = "JAKARTA", Phone = "(021) 3903238", Contact = "RICKY IRAWAN", Position = "OPERATION D. HEAD", AccNo = "7340362122", Bank = "BCA", BankBranch = "WISMA NUSANTARA - JAKARTA"}
                };

                result.DataSources.Add(new ReportDataSource("DataSet1", listSupplier));

                result.Parameters.AddRange(new[]
                {
                    new ReportParameter("Branch", "")
                });
            }
            else if (reportName == "Report List of Model")
            {
                var listofModel = new List<ListofModel>
                {
                    new ListofModel { Model = "MINIBUS"},
                    new ListofModel { Model = "SEDAN"},
                    new ListofModel { Model = "JEEP"},
                    new ListofModel { Model = "PICK UP"},
                    new ListofModel { Model = "LIGHT TRUCK"}
                };

                result.DataSources.Add(new ReportDataSource("DataSet1", listofModel));
            }
            else if (reportName == "Report List of Insurance Premi")
            {
                var listInsurancePremi = new List<ListofInsurancePremi>
                {
                    new ListofInsurancePremi { InsuranceCompany = "Asuransi Central Asia", Model = "BUS (>30)", TypeofInsurance = "CP", Premi = "1:1", Branch = "Gatot Subroto"},
                    new ListofInsurancePremi { InsuranceCompany = "Asuransi Pan Pacific", Model = "BUS (>30)", TypeofInsurance = "CP", Premi = "1:1", Branch = "Gatot Subroto"},
                    new ListofInsurancePremi { InsuranceCompany = "Asuransi MSIG Indonesia", Model = "BUS (>30)", TypeofInsurance = "TLO", Premi = "0.23", Branch = "Gatot Subroto"},
                    new ListofInsurancePremi { InsuranceCompany = "Asuransi Raksa Pratikara", Model = "BUS (>30)", TypeofInsurance = "CP", Premi = "1:1", Branch = "Gatot Subroto"}
                };

                result.DataSources.Add(new ReportDataSource("DataSet1", listInsurancePremi));

                result.Parameters.AddRange(new[]
                {
                    new ReportParameter("BranchName", ""),
                    new ReportParameter("InsuranceCompany", ""),
                    new ReportParameter("ModelName", "")
                });
            }
            else if (reportName == "Report List of Insurance TPL")
            {
                var listofInsuranceTPL = new List<ListofInsuranceTPL>
                {
                    new ListofInsuranceTPL { InsuranceCompany = "Asuransi Central Asia", Model = "BUS (>30)", TPLAMT = "0", TPLFEE = "0", TPLFEE2 = "0", Branch = "Gatot Subroto"},
                    new ListofInsuranceTPL { InsuranceCompany = "Asuransi Sinar Mas", Model = "BUS (>30)", TPLAMT = "0", TPLFEE = "0", TPLFEE2 = "0", Branch = "Gatot Subroto"},
                    new ListofInsuranceTPL { InsuranceCompany = "Asuransi MSIG Indonesia", Model = "BUS (>30)", TPLAMT = "0", TPLFEE = "0", TPLFEE2 = "0", Branch = "Gatot Subroto"},
                };

                result.DataSources.Add(new ReportDataSource("DataSet1", listofInsuranceTPL));

                result.Parameters.AddRange(new[]
                {
                    new ReportParameter("BranchName", ""),
                    new ReportParameter("InsuranceCompany", ""),
                    new ReportParameter("ModelName", "")
                });
            }
            else if (reportName == "Report High Risk Customer APU PPT")
            {
                var highRiskCustomer = new List<HighRiskCustomer>
                {
                    new HighRiskCustomer { CustomerName = "A LIONG", Status = "Beneficial Owner (BO)", CustomerNumber = "", PlaceandDOB = "PKL PINANG, 17 Juni 1970", HighRiskCategory = "High Risk Business", Address = "DR MAKALIWE I/9, RT. 001/007, GROGOL, GROGOL PETAMBURAN RT:001 RW:007, GROGOL PETAMBURAN, GROGOL, JAKARTA BARAT", IDCard = "3173021706700001", CreatedDate = "29-11-2023"},
                    new HighRiskCustomer { CustomerName = "A RAHMAN SUDIRO", Status = "Beneficial Owner (BO)", CustomerNumber = "Beneficial Owner (BO)", PlaceandDOB = "PKL PINANG, 17 Juni 1970", HighRiskCategory = "High Risk Business", Address = "DR MAKALIWE I/9, RT. 001/007, GROGOL, GROGOL PETAMBURAN RT:001 RW:007, GROGOL PETAMBURAN, GROGOL, JAKARTA BARAT", IDCard = "3173021706700001", CreatedDate = "29-11-2023"},
                    new HighRiskCustomer { CustomerName = "A`AT", Status = "Customer", CustomerNumber = "131018", PlaceandDOB = "TANGERANG, 31 Agustus 1981", HighRiskCategory = "High Risk Business", Address = "KP. LEGOK, RT. 006/002, LEGOK, LEGOK ,TANGERANG", IDCard = "3603207108810003", CreatedDate = "18-02-2025"},
                };

                result.DataSources.Add(new ReportDataSource("DataSet1", highRiskCustomer));

                result.Parameters.AddRange(new[]
                {
                    new ReportParameter("CustomerNo", ""),
                    new ReportParameter("CustomerName", ""),
                    new ReportParameter("StartDate", "18-02-2025")
                });
            }
            else
            {
                var data = new List<Produk> { new Produk { ID = 1, Nama = "Produk A", Jumlah = 10 }, new Produk { ID = 2, Nama = "Produk B", Jumlah = 20 } };
                result.DataSources.Add(new ReportDataSource("DataSet1", data));
            }

            return result;
        }

        private static List<ClauseData> GetClauseData()
        {
            var result = new List<ClauseData>();
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=ReportMLCI;Trusted_Connection=True;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT mhc.ClauseHeaderCode, mhc.ClauseHeaderTitle, mhc.ClauseHeaderDescription, mhc.IsActive AS IsActiveHeader, mc.Id, mc.ClauseCode, mc.ClauseTitle, mc.ClauseContent, mc.IsActive AS IsActiveClause, mcd.ClauseSubCode, mcd.ClauseSubTitle, mcd.ClauseSubContent, mcd.MasterClauseId, mcd.IsActive AS IsActiveClauseSub, mcsd.SubDetailCode, mcsd.SubDetailTitle, mcsd.SubDetailContent, mcsd.MasterClauseDetailId, mcsd.IsActive AS IsActiveSubDetail FROM MasterClauses mc INNER JOIN MasterHeaderClauses mhc on mhc.Id = mc.MasterHeaderClauseId LEFT JOIN MasterClausesDetails mcd ON mc.Id = mcd.MasterClauseId LEFT JOIN MasterClausesSubDetails mcsd ON mcsd.MasterClauseDetailId = mcd.Id ORDER BY TRY_CAST(REPLACE(mc.ClauseCode, 'PASAL ', '') AS INT), COALESCE(TRY_CAST(PARSENAME(mcd.ClauseSubCode, 2) AS INT), 999999) * 1000 + COALESCE(TRY_CAST(PARSENAME(mcd.ClauseSubCode, 1) AS INT), 999999)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Guid? masterClauseId = null;
                            if (!reader.IsDBNull(reader.GetOrdinal("MasterClauseId"))) masterClauseId = Guid.Parse(reader["MasterClauseId"].ToString());
                            result.Add(new ClauseData
                            {
                                ClauseCode = reader["ClauseCode"].ToString() ?? "",
                                ClauseTitle = reader["ClauseTitle"].ToString() ?? "",
                                ClauseContent = reader["ClauseContent"].ToString() ?? "",
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
                                ClauseHeaderCode = reader["ClauseHeaderCode"].ToString() ?? "",
                                ClauseHeaderTitle = reader["ClauseHeaderTitle"].ToString() ?? "",
                                ClauseHeaderDescription = reader["ClauseHeaderDescription"].ToString() ?? "",
                                IsActiveHeader = Convert.ToBoolean(reader["IsActiveHeader"]),
                                MasterClauseId = masterClauseId
                            });
                        }
                    }
                }
            }
            catch
            {
                // Fallback to empty list if database connection is unavailable
            }
            return result;
        }
    }
}
