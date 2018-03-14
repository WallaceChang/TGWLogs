using PagedList;
using System.ComponentModel.DataAnnotations;
using TGWLogs.Models;

namespace TGWLogs.ViewModels
{
    public class TlogViewModels
    {
        /*查詢條件*/
        [Display(Name = "交易日(起)")]
        public string TxDateST { get; set; } = "";

        [Display(Name = "交易日(訖)")]
        public string TxDateED { get; set; } = "";

        [Display(Name = "卡號")]
        public string CardNo { get; set; } = "";


        /*資料區*/
        public IPagedList<CosesTlog> TlogSendState { get; set; }
    }
}