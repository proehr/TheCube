using DataStructures.Variables;
using UnityEngine;

namespace Features.Gui.PauseMenu
{
    public class PauseMenu : MonoBehaviour
    {
        private bool isOpen;
        [SerializeField] private BoolVariable pauseState;
    
        public void Toggle()
        {
            if (this.pauseState.Get())
            {
                this.Open();
            }
            else
            {
                this.Close();
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
