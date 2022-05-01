using System;
using UnityEngine;

namespace Features.Gui
{
    [CreateAssetMenu(fileName = "NewMouseCursorHandler", menuName = "Gui/MouseCursorHandler")]
    public class MouseCursorHandler_SO : ScriptableObject
    {
        [SerializeField] private CursorMode cursorMode = CursorMode.Auto;
        [SerializeField] private MouseCursor_SO cursorDefault;
        [SerializeField] private MouseCursor_SO cursorExcavate;
        private MouseCursorLook currentLook;
        
        public void SetCursorToDefault()
        {
            SetCursor(this.cursorDefault);
            this.currentLook = MouseCursorLook.Default;
        }

        public void SetCursor(MouseCursorLook cursorLook)
        {
            if (currentLook == cursorLook) return;
            switch (cursorLook)
            {
                case MouseCursorLook.Default:
                    SetCursor(this.cursorDefault);
                    break;
                case MouseCursorLook.Excavate:
                    SetCursor(this.cursorExcavate);
                    break;
                case MouseCursorLook.TransportLine:
                    SetCursor(this.cursorDefault);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cursorLook), cursorLook, null);
            }

            this.currentLook = cursorLook;
        }

        private void SetCursor(MouseCursor_SO cursor)
        {
            Cursor.SetCursor(cursor.cursorTexture, cursor.hotSpot, cursorMode);
        }
    }
    
    public enum MouseCursorLook
    {
        Default,
        Excavate,
        TransportLine
    }
}
