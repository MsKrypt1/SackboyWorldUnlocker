namespace SackboySaveFix
{
    public sealed record SectionProcessorResults(int OriginalSectionStart, int OriginalSectionEnd, in byte[] NewSection);
}
