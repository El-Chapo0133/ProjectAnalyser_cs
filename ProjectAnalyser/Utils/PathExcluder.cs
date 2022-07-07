namespace ProjectAnalyser.Utils;

public static class PathExcluding
{
    private const string ExcludedFoldersFile = @"./Resources/FoldersExcluded.txt";
    
    private static void ExcludeFolder(ref List<string> paths, string excludedFolder) =>
        paths = paths.Except(paths.Where(path => path.Split(new char[] { '\\', '/' }).Contains(excludedFolder)))
            .ToList();
    
    private static void ExcludeFolders(ref List<string> paths, IEnumerable<string> excludedFolders) =>
        paths = paths.Except(paths.Where(path => path.Split(new char[] { '\\', '/' }).Any(excludedFolders.Contains))
        ).ToList();

    public static void ExcludedFoldersFromFile(ref List<string> paths) =>
        ExcludeFolders(ref paths, GetExcludedFoldersFromFile());
    



    private static IEnumerable<string> GetExcludedFoldersFromFile() =>
        File.ReadLines(ExcludedFoldersFile);
}