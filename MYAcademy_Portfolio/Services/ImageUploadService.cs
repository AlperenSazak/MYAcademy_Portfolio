namespace MYAcademy_Portfolio.Services
{
    public class ImageUploadService
    {
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };
        private const long MaxFileSize = 2 * 1024 * 1024; // 2 MB

        private readonly IWebHostEnvironment _environment;

        public ImageUploadService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public async Task<ImageUploadResult> SaveAsync(IFormFile file, string folder)
        {
            if (file == null || file.Length == 0)
            {
                return ImageUploadResult.Fail("Lütfen bir görsel seçiniz.");
            }

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!AllowedExtensions.Contains(extension))
            {
                return ImageUploadResult.Fail("Sadece .jpg, .jpeg, .png veya .webp dosyaları yüklenebilir.");
            }

            if (file.Length > MaxFileSize)
            {
                return ImageUploadResult.Fail("Görsel boyutu en fazla 2 MB olabilir.");
            }

            var safeFileName = $"{Guid.NewGuid():N}{extension}";
            var uploadFolder = Path.Combine(_environment.WebRootPath, "uploads", folder);
            Directory.CreateDirectory(uploadFolder);

            var fullPath = Path.Combine(uploadFolder, safeFileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return ImageUploadResult.Ok($"/uploads/{folder}/{safeFileName}");
        }

        public void Delete(string? imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath) || !imagePath.StartsWith("/uploads/"))
            {
                return;
            }

            var fullPath = Path.Combine(_environment.WebRootPath, imagePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }

    public class ImageUploadResult
    {
        public bool Success { get; private set; }
        public string? Path { get; private set; }
        public string? ErrorMessage { get; private set; }
        public static ImageUploadResult Ok(string path) => new ImageUploadResult { Success = true, Path = path };
        public static ImageUploadResult Fail(string error) => new ImageUploadResult { Success = false, ErrorMessage = error };
    }
}
