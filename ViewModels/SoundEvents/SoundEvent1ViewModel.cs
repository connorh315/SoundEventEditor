using SoundEventEditor.SoundEvents;
using System;

namespace SoundEventEditor.ViewModels.SoundEvents
{
    public class SoundEvent1ViewModel : SoundEventViewModel
    {
        public override string Title => "Sample";

        public override string BorderColour => "White";

        protected override void BuildFromEvent(SoundEvent rawEvent)
        {
            SoundEvent1 soundEvent1 = (SoundEvent1)rawEvent;

            Model = soundEvent1;

            Connections = [];

            foreach (SoundEvent child in rawEvent.Children)
            {
                if (child is SoundEvent4 busConn)
                {
                    Connections.Add(ConvertToViewModel(busConn));
                }
                else
                {
                    throw new Exception($"Unexpected child of SoundEvent1 - {child.GetType()}");
                }
            }

            Options =
            [
                new DividerViewModel("FileSettings"),

                new StringOptionViewModel("Filename", soundEvent1.Filename),
                new SelectionOptionViewModel("MainFiletype", FiletypeStrings, FiletypeStrings[soundEvent1.MainFiletype]),
                new SelectionOptionViewModel("FiletypeGen7", FiletypeStrings, FiletypeStrings[soundEvent1.FiletypeGen7]),
                new StringOptionViewModel("LoadPriority", soundEvent1.LoadPriority, true, false),

                new DividerViewModel("OutputSettings"),

                new StringOptionViewModel("Mix", soundEvent1.Mix),
                new StringOptionViewModel("RoutingTable", soundEvent1.RoutingTable),

                new BoolOptionViewModel("Global", soundEvent1.Global),
                new BoolOptionViewModel("Regional", soundEvent1.Regional),
                new BoolOptionViewModel("NoOcclusion", soundEvent1.HasOcclusion, true),
                new BoolOptionViewModel("MixUpdate", soundEvent1.UpdateMix, true),
                new BoolOptionViewModel("NoRear", soundEvent1.NoRear),
                new BoolOptionViewModel("ForceNonPos", soundEvent1.ForceNonPos, true),
                new BoolOptionViewModel("OverrideUserMusic", soundEvent1.OverrideUserMusic),
                new BoolOptionViewModel("ShouldLoopFlag", soundEvent1.Loopable),
                new BoolOptionViewModel("FallOffTypeInverse", soundEvent1.FallOffTypeInverse),

                new DividerViewModel("SampleSettings"),

                new StringOptionViewModel("FieldAngleMin", soundEvent1.FieldAngleMin),
                new StringOptionViewModel("FieldAngleMax", soundEvent1.FieldAngleMax),
                new StringOptionViewModel("BleedAngle", soundEvent1.BleedAngle),
                new StringOptionViewModel("BleedNear", soundEvent1.BleedNear),
                new StringOptionViewModel("BleedFar", soundEvent1.BleedFar),
                new StringOptionViewModel("LowPassNear", soundEvent1.LowPassNear),
                new StringOptionViewModel("LowPassFar", soundEvent1.LowPassFar),
                new StringOptionViewModel("FadeIn", soundEvent1.FadeIn),
                new StringOptionViewModel("FadeOut", soundEvent1.FadeOut),
                new StringOptionViewModel("PitchRampUpTime", soundEvent1.PitchRampUpTime),
                new StringOptionViewModel("PitchRampDownTime", soundEvent1.PitchRampDownTime),
                new StringOptionViewModel("Near", soundEvent1.Near),
                new StringOptionViewModel("Far", soundEvent1.Far),
                new StringOptionViewModel("Volume", soundEvent1.Volume),
                new StringOptionViewModel("RandomVolume", soundEvent1.RandomVolume),
                new StringOptionViewModel("Pitch", soundEvent1.Pitch),
                new StringOptionViewModel("RandomPitch", soundEvent1.RandomPitch),
                new StringOptionViewModel("PitchTimeScaleFactor", soundEvent1.PitchTimeScaleFactor),
                new StringOptionViewModel("LowPass", soundEvent1.LowPass),
                new StringOptionViewModel("RandomLowPass", soundEvent1.RandomLowPass),
                new StringOptionViewModel("LowPassQ", soundEvent1.LowPassQ),
                new StringOptionViewModel("LFEMix", soundEvent1.LfeMix),
                new StringOptionViewModel("ReverbWetMix", soundEvent1.ReverbWetMix),
                new StringOptionViewModel("StartOffset", soundEvent1.StartOffset),
                new StringOptionViewModel("RandomStartOffset", soundEvent1.RandomStartOffset),
                new StringOptionViewModel("DopplerEffectScalar", soundEvent1.DopplerEffectScalar),
                new StringOptionViewModel("StartPitch", soundEvent1.StartPitch),
                new StringOptionViewModel("TargetPitch", soundEvent1.TargetPitch),
                new StringOptionViewModel("EndPitch", soundEvent1.EndPitch),
            ];

            SelectableChildren = [];
        }

