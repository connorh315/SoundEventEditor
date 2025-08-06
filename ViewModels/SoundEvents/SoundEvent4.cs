using Avalonia.Controls.Chrome;
using SoundEventEditor.SoundEvents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundEventEditor.ViewModels.SoundEvents
{
    public class SoundEvent4 : SoundEventViewModel
    {
        public override string Title => "Bus Connection";

        public override string BorderColour => "Green";

        protected override void BuildFromEvent(SoundEvent rawEvent)
        {
            SEVT_4 evt = (SEVT_4)rawEvent;

            Model = evt;

            Options = new()
            {
                new StringListOptionViewModel("Buses", evt.Buses),
                //new StringOptionViewModel("Bus", evt.Buses[0])
            };
        }

        public override SoundEvent RebuildEvent()
        {
            SEVT_4 evt = (SEVT_4)Model;

            evt.Buses = ((StringListOptionViewModel)GetOption("Buses")).StringValues;

            return evt;
        }

        public SoundEvent4()
        {
            BuildFromEvent(new SEVT_4());
        }

        public SoundEvent4(SEVT_4 evt)
        {
            BuildFromEvent(evt);
        }
    }
}
