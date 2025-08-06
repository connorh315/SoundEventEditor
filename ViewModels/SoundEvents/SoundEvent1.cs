using Avalonia.Controls.Chrome;
using SoundEventEditor.SoundEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SoundEventEditor.ViewModels.SoundEvents
{
    public class SoundEvent1 : SoundEventViewModel
    {
        public override string Title => "Sample";

        public override string BorderColour => "White";

        protected override void BuildFromEvent(SoundEvent rawEvent)
        {
            SEVT_1 evt = (SEVT_1)rawEvent;

            Model = evt;

            Connections = new();

            foreach (SoundEvent child in rawEvent.Children)
            {
                if (child is SEVT_4 busConn)
                {
                    Connections.Add(ConvertToViewModel(busConn));
                }
                else
                {
                    throw new Exception($"Unexpected child of SoundEvent1 - {child.GetType()}");
                }
            }


            Options = new()
            {
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
            };

            SelectableChildren = new()
            {
            };
        }

        public override SoundEvent RebuildEvent()
        {
            SEVT_1 evt = (SEVT_1)Model;

            evt.Children = new();

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

            return evt;
        }

        public SoundEvent1()
        {
            BuildFromEvent(new SEVT_1());
        }

        public SoundEvent1(SEVT_1 evt)
        {
            BuildFromEvent(evt);
        }
    }
}
