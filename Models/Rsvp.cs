using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    public class Rsvp{
        public int RsvpId {get;set;}
        public int UserId {get;set;}
        public User guest {get;set;}
        public int WeddingId {get;set;}
        public Wedding Event {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt  {get; set;}
        public Rsvp(){
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
        public Rsvp(int user, int wedding){
            UserId = user;
            WeddingId = wedding;
            CreatedAt = DateTime.Now;
            UpdatedAt = CreatedAt;
        }
    }
}