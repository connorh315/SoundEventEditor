using SoundEventEditor.SoundEvents;
using System;

namespace SoundEventEditor.ViewModels.SoundEvents
{
    public class SoundEvent10ViewModel : SoundEventViewModel
    {
        public override string Title => "StreamingSpeech";

        public override string BorderColour => "Purple";

        private SoundEvent6ViewModel Name;

        //private string GetFiletypeString(byte choice)
        //{
        //    switch (choice)
        //    {
        //        case 0:
        //            return "VAG";
        //        case 1:
        //            return "OGG";
        //        case 9:
        //            return "CBX";
        //        default:
        //            return "";
        //    }
        //}

        protected override void BuildFromEvent(SoundEvent rawEvent)
        {
            SoundEvent10 evt = (SoundEvent10)rawEvent;

            Model = evt;

            Connections = [];

            foreach (SoundEvent child in rawEvent.Children)
            {
                if (child is SoundEvent6 name)
                {
                    Name = (SoundEvent6ViewModel)ConvertToViewModel(name);
                }
                else if (child is SoundEvent4 busConn)
                {
                    Connections.Add(ConvertToViewModel(busConn));
                }
                else
                {
                    throw new Exception($"Unexpected child of SoundEvent10 - {child.GetType()}");
                }
            }

            Options =
            [
                Name.GetOption("Name"),
                Name.GetOption("SubAlwaysOn"),
                Name.GetOption("HighPriority"),

                new DividerViewModel("FileSettings"),

                new StringOptionViewModel("Filename", evt.Filename),
                new SelectionOptionViewModel("MainFiletype", FiletypeStrings, FiletypeStrings[evt.MainFiletype]),
                new SelectionOptionViewModel("FiletypeGen7", FiletypeStrings, FiletypeStrings[evt.FiletypeGen7]),
                new StringOptionViewModel("LoadPriority", evt.LoadPriority, true, false),

                new DividerViewModel("OutputSettings"),

                new StringOptionViewModel("Mix", evt.Mix),
                new StringOptionViewModel("RoutingTable", evt.RoutingTable),

                new BoolOptionViewModel("Global", evt.Global),
                new BoolOptionViewModel("Regional", evt.Regional),
                new BoolOptionViewModel("NoOcclusion", evt.HasOcclusion, true),
                new BoolOptionViewModel("MixUpdate", evt.UpdateMix, true),
                new BoolOptionViewModel("NoRear", evt.NoRear),
                new BoolOptionViewModel("ForceNonPos", evt.ForceNonPos, true),
                new BoolOptionViewModel("OverrideUserMusic", evt.OverrideUserMusic),
                new BoolOptionViewModel("ShouldLoopFlag", evt.Loopable),
                new BoolOptionViewModel("FallOffTypeInverse", evt.FallOffTypeInverse),

                new DividerViewModel("SampleSettings"),

                new StringOptionViewModel("FieldAngleMin", evt.FieldAngleMin),
                new StringOptionViewModel("FieldAngleMax", evt.FieldAngleMax),
                new StringOptionViewModel("BleedAngle", evt.BleedAngle),
                new StringOptionViewModel("BleedNear", evt.BleedNear),
                new StringOptionViewModel("BleedFar", evt.BleedFar),
                new StringOptionViewModel("LowPassNear", evt.LowPassNear),
                new StringOptionViewModel("LowPassFar", evt.LowPassFar),
                new StringOptionViewModel("FadeIn", evt.FadeIn),
                new StringOptionViewModel("FadeOut", evt.FadeOut),
                new StringOptionViewModel("PitchRampUpTime", evt.PitchRampUpTime),
                new StringOptionViewModel("PitchRampDownTime", evt.PitchRampDownTime),
                new StringOptionViewModel("Near", evt.Near),
                new StringOptionViewModel("Far", evt.Far),
                new StringOptionViewModel("Volume", evt.Volume),
                new StringOptionViewModel("RandomVolume", evt.RandomVolume),
                new StringOptionViewModel("Pitch", evt.Pitch),
                new StringOptionViewModel("RandomPitch", evt.RandomPitch),
                new StringOptionViewModel("PitchTimeScaleFactor", evt.PitchTimeScaleFactor),
                new StringOptionViewModel("LowPass", evt.LowPass),
                new StringOptionViewModel("RandomLowPass", evt.RandomLowPass),
                new StringOptionViewModel("LowPassQ", evt.LowPassQ),
                new StringOptionViewModel("LFEMix", evt.LfeMix),
                new StringOptionViewModel("ReverbWetMix", evt.ReverbWetMix),
                new StringOptionViewModel("StartOffset", evt.StartOffset),
                new StringOptionViewModel("RandomStartOffset", evt.RandomStartOffset),
                new StringOptionViewModel("DopplerEffectScalar", evt.DopplerEffectScalar),
                new StringOptionViewModel("StartPitch", evt.StartPitch),
                new StringOptionViewModel("TargetPitch", evt.TargetPitch),
                new StringOptionViewModel("EndPitch", evt.EndPitch),
            ];

            
            foreach (var character in evt.Characters)
            {
                Children.Add(ConvertToViewModel(character));
            }

            SelectableChildren = [SoundEventType.Character]; // no children allowed
        }

