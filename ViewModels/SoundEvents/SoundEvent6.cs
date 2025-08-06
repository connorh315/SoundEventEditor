using Avalonia.Controls.Chrome;
using SoundEventEditor.SoundEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundEventEditor.ViewModels.SoundEvents
{
    public class SoundEvent6 : SoundEventViewModel
    {
        public override string Title => "Name";

        public override string BorderColour => "Red";

        protected override void BuildFromEvent(SoundEvent rawEvent)
        {
            SEVT_6 evt = (SEVT_6)rawEvent;

            Model = evt;

            Options = new()
            {
                new StringOptionViewModel("Name", evt.Name),
                new BoolOptionViewModel("SubAlwaysOn", evt.SubAlwaysOn),
                new BoolOptionViewModel("HighPriority", evt.HighPriority)
            };
        }

        public override SoundEvent RebuildEvent()
        {
            SEVT_6 evt = (SEVT_6)Model;

            evt.Name = (string)GetOption("Name").Value;
            evt.SubAlwaysOn = GetOption("SubAlwaysOn").GetByte();
            evt.HighPriority = GetOption("HighPriority").GetByte();

            return evt;
        }

        public SoundEvent6()
        {
            BuildFromEvent(new SEVT_6());
        }

        public SoundEvent6(SEVT_6 evt)
        {
            BuildFromEvent(evt);
        }
    }
}
