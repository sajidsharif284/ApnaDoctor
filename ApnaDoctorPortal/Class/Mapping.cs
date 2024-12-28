using ApnaDoctor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ApnaDoctor.Class
{

    public class PatientInformationForFirebase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public string Mobilenumber { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Disease Disease { get; set; }
        [ForeignKey("Disease")]
        public int DiseaseId { get; set; }
    }
    public class TimeList
    {
        public string strtime { get; set; }
        public string Slotes { get; set; }
    }
    public class Rough
    {

        public string Sloting { get; set; }
    }
    public class Param
    {
        public string Email { get; set; }
    }
    public class doctorsoltmapping
    {
        public List<DoctorSlotsResult> DoctorSlotsResult { get; set; }
    }
    public class DoctorSlotsResult
    {
        public List<DoctorSlotsContract> DoctorSlotsContract { get; set; }
        public List<Slots> Slots { get; set; }
        public string ResponseCode { get; set; }
        public string Message { get; set; }
    }
    public class DoctorSlotsContract
    {
        public string Day { get; set; }
       public List<Slots> Slots { get; set; }
    }
    public class Slots
    {
        public int Id { get; set; }
        public string Time { get; set; }
    }
    public class DoctorScheduleContract
    {
        [Key]
        public int Id { get; set; }

        public DoctorProfile DoctorProfile { get; set; }
        [ForeignKey("DoctorProfile")]
        public string DoctorProfileId { get; set; }
        public string FirstName { get; set; }
        public string DayFrom { get; set; }
        public string DayTo { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Week { get; set; }
        public string SlotDuration { get; set; }
    }

    public class DiscountContract
    {
        public string Discount { get; set; }
    }
    public class MedicineDetails
    {
        public string id { get; set; }
        public string medicineName { get; set; }
        public string night { get; set; }
        public string morning { get; set; }
        public string day { get; set; }
        public string noOfDays { get; set; }
        public string additionalNotes { get; set; }
    }
    public class Patientprofile
    {
        public string MobileNumber { get; set; }
        public string Name { get; set; }
    }
    public class GetTplRecordResult
    {
        public List<MedicineDetails> MedicineDetails { get; set; }
        public PatientProfile PatientProfile { get; set; }
        public DiscountContract DiscountContract { get; set; }
    }
    public class GetTplRecordResultMapping
    {
        public List<GetTplRecordResult> GetTplRecordResult { get; set; }
       
    }







    public class loginresultmapping
    {
        public LoginResult LoginResult { get; set; }
    }
    public class ParamInsurance
    {
        public string Email  { get; set; }
        public string Password { get; set; }
    }
    public class LoginResult
    {
        public List<PanelDetails> PanelDetails { get; set; }
        public PaymentDetails PaymentDetails { get; set; }
        public List<FamilyMember> FamilyMember { get; set; }
        public List<PatientProfile> PatientProfile { get; set; }
        public List<Disease> Disease { get; set; }
        public string Message { get; set; }
        public string ResponseCode { get; set; }
    }

    public class PanelDetails
    {

        public string CNIC { get; set; }
        public string CompanyName { get; set; }
        public string Id { get; set; }
        public string LogoUrl { get; set; }
        public string PolicyNumber { get; set; }
        public string status { get; set; }

    }
    public class FamilyMember
    {
        public string Name { get; set; }
        public string Relation { get; set; }
        public string id { get; set; }
       

    }
    public class PatientProfile
    {

        public string Age { get; set; }
        public string ApplicationUserId { get; set; }
        public List<Disease> Disease { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public string Height { get; set; }
        public string LastName { get; set; }
        public string Message { get; set; }
        public string MobileNumber { get; set; }
        public string ResponseCode { get; set; }
        public string Weight { get; set; }
    }
    public class Disease
    {

        public string Id { get; set; }
        public string Name { get; set; }

    }
    public class PaymentDetails
    {

        public bool IsAlreadyPaid { get; set; }

    }
    public class EasypaisaParam
    {

        public string orderId { get; set; }
        public string storeId { get; set; }
        public string transactionAmount { get; set; }
        public string transactionType { get; set; }
        public string mobileAccountNo { get; set; }
        public string emailAddress { get; set; }
    }
    public class EasypaisaResultMapping
    {

        public string orderId { get; set; }
        public string storeId { get; set; }
        public string transactionId { get; set; }
        public string transactionDateTime { get; set; }
        public string responseCode { get; set; }
        public string responseDesc { get; set; }

    }
    public class JazzCashParam
    {

        public string pp_TxnRefNo { get; set; }
        public string pp_Password { get; set; }
        public string pp_BillReference { get; set; }
        public string pp_TxnExpiryDateTime { get; set; }
        public string pp_SecureHash { get; set; }
        public string pp_ProductID { get; set; }
        public string pp_TxnCurrency { get; set; }
        public string pp_MerchantID { get; set; }
        public string pp_DiscountedAmount { get; set; }
        public string pp_Amount { get; set; }
        public string ppmpf_5 { get; set; }
        public string ppmpf_4 { get; set; }
        public string pp_MobileNumber { get; set; }
        public string pp_Language { get; set; }
        public string pp_CNIC { get; set; }
        public string pp_BankID { get; set; }
        public string ppmpf_1 { get; set; }
        public string pp_SubMerchantID { get; set; }
        public string pp_TxnDateTime { get; set; }
        public string ppmpf_3 { get; set; }
        public string pp_Description { get; set; }
        public string ppmpf_2 { get; set; }
        

    }
    public class JazzCashResultMapping
    {

        public string pp_TxnRefNo { get; set; }
        public string pp_Password { get; set; }
        public string pp_BillReference { get; set; }
        public string pp_TxnExpiryDateTime { get; set; }
        public string pp_SecureHash { get; set; }
        public string pp_ProductID { get; set; }
        public string pp_TxnCurrency { get; set; }
        public string pp_MerchantID { get; set; }
        public string pp_DiscountedAmount { get; set; }
        public string pp_Amount { get; set; }
        public string ppmpf_5 { get; set; }
        public string ppmpf_4 { get; set; }
        public string pp_MobileNumber { get; set; }
        public string pp_Language { get; set; }
        public string pp_CNIC { get; set; }
        public string pp_BankID { get; set; }
        public string ppmpf_1 { get; set; }
        public string pp_SubMerchantID { get; set; }
        public string pp_TxnDateTime { get; set; }
        public string ppmpf_3 { get; set; }
        public string pp_Description { get; set; }
        public string ppmpf_2 { get; set; }
        public string pp_ResponseCode { get; set; }
        public string pp_ResponseMessage { get; set; }

    }
    public class JSBankResultMapping
    {

        public string refresh_token_expires_in { get; set; }
        //public List<api_product_list> api_product_list { get; set; }
        //public List<api_product_list_json> api_product_list_json { get; set; }
        public string organization_name { get; set; }
        public string token_type { get; set; }
        public string issued_at { get; set; }
        public string client_id { get; set; }
        public string access_token { get; set; }
        public string application_name { get; set; }
        public string scope { get; set; }
        public string expires_in { get; set; }
        public string refresh_count { get; set; }
        public string status { get; set; }


    }
    public class JSBankAccountInquiryRequestParam
    {
        public AccountInquiryRequest AccountInquiryRequest { get; set; }
        
        

    }
    public class AccountInquiryRequest
    {
        
        public string MerchantType { get; set; }
        public string TraceNo { get; set; }
        public string CompanyName { get; set; }
        public string DateTime { get; set; }
        public string TerminalId { get; set; }
        public string MobileNo { get; set; }
        public string Amount { get; set; }
        public string TransactionType { get; set; }
        public string Reserved1 { get; set; }


    }
    public class JSBankAccountInquiryResponse
    {

        public string MerchantType { get; set; }
        public string TraceNo { get; set; }
        public string CompanyName { get; set; }
        public string DateTime { get; set; }
        public string Charges { get; set; }
        public string ResponseCode { get; set; }
        
    }
    public class JSBankPaymentRequestParam
    {

        public PaymentRequest PaymentRequest { get; set; }
       

    }
    public class PaymentRequest
    {

        public string Amount { get; set; }
        public string Reserved1 { get; set; }
        public string Charges { get; set; }
        public string PaymentType { get; set; }
        public string TerminalId { get; set; }
        public string AccountNumber { get; set; }
        public string OtpPin { get; set; }
        public string CompanyName { get; set; }
        public string DateTime { get; set; }
        public string MerchantType { get; set; }
        public string TraceNo { get; set; }
        public string TransactionType { get; set; }
        public string SettlementType { get; set; }
        public string TransactionCode { get; set; }
       
    }

    public class PaymentResponse
    {

        public string MerchantType { get; set; }
        public string TraceNo { get; set; }
        public string CompanyName { get; set; }
        public string DateTime { get; set; }
        public string TransactionCode { get; set; }
        public string ResponseCode { get; set; }
        

    }



}