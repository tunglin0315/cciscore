using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using CCISWebCore.Models.EF;

namespace CCISWebCore.Models.ViewModels
{
    public class InfoListViewModel
    {
        public IEnumerable<Infos> Infos { get; set; }
        public PagingInfo PagingInfo { get; set; }
        //public string CurrentCategory { get; set; }
    }

    public class InfoEditViewModel
    {
        [Required]
        public int ID { get; set; }
        [Required(ErrorMessage = "請輸入標題")]
        [Display(Name ="標題")]
        public string Title { get; set; }
        [Display(Name = "對應路徑")]
        public string HtmlUrl { get; set; }
        [Display(Name = "新聞類別")]
        public short? DType { get; set; }
        [Required(ErrorMessage = "請輸入內容")]
        [Display(Name = "內容")]
        public string HtmlContent { get; set; }
        [Display(Name = "建立時間")]
        public DateTime? CreateDate { get; set; }
        [Display(Name = "編輯時間")]
        public DateTime? ModifyDate { get; set; }
        [Display(Name = "發布時間")]
        public DateTime? PublishDate { get; set; }
        [Display(Name = "下架時間")]       
        public DateTime? OffDateTime { get; set; }
        [Display(Name = "標籤")]
        public ICollection<InfoTag> InfoTag { get; set; }
    }
}
