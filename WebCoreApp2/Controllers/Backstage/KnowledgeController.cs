using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CCISWebCore.Models.EF;
using CCISWebCore.Models.ViewModels;
using X.PagedList;
using X.PagedList.Mvc.Core;
using X.PagedList.Mvc.Common;
using Microsoft.AspNetCore.Authorization;

namespace CCISWebCore.Controllers.Backstage
{
    [Authorize]
    public class KnowledgeController : Controller
    {
        private readonly dbCCISCoreContext _context;
        private int pageSize = 10;
        private List<KnowledgeViewModel> knowledgeViewModels = new List<KnowledgeViewModel>();

        public KnowledgeController(dbCCISCoreContext context)
        {
            _context = context;
        }

        // GET: Knowledge
        [HttpGet]
        public async Task<IActionResult> Index(int? page)
        {
            foreach (var item in await _context.Knowledged.OrderBy(k => k.Id).ToListAsync())
            {
                knowledgeViewModels.Add(new KnowledgeViewModel()
                {
                    Id = item.Id,
                    CostYear = item.CostYear,
                    PlanName = item.PlanName,
                    NumberLaw = item.NumberLaw,
                    DefiProjectClass = item.DefiProjectClass,
                    ExecutiveUnit = item.ExecutiveUnit,
                    PlanHost = item.PlanHost,
                    ProjectClass = item.ProjectClass,
                    KeywordTw = item.KeywordTw,
                    SummaryTw = item.SummaryTw,
                    KeywordEng = item.KeywordEng,
                    InfoLink = item.InfoLink

                });
            }
            //撈取 資料表中指定的欄位 
            var costyearSelect = (from c in _context.Knowledged.OrderBy(c => c.Id)
                                  select c.CostYear).ToList().Distinct();
            var numberlawSelect = _context.Knowledged.Where(w => w.NumberLaw != null)
                .Select(a => new
                {
                    index = Convert.ToInt32(a.NumberLaw.Replace("第", "").Replace("條", "")),
                    a.NumberLaw
                }).Distinct()
                .OrderBy(o => o.index);

            List<SelectListItem> CostYear = new List<SelectListItem>();
            List<SelectListItem> NumberLaw = new List<SelectListItem>();
            foreach (var item in costyearSelect)
            {
                CostYear.Add(new SelectListItem { Value = item, Text = item });
            }
            foreach (var item in numberlawSelect)
            {
                NumberLaw.Add(new SelectListItem { Value = item.NumberLaw, Text = item.NumberLaw });
            }
     
            ViewBag.CostYear = new SelectList(CostYear, "Value", "Text");
            ViewBag.Numberlaw = new SelectList(NumberLaw, "Value", "Text");


            //IQueryable<Knowledged> ii = _context.Knowledged.AsNoTracking();
            //ViewBag.PageData =ii.ToPagedList(page ?? 1, pageSize);

            return View(knowledgeViewModels.ToPagedList(page ?? 1, pageSize));
        }

