using Avalonia.Controls.Chrome;
using SoundEventEditor.SoundEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundEventEditor.ViewModels.SoundEvents
{
    public class SoundEvent7 : SoundEventViewModel
    {
        public override string Title => "Sequence";

        public override string BorderColour => "Red";

        protected override void BuildFromEvent(SoundEvent rawEvent)
        {
            SEVT_7 evt = (SEVT_7)rawEvent;

            Model = evt;

            foreach (SoundEvent child in rawEvent.Children)
            {
                Connections.Add(ConvertToViewModel(child));
            }

            Options = new()
            {
                new StringOptionViewModel("Delay", evt.Delay, true, true)
            };

            SelectableChildren = new() { SoundEventType.BusConnection };
        }

        public SoundEvent7()
        {
            BuildFromEvent(new SEVT_7());
        }

        public SoundEvent7(SEVT_7 evt)
        {
            BuildFromEvent(evt);
        }
    }
}
