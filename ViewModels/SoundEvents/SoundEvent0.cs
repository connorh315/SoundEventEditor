using Avalonia.Controls.Chrome;
using SoundEventEditor.SoundEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundEventEditor.ViewModels.SoundEvents
{
    public class SoundEvent0 : SoundEventViewModel
    {
        public override string Title => "Event";

        public override string BorderColour => "White";

        private static readonly Dictionary<byte, string> maxVoicesBehaviourChoices = new()
        {
            { 0, "Default" },
            { 1, "Replace" }
        };

        protected override void BuildFromEvent(SoundEvent rawEvent)
        {
            SEVT_0 evt = (SEVT_0)rawEvent;

            Model = evt;

            foreach (var child in rawEvent.Children)
            {
                Children.Add(ConvertToViewModel(child));
            }

            Options = new()
            {
                new StringOptionViewModel("MaxVoices", evt.MaxVoices, true, false),
                new SelectionOptionViewModel("MaxVoicesBehaviour", maxVoicesBehaviourChoices, maxVoicesBehaviourChoices[evt.MaxVoicesBehaviour]),
            };

            SelectableChildren = new()
            {
                SoundEventType.Sample,
                SoundEventType.Group,
                SoundEventType.Sequence
            };
        }

        public SoundEvent0()
        {
            BuildFromEvent(new SEVT_0());
        }

        public SoundEvent0(SEVT_0 evt)
        {
            BuildFromEvent(evt);
        }
    }
}
