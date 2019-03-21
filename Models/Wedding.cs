using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    public class Wedding{
        [Key]
        public int WeddingId {get;set;}
        [Required]
        [MinLength(2)]
        public string Wedder1 {get;set;}
        [Required]
        [MinLength(2)]
        public string Wedder2 {get;set;}
        [Required]
        [NoPast]
        [DataType(DataType.Date)]
        public DateTime Date {get;set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt  {get; set;}
        public int UserId {get;set;}
        public User planner {get;set;}
        public List<Rsvp> Guests {get;set;}
        public int LocationId {get;set;}
        public  Location venue {get;set;}
        [NotMapped]
        public int Status {get;set;}
        public Wedding(){
            Guests = new List<Rsvp>();
            CreatedAt = DateTime.Now;
            UpdatedAt = CreatedAt;
            Status = 0;

        }
    }
}