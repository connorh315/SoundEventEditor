using System;
using System.Collections.Generic;
using System.IO;

namespace SoundEventEditor.SoundEvents
{
    public abstract class SoundEvent
    {
        public List<SoundEvent> Connections;
        public List<SoundEvent> Children = [];

        public abstract void Parse(RawFile rawFile);

        public abstract int Version { get; }

        public virtual void Write(RawFile rawFile)
        {
            rawFile.WriteString("TVES");
            rawFile.WriteInt(1, true);
            rawFile.WriteInt(Version, true);
            rawFile.WriteInt(-1); // Checksum :)
        }

        public static SoundEvent GetSoundEvent(RawFile rawFile)
        {
            string test = rawFile.ReadString(4);

            if (test != "TVES")
            {
                throw new DataMisalignedException("Misaligned!");
            }

            uint version = rawFile.ReadUInt(true);
            uint type    = rawFile.ReadUInt(true);
            uint id      = rawFile.ReadUInt(true);

            SoundEvent soundEvent = type switch
            {
                0  => new SoundEvent0(),
                1  => new SoundEvent1(),
                2  => new SoundEvent2(),
                3  => new SoundEvent3(),
                4  => new SoundEvent4(),
                6  => new SoundEvent6(),
                7  => new SoundEvent7(),
                8  => new SoundEvent8(),
                9  => new SoundEvent9(),
                10 => new SoundEvent10(),
                _  => throw new AccessViolationException($"Unknown SoundEvent type: {type}"),
            };

            soundEvent.Parse(rawFile);

            return soundEvent;
        }

        public static SoundEvent OpenFile(string filePath)
        {
            RawFile file = new(filePath);

            _ = file.ReadInt(true); // File length

            return GetSoundEvent(file);
        }

        public static void SaveFile(string filePath, SoundEvent soundEvent)
        {
            using RawFile file = new(filePath);

            file.WriteInt(0); // The length of the file, fixed at the end

            soundEvent.Write(file);

            file.Seek(0, SeekOrigin.Begin);

            file.WriteInt((int)file.fileStream.Length - 4, true);
        }
    }
}
