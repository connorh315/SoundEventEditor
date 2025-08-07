using System.Collections.Generic;

namespace SoundEventEditor.SoundEvents
{
    public class SoundEvent9 : SoundEvent
    {
        public override int Version => 9;

        public byte Val1 = 0;
        public byte Val2 = 1;
        public byte Val3 = 4;
        public byte Val4 = 1;
        public byte Val5 = 0;
        public byte Val6 = 5;

        public byte Val14 = 1;
        public byte Val15 = 1;
        public byte Global = 0; // Always
        public byte Val17 = 0; // Always

        public byte MainFiletype = 1;
        public byte FiletypeGen7 = 1;
        public byte UnknownFiletype = 9;
        public byte UnknownFiletype2 = 9;

        public byte UnknownFiletype3 = 1;
        public byte UnknownFiletype4 = 1;
        public byte UnknownFiletype5 = 1;
        public byte UnknownFiletype6 = 1;
        public byte UnknownFiletype7 = 1;

        public byte HasOcclusion = 1;
        public byte UpdateMix = 1;
        public byte NoRear = 0;
        public byte ForceNonPos = 1; // Or 0
        public byte Val25 = 0; // Always
        public byte OverrideUserMusic = 0;
        public byte Loopable = 0;
        public byte FallOffTypeInverse = 0;
        public byte Val29 = 0;
        public byte Regional = 1;
        public byte LoadPriority = 10;

        public float FieldAngleMin = 20;
        public float FieldAngleMax = 180;
        public float BleedAngle = 70;
        public float BleedNear = 0;
        public float BleedFar = 0;
        public float LowPassNear = 0;
        public float LowPassFar = 0;
        public float FadeIn = 0;
        public float FadeOut = 1; // Or 0
        public float PitchRampUpTime = 0;
        public float PitchRampDownTime = 0;
        public float Near = 90;
        public float Far = 100;
        public float Volume = 0.7f;
        public float RandomVolume = 0;
        public float Pitch = 1;
        public float RandomPitch = 0;
        public float PitchTimeScaleFactor = 0;
        public float LowPass = 0;
        public float RandomLowPass = 0;
        public float LowPassQ = 1;
        public float LfeMix = 0.1f;
        public float ReverbWetMix = 0.7f; // or 0
        public float StartOffset = 0;
        public float RandomStartOffset = 0;
        public float DopplerEffectScalar = 0;
        public float StartPitch = 1;
        public float TargetPitch = 1;
        public float EndPitch = 1;

        public byte SoundType = 9;
        public byte Val27 = 1;

        public string Filename;
        public string Mix;
        public string RoutingTable;
        public List<SoundEvent12> Characters = [];

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

            file.WriteByte(Val14);
            file.WriteByte(Val15);
            file.WriteByte(Global);
            file.WriteByte(Val17);

            file.WriteByte(MainFiletype);
            file.WriteByte(UnknownFiletype);
            file.WriteByte(FiletypeGen7);
            file.WriteByte(UnknownFiletype2);
            file.WriteByte(UnknownFiletype3);
            file.WriteByte(UnknownFiletype4);
            file.WriteByte(UnknownFiletype5);
            file.WriteByte(UnknownFiletype6);
            file.WriteByte(UnknownFiletype7);

            file.WriteByte(HasOcclusion);
            file.WriteByte(UpdateMix);
            file.WriteByte(NoRear);
            file.WriteByte(ForceNonPos);
            file.WriteByte(Val25);
            file.WriteByte(OverrideUserMusic);
            file.WriteByte(Loopable);
            file.WriteByte(FallOffTypeInverse);
            file.WriteByte(Val29);
            file.WriteByte(Regional);
            file.WriteByte(LoadPriority);

            file.WriteFloat(FieldAngleMin, true);
            file.WriteFloat(FieldAngleMax, true);
            file.WriteFloat(BleedAngle, true);
            file.WriteFloat(BleedNear, true);
            file.WriteFloat(BleedFar, true);
            file.WriteFloat(LowPassNear, true);
            file.WriteFloat(LowPassFar, true);
            file.WriteFloat(FadeIn, true);
            file.WriteFloat(FadeOut, true);
            file.WriteFloat(PitchRampUpTime, true);
            file.WriteFloat(PitchRampDownTime, true);
            file.WriteFloat(Near, true);
            file.WriteFloat(Far, true);
            file.WriteFloat(Volume, true);
            file.WriteFloat(RandomVolume, true);
            file.WriteFloat(Pitch, true);
            file.WriteFloat(RandomPitch, true);
            file.WriteFloat(PitchTimeScaleFactor, true);
            file.WriteFloat(LowPass, true);
            file.WriteFloat(RandomLowPass, true);
            file.WriteFloat(LowPassQ, true);
            file.WriteFloat(LfeMix, true);
            file.WriteFloat(ReverbWetMix, true);
            file.WriteFloat(StartOffset, true);
            file.WriteFloat(RandomStartOffset, true);
            file.WriteFloat(DopplerEffectScalar, true);
            file.WriteFloat(StartPitch, true);
            file.WriteFloat(TargetPitch, true);
            file.WriteFloat(EndPitch, true);

