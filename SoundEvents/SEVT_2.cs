using Avalonia.Remote.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundEventEditor.SoundEvents
{
    public class SEVT_2 : SoundEvent
    {
        public override int Version => 2;

        public byte Val1 = 0;
        public byte Val2 = 1;
        public byte MaxVoices = 4; // or 1 or 16
        public byte MaxVoicesBehaviour = 1; // or 0
        public byte Val5 = 0;
        public byte Val6 = 5;

        public byte Val14 = 1; // Always
        public byte Val15 = 1; // Always
        public byte Global = 0; // Always
        public byte Val17 = 0;

        public byte MainFiletype = 1;
        public byte FiletypeGen7 = 1;
        public byte UnknownFiletype = 3;
        public byte UnknownFiletype2 = 4;

        public byte UnknownFiletype3 = 1;
        public byte UnknownFiletype4 = 1;
        public byte UnknownFiletype5 = 1;
        public byte UnknownFiletype6 = 10;
        public byte UnknownFiletype7 = 1;

        public byte HasOcclusion = 1;
        public byte UpdateMix = 1;
        public byte NoRear = 0;
        public byte ForceNonPos = 1; // or 0
        public byte Val25 = 0;
        public byte OverrideUserMusic = 0; // or 1
        public byte Loopable = 0; // or 1
        public byte FallOffTypeInverse = 0; // Always
        public byte Val29 = 0; // Always
        public byte Regional = 1; // Always
        public byte LoadPriority = 5; // or 10, 9

        public float FieldAngleMin = 20; // Always
        public float FieldAngleMax = 180; // Always
        public float BleedAngle = 70; // Always
        public float BleedNear = 0; // Always
        public float BleedFar = 0; // Always
        public float LowPassNear = 0; // Always
        public float LowPassFar = 0; // Always
        public float FadeIn = 0; // Range of values
        public float FadeOut = 0; // Range of values
        public float PitchRampUpTime = 0; // Always
        public float PitchRampDownTime = 0; // Always
        public float Near = 1;
        public float Far = 6;
        public float Volume = 1; // Range of values
        public float RandomVolume = 0; // Range of values
        public float Pitch = 1; // Always
        public float RandomPitch = 0; // Always
        public float PitchTimeScaleFactor = 0.5f; // Always
        public float LowPass = 1; // Always
        public float RandomLowPass = 0; // Always
        public float LowPassQ = 1; // Always
        public float LfeMix = 0.1f; // Always
        public float ReverbWetMix = 1; // Always
        public float StartOffset = 0; // Always
        public float RandomStartOffset = 0; // Always
        public float DopplerEffectScalar = 0; // Always
        public float StartPitch = 1; // Always
        public float TargetPitch = 1; // Always
        public float EndPitch = 1; // Always

        public string Filename;
        public string Mix;
        public string RoutingTable;

        public byte Val100 = 2; // Always
        public byte Val101 = 1; // Always

        public List<float> Cues = new();

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

            file.WriteByte(Val100);
            file.WriteByte(Val101);
            file.WriteByte((byte)Cues.Count);
            foreach (float cue in Cues)
            {
                file.WriteFloat(cue, true);
            }
        }

        public override void Parse(RawFile file)
        {
            Val1 = file.ReadByte();
            Val2 = file.ReadByte();
            MaxVoices = file.ReadByte();
            MaxVoicesBehaviour = file.ReadByte();
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

            Val100 = file.ReadByte();
            Val101 = file.ReadByte();
            byte cueCount = file.ReadByte();

            for (int i = 0; i < cueCount; i++)
            {
                Cues.Add(file.ReadFloat(true));
            }
        }
    }
}
