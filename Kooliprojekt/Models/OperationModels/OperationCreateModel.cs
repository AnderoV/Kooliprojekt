using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kooliprojekt.Models.OperationModels
{
    public class OperationCreateModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public IList<SelectListItem> Car { get; set; }
        public int CarId { get; set; }
    }
}
