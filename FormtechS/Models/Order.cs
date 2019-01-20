using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FormtechS.Models
{
    public class Order
    {
        public int Id { get; set; }
        [StringLength(80)]
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }
        [Required]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public float Precio { get; set; }
        [DataType(DataType.PhoneNumber)]
        [StringLength(15)]
        public string Phone { get; set; }
        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Fecha { get; set; }
        [Required]
        [Display(Name = "Job Number")]
        [StringLength(8)]
        public string JobNumber { get; set; }
        [Display(Name = "Job Reference")]
        [DisplayFormat(NullDisplayText = "Doesn't exist")]
        [StringLength(8)]
        public string ReferenceNumber { get; set; }

        //FEMA
        [StringLength(10)]
        public string Panel { get; set; }
        [Display(Name = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [StringLength(1)]
        public string Suffix { get; set; }
        [Display(Name = "Flood Zone")]
        [StringLength(3)]
        public string FloodZone { get; set; }
        public decimal Elevation { get; set; }
        
        public int CompanyId { get; set; }
        public int ClientId { get; set; }
        public int AddressId { get; set; }
        public int TypeOfSurveyId { get; set; }
        public int DescriptionId { get; set; }

        // NAVIGATION PROPERTIES
        public virtual Company Company { get; set; }
        public virtual Client Client { get; set; }
        public virtual Address Address { get; set; }
        public virtual TypeOfSurvey TypeOfSurvey { get; set; }
        public virtual Description Description { get; set; }



    }
}