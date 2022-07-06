namespace ProjectAnalyser.Utils;

public static class PathExcluding
{
    private const string ExcludedFoldersFile = @"./Resources/FoldersExcluded.txt";
    
    public static void ExcludeFolder(ref List<string> paths, string excludedFolder)
    {
        paths = paths.Except(paths.Where(path => path.Split(new char[] { '\\', '/' }).Contains(excludedFolder)))
            .ToList();
    }

    public static void ExcludedFoldersFromFile(ref List<string> paths)
    {
        var excludedFolders = GetExcludedFoldersFromFile();
        paths = paths.Except(paths.Where(path => path.Split(new char[] { '\\', '/' }).Any(excludedFolders.Contains))
            ).ToList();
    }



    private static IEnumerable<string> GetExcludedFoldersFromFile() =>
        File.ReadLines(ExcludedFoldersFile);
}