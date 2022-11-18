﻿using System;
using PublicHoliday;
using PortlandPublishedCalculator;
using PortlandPublishedCalculator.DatabaseQueries;
using PortlandPublishedCalculator.Dates;
using PortlandPublishedCalculator.Calculations;
using PortlandPublishedCalculator.Utility;
using System.Text;
#pragma warning disable CS8321 // Local function is declared but never used.

///----------------- notes on program -----------------///

// This program will calculate the Portland Published Wholesale prices for each of the grades that will be uploaded in the y_published_wholesale table of the database.
// The grades that are currently being calculated are: diesel_cif_nwe, fame-10, unleaded_cif_nwe, ethanol_eur_cbm, jet_cif_nwe, propane_cif_nwe

///----------------- notes on program -----------------///


// Calculates and uploads the Portland Published Wholesale prices for every working day inbetween and including the 'startDate' and 'endDate' variables.
// Good for testing purposes only. Not intended to be used for daily automation of the program.
static void BetweenTwoDates()
{
    //yy - mm - dd format:
    DateOnly startDate = new(2022, 07, 15);
    DateOnly endDate = new(2022, 11, 16);

    DateOnly date = Date.WorkingDayCheck(startDate);
    while (date <= endDate)
    {
        //// Calculates the Portland Diesel CIF NWE for a given date. 
        //double? diesel_price = Calculations.Portland_Diesel_CIF_NWE(date);
        //if (diesel_price.HasValue)
        //{
        //    Console.WriteLine("The Portland Diesel Price for " + date + " is " + diesel_price);
        //    //UploadToDB.YPublishedWholesale(date, diesel_price, "diesel"); //Uploads the price to the database.
        //}

        //// Calculates the Portland FAME-10 price for a given date.
        //double? portland_fame_price = Calculations.Portland_FAME_minus_ten(date);
        //if (portland_fame_price.HasValue)
        //{
        //    Console.WriteLine("The Portland FAME-10 Price for " + date + " is " + portland_fame_price);
        //    //UploadToDB.YPublishedWholesale(date, portland_fame_price, "fame");
        //}

        //// Calculates the Portland Unleaded CIF NWE price for a given date.
        //double? portland_unleaded_petrol = Calculations.Portland_Unleaded_CIF_NWE(date);
        //if (portland_unleaded_petrol.HasValue)
        //{
        //    Console.WriteLine("The Portland Unleaded CIF NWE Price for " + date + " is " + portland_unleaded_petrol);
        //    //UploadToDB.YPublishedWholesale(date, portland_unleaded_petrol, "petrol");
        //}

        // Calculates the Portland Ethanol EUR CBM price for a given date.
        double? portland_ethanol_eur_cbm = Calculations.Portland_Ethanol_EUR_CBM(date);
        if (portland_ethanol_eur_cbm.HasValue)
        {
            Console.WriteLine("The Portland Ethanol EUR CBM Price for " + date + " is " + portland_ethanol_eur_cbm);
            // UploadToDB.YPublishedWholesale(date, portland_ethanol_eur_cbm, "ethanol");
        }

        //// Calculates the Portland Jet CIF NWE price for a given date.
        //double? portland_jet_cif_nwe = Calculations.Portland_Jet_CIF_NWE(date);
        //if (portland_jet_cif_nwe.HasValue)
        //{
        //    Console.WriteLine("The Portland Jet CIF NWE Price for " + date + " is " + portland_jet_cif_nwe);
        //    //UploadToDB.YPublishedWholesale(date, portland_jet_cif_nwe, "jet");
        //}

        //// Calculates the Portland Propane CIF NWE price for a given date.
        //double? portland_propane_cif_nwe = Calculations.Portland_Propane_CIF_NWE(date);
        //if (portland_propane_cif_nwe.HasValue)
        //{
        //    Console.WriteLine("The Portland Propane CIF NWE Price for " + date + " is " + portland_propane_cif_nwe);
        //    //UploadToDB.YPublishedWholesale(date, portland_propane_cif_nwe, "propane");
        //}

        date = Date.NextWorkingDay(date); //Goes to the following working day. 
    }
}

// Calculates and uploads the previous working days Portland Published Wholesale prices for each of the grades.
// To be used for daily running of the program. 
static void CalculatePrices()
{
    DateOnly currentDate = DateOnly.FromDateTime(DateTime.UtcNow);
    DateOnly previousWorkingDay = Date.PreviousWorkingDay(currentDate);
    StringBuilderPlusConsole.WriteLine("The Portland Published Prices for " + previousWorkingDay + " are:");
    StringBuilderPlusConsole.WriteLineSBOnly("<br>");

    GradesEnum.Grade[] grades = GradesEnum.GetAllGradesAsArray();
    bool ErrorChecker = false;
    foreach (var grade in grades)
    {
        string gradename = GradesEnum.GetGradeName(grade);
        double? price = Calculated.Price(grade, previousWorkingDay);
        if (price.HasValue)
        {
            StringBuilderPlusConsole.GradeAppend(gradename, price);
            UploadToDB.YPublishedWholesale(previousWorkingDay, price, grade.ToString()); //Uploads the price to the database.
        } else { StringBuilderPlusConsole.WriteLine(gradename + ": ERROR - Cannot generate a value."); ErrorChecker = true; }
    }

    // Sends an email of all the generated prices that have been added to the StringBuilder string.
    Email.SendEmail(ErrorChecker);
}

CalculatePrices();