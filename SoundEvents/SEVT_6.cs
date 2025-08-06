using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundEventEditor.SoundEvents
{
    public class SEVT_6 : SoundEvent
    {
        public override int Version => 6;

        public byte Val1 = 0;
        public byte Val2 = 1;
        public byte Val3 = 4;
        public byte Val4 = 1;
        public byte Val5 = 0;
        public byte Val6 = 5;
        public byte Val7 = 0;
        public byte EventType = 6;
        public byte EventVersion = 1;
        public byte SubAlwaysOn; // sub_is_always_on = 1
        public byte Scrolling = 0; // I think this refers to whether to scroll the subtitles, probably the factor too. Never used.
        public byte LowPriority = 0; // Not used
        public byte HighPriority = 0; // HighPriority = 1

        public string Name;

        public override void Write(RawFile file)
        {
            base.Write(file);

            file.WriteByte(Val1);
            file.WriteByte(Val2);
            file.WriteByte(Val3);
            file.WriteByte(Val4);
            file.WriteByte(Val5);
            file.WriteByte(Val6);
            file.WriteByte(Val7);
            file.WriteByte(EventType);
            file.WriteByte(EventVersion);
            file.WriteByte(SubAlwaysOn);
            file.WriteByte(Scrolling);
            file.WriteByte(LowPriority);
            file.WriteByte(HighPriority);

            file.WritePascalString(Name);
        }

        public override void Parse(RawFile file)
        {
            Val1 = file.ReadByte();
            Val2 = file.ReadByte();
            Val3 = file.ReadByte();
            Val4 = file.ReadByte();
            Val5 = file.ReadByte();
            Val6 = file.ReadByte();
            Val7 = file.ReadByte();
            EventType = file.ReadByte();
            EventVersion = file.ReadByte();
            SubAlwaysOn = file.ReadByte();
            Scrolling = file.ReadByte();
            LowPriority = file.ReadByte();
            HighPriority = file.ReadByte();

            Name = file.ReadPascalString();
        }
    }
}
