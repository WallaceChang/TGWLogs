using System;
using System.ComponentModel.DataAnnotations;

namespace TGWLogs.Models
{
    public class LogFileInfo
    {
        [Display(Name = "檔案名稱：")]
        public string FileName { get; set; }

        [Display(Name = "異動時間：")]
        public DateTime LastModTime { get; set; }
    }
}