        public override SoundEvent RebuildEvent()
        {
            SoundEvent10 evt = (SoundEvent10)Model;

            evt.Children = [Name.RebuildEvent()];

            foreach (var conn in Connections)
            {
                evt.Children.Add(conn.RebuildEvent());
            }

            evt.Filename = (string)GetOption("Filename").Value;
            evt.MainFiletype = GetOption("MainFiletype").GetByte();
            evt.FiletypeGen7 = GetOption("FiletypeGen7").GetByte();
            evt.LoadPriority = GetOption("LoadPriority").GetByte();

            evt.Mix = (string)GetOption("Mix").Value;
            evt.RoutingTable = (string)GetOption("RoutingTable").Value;

            evt.Global = GetOption("Global").GetByte();
            evt.Regional = GetOption("Regional").GetByte();
            evt.HasOcclusion = GetOption("NoOcclusion").GetByte();
            evt.UpdateMix = GetOption("MixUpdate").GetByte();
            evt.NoRear = GetOption("NoRear").GetByte();
            evt.ForceNonPos = GetOption("ForceNonPos").GetByte();
            evt.OverrideUserMusic = GetOption("OverrideUserMusic").GetByte();
            evt.Loopable = GetOption("ShouldLoopFlag").GetByte();
            evt.FallOffTypeInverse = GetOption("FallOffTypeInverse").GetByte();

            evt.FieldAngleMin = GetOption("FieldAngleMin").GetFloat();
            evt.FieldAngleMax = GetOption("FieldAngleMax").GetFloat();
            evt.BleedAngle = GetOption("BleedAngle").GetFloat();
            evt.BleedNear = GetOption("BleedNear").GetFloat();
            evt.BleedFar = GetOption("BleedFar").GetFloat();
            evt.LowPassNear = GetOption("LowPassNear").GetFloat();
            evt.LowPassFar = GetOption("LowPassFar").GetFloat();
            evt.FadeIn = GetOption("FadeIn").GetFloat();
            evt.FadeOut = GetOption("FadeOut").GetFloat();
            evt.PitchRampUpTime = GetOption("PitchRampUpTime").GetFloat();
            evt.PitchRampDownTime = GetOption("PitchRampDownTime").GetFloat();
            evt.Near = GetOption("Near").GetFloat();
            evt.Far = GetOption("Far").GetFloat();
            evt.Volume = GetOption("Volume").GetFloat();
            evt.RandomVolume = GetOption("RandomVolume").GetFloat();
            evt.Pitch = GetOption("Pitch").GetFloat();
            evt.RandomPitch = GetOption("RandomPitch").GetFloat();
            evt.PitchTimeScaleFactor = GetOption("PitchTimeScaleFactor").GetFloat();
            evt.LowPass = GetOption("LowPass").GetFloat();
            evt.RandomLowPass = GetOption("RandomLowPass").GetFloat();
            evt.LowPassQ = GetOption("LowPassQ").GetFloat();
            evt.LfeMix = GetOption("LFEMix").GetFloat();
            evt.ReverbWetMix = GetOption("ReverbWetMix").GetFloat();
            evt.StartOffset = GetOption("StartOffset").GetFloat();
            evt.RandomStartOffset = GetOption("RandomStartOffset").GetFloat();
            evt.DopplerEffectScalar = GetOption("DopplerEffectScalar").GetFloat();
            evt.StartPitch = GetOption("StartPitch").GetFloat();
            evt.TargetPitch = GetOption("TargetPitch").GetFloat();
            evt.EndPitch = GetOption("EndPitch").GetFloat();

            evt.Characters.Clear();
            foreach (var c in Children)
            {
                evt.Characters.Add((SoundEvent12)c.RebuildEvent());
            }

            return evt;
        }

        public SoundEvent10ViewModel()
        {
            SoundEvent6 name = new();
            SoundEvent10 evt10 = new();

            evt10.Children.Add(name);

            BuildFromEvent(evt10);
        }

        public SoundEvent10ViewModel(SoundEvent10 evt)
        {
            BuildFromEvent(evt);
        }
    }
}
