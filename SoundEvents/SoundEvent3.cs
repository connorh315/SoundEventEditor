using System.Collections.Generic;

namespace SoundEventEditor.SoundEvents
{
    public class SoundEvent3 : SoundEvent // Group
    {
        public override int Version => 3;

        public byte Val1 = 0;
        public byte Val2 = 1;
        public byte MaxVoices = 4;
        public byte MaxVoicesBehaviour = 1;
        public byte Val5 = 0;
        public byte Val6 = 5;
        
        public List<SoundEvent> Connections; // This is used once, across all the sample files available

        public byte EventType = 3;
        public byte EventVersion = 1;

        public byte Behaviour = 0; // 0 = Default (Seems to be referred to in ghidra as "random"), 1 = Shuffle, 2 = Sequence, 3 = PlayAll
        public byte NumEventsToTrigger = 1; // Never used
        public byte NumNoRepeat = 1; // Not sure I really understand this one - TODO: Give this as a settable option to the user?
        public byte RetryOnFailure = 0;

        public override void Write(RawFile file)
        {
            base.Write(file);

            file.WriteByte(Val1);
            file.WriteByte(Val2);
            file.WriteByte(MaxVoices);
            file.WriteByte(MaxVoicesBehaviour);
            file.WriteByte(Val5);
            file.WriteByte(Val6);

            if (Connections != null)
            {
                file.WriteByte((byte)Connections.Count);
                foreach (var conn in Connections)
                {
                    conn.Write(file);
                }
            }
            else
                file.WriteByte(0);

            file.WriteByte(EventType);
            file.WriteByte(EventVersion);
            file.WriteByte(Behaviour);
            file.WriteByte(NumEventsToTrigger);
            file.WriteByte(NumNoRepeat);
            file.WriteByte(RetryOnFailure);

            file.WriteByte((byte)Children.Count);
            foreach (var child in Children)
            {
                child.Write(file);
            }
        }

        public override void Parse(RawFile file)
        {
            Val1 = file.ReadByte();
            Val2 = file.ReadByte();
            MaxVoices = file.ReadByte();
            MaxVoicesBehaviour = file.ReadByte(); // 0 = replace
            Val5 = file.ReadByte();
            Val6 = file.ReadByte();

            byte componentsCount = file.ReadByte();
            if (componentsCount > 0)
            {
                Connections = new List<SoundEvent>(componentsCount);
                for (int i = 0; i < componentsCount; i++)
                {
                    Connections.Add(GetSoundEvent(file));
                }
            }

            EventType = file.ReadByte();
            EventVersion = file.ReadByte();
            Behaviour = file.ReadByte(); // shuffle = 1, playall = 3 (meaning there are events registered)
            NumEventsToTrigger = file.ReadByte();
            NumNoRepeat = file.ReadByte();
            RetryOnFailure = file.ReadByte();
            
            byte sampleCount = file.ReadByte();
            Children = new List<SoundEvent>(sampleCount);

            for (int i = 0; i < sampleCount; i++)
            {
                Children.Add(GetSoundEvent(file));
            }
        }
    }
}