        public override SoundEvent RebuildEvent()
        {
            SoundEvent1 soundEvent1 = (SoundEvent1)Model;

            soundEvent1.Children = [];

            foreach (var conn in Connections)
            {
                soundEvent1.Children.Add(conn.RebuildEvent());
            }

            soundEvent1.Filename = (string)GetOption("Filename").Value;
            soundEvent1.MainFiletype = GetOption("MainFiletype").GetByte();
            soundEvent1.FiletypeGen7 = GetOption("FiletypeGen7").GetByte();
            soundEvent1.LoadPriority = GetOption("LoadPriority").GetByte();

            soundEvent1.Mix = (string)GetOption("Mix").Value;
            soundEvent1.RoutingTable = (string)GetOption("RoutingTable").Value;

            soundEvent1.Global = GetOption("Global").GetByte();
            soundEvent1.Regional = GetOption("Regional").GetByte();
            soundEvent1.HasOcclusion = GetOption("NoOcclusion").GetByte();
            soundEvent1.UpdateMix = GetOption("MixUpdate").GetByte();
            soundEvent1.NoRear = GetOption("NoRear").GetByte();
            soundEvent1.ForceNonPos = GetOption("ForceNonPos").GetByte();
            soundEvent1.OverrideUserMusic = GetOption("OverrideUserMusic").GetByte();
            soundEvent1.Loopable = GetOption("ShouldLoopFlag").GetByte();
            soundEvent1.FallOffTypeInverse = GetOption("FallOffTypeInverse").GetByte();

            soundEvent1.FieldAngleMin = GetOption("FieldAngleMin").GetFloat();
            soundEvent1.FieldAngleMax = GetOption("FieldAngleMax").GetFloat();
            soundEvent1.BleedAngle = GetOption("BleedAngle").GetFloat();
            soundEvent1.BleedNear = GetOption("BleedNear").GetFloat();
            soundEvent1.BleedFar = GetOption("BleedFar").GetFloat();
            soundEvent1.LowPassNear = GetOption("LowPassNear").GetFloat();
            soundEvent1.LowPassFar = GetOption("LowPassFar").GetFloat();
            soundEvent1.FadeIn = GetOption("FadeIn").GetFloat();
            soundEvent1.FadeOut = GetOption("FadeOut").GetFloat();
            soundEvent1.PitchRampUpTime = GetOption("PitchRampUpTime").GetFloat();
            soundEvent1.PitchRampDownTime = GetOption("PitchRampDownTime").GetFloat();
            soundEvent1.Near = GetOption("Near").GetFloat();
            soundEvent1.Far = GetOption("Far").GetFloat();
            soundEvent1.Volume = GetOption("Volume").GetFloat();
            soundEvent1.RandomVolume = GetOption("RandomVolume").GetFloat();
            soundEvent1.Pitch = GetOption("Pitch").GetFloat();
            soundEvent1.RandomPitch = GetOption("RandomPitch").GetFloat();
            soundEvent1.PitchTimeScaleFactor = GetOption("PitchTimeScaleFactor").GetFloat();
            soundEvent1.LowPass = GetOption("LowPass").GetFloat();
            soundEvent1.RandomLowPass = GetOption("RandomLowPass").GetFloat();
            soundEvent1.LowPassQ = GetOption("LowPassQ").GetFloat();
            soundEvent1.LfeMix = GetOption("LFEMix").GetFloat();
            soundEvent1.ReverbWetMix = GetOption("ReverbWetMix").GetFloat();
            soundEvent1.StartOffset = GetOption("StartOffset").GetFloat();
            soundEvent1.RandomStartOffset = GetOption("RandomStartOffset").GetFloat();
            soundEvent1.DopplerEffectScalar = GetOption("DopplerEffectScalar").GetFloat();
            soundEvent1.StartPitch = GetOption("StartPitch").GetFloat();
            soundEvent1.TargetPitch = GetOption("TargetPitch").GetFloat();
            soundEvent1.EndPitch = GetOption("EndPitch").GetFloat();

            return soundEvent1;
        }

        public SoundEvent1ViewModel()
        {
            BuildFromEvent(new SoundEvent1());
        }

        public SoundEvent1ViewModel(SoundEvent1 soundEvent1)
        {
            BuildFromEvent(soundEvent1);
        }
    }
}
