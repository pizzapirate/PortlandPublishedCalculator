﻿using System;
using System.Collections.Generic;

namespace PortlandPublishedCalculator.Prices
{
    public partial class YPublishedWholesale
    {
        public DateOnly PublishedDate { get; set; }
        public double? DieselCifNwe { get; set; }
        public double? Fame10 { get; set; }
        public double? UnleadedCifNwe { get; set; }
        public double? EthanolEurCbm { get; set; }
        public double? JetCifNwe { get; set; }
        public double? PropaneCifNwe { get; set; }
    }
}