using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CCISWebCore.Models.ViewModels
{
    public class HomeActivityViewModel
    {
        public int Id { get; set; }
        //[Required]
        [Display(Name="會議活動Banner")]
        public string ActivityBanner { get; set; }
        //[Required]
        [Display(Name ="無障礙文字敘述")]
        public string AccessibilityTips { get; set; }
        //[Required]
        [Display(Name ="內容連結")]
        public string ActivityUrl { get; set; }
        [Display(Name ="排序")]
        public byte ActivityOrder { get; set; }
        //[Required]
        [Display(Name ="活動起始日期")]
        public DateTime ActivityStartDate { get; set; }
        //[Required]
        [Display (Name="活動結束日期")]
        public DateTime ActivityEndDate { get; set; }
        [Display(Name ="活動狀態")]
        public string ActivityStatus { get; set; }
        [Display(Name ="主辦單位")]
        public string HostUnit { get; set; }
        [Display(Name ="指導單位")]
        public string AssistUnit { get; set; }
        [Display (Name ="時效起始日期")]
        public DateTime? BeginDate { get; set; }
        [Display(Name ="時效結束日期")]
        public DateTime OffDate { get; set; }
        [Display(Name ="創建者")]
        public string CreateUser { get; set; }
        [Display(Name ="創建時間")]
        public DateTime? CreateDate { get; set; }
        [Display(Name = "最後更新者")]
        public string LastModifyUser { get; set; }
        [Display(Name = "最後更新日期")]
        public DateTime? LastModifyDate { get; set; }
        [Display(Name ="會議活動Banner")]
        public IFormFile files { get; set; }
    }
}
