using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace wordNote.Models
{
    public class User
    {
        [Column("Id")]
        public string Id { get; set; }
        [Column("Password")]
        public string Password { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Age")]
        public int    Age { get; set; }
        [Column("Salt")]
        public byte[] Salt { get; set; }
    }
}



