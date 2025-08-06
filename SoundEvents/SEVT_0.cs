using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundEventEditor.SoundEvents
{
    public class SEVT_0 : SoundEvent
    {
        public override int Version => 0;

        public byte Val1 = 0;
        public byte Val2 = 1;

        public byte MaxVoices = 4;
        public byte MaxVoicesBehaviour = 1;

        public byte Val5 = 0;
        public byte Val6 = 5;

        public override void Parse(RawFile file) // Event (check BRK_STONELRG_WDEB)
        {
            Val1 = file.ReadByte();
            Val2 = file.ReadByte();
            MaxVoices = file.ReadByte();
            MaxVoicesBehaviour = file.ReadByte(); // replace = 0
            Val5 = file.ReadByte();
            Val6 = file.ReadByte();

            byte components = file.ReadByte();
            Children = new List<SoundEvent>(components);

            for (int i = 0; i < components; i++)
            {
                Children.Add(SoundEvent.GetSoundEvent(file));
            }
        }

        public override void Write(RawFile file)
        {
            base.Write(file);

            file.WriteByte(Val1);
            file.WriteByte(Val2);
            file.WriteByte(MaxVoices);
            file.WriteByte(MaxVoicesBehaviour);
            file.WriteByte(Val5);
            file.WriteByte(Val6);

            file.WriteByte((byte)Children.Count);

            foreach (var child in Children)
            {
                child.Write(file);
            }
        }
    }
}
