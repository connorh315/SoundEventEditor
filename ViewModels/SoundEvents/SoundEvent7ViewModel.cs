using SoundEventEditor.SoundEvents;

namespace SoundEventEditor.ViewModels.SoundEvents
{
    public class SoundEvent7ViewModel : SoundEventViewModel
    {
        public override string Title => "Sequence";

        public override string BorderColour => "Red";

        protected override void BuildFromEvent(SoundEvent rawEvent)
        {
            SoundEvent7 evt = (SoundEvent7)rawEvent;

            Model = evt;

            foreach (SoundEvent child in rawEvent.Children)
            {
                Connections.Add(ConvertToViewModel(child));
            }

            Options =
            [
                new StringOptionViewModel("Delay", evt.Delay, true, true)
            ];

            SelectableChildren = new() { SoundEventType.BusConnection };
        }

        public SoundEvent7ViewModel()
        {
            BuildFromEvent(new SoundEvent7());
        }

        public SoundEvent7ViewModel(SoundEvent7 evt)
        {
            BuildFromEvent(evt);
        }
    }
}
