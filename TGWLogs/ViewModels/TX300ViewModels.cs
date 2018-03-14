using PagedList;
using System.ComponentModel.DataAnnotations;
using TGWLogs.Models;

namespace TGWLogs.ViewModels
{
    public class TX300ViewModels
    {
        /*查詢條件*/
        [Display(Name = "傳送日(起)")]
        public string SendDateST { get; set; } = "";

        [Display(Name = "傳送日(訖)")]
        public string SendDateED { get; set; } = "";

        [Display(Name = "卡號")]
        public string CardNo { get; set; } = "";


        /*資料區*/
        public IPagedList<ISOTextOfTX300> TX300SendState { get; set; }
    }
}