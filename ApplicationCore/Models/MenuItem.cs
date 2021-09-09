﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationCore.Models
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name="Menu Item")]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage ="Price should be greater than $1")]
        public float Price { get; set; }

        public int CategoryId { get; set; }

        public int FoodCategoryId { get; set; }


        //Connecting Objects or Tables

        [ForeignKey("CategoryId")] // this foreign key (the code below) points to the primary key in the Category model, that way the values are consistent. 
        public virtual Category Category { get; set; }

        [ForeignKey("FoodTypeId")]
        public virtual FoodType FoodType { get; set; }
    }
}
