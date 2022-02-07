namespace SackboySaveFix
{
    public sealed class UnlockedWorldTransitionCannonsSectionProcessor : UnlockedWorldsArraySectionProcessor
    {
        public UnlockedWorldTransitionCannonsSectionProcessor(in byte[] file, World selectedWorld) : base(file, selectedWorld)
        {
        }

        public override SectionProcessorResults Process() => Process(new BytesPattern("UnlockedWorldTransitionCannons"));
    }
}
