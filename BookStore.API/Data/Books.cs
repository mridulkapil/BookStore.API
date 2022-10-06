using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Data
{
    public class Books
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Please add the title for the book")]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
