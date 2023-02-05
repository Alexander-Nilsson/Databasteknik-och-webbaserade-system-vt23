using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;


namespace NotBlocket2.Models {
    public class Profile {

        //Konstruktor
        public Profile() { }

        //Publika egenskaper
        
        public string Name { get; set; }

        [Required, StringLength(60, MinimumLength = 3)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public int Location_Id { get; set; }
        public int Id { get; set; }

    }
    
}
