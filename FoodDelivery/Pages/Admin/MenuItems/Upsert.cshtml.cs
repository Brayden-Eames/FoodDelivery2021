using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using FoodDelivery.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodDelivery.Pages.Admin.MenuItems
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        [BindProperty]
        public MenuItemVM MenuItemObj { get; set; }

        public UpsertModel(IUnitOfWork unitofWork, IWebHostEnvironment hostEnvironment)
        {
            _unitofWork = unitofWork;
            _hostEnvironment = hostEnvironment;
        }


        public IActionResult OnGet(int? id)
        {
            var categories = _unitofWork.Category.List();
            var foodTypes = _unitofWork.FoodType.List();

            MenuItemObj = new MenuItemVM
             {
                MenuItem = new MenuItem(),
                CategoryList = categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }),
                FoodTypeList = foodTypes.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.Name }),
            };

            if (id != 0)
            {
                MenuItemObj.MenuItem = _unitofWork.MenuItem.Get(u => u.Id == id, true);
                if (MenuItemObj == null)
                {
                    return NotFound();
                    
                }
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            string webRootPath = _hostEnvironment.WebRootPath; //give root location on server
            var files = HttpContext.Request.Form.Files;

            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (MenuItemObj.MenuItem.Id == 0) //if it equals 0 it's a new menu item. 
            {
                if (files.Count > 0 )
                {
                    string fileName = Guid.NewGuid().ToString(); //unique name for the file, prevents 2 or more files from having same name. 
                    var uploads = Path.Combine(webRootPath, @"images\menuitems\"); // combines the images\menuitems\ portion of the path with the rest of the file path
                    var extension = Path.GetExtension(files[0].FileName); //retrieves file extension name for retention
                    var fullpath = uploads + fileName + extension; // literally appends the entire full file path together based on previous vars. 

                    using (var fileStream = System.IO.File.Create(fullpath))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    MenuItemObj.MenuItem.Image = @"\images\menuitems\" + fileName + extension; //this is what get's saved into the database
                }

                _unitofWork.MenuItem.Add(MenuItemObj.MenuItem);
            }
            else //update
            {
                var objFromDb = _unitofWork.MenuItem.Get(m => m.Id == MenuItemObj.MenuItem.Id, true);

                if(files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString(); //unique name for the file, prevents 2 or more files from having same name. 
                    var uploads = Path.Combine(webRootPath, @"images\menuitems\"); // combines the images\menuitems\ portion of the path with the rest of the file path
                    var extension = Path.GetExtension(files[0].FileName); //retrieves file extension name for retention

                    if (objFromDb.Image != null)
                    {
                        var imagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath); //physically delete file 
                        }

                        var fullpath = uploads + fileName + extension; // literally appends the entire full file path together based on previous vars. 
                        using (var fileStream = System.IO.File.Create(fullpath))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        MenuItemObj.MenuItem.Image = @"\images\menuitems\" + fileName + extension; //this is what get's saved into the database
                    }
                    else
                    {
                        MenuItemObj.MenuItem.Image = objFromDb.Image; 
                    }

                    _unitofWork.MenuItem.Update(MenuItemObj.MenuItem);
                }           
            }
            _unitofWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}
