using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    public class Location{
    [Key]
    public int LocationId {get;set;}
    [Required]
    [MinLength(2)]
    public string Name {get;set;}
    [Required]
    [MinLength(2)]
    public string StreetAddress {get;set;}
    public string StreetAddress2 {get;set;}
    [Required]
    [MinLength(2)]
    public string City {get;set;}
    [Required]
    public string State {get;set;}
    [Required]
    public int Zipcode {get;set;}
    public DateTime CreatedAt {get; set;}
    public DateTime UpdatedAt  {get; set;}
    public List<Wedding> Bookings {get;set;}
    public Location(){
        Bookings = new List<Wedding>();
        CreatedAt = DateTime.Now;
        UpdatedAt = CreatedAt;
    }
    }

}