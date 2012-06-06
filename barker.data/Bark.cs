using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace barker.data
{
    public class Bark
    {
        public Guid ID { get; set; }       
        [DataType(DataType.MultilineText)]
        [MaxLength(140)]
        public string Message { get; set; }
        public DateTime CreateAt { get; set; }
        public Guid UserID { get; set; }
        public virtual User User { get; set; } // This is new

        public Bark()
        {
            ID = Guid.NewGuid();
        }
    }
}
