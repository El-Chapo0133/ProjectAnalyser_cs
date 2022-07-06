namespace ProjectAnalyser;

public static class ArgsChecker
{
    public static bool CheckArgs(string[] args) =>
        CheckArgsCount(args) &&
        CheckFolderAtFirstArgsExists(args);
    

    private static bool CheckArgsCount(string[] args) =>
        args.Count() > 0;

    private static bool CheckFolderAtFirstArgsExists(string[] args) =>
        Directory.Exists(args[0]);
}