using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmission.DAL.BOs.GeneralObject
{
    public class ScholarshipDataBO
    {
        public int ScholarrshipID { get; set; }
        public String? Name { get; set; }
        public String? Provider { get; set; }
        public String? Benerfit {  get; set; }
    }
}
