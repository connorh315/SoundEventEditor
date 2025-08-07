using System.Collections.Generic;

namespace SoundEventEditor.SoundEvents
{
    public class SoundEvent4 : SoundEvent // Connect to bus(es)
    {
        public override int Version => 4;

        public List<string> Buses;

        public byte Val1 = 0;
        public byte Val2 = 1;
        public byte Val3 = 4;
        public byte Val4 = 1;
        public byte Val5 = 0;
        public byte Val6 = 5;
        public byte Val7 = 0;
        public byte Val8 = 4;
        public byte Val9 = 1;
        public byte DuckScale = 0; // TODO: Seems to be ignored in code... Might be worth testing

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
            file.WriteByte(Val8);
            file.WriteByte(Val9);
            file.WriteByte(DuckScale);

            file.WriteByte((byte)Buses.Count);
            foreach (string bus in Buses)
            {
                file.WritePascalString(bus);
            }
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
            Val8 = file.ReadByte();
            Val9 = file.ReadByte();
            DuckScale = file.ReadByte();

            byte busCount = file.ReadByte();
            Buses = new List<string>(busCount);
            for (int i = 0; i < busCount; i++)
            {
                string bus = file.ReadPascalString();
                Buses.Add(bus);
            }
        }
    }
}
