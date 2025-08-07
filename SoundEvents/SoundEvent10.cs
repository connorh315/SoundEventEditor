using System.Collections.Generic;

namespace SoundEventEditor.SoundEvents
{
    public class SoundEvent10 : SoundEvent
    {
        public override int Version => 10;

        public byte Val1 = 0;
        public byte Val2 = 1;
        public byte MaxVoices = 4; // Not used here
        public byte MaxVoicesBehaviour = 1; // Not used here
        public byte Exclusive = 0; // Not used
        public byte PlayPriority = 5; // Not really used here

        public byte Val14 = 1; // EventType (underlying event type? Since this technically would be an extension of the Sample class?)
        public byte Val15 = 1; // EventVersion
        public byte Global = 0; // Always
        public byte Val17 = 0; // ContentIndex (always 0)

        public byte MainFiletype = 1; // I've just assumed this, it's not necessarily true
        public byte UnknownFiletype = 1;
        public byte FiletypeGen7 = 9; // This one seems to be a weak console
        public byte UnknownFiletype2 = 9; // This one seems to be a weak console

        public byte UnknownFiletype3 = 1;
        public byte UnknownFiletype4 = 1;
        public byte UnknownFiletype5 = 1;
        public byte UnknownFiletype6 = 1;
        public byte UnknownFiletype7 = 1;

        public byte HasOcclusion = 1; // Always
        public byte UpdateMix = 1; // Always
        public byte NoRear = 0; // Always (also referred to as downmixType)
        public byte ForceNonPos = 1; // (also referred to as positional)
        public byte Val25 = 0; // Always (TriggerOutsideFallOff)
        public byte OverrideUserMusic = 0; // Always
        public byte Loopable = 0; // Always
        public byte FallOffTypeInverse = 0; // Always
        public byte CharacterMustExist = 0;
        public byte Regional = 1; // Always
        public byte LoadPriority = 10; // Always

        public float FieldAngleMin = 20; // Or 120
        public float FieldAngleMax = 180; // Or 360
        public float BleedAngle = 70; // Or 160
        public float BleedNear = 0; // Or 2
        public float BleedFar = 0; // Or 25
        public float LowPassNear = 0; // Or 5
        public float LowPassFar = 0; // Or 15
        public float FadeIn = 0;
        public float FadeOut = 1;
        public float PitchRampUpTime = 0;
        public float PitchRampDownTime = 0;
        public float Near = 90; // Or values from 1-300 (second best is 100)
        public float Far = 100; // Or values from 10-500 (second best is 300)
        public float Volume = 0.7f; // second best is 0.4
        public float RandomVolume = 0;
        public float Pitch = 1;
        public float RandomPitch = 0;
        public float PitchTimeScaleFactor = 0;
        public float LowPass = 1; // Or 0 / 0.9
        public float RandomLowPass = 0;
        public float LowPassQ = 1; // Or 1.5
        public float LfeMix = 0.1f;
        public float ReverbWetMix = 0.7f; // Or 0.2
        public float StartOffset = 0; 
        public float RandomStartOffset = 0;
        public float DopplerEffectScalar = 0;
        public float StartPitch = 1;
        public float TargetPitch = 1;
        public float EndPitch = 1;

        public byte SoundType = 0xA; // Always
        public byte Val46 = 2; // Always
        public byte Val47 = 1; // Always
        public byte Val48 = 0; // Always
        public byte Val50 = 1;

        public string Filename;
        public string Mix;
        public string RoutingTable;
        public List<SoundEvent12> Characters = [];

        public override void Write(RawFile file)
        {
            base.Write(file);

            file.WriteByte(Val1);
            file.WriteByte(Val2);
            file.WriteByte(MaxVoices);
            file.WriteByte(MaxVoicesBehaviour);
            file.WriteByte(Exclusive);
            file.WriteByte(PlayPriority);
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
            file.WriteByte(CharacterMustExist);
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

            file.WriteByte(Val46);
            file.WriteByte(Val47);
            file.WriteByte(Val48);

            file.WriteByte(SoundType);
            file.WriteByte(Val50);
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

        public override void Parse(RawFile file)
        {
            Val1 = file.ReadByte();
            Val2 = file.ReadByte();
            MaxVoices = file.ReadByte();
            MaxVoicesBehaviour = file.ReadByte();
            Exclusive = file.ReadByte();
            PlayPriority = file.ReadByte();
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
            CharacterMustExist = file.ReadByte();
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

            Val46 = file.ReadByte();
            Val47 = file.ReadByte();
            Val48 = file.ReadByte();

            SoundType = file.ReadByte();
            Val50 = file.ReadByte();
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
