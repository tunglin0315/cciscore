﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace CCISWebCore.Models.EF
{
    public partial class InsideActivity
    {
        public int Id { get; set; }
        public string ActivityBanner { get; set; }
        public string ActivityMsg { get; set; }
        public string ActivityUrl { get; set; }
        public byte ActivityOrder { get; set; }
        public string AccessibilityTips { get; set; }
        public bool IsLayout { get; set; }
        public DateTime? ActivityStartDate { get; set; }
        public DateTime? ActivityEndDate { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime OffDate { get; set; }
        public string CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string LastModifyUser { get; set; }
        public DateTime? LastModifyDate { get; set; }
        public string FirstModifyUser { get; set; }
        public DateTime? FirstModifyDate { get; set; }
        public string SecondModifyUser { get; set; }
        public DateTime? SecondModifyDate { get; set; }
        public string ThirdModifyUser { get; set; }
        public DateTime? ThirdModifyDate { get; set; }
    }
}