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
    }
}