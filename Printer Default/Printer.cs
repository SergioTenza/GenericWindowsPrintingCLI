using System;
using System.Drawing;
using System.Drawing.Printing;

namespace Printer_Default
{
    public class Printer
    {
        public string printerName { get; set; }
        public short selectedCopies { get; set; }
        public string filePath { get; set; }
        public bool landscape { get; set; }

        public bool printTask()
        {
            try
            {   
                PrintDocument pd = new PrintDocument();
                PrintController pc = new StandardPrintController();
                pd.PrintController = pc;
                pd.DefaultPageSettings.PrinterSettings.PrinterName = printerName;
                pd.DefaultPageSettings.Landscape = landscape; //true or false! 
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
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            
        }
    }
   
}
