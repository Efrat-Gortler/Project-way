//using System;
//using System.IO;
////Add Aspose.OCR for .NET package reference
////Use following namespaces to Extract Text from Image
//using Aspose.OCR;


//namespace ExtractTextFromImage
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            //Set Aspose license before extracting text from image
//            //using Aspose.OCR for .NET 
//            Aspose.OCR.License AsposeOCRLicense = new Aspose.OCR.License();
//            AsposeOCRLicense.SetLicense(@"c:\asposelicense\license.lic");


//            //Create an instance of AsposeOcr class before you can apply
//            //OCR on an image to extract the text from it
//            AsposeOcr ExtractTextFromImage = new AsposeOcr();

//            //Read image using RecognizeImage method on which OCR need to be applied for text extraction
//            string TextExtractedFromImage = ExtractTextFromImage.RecognizeImage("ExampleOCRImageToExtractText.jpg");

//            //Save extracted text to a text file using File Stream and StreamWriter
//            //classes of System.IO
//            FileStream FStream = new FileStream("ExtractTextFromImageUsingOCR.txt", FileMode.Create);
//            StreamWriter SWriter = new StreamWriter(FStream);
//            //Write extracted text to the file
//            SWriter.WriteLine(TextExtractedFromImage);
//            SWriter.Flush();
//            //Close FileStream and StreamWriter bojects
//            SWriter.Close();
//            FStream.Close();

//        }
//    }
//}
