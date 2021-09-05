using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.Pages.Admin.Categories
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitofWork;

        [BindProperty] //fields on front end, as they are changed, they are synced with what's going on in the code, relates front end values to code-end values
        public Category CategoryObj { get; set; }

        public UpsertModel(IUnitOfWork unitofWork)   
            => _unitofWork = unitofWork;
        
        public IActionResult OnGet(int ? id)
        {
            CategoryObj = new Category();

            if (id != 0) //edit mode
            {
                CategoryObj = _unitofWork.Category.Get(u => u.Id == id);
                if (CategoryObj == null)
                {
                    return NotFound();
                }
            }           
            return Page(); //assume insert new mode
        }     

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //If New
            if (CategoryObj.Id == 0)
            {
                _unitofWork.Category.Add(CategoryObj); //simply add the object to the category table
            }
            else //existing
            {
                _unitofWork.Category.Update(CategoryObj); //simply update the object in the category table
            }

            _unitofWork.Commit(); //commit changes to the database
            return RedirectToPage("./Index");
        }
    }
}
