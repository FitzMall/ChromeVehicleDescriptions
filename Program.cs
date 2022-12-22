using ChromeVehicleDescriptions.Models;
using ChromeVehicleDescriptions.Business;
using System;
using System.Linq;

namespace ChromeVehicleDescriptions
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var errorMessage = "";
                var vehicles = SQLQueries.GetInStockVehicles();

                Console.WriteLine(vehicles.Count() + " Found, starting import...");
                var index = 0;
                foreach (var vehicle in vehicles)
                {
                    index += 1;
                    Console.WriteLine("Vehicle " + index + " of " + vehicles.Count());

                    var vehicleId = SQLQueries.CheckVehicle(vehicle.VIN);

                    if (vehicleId == 0)
                    {
                        var vehicleDescription = ChromeVehicleDescriptions.Business.APIHelper.GetVehicleDescription(vehicle.VIN);
                        AddVehicle(vehicleDescription, vehicle.StockNumber, vehicle.XrefId);
                    }
                }

                //var vehicleDescription = ChromeVehicleDescriptions.Business.APIHelper.GetVehicleDescription("1G6AU5S8XE0143881");
                //AddVehicle(vehicleDescription, "MV43881", "1G6AU5S8XE0143881");

            }
            catch (Exception ex)
            {
                var x = ex.Message;
            }
        }

        public static void AddVehicle(ChromeVehicleDescriptionModel vehicleDescription, string stockNumber, string xrefId)
        {
            var vehicleData = MapVehicleData(vehicleDescription, stockNumber, xrefId);


        }

        public static VehicleDataModel MapVehicleData(ChromeVehicleDescriptionModel vehicleDescription, string stockNumber, string xrefId)
        {
            var vehicleDataModel = new VehicleDataModel();
            
            if(vehicleDescription.result != null)
            {
                var styleData = new StyleData();
                var vehicleData = new VehicleData();
                var exteriorColorData = new ExteriorColorData();
                var interiorColorData = new InteriorColorData();

                //MAP AND ADD STYLE
                if (vehicleDescription.result.vehicles != null && vehicleDescription.result.vehicles.Count() == 1)
                {
                    try
                    {
                         
                        var styleDescription = vehicleDescription.result.vehicles.First();

                        styleData.BaseMSRP = styleDescription.baseMSRP;
                        styleData.BodyType = styleDescription.bodyType;
                        styleData.Country = styleDescription.country;
                        styleData.DestinationCharge = styleDescription.destinationCharge;
                        styleData.Doors = styleDescription.doors;
                        styleData.DriveType = styleDescription.driveType;
                        styleData.MFRModelCode = styleDescription.mfrModelCode;
                        if (styleDescription.segment != null && styleDescription.segment.Length > 0)
                        {
                            styleData.Segment = styleDescription.segment[0];
                        }
                        styleData.StandardCurbWeight = styleDescription.standardCurbWeight;
                        styleData.StandardGVWR = styleDescription.standardGVWR;
                        styleData.StandardTowingCapacity = styleDescription.standardTowingCapacity;
                        styleData.StyleDescription = styleDescription.styleDescription;
                        styleData.StyleId = styleDescription.styleId;
                        styleData.Trim = styleDescription.trim;
                        styleData.Wheelbase = styleDescription.wheelbase;

                        //SQLQueries.AddStyle(styleData);

                        //Check to see if the style exists
                        if (!SQLQueries.CheckStyle(styleData.StyleId))
                        {
                            // Style does not exist, add it
                            SQLQueries.AddStyle(styleData);
                        }
                    }
                    catch (Exception ex)
                    {
                        var x = ex.Message;
                    }
                }

                //MAP AND ADD EXTERIOR COLOR
                if(vehicleDescription.result.exteriorColors != null && vehicleDescription.result.exteriorColors.Count() > 0)
                {
                    try
                    { 
                        exteriorColorData.ColorCode = vehicleDescription.result.exteriorColors.First().colorCode;
                        exteriorColorData.Description = vehicleDescription.result.exteriorColors.First().description;
                        exteriorColorData.GenericDescription = vehicleDescription.result.exteriorColors.First().genericDesc;
                        exteriorColorData.InstallCause = vehicleDescription.result.exteriorColors.First().installCause;
                        exteriorColorData.Primary = vehicleDescription.result.exteriorColors.First().primary;
                        exteriorColorData.RGBHexValue = vehicleDescription.result.exteriorColors.First().rgbHexValue;
                        exteriorColorData.RGBValue = vehicleDescription.result.exteriorColors.First().rgbValue;
                        if (vehicleDescription.result.exteriorColors.First().styles.Length > 0)
                        {
                            exteriorColorData.StyleId = vehicleDescription.result.exteriorColors.First().styles[0];
                        }
                    
                        exteriorColorData.Type = vehicleDescription.result.exteriorColors.First().type;

                        vehicleData.ExteriorColorId = SQLQueries.AddExteriorColor(exteriorColorData);

                        //Check to see if the color exists
                        var colorCodeId = SQLQueries.CheckExteriorColor(exteriorColorData.ColorCode, styleData.StyleId);
                        if (colorCodeId > 0)
                            vehicleData.ExteriorColorId = colorCodeId;
                        else
                        {
                            // Color does not exist, add it
                            vehicleData.ExteriorColorId = SQLQueries.AddExteriorColor(exteriorColorData);
                        }
                    }
                    catch (Exception ex)
                    {
                        var x = ex.Message;
                    }
                }
                //MAP AND ADD INTERIOR COLOR
                if (vehicleDescription.result.interiorColors != null && vehicleDescription.result.interiorColors.Count() > 0)
                {
                    try
                    { 
                        interiorColorData.ColorCode = vehicleDescription.result.interiorColors.First().colorCode;
                        interiorColorData.Description = vehicleDescription.result.interiorColors.First().description;
                        interiorColorData.GenericDescription = vehicleDescription.result.interiorColors.First().genericDesc;
                        interiorColorData.InstallCause = vehicleDescription.result.interiorColors.First().installCause;
                        if (vehicleDescription.result.interiorColors.First().styles.Length > 0)
                        {
                            interiorColorData.StyleId = vehicleDescription.result.interiorColors.First().styles[0];
                        }

                        vehicleData.InteriorColorId = SQLQueries.AddInteriorColor(interiorColorData);

                        //Check to see if the color exists
                        var colorCodeId = SQLQueries.CheckInteriorColor(interiorColorData.ColorCode, styleData.StyleId);
                        if (colorCodeId > 0)
                            vehicleData.InteriorColorId = colorCodeId;
                        else
                        {
                            // Color does not exist, add it
                            vehicleData.InteriorColorId = SQLQueries.AddInteriorColor(interiorColorData);

                        }
                    }
                    catch (Exception ex)
                    {
                        var x = ex.Message;
                    }
                }

                //MAP AND ADD VEHICLE
                try
                { 
                    if (vehicleDescription.result.buildDate.ToShortDateString() == "1/1/0001")
                    {
                        vehicleData.BuildDate = new DateTime(1900, 1, 1);
                    }
                    else
                    {
                        vehicleData.BuildDate = vehicleDescription.result.buildDate;
                    }
                
                    vehicleData.BuildMSRP = vehicleDescription.result.buildMSRP;
                    vehicleData.BuildSource = vehicleDescription.result.buildSource;
                    vehicleData.Country = vehicleDescription.result.wmiCountry;
                    //vehicleData.ExteriorColorId = "";
                    //vehicleData.InteriorColorId = "";
                    vehicleData.Make = vehicleDescription.result.make;
                    vehicleData.Manufacturer = vehicleDescription.result.wmiManufacturer;
                    vehicleData.Model = vehicleDescription.result.model;
                    vehicleData.ModelId = vehicleDescription.result.modelID;
                    vehicleData.Source = vehicleDescription.result.source;
                    vehicleData.StockNumber = stockNumber;
                    vehicleData.StyleId = styleData.StyleId;
                    vehicleData.VIN = vehicleDescription.result.vinProcessed;
                    vehicleData.XrefId = xrefId;
                    vehicleData.Year = vehicleDescription.result.year;
                    vehicleData.DateUpdated = DateTime.Now;

                    vehicleData.ManagerSpecialEndDate = new DateTime(1900, 1, 1);
                    vehicleData.ManagerSpecialStartDate = new DateTime(1900, 1, 1);
                    vehicleData.OptionsApprovedDate = new DateTime(1900, 1, 1);

                    //vehicleData.Id = SQLQueries.AddVehicle(vehicleData);
                    // Check to see if the vehicle exists
                    var vehicleId = SQLQueries.CheckVehicle(vehicleData.VIN);
                    if (vehicleId > 0)
                        vehicleData.Id = vehicleId;
                    else
                    {
                        // Vehicle does not exist, add it
                        vehicleData.Id = SQLQueries.AddVehicle(vehicleData);

                    }
                }
                catch (Exception ex)
                {
                    var x = ex.Message;
                }

                //MAP AND ADD FEATURES
                if ((vehicleDescription.result.features != null && vehicleDescription.result.features.Count() > 0))
                {
                    foreach(var feature in vehicleDescription.result.features)
                    {
                        try
                        { 
                            var featureData = new FeatureData();

                            if(feature.benefitStatement != null && feature.benefitStatement.Count() > 0)
                            {
                                featureData.BenefitDefinition = feature.benefitStatement.First().definition;
                                featureData.BenefitStatement = feature.benefitStatement.First().statement;
                                featureData.BenefitTitle = feature.benefitStatement.First().title;
                            }
                                                 
                            featureData.Description = feature.description;
                            featureData.FeatureId = feature.id;
                            featureData.Key = feature.key;
                            featureData.Name = feature.name;
                            featureData.NameNoBrand = feature.nameNoBrand;
                            featureData.RankingValue = feature.rankingValue;
                            featureData.SectionId = feature.sectionId;
                            featureData.SectionName = feature.sectionName;
                            if (feature.styles.First().styleIds.Length > 0)
                            {
                                featureData.StyleId = feature.styles.First().styleIds[0];
                            }
                            featureData.InstallCause = feature.styles.First().installCause;
                            featureData.IsStandard = feature.styles.First().isStandard;

                            featureData.SubSectionId = feature.subSectionId;

                            // Check to see if the feature exists
                            var featureId = SQLQueries.CheckFeature(featureData.FeatureId, featureData.Key);
                            if (featureId > 0)
                                featureData.Id = featureId;
                            else
                            {
                                // Feature does not exist, add it
                                featureData.Id = SQLQueries.AddFeature(featureData);
                            }

                            SQLQueries.AddVehicleFeatureMapping(vehicleData.Id, featureData.Id);
                        }
                        catch (Exception ex)
                        {
                            var x = ex.Message;
                        }
                    }
                }

                //MAP AND ADD PACKAGES
                if ((vehicleDescription.result.packages != null && vehicleDescription.result.packages.Count() > 0))
                {
                    foreach (var package in vehicleDescription.result.packages)
                    {
                        try
                        { 
                            var packageData = new PackageData();
                        
                            packageData.Description = package.description;
                            packageData.Key = package.key;
                            packageData.PackageId = package.id;
                            packageData.Name = package.name;
                            packageData.NameNoBrand = package.nameNoBrand;
                            packageData.SectionId = package.sectionId;
                            packageData.SectionName = package.sectionName;
                            packageData.SubSectionId = package.subSectionId;
                            packageData.RankingValue = package.rankingValue;

                            if (package.optionDetails.Count() > 0)
                            {
                                packageData.OptionCode = package.optionDetails.First().optionCode;
                                packageData.MSRP = package.optionDetails.First().msrp;

                                if (package.optionDetails.First().content != null)
                                {
                                    packageData.Content = package.optionDetails.First().content.ToString();
                                }
                            }

                            if (package.styles.First().styleIds.Length > 0)
                            {
                                packageData.StyleId = package.styles.First().styleIds[0];
                            }
                            packageData.InstallCause = package.styles.First().installCause;
                            packageData.IsStandard = package.styles.First().isStandard;

                            // Check to see if the feature exists
                            var packageId = SQLQueries.CheckPackage(packageData.PackageId, packageData.Key);
                            if (packageId > 0)
                                packageData.Id = packageId;
                            else
                            {
                                // Feature does not exist, add it
                                packageData.Id = SQLQueries.AddPackage(packageData);
                            }


                            SQLQueries.AddVehiclePackageMapping(vehicleData.Id, packageData.Id);
                        }
                        catch (Exception ex)
                        {
                            var x = ex.Message;
                        }
                    }
                }

                //MAP AND ADD OPTIONS
                if (vehicleDescription.result.optionCodes != null && vehicleDescription.result.optionCodes.Count() > 0)
                {
                    //foreach(var option in vehicleDescription.result.optionCodes)
                    //{
                    //    
                    //    optionData.OptionCode = option;

                    //}

                    foreach(var optionContent in vehicleDescription.result.optionCodeContent)
                    {
                        try
                        { 
                            var optionData = new OptionData();

                            optionData.StyleId = vehicleData.StyleId;
                            optionData.InstallCause = optionContent.installCause;
                            optionData.MSRP = optionContent.msrp;
                            optionData.OptionCode = optionContent.optionCode;
                            optionData.OptionDescription = optionContent.optionDescription;

                            // Check to see if the feature exists
                            var optionId = SQLQueries.CheckOption(optionData.OptionCode, optionData.StyleId);
                            if (optionId > 0)
                                optionData.Id = optionId;
                            else
                            {
                                // Feature does not exist, add it
                                optionData.Id = SQLQueries.AddOption(optionData);
                            }

                            SQLQueries.AddVehicleOptionMapping(vehicleData.Id, optionData.Id);
                        }
                        catch (Exception ex)
                        {
                            var x = ex.Message;
                        }
                    }
                }
                //MAP AND ADD TECHSPECS
                if ((vehicleDescription.result.techSpecs != null && vehicleDescription.result.techSpecs.Count() > 0))
                {
                    foreach (var techspec in vehicleDescription.result.techSpecs)
                    {
                        try
                        { 
                            var techSpecData = new TechSpecData();

                            techSpecData.Description = techspec.description;
                            techSpecData.TechSpecId = techspec.id;
                            techSpecData.Key = techspec.key;
                            techSpecData.Name = techspec.name;
                            techSpecData.NameNoBrand = techspec.nameNoBrand;
                            techSpecData.RankingValue = techspec.rankingValue;
                            techSpecData.SectionId = techspec.sectionId;
                            techSpecData.SectionName = techspec.sectionName;
                            if (techspec.styles.First().styleIds.Length > 0)
                            {
                                techSpecData.StyleId = techspec.styles.First().styleIds[0];
                            }
                            techSpecData.InstallCause = techspec.styles.First().installCause;
                            techSpecData.IsStandard = techspec.styles.First().isStandard;

                            techSpecData.SubSectionId = techspec.subSectionId;

                            // Check to see if the feature exists
                            var techSpecId = SQLQueries.CheckTechSpec(techSpecData.TechSpecId, techSpecData.Key);
                            if (techSpecId > 0)
                                techSpecData.Id = techSpecId;
                            else
                            {
                                // Feature does not exist, add it
                                techSpecData.Id = SQLQueries.AddTechSpec(techSpecData);
                            }

                            SQLQueries.AddVehicleTechSpecMapping(vehicleData.Id, techSpecData.Id);
                        }
                        catch (Exception ex)
                        {
                            var x = ex.Message;
                        }
                    }
                }
            }

            return vehicleDataModel;
        }

    }
}
