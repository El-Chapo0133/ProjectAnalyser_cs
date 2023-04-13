namespace ProjectAnalyser.Utils;

public static class FileUtils
{
        public static IEnumerable<string> GetAllSubFiles(string path) =>
                Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

        public static int GetLineCount(string path) =>
                File.ReadLines(path).Count();

        public static int GetCharsCount(string path) =>
                File.ReadAllText(path).Length;
        
        public static string GetFileExtension(string filename) =>
                // Path.GetExtension(filename); // take the last . and after is the "Extension" eg. foo.Designer.cs -> .cs
                string.Join('.', filename.Split('\\').Last().Split('.')[1..]); // take the first . and after is the "Extension" eg. foo.Designer.cs -> Designer.cs
}