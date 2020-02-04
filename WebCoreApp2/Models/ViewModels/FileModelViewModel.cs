using Microsoft.AspNetCore.Http;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CCISWebCore.Models.ViewModels
{
    /// <summary>
    /// Banner 的摘要描述
    /// </summary>
    public class Banner
    {
        [Display(Name = "key")]
        public string Guid { get; set; }

        [Required(ErrorMessage = "請輸入標題")]
        [Display(Name = "標題")]
        public string Title { get; set; }

        [Display(Name = "H2文字")]
        public string H2 { get; set; }

        [Display(Name = "圖片連結")]
        public string ImgPath { get; set; }

        [Display(Name = "超連結")]       
        public string Href { get; set; }

        [Display(Name = "是否另開視窗")]
        public bool IsBlank { get; set; }

        [Required(ErrorMessage = "請輸入排序")]      
        [Display(Name = "排序")]
        public int? Sort { get; set; }

        [XmlIgnore]
        [Display(Name = "檔案上傳")]            
        public IFormFile File { get; set; }
    }
}
