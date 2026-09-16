namespace ReportMLCI.Helper
{
    public static class ReportPathHelper
    {
        public static string GetReportFileName(string reportName)
        {
            return reportName switch
            {
                "Report 1" => "Report1.rdlc",
                "Report Lampiran 1" => "Report_Lampiran1.rdlc",
                "Report Lampiran 2" => "Report_Lampiran2.rdlc",
                "Report Clause MLCI" => "Report_ClauseMLCI.rdlc",
                "Report List CAM Approve Date" => "ReportListCAMByApprovalDate.rdlc",
                "Report List Rejected CAM" => "ReportListRejectedCAM.rdlc",
                "Report History Payment" => "ReportHistoryPayment.rdlc",
                "Report Early Termination" => "ReportEarlyTermination.rdlc",
                "Report Monthly Summary Approved By CMO" => "ReportMonthlySummaryApprovedByCMO.rdlc",
                "Report History of BPKB Submission by Dealer/ Showroom" => "ReportHistoryofBPKBSubmissionbyDealerorShowroom.rdlc",
                "Report Summary CAM MOU Saran MCA" => "ReportSummaryCAMMOUSaranMCA.rdlc",
                "Report List Approved Cam By Cars Condition" => "ReportListApprovedCamByCarsCondition.rdlc",
                "Report Customer Overdue BPKB" => "ReportCustomerOverdueBPKB.rdlc",
                "Report List of Sales" => "ReportListofSales.rdlc",
                "Report List of Supplier" => "ReportListofSupplier.rdlc",
                "Report List of Model" => "ReportListofModel.rdlc",
                "Report List of Insurance Premi" => "ReportListofInsurancePremi.rdlc",
                "Report List of Insurance TPL" => "ReportListofInsuranceTPL.rdlc",
                "Report High Risk Customer APU PPT" => "ReportHighRiskCustomerAPUPPT.rdlc",
                "Report Blacklist" => "ReportBlacklist.rdlc",
                "Report Detail SID Checking" => "ReportDetailSIDChecking.rdlc",
                "CF_FD_PO_TC" => "Retail/CF_FD_PO_TC.rdlc",
                _ => string.Empty
            };
        }
    }
}
