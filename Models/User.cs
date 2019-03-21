using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    public class User
    {
       [Key]
       public int UserId {get; set;}
       [Required]
       [MinLength(2)]
       public string FirstName {get; set;}
       [Required]
       [MinLength(2)]
       public string LastName {get; set;}
       [Required]
       [EmailAddress]
       public string Email {get; set;}
       [Required]
       [DataType(DataType.Password)]
       [MinLength(8, ErrorMessage="Password must be 8 characters or longer")]
       public string Password {get; set;}
       [NotMapped]
       [Compare("Password")]
       [DataType(DataType.Password)]
       public string Confirm {get; set;}
       public DateTime CreatedAt {get; set;}
       public DateTime UpdatedAt  {get; set;}
       public List<Rsvp> Commitments {get; set;}
       public List<Wedding> PlannedWeddings {get; set;}
       public User(){
           CreatedAt = DateTime.Now;
           UpdatedAt = CreatedAt;
           Commitments = new List<Rsvp>();
           PlannedWeddings = new List<Wedding>();
       }
    }
}