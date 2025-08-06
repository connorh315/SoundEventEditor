using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundEventEditor.SoundEvents
{
    public class SEVT_12 : SoundEvent
    {
        public override int Version => 0xC;
        
        public byte EventType = 0xC; // Type
        public byte EventVersion = 1; // Version
        public byte NoFlappy = 1;
        public string CharacterName;
        public string BodyAnimationSet; // Never used
        public string BodyAnimationAction; // Never used
        public string FacialAnimationSet;
        public string FacialAnimationAction;

        public override void Parse(RawFile file)
        {
            EventType = file.ReadByte();
            EventVersion = file.ReadByte();
            NoFlappy = file.ReadByte();
            CharacterName = file.ReadPascalString();
            BodyAnimationSet = file.ReadPascalString();
            BodyAnimationAction = file.ReadPascalString();
            FacialAnimationSet = file.ReadPascalString();
            FacialAnimationAction = file.ReadPascalString();
        }
    }
}
