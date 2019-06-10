using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace EPaperDemo2
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
          //  AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            //获取加载失败的程序集的全名
           
            var assName = new AssemblyName(args.Name).FullName;
            Console.WriteLine("assName" + assName);
            if (args.Name == "zxing, Version=0.16.2.0, Culture=neutral, PublicKeyToken=4e88037ac681fe60")
            {
                //读取资源
                Console.WriteLine("ININ");
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("EPaperDemo2.zxing.dll"))
                {
                    var bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, (int)stream.Length);
                    return Assembly.Load(bytes);//加载资源文件中的dll,代替加载失败的程序集
                }
            }
            else if (args.Name == "zxing.presentation, Version=0.16.2.0, Culture=neutral, PublicKeyToken=4e88037ac681fe60")
            {
                //读取资源
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("EPaperDemo2.zxing.presentation.dll"))
                {
                    var bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, (int)stream.Length);
                    return Assembly.Load(bytes);//加载资源文件中的dll,代替加载失败的程序集
                }
            }
            throw new DllNotFoundException(assName);
        }
    }
}
