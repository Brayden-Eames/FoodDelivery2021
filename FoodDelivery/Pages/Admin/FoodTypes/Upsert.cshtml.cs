using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.Pages.Admin.FoodTypes
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitofWork;

        [BindProperty] //fields on front end, as they are changed, they are synced with what's going on in the code, relates front end values to code-end values
        public FoodType FoodTypeObj { get; set; }

        public UpsertModel(IUnitOfWork unitofWork)
            => _unitofWork = unitofWork;

        public IActionResult OnGet(int? id)
        {
            FoodTypeObj = new FoodType();

            if (id != 0) //edit mode
            {
                FoodTypeObj = _unitofWork.FoodType.Get(u => u.Id == id);
                if (FoodTypeObj == null)
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
            if (FoodTypeObj.Id == 0)
            {
                _unitofWork.FoodType.Add(FoodTypeObj); //simply add the object to the category table
            }
            else //existing
            {
                _unitofWork.FoodType.Update(FoodTypeObj); //simply update the object in the category table
            }

            _unitofWork.Commit(); //commit changes to the database
            return RedirectToPage("./Index");
        }
    }
}
