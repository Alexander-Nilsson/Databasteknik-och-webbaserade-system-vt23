using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Web.Mvc;
using System.Data.Entity;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;


namespace NotBlocket2.Models {
    //File : /Models/ImageModel.cs
    public class ImageModel {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
    }

    public class MyDbContext : DbContext {
        public System.Data.Entity.DbSet<ImageModel> Images { get; set; }
    }

}

