using System.ComponentModel.DataAnnotations;

namespace StudenManagementBackEnd.Models
{
    public class Student
    {
        [Key]
        public int rollnum { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int? totalmarks { get; set; } // ? for option value , so you can add null value
        public int isPass { get; set; } 

    }
}
