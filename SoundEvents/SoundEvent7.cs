namespace SoundEventEditor.SoundEvents
{
    public class SoundEvent7 : SoundEvent // Sequence (or connect with delay)
    {
        public override int Version => 7;

        public byte Val1 = 0;
        public byte Val2 = 1;
        public byte Val3 = 4;
        public byte Val4 = 1;
        public byte Val5 = 0;
        public byte Val6 = 5;

        public byte Val8 = 7;
        public byte Val9 = 1;
        public float Delay = 1;

        public override void Write(RawFile file)
        {
            base.Write(file);

            file.WriteByte(Val1);
            file.WriteByte(Val2);
            file.WriteByte(Val3);
            file.WriteByte(Val4);
            file.WriteByte(Val5);
            file.WriteByte(Val6);
            file.WriteByte((byte)Children.Count);
            foreach (var child in Children)
            {
                child.Write(file);
            }

            file.WriteByte(Val8);
            file.WriteByte(Val9);
            file.WriteFloat(Delay);
        }

        public override void Parse(RawFile file)
        {
            Val1 = file.ReadByte(); 
            Val2 = file.ReadByte();
            Val3 = file.ReadByte(); // isSequence = 4 (maybe even defaultbus)? - ui_instructionbegin
            Val4 = file.ReadByte();
            Val5 = file.ReadByte();
            Val6 = file.ReadByte();
            byte componentsCount = file.ReadByte();

            for (int i = 0; i < componentsCount; i++)
            {
                Children.Add(SoundEvent.GetSoundEvent(file));
            }

            Val8 = file.ReadByte();
            Val9 = file.ReadByte();
            Delay = file.ReadFloat(true);
        }
    }
}
