using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JLib.Localization;

public class LocalizationTextTestScript : MonoBehaviour
{
    public void OnEnglishClicked()
    {
        LocalizationTextTable.Instance.ChangeLanguage(SystemLanguage.English);
    }

    public void OnKoreanClicked()
    {
        LocalizationTextTable.Instance.ChangeLanguage(SystemLanguage.Korean);
    }

    public void OnChineseClicked()
    {
        LocalizationTextTable.Instance.ChangeLanguage(SystemLanguage.Chinese);
    }

    public void OnJapaneseClicked()
    {
        LocalizationTextTable.Instance.ChangeLanguage(SystemLanguage.Japanese);
    }
}