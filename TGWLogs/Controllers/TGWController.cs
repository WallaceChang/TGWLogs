using Dapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security.AntiXss;

namespace TGWLogs.Controllers
{
    public class TGWController : Controller
    {
        // GET: TGW
        public ActionResult Index()
        {
            /*TGW Log 查詢說明頁*/
            return View();
        }

        private IEnumerable<Models.CosesTlog> getTlogSendState(string TxStartDate = "", string TxEndDate = "", string CardNo = "")
        {
            IEnumerable<Models.CosesTlog> QryRst = null;
            App_Code.clsUtility objUtil = null;
            string SqlCmd = "";
            try
            {
                objUtil = new App_Code.clsUtility();
                using (var SqlConn = new SqlConnection(objUtil.ConnStrDecrypt(ConfigurationManager.ConnectionStrings["TGWLogs"].ConnectionString)))
                {
                    SqlCmd = "EXEC SP_TLOG_SENDSTATE @TxDateST,@TxDateED,500,'','',@CardNo";
                    var SqlParams = new { @TxDateST = TxStartDate, @TxDateED = TxEndDate, @CardNo = CardNo };
                    QryRst = SqlConn.Query<Models.CosesTlog>(SqlCmd, SqlParams);
                    return QryRst;
                }
            }
            catch (Exception)
            {
                return QryRst;
            }
            finally
            {
                QryRst = null;
            }
        }

        public ActionResult TlogSendState(ViewModels.TlogViewModels FormCols, int PageIdx = 0)
        {
            /*初始化*/
            ViewModels.TlogViewModels vmTlogSendStat = null;
            try
            {
                if (PageIdx > 0)
                {
                    /*OWASP檢查*/
                    OWASP_Verify(FormCols.TxDateST);
                    OWASP_Verify(FormCols.TxDateED);
                    OWASP_Verify(FormCols.CardNo);

                    /*參數透過AntiXss編碼*/
                    FormCols.TxDateST = AntiXssEncoder.HtmlEncode(FormCols.TxDateST, true);
                    FormCols.TxDateED = AntiXssEncoder.HtmlEncode(FormCols.TxDateED, true);
                    FormCols.CardNo = AntiXssEncoder.HtmlEncode(FormCols.CardNo, true);

                    /*取得TLOG*/
                    vmTlogSendStat = new ViewModels.TlogViewModels();
                    vmTlogSendStat.TxDateST = FormCols.TxDateST;
                    vmTlogSendStat.TxDateED = FormCols.TxDateED;
                    vmTlogSendStat.CardNo = string.IsNullOrEmpty(FormCols.CardNo) ? "" : FormCols.CardNo.Trim();
                    vmTlogSendStat.TlogSendState = getTlogSendState(FormCols.TxDateST, FormCols.TxDateED, FormCols.CardNo).ToPagedList(PageIdx, 5);
                }
                return View(vmTlogSendStat);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Exception", "TGW", new { @MsgCont = ex.Message });
            }
            finally
            {
                vmTlogSendStat = null;
            }
        }

        [HttpPost]
        public ActionResult TlogSendState(ViewModels.TlogViewModels FormCols)
        {
            /*初始化宣告*/
            ViewModels.TlogViewModels vmTlogSendStat = null;
            try
            {
                /*OWASP檢查*/
                OWASP_Verify(FormCols.TxDateST);
                OWASP_Verify(FormCols.TxDateED);
                OWASP_Verify(FormCols.CardNo);

                /*參數透過AntiXss編碼*/
                FormCols.TxDateST = AntiXssEncoder.HtmlEncode(FormCols.TxDateST, true);
                FormCols.TxDateED = AntiXssEncoder.HtmlEncode(FormCols.TxDateED, true);
                FormCols.CardNo = AntiXssEncoder.HtmlEncode(FormCols.CardNo, true);

                /*取得TLOG*/
                vmTlogSendStat = new ViewModels.TlogViewModels();
                vmTlogSendStat.TxDateST = FormCols.TxDateST;
                vmTlogSendStat.TxDateED = FormCols.TxDateED;
                vmTlogSendStat.CardNo = string.IsNullOrEmpty(FormCols.CardNo) ? "" : FormCols.CardNo.Trim();
                vmTlogSendStat.TlogSendState = getTlogSendState(FormCols.TxDateST, FormCols.TxDateED, FormCols.CardNo).ToPagedList(1, 5);
                return View(vmTlogSendStat);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Exception", "TGW", new { @MsgCont = ex.Message });
            }
            finally
            {
                vmTlogSendStat = null;
            }
        }

