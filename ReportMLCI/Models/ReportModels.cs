using System;

namespace ReportMLCI.Models
{
    public class Produk
    {
        public int ID { get; set; }
        public string Nama { get; set; } = string.Empty;
        public int Jumlah { get; set; }
    }

    public class DataLampiran2
    {
        public decimal ItemPrice { get; set; }
        public decimal DownpaymentAmount { get; set; }
        public decimal MonthlyInstallmentAmount { get; set; }
        public decimal PaymentDurationMonths { get; set; }
        public decimal TotalPayableAmount { get; set; }
        public decimal InsurancePremiumAmount { get; set; }
        public decimal SurveyAdminFeeAmount { get; set; }
        public decimal ProvincialFeeAmount { get; set; }
        public decimal NotaryFeeAmount { get; set; }
        public decimal FiduciaryRegistrationFeeAmount { get; set; }
        public decimal OtherFeeAmount { get; set; }
        public decimal TotalFeeAmount { get; set; }
        public string Np { get; set; } = string.Empty;
        public string ItemBrand { get; set; } = string.Empty;
        public DateTime YearCondition { get; set; }
        public string NamaKreditur { get; set; } = string.Empty;
        public string AlamatKreditur { get; set; } = string.Empty;
        public string NorekKreditur { get; set; } = string.Empty;
        public string NameDebitur { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string InterestFlatRate { get; set; } = string.Empty;
        public string InterestEffectiveRate { get; set; } = string.Empty;
        public string InstallmentPaymentTiming { get; set; } = string.Empty;
        public string InstallmentPaymentMethod { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public string DueDateDescription { get; set; } = string.Empty;
        public string InsuranceCompanyName { get; set; } = string.Empty;
        public string PolicyDuration { get; set; } = string.Empty;
        public string PremiumPaymentMethod { get; set; } = string.Empty;
        public string CoverageDetail { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
    }

    public class CoverageDetail
    {
        public int Year { get; set; }
        public decimal CoverageAmount { get; set; }
        public decimal ThirdPartyLiabilityAmount { get; set; }
        public string CoverageType { get; set; } = string.Empty;
    }

    public class DataDebitur
    {
        public string Name { get; set; } = string.Empty;
    }

    public class ClauseData
    {
        public string ClauseHeaderCode { get; set; } = string.Empty;
        public string ClauseHeaderTitle { get; set; } = string.Empty;
        public string ClauseHeaderDescription { get; set; } = string.Empty;
        public bool IsActiveHeader { get; set; }
        public string ClauseCode { get; set; } = string.Empty;
        public string ClauseTitle { get; set; } = string.Empty;
        public string ClauseContent { get; set; } = string.Empty;
        public bool IsActiveClause { get; set; }
        public string ClauseSubCode { get; set; } = string.Empty;
        public string ClauseSubTitle { get; set; } = string.Empty;
        public string ClauseSubContent { get; set; } = string.Empty;
        public Guid? MasterClauseId { get; set; }
        public bool IsActiveClauseSub { get; set; }
        public string SubDetailCode { get; set; } = string.Empty;
        public string SubDetailTitle { get; set; } = string.Empty;
        public string SubDetailContent { get; set; } = string.Empty;
        public string MasterClauseDetailId { get; set; } = string.Empty;
        public bool IsActiveSubDetail { get; set; }
    }

    public class CMOProductivityData
    {
        public string BranchName { get; set; } = string.Empty;
        public string No { get; set; } = string.Empty;
        public string CMOName { get; set; } = string.Empty;
        public string D01 { get; set; } = string.Empty;
        public string D02 { get; set; } = string.Empty;
        public string D03 { get; set; } = string.Empty;
        public string D04 { get; set; } = string.Empty;
        public string D05 { get; set; } = string.Empty;
        public string D06 { get; set; } = string.Empty;
        public string D07 { get; set; } = string.Empty;
        public string D08 { get; set; } = string.Empty;
        public string D09 { get; set; } = string.Empty;
        public string D10 { get; set; } = string.Empty;
        public string D11 { get; set; } = string.Empty;
        public string D12 { get; set; } = string.Empty;
        public string D13 { get; set; } = string.Empty;
        public string D14 { get; set; } = string.Empty;
        public string D15 { get; set; } = string.Empty;
        public string D16 { get; set; } = string.Empty;
        public string D17 { get; set; } = string.Empty;
        public string D18 { get; set; } = string.Empty;
        public string D19 { get; set; } = string.Empty;
        public string D20 { get; set; } = string.Empty;
        public string D21 { get; set; } = string.Empty;
        public string D22 { get; set; } = string.Empty;
        public string D23 { get; set; } = string.Empty;
        public string D24 { get; set; } = string.Empty;
        public string D25 { get; set; } = string.Empty;
        public string D26 { get; set; } = string.Empty;
        public string D27 { get; set; } = string.Empty;
        public string D28 { get; set; } = string.Empty;
        public string D29 { get; set; } = string.Empty;
        public string D30 { get; set; } = string.Empty;
        public string Total { get; set; } = string.Empty;
        public string AvgDay { get; set; } = string.Empty;
        public string BranchCMOCount { get; set; } = string.Empty;
        public string GrandTotalCMOCount { get; set; } = string.Empty;
    }

    public class BPKBSubmissionData
    {
        public string No { get; set; } = string.Empty;
        public string DealerShowroomName { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string ContractNo { get; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty;
        public string DisbursementDate { get; set; } = string.Empty;
        public string ReceivedDateCA { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;
    }

    public class SummaryCAMMOUHeaderData
    {
        public string CustomerName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string EstablishedDate { get; set; } = string.Empty;
        public string NPWPID { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string Business { get; set; } = string.Empty;
        public string CustomerStatus { get; set; } = string.Empty;
        public string CustomerGrading { get; set; } = string.Empty;
        public string CustomerRating { get; set; } = string.Empty;
        public string CustomerAssignedCategory { get; set; } = string.Empty;
        public string Watchlist { get; set; } = string.Empty;
        public string FinanceScheme { get; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty;
        public string AuthorizedCapital { get; set; } = string.Empty;
        public string PaidInCapital { get; set; } = string.Empty;
    }

    public class ShareholderData
    {
        public string No { get; set; } = string.Empty;
        public string NameInIDCard { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string ShareholderPercentage { get; set; } = string.Empty;
        public string TotalShares { get; set; } = string.Empty;
        public string NominalShares { get; set; } = string.Empty;
        public string BO { get; set; } = string.Empty;
        public string Signer { get; set; } = string.Empty;
    }

    public class DeedData
    {
        public string No { get; set; } = string.Empty;
        public string StateGazetteNo { get; set; } = string.Empty;
        public string DeedNo { get; set; } = string.Empty;
        public string DateOfDeed { get; set; } = string.Empty;
        public string NotaryName { get; set; } = string.Empty;
        public string CertificateMinistry { get; set; } = string.Empty;
    }

    public class SignerData
    {
        public string Name { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string EndTermOffice { get; set; } = string.Empty;
    }

    public class ManagementDetailData
    {
        public string No { get; set; } = string.Empty;
        public string NameInIDCard { get; set; } = string.Empty;
        public string IDCard { get; set; } = string.Empty;
        public string AddressInID { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }

    public class OutstandingObligorData
    {
        public string Category { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public string Amount { get; set; } = string.Empty;
    }

    public class EDDQuestionnaireData
    {
        public string No { get; set; } = string.Empty;
        public string Questionnaire { get; set; } = string.Empty;
        public string YesNo { get; set; } = string.Empty;
    }

    public class DeviationData
    {
        public string No { get; set; } = string.Empty;
        public string DeviationText { get; set; } = string.Empty;
    }

    public class FinancialData
    {
        public string LineItem { get; set; } = string.Empty;
        public string Year1 { get; set; } = string.Empty;
        public string Year2 { get; set; } = string.Empty;
        public string IsHeader { get; set; } = string.Empty;
        public string IsBold { get; set; } = string.Empty;
    }

    public class FinancialProjectionData
    {
        public string LineItem { get; set; } = string.Empty;
        public string Projection { get; set; } = string.Empty;
        public string IsHeader { get; set; } = string.Empty;
        public string IsBold { get; set; } = string.Empty;
    }

    public class BankStatementData
    {
        public string Bank { get; set; } = string.Empty;
        public string AccountName { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }

    public class BankSummaryData
    {
        public string MonthYear { get; set; } = string.Empty;
        public string BeginningBalance { get; set; } = string.Empty;
        public string Debet { get; set; } = string.Empty;
        public string Credit { get; set; } = string.Empty;
        public string EndingBalance { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }

    public class TermConditionDocData
    {
        public string No { get; set; } = string.Empty;
        public string DocumentName { get; set; } = string.Empty;
        public string PriorTo { get; set; } = string.Empty;
        public string Check { get; set; } = string.Empty;
        public string Waived { get; set; } = string.Empty;
        public string PromiseDate { get; set; } = string.Empty;
        public string ExpiredDate { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public string View { get; set; } = string.Empty;
    }

    public class LoanRatioData
    {
        public string Category { get; set; } = string.Empty;
        public string No { get; set; } = string.Empty;
        public string ApplicationNo { get; set; } = string.Empty;
        public string BrandType { get; set; } = string.Empty;
        public string UnitYear { get; set; } = string.Empty;
        public string Financing { get; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty;
        public string InstalmentMonth { get; set; } = string.Empty;
        public string MarketPrice { get; set; } = string.Empty;
        public string Outstanding { get; set; } = string.Empty;
        public string LoanRatioPercent { get; set; } = string.Empty;
    }

    public class ListCamByCarConditionData
    {
        public string ApprovalDate { get; set; } = string.Empty;
        public int NewNonTruckUnit { get; set; }
        public decimal NewNonTruckNetFinance { get; set; }
        public int NewTruckUnit { get; set; }
        public decimal NewTruckNetFinance { get; set; }
        public int UsedNonTruckUnit { get; set; }
        public decimal UsedNonTruckNetFinance { get; set; }
        public int UsedTruckUnit { get; set; }
        public decimal UsedTruckNetFinance { get; set; }
    }

    public class ConditionSummaryData
    {
        public string Condition { get; set; } = string.Empty;
        public int Unit { get; set; }
        public string UnitPercent { get; set; } = string.Empty;
        public decimal NetFinance { get; set; }
        public string NetFinancePercent { get; set; } = string.Empty;
    }

    public class VehicleTypeSummaryData
    {
        public string VehicleType { get; set; } = string.Empty;
        public int Unit { get; set; }
        public string UnitPercent { get; set; } = string.Empty;
        public decimal NetFinance { get; set; }
        public string NetFinancePercent { get; set; } = string.Empty;
    }

    public class CustomerOverdueBPKBData
    {
        public string CustomerName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string UpdateBy { get; set; } = string.Empty;
        public string LastUpdate { get; set; } = string.Empty;
    }
    public class ListofSales
    {
        public string SalesNo { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } =string.Empty;
    }
    public class ListofSupplier
    {
        public string Name { get; set; } = string.Empty;
        public string NickName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string AccNo { get; set; } = string.Empty;
        public string Bank { get; set; } = string.Empty;
        public string BankBranch { get; set; } = string.Empty;
    }
    public class ListofModel
    { 
        public string Model { get; set; } = string.Empty;
    }
    public class ListofInsurancePremi
    { 
        public string InsuranceCompany { get; set; } = string.Empty;
        public string Model { get; set;} = string.Empty;
        public string TypeofInsurance { get; set; } = string.Empty;
        public string Premi { get; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty;
    }
    public class ListofInsuranceTPL
    {
        public string InsuranceCompany { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string TPLAMT { get; set; } = string.Empty;
        public string TPLFEE { get; set; } = string.Empty;
        public string TPLFEE2 { get; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty;
    }
    public class HighRiskCustomer
    {
        public string CustomerName { get; set; } = string.Empty;
        public string Status { get; set;} = string.Empty;
        public string CustomerNumber { get; set; } = string.Empty;
        public string PlaceandDOB {  get; set; } = string.Empty;
        public string HighRiskCategory { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string IDCard { get; set; } = string.Empty;
        public string CreatedDate { get; set; } = string.Empty;
    }
}
