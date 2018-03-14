using System;
using System.ComponentModel.DataAnnotations;

namespace TGWLogs.Models
{
    public class FullSysInfo
    {
        [Display(Name = "系統代碼")]
        public int SysCode { get; set; }

        [Display(Name = "系統名稱")]
        public string SysName { get; set; }

        [Display(Name = "群組名稱")]
        public string GrpName { get; set; }

        [Display(Name = "維護日")]
        public DateTime ModDate { get; set; }
    }

    public class TaskOfUpdate
    {
        [Display(Name = "系統代號")]
        [Required(ErrorMessage = "未指定系統代號")]
        public int SysCode { get; set; } = 0;

        [Display(Name = "系統名稱")]
        [Required(ErrorMessage = "尚未輸入系統名稱")]
        public string SysName { get; set; } = "";

        [Display(Name = "群組名稱")]
        [Required(ErrorMessage = "尚未輸入群組名稱")]
        public string GrpName { get; set; } = "";
    }

    public class TaskOfCreate
    {
        [Display(Name = "系統名稱")]
        [Required(ErrorMessage = "尚未輸入系統名稱")]
        public string SysName { get; set; } = "";

        [Display(Name = "群組名稱")]
        [Required(ErrorMessage = "尚未輸入群組名稱")]
        public string GrpName { get; set; } = "";
    }
}