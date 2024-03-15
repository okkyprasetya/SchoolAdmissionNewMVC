using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmission.DAL.BOs.GeneralObject
{
    public class BillBO
    {
        public int BillsID {  get; set; }
        public String? PaymentDueDate { get; set; }
        public String? Type {  get; set; }
        public int Nominals {  get; set; }
    }
}