        [HttpGet]
        public ActionResult Query(string Costyear, string Numberlaw, string KeyWord, int page = 1)
        {
            //condition ? ref consequent : ref alternative
            string costyear = Costyear ?? string.Empty;
            string numberlaw = Numberlaw ?? string.Empty;
            string keyword = KeyWord ?? string.Empty;
            ViewBag.year = costyear;
            ViewBag.law = numberlaw;
            ViewBag.keyword = keyword;
            List<KnowledgeViewModel> knowledgeViewModels = new List<KnowledgeViewModel>();
            var list = new List<Knowledged>();
            var knowledgeList = _context.Knowledged.OrderBy(k => k.Id);

            if (numberlaw != string.Empty && costyear != string.Empty && keyword != string.Empty)
            {
                list = knowledgeList.Where(k => k.CostYear == costyear && k.NumberLaw == numberlaw && (k.KeywordTw.Contains(keyword) || k.KeywordEng.Contains(keyword))).ToList();
            }
            else if (numberlaw == string.Empty && costyear == string.Empty && keyword == string.Empty)
            {
                list = knowledgeList.ToList();
            }
            else if (numberlaw != string.Empty && costyear != string.Empty && keyword == string.Empty)
            {
                list = knowledgeList.Where(k => k.CostYear == costyear && k.NumberLaw == numberlaw).ToList();
            }
            else if (numberlaw == string.Empty && costyear != string.Empty && keyword != string.Empty)
            {
                list = knowledgeList.Where(k => k.CostYear == costyear && (k.KeywordTw.Contains(keyword) || k.KeywordEng.Contains(keyword))).ToList();
            }
            else if (numberlaw != string.Empty && costyear == string.Empty && keyword != string.Empty)
            {
                list = knowledgeList.Where(k => k.NumberLaw == numberlaw && (k.KeywordTw.Contains(keyword) || k.KeywordEng.Contains(keyword))).ToList();
            }
            else if (numberlaw != string.Empty && costyear == string.Empty && keyword == string.Empty)
            {
                list = knowledgeList.Where(k => k.NumberLaw == numberlaw).ToList();
            }
            else if (numberlaw == string.Empty && costyear == string.Empty && keyword != string.Empty)
            {
                list = knowledgeList.Where(k => k.KeywordTw.Contains(keyword) || k.KeywordEng.Contains(keyword)).ToList();
            }
            else if (numberlaw == string.Empty && costyear != string.Empty && keyword == string.Empty)
            {
                list = knowledgeList.Where(k => k.CostYear == costyear).ToList();
            }

            for (int i = 0; i < list.Count; i++)
            {
                Knowledged item = list[i];
                knowledgeViewModels.Add(new KnowledgeViewModel()
                {
                    Id = item.Id,
                    CostYear = item.CostYear,
                    PlanName = item.PlanName,
                    NumberLaw = item.NumberLaw,
                    DefiProjectClass = item.DefiProjectClass,
                    ExecutiveUnit = item.ExecutiveUnit,
                    PlanHost = item.PlanHost,
                    ProjectClass = item.ProjectClass,
                    KeywordTw = item.KeywordTw,
                    SummaryTw = item.SummaryTw,
                    KeywordEng = item.KeywordEng,
                    InfoLink = item.InfoLink
                });
            }

            //撈取 資料表中指定的欄位 
            var costyearSelect = (from c in _context.Knowledged.OrderBy(c => c.Id)
                                  select c.CostYear).ToList().Distinct();

            var numberlawSelect = _context.Knowledged.Where(w => w.NumberLaw != null)
                .Select(a => new
                {
                    index = Convert.ToInt32(a.NumberLaw.Replace("第", "").Replace("條", "")),
                    a.NumberLaw
                }).Distinct()
                .OrderBy(o => o.index);
            //新增 下拉選單 物件
            List<SelectListItem> CostYear = new List<SelectListItem>();
            List<SelectListItem> NumberLaw = new List<SelectListItem>();
            //撈取 下拉選單所需的資料內容 並加入下拉選單物件
            foreach (var item in costyearSelect)
            {
                CostYear.Add(new SelectListItem { Value = item, Text = item });
            }
            foreach (var item in numberlawSelect)
            {
                NumberLaw.Add(new SelectListItem { Value = item.NumberLaw, Text = item.NumberLaw });
            }
            //將下拉選單 傳到前端顯示
            ViewBag.CostYear = new SelectList(CostYear, "Value", "Text");
            ViewBag.Numberlaw = new SelectList(NumberLaw, "Value", "Text");
            ViewBag.num = knowledgeViewModels.Count().ToString();

            return View(knowledgeViewModels.ToPagedList(page, pageSize));

        }
        // GET: Knowledge/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var knowledge = await _context.Knowledged
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (knowledge == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(knowledge);
        //}

        // GET: Knowledge/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Knowledge/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create([Bind("Id,CostYear,PlanName,NumberLaw,DefiProjectCalss,ExecutiveUnit,PlanHost,ProjectClass,KeywordTw,SummaryTw,KeywordEng,SummaryEng")] Knowledged knowledge)
        public async Task<IActionResult> Create(KnowledgeViewModel vm_knowledge)
        {
            if (ModelState.IsValid)
            {
                Knowledged knowledgeds = new Knowledged
                {
                    Id = vm_knowledge.Id,
                    CostYear = vm_knowledge.CostYear,
                    PlanName = vm_knowledge.PlanName,
                    NumberLaw = vm_knowledge.NumberLaw,
                    DefiProjectClass = vm_knowledge.DefiProjectClass,
                    ExecutiveUnit = vm_knowledge.ExecutiveUnit,
                    PlanHost = vm_knowledge.PlanHost,
                    ProjectClass = vm_knowledge.ProjectClass,
                    KeywordTw = vm_knowledge.KeywordTw,
                    SummaryTw = vm_knowledge.SummaryTw,
                    KeywordEng = vm_knowledge.KeywordEng,
                    InfoLink = vm_knowledge.InfoLink
                };

                _context.Add(knowledgeds);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm_knowledge);
        }

        // GET: Knowledge/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var knowledge = await _context.Knowledged.FindAsync(id);
            if (knowledge == null)
            {
                return NotFound();
            }

            KnowledgeViewModel vm_knowledge = new KnowledgeViewModel
            {
                Id = knowledge.Id,
                CostYear = knowledge.CostYear,
                PlanName = knowledge.PlanName,
                NumberLaw = knowledge.NumberLaw,
                DefiProjectClass = knowledge.DefiProjectClass,
                ExecutiveUnit = knowledge.ExecutiveUnit,
                PlanHost = knowledge.PlanHost,
                ProjectClass = knowledge.ProjectClass,
                KeywordTw = knowledge.KeywordTw,
                SummaryTw = knowledge.SummaryTw,
                KeywordEng = knowledge.KeywordEng,
                InfoLink = knowledge.InfoLink
            };
            return View(vm_knowledge);
        }