        private IEnumerable<Models.ISOTextOfTX300> getTX300SendStat(string DateST = "", string DateED = "", string CardNo = "")
        {
            IEnumerable<Models.ISOTextOfTX300> QryRst = null;
            App_Code.clsUtility objUtil = null;
            string SqlCmd = "";
            try
            {
                objUtil = new App_Code.clsUtility();
                using (var Sqlconn = new SqlConnection(objUtil.ConnStrDecrypt(ConfigurationManager.ConnectionStrings["TGWLogs"].ConnectionString)))
                {
                    SqlCmd = "EXEC SP_TX300_SENDSTATE @StartDate,@EndDate,1,500,@CardNo";
                    var SqlParams = new { @StartDate = DateST, @EndDate = DateED, @CardNo = CardNo };
                    QryRst = Sqlconn.Query<Models.ISOTextOfTX300>(SqlCmd, SqlParams);
                    return QryRst;
                }
            }
            catch (Exception)
            {
                return QryRst;
            }
            finally
            {
                QryRst = null;
            }
        }

        public ActionResult TX300SendState(ViewModels.TX300ViewModels FormCols, int PageIdx = 0)
        {
            /*初始化*/
            ViewModels.TX300ViewModels vmTX300SendStat = null;
            try
            {
                if (PageIdx > 0)
                {
                    /*OWASP檢查*/
                    OWASP_Verify(FormCols.SendDateST);
                    OWASP_Verify(FormCols.SendDateED);
                    OWASP_Verify(FormCols.CardNo);

                    /*參數透過AntiXss編碼*/
                    FormCols.SendDateST = AntiXssEncoder.HtmlEncode(FormCols.SendDateST, true);
                    FormCols.SendDateED = AntiXssEncoder.HtmlEncode(FormCols.SendDateED, true);
                    FormCols.CardNo = AntiXssEncoder.HtmlEncode(FormCols.CardNo, true);

                    /*取得ISO電文*/
                    vmTX300SendStat = new ViewModels.TX300ViewModels();
                    vmTX300SendStat.SendDateST = FormCols.SendDateST;
                    vmTX300SendStat.SendDateED = FormCols.SendDateED;
                    vmTX300SendStat.CardNo = string.IsNullOrEmpty(FormCols.CardNo) ? "" : FormCols.CardNo.Trim();
                    vmTX300SendStat.TX300SendState = getTX300SendStat(FormCols.SendDateST, FormCols.SendDateED, FormCols.CardNo).ToPagedList(PageIdx, 5);
                }
                return View(vmTX300SendStat);
            }
            catch (Exception ex)
            {
                return RedirectToAction("", "", new { @MsgCont = ex.Message });
            }
            finally
            {
                vmTX300SendStat = null;
            }
        }

        [HttpPost]
        public ActionResult TX300SendState(ViewModels.TX300ViewModels FormCols)
        {
            /*初始化宣告*/
            ViewModels.TX300ViewModels vmTX300SendStat = null;
            try
            {
                /*OWASP檢查*/
                OWASP_Verify(FormCols.SendDateST);
                OWASP_Verify(FormCols.SendDateED);
                OWASP_Verify(FormCols.CardNo);

                /*參數透過AntiXss編碼*/
                FormCols.SendDateST = AntiXssEncoder.HtmlEncode(FormCols.SendDateST, true);
                FormCols.SendDateED = AntiXssEncoder.HtmlEncode(FormCols.SendDateED, true);
                FormCols.CardNo = AntiXssEncoder.HtmlEncode(FormCols.CardNo, true);

                /*取得ISO電文*/
                vmTX300SendStat = new ViewModels.TX300ViewModels();
                vmTX300SendStat.SendDateST = FormCols.SendDateST;
                vmTX300SendStat.SendDateED = FormCols.SendDateED;
                vmTX300SendStat.CardNo = string.IsNullOrEmpty(FormCols.CardNo) ? "" : FormCols.CardNo.Trim();
                vmTX300SendStat.TX300SendState = getTX300SendStat(FormCols.SendDateST, FormCols.SendDateED, FormCols.CardNo).ToPagedList(1, 5);
                return View(vmTX300SendStat);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Exception", "TGW", new { @MsgCont = ex.Message });
            }
            finally
            {
                vmTX300SendStat = null;
            }
        }

