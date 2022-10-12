namespace FooterAppender.Interfaces;

public interface IFooter
{
    /// <summary>
    /// Append SHA-256 to footer.
    /// </summary>
    /// <param name="path">File Path</param>
    /// <param name="hash">Hash of file</param>
    /// <param name="separator">The separator added at the end of the tmp file</param>
    /// <returns></returns>
    void AppendSha256Async(string path, string hash, char separator);
}