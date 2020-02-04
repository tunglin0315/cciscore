using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Common.Helper
{
    /// <summary>
    /// 加解密類別庫
    /// </summary>
    public class CryptionHelper
    {


            #region <Key值產生>

            /// <summary>
            /// 取得8碼日期(加密KEY值1)
            /// </summary>
            /// <param name="objDate">傳入日期</param>
            /// <returns>YYYYMMDD</returns>
            public string DateTimeString(DateTime objDate)
            {

                int objYearTemp = objDate.Year - 64;
                String objYear = objYearTemp.ToString();
                String objMonth = objDate.Month.ToString();
                String objDay = objDate.Day.ToString();
                if (objMonth.Length < 2)
                {
                    objMonth = "0" + objMonth;
                }
                if (objDay.Length < 2)
                {
                    objDay = "0" + objDay;
                }
                return objYear + objMonth + objDay;
            }

            /// <summary>
            /// 取得IP(加密KEY值2)
            /// </summary>
            /// <returns>8碼加密KEY值</returns>
            public string CheckIP()
            {
                //string sUserHost = Context.Request.UserHostAddress;
                //if ((sUserHost == "127.0.0.1") || (sUserHost.Substring(0, 7).ToString() == "192.168") || (sUserHost == "59.124.13.202"))
                //{
                return "EPSYSTEM";
                //}
                //else
                //{
                //    return "False";
                //}
            }

            #endregion

            IBlockCipher engine = new DesEngine();

            /// <summary>
            /// 使用DES加密，key输入密码的时候，必须使用英文字符，区分大小写，且字符数量是8个，不能多也不能少
            /// </summary>
            /// <param name="plainText">需要加密的字符串</param>
            /// <param name="keys">加密字符串的密钥</param>
            /// <returns>加密后的字符串</returns>
            public string Encrypt(string keys, string plainText)
            {

                byte[] ptBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] rv = Encrypt(keys, ptBytes);
                StringBuilder ret = new StringBuilder();
                foreach (byte b in rv)
                {
                    ret.AppendFormat("{0:X2}", b);
                }
                return ret.ToString();
            }

            private byte[] Encrypt(string keys, byte[] ptBytes)
            {
                byte[] key = Encoding.UTF8.GetBytes(keys);
                BufferedBlockCipher cipher = new PaddedBufferedBlockCipher(new CbcBlockCipher(engine), new Pkcs7Padding());
                cipher.Init(true, new ParametersWithIV(new DesParameters(key), key));
                byte[] rv = new byte[cipher.GetOutputSize(ptBytes.Length)];
                int tam = cipher.ProcessBytes(ptBytes, 0, ptBytes.Length, rv, 0);

                cipher.DoFinal(rv, tam);
                return rv;
            }

            /// <summary>
            /// 使用DES解密，key输入密码的时候，必须使用英文字符，区分大小写，且字符数量是8个，不能多也不能少
            /// </summary>
            /// <param name="cipherText">需要加密的字符串</param>
            /// <param name="keys">加密字符串的密钥</param>
            /// <returns>解密后的字符串</returns>
            public string Decrypt(string keys, string cipherText)
            {
                byte[] inputByteArray = new byte[cipherText.Length / 2];
                for (int x = 0; x < cipherText.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(cipherText.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }
                var rv = Decrypt(keys, inputByteArray);

                return Encoding.UTF8.GetString(rv);

            }

            private byte[] Decrypt(string keys, byte[] cipherText)
            {
                byte[] key = Encoding.UTF8.GetBytes(keys);
                BufferedBlockCipher cipher = new PaddedBufferedBlockCipher(new CbcBlockCipher(engine));
                cipher.Init(false, new ParametersWithIV(new DesParameters(key), key));
                byte[] rv = new byte[cipher.GetOutputSize(cipherText.Length)];
                int tam = cipher.ProcessBytes(cipherText, 0, cipherText.Length, rv, 0);

                cipher.DoFinal(rv, tam);

                return rv;
            }

        }
    }

