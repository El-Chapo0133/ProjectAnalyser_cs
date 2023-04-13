using ProjectAnalyser.Utils;
using ProjectAnalyser.Records;

const bool VERBOSE = false;


if (!ArgsChecker.CheckArgs(args))
{
        Console.WriteLine(@$"There is an error with your first arg, i should be the Project Path. Given arguments: {args.Length}");
        Environment.Exit(0);
}

StartAnalyze();


void StartAnalyze()
{
        var fileTypes = new List<FileType>();
        var allFilesOfTheFolder = FileUtils.GetAllSubFiles(args[0]).ToList();
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
        Console.WriteLine(@$"Total {string.Concat(Enumerable.Repeat(" ", 7))}:{GetTotalNumberOfLines(fileTypes)}");
}

void PrintHeaders(int maxExtensionLength)
{
        const string FILE_EXTENSION = "File extension";

        Console.WriteLine($"{FILE_EXTENSION + string.Concat(Enumerable.Repeat(" ", maxExtensionLength - FILE_EXTENSION.Length))}:line count - chars count");
}


bool FileTypeExists(IEnumerable<FileType> fileTypes, string extension) =>
        fileTypes.Count(item => item.FileExtension == extension) == 1;

int GetIndexOfFileTypeInListFromItsExtension(IEnumerable<FileType> fileTypes, string extension) =>
        fileTypes.Select((value, index) => new { value, index }).Where(item => item.value.FileExtension == extension)
                .Select(item => item.index).First();

int GetMaxLengthOfFileTypesExtension(IEnumerable<FileType> fileTypes) =>
        fileTypes.Select(fileType => fileType.FileExtension.Length).Prepend(0).Max();

int GetTotalNumberOfLines(IEnumerable<FileType> fileTypes) =>
        fileTypes.Select(fileType => fileType.FileExtension.Length).Sum();