namespace gymus_server.Shared.Utlis;

public static class Helpers
{
    public static bool IsIdValid(int id) { return int.IsNegative(id) || id == 0; }

    public static async Task<string?> UploadFile(IFormFile? file)
    {
        if (file == null) return null;
        /***
         * validate the length of the file
         * if 0 throw exception that is empty
         */
        if (file.Length == 0) throw new Exception("File is empty");

        /***
         * validate the size of the file
         * if exceed throw exception that that is exceeded
         */
        if (file.Length > 1024 * 512) throw new Exception("File is more than 5 mb");

        /***
         *
         */
        var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");

        if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

        /***
         * validate the extensions allowed
         */
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf" };
        var fileExtension = Path.GetExtension(file.FileName).ToLower();

        if (!allowedExtensions.Contains(fileExtension))
            throw new Exception("invalid file extension, try [.jpg, .jpeg, .png,.pdf]");

        var fileName = $"{Guid.NewGuid()}{fileExtension}";
        var filePath = Path.Combine(uploadFolder, fileName);

        var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);
        await stream.DisposeAsync();
        stream.Close();

        var relativePath = Path.Combine("/Uploads/", fileName);
        return relativePath;
    }

    public static void DeleteFile(string filePath)
    {
        var path = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            filePath.TrimStart('/', '\\')
        );
        if (File.Exists(path)) File.Delete(path);
    }
}