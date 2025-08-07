using SoundEventEditor.SoundEvents;
using System.Collections.Generic;

namespace SoundEventEditor.ViewModels.SoundEvents
{
    public class SoundEvent0ViewModel : SoundEventViewModel
    {
        public override string Title => "Event";

        public override string BorderColour => "White";

        private static readonly Dictionary<byte, string> _maxVoicesBehaviourChoices = new()
        {
            { 0, "Default" },
            { 1, "Replace" }
        };

        protected override void BuildFromEvent(SoundEvent rawEvent)
        {
            SoundEvent0 soundEvent0 = (SoundEvent0)rawEvent;

            Model = soundEvent0;

            foreach (var child in rawEvent.Children)
            {
                Children.Add(ConvertToViewModel(child));
            }

            Options =
            [
                new StringOptionViewModel("MaxVoices", soundEvent0.MaxVoices, true, false),
                new SelectionOptionViewModel("MaxVoicesBehaviour", _maxVoicesBehaviourChoices, _maxVoicesBehaviourChoices[soundEvent0.MaxVoicesBehaviour]),
            ];

            SelectableChildren =
            [
                SoundEventType.Sample,
                SoundEventType.Group,
                SoundEventType.Sequence
            ];
        }

        public SoundEvent0ViewModel()
        {
            BuildFromEvent(new SoundEvent0());
        }

        public SoundEvent0ViewModel(SoundEvent0 soundEvent0)
        {
            BuildFromEvent(soundEvent0);
        }
    }
}
