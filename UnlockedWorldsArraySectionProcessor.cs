using System;
using System.Collections.Immutable;
using System.Text;

namespace SackboySaveFix
{
    public abstract class UnlockedWorldsArraySectionProcessor : ISectionProcessor
    {
        private readonly byte[] _file;
        private readonly World _selectedWorld;

        public UnlockedWorldsArraySectionProcessor(in byte[] file, World selectedWorld)
        {
            _file = file;
            _selectedWorld = selectedWorld;
        }

        protected SectionProcessorResults Process(BytesPattern inputPattern)
        {
            // The unlocked worlds section looks like:
            // UnlockedWorlds: { ArrayProperty: { SizeInBytes: byte, EnumProperty: { ArrayLength:byte, Worlds: ELevelType[] } } }

            // First find where the label starts in file
            var unlockedWorldsSectionStart = inputPattern.FindPattern(_file);

            // Find the ArrayLength of the section
            var relativeArrayLengthIdx = inputPattern.Bytes.Length + 0x2d;
            var originalArrayLengthIdx = unlockedWorldsSectionStart + relativeArrayLengthIdx;
            var originalArrayLength = _file[originalArrayLengthIdx];

            // The worlds in the section are always in order
            // Number of items == last unlocked world
            var selectedWorldNumber = (byte)_selectedWorld;
            if (originalArrayLength >= selectedWorldNumber)
            {
                throw new SectionProcessorException(_selectedWorld);
            }

            // Find the SizeInBytes of the section
            var relativeSizeInBytesIdx = inputPattern.Bytes.Length + 0x13;
            var originalSizeInBytes = _file[unlockedWorldsSectionStart + relativeSizeInBytesIdx];

            // Find where the original section ends for future reference
            var originalSectionEnd = originalSizeInBytes + originalArrayLengthIdx;

            // Build new worlds array
            var modifiedWorlds = ImmutableArray.CreateBuilder<byte>();

            // Set the ArrayLength of the modified section
            // The value is expected to be 4 bytes long
            modifiedWorlds.AddRange(selectedWorldNumber, 0x00, 0x00, 0x00);

            for(var i = 1; i <= selectedWorldNumber; i++)
            {
                var fullName = $"ELevelType::{Enum.GetName((World)i)}";

                // Add world name size + NULL terminator
                // The value is expected to be 4 bytes long
                modifiedWorlds.AddRange((byte)(fullName.Length + 1), 0x00, 0x00, 0x00);

                modifiedWorlds.AddRange(Encoding.ASCII.GetBytes(fullName));

                // Add NULL terminator
                modifiedWorlds.Add(0x00);
            }

            var modifiedSizeInBytes = modifiedWorlds.Count;

            // Copy original header - everything before the ArrayLength property
            var modifiedSection = new byte[relativeArrayLengthIdx + modifiedSizeInBytes];
            Array.Copy(_file, unlockedWorldsSectionStart, modifiedSection, 0, relativeArrayLengthIdx);

            // Set the correct SizeInBytes
            // Max possible value for all worlds would be 0x93, which is a byte
            modifiedSection[relativeSizeInBytesIdx] = (byte)modifiedSizeInBytes;

            // Copy new worlds array to the new section
            modifiedWorlds.ToImmutable().CopyTo(0, modifiedSection, relativeArrayLengthIdx, modifiedSizeInBytes);

            return new SectionProcessorResults(unlockedWorldsSectionStart, originalSectionEnd, modifiedSection);
        }

        public abstract SectionProcessorResults Process();
    }
}
