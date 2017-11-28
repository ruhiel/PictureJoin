using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PictureJoin
{
    public class Options
    {
        //配列のオプション
        [OptionArray('i', Required = true, HelpText ="入力ファイルパス")]
        public string[] InputFiles { get; set; }

        [Option('o', Required = true, HelpText = "出力ファイル")]
        public string OutputFile { get; set; }

        //(3)HelpOption属性
        [HelpOption('h', "help")]
        public string GetUsage()
        {
            var title = ((System.Reflection.AssemblyTitleAttribute)Attribute.GetCustomAttribute(
                            System.Reflection.Assembly.GetExecutingAssembly(),
                            typeof(System.Reflection.AssemblyTitleAttribute))).Title;
            //ヘッダーの設定
            var head = new HeadingInfo(
                            title,
                            System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());

            var asmcpy =
                (System.Reflection.AssemblyCopyrightAttribute)
                Attribute.GetCustomAttribute(
                System.Reflection.Assembly.GetExecutingAssembly(),
                typeof(System.Reflection.AssemblyCopyrightAttribute));

            var regex = new Regex(@".+\s+(\d+)\s+(.+)");
            var m = regex.Match(asmcpy.Copyright);

            var help = new HelpText(head);
            if (m.Success)
            {
                help.Copyright = new CopyrightInfo(m.Groups[2].Value, int.Parse(m.Groups[1].Value));
            }

            help.AddPreOptionsLine("オプション一覧");

            //全オプションを表示(1行間隔)
            help.AdditionalNewLineAfterOption = true;
            help.AddOptions(this);

            return help.ToString();
        }
    }
}
