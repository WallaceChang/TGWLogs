using System;
using System.ComponentModel.DataAnnotations;

namespace TGWLogs.Models
{
    public class CosesTlog
    {
        [Display(Name = "交易別")]
        public string TxType { get; set; }

        [Display(Name = "卡號")]
        public string CardNo { get; set; }

        [Display(Name = "交易日")]
        public string TxDate { get; set; }

        [Display(Name = "RRN")]
        public string RRN { get; set; }

        [Display(Name = "傳送否")]
        public string SendYet { get; set; }

        [Display(Name = "傳送日期")]
        public DateTime SendDate { get; set; }

        [Display(Name = "回覆碼")]
        public string RtnCode { get; set; }

        [Display(Name = "回覆日期")]
        public DateTime RtnDate { get; set; }
    }
}