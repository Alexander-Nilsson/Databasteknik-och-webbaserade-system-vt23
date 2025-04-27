using System.ComponentModel.DataAnnotations;
using Microsoft.Build.Framework;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace _5TF048_lab1.Models
{
    public class Dinner
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int NumberofPortions { get; set; }
        public string Review { get; set; }

        [Display(Name = "dl Flour")]
        public int Flour { get; set; }

        [Display(Name = "nr of Eggs")]
        public int Egg { get; set; }

        [Display(Name = "Salt (spoons)")]
        public int Salt { get; set; }

        [Display(Name = "Milk (dl)")]
        public int Milk { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public Dinner() { 
            Flour=0; 
            Egg=0;
            Salt=0;
            Milk=0;
            Name= string.Empty;

        }

        public void Calculate()
        {
            Flour = Flour* NumberofPortions;
            Egg = Egg*2* NumberofPortions;
            Salt = Salt*5*NumberofPortions;
            Milk = Milk * 31 * NumberofPortions;
        }
    }

}
