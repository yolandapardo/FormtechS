using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FormtechS.Models
{
    public class UsersRole
    {
        public string Id { get; set; }
        [Display(Name ="User Name")]
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DisplayFormat(NullDisplayText ="Anonymous")]
        public List<string> Roles { get; set; }

    }
}