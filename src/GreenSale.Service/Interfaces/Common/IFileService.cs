﻿using Microsoft.AspNetCore.Http;

namespace GreenSale.Service.Interfaces.Common;

public interface IFileService
{
    public Task<string> UploadImageAsync(IFormFile image);
    public Task<string> DeleteImageAsync(string subpath);
}
