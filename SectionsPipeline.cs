using Spectre.Console;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace SackboySaveFix
{
    public sealed class SectionsPipeline
    {
        private readonly ISectionProcessor[] _pipeline;

        public SectionsPipeline(in byte[] file, World selectedWorld) =>
            _pipeline = new ISectionProcessor[] {
                    new UnlockedWorldsSectionProcessor(file, selectedWorld),
                    new UnlockedWorldTransitionCannonsSectionProcessor(file, selectedWorld)
            };

        public IReadOnlyCollection<SectionProcessorResults> Run() =>
            _pipeline
                .Select(section => section.Process())
                .OrderBy(result => result.OriginalSectionStart)
                .ToImmutableList();
    }
}
