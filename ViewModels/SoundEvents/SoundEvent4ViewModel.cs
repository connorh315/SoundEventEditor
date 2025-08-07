using SoundEventEditor.SoundEvents;

namespace SoundEventEditor.ViewModels.SoundEvents
{
    public class SoundEvent4ViewModel : SoundEventViewModel
    {
        public override string Title => "Bus Connection";

        public override string BorderColour => "Green";

        protected override void BuildFromEvent(SoundEvent rawEvent)
        {
            SoundEvent4 evt = (SoundEvent4)rawEvent;

            Model = evt;

            Options =
            [
                new StringListOptionViewModel("Buses", evt.Buses),
                //new StringOptionViewModel("Bus", evt.Buses[0])
            ];
        }

        public override SoundEvent RebuildEvent()
        {
            SoundEvent4 evt = (SoundEvent4)Model;

            evt.Buses = ((StringListOptionViewModel)GetOption("Buses")).StringValues;

            return evt;
        }

        public SoundEvent4ViewModel()
        {
            BuildFromEvent(new SoundEvent4());
        }

        public SoundEvent4ViewModel(SoundEvent4 evt)
        {
            BuildFromEvent(evt);
        }
    }
}
