using BrexitTime.UI;

namespace BrexitTime.Managers
{
    public class AudioManager
    {
        private readonly ContentChest _contentChest;

        public AudioManager(ContentChest contentChest)
        {
            _contentChest = contentChest;
        }

        public void OnButtonClick(UIElement obj)
        {
            _contentChest.Click.Play();
        }

        public void OnSelect()
        {
            _contentChest.Select.Play();
        }

        public void OnStart()
        {
            _contentChest.Start.Play();
        }
    }
}