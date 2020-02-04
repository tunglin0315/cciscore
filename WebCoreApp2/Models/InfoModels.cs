using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCISWebCore.Models.EF;

namespace CCISWebCore.Models
{
    //public class Info
    //{
    //    public int ID { get; set; }
    //    public string Title { get; set; }
    //    public short DType { get; set; }
    //    public string HtmlContent { get; set; }
    //    public DateTime CreateDate { get; set; }
    //    public DateTime ModifyDate { get; set; }
    //    public DateTime PublishDate { get; set; }
    //    public DateTime OffDateTime { get; set; }
    //}

    //public interface IInfoRepository
    //{
    //    IQueryable<Info> List { get; }
    //    Info Single(int id);
    //    void Save(Info e);
    //    void Delete(int infoid);

    //}

    /// <summary>
    /// 測試用資料
    /// </summary>
    //public class FakeInfoRepository/* : IInfoRepository*/
    //{
    //    public IQueryable<Info> Infos => new List<Info> {
    //        new Info { ID=1, Title = "標題一", DType = 1,HtmlContent="內容一",CreateDate=DateTime.Now,PublishDate=DateTime.Now},
    //        new Info { ID=2, Title = "標題二", DType = 2,HtmlContent="內容二",CreateDate=DateTime.Now,PublishDate=DateTime.Now},
    //        new Info { ID=3, Title = "標題三", DType = 3,HtmlContent="內容三",CreateDate=DateTime.Now,PublishDate=DateTime.Now},
    //    }.AsQueryable();
    //}

    public class EFInfoRepository /*: IInfoRepository*/
    {
        private dbCCISCoreContext context;
        public EFInfoRepository(dbCCISCoreContext ctx) { context = ctx; }
        public IQueryable<Infos> List => context.Infos;

        public Infos Single(int id)
        {
            return context.Infos.Single(a => a.Id == id);
        }

        public void Save(Infos data)
        {
            DateTime today = DateTime.Now;
            //新增
            if (data.Id == 0)
            {
                data.CreateDate = today;
                data.ModifyDate = today;
                context.Infos.Add(data);
            }
            else
            {
                //編輯
                Infos dbEntry =
                    context.Infos.FirstOrDefault(p => p.Id == data.Id);
                if (dbEntry != null)
                {
                    dbEntry.Title = data.Title;
                    dbEntry.PublishDate = data.PublishDate;
                    dbEntry.OffDateTime = data.OffDateTime;
                    dbEntry.HtmlContent = data.HtmlContent;
                    dbEntry.ModifyDate = today;
                }
            }
            context.SaveChanges();
        }
        public void Delete(int infoid)
        {
            var Info = context.Set<Infos>();
            Info.Remove(Single(infoid));
            context.SaveChanges();
        }
    }
}