using System.Security.Cryptography;

namespace Core;

public static class FileHelper
{
    public static string GenerateRandomFileName()
    {
        return $"ByteScoutPdfConverter-{DateTime.UtcNow:yyyy-M-d}-{DateTime.UtcNow.Ticks}";
    }

    public static async Task<string> GetChecksum(string path)
    {
        using var sha = HashAlgorithm.Create("SHA-256");
        if (sha == null)
            return "";
        await using var stream = File.OpenRead(path);
        var hash = await sha.ComputeHashAsync(stream);
        return BitConverter.ToString(hash).Replace("-", string.Empty);
    }
}