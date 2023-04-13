using ProjectAnalyser.Utils;
using ProjectAnalyser.Records;

const bool VERBOSE = false;


if (!ArgsChecker.CheckArgs(args))
{
        Console.WriteLine(@$"There is an error with your first arg");
        Environment.Exit(0);
}

StartAnalyze();


void StartAnalyze()
{
        var fileTypes = new List<FileType>();
        List<string> allFilesOfTheFolder = FileUtils.GetAllSubFiles(args[0]).ToList();
        PathExcluding.ExcludedFoldersFromFile(ref allFilesOfTheFolder);

        foreach (var filename in allFilesOfTheFolder)
        {
                if (VERBOSE)
                        Console.WriteLine(filename);
                var fileExtension = FileUtils.GetFileExtension(filename);
                if (!FileTypeExists(fileTypes, fileExtension))
                        fileTypes.Add(new FileType(fileExtension));

                fileTypes[GetIndexOfFileTypeInListFromItsExtension(fileTypes, fileExtension)].LinesCount +=
                        FileUtils.GetLineCount(filename);
                fileTypes[GetIndexOfFileTypeInListFromItsExtension(fileTypes, fileExtension)].CharsCount +=
                        FileUtils.GetCharsCount(filename);
        }

        var maxExtensionLength = GetMaxLengthOfFileTypesExtension(fileTypes);
        PrintHeaders(maxExtensionLength);
        PrintResults(fileTypes, maxExtensionLength);
}

void PrintResults(List<FileType> fileTypes, int maxExtensionLength)
{
        foreach (var fileType in fileTypes)
                Console.WriteLine(fileType.ToStringFormatted(maxExtensionLength + 3));
}

void PrintHeaders(int maxExtensionLength)
{
        const string FILE_EXTENSION = "File extension";
        
        Console.WriteLine($"{FILE_EXTENSION + string.Concat(Enumerable.Repeat(" ", maxExtensionLength - FILE_EXTENSION.Length))}:line count - chars count");
}


bool FileTypeExists(List<FileType> fileTypes, string extension) =>
        fileTypes.Count(item => item.FileExtension == extension) == 1;

int GetIndexOfFileTypeInListFromItsExtension(List<FileType> fileTypes, string extension) =>
        fileTypes.Select((value, index) => new { value, index }).Where(item => item.value.FileExtension == extension)
                .Select(item => item.index).First();

int GetMaxLengthOfFileTypesExtension(List<FileType> fileTypes) =>
        fileTypes.Select(fileType => fileType.FileExtension.Length).Prepend(0).Max();