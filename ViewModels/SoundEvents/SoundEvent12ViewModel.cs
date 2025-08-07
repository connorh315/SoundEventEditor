using SoundEventEditor.SoundEvents;

namespace SoundEventEditor.ViewModels.SoundEvents
{
    public class SoundEvent12ViewModel : SoundEventViewModel
    {
        public override string Title => "Character";

        public override string BorderColour => "Green";

        protected override void BuildFromEvent(SoundEvent rawEvent)
        {
            SoundEvent12 evt = (SoundEvent12)rawEvent;

            Model = evt;

            Options =
            [
                new StringOptionViewModel("CharacterName", evt.CharacterName),
                new BoolOptionViewModel("NoFlappy", evt.NoFlappy, true),
                new StringOptionViewModel("FacialAnimationSet", evt.FacialAnimationSet),
                new StringOptionViewModel("FacialAnimationAction", evt.FacialAnimationAction),
                new StringOptionViewModel("BodyAnimationSet", evt.BodyAnimationSet),
                new StringOptionViewModel("BodyAnimationAction", evt.BodyAnimationAction),
            ];

            Children = null;
        }

        public override SoundEvent RebuildEvent()
        {
            SoundEvent12 evt = (SoundEvent12)Model;

            evt.CharacterName = (string)GetOption("CharacterName").Value;
            evt.NoFlappy = GetOption("NoFlappy").GetByte();
            evt.FacialAnimationSet = (string)GetOption("FacialAnimationSet").Value;
            evt.FacialAnimationAction = (string)GetOption("FacialAnimationAction").Value;
            evt.BodyAnimationSet = (string)GetOption("BodyAnimationSet").Value;
            evt.BodyAnimationAction = (string)GetOption("BodyAnimationAction").Value;

            return evt;
        }

        public SoundEvent12ViewModel(SoundEvent12 evt)
        {
            BuildFromEvent(evt);
        }

        public SoundEvent12ViewModel()
        {
            BuildFromEvent(new SoundEvent12());
        }
    }
}
