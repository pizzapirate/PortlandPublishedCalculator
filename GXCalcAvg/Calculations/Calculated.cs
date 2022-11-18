﻿using PortlandPublishedCalculator.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PortlandPublishedCalculator.Utility.GradesEnum;

namespace PortlandPublishedCalculator.Calculations
{
    public static class Calculated
    {
        // A switch statement used to retrieve the Portland Published price for the grades
        public static double? Price(Grade grade, DateOnly date) => (grade) switch
        {
            Grade.diesel => Calculations.Portland_Diesel_CIF_NWE(date),
            Grade.fame => Calculations.Portland_FAME_minus_ten(date),
            Grade.petrol => Calculations.Portland_Unleaded_CIF_NWE(date),
            Grade.ethanol => Calculations.Portland_Ethanol_EUR_CBM(date),
            Grade.jet => Calculations.Portland_Jet_CIF_NWE(date),
            Grade.propane => Calculations.Portland_Propane_CIF_NWE(date),
            _ => null,
        };
    }
}