            file.WritePascalString(Filename);
            file.WritePascalString(Mix);
            file.WritePascalString(RoutingTable);

            file.WriteByte(SoundType);
            file.WriteByte(Val27);
            file.WriteByte((byte)Characters.Count);

            foreach (var character in Characters)
            {
                file.WriteByte(character.EventType);
                file.WriteByte(character.EventVersion);
                file.WriteByte(character.NoFlappy);
                file.WritePascalString(character.CharacterName);
                file.WritePascalString(character.BodyAnimationSet);
                file.WritePascalString(character.BodyAnimationAction);
                file.WritePascalString(character.FacialAnimationSet);
                file.WritePascalString(character.FacialAnimationAction);
            }
        }

        public override void Parse(RawFile file) // Speech
        {
            Val1 = file.ReadByte();
            Val2 = file.ReadByte();
            Val3 = file.ReadByte();
            Val4 = file.ReadByte();
            Val5 = file.ReadByte();
            Val6 = file.ReadByte();
            byte components = file.ReadByte();

            Children = new List<SoundEvent>(components);

            for (int i = 0; i < components; i++)
            {
                Children.Add(SoundEvent.GetSoundEvent(file));
            }

            Val14 = file.ReadByte();
            Val15 = file.ReadByte();
            Global = file.ReadByte();
            Val17 = file.ReadByte();
            MainFiletype = file.ReadByte();
            UnknownFiletype = file.ReadByte();
            FiletypeGen7 = file.ReadByte();
            UnknownFiletype2 = file.ReadByte();
            UnknownFiletype3 = file.ReadByte();
            UnknownFiletype4 = file.ReadByte();
            UnknownFiletype5 = file.ReadByte();
            UnknownFiletype6 = file.ReadByte();
            UnknownFiletype7 = file.ReadByte();
            HasOcclusion = file.ReadByte();
            UpdateMix = file.ReadByte();
            NoRear = file.ReadByte();
            ForceNonPos = file.ReadByte();
            Val25 = file.ReadByte();
            OverrideUserMusic = file.ReadByte();
            Loopable = file.ReadByte();
            FallOffTypeInverse = file.ReadByte();
            Val29 = file.ReadByte();
            Regional = file.ReadByte();
            LoadPriority = file.ReadByte();

            FieldAngleMin = file.ReadFloat(true);
            FieldAngleMax = file.ReadFloat(true);
            BleedAngle = file.ReadFloat(true);
            BleedNear = file.ReadFloat(true);
            BleedFar = file.ReadFloat(true);
            LowPassNear = file.ReadFloat(true);
            LowPassFar = file.ReadFloat(true);
            FadeIn = file.ReadFloat(true);
            FadeOut = file.ReadFloat(true);
            PitchRampUpTime = file.ReadFloat(true);
            PitchRampDownTime = file.ReadFloat(true);
            Near = file.ReadFloat(true);
            Far = file.ReadFloat(true);
            Volume = file.ReadFloat(true);
            RandomVolume = file.ReadFloat(true);
            Pitch = file.ReadFloat(true);
            RandomPitch = file.ReadFloat(true);
            PitchTimeScaleFactor = file.ReadFloat(true);
            LowPass = file.ReadFloat(true);
            RandomLowPass = file.ReadFloat(true);
            LowPassQ = file.ReadFloat(true);
            LfeMix = file.ReadFloat(true);
            ReverbWetMix = file.ReadFloat(true);
            StartOffset = file.ReadFloat(true);
            RandomStartOffset = file.ReadFloat(true);
            DopplerEffectScalar = file.ReadFloat(true); // loop = 1
            StartPitch = file.ReadFloat(true);
            TargetPitch = file.ReadFloat(true);
            EndPitch = file.ReadFloat(true);

            Filename = file.ReadPascalString();
            Mix = file.ReadPascalString();
            RoutingTable = file.ReadPascalString();

            SoundType = file.ReadByte();
            Val27 = file.ReadByte();
            byte characterCount = file.ReadByte();

            Characters = new List<SoundEvent12>(characterCount);

            for (int i = 0; i < characterCount; i++)
            {
                var character = new SoundEvent12();

                character.Parse(file);

                Characters.Add(character);
            }
        }
    }
}
