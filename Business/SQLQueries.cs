using System;
using System.Collections.Generic;
using ChromeVehicleDescriptions.Models;
using ChromeVehicleDescriptions.Business;

namespace ChromeVehicleDescriptions.Business
{
    public class SQLQueries
    {

        public static List<InStockVehicle> GetInStockVehicles()
        {
            //var results = SqlMapperUtil.SqlWithParams<InStockVehicle>("Select V_Vin as VIN, v_Stock as StockNumber, V_xrefid as XrefId from fitzway.dbo.AllInventoryFM where v_vin <> 'XX' and v_vin not in (Select VIN from ChromeDataCVD.dbo.Vehicle)", "", "JJFServer");
            //var results = SqlMapperUtil.SqlWithParams<InStockVehicle>("Select vin as VIN, stk as StockNumber, '' as XrefId from Junk.dbo.csv_vehicleused where (vin <> 'XX' and vin <> '') and (status = 1 or status = 2 or status = 4) and vin not in (Select VIN from ChromeDataCVD.dbo.Vehicle)", "", "JJFServer");
            var results = SqlMapperUtil.SqlWithParams<InStockVehicle>("Select vin as VIN, stk_no as StockNumber, '' as XrefId from Junk.dbo.csv_vehiclenew where (vin <> 'XX' and vin <> '') and status <> 26 and vin not in (Select VIN from ChromeDataCVD.dbo.Vehicle)", "", "JJFServer");

            
            return results;
        }
        public static bool CheckStyle(int styleId)
        {
            var styleExists = false;

            var results = SqlMapperUtil.StoredProcWithParams<StyleData>("sp_GetStyle", new { StyleId = styleId}, "JJFServer");

            if(results != null && results.Count > 0)
            {
                styleExists = true;
            }

            return styleExists;

        }

        public static int CheckExteriorColor(string colorCode, int styleId)
        {
            var colorCodeId = 0;

            var results = SqlMapperUtil.StoredProcWithParams<ExteriorColorData>("sp_GetExteriorColor", new {ColorCode = colorCode, StyleId = styleId }, "JJFServer");

            if (results != null && results.Count > 0)
            {
                colorCodeId = results[0].Id;
            }

            return colorCodeId;

        }


        public static int CheckInteriorColor(string colorCode, int styleId)
        {
            var colorCodeId = 0;

            var results = SqlMapperUtil.StoredProcWithParams<InteriorColorData>("sp_GetInteriorColor", new { ColorCode = colorCode, StyleId = styleId }, "JJFServer");

            if (results != null && results.Count > 0)
            {
                colorCodeId = results[0].Id;
            }

            return colorCodeId;

        }

        public static int CheckFeature(int Id, string key)
        {
            var featureId = 0;

            var results = SqlMapperUtil.StoredProcWithParams<FeatureData>("sp_GetFeature", new { FeatureId = Id, Key = key }, "JJFServer");

            if (results != null && results.Count > 0)
            {
                featureId = results[0].Id;
            }

            return featureId;

        }

        public static int CheckOption(string optionCode, int styleId)
        {
            var optionId = 0;

            var results = SqlMapperUtil.StoredProcWithParams<OptionData>("sp_GetOptionCodeContent", new { OptionCode = optionCode, StyleId = styleId }, "JJFServer");

            if (results != null && results.Count > 0)
            {
                optionId = results[0].Id;
            }

            return optionId;

        }

        public static int CheckPackage(int Id, string key)
        {
            var packageId = 0;

            var results = SqlMapperUtil.StoredProcWithParams<PackageData>("sp_GetPackage", new { PackageId = Id, Key = key }, "JJFServer");

            if (results != null && results.Count > 0)
            {
                packageId = results[0].Id;
            }

            return packageId;

        }

        public static int CheckTechSpec(int Id, string key)
        {
            var techSpecId = 0;

            var results = SqlMapperUtil.StoredProcWithParams<TechSpecData>("sp_GetTechSpec", new { TechSpecId = Id, Key = key }, "JJFServer");

            if (results != null && results.Count > 0)
            {
                techSpecId = results[0].Id;
            }

            return techSpecId;

        }

        public static int CheckVehicle(string vin)
        {
            var vehicleId = 0;

            var results = SqlMapperUtil.StoredProcWithParams<VehicleData>("sp_GetVehicle", new { VIN = vin }, "JJFServer");

            if (results != null && results.Count > 0)
            {
                vehicleId = results[0].Id;
            }

            return vehicleId;

        }

