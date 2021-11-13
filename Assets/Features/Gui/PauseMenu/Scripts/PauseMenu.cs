using UnityEngine;

namespace Features.Gui.PauseMenu
{
    public class PauseMenu : MonoBehaviour
    {
        private bool isOpen;
    
        public void Toggle()
        {
            if (this.isOpen)
            {
                this.Close();
            }
            else
            {
                this.Open();
            }
        }

        public void Close()
        {
            this.isOpen = false;
            this.gameObject.SetActive(this.isOpen);
        }

        private void Open()
        {
            this.isOpen = true;
            this.gameObject.SetActive(this.isOpen);
        }

        public void OnExitGame()
        {
            Application.Quit();
        }
    }
}
