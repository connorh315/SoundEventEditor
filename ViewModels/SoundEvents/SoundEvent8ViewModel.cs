using SoundEventEditor.SoundEvents;

namespace SoundEventEditor.ViewModels.SoundEvents
{
    public class SoundEvent8ViewModel : SoundEventViewModel
    {
        public override string Title => "Conversation";

        public override string BorderColour => "Blue";

        protected override void BuildFromEvent(SoundEvent rawEvent)
        {
            SoundEvent8 evt = (SoundEvent8)rawEvent;

            Model = evt;

            Options =
            [
                new StringOptionViewModel("PlayPriority", evt.PlayPriority, true, false),

                new BoolOptionViewModel("OnFailedInterruptWait", evt.OnFailedInterruptWait),
                new BoolOptionViewModel("PriorityOverSame", evt.PriorityOverSame, true),
            ];

            SelectableChildren =
            [
                SoundEventType.Group,
                SoundEventType.StreamingSpeech,
                SoundEventType.Speech
            ];

            Connections = [];
            if (evt.Connections != null && evt.Connections.Count > 0)
            {
                foreach (var conn in evt.Connections)
                {
                    Connections.Add(ConvertToViewModel(conn));
                }
            }

            base.BuildFromEvent(rawEvent);
        }

        public override SoundEvent RebuildEvent()
        {
            SoundEvent8 evt = (SoundEvent8)Model;
            evt.PlayPriority = GetOption("PlayPriority").GetByte();
            evt.OnFailedInterruptWait = GetOption("OnFailedInterruptWait").GetByte();
            evt.PriorityOverSame = GetOption("PriorityOverSame").GetByte();

            evt.Connections = [];
            foreach (var conn in Connections)
            {
                evt.Connections.Add(conn.RebuildEvent());
            }

            base.RebuildEvent();

            return evt;
        }

        public SoundEvent8ViewModel()
        {
            BuildFromEvent(new SoundEvent8());
        }

        public SoundEvent8ViewModel(SoundEvent8 evt)
        {
            BuildFromEvent(evt);
        }
    }
}
