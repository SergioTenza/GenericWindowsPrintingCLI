using System.Drawing;
using System.Drawing.Printing;

string printerName = string.Empty;
short selectedCopies;
string filePath = string.Empty;
bool landscape = true;


if (args.Length > 0)
{
    try
    {
        selectedCopies = Convert.ToInt16(args[0]);
        if (selectedCopies <= 0)
        {
            Console.WriteLine("Number of copies cannot be Zero or negative");
            Environment.Exit(1);
        }
        filePath = args.Length > 1 && File.Exists(args[1]) ? args[1] : string.Empty;
        if (string.IsNullOrEmpty(filePath))
        {
            Console.WriteLine("Image file path cannot be empty or null.");
            Environment.Exit(1);
        }        
        printerName = args.Length > 2 ? string.Join(" ", args.Skip(2)) : string.Empty;
        if (string.IsNullOrEmpty(printerName))
        {
            Console.WriteLine("PrinterName cannot be empty or null.");
            Environment.Exit(1);
        }

        PrintDocument pd = new();
        PrintController pc = new StandardPrintController();
        pd.PrintController = pc;
        pd.DefaultPageSettings.PrinterSettings.PrinterName = printerName;
        pd.DefaultPageSettings.Landscape = landscape;
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
                    newWidth /= widthFactor;
                    newHeight /= widthFactor;
                }
                else
                {
                    newWidth /= heightFactor;
                    newHeight /= heightFactor;
                }
            }
            args2?.Graphics?.DrawImage(i, 0, 0, (int)newWidth, (int)newHeight);

        };
        pd.Print();
        Console.WriteLine($"[{selectedCopies}] Jobs of file [{filePath}] printed successfully on printer [{printerName}]");
        Environment.Exit(0);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        Environment.Exit(1);
    }
}
else
{
    Console.WriteLine("No parameters provided.");
    Console.WriteLine("Expected 4 Parameters");
    Console.WriteLine("Parameter 1: number of copies");
    Console.WriteLine("Parameter 2: Path to image");    
    Console.WriteLine("Parameter 3: PrinterName as it appears on Device Manager / Printers / on Windows");
    Environment.Exit(1);
}
