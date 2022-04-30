using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace emlakprjsonins.ViewModel
{
    public class EmlakModel
    {
        public int emlakId { get; set; }
        public string emlakAdi { get; set; }
        public string emlakFiyat { get; set; }
        public string emlakAciklama { get; set; }
        public int emlakKatId { get; set; }
        public int emlakUyeId { get; set; }
    }
}