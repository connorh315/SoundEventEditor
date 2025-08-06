using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundEventEditor.SoundEvents
{
    public class SEVT_8 : SoundEvent // Conversation
    {
        public override int Version => 8;

        public byte Val1 = 0;
        public byte Val2 = 1;
        public byte Val3 = 1; // This is 4 for sequence (literally one sound_event out of 8000)
        public byte Val4 = 1;
        public byte Val5 = 0;
        public byte PlayPriority = 8;

        public List<SoundEvent> Connections;

        public byte Val8 = 8;
        public byte Val9 = 1;
        public byte Val10 = 0; // This is 2 for ui_instructionbegin
        public byte Val11 = 1; // This is 3 for ui_instructionbegin

        public byte OnFailedInterruptWait = 0; // Wait = 1 (This is 2 for ui_instructionbegin)
        public byte PriorityOverSame = 1; // PriorityOverSame enabled = 0

        public byte Val14 = 0;

        public override void Write(RawFile file)
        {
            base.Write(file);

            file.WriteByte(Val1);
            file.WriteByte(Val2);
            file.WriteByte(Val3);
            file.WriteByte(Val4);
            file.WriteByte(Val5);
            file.WriteByte(PlayPriority);
            file.WriteByte((byte)Connections.Count);
            foreach (var conn in Connections)
            {
                conn.Write(file);
            }

            file.WriteByte(Val8);
            file.WriteByte(Val9);
            file.WriteByte(Val10);
            file.WriteByte(Val11);

            file.WriteByte(OnFailedInterruptWait);
            file.WriteByte(PriorityOverSame);
            file.WriteByte(Val14);

            file.WriteByte((byte)Children.Count);
            foreach (var child in Children)
            {
                child.Write(file);
                file.WriteInt(0);
            }
        }

        public override void Parse(RawFile file)
        {
            Val1 = file.ReadByte(); 
            Val2 = file.ReadByte();
            Val3 = file.ReadByte(); // isSequence = 4 (maybe even defaultbus)? - ui_instructionbegin
            Val4 = file.ReadByte();
            Val5 = file.ReadByte();
            PlayPriority = file.ReadByte(); // PlayPriority
            byte connectionsCount = file.ReadByte(); // ConnectTo

            Connections = new List<SoundEvent>(connectionsCount);

            for (int i = 0; i < connectionsCount; i++)
            {
                var conn = GetSoundEvent(file);

                if (conn is not SEVT_4 sevt)
                    throw new InvalidDataException($"Unexpected SoundEvent type: {conn.GetType().Name}");

                Connections.Add(conn);
            }

            Val8 = file.ReadByte(); // always 8
            Val9 = file.ReadByte(); // always 1
            Val10 = file.ReadByte(); // ui_instructionbegin
            Val11 = file.ReadByte(); // ui_instructionbegin

            OnFailedInterruptWait = file.ReadByte(); // wait = 1
            PriorityOverSame = file.ReadByte(); // priorityOverSame = 0
            Val14 = file.ReadByte(); // always 0

            byte eventsCount = file.ReadByte(); // number of events (samples or group(s))
            Children = new List<SoundEvent>(eventsCount);
            for (int i = 0; i < eventsCount; i++)
            {
                Children.Add(SoundEvent.GetSoundEvent(file));
                int pad = file.ReadInt();
            }
        }
    }
}
