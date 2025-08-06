using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundEventEditor.SoundEvents
{
    public abstract class SoundEvent
    {
        public List<SoundEvent> Connections;
        public List<SoundEvent> Children = new();

        public abstract void Parse(RawFile file);

        public abstract int Version { get; }

        public virtual void Write(RawFile file)
        {
            file.WriteString("TVES");
            file.WriteInt(1, true);
            file.WriteInt(Version, true);
            file.WriteInt(-1); // Checksum :)
        }

        public static SoundEvent GetSoundEvent(RawFile file)
        {
            if (file.ReadString(4) != "TVES") throw new DataMisalignedException("Misaligned!");
            uint version = file.ReadUInt(true);
            uint seType = file.ReadUInt(true);
            uint seId = file.ReadUInt(true);

            SoundEvent evt;
            switch (seType)
            {
                case 9:
                    evt = new SEVT_9();
                    break;
                case 8:
                    evt = new SEVT_8();
                    break;
                case 7:
                    evt = new SEVT_7();
                    break;
                case 4:
                    evt = new SEVT_4();
                    break;
                case 3:
                    evt = new SEVT_3();
                    break;
                case 10:
                    evt = new SEVT_10();
                    break;
                case 6:
                    evt = new SEVT_6();
                    break;
                case 2:
                    evt = new SEVT_2();
                    break;
                case 1:
                    evt = new SEVT_1();
                    break;
                case 0:
                    evt = new SEVT_0();
                    break;
                default:
                    throw new AccessViolationException($"Unknown SoundEvent type: {seType}");
            }

            evt.Parse(file);

            return evt;
        }

        public static SoundEvent OpenFile(string fileLocation)
        {
            using (RawFile file = new RawFile(fileLocation))
            {
                int fileLength = file.ReadInt(true);

                SoundEvent root = SoundEvent.GetSoundEvent(file);

                return root;
            }
        }

        public static void SaveFile(string fileLocation, SoundEvent root)
        {
            using (RawFile file = new RawFile(fileLocation))
            {
                file.WriteInt(0); // The length of the file, fixed at the end

                root.Write(file);

                file.Seek(0, System.IO.SeekOrigin.Begin);

                file.WriteInt((int)file.fileStream.Length - 4, true);
            }
        }
    }
}
