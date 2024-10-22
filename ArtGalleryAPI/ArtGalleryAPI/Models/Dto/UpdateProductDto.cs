﻿using ArtGalleryAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ArtGalleryAPI.Models.Dto
{
    public class UpdateProductDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Product Name can have a maximum of 100 characters!")]
        public required string Name { get; set; }

        [Required]
        [MaxLength(4000, ErrorMessage = "Product Description can have a maximum of 4000 characters!")]
        public required string Description { get; set; }

        [Required]
        [MaxLength(4000, ErrorMessage = "Product image url can have a maximum of 3000 characters!")]
        public required string ImageUrl { get; set; }

        [Required]
        [Precision(13, 3)]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Product status can have a maximum of 30 characters!")]
        public required string Status { get; set; }

        public DateTime? ModifiedAt { get; set; } 

        public string? ModifiedBy {  get; set; }

        [Required]
        public required Guid CategoryId { get; set; }

        //public Category Category { get; set; }
    }
}
