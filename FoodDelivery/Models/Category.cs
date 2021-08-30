﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Pages
{
    public class Category
    {
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category Name")] //the values within the [ ] are called 'Data Annotations'
        public string Name { get; set; }

        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }
    }
}
