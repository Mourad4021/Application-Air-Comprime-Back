using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pgh.Auth.Model.Models
{
    public class Applications
    {
        [Key]
        public Guid AppId { get; set; }
        public string AppCode { get; set; }
        public string AppName { get; set; }
        public string AppDisplayName { get;set; }
        public string AppDescription { get; set; }
        public bool AppState { get; set; }


        public virtual ICollection<AffApplicationUsers> AffApplicationUsers { get; set; }
        public virtual ICollection<Groupes> Groupes { get;set; }
        public virtual ICollection<Menus> Menus { get; set; }
    }
}