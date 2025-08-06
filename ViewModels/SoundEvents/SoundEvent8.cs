using Avalonia.Controls.Chrome;
using SoundEventEditor.SoundEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundEventEditor.ViewModels.SoundEvents
{
    public class SoundEvent8 : SoundEventViewModel
    {
        public override string Title => "Conversation";

        public override string BorderColour => "Blue";

        protected override void BuildFromEvent(SoundEvent rawEvent)
        {
            SEVT_8 evt = (SEVT_8)rawEvent;

            Model = evt;

            Options = new()
            {
                new StringOptionViewModel("PlayPriority", evt.PlayPriority, true, false),

                new BoolOptionViewModel("OnFailedInterruptWait", evt.OnFailedInterruptWait),
                new BoolOptionViewModel("PriorityOverSame", evt.PriorityOverSame, true),
            };

            SelectableChildren = new()
            {
                SoundEventType.Group,
                SoundEventType.StreamingSpeech,
                SoundEventType.Speech
            };

            Connections = new();
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
            SEVT_8 evt = (SEVT_8)Model;
            evt.PlayPriority = GetOption("PlayPriority").GetByte();
            evt.OnFailedInterruptWait = GetOption("OnFailedInterruptWait").GetByte();
            evt.PriorityOverSame = GetOption("PriorityOverSame").GetByte();

            evt.Connections = new();
            foreach (var conn in Connections)
            {
                evt.Connections.Add(conn.RebuildEvent());
            }

            base.RebuildEvent();

            return evt;
        }

        public SoundEvent8()
        {
            BuildFromEvent(new SEVT_8());
        }

        public SoundEvent8(SEVT_8 evt)
        {
            BuildFromEvent(evt);
        }
    }
}
