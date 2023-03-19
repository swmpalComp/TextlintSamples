using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EscSeqRichText
{
    public partial class EscSeqRichText: RichTextBox
    {
        private string _strEscText = "\u001b[";
        private Font _DefFont = null;

        public EscSeqRichText()
        {
            InitializeComponent();
            _DefFont = this.Font;
        }

        public string strEscText { get { return _strEscText; } set { _strEscText = value; } }

        public string EscSeqText
        {
            set
            {
                bool bolModeEsc = false;
                string strWorkSrc = value;

                Text = string.Empty;

                while (strWorkSrc.Length > 0)
                {
                    this.SelectionStart = this.Text.Length;
                    this.SelectionLength = 0;
                    if (bolModeEsc)
                    {
                        ///エスケープ中の処理
                        int intEscPos = strWorkSrc.IndexOf("m");
                        int intSemiPos = strWorkSrc.IndexOf(";");
                        if (intSemiPos >= 0 && intSemiPos < intEscPos)
                        {
                            intEscPos = intSemiPos;
                        }
                        else
                        {
                            bolModeEsc = false;
                        }

                        if (intEscPos < 0)
                        {
                            // 全て出力して終了。
                            Text += strWorkSrc;
                            strWorkSrc = String.Empty;
                        }
                        else
                        {
                            string str1 = strWorkSrc.Substring(0, intEscPos);       // 制御コードをちぎる
                            Font fnt = null;
                            // 制御
                            switch (str1)
                            {
                                case "0":
                                    this.SelectionFont = _DefFont;
                                    this.SelectionColor = Color.Empty;
                                    this.SelectionBackColor = Color.Empty;
                                    break;
                                case "1":
                                    fnt = new Font(SelectionFont.FontFamily,
                                                        SelectionFont.Size,
                                                        SelectionFont.Style | FontStyle.Bold);
                                    this.SelectionFont = fnt;
                                    break;
                                case "3":
                                    fnt = new Font(SelectionFont.FontFamily,
                                                        SelectionFont.Size,
                                                        SelectionFont.Style | FontStyle.Italic);
                                    this.SelectionFont = fnt;
                                    break;
                                case "4":
                                    fnt = new Font(SelectionFont.FontFamily,
                                                        SelectionFont.Size,
                                                        SelectionFont.Style | FontStyle.Underline);
                                    this.SelectionFont = fnt;
                                    break;
                                case "9":
                                    fnt = new Font(SelectionFont.FontFamily,
                                                        SelectionFont.Size,
                                                        SelectionFont.Style | FontStyle.Strikeout);
                                    this.SelectionFont = fnt;
                                    break;
                                case "22":
                                    fnt = new Font(SelectionFont.FontFamily,
                                                        SelectionFont.Size,
                                                        SelectionFont.Style ^ FontStyle.Bold);
                                    this.SelectionFont = fnt;
                                    break;
                                case "23":
                                    fnt = new Font(SelectionFont.FontFamily,
                                                        SelectionFont.Size,
                                                        SelectionFont.Style ^ FontStyle.Italic);
                                    this.SelectionFont = fnt;
                                    break;
                                case "24":
                                    fnt = new Font(SelectionFont.FontFamily,
                                                        SelectionFont.Size,
                                                        SelectionFont.Style ^ FontStyle.Underline);
                                    this.SelectionFont = fnt;
                                    break;
                                case "29":
                                    fnt = new Font(SelectionFont.FontFamily,
                                                        SelectionFont.Size,
                                                        SelectionFont.Style ^ FontStyle.Strikeout);
                                    this.SelectionFont = fnt;
                                    break;
                                case "30":
                                    this.SelectionColor = Color.Black;
                                    break;
                                case "31":
                                    this.SelectionColor = Color.Red;
                                    break;
                                case "32":
                                    this.SelectionColor = Color.Green;
                                    break;
                                case "33":
                                    this.SelectionColor = Color.Yellow;
                                    break;
                                case "34":
                                    this.SelectionColor = Color.Blue;
                                    break;
                                case "35":
                                    this.SelectionColor = Color.Magenta;
                                    break;
                                case "36":
                                    this.SelectionColor = Color.Cyan;
                                    break;
                                case "37":
                                    this.SelectionColor = Color.White;
                                    break;
                                case "39":
                                    this.SelectionColor = Color.Empty;
                                    break;
                                case "40":
                                    this.SelectionBackColor = Color.Black;
                                    break;
                                case "41":
                                    this.SelectionBackColor = Color.Red;
                                    break;
                                case "42":
                                    this.SelectionBackColor = Color.Green;
                                    break;
                                case "43":
                                    this.SelectionBackColor = Color.Yellow;
                                    break;
                                case "44":
                                    this.SelectionBackColor = Color.Blue;
                                    break;
                                case "45":
                                    this.SelectionBackColor = Color.Magenta;
                                    break;
                                case "46":
                                    this.SelectionBackColor = Color.Cyan;
                                    break;
                                case "47":
                                    this.SelectionBackColor = Color.White;
                                    break;
                                case "49":
                                    this.SelectionBackColor = Color.Empty;
                                    break;
                                case "90":
                                    this.SelectionColor = Color.DarkGray;
                                    break;
                                case "91":
                                    this.SelectionColor = Color.DeepPink;
                                    break;
                                case "92":
                                    this.SelectionColor = Color.DarkGreen;
                                    break;
                                case "93":
                                    this.SelectionColor = Color.Gold;
                                    break;
                                case "94":
                                    this.SelectionColor = Color.LightBlue;
                                    break;
                                case "95":
                                    this.SelectionColor = Color.DarkMagenta;
                                    break;
                                case "96":
                                    this.SelectionColor = Color.LightCyan;
                                    break;
                                case "97":
                                    this.SelectionColor = Color.LightGray;
                                    break;
                            }
                            strWorkSrc = strWorkSrc.Substring(intEscPos + 1);     // 制御コードとシーケンス自体を除外
                        }
                    }
                    else
                    {
                        ///そうでない処理
                        int intEscPos = strWorkSrc.IndexOf(_strEscText);
                        if (intEscPos < 0)
                        {
                            // 全て出力して終了。
                            this.SelectedText = strWorkSrc;
                            strWorkSrc = String.Empty;
                        }
                        else
                        {
                            string str1 = strWorkSrc.Substring(0, intEscPos);
                            this.SelectedText = str1;       // テキストの後ろに文字をそのまま出力する。
                            strWorkSrc = strWorkSrc.Substring(intEscPos).Trim(_strEscText.ToCharArray());     // 出力済み文字列とシーケンス自体を除外
                            bolModeEsc = true;      // モードを切り替える
                        }
                    }
                }

            }
        }
    }
}
