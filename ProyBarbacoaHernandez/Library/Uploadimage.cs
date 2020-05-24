using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProyBarbacoaHernandez.Library
{
    public class Uploadimage
    {
        public async Task copiarImagenAsync(IFormFile AvatarImage, string fileName, IHostingEnvironment environment, string carpeta, String imagen)
        {
            if (null == AvatarImage)
            {
                String archivoOrigen;
                var destFileName = environment.ContentRootPath + "/wwwroot/images/fotos/" + carpeta + "/" + fileName;
                if (imagen == null)
                {
                    archivoOrigen = environment.ContentRootPath + "/wwwroot/images/fotos/" + carpeta + "/default.png";
                    File.Copy(archivoOrigen, destFileName, true);
                }
                else
                {
                    archivoOrigen = environment.ContentRootPath + "/wwwroot/images/fotos/" + carpeta + "/" + imagen + ".png";
                    if (fileName != imagen + " .png")
                    {
                        File.Copy(archivoOrigen, destFileName, true);
                        File.Delete(archivoOrigen);
                    }
                }
                
            }
            else
            {
                var filePath = Path.Combine(environment.ContentRootPath, "wwwroot/images/fotos/" + carpeta, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await AvatarImage.CopyToAsync(stream);
                }
            }
        }
    }
}
