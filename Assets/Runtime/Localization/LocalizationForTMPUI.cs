using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

namespace JLib.Localization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizationForTMPUI : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI text = null;

        [SerializeField,HideInInspector]
        string key = null;

        public string Key 
        { 
            get => key;
            set
            {
                key = value;
                text.text = LocalizationTextTable.Instance.GetText(key);
            }
        }
        private void Reset()
        {
            text = GetComponent<TextMeshProUGUI>(); 
        }

        public void Start()
        {
            text.text = LocalizationTextTable.Instance.GetText(key);
        }

        public void SetKeyOnly(string value)
        {
            key = value;
        }

        public void SetTextOnly(string value)
        {
            text.text = value;
        }

        public void OnSystemLanguageChanged(SystemLanguage language)
        {
            text.font = LocalizationConfig.Instance.GetFont(language);
            Start();
        }
    }
}
