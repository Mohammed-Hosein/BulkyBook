﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BulkyBook.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [Range(1,10000)]
        public Double ListPrice { get; set; }

        [Required]
        [Range(1, 10000)]
        public Double Price { get; set; }

        [Required]
        [Range(1, 10000)]
        public Double Price50 { get; set; }

        [Required]
        [Range(1,10000)]
        public Double Price100 { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public int CovertypeId { get; set; }

        [ForeignKey("CovertypeId")]
        public CoverType CoverType { get; set; }

        public string ImageUrl { get; set; }
    }
}
