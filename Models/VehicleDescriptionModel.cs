using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromeVehicleDescriptions.Models
{
    public class InStockVehicle
    {
        public string VIN { get; set; }
        public string StockNumber { get; set; }
        public string XrefId { get; set; }
        public string Condition { get; set; }
    }

    public class ChromeVehicleDescriptionModel
    {
        public string error { get; set; }
        public string copyright { get; set; }
        public VehicleResult result { get; set; }
    }

    public class VehicleResult
    {
        public string vinSubmitted { get; set; }
        public string vinProcessed { get; set; }
        public bool validVin { get; set; }
        public string source { get; set; }
        public int year { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public string modelID { get; set; }
        public decimal buildMSRP { get; set; }
        public DateTime buildDate { get; set; }
        public string buildSource { get; set; }
        public decimal estimatedMSRP { get; set; }
        public string wmiCountry { get; set; }
        public string wmiManufacturer { get; set; }
        public IEnumerable<Vehicle> vehicles { get; set; }
        public IEnumerable<ExteriorColor> exteriorColors { get; set; }
        public IEnumerable<InteriorColor> interiorColors { get; set; }
        public IEnumerable<Feature> features { get; set; }
        public IEnumerable<TechSpec> techSpecs { get; set; }
        public IEnumerable<Package> packages { get; set; }
        public string[] optionCodes { get; set; }
        public IEnumerable<OptionCodeContent> optionCodeContent { get; set; }
    }

    public class TechSpec
    {
        public int id { get; set; }
        public string key { get; set; }
        public int sectionId { get; set; }
        public int subSectionId { get; set; }
        public string sectionName { get; set; }
        public string name { get; set; }
        public string nameNoBrand { get; set; }
        public string description { get; set; }
        public int rankingValue { get; set; }
        public IEnumerable<Style> styles { get; set; }
    }

    public class Package
    {
        public int id { get; set; }
        public string key { get; set; }
        public int sectionId { get; set; }
        public int subSectionId { get; set; }
        public string sectionName { get; set; }
        public string name { get; set; }
        public string nameNoBrand { get; set; }
        public string description { get; set; }
        public int rankingValue { get; set; }
        public IEnumerable<Style> styles { get; set; }
        public IEnumerable<OptionDetail> optionDetails { get; set; }
    }

    public class OptionDetail
    {
        public string optionCode { get; set; }
        public decimal msrp { get; set; }
        public string[] content { get; set; }
    }

    public class OptionCodeContent
    {
        public string optionCode { get; set; }
        public decimal msrp  { get; set; }
        public string optionDescription { get; set; }
        public string installCause { get; set; }
    }

    public class Feature
    {
        public int id { get; set; }
        public string key { get; set; }
        public int sectionId { get; set; }
        public int subSectionId { get; set; }
        public string sectionName { get; set; }
        public string name { get; set; }
        public string nameNoBrand { get; set; }
        public string description { get; set; }
        public int rankingValue { get; set; }
        public IEnumerable<Style> styles { get; set; }
        public IEnumerable<BenefitStatement> benefitStatement { get; set; }

    }
    public class Style
    {
        public int[] styleIds { get; set; }
        public string installCause { get; set; }
        public bool isStandard { get; set; }
    }

    public class BenefitStatement
    {
        public string title { get; set; }
        public string definition { get; set; }
        public string statement { get; set; }
    }

    public class ExteriorColor
    {
        public string genericDesc { get; set; }
        public string description { get; set; }
        public string colorCode { get; set; }
        public string installCause { get; set; }
        public int[] styles { get; set; }
        public string rgbValue { get; set; }
        public string rgbHexValue { get; set; }
        public int type { get; set; }
        public bool primary { get; set; }
    }

    public class InteriorColor
    {
        public string genericDesc { get; set; }
        public string description { get; set; }
        public string colorCode { get; set; }
        public string installCause { get; set; }
        public int[] styles { get; set; }
    }

    public class Vehicle
    {
        public int styleId { get; set; }
        public string styleDescription { get; set; }
        public string trim { get; set; }
        public decimal baseMSRP { get; set; }
        public decimal destinationCharge { get; set; }
        public string driveType { get; set; }
        public string bodyType { get; set; }
        public int standardCurbWeight { get; set; }
        public int standardPayload { get; set; }
        public string country { get; set; }
        public int standardGVWR { get; set; }
        public int standardTowingCapacity { get; set; }
        public string mfrModelCode { get; set; }
        public int doors { get; set; }
        public string[] segment { get; set; }
        public decimal wheelbase { get; set; }

    }
}
