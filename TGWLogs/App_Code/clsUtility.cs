using System;
using System.Text.RegularExpressions;
using System.Configuration;

namespace TGWLogs.App_Code
{
    public class clsUtility
    {
        #region 連線字串解密(Triple DES)
        public string ConnStrDecrypt(string strConnStrOfEncrypt)
        {
            string strTmp = "";
            System.Type myType = null;
            object objTmp = null;
            try
            {
                /*
                 * 由於C#不支援VB/VBA，故CreateObject的語法在C#無法使用，
                 * 若要動態呼叫已註冊之COM元件，則必須改變寫法。
                 * 下兩行程式段落，效果同VB.NET的
                 * Dim objTmp As Object = CreateObject("UITC.decipher")
                 */
                myType = System.Type.GetTypeFromProgID("UITC.decipher");
                objTmp = System.Activator.CreateInstance(myType);

                /*
                 * 因.Net 4.0以後C#才有支援Optional Parameters，故在非.Net 4.0的開發環境上，
                 * Function的所有參數一律全部要指定清楚，即便該參數實際被定義為Optional，
                 * 下行程式效果同 strTmp = objTmp.CreateDecryptorConn(strConnStrOfEncrypt,"1")
                 */
                strTmp = myType.InvokeMember("CreateDecryptorConn", System.Reflection.BindingFlags.InvokeMethod, null, objTmp, new object[] { strConnStrOfEncrypt, "1" }).ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                myType = null;
                objTmp = null;
            }
            return strTmp;
        }
        #endregion

        #region 字串解密(Triple DES)
        public string DecryptOfTripleDES(string strEncrypt)
        {
            string strTmp = "";
            System.Type myType = null;
            object objTmp = null;
            try
            {
                /*
                 * 由於C#不支援VB/VBA，故CreateObject的語法在C#無法使用，
                 * 若要動態呼叫已註冊之COM元件，則必須改變寫法。
                 * 下兩行程式段落，效果同VB.NET的
                 * Dim objTmp As Object = CreateObject("UITC.decipher")
                 */
                myType = System.Type.GetTypeFromProgID("UITC.decipher");
                objTmp = System.Activator.CreateInstance(myType);

                /*
                 * 因.Net 4.0以後C#才有支援Optional Parameters，故在非.Net 4.0的開發環境上，
                 * Function的所有參數一律全部要指定清楚，即便該參數實際被定義為Optional，
                 * 下行程式效果同 strTmp = objTmp.CreateDecryptorConn(strConnStrOfEncrypt,"1")
                 */
                strTmp = myType.InvokeMember("CreateDecryptor", System.Reflection.BindingFlags.InvokeMethod, null, objTmp, new object[] { strEncrypt, "1" }).ToString();
                return strTmp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                myType = null;
                objTmp = null;
            }
        }
        #endregion

        #region 字串加密(Triple DES)
        public string EncryptOfTripleDES(string strDecrypt)
        {
            string strTmp = "";
            System.Type myType = null;
            object objTmp = null;
            try
            {
                /*
                 * 由於C#不支援VB/VBA，故CreateObject的語法在C#無法使用，
                 * 若要動態呼叫已註冊之COM元件，則必須改變寫法。
                 * 下兩行程式段落，效果同VB.NET的
                 * Dim objTmp As Object = CreateObject("UITC.decipher")
                 */
                myType = System.Type.GetTypeFromProgID("UITC.decipher");
                objTmp = System.Activator.CreateInstance(myType);

                /*
                 * 因.Net 4.0以後C#才有支援Optional Parameters，故在非.Net 4.0的開發環境上，
                 * Function的所有參數一律全部要指定清楚，即便該參數實際被定義為Optional，
                 * 下行程式效果同 strTmp = objTmp.CreateDecryptorConn(strConnStrOfEncrypt,"1")
                 */
                strTmp = myType.InvokeMember("CreateEncryptor", System.Reflection.BindingFlags.InvokeMethod, null, objTmp, new object[] { strDecrypt, "1" }).ToString();
                return strTmp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                myType = null;
                objTmp = null;
            }
        }
        #endregion

        #region 動態組成連線字串
        public string getEFConnStr()
        {
            /*初始化*/
            System.Data.SqlClient.SqlConnectionStringBuilder objSCSB = null;
            try
            {
                objSCSB = new System.Data.SqlClient.SqlConnectionStringBuilder();
                objSCSB.DataSource = DecryptOfTripleDES(ConfigurationManager.AppSettings["SqlSvrIP"].ToString());
                objSCSB.InitialCatalog = DecryptOfTripleDES(ConfigurationManager.AppSettings["InitialDB"].ToString());
                objSCSB.UserID = DecryptOfTripleDES(ConfigurationManager.AppSettings["SqlDBAcct"].ToString());
                objSCSB.Password = DecryptOfTripleDES(ConfigurationManager.AppSettings["SqlDBPwd"].ToString());
                return objSCSB.ConnectionString;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objSCSB.Clear();
                objSCSB = null;
            }
        }
        #endregion

        #region 密碼格式驗證
        public string VerifyOfPwd(string strPwd)
        {
            string strPattern1 = "[a-z]";
            string strPattern2 = "[0-9]";
            string strPattern3 = "[A-Z]";
            string strPattern4 = "[!@#$%^&*]";
            string strRtnMsg = "";
            try
            {
                if (strPwd.Length < 7)
                {
                    strRtnMsg = "密碼不足七碼";
                    return strRtnMsg;
                }

                //檢查密碼是否有英文小寫字母
                if (Regex.IsMatch(strPwd, strPattern1) == false)
                {
                    strRtnMsg = "密碼未包含英文小寫字母";
                    return strRtnMsg;
                }

                //檢查密碼是否有數字
                if (Regex.IsMatch(strPwd, strPattern2) == false)
                {
                    strRtnMsg = "密碼未包含數字";
                    return strRtnMsg;
                }

                //檢查密碼是否有英文大寫字母
                if (Regex.IsMatch(strPwd, strPattern3) == false)
                {
                    strRtnMsg = "密碼未包含英文大寫字母";
                    return strRtnMsg;
                }

                //檢查密碼是否有特殊符號
                if (Regex.IsMatch(strPwd, strPattern4) == false)
                {
                    strRtnMsg = "密碼未包含特殊符號";
                    return strRtnMsg;
                }

                //檢查均無問題則回覆【OK】
                return "OK";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region SqlInjection驗證
        public Boolean VerifyOfSqlInject(string strTarget)
        {
            try
            {
                string strPattern = "(select|insert|update|delete|exec\\(|drop|create|[']|-{2}|\\/\\*|\\*\\/)";
                string strResult = Regex.Replace(strTarget, strPattern, string.Empty, RegexOptions.IgnoreCase);
                if (strResult != strTarget)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Cross Site Scripting(XSS)驗證
        public Boolean VerifyOfXss(string strTarget)
        {
            try
            {
                string strTmp = strTarget;
                strTmp = strTmp.Replace("<", "");
                strTmp = strTmp.Replace(">", "");
                strTmp = strTmp.Replace("%3C", ""); //編碼過後的左邊角括號
                strTmp = strTmp.Replace("%3E", ""); //編碼過後的右邊角括號
                strTmp = strTmp.Replace("javascript:", "");
                strTmp = strTmp.Replace("url(", "");
                strTmp = strTmp.Replace("=", "");
                strTmp = strTmp.Replace("!", "");
                strTmp = strTmp.Replace("#", "");
                strTmp = strTmp.Replace("&rsquo", "");
                if (strTmp != strTarget)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }
        #endregion
    }
}