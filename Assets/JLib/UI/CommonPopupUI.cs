using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace JLib.UI
{
    public class CommonPopupUIManager : MonoSingle<CommonPopupUIManager>
    {
        [SerializeField]
        Button okButton = null;

        [SerializeField]
        TextMeshProUGUI okButtonText = null;

        [SerializeField]
        Button cancelButton = null;

        [SerializeField]
        TextMeshProUGUI cancelButtonText = null;

        [SerializeField]
        TextMeshProUGUI titleText = null;

        [SerializeField]
        TextMeshProUGUI messageText = null;

        Action okCallback = null;
        Action cancelCallack = null;

        public void Awake()
        {
            okButton.onClick.AddListener(new UnityAction(OnOkClicked));
            cancelButton.onClick.AddListener(new UnityAction(OnCancelClicked));

            gameObject.SetActive(false);
        }

        public void Show(string title, string message, string okText, string cancelText, Action okCallback , Action cancelCallback )
        {
            this.titleText.text = title;
            this.messageText.text = message;
            this.okButtonText.text = okText;
            this.cancelButtonText.text = cancelText;

            this.okCallback = okCallback;
            this.cancelCallack = cancelCallback;
        }

        public void OnOkClicked()
        {
            okCallback?.Invoke();
            gameObject.SetActive(false);
        }

        public void OnCancelClicked()
        {
            cancelCallack?.Invoke();
            gameObject.SetActive(false);
        }
    }
}