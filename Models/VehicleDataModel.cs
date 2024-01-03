using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromeVehicleDescriptions.Models
{
    class VehicleDataModel
    {
        public VehicleData VehicleData { get; set; }
        public StyleData StyleData { get; set; }
        public ExteriorColorData ExteriorColorData { get; set; }
        public InteriorColorData InteriorColorData { get; set; }

        public List<FeatureData> FeatureData { get; set; }
        public List<PackageData> PackageData { get; set; }
        public List<TechSpecData> TechSpecData { get; set; }
        public List<OptionData> OptionData { get; set; }
    }

    public class VehicleData
    {
        public int Id { get; set; }
        public string VIN { get; set; }
        public string StockNumber { get; set; }
        public string XrefId { get; set; }
        public string Source { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string ModelId { get; set; }
        public decimal BuildMSRP { get; set; }
        public DateTime BuildDate { get; set; }
        public string Country { get; set; }
        public string Manufacturer { get; set; }
        public string BuildSource { get; set; }
        public int StyleId { get; set; }
        public int ExteriorColorId { get; set; }
        public int InteriorColorId { get; set; }
        public DateTime DateUpdated { get; set; }

        public string Condition { get; set; }
        public string CertificationLevelCode { get; set; }
        public string CertificationLevel { get; set; }
        public int InReconditioning { get; set; }
        public int ManagerSpecial { get; set; }
        public DateTime ManagerSpecialStartDate { get; set; }
        public DateTime ManagerSpecialEndDate { get; set; }
		public decimal VehiclePrice { get; set; }
        public string OptionsApprovedBy { get; set; }
        public DateTime OptionsApprovedDate { get; set; }
        public string VehicleLocation { get; set; }
        public string FuelType { get; set; }
        public string DealerComments { get; set;}
        public int HasDealerOptions { get; set; }
        public int OptionCount { get; set; }
        public int FitzWayCheckedOut { get; set; }
    }

    public class StyleData
    {
        public int Id { get; set; }
        public int StyleId { get; set; }
        public string StyleDescription { get; set; }
        public string Trim { get; set; }
        public decimal BaseMSRP { get; set; }
        public decimal DestinationCharge { get; set; }
        public string DriveType { get; set; }
        public string BodyType { get; set; }
        public int StandardCurbWeight { get; set; }
        public int StandardTowingCapacity { get; set; }
        public string Country { get; set; }
        public int StandardGVWR { get; set; }
        public string MFRModelCode { get; set; }
        public int Doors { get; set; }
        public string Segment { get; set; }
        public decimal Wheelbase { get; set; }
    }

    public class ExteriorColorData
    {
        public int Id { get; set; }
	    public string GenericDescription { get; set; }
        public string Description { get; set; }
        public string ColorCode { get; set; }
        public string InstallCause { get; set; }
	    public int StyleId { get; set; }
        public string RGBValue { get; set; }
        public string RGBHexValue { get; set; }
        public int Type { get; set; }
        public bool Primary { get; set; }
    }

    public class InteriorColorData
    {
        public int Id { get; set; }
        public string GenericDescription { get; set; }
        public string ColorCode { get; set; }
        public string Description { get; set; }
        public string InstallCause { get; set; }
        public int StyleId { get; set; }
    }

    public class FeatureData
    {
        public int Id { get; set; }
        public int FeatureId { get; set; }
        public string Key { get; set; }
        public int SectionId { get; set; }
        public int SubSectionId { get; set; }
        public string SectionName { get; set; }
        public string Name { get; set; }
        public string NameNoBrand { get; set; }
        public string Description { get; set; }
        public int RankingValue { get; set; }
        public int StyleId { get; set; }
        public string InstallCause { get; set; }
        public bool IsStandard { get; set; }
        public string BenefitTitle { get; set; }
        public string BenefitDefinition { get; set; }
        public string BenefitStatement { get; set; }
        public bool ShowOption { get; set; }
    }

    public class PackageData
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        public string Key { get; set; }
        public int SectionId { get; set; }
        public int SubSectionId { get; set; }
        public string SectionName { get; set; }
        public string Name { get; set; }
        public string NameNoBrand { get; set; }
        public string Description { get; set; }
        public int RankingValue { get; set; }
        public int StyleId { get; set; }
        public string InstallCause { get; set; }
        public bool IsStandard { get; set; }
        public string OptionCode { get; set; }
        public decimal MSRP { get; set; }
        public string Content { get; set; }
        public bool ShowOption { get; set; }
    }

    public class TechSpecData
    {
        public int Id { get; set; }
        public int TechSpecId { get; set; }
        public string Key { get; set; }
        public int SectionId { get; set; }
        public int SubSectionId { get; set; }
        public string SectionName { get; set; }
        public string Name { get; set; }
        public string NameNoBrand { get; set; }
        public string Description { get; set; }
        public int RankingValue { get; set; }
        public int StyleId { get; set; }
        public string InstallCause { get; set; }
        public bool IsStandard { get; set; }
        public string BenefitTitle { get; set; }
        public string BenefitDefinition { get; set; }
        public string BenefitStatement { get; set; }
        public bool ShowOption { get; set; }
    }

    public class OptionData
    {
        public int Id { get; set; }
        public int StyleId { get; set; }
        public string OptionCode { get; set; }
        public decimal MSRP { get; set; }
        public string OptionDescription { get; set; }
        public string InstallCause { get; set; }
        public string Content { get; set; }
        public bool ShowOption { get; set; }
    }

}
