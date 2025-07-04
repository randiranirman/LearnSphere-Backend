using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.Domain
{
    public  class Subject

    {
        [Required]
        public  int Id { get; set; }
        public string Name{ get; set; }
        public string Code { get; set; }
        public DateTime CreatedDate { get; set; }

        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        // navigation properesty  one subject has many students 
        public ICollection<Student>? Students { get; set; }






    }
}
