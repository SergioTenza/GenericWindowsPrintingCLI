using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Printer_Default
{
    class Program
    {
        public static string printerName = "CITIZEN CX-02";
        public static short selectedCopies = 1;
        public static string filePath = @"D:\descarga.jpg";

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                try
                {   
                    PrintDocument pd = new PrintDocument();
                    PrintController pc = new StandardPrintController();
                    pd.PrintController = pc;
                    pd.DefaultPageSettings.PrinterSettings.PrinterName = printerName;
                    pd.DefaultPageSettings.Landscape = false; //or false! 
                    pd.PrinterSettings.Copies = selectedCopies;
                    pd.PrintPage += (sender, args2) =>
                    {
                        Image i = Image.FromFile(filePath);

                        float newWidth = i.Width * 100 / i.HorizontalResolution;
                        float newHeight = i.Height * 100 / i.VerticalResolution;

                        float widthFactor = newWidth / args2.PageBounds.Width;
                        float heightFactor = newHeight / args2.PageBounds.Height;

                        if (widthFactor > 1 | heightFactor > 1)
                        {
                            if (widthFactor > heightFactor)
                            {
                                newWidth = newWidth / widthFactor;
                                newHeight = newHeight / widthFactor;
                            }
                            else
                            {
                                newWidth = newWidth / heightFactor;
                                newHeight = newHeight / heightFactor;
                            }
                        }
                        args2.Graphics.DrawImage(i, 0, 0, (int)newWidth, (int)newHeight);

                    };
                    pd.Print();

                }
                catch ( Exception ex1)
                {
                    Console.WriteLine(ex1);
                    Console.ReadKey();
                }
            }
            else
            {
                try 
                {
                    selectedCopies = Convert.ToInt16(args[0]);
                    filePath = args[1];

                    var length = args.Count();
                    List<string> list = new List<string>();
                    for (int i = 2; i < length; i++)
                    {
                        list.Add(args[i]);
                    }
                    string[] array = list.ToArray();

                    printerName = String.Join(" ", array);                                  

                    PrintDocument pd = new PrintDocument();
                    PrintController pc = new StandardPrintController();
                    pd.PrintController = pc;
                    pd.DefaultPageSettings.PrinterSettings.PrinterName = printerName;
                    pd.DefaultPageSettings.Landscape = true; //or false! 
                    pd.PrinterSettings.Copies = selectedCopies;
                    pd.PrintPage += (sender, args2) =>
                    {
                        Image i = Image.FromFile(filePath);

                        float newWidth = i.Width * 100 / i.HorizontalResolution;
                        float newHeight = i.Height * 100 / i.VerticalResolution;

                        float widthFactor = newWidth / args2.PageBounds.Width;
                        float heightFactor = newHeight / args2.PageBounds.Height;

                        if (widthFactor > 1 | heightFactor > 1)
                        {
                            if (widthFactor > heightFactor)
                            {
                                newWidth = newWidth / widthFactor;
                                newHeight = newHeight / widthFactor;
                            }
                            else
                            {
                                newWidth = newWidth / heightFactor;
                                newHeight = newHeight / heightFactor;
                            }
                        }
                        args2.Graphics.DrawImage(i, 0, 0, (int)newWidth, (int)newHeight);

                    };
                    pd.Print();
                }
                catch(Exception ex)
                {
                    Environment.Exit(0);
                    Console.WriteLine(ex);
                    Console.ReadKey();
                }               
            }
        }         
    }    
}