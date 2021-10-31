using UnityEngine;

namespace Features.Gui
{
    [CreateAssetMenu(fileName = "NewMouseCursor", menuName = "Gui/MouseCursor")]
    public class MouseCursor_SO: ScriptableObject
    {
        public Texture2D cursorTexture;
        public Vector2 hotSpot = Vector2.zero;
    }
}
