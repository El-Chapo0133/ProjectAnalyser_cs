namespace ProjectAnalyser.Records;

public record FileType
{
    public string FileExtension { get; init; }
    public int LinesCount { get; set; }
    // public static bool operator ==(FileType self, FileType other) =>
    //     self.FileExtension == other.FileExtension && self.LinesNumbers == other.LinesNumbers;

    public FileType(string extension, int linesCount)
    {
        FileExtension = extension;
        LinesCount = linesCount;
    }

    public override string ToString()
    {
        return $@"{FileExtension}:{LinesCount}";
    }
    public string ToStringFormatted(int spacesNumber)
    {
        return $@"{FileExtension + string.Concat(Enumerable.Repeat(" ", spacesNumber - FileExtension.Length))}:{LinesCount}";
    }
}