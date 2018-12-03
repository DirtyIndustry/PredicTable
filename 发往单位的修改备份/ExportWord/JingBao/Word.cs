using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using MSWord = Microsoft.Office.Interop.Word;
using System.Drawing.Imaging;
using O2S.Components.PDFRender4NET;

//O2S.Components.PDFRender4NET.dll

namespace PredicTable.ExportWord.JingBao
{
    public class Word
    {
        /// <summary>
        /// 将word转换为pdf
        /// </summary>
        /// <param name="file">word文件</param>
        /// <param name="mapName">pdf保存地址及pdf文件名,为null或空是和word名相同</param>
        public void WordToPDF(FileInfo file,string filepaths, string pdfName)
        {
            if (file.Exists)
            {
                object filepath = file.FullName;
                string filehzm = Path.GetFileName(filepath.ToString()).Replace(Path.GetFileNameWithoutExtension(filepath.ToString()), "");
                if (filehzm == ".doc" || filehzm == ".docx")
                {
                    if (pdfName == null || pdfName.Replace(" ", "") == "")
                    {
                        pdfName = filepaths + Path.GetFileNameWithoutExtension(filepath.ToString()) + ".pdf";
                    }
                    string pdfpath = filepaths + Path.GetFileNameWithoutExtension(file.ToString()).ToString();
                    //word转pfd
                    MSWord.Application app = new MSWord.Application();
                    object unknow = Type.Missing;
                    app.Visible = false;
                    MSWord.Document doc = app.Documents.Open(ref filepath, ref unknow, ref unknow, ref unknow,
                                                            ref unknow, ref unknow, ref unknow, ref unknow,
                                                            ref unknow, ref unknow, ref unknow, ref unknow,
                                                            ref unknow, ref unknow, ref unknow, ref unknow);
                    MSWord.WdStatistic stat = MSWord.WdStatistic.wdStatisticPages;
                    int num = doc.ComputeStatistics(stat, ref unknow);
                    if (File.Exists(pdfName))
                    {
                        File.Delete(pdfName);
                    }

                    doc.ExportAsFixedFormat(pdfName, MSWord.WdExportFormat.wdExportFormatPDF, false, MSWord.WdExportOptimizeFor.wdExportOptimizeForPrint, MSWord.WdExportRange.wdExportFromTo,
                                            1, num, MSWord.WdExportItem.wdExportDocumentContent, false, true, MSWord.WdExportCreateBookmarks.wdExportCreateNoBookmarks, true, true, false, unknow);
                    app.Documents.Close(MSWord.WdSaveOptions.wdDoNotSaveChanges, ref unknow, ref unknow);
                    app.Quit(ref unknow, ref unknow, ref unknow);
                }
                else
                    throw new Exception("文件不是word文件");
            }
            else
                throw new Exception("查找不到文件");
        }

