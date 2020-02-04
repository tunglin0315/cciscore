using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CCISWebCore.Models;
using CCISWebCore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using CCISWebCore.Models.EF;

namespace CCISWebCore.Controllers.Backstage
{
    [Authorize]
    public class InfoController : Controller
    {
        private dbCCISCoreContext context;
        public int PageSize = 10;
        public InfoController(dbCCISCoreContext _context)
        {
            context = _context;
        }

        public ViewResult List(int id = 1)
            => View(new InfoListViewModel
            {
                Infos = context.Infos
                .OrderBy(p => p.Id)
                .Skip((id - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = id,
                    ItemsPerPage = PageSize,
                    TotalItems = context.Infos.Count()
                }
            });

        /// <summary>
        /// 編輯顯示
        /// </summary>
        /// <param name="id">若id為0視為新增</param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id = 0)
        {            
            DateTime _now = DateTime.Now;
            InfoEditViewModel vm = new InfoEditViewModel()
            {
                ID = id,
                CreateDate = _now,
                ModifyDate = _now,
                PublishDate = _now,
            };

            Infos Info = null;
            if (id != 0)
            {
                Info =
                    await context.Infos
                    .Include(s => s.InfoTag)
                    .ThenInclude(e => e.Tag)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (Info == null)
                {
                    return NotFound();
                }

                vm.ID = Info.Id;
                vm.Title = Info.Title;
                vm.HtmlUrl = Info.HtmlUrl;
                vm.DType = Info.Dtype;
                vm.HtmlContent = Info.HtmlContent;
                vm.CreateDate = Info.CreateDate;
                vm.ModifyDate = Info.ModifyDate;
                vm.PublishDate = Info.PublishDate;
                vm.OffDateTime = Info.OffDateTime;
                vm.InfoTag = Info.InfoTag;
            }

            //Infos Info = repository.Infos.Find(id);
            PopulateAssignedTagData(Info);
            return View(vm);
        }

        //載入標籤
        private void PopulateAssignedTagData(Infos info)
        {
            var Tag = context.Tag;
            var infoTag =
                info == null ?
                new HashSet<int>() : new HashSet<int>(info.InfoTag.Select(c => c.TagId));
            var viewModel = new List<SelectListViewModel>();
            foreach (var _tag in Tag)
            {
                viewModel.Add(new SelectListViewModel
                {
                    Id = _tag.Id,
                    Name = _tag.TagName,
                    Assigned = infoTag.Contains(_tag.Id)
                });
            }
            ViewData["Tags"] = viewModel;
        }

        [HttpPost]
        public ActionResult Edit(InfoEditViewModel vm, string[] selectedTags)
        {
            if (ModelState.IsValid)
            {
                DateTime today = DateTime.Now;
                Infos dbEntry;
                if (vm.ID == 0)
                {
                    dbEntry = new Infos()
                    {
                        Title = vm.Title,
                        HtmlUrl = vm.HtmlUrl,
                        Dtype = 1,
                        CreateDate= today,
                        PublishDate = vm.PublishDate,
                        OffDateTime = vm.OffDateTime,
                        HtmlContent = vm.HtmlContent

                      
                    };
                    //新增
                    context.Infos.Add(dbEntry);
                }
                else
                {
                    //編輯
                    dbEntry =
                        context.Infos
                        .Include(s => s.InfoTag)
                        .ThenInclude(e => e.Tag)
                        .FirstOrDefault(p => p.Id == vm.ID);

                    if (dbEntry != null)
                    {
                        dbEntry.Title = vm.Title;
                        dbEntry.HtmlUrl = vm.HtmlUrl;
                        dbEntry.Dtype = 1;
                        dbEntry.PublishDate = vm.PublishDate;
                        dbEntry.OffDateTime = vm.OffDateTime;
                        dbEntry.HtmlContent = vm.HtmlContent;
                        dbEntry.ModifyDate = today;
                        //UpdateInstructorTags(selectedTags, dbEntry);
                    }
                }
                UpdateInstructorTags(selectedTags, dbEntry);
                context.SaveChanges();
                TempData["message"] = $"已經儲存!";
                return RedirectToAction("List");
            }
            else
            {
                var infoTag =
                    new HashSet<int>(selectedTags.Select(c => Convert.ToInt32(c)));
                var viewModel = new List<SelectListViewModel>();
                foreach (var _tag in context.Tag)
                {
                    viewModel.Add(new SelectListViewModel
                    {
                        Id = _tag.Id,
                        Name = _tag.TagName,
                        Assigned = infoTag.Contains(_tag.Id)
                    });
                }
                ViewData["Tags"] = viewModel;
                return View(vm);
            }
            //return View(vm);
        }

        private void UpdateInstructorTags(string[] selectedTags, Infos infosToUpdate)
        {
            if (selectedTags == null)
            {
                infosToUpdate.InfoTag = new List<InfoTag>();
                return;
            }

            var selectedTagsHS = new HashSet<string>(selectedTags);
            var instructorTags = new HashSet<int>(infosToUpdate.InfoTag.Select(c => c.Tag.Id));
            foreach (var tag in context.Tag)
            {
                if (selectedTagsHS.Contains(tag.Id.ToString()))
                {
                    if (!instructorTags.Contains(tag.Id))
                    {
                        infosToUpdate.InfoTag.Add(new InfoTag { InfoId = infosToUpdate.Id, TagId = tag.Id });
                    }
                }
                else
                {

                    if (instructorTags.Contains(tag.Id))
                    {
                        InfoTag tagToRemove = infosToUpdate.InfoTag.FirstOrDefault(i => i.TagId == tag.Id);
                        context.Remove(tagToRemove);
                    }
                }
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var _data = await context.Infos.FindAsync(id);
            context.Infos.Remove(_data);
            await context.SaveChangesAsync();
            return RedirectToAction("List");
        }

   
    }
}