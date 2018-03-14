using System;
using System.ComponentModel.DataAnnotations;

namespace TGWLogs.Models
{
    public class ISOTextOfTX300
    {
        [Display(Name = "流水號")]
        public string SeqNo { get; set; }

        [Display(Name = "系統名稱")]
        public string SysName { get; set; }

        [Display(Name = "功能名稱")]
        public string FuncName { get; set; }

        [Display(Name = "傳送否")]
        public string SendText { get; set; }

        [Display(Name = "傳送日")]
        public DateTime SendDate { get; set; }

        [Display(Name = "回覆碼")]
        public string RtnCode { get; set; }

        [Display(Name = "回覆電文")]
        public string RtnText { get; set; }

        [Display(Name = "回覆日")]
        public DateTime RtnDate { get; set; }

        [Display(Name = "收送秒差")]
        public string GapOfSecs { get; set; }
    }
}