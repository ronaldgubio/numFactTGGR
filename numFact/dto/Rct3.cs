using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace numFact.dto
{
    class Rct3
    {
        public String DocNum { get; set; }
        public String CreditCard { get; set; }
        public String CreditAcct { get; set; }
        public int Line_ID { get; set; }
        public String CrTypeCode { get; set; }
        public String CrCardNum { get; set; }
        public DateTime CardValid { get; set; }
        public String VoucherNum { get; set; }
        public int NumOfPmnts { get; set; }
        public decimal CreditSum { get; set; }
        public DateTime CreateDatePOS { get; set; }
        public DateTime CreateDateSap { get; set; }
        public String Origen { get; set; }
        public String PaymentCode { get; set; }
        public String Sincronizado { get; set; }
        public String Accion { get; set; }
        public String Identificador { get; set; }
        public String U_HBT_Depositado { get; set; }
        public String U_LTG_ID_COLAB { get; set; }
        public int UpdateStatus { get; set; }
    }
}