        public static int AddStyle(StyleData styleData)
        {
            var styleKeyId = 0;

            styleKeyId = SqlMapperUtil.InsertUpdateOrDeleteStoredProc("sp_InsertStyle", styleData, "JJFServer");
            
            return styleKeyId;

        }
        public static int AddExteriorColor(ExteriorColorData colorData)
        {
            var colorId = 0;

            var result = SqlMapperUtil.InsertUpdateOrDeleteStoredProc("sp_InsertExteriorColor", colorData, "JJFServer");
            colorId = CheckExteriorColor(colorData.ColorCode, colorData.StyleId);

            return colorId;

        }
        public static int AddInteriorColor(InteriorColorData colorData)
        {
            var colorId = 0;

            var result = SqlMapperUtil.InsertUpdateOrDeleteStoredProc("sp_InsertInteriorColor", colorData, "JJFServer");
            colorId = CheckInteriorColor(colorData.ColorCode, colorData.StyleId);

            return colorId;

        }

        public static int AddVehicle(VehicleData vehicleData)
        {
            var vehicleId = 0;

            var result = SqlMapperUtil.InsertUpdateOrDeleteStoredProc("sp_InsertVehicle", vehicleData, "JJFServer");
            vehicleId = CheckVehicle(vehicleData.VIN);

            return vehicleId;

        }

        public static int AddFeature(FeatureData featureData)
        {
            var featureId = 0;

            var result = SqlMapperUtil.InsertUpdateOrDeleteStoredProc("sp_InsertFeature", featureData, "JJFServer");
            featureId = CheckFeature(featureData.FeatureId, featureData.Key);

            return featureId;

        }

        public static int AddOption(OptionData optionData)
        {
            var optionId = 0;

            var result = SqlMapperUtil.InsertUpdateOrDeleteStoredProc("sp_InsertOptionCode", optionData, "JJFServer");
            optionId = CheckOption(optionData.OptionCode, optionData.StyleId);

            return optionId;

        }

        public static int AddPackage(PackageData packageData)
        {
            var packageId = 0;

            var result = SqlMapperUtil.InsertUpdateOrDeleteStoredProc("sp_InsertPackage", packageData, "JJFServer");
            packageId = CheckPackage(packageData.PackageId, packageData.Key);

            return packageId;

        }

        public static int AddTechSpec(TechSpecData techSpecData)
        {
            var techSpecId = 0;

            var result = SqlMapperUtil.InsertUpdateOrDeleteStoredProc("sp_InsertTechSpec", techSpecData, "JJFServer");
            techSpecId = CheckTechSpec(techSpecData.TechSpecId, techSpecData.Key);

            return techSpecId;

        }

        public static void AddVehicleFeatureMapping(int vehicleId, int featureId)
        {

            var result = SqlMapperUtil.InsertUpdateOrDeleteStoredProc("sp_InsertVehicleFeatureMapping", new { VehicleId = vehicleId, FeatureId = featureId }, "JJFServer");

        }

        public static void AddVehicleOptionMapping(int vehicleId, int optionId)
        {

            var result = SqlMapperUtil.InsertUpdateOrDeleteStoredProc("sp_InsertVehicleOptionMapping", new { VehicleId = vehicleId, OptionId = optionId }, "JJFServer");

        }

        public static void AddVehiclePackageMapping(int vehicleId, int packageId)
        {

            var result = SqlMapperUtil.InsertUpdateOrDeleteStoredProc("sp_InsertVehiclePackageMapping", new { VehicleId = vehicleId, PackageId = packageId }, "JJFServer");

        }

        public static void AddVehicleTechSpecMapping(int vehicleId, int techSpecId)
        {

            var result = SqlMapperUtil.InsertUpdateOrDeleteStoredProc("sp_InsertVehicleTechSpecMapping", new { VehicleId = vehicleId, TechSpecId = techSpecId }, "JJFServer");

        }

        public static bool InsertOrUpdateDealers() //IEnumerable<DealerModel> dealers
        {
            var bSuccess = false;

            //foreach (var dealer in dealers)
            //{
            //    SqlMapperUtil.InsertUpdateOrDeleteSql("INSERT INTO [dbo].[Dealer] VALUES (@DealerID, @DealerName, @DealerBrands, @DealerAddress, @DealerCity, @DealerStateProvince, @DealerPostalCode, @DealerCountry, @RecordStatusCode, @LastUpdatedUTCDate)", 
            //        new { DealerID = dealer.DealerID,
            //            DealerName = dealer.DealerName,
            //            DealerBrands = dealer.DealerBrands,
            //            DealerAddress = dealer.DealerAddress,
            //            DealerCity = dealer.DealerCity,
            //            DealerStateProvince = dealer.DealerStateProvince,
            //            DealerPostalCode = dealer.DealerPostalCode,
            //            DealerCountry = dealer.DealerCountry,
            //            RecordStatusCode = dealer.RecordStatusCode,
            //            LastUpdatedUTCDate = dealer.LastUpdatedUTCDate
            //        }, "JJFServer");
            //}
            return bSuccess;
        }


    }
}