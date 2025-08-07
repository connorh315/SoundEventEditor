using SoundEventEditor.SoundEvents;

namespace SoundEventEditor.ViewModels.SoundEvents
{
    public class SoundEvent6ViewModel : SoundEventViewModel
    {
        public override string Title => "Name";

        public override string BorderColour => "Red";

        protected override void BuildFromEvent(SoundEvent rawEvent)
        {
            SoundEvent6 evt = (SoundEvent6)rawEvent;

            Model = evt;

            Options =
            [
                new StringOptionViewModel("Name", evt.Name),
                new BoolOptionViewModel("SubAlwaysOn", evt.SubAlwaysOn),
                new BoolOptionViewModel("HighPriority", evt.HighPriority)
            ];
        }

        public override SoundEvent RebuildEvent()
        {
            SoundEvent6 evt = (SoundEvent6)Model;

            evt.Name = (string)GetOption("Name").Value;
            evt.SubAlwaysOn = GetOption("SubAlwaysOn").GetByte();
            evt.HighPriority = GetOption("HighPriority").GetByte();

            return evt;
        }

        public SoundEvent6ViewModel()
        {
            BuildFromEvent(new SoundEvent6());
        }

        public SoundEvent6ViewModel(SoundEvent6 evt)
        {
            BuildFromEvent(evt);
        }
    }
}
