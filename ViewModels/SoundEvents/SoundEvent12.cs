using SoundEventEditor.SoundEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundEventEditor.ViewModels.SoundEvents
{
    public class SoundEvent12 : SoundEventViewModel
    {
        public override string Title => "Character";

        public override string BorderColour => "Green";

        protected override void BuildFromEvent(SoundEvent rawEvent)
        {
            SEVT_12 evt = (SEVT_12)rawEvent;

            Model = evt;

            Options = new()
            {
                new StringOptionViewModel("CharacterName", evt.CharacterName),
                new BoolOptionViewModel("NoFlappy", evt.NoFlappy, true),
                new StringOptionViewModel("FacialAnimationSet", evt.FacialAnimationSet),
                new StringOptionViewModel("FacialAnimationAction", evt.FacialAnimationAction),
                new StringOptionViewModel("BodyAnimationSet", evt.BodyAnimationSet),
                new StringOptionViewModel("BodyAnimationAction", evt.BodyAnimationAction),
            };

            Children = null;
        }

        public override SoundEvent RebuildEvent()
        {
            SEVT_12 evt = (SEVT_12)Model;

            evt.CharacterName = (string)GetOption("CharacterName").Value;
            evt.NoFlappy = GetOption("NoFlappy").GetByte();
            evt.FacialAnimationSet = (string)GetOption("FacialAnimationSet").Value;
            evt.FacialAnimationAction = (string)GetOption("FacialAnimationAction").Value;
            evt.BodyAnimationSet = (string)GetOption("BodyAnimationSet").Value;
            evt.BodyAnimationAction = (string)GetOption("BodyAnimationAction").Value;

            return evt;
        }

        public SoundEvent12(SEVT_12 evt)
        {
            BuildFromEvent(evt);
        }

        public SoundEvent12()
        {
            BuildFromEvent(new SEVT_12());
        }
    }
}
