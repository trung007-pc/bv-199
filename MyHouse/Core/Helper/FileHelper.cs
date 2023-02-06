﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Core.Const;
using Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Core.Helper
{
    public static class FileHelper
    {
        public static async Task<FileModel> UploadFile(IFormFile file,string basePath,List<string> ExtensionsConstraint,string baseUri = "")
        {
            string directoryPath = "";
            var filePath = "";
            var fileDto = new FileModel();
                if (file.Length > 0)
                {
                    var ext = Path.GetExtension(file.FileName);
    
                    if (!ExtensionsConstraint.Any(x => x.Contains(ext)))
                    {
                        throw new GlobalException(HttpMessage.InvalidExtension, HttpStatusCode.BadRequest);
                    }

                    var directory = $"{DateTime.Now:dd-MM-yyyy}";
                    directoryPath = Path.GetFullPath(Path.Combine(basePath,directory));
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    var fileName = $"{DateTime.Now:yyyyMMddHHmmss}_" + file.FileName;
                    filePath = Path.Combine(directoryPath,fileName);
                    using (var fileStream = new FileStream(filePath , FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    fileDto.Path = filePath;
                    fileDto.FileName =Path.Combine(directory , fileName);
                    if (baseUri != "")
                    {
                        fileDto.Url = baseUri + fileDto.FileName;
                    }
                    return fileDto;
                }
                else
                {
                    return fileDto;
                }
           
        }

        public static void WriteLog(Exception e,string basePath)
        {
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }
            
            string filePath = Path.GetFullPath(Path.Combine(basePath,$"{DateTime.Now:dd-MM-yyyy}.txt"));
            StreamWriter sw;

            if (!File.Exists(filePath))
            {
               sw = File.CreateText(filePath);
            }
            else
            {
                sw = File.AppendText(filePath);
            }


            sw.WriteLine("----------------------------------------");
            sw.WriteLine($"Time:{DateTime.Now:dd-MM-yyyy-->HH/mm}");
            sw.WriteLine($"Error:{e.Message}");
            sw.WriteLine(e.StackTrace);
            sw.WriteLine("----------------------------------------");

            sw.Flush();
            sw.Close();
        }
        
  
        
        
    }

    public class FileModel
    {
        public string Path { get; set; }
        public string FileName { get; set;}
        
        public string Url { get; set; }
    }
}