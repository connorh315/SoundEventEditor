using Avalonia.Animation.Easings;
using Avalonia.Controls.Chrome;
using SoundEventEditor.SoundEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundEventEditor.ViewModels.SoundEvents
{
    public class SoundEvent3 : SoundEventViewModel
    {
        public override string Title => "Group";

        public override string BorderColour => "Yellow";

        private static readonly Dictionary<byte, string> maxVoicesBehaviourChoices = new()
        {
            { 0, "Default" },
            { 1, "Replace" }
        };

        protected override void BuildFromEvent(SoundEvent rawEvent)
        {
            SEVT_3 evt = (SEVT_3)rawEvent;

            Model = evt;

            Options = new()
            {
                new StringOptionViewModel("MaxVoices", evt.MaxVoices, true, false),
                new SelectionOptionViewModel("MaxVoicesBehaviour", maxVoicesBehaviourChoices, maxVoicesBehaviourChoices[evt.MaxVoicesBehaviour]),
                new StringOptionViewModel("Behaviour", evt.Behaviour, true, false),
                new BoolOptionViewModel("RetryOnFailure", evt.RetryOnFailure)
            };

            SelectableChildren = new()
            {
                SoundEventType.StreamingSpeech,
                SoundEventType.Conversation,
                SoundEventType.Event,
                SoundEventType.Group,
                SoundEventType.Speech
            };

            if (evt.Connections != null && evt.Connections.Count > 0)
            {
                Connections = new();
                foreach (var conn in evt.Connections)
                {
                    Connections.Add(ConvertToViewModel(conn));
                }
            }

            base.BuildFromEvent(rawEvent);
        }

        public override SoundEvent RebuildEvent()
        {
            SEVT_3 evt = (SEVT_3)Model;

            evt.MaxVoices = GetOption("MaxVoices").GetByte();
            evt.MaxVoicesBehaviour = GetOption("MaxVoicesBehaviour").GetByte();
            evt.Behaviour = GetOption("Behaviour").GetByte();
            evt.RetryOnFailure = GetOption("RetryOnFailure").GetByte();

            if (evt.Connections != null)
            {
                evt.Connections = new(); // This is used once, it's not justifiable to fully implement.
                foreach (var conn in Connections)
                {
                    evt.Connections.Add(conn.RebuildEvent());
                }
            }

            base.RebuildEvent();

            return evt;
        }

        public SoundEvent3()
        {
            BuildFromEvent(new SEVT_3());
        }

        public SoundEvent3(SEVT_3 evt)
        {
            BuildFromEvent(evt);
        }
    }
}
