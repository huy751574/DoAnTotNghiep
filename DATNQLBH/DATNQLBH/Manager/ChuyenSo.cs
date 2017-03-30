﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DATNQLBH.Manager
{
    public sealed class ChuyenSo
    {
        public static string ConvertIntToString(string number)
        {
            var dv = new string[] { string.Empty, "mươi", "trăm", "nghìn", "triệu", "tỉ" };
            var cs = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string doc;
            int i, j, k, n, len, found, ddv, rd;

            len = number.Length;
            number += "ss";
            doc = string.Empty;
            found = 0;
            ddv = 0;
            rd = 0;

            i = 0;
            while (i < len)
            {
                n = (len - i + 2) % 3 + 1;


                found = 0;
                for (j = 0; j < n; j++)
                {
                    if (number[i + j] != '0')
                    {
                        found = 1;
                        break;
                    }
                }


                if (found == 1)
                {
                    rd = 1;
                    for (j = 0; j < n; j++)
                    {
                        ddv = 1;
                        switch (number[i + j])
                        {
                            case '0':
                                if (n - j == 3)
                                {
                                    doc += cs[0] + " ";
                                }
                                if (n - j == 2)
                                {
                                    if (number[i + j + 1] != '0')
                                    {
                                        doc += "lẻ ";
                                    }
                                    ddv = 0;
                                }
                                break;
                            case '1':
                                if (n - j == 3)
                                {
                                    doc += cs[1] + " ";
                                }
                                if (n - j == 2)
                                {
                                    doc += "mười ";
                                    ddv = 0;
                                }
                                if (n - j == 1)
                                {
                                    if (i + j == 0)
                                    {
                                        k = 0;
                                    }
                                    else
                                    {
                                        k = i + j - 1;
                                    }
                                    if (number[k] != '1' && number[k] != '0')
                                    {
                                        doc += "mốt ";
                                    }
                                    else
                                    {
                                        doc += cs[1] + " ";
                                    }
                                }
                                break;
                            case '5':
                                if (i + j == len - 1)
                                {
                                    doc += "lăm ";
                                }
                                else
                                {
                                    doc += cs[5] + " ";
                                }
                                break;
                            default:
                                doc += cs[(int)number[i + j] - 48] + " ";
                                break;
                        }


                        if (ddv == 1)
                        {
                            doc += dv[n - j - 1] + " ";
                        }
                    }
                }
                if (len - i - n > 0)
                {
                    if ((len - i - n) % 9 == 0)
                    {
                        if (rd == 1)
                        {
                            for (k = 0; k < (len - i - n) / 9; k++)
                            {
                                doc += "tỉ ";
                            }
                        }
                        rd = 0;
                    }
                    else
                    {
                        if (found != 0)
                        {
                            doc += dv[((len - i - n + 1) % 9) / 3 + 2] + " ";
                        }
                    }
                }

                i += n;
            }

            if (len == 1)
            {
                if (number[0] == '0' || number[0] == '5')
                {
                    return cs[(int)number[0] - 48];
                }
            }
            return doc + "đồng";
        }
        private static List<LapHoaDon> tam;
        public static List<LapHoaDon> HoaDon
        {
            get { return tam; }
            set { tam = value; }
        }
    }
}