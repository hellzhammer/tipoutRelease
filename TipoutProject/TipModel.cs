﻿using System;

namespace TipoutProject
{
    public class TipModel
    {
        public string ID { get; set; }
        public double TipAmount { get; set; }
        public double TipoutAmount { get; set; }
        public string Workplace { get; set; }
        public DateTime ShiftDate { get; set; }
        public double TipPercentage { get; set; }
        public string ownerID { get; set; }
    }
}
