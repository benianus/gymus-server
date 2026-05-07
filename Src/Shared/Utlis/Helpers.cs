using gymus_server.Shared.Dtos;

namespace gymus_server.Shared.Utlis;

public static class Helpers {
    /**
     * <summary>Check if the comes from client side is valid</summary>
     * <param name="id">Id comes from url path parameter</param>
     * <returns>returns true if the id is valid, otherwise return false</returns>
     */
    public static bool IsIdValid(int id) => int.IsNegative(id) || id == 0;

    /**
     * <summary>upload file comes from multipart form-data request to wwwroot folder.</summary>
     * <summary>the function return the relative path of the file, which will be stored in database later.</summary>
     * <summary>this path will be sent to client and used in each request.</summary>
     * <param name="file">the file uploaded</param>
     * <returns>returns the relative path of the file</returns>
     */
    public static async Task<string?> UploadFile(IFormFile? file) {
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

    /// <summary>
    ///     delete file from the uploads folder in wwwroot directory
    /// </summary>
    /// <param name="filePath">the relative file path</param>
    /// <remarks>don't add wwwroot to the file path, it will be added automatically in the function</remarks>
    public static void DeleteFile(string filePath) {
        var path = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            filePath.TrimStart('/', '\\')
        );
        if (File.Exists(path)) File.Delete(path);
    }

    /**
     * <summary>
     *     Map the response comes from data based to paged response dto, this function is generic &
     *     static
     * </summary>
     * <param name="pageNumber">page number from the url query parameter, default is 1</param>
     * <param name="pageSize">send the page size param comes from url query parameter, default is 9</param>
     * <param name="totalItems">total items after pagination</param>
     * <param name="value">data comes from database</param>
     * <returns>return paged response object</returns>
     */
    public static PagedResponse<ApiResponse<List<T>>> ToPagedResponse<T>(
        int pageNumber,
        int pageSize,
        long totalItems,
        List<T> value
    ) {
        var data = new ApiResponse<List<T>>(value);
        var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
        var hasNextPage = pageNumber < totalPages;
        var hasPreviousPage = pageNumber > 1;

        return new PagedResponse<ApiResponse<List<T>>>(
            data,
            pageSize,
            totalItems,
            totalPages,
            hasNextPage,
            hasPreviousPage
        );
    }
}