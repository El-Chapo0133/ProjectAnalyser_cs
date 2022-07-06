namespace ProjectAnalyser.Utils;

public static class ArgsChecker
{
    public static bool CheckArgs(string[] args) =>
        CheckArgsCount(args) &&
        CheckFolderAtFirstArgsExists(args);
    

    private static bool CheckArgsCount(IEnumerable<string> args) =>
        args.Any();

    private static bool CheckFolderAtFirstArgsExists(IReadOnlyList<string> args) =>
        Directory.Exists(args[0]);
}