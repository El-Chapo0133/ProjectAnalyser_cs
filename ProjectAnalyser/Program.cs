using ProjectAnalyser;

if (!ArgsChecker.CheckArgs(args))
{
    Console.WriteLine(@$"There is an error with your first arg");
    Environment.Exit(0);
}

StartAnalyze();




void StartAnalyze()
{
    var fileTypes = new List<FileType>();
    var allFilesOfTheFolder = FileUtils.GetAllSubFiles(args[0]);

    foreach (var filename in allFilesOfTheFolder)
    {
        var fileExtension = FileUtils.GetFileExtension(filename);
        if (!FileTypeExists(fileTypes, fileExtension))
            fileTypes.Add(new FileType(fileExtension, 0));

        fileTypes[GetIndexOfFileTypeInListFromItsExtension(fileTypes, fileExtension)].LinesCount +=
            FileUtils.GetLineCount(filename);
    }
    
    PrintResults(fileTypes);
}

void PrintResults(List<FileType> fileTypes)
{
    var maxExtensionLength = GetMaxLengthOfFileTypesExtension(fileTypes);
    foreach (var fileType in fileTypes)
        Console.WriteLine(fileType.ToStringFormatted(maxExtensionLength + 3));
}



bool FileTypeExists(List<FileType> fileTypes, string extension) =>
    fileTypes.Where(item => item.FileExtension == extension).Count() == 1;

int GetIndexOfFileTypeInListFromItsExtension(List<FileType> fileTypes, string extension) =>
    fileTypes.Select((value, index) => new { value, index }).Where(item => item.value.FileExtension == extension)
        .Select(item => item.index).First();

int GetMaxLengthOfFileTypesExtension(List<FileType> fileTypes)
{
    var max = 0;
    foreach (var fileType in fileTypes)
        if (fileType.FileExtension.Length > max)
            max = fileType.FileExtension.Length;
    return max;
}