        /// <summary>
        /// pdf转为image
        /// </summary>
        /// <param name="file">pdf文件</param>
        /// <param name="mapPath">图片存地址</param>
        /// <param name="mapname">图片名不含后缀名,为null或空是和word名相同</param>
        /// <param name="imageFormat">图片保存格式</param>
        /// <param name="definition">图片清晰度1-10,默认为2</param>
        public void PdfToImage(FileInfo file, string mapPath, string mapname, ImageFormat imageFormat, int definition = 2)
        {
            if (file.Exists)
            {
                string pdfInputPath = file.FullName;
                string filehzm = Path.GetFileName(pdfInputPath.ToString()).Replace(Path.GetFileNameWithoutExtension(pdfInputPath.ToString()), "");
                if (filehzm == ".pdf")
                {
                    if (mapPath == null || mapPath.Replace(" ", "") == "")
                    {
                        mapPath = "";
                    }
                    if (mapname == null || mapname.Replace(" ", "") == "")
                    {
                        mapname = Path.GetFileNameWithoutExtension(file.FullName.ToString()) + "." + imageFormat.ToString();
                    }
                    if (imageFormat == null)
                    {
                        imageFormat = ImageFormat.Bmp;
                    }
                    List<Bitmap> list_bmp = new List<Bitmap>();
                    PDFFile pdfFile = PDFFile.Open(pdfInputPath);
                    if (!Directory.Exists(mapPath))
                    {
                        Directory.CreateDirectory(mapPath);
                    }
                    for (int i = 1; i <= pdfFile.PageCount; i++)
                    {
                        Bitmap pageImage = pdfFile.GetPageImage(i - 1, 56 * (int)definition);
                        list_bmp.Add(pageImage);
                    }
                    pdfFile.Dispose();
                    int height = 0;
                    foreach (Bitmap bit in list_bmp)
                    {
                        height += bit.Height;
                    }
                    Bitmap newBmp = new Bitmap(list_bmp[0].Width, height);
                    Graphics gra = Graphics.FromImage(newBmp);
                    height = 0;
                    foreach (Bitmap bit in list_bmp)
                    {
                        gra.DrawImage(bit, 0, height, bit.Width, bit.Height);
                        height += bit.Height;
                    }
                    string bmpname = Path.GetFileNameWithoutExtension(pdfInputPath.ToString()).ToString();
                    newBmp.Save(mapPath + mapname, imageFormat);
                    File.Delete(pdfInputPath);
                }
            }
        

        }

        /// <summary>
        /// 将word转换为图片
        /// </summary>
        /// <param name="file">word文件</param>
        /// <param name="filepath">word文件地址</param>
        ///<param name="imagepath">图片存地址</param> 
        /// <param name="imagename">图片名不含后缀名,为null或空是和word名相同</param>
        /// <param name="imageFormat">图片保存格式</param>
        /// <param name="definition">图片清晰度1-10,默认为2</param>
        public void WordToImage(string files,string filepath, string imagepath, string imagename, ImageFormat imageFormat , int definition = 2)
        {
            FileInfo file = new FileInfo(filepath+"\\"+files);
            if (file.Exists)
            {
                WordToPDF(file, filepath, "");
                string pdfInputPath = filepath + Path.GetFileNameWithoutExtension(file.FullName.ToString()) + ".pdf";
                if (imagepath == null || imagepath.Replace(" ", "") == "")
                {
                    imagepath = "";
                }
                if (imagename == null || imagename.Replace(" ", "") == "")
                {
                    imagename = Path.GetFileNameWithoutExtension(file.FullName.ToString()) + "." + imageFormat.ToString();
                }
                if (imageFormat == null)
                {
                    imageFormat = ImageFormat.Bmp;
                }
                List<Bitmap> list_bmp = new List<Bitmap>();
                PDFFile pdfFile = PDFFile.Open(pdfInputPath);
                if (!Directory.Exists(imagepath))
                {
                    Directory.CreateDirectory(imagepath);
                }
                for (int i = 1; i <= pdfFile.PageCount; i++)
                {
                    Bitmap pageImage = pdfFile.GetPageImage(i - 1, 56 * (int)definition);
                    list_bmp.Add(pageImage);
                }
                pdfFile.Dispose();
                int height = 0;
                foreach (Bitmap bit in list_bmp)
                {
                    height += bit.Height;
                }
                Bitmap newBmp = new Bitmap(list_bmp[0].Width, height);
                Graphics gra = Graphics.FromImage(newBmp);
                height = 0;
                foreach (Bitmap bit in list_bmp)
                {
                    gra.DrawImage(bit, 0, height, bit.Width, bit.Height);
                    height += bit.Height;
                }
                string bmpname = Path.GetFileNameWithoutExtension(pdfInputPath.ToString()).ToString();
                newBmp.Save(imagepath + "\\" + imagename+".png", imageFormat);
                File.Delete(pdfInputPath);
            }
        }
    }
}
