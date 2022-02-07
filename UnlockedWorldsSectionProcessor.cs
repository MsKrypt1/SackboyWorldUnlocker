namespace SackboySaveFix
{
    public sealed class UnlockedWorldsSectionProcessor : UnlockedWorldsArraySectionProcessor
    {
        public UnlockedWorldsSectionProcessor(in byte[] file, World selectedWorld) : base(file, selectedWorld)
        {
        }

        public override SectionProcessorResults Process() => Process(new BytesPattern("UnlockedWorlds"));
    }
}
