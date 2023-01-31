using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace NotBlocket2.Models {
    public class Profile {

        //Konstruktor
        public Profile() { }

        //Publika egenskaper
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Location_Id { get; set; }
        public int Id { get; set; }

    }
    
}
