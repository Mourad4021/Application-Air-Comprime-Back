using System;
using System.ComponentModel.DataAnnotations;

namespace Pgh.Auth.Model.ModelViews.Delete
{

    //Use this class with all the entities when we want to remove recursive or Replace by Null
    public class DeleteRecursiveModel
    {
        [Required]
        public Guid Id { get; set; }
        public bool RemoveAll { get; set; }
    }
}