using PagedList;
using System.ComponentModel.DataAnnotations;
using TGWLogs.Models;

namespace TGWLogs.ViewModels
{
    public class SysInfoViewModels
    {
        /*查詢條件*/
        [Display(Name = "系統名稱")]
        public string SysName { get; set; } = "";

        [Display(Name = "群組名稱")]
        public string GrpName { get; set; } = "";

        /*資料區*/
        public IPagedList<FullSysInfo> SysInfoList { get; set; }
    }
}