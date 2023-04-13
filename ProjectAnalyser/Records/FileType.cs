namespace ProjectAnalyser.Records;

public record FileType(string FileExtension, int LinesCount = 0, int CharsCount = 0)
{
        public int LinesCount { get; set; } = LinesCount;
        public int CharsCount { get; set; } = CharsCount;
        
        // public static bool operator ==(FileType self, FileType other) =>
        //     self.FileExtension == other.FileExtension && self.LinesNumbers == other.LinesNumbers;

        public override string ToString()
        {
                return $@"{FileExtension}:{LinesCount}:{CharsCount}";
        }

        public string ToStringFormatted(int spacesNumber)
        {
                return $@"{FileExtension + string.Concat(Enumerable.Repeat(" ", spacesNumber - FileExtension.Length))}:{LinesCount} - {CharsCount}";
        }
}