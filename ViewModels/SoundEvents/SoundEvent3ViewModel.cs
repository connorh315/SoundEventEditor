using SoundEventEditor.SoundEvents;
using System.Collections.Generic;

namespace SoundEventEditor.ViewModels.SoundEvents
{
    public class SoundEvent3ViewModel : SoundEventViewModel
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
            SoundEvent3 evt = (SoundEvent3)rawEvent;

            Model = evt;

            Options =
            [
                new StringOptionViewModel("MaxVoices", evt.MaxVoices, true, false),
                new SelectionOptionViewModel("MaxVoicesBehaviour", maxVoicesBehaviourChoices, maxVoicesBehaviourChoices[evt.MaxVoicesBehaviour]),
                new StringOptionViewModel("Behaviour", evt.Behaviour, true, false),
                new BoolOptionViewModel("RetryOnFailure", evt.RetryOnFailure)
            ];

            SelectableChildren =
            [
                SoundEventType.StreamingSpeech,
                SoundEventType.Conversation,
                SoundEventType.Event,
                SoundEventType.Group,
                SoundEventType.Speech
            ];

            if (evt.Connections != null && evt.Connections.Count > 0)
            {
                Connections = [];
                foreach (var conn in evt.Connections)
                {
                    Connections.Add(ConvertToViewModel(conn));
                }
            }

            base.BuildFromEvent(rawEvent);
        }

        public override SoundEvent RebuildEvent()
        {
            SoundEvent3 evt = (SoundEvent3)Model;

            evt.MaxVoices = GetOption("MaxVoices").GetByte();
            evt.MaxVoicesBehaviour = GetOption("MaxVoicesBehaviour").GetByte();
            evt.Behaviour = GetOption("Behaviour").GetByte();
            evt.RetryOnFailure = GetOption("RetryOnFailure").GetByte();

            if (evt.Connections != null)
            {
                evt.Connections = []; // This is used once, it's not justifiable to fully implement.
                foreach (var conn in Connections)
                {
                    evt.Connections.Add(conn.RebuildEvent());
                }
            }

            base.RebuildEvent();

            return evt;
        }

        public SoundEvent3ViewModel()
        {
            BuildFromEvent(new SoundEvent3());
        }

        public SoundEvent3ViewModel(SoundEvent3 evt)
        {
            BuildFromEvent(evt);
        }
    }
}
