namespace Client.Actions
{
    public class PhotoFileAction
    {
        public static async Task<string> AddPhoto(IFormFile file, IWebHostEnvironment webHost)
        {
            string path = "/photos/" + file.FileName;

            using (var fileStream = new FileStream(webHost.WebRootPath + path, FileMode.Create))
                await file.CopyToAsync(fileStream);

            return path;
        }

        public static async Task<string> UpdatePhoto(IFormFile file, string lastPhotoUrl, IWebHostEnvironment webHost)
        {
            var delete = new FileInfo(webHost.WebRootPath + lastPhotoUrl);
            string path = "/photos/" + file.FileName;

            if (delete.Exists)
            {
                delete.Delete();

                using (var fileStream = new FileStream(webHost.WebRootPath + path, FileMode.Create))
                    await file.CopyToAsync(fileStream);
            }

            return path;        
        }
    }
}