        private IEnumerable<Models.FullSysInfo> getSysInfo(string SystemName = "", string GroupName = "")
        {
            IEnumerable<Models.FullSysInfo> QryRst = null;
            App_Code.clsUtility objUtil = null;
            string SqlCmd = "";
            try
            {
                objUtil = new App_Code.clsUtility();
                using (var SqlConn = new SqlConnection(objUtil.ConnStrDecrypt(ConfigurationManager.ConnectionStrings["TGWLogs"].ConnectionString)))
                {
                    SqlCmd = "SELECT intSYS_CODE AS SysCode,varSYS_DESC AS SysName," +
                             "varOWN_GRP AS GrpName,dtMODIFY_DATE AS ModDate " +
                             "FROM SYSTEM_LIST";
                    QryRst = SqlConn.Query<Models.FullSysInfo>(SqlCmd);
                    if (SystemName != null) { QryRst = QryRst.Where(s => s.SysName.Contains(SystemName)); }
                    if (GroupName != null) { QryRst = QryRst.Where(s => s.GrpName.Contains(GroupName)); }
                    return QryRst;
                }
            }
            catch (Exception)
            {
                return QryRst;
            }
            finally
            {
                QryRst = null;
            }
        }

        public ActionResult SysList(ViewModels.SysInfoViewModels FormCols, int PageIdx = 0)
        {
            /*初始化*/
            ViewModels.SysInfoViewModels vmSysInfo = null;
            try
            {
                if (PageIdx > 0)
                {
                    /*OWASP檢查*/
                    OWASP_Verify(FormCols.SysName);
                    OWASP_Verify(FormCols.GrpName);

                    /*參數透過AntiXss編碼*/
                    FormCols.SysName = AntiXssEncoder.HtmlEncode(FormCols.SysName, true);
                    FormCols.GrpName = AntiXssEncoder.HtmlEncode(FormCols.GrpName, true);

                    /*取得系統清單*/
                    vmSysInfo = new ViewModels.SysInfoViewModels();
                    vmSysInfo.SysName = Server.HtmlDecode(FormCols.SysName);
                    vmSysInfo.GrpName = Server.HtmlDecode(FormCols.GrpName);
                    vmSysInfo.SysInfoList = getSysInfo(Server.HtmlDecode(FormCols.SysName), Server.HtmlDecode(FormCols.GrpName)).ToPagedList(PageIdx, 3);
                }
                return View(vmSysInfo);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Exception", "TGW", new { @MsgCont = ex.Message });
            }
            finally
            {
                vmSysInfo = null;
            }
        }

        [HttpPost]
        public ActionResult SysList(ViewModels.SysInfoViewModels FormCols)
        {
            /*初始化宣告*/
            ViewModels.SysInfoViewModels vmSysInfo = null;
            try
            {
                /*OWASP檢查*/
                OWASP_Verify(FormCols.SysName);
                OWASP_Verify(FormCols.GrpName);

                /*參數透過AntiXss編碼*/
                FormCols.SysName = AntiXssEncoder.HtmlEncode(FormCols.SysName, true);
                FormCols.GrpName = AntiXssEncoder.HtmlEncode(FormCols.GrpName, true);

                /*取得系統清單*/
                vmSysInfo = new ViewModels.SysInfoViewModels();
                vmSysInfo.SysName = Server.HtmlDecode(FormCols.SysName);
                vmSysInfo.GrpName = Server.HtmlDecode(FormCols.GrpName);
                vmSysInfo.SysInfoList = getSysInfo(Server.HtmlDecode(FormCols.SysName), Server.HtmlDecode(FormCols.GrpName)).ToPagedList(1, 3);
                return View(vmSysInfo);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Exception", "TGW", new { @MsgCont = ex.Message });
            }
            finally
            {
                vmSysInfo = null;
            }
        }

