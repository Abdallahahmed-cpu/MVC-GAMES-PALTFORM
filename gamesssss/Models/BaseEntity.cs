﻿using System.ComponentModel.DataAnnotations;

namespace gamesssss.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        [MaxLength(length: 250)]
        public string Name { get; set; } = string.Empty;
        
    }
}
