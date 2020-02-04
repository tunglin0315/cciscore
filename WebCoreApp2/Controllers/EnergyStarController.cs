using CCISWebCore.Models.EF;
using CCISWebCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CCISWebCore.Controllers
{
    public class EnergyStarController : Controller
    {
        private readonly IConfiguration _config;
        private readonly dbCCISCoreContext _context;

        public EnergyStarController(IConfiguration config, dbCCISCoreContext context)
        {
            this._config = config;
            _context = context;

        }

        // 能源之星
        public IActionResult Index()
        {
            return View();
        }

        //能源之星標章計畫
        public IActionResult Plan()
        {
            ViewBag.date = DateTime.Now.AddDays(-3).ToShortDateString();
    
            return View();
        }

        //能源之星標章申請
        public IActionResult Apply()
        {
            ViewBag.date = DateTime.Now.AddDays(-3).ToShortDateString();
            return View();
        }

        //能源標章產品資訊
        public IActionResult Infomation()
        {
            return View();
        }
        //常見問答
        public IActionResult Question()
        {
            ViewBag.date = DateTime.Now.AddDays(-7).ToShortDateString();
            return View();
        }

        public IActionResult Partner()
        {
            return View();
        }

        public IActionResult Corporation(int? id)
        {
            var _v = _context.EnergyStarCriteria;
            ViewBag.Contact = _v;

            if (id == null)
            {
                return View(_context.EnergyStarCorporation);
            }

            var _criterias =
                _context.EnergyStarCriteria
                .Include(d => d.EnergyStarProduct)
                .ThenInclude(d => d.IPdtCorporation)
                .AsNoTracking()
                .FirstOrDefault(m => m.IdCrt == id);

            var _d =
                _criterias
                .EnergyStarProduct
                .Select(a => a.IPdtCorporation).Distinct();

            ViewBag.ContactName = _criterias.VrCrtName;
            return View(_d);
        }

        public IActionResult CorporationDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var _v =
                _context.EnergyStarCorporation
                .Include(d => d.EnergyStarProduct)
                .AsNoTracking()
                .FirstOrDefault(m => m.IdCop == id);

            if (_v == null)
            {
                return NotFound();
            }

            return View(_v);
        }

        public IActionResult ProductDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var _v =
                _context.EnergyStarProduct
                .Include(d => d.IPdtCorporation)
                .Include(d => d.IdContactsNavigation)
                .Include(d => d.IPdtCriteria)
                .Include(d => d.EnergyStarProductSpec)
                .AsNoTracking()
                .FirstOrDefault(m => m.IdPdt == id);

            if (_v == null)
            {
                return NotFound();
            }

            return View(_v);
        }

        public IActionResult Search()
        {
            SelectListData();
            return View();
        }

        [HttpPost]
        public IActionResult Search(string Stxt, string[] selectedTags)
        {
            SelectListData(selectedTags);

            var _p = _context.EnergyStarProduct
                .AsQueryable();

            //檢索詞
            if (!string.IsNullOrWhiteSpace(Stxt))
            {
                ViewData["Stxt"] = Stxt;
                _p = _p.Where(s => s.VrPdtName.Contains(Stxt));
            }

            //產品類別
            if (selectedTags.Any())
            {
                _p = _p.Where(s => selectedTags.Contains(s.IPdtCriteriaId.ToString()));
            }

            return View(_p.AsNoTracking().ToList());
        }

        private void SelectListData(string[] selectedTags = null)
        {
            var _vs = _context.EnergyStarCriteria.AsQueryable();
            var _st =
                selectedTags == null ?
                new HashSet<int>() : new HashSet<int>(selectedTags.Select(c => Convert.ToInt32(c)));
            var viewModel = new List<SelectListViewModel>();
            foreach (var _v in _vs)
            {
                viewModel.Add(new SelectListViewModel
                {
                    Id = _v.IdCrt,
                    Name = _v.VrCrtName,
                    Assigned = _st.Contains(_v.IdCrt)
                });
            }
            ViewBag.Contact = viewModel;
        }
    }
}