        // POST: Knowledge/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,CostYear,PlanName,NumberLaw,DefiProjectCalss,ExecutiveUnit,PlanHost,ProjectClass,KeywordTw,SummaryTw,KeywordEng,SummaryEng")] Knowledged knowledge)
        public async Task<IActionResult> Edit(int id, KnowledgeViewModel vm_knowledge)
        {


            if (id != vm_knowledge.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    Knowledged knowledgeds = new Knowledged
                    {
                        Id = vm_knowledge.Id,
                        CostYear = vm_knowledge.CostYear,
                        PlanName = vm_knowledge.PlanName,
                        NumberLaw = vm_knowledge.NumberLaw,
                        DefiProjectClass = vm_knowledge.DefiProjectClass,
                        ExecutiveUnit = vm_knowledge.ExecutiveUnit,
                        PlanHost = vm_knowledge.PlanHost,
                        ProjectClass = vm_knowledge.ProjectClass,
                        KeywordTw = vm_knowledge.KeywordTw,
                        SummaryTw = vm_knowledge.SummaryTw,
                        KeywordEng = vm_knowledge.KeywordEng,
                        InfoLink = vm_knowledge.InfoLink

                    };
                    _context.Update(knowledgeds);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KnowledgeExists(vm_knowledge.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vm_knowledge);
        }

        // GET: Knowledge/Delete/5

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var knowledge = await _context.Knowledged.FindAsync(id);
            if (knowledge == null)
            {
                return NotFound();
            }
            //var knowledge = await _context.Knowledged.FindAsync(id);
            _context.Knowledged.Remove(knowledge);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }

        // POST: Knowledge/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var knowledge = await _context.Knowledged.FindAsync(id);
        //    _context.Knowledged.Remove(knowledge);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool KnowledgeExists(int id)
        {
            return _context.Knowledged.Any(e => e.Id == id);
        }
    }
}
