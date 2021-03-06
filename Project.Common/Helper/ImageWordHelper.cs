﻿using System;
using System.Collections.Generic;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.IO;
using System.Text;


namespace Project.Common.Helper
{
    /// <summary>
    /// 驗證碼類別庫
    /// </summary>
    public class ImageWordHelper
    {

            /// <summary>  
            /// 該方法用於生成指定位數的亂數  
            /// </summary>  
            /// <param name="VcodeNum">參數是亂數的位數</param>  
            /// <returns>返回一個亂數字串</returns>  
            private string RndNum(int VcodeNum)
            {
                //驗證碼可以顯示的字元集合  
                string Vchar = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,p" +
                    ",q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,P,P,Q" +
                    ",R,S,T,U,V,W,X,Y,Z";
                string[] VcArray = Vchar.Split(new Char[] { ',' });//拆分成陣列   
                string code = "";//產生的亂數  
                int temp = -1;//記錄上次亂數值，儘量避避免生產幾個一樣的亂數  

                Random rand = new Random();
                //採用一個簡單的演算法以保證生成亂數的不同  
                for (int i = 1; i < VcodeNum + 1; i++)
                {
                    if (temp != -1)
                    {
                        rand = new Random(i * temp * unchecked((int)DateTime.Now.Ticks));//初始化隨機類  
                    }
                    int t = rand.Next(61);//獲取亂數  
                    if (temp != -1 && temp == t)
                    {
                        return RndNum(VcodeNum);//如果獲取的亂數重複，則遞迴呼叫  
                    }
                    temp = t;//把本次產生的亂數記錄起來  
                    code += VcArray[t];//亂數的位數加一  
                }
                return code;
            }



            /// <summary>  
            /// 該方法是將生成的亂數寫入影像檔  
            /// </summary>  
            /// <param name="code">code是一個亂數</param>
            /// <param name="numbers">生成位數（默認4位）</param>  
            public MemoryStream Create(out string code, int numbers = 4)
            {
                code = RndNum(numbers);
                Bitmap Img = null;
                Graphics g = null;
                MemoryStream ms = null;
                Random random = new Random();
                //驗證碼顏色集合  
                Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };

                //驗證碼字體集合
                string[] fonts = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋體" };


                //定義圖像的大小，生成圖像的實例  
                Img = new Bitmap((int)code.Length * 18, 32);

                g = Graphics.FromImage(Img);//從Img物件生成新的Graphics物件    

                g.Clear(Color.White);//背景設為白色  

                //在隨機位置畫背景點  
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(Img.Width);
                    int y = random.Next(Img.Height);
                    g.DrawRectangle(new Pen(Color.LightGray, 0), x, y, 1, 1);
                }
                //驗證碼繪製在g中  
                for (int i = 0; i < code.Length; i++)
                {
                    int cindex = random.Next(7);//隨機色彩索引值  
                    int findex = random.Next(5);//隨機字體索引值  
                    Font f = new Font(fonts[findex], 15, FontStyle.Bold);//字體  
                    Brush b = new SolidBrush(c[cindex]);//顏色  
                    int ii = 4;
                    if ((i + 1) % 2 == 0)//控制驗證碼不在同一高度  
                    {
                        ii = 2;
                    }
                    g.DrawString(code.Substring(i, 1), f, b, 3 + (i * 12), ii);//繪製一個驗證字元  
                }
                ms = new MemoryStream();//生成記憶體流物件  
                Img.Save(ms, ImageFormat.Jpeg);//將此圖像以Png影像檔的格式保存到流中  

                //回收資源  
                g.Dispose();
                Img.Dispose();
                return ms;
            }


    }

}