        public ActionResult UpdSysInfo(int SCode = 0, string SName = "", string GName = "")
        {
            try
            {
                /*OWASP檢查*/
                OWASP_Verify(SCode.ToString());
                OWASP_Verify(SName);
                OWASP_Verify(GName);

                /*參數透過AntiXss編碼*/
                SCode = Convert.ToInt16(AntiXssEncoder.HtmlEncode(SCode.ToString(), true));
                SName = AntiXssEncoder.HtmlEncode(SName, true);
                GName = AntiXssEncoder.HtmlEncode(GName, true);

                var ColCollect = new Models.TaskOfUpdate { SysCode = SCode, SysName = Server.HtmlDecode(SName), GrpName = Server.HtmlDecode(GName) };
                return View(ColCollect);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Exception", "TGW", new { @MsgCont = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult UpdSysInfo(Models.TaskOfUpdate FormCols)
        {
            /*欄位Validation*/
            if (ModelState.IsValid == false) { return View(FormCols); }

            /*初始化宣告*/
            App_Code.clsUtility objUtil = null;
            string SqlCmd = "";

            try
            {
                /*OWASP檢查*/
                OWASP_Verify(FormCols.SysName);
                OWASP_Verify(FormCols.GrpName);

                /*參數透過AntiXss編碼*/
                FormCols.SysCode = Convert.ToInt16(AntiXssEncoder.HtmlEncode(FormCols.SysCode.ToString(), true));
                FormCols.SysName = AntiXssEncoder.HtmlEncode(FormCols.SysName, true);
                FormCols.GrpName = AntiXssEncoder.HtmlEncode(FormCols.GrpName, true);

                objUtil = new App_Code.clsUtility();

                using (var SqlConn = new SqlConnection(objUtil.ConnStrDecrypt(ConfigurationManager.ConnectionStrings["TGWLogs"].ConnectionString)))
                {
                    SqlCmd = "UPDATE SYSTEM_LIST SET " +
                             "varSYS_DESC = @SysName," +
                             "varOWN_GRP = @GrpName," +
                             "dtMODIFY_DATE = GETDATE() " +
                             "WHERE intSYS_CODE = @SysCode";
                    var SqlParams = new { @SysCode = FormCols.SysCode, @SysName = Server.HtmlDecode(FormCols.SysName), @GrpName = Server.HtmlDecode(FormCols.GrpName) };
                    SqlConn.Execute(SqlCmd, SqlParams);
                }
                TempData["ExeRst"] = "【" + Server.HtmlDecode(FormCols.SysName) + "】更新成功";
                return RedirectToAction("SysList", "TGW", new { PageIdx = 1, SystemName = FormCols.SysName });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Exception", "TGW", new { @MsgCont = ex.Message });
            }
            finally
            {
                objUtil = null;
            }
        }

        public ActionResult InsSysInfo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsSysInfo(Models.TaskOfCreate FormCols)
        {
            /*欄位Validation*/
            if (ModelState.IsValid == false) { return View(FormCols); }

            /*初始化宣告*/
            App_Code.clsUtility objUtil = null;
            string SqlCmd = "";

            try
            {
                /*OWASP檢查*/
                OWASP_Verify(FormCols.SysName);
                OWASP_Verify(FormCols.GrpName);

                /*參數透過AntiXss編碼*/
                FormCols.SysName = AntiXssEncoder.HtmlEncode(FormCols.SysName, true);
                FormCols.GrpName = AntiXssEncoder.HtmlEncode(FormCols.GrpName, true);

                objUtil = new App_Code.clsUtility();
                using (var SqlConn = new SqlConnection(objUtil.ConnStrDecrypt(ConfigurationManager.ConnectionStrings["TGWLogs"].ConnectionString)))
                {
                    SqlCmd = "INSERT INTO SYSTEM_LIST (intSYS_CODE,varSYS_DESC,varOWN_GRP,dtMODIFY_DATE) " +
                             "VALUES " +
                             "((SELECT MAX(intSYS_CODE) + 1 FROM SYSTEM_LIST),@SysName,@GrpName,GETDATE())";
                    var SqlParams = new { @SysName = Server.HtmlDecode(FormCols.SysName), @GrpName = Server.HtmlDecode(FormCols.GrpName) };
                    SqlConn.Execute(SqlCmd, SqlParams);
                    TempData["ExeRst"] = "【" + Server.HtmlDecode(FormCols.SysName) + "】新增成功";
                    return RedirectToAction("SysList", "TGW");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Exception", "TGW", new { @MsgCont = ex.Message });
            }
            finally
            {
                objUtil = null;
            }
        }

        [HttpPost]
        public ActionResult DelSysInfo(int SCodeByDel = 0)
        {
            /*參數透過AntiXss編碼*/
            SCodeByDel = Convert.ToInt16(AntiXssEncoder.HtmlEncode(SCodeByDel.ToString(), true));

            /*初始化宣告*/
            App_Code.clsUtility objUtil = null;
            string SqlCmd = "";
            try
            {
                objUtil = new App_Code.clsUtility();
                using (var SqlConn = new SqlConnection(objUtil.ConnStrDecrypt(ConfigurationManager.ConnectionStrings["TGWLogs"].ConnectionString)))
                {
                    SqlCmd = "DELETE SYSTEM_LIST WHERE intSYS_CODE = @SysCode";
                    var SqlParams = new { @SysCode = SCodeByDel };
                    SqlConn.Execute(SqlCmd, SqlParams);
                    return RedirectToAction("SysList", "TGW", new { PageIdx = 1 });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Exception", "TGW", new { @MsgCont = ex.Message });
            }
        }

        #region XSS & SQL-Injection 檢測
        private void OWASP_Verify(string ParamValue)
        {
            App_Code.clsUtility objUtil = null;
            try
            {
                if (ParamValue != null)
                {
                    objUtil = new App_Code.clsUtility();

                    /*XSS檢查*/
                    if (objUtil.VerifyOfXss(ParamValue) == false) { RedirectToAction("Exception", "TGW", new { @MsgCont = "參數含有XSS內容" }); }

                    /*SqlInjection檢查*/
                    if (objUtil.VerifyOfSqlInject(ParamValue) == false) { RedirectToAction("Exception", "TGW", new { @MsgCont = "參數含有SqlInjection內容" }); }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objUtil = null;
            }
        }
        #endregion

        #region TGW Controller 統一 Exception 訊息顯示
        public ActionResult Exception(string MsgCont = "")
        {
            ViewBag.ErrMsg = MsgCont;
            return View();
        }
        #endregion

        public ActionResult LogFilesList(string FileSource = "")
        {
            /*OWASP檢查*/
            OWASP_Verify(FileSource);

            /*參數透過AntiXss編碼*/
            FileSource = AntiXssEncoder.HtmlEncode(FileSource, true);

            /*初始化宣告*/
            List<Models.LogFileInfo> FileList = null;
            Models.LogFileInfo FileInfo = null;
            string[] arrAllFilePath = null;
            try
            {
                FileList = new List<Models.LogFileInfo>();
                arrAllFilePath = Directory.GetFiles(ConfigurationManager.AppSettings["LOG-" + FileSource].ToString());
                foreach (string eachPath in arrAllFilePath)
                {
                    FileInfo = new Models.LogFileInfo();
                    FileInfo.FileName = Path.GetFileName(eachPath);
                    FileInfo.LastModTime = Directory.GetLastWriteTime(eachPath);
                    FileList.Add(FileInfo);
                    FileInfo = null;
                }
                ViewData["FileSource"] = FileSource;
                ViewData["URI"] = ConfigurationManager.AppSettings["Host"].ToString() + "/LOG-" + FileSource;
                return View(FileList);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Exception", "TGW", new { @MsgCont = ex.Message });
            }
            finally
            {
                FileList = null;
                arrAllFilePath = null;
            }
        }

        [HttpPost]
        public ActionResult SendLogFile(string[] LogFileName = null, string SourceType = "")
        {
            App_Code.clsMail objMail = null;
            int i = 0;
            try
            {
                if (LogFileName != null && LogFileName.Length > 0)
                {
                    objMail = new App_Code.clsMail();
                    for (i = 0; i < LogFileName.Length; i++)
                    {
                        LogFileName[i] = ConfigurationManager.AppSettings["LOG-" + SourceType].ToString() + @"\" + LogFileName[i];
                    }
                    objMail.SendMail(ConfigurationManager.AppSettings["MailSvr"].ToString(),
                                     Convert.ToInt16(ConfigurationManager.AppSettings["SendPort"].ToString()),
                                     "wallacechang@uitc.com.tw",
                                     "wallacechang@uitc.com.tw",
                                     "TGW LogFile by " + SourceType,
                                     "請參閱附件<br /><br />", "", "", LogFileName,
                                     App_Code.clsMail.MailFormat.Html,
                                     System.Net.Mail.MailPriority.Normal,
                                     App_Code.clsMail.EncodingType.UTF8);
                }
                return RedirectToAction("LogFilesList", "TGW", new { FileSource = SourceType });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Exception", "TGW", new { @MsgCont = ex.Message });
            }
            finally
            {
                objMail = null;
            }
        }
    }
}