using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models{
    public class WeddingLocation{
        public Wedding wedding {get;set;}
        public Location location {get;set;}
    }
}