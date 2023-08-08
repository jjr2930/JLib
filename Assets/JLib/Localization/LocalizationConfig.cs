using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;

namespace JLib.Localization
{
    [Serializable]
    public class FontInfo
    {
        /// <summary>
        /// system language has bug when serialization and deserialization
        /// </summary>
        public string language;
        public AssetReference fontAsset;
    }

    [CreateAssetMenu(menuName = "Localization Config")]
    public class LocalizationConfig : ScriptableObject
    {
        static LocalizationConfig instance;
        public static LocalizationConfig Instance
        {
            get
            {
                if (null == instance)
                {
                    var configs = Resources.LoadAll<LocalizationConfig>("");
                    if (1 != configs.Length)
                        throw new InvalidOperationException("configs.length != 1");

                    instance = configs[0];
                    instance.Init();
                }
                return instance;
            }
        }

        [SerializeField]
        AssetReference defaultFontAsset;

        [SerializeField]
        List<FontInfo> fonts = new List<FontInfo>();

        TMP_FontAsset loadedDefaultFont;

        Dictionary<SystemLanguage, TMP_FontAsset> loadedFonts
            = new Dictionary<SystemLanguage, TMP_FontAsset>();

        public TMP_FontAsset GetFont(SystemLanguage language)
        {
            TMP_FontAsset found = null;
            if(loadedFonts.TryGetValue(language, out found))
            {
                return found;
            }

            return loadedDefaultFont;
        }

        private void Init()
        {
            var loadedObjects =  defaultFontAsset.LoadAssetAsync<IList<UnityEngine.Object>>().WaitForCompletion();
            for (int i = 0; i < loadedObjects.Count; i++)
            {
                if (loadedObjects[i] is TMP_FontAsset)
                {
                    loadedDefaultFont = loadedObjects[i] as TMP_FontAsset;
                    break;
                }
            }

            for (int i = 0; i < fonts.Count; i++)
            {
                loadedObjects = fonts[i].fontAsset.LoadAssetAsync<IList<UnityEngine.Object>>().WaitForCompletion();
                for (int j = 0; j < loadedObjects.Count; j++)
                {
                    if (loadedObjects[j] is TMP_FontAsset)
                    {
                        SystemLanguage language = Enum.Parse<SystemLanguage>(fonts[i].language);
                        loadedFonts.Add(language, loadedObjects[j] as TMP_FontAsset);
                        break;                        
                    }
                }
            }
        }
    }
}
