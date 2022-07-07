namespace ProjectAnalyser.Utils;

public static class FileUtils
{
    public static string? ReadFile(string filename) =>
        !File.Exists(filename) ? null : new StreamReader(filename).ReadToEnd();
    public static void WriteFile(string? filename, string? data)
    {
        if (filename is null || data is null)
            return;

        var writer = CreateStreamWriter(filename);

        var lines = data.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

        foreach (var line in lines)
            writer.WriteLine(line);
        
        writer.Close();
    }
    public static FileStream? CreateFile(string filename, bool closeStream)
    {
        var directoryName = GetFolder(filename);
        if (directoryName is null)
            return null;

        if (!FolderExists(directoryName))
            Directory.CreateDirectory(directoryName);

        var fileStream = File.Create(filename);
        if (!closeStream) return fileStream;
        
        fileStream.Close();
        return null;
    }
    
    public static IEnumerable<string> GetAllSubFiles(string path) =>
        Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
    public static int GetLineCount(string path) =>
        File.ReadLines(path).Count();
    public static bool FileExists(string filename) =>
        File.Exists(filename);
    private static bool FolderExists(string filename) =>
        Directory.Exists(filename);
    private static string? GetFolder(string filename) =>
        Path.GetDirectoryName(filename);
    private static StreamWriter CreateStreamWriter(string filename) =>
        !File.Exists(filename) ? new StreamWriter(File.Create(filename)) : new StreamWriter(filename);
    public static string GetFileExtension(string filename) =>
        Path.GetExtension(filename);
    // string.Join('.', filename.Split('.')[1..]);
}