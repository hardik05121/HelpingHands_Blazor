﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HelpingHands_Models
{
    public class CompanyXAmenityUpdateDTO   
    {
        [Required]
        public int Id { get; set; }

        public int AmenityId { get; set; }

        public int CompanyId{ get; set; }

        
    }
}
