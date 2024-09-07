using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Helper
{
    public class DocumentSettings
    {
        public  static string UploadFile(IFormFile file , string folderName)
        {
            // Get Folder Path
            //var folderPath = @"D:\\Route _Diploma\\Back_end\\.NET\\MVC\\Session03\\Demo03\\Company.Web\\wwwroot\\Files\\Images\\";
            
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files\\", folderName);

            //2. Get fileName
            var filename = $"{Guid.NewGuid()}_{file.FileName}";
            
            //3. Combine FolderPath + FilePath
            var filePath = Path.Combine(folderPath, filename);

            //4. Save File
            using var fileStream = new FileStream(filePath, FileMode.Create);
            
            // 5. make copy my file tok fileStream
            file.CopyTo(fileStream);

            return filename;
        }
    }
}
