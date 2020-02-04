using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CCISWebCore.Models.EF;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CCISWebCore.Models.ViewModels
{
    
    public class KnowledgeViewModel
    {
        
        public int Id { get; set; }
        [Display(Name ="執行年度")]
       [Required(ErrorMessage = "請輸入執行年度")]
        public string CostYear { get; set; }
        [Display(Name ="計畫名稱")]
        [Required(ErrorMessage = "請輸入計畫名稱")]
        public string PlanName { get; set; }
        [Display(Name ="溫管法")]
        [Required(ErrorMessage = "請輸入溫管法")]
        public string NumberLaw { get; set; }
        [Display(Name ="自定專案分類")]
        [Required(ErrorMessage = "請輸入自定專案分類")]
        public string DefiProjectClass { get; set; }
        [Display(Name ="執行單位")]
        [Required(ErrorMessage = "請輸入執行單位")]
        public string ExecutiveUnit { get; set; }
        [Display(Name = "專案主持人")]
        [Required(ErrorMessage = "請輸入專案主持人")]
        public string PlanHost { get; set; }
        [Display(Name = "專案分類")]
        [Required(ErrorMessage = "請輸入專案分類")]
        public string ProjectClass { get; set; }
        [Display(Name ="中文關鍵字")]
        public string KeywordTw { get; set; }
        [Display(Name ="中文摘要")]
        [Required(ErrorMessage = "請輸入中文摘要")]
        public string SummaryTw { get; set; }
        [Display(Name ="英文關鍵字")]
        public string KeywordEng { get; set; }
        [Display(Name ="下載連結")]
        [Required(ErrorMessage = "請輸入下載連結")]
        public string InfoLink { get; set; }

    }
}
