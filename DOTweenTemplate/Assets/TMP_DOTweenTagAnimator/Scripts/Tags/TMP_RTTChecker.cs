using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace TMP_DOTweenTagAnimator.Tags
{
    /// <summary>
    /// Rich Text Tag を判定するクラス
    /// </summary>
    public class TMP_RTTChecker
    {
        /// <summary>
        /// RichTextTagの種類
        /// </summary>
        //B->Begin
        //E->End
        public enum TMP_RTT
        {
            Align_B = 2,            //<align="??">              ?? = left,center,right,justified,flush
            Align_E = 3,            //</align>
            Allcaps_B = 4,          //<allcaps>
            Allcaps_E = 5,          //</allcaps>
            Alpha_B = 6,            //<alpha=#??>               ?? = 16進数2桁
            Bold_B = 7,             //<b>
            Bold_E = 8,             //</b>
            Color_B = 9,            //<color="??">              ?? = black,blue,green,orange,purple,red,white,yellow
            Color_E = 10,            //</color>
            Cspace_B = 11,          //<cspace=??em>             ?? = number(double)
            Cspace_E = 12,          //</cspace>
            Font_B = 13,            //<font="??">               ?? = font name(任意のstring)
            Font_E = 14,            //</font>
            Font_Weight_B = 15,     //<font-weight="??">        ?? = number100,200~900(int)
            Font_Weight_E = 16,     //</font-weight>
            Gradient_B = 17,        //<gradient="??">           ?? = gradient preset(任意のstring)
            Gradient_E = 18,        //</gradient>
            Italic_B = 19,          //<i>
            Italic_E = 20,          //</i>
            Indent_B = 21,          //<indent=??%>              ?? = number(double)
            Indent_E = 22,          //</indent>
            Line_Height_B = 23,     //<line-height=??%>         ?? = number(double)
            Line_Height_E = 24,     //</line-height>
            Line_Indent_B = 25,     //<line-indent=??%>         ?? = number(double)
            Line_Indent_E = 26,     //</line-indent>
            Link_B = 27,            //<link="??">               ?? = font name(任意のstring)
            Link_E = 28,            //</link>
            Lowercase_B = 29,       //<lowercase>
            Lowercase_E = 30,       //</lowercase>
            Margin_B = 31,          //<margin=??em>             ?? = number(double)
            Margin_Left_B = 32,     //<margin-left>
            Margin_Right_B = 33,    //<margin-right>
            Margin_E = 34,          //</margin>
            Mark_B = 35,            //<mark=#??>                ?? = 16進数6桁or8桁
            Mark_E = 36,            //</mark>
            Mspace_B = 37,          //<mspace=??em>             ?? = number(double)
            Mspace_E = 38,          //</mspace>
            Nobr_B = 39,            //<nobr>
            Nobr_E = 40,            //</nobr>
            Noparse_B = 41,         //<noparse>
            Noparse_E = 42,         //</noparse>
            Page_B = 43,            //<page>
            Position_B = 44,        //<pos=??%>                 ?? = number(double)
            Rotate_B = 45,          //<rotate="??">             ?? = number(double)
            Rotate_E = 46,          //<rotate="??">             ?? = number(double)
            Strikethrough_B = 47,   //<s>
            Strikethrough_E = 48,   //</s>
            Size_B = 49,            //<size=??%>                ?? = number(double)
            Size_E = 50,            //</size>                   ?? = number(double)
            Smallcaps_B = 51,       //<smallcaps>
            Smallcaps_E = 52,       //</smallcaps>
            Space_B = 53,           //<space=??em>              ?? = number(double)
            Sprite_B = 54,          //<sprite??>                ?? = 任意のstring
            Style_B = 55,           //<style="??">              ?? = 任意のstring
            Style_E = 56,           //</style>
            Subscript_B = 57,       //<sub>
            Subscript_E = 58,       //</sub>
            Superscript_B = 59,     //<sup>
            Superscript_E = 60,     //</sup>
            Underline_B = 61,       //<u>
            Underline_E = 62,       //</u>
            Uppercase_B = 63,       //<uppercase>
            Uppercase_E = 64,       //</uppercase>
            Voffset_B = 65,         //<voffset=??em>            ?? = number(double)
            Voffset_E = 66,         //</voffset>
            Width_B = 67,           //<width=??%>               ?? = number(double)
            Width_E = 68,           //</width>                  ?? = number(double)
        }

        /// <summary>
        /// タグの種類とその判定文字列(正規表現)
        /// </summary>
        private Dictionary<TMP_RTT, string> tagMatchDictionary;
        
        /// <summary>
        /// コンストラクタ
        /// タグと判定文字列の初期化
        /// </summary>
        public TMP_RTTChecker()
        {
            tagMatchDictionary = new Dictionary<TMP_RTT, string>()
            {
                {TMP_RTT.Align_B,"<align=[^>]*>"},
                {TMP_RTT.Align_E,"</align>"},
                {TMP_RTT.Allcaps_B,"<allcaps>"},
                {TMP_RTT.Allcaps_E,"</allcaps>"},
                {TMP_RTT.Alpha_B,"<alpha=#[^>]*>"},
                {TMP_RTT.Bold_B,"<b>"},
                {TMP_RTT.Bold_E,"</b>"},
                {TMP_RTT.Color_B,"<color=[^>]*>"},
                {TMP_RTT.Color_E,"</color>"},
                {TMP_RTT.Cspace_B,"<cspace=[^>]*> "},
                {TMP_RTT.Cspace_E,"</cspace>"},
                {TMP_RTT.Font_B,"<font=[^>]*>"},
                {TMP_RTT.Font_E,"</font>"},
                {TMP_RTT.Font_Weight_B,"<font-weight=[^>]*>"},
                {TMP_RTT.Font_Weight_E,"</font-weight>"},
                {TMP_RTT.Gradient_B,"<gradient=[^>]*>"},
                {TMP_RTT.Gradient_E,"</gradient>"},
                {TMP_RTT.Italic_B,"<i>"},
                {TMP_RTT.Italic_E,"</i>"},
                {TMP_RTT.Indent_B,"<indent=[^>]*>"},
                {TMP_RTT.Indent_E,"</indent>"},
                {TMP_RTT.Line_Height_B,"<line-height=[^>]*>"},
                {TMP_RTT.Line_Height_E,"</line-height>"},
                {TMP_RTT.Line_Indent_B,"<line-indent=[^>]*>"},
                {TMP_RTT.Line_Indent_E,"</line-indent>"},
                {TMP_RTT.Link_B,"<link=[^>]*>"},
                {TMP_RTT.Link_E,"</link>"},
                {TMP_RTT.Lowercase_B,"<lowercase>"},
                {TMP_RTT.Lowercase_E,"</lowercase>"},
                {TMP_RTT.Margin_B,"<margin=[^>]*>"},
                {TMP_RTT.Margin_Left_B,"<margin-left>"},
                {TMP_RTT.Margin_Right_B,"<margin-right>"},
                {TMP_RTT.Margin_E,"</margin>"},
                {TMP_RTT.Mark_B,"<mark=[^>]*>"},
                {TMP_RTT.Mark_E,"</mark>"},
                {TMP_RTT.Mspace_B,"<mspace=[^>]*>"},
                {TMP_RTT.Mspace_E,"</mspace>"},
                {TMP_RTT.Nobr_B,"<nobr>"},
                {TMP_RTT.Nobr_E,"</nobr>"},
                {TMP_RTT.Noparse_B,"<noparse>"},
                {TMP_RTT.Noparse_E,"</noparse>"},
                {TMP_RTT.Page_B,"<page>"},
                {TMP_RTT.Position_B,"<pos=[^>]*>"},
                {TMP_RTT.Rotate_B,"<rotate=[^>]*>"},
                {TMP_RTT.Rotate_E,"</rotate>"},
                {TMP_RTT.Strikethrough_B,"<s>"},
                {TMP_RTT.Strikethrough_E,"</s>"},
                {TMP_RTT.Size_B,"<size=[^>]*>"},
                {TMP_RTT.Size_E,"</size>"},
                {TMP_RTT.Smallcaps_B,"<smallcaps>"},
                {TMP_RTT.Smallcaps_E,"</smallcaps>"},
                {TMP_RTT.Space_B,"<space=[^>]*>"},
                {TMP_RTT.Sprite_B,"<sprite[^>]*>"},
                {TMP_RTT.Style_B,"<style=[^>]*>"},
                {TMP_RTT.Style_E,"</style>"},
                {TMP_RTT.Subscript_B,"<sub>"},
                {TMP_RTT.Subscript_E,"</sub>"},
                {TMP_RTT.Superscript_B,"<sup>"},
                {TMP_RTT.Superscript_E,"</sup>"},
                {TMP_RTT.Underline_B,"<u>"},
                {TMP_RTT.Underline_E,"</u>"},
                {TMP_RTT.Uppercase_B,"<uppercase>"},
                {TMP_RTT.Uppercase_E,"</uppercase>"},
                {TMP_RTT.Voffset_B,"<voffset=[^>]*>"},
                {TMP_RTT.Voffset_E,"</voffset>"},
                {TMP_RTT.Width_B,"<width=[^>]*>"},
                {TMP_RTT.Width_E,"</width>"},
            };
        }

        /// <summary>
        /// Rich Text Tagのチェック
        /// </summary>
        /// <param name="checkText">判定する文字列</param>
        /// <returns>タグ判定のあった文字列の範囲のリスト</returns>
        public List<RangeInt> RTTCheck(string checkText)
        {
            //返す用のリスト
            List<RangeInt> rangeDataList = new List<RangeInt>();

            //全タグ分判定
            foreach (var match in tagMatchDictionary)
            {
                //タグがあるかどうか判定
                string tag = match.Value;
                var result = Regex.Match(checkText, tag);
                
                //判定が成功の限り無限ループ
                while (result.Success)
                {
                    //範囲設定
                    RangeInt range = new RangeInt(result.Index, result.Value.Length);
                    rangeDataList.Add(range);
                    //Debug.Log(tag + " is hit b:" + range.start + " e:" + range.end);
                    
                    //次の判定
                    result = result.NextMatch();
                }
            }

            return rangeDataList;
        }
    }
}