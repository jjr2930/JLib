using UnityEngine.Events;
namespace JLib
{
    public class CommonPopupEventParameter
    {
        public string titleKey;
        public string descriptKey;
        public string btn1LabelKey;
        public string btn2LabelKey;
        public string cancelLabelKey;
        public UnityAction btn1Action;
        public UnityAction btn2Action;
        public UnityAction cancelAction;
        public bool pauseGame;
    }
}
