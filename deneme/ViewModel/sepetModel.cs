using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace deneme.ViewModel
{
    public class sepetModel
    {
        public string sepetId { get; set; }
        public string sepetUyeId { get; set; }
        public string sepetUrunId { get; set; }
        public UrunlerModel urunBilgi { get; set; }
        public uyeModel uyeBilgi { get; set; }
    }
}