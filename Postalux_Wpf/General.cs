using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postalux_Wpf
{
    class General
    {
        public string NormalizePath(string Path_Orig)
        {
            if (Path_Orig.EndsWith("\\"))
            {
                return Path_Orig;
            }
            else
            {
                return Path_Orig + "\\";
            }
        }

        /// <summary>
        /// Determine whether a string represents a valid decimal value.
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static bool IsTextNumeric(string Text)
        {
            // ToDo: Make sure there is only one decimal point and only one minus sign.

            bool b_OK = true;

            if (Text.Length > 0)
            {
                // Test the first character:
                string c = Text[0].ToString();
                if (!"-0123456789.".Contains(c)) b_OK = false;

                for (int Pos = 1; Pos < Text.Length; Pos++)
                {
                    c = Text[Pos].ToString();
                    if (!"0123456789.".Contains(c)) b_OK = false;
                }

                if (b_OK)
                {
                    // Count the decimal points in the text.
                    int count = Text.Split('.').Length - 1;
                    if (count > 1) b_OK = false;
                }
            }
            return b_OK;
        }

        /// <summary>
        /// Format an amount of money using the dollar sign if it's at least a dollar, or the cents sign if it's less than a dollar.
        /// </summary>
        /// <param name="dec_Val">amount of money in dollars</param>
        /// <returns>formatted string</returns>
        public string FormatDollarsOrCents(decimal dec_Val)
        {
            string str_Val;

            if (dec_Val < 1)
            {
                str_Val = (dec_Val * 100).ToString("F0") + "¢";
            }
            else
            {
                str_Val = dec_Val.ToString("C2");
            }

            return str_Val;
        }
    }
}