using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS2.Models
{
    public class Checkbox
    {
        public bool IsSelected { get; set; }
        public string Email { get; set; }
        public string reviewerId { get; set; }
        public string name { get; set; }
        public string keyword { get; set; }
    }

    public class Checkboxlist
    {
        public List<Checkbox> checklist { get; set; }

        public string message { get; set; }
        public Paper paper { get; set; }
        public IEnumerable<string> GetSelectedId()
        {
            return (from reviewer in checklist
                    where reviewer.IsSelected
                    select reviewer.reviewerId).ToList();
        }
    }
}