﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpingHands_DataAccess
{
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DisplayName("Service Name")]
        public string ServiceName { get; set; }

        [ForeignKey("FirstCategory")]
        public int FirstCategoryId { get; set; }
        [ValidateNever]
        public FirstCategory FirstCategory { get; set; }
        public bool IsActive { get; set; }


    }
}