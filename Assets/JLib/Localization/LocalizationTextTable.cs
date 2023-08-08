using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Text;
using System.IO;
using TMPro;

namespace JLib.Localization
{
    [Serializable]
    public class LocalizationTableScheme
    {
        public string key;
        public string english;
        public string korean;
        public string chinese;
        public string japanese; 
    }
    public enum ErrorType
    {
        NoError,
        DuplicateKey,
        EmptyString,
    }

    public struct ErrorInfo
    {
        public ErrorType errorType;
        public string column;
        public int line;
    }

    [CreateAssetMenu(menuName = "Tables/LocalizationTable")]
    public class LocalizationTextTable : ScriptableObject
    {
        [SerializeField] 
        List<LocalizationTableScheme> data = new List<LocalizationTableScheme>();

        Dictionary<string, string> localizationTextMap = new Dictionary<string, string>();

        StringBuilder stringBuilder = new StringBuilder();
        /// <summary>
        /// 어차피 메인쓰레드만 이용할텐데 이게 의미 있나?
        /// </summary>
        static object mutex = new object();
        static LocalizationTextTable instance = null;
        public static LocalizationTextTable Instance
        {
            get
            {
                lock (mutex)
                { 
                    if (null == instance)
                    {
                        var loadedList = Addressables.LoadAssetsAsync<UnityEngine.Object>("Tables", null).WaitForCompletion();
                        for (int i = 0; i < loadedList.Count; i++)
                        {
                            if (loadedList[i] is LocalizationTextTable)
                            {
                                instance = loadedList[i] as LocalizationTextTable;
                                instance.ChangeLanguage(Application.systemLanguage);
                                Debug.Log("localization table loaded");
                                break;
                            }
                        }
                    }
                }

                return instance;                
            }
        }

        public List<LocalizationTableScheme> Data { get => data; }
        
        public void Export(string path)
        {
            stringBuilder.Clear();
            stringBuilder.Append("key\t");
            stringBuilder.Append("english\t");
            stringBuilder.Append("korean\t");
            stringBuilder.Append("chinese\t");
            stringBuilder.Append("japanese\t");
            stringBuilder.Append("\n");
            for (int i = 0; i < data.Count; i++)
            {
                stringBuilder.Append(data[i].key);
                stringBuilder.Append("\t");
                stringBuilder.Append(data[i].english);
                stringBuilder.Append("\t");
                stringBuilder.Append(data[i].korean);
                stringBuilder.Append("\t");
                stringBuilder.Append(data[i].chinese);
                stringBuilder.Append("\t");
                stringBuilder.Append(data[i].japanese);
                stringBuilder.Append("\n");
            }

            if (File.Exists(path))
                File.Delete(path);

            File.WriteAllText(path, stringBuilder.ToString(), Encoding.UTF8);
        }

        public void ImportFromTSV(string tsv)
        {
            var lines = tsv.Split(new string[] { Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries);

            data.Clear();
            for (int i = 1; i < lines.Length; i++)
            {
                string[] splits = lines[i].Split('\t');
                LocalizationTableScheme row = new LocalizationTableScheme();
                row.key = splits[0];
                //1 is description
                row.english = splits[2];
                row.korean = splits[3];
                row.chinese = splits[4];
                row.japanese = splits[5];

                data.Add(row);
            }
        }

        public void ChangeLanguage(SystemLanguage language)
        {
            localizationTextMap.Clear();
            int count = data.Count;
            for (int i = 0; i < count; i++)
            {
                var row = Instance.data[i];
                string localiztionText = "";
                switch (language)
                {
                    case SystemLanguage.Korean:
                        localiztionText = row.korean;
                        break;

                    case SystemLanguage.English:
                        localiztionText = row.english;
                        break;

                    case SystemLanguage.Chinese:
                        localiztionText = row.chinese;
                        break;

                    case SystemLanguage.Japanese:
                        localiztionText = row.japanese;
                        break;

                    default:
                        throw new System.NotImplementedException($"Language ({language}) is not supported yet");
                }

                Instance.localizationTextMap.Add(row.key, localiztionText);
            }

            FindFirstObjectByType<LocalizationForTMPUI>().OnSystemLanguageChanged(language);
        }

        public string GetText(string key)
        {
            if (0 == Instance.localizationTextMap.Count)
            { 
                Debug.LogError("localization text table is not loaded");
                return key;
            }

            string found = null;
            if (!Instance.localizationTextMap.TryGetValue(key, out found))
            {
                Debug.LogError($"There is no such a {key}");
                return key;
            }

            return found;
        }

        public string[] GetKeyList()
        {
            string[] keyList = new string[data.Count];
            for (int i = 0; i < data.Count; i++)
            {
                keyList[i] = data[i].key;
            }

            return keyList;
        }


        public ErrorInfo FindError()
        {
            ErrorInfo info = new ErrorInfo();
            info.errorType = ErrorType.NoError;
            HashSet<string> keys = new HashSet<string>();
            for (int i = 0; i < data.Count; i++)
            {
                info.line = i;
                if (!keys.Add(data[i].key))
                {
                    info.column = "key";
                    info.errorType = ErrorType.DuplicateKey;
                    return info;
                }

                if (string.IsNullOrEmpty(data[i].korean))
                {
                    info.column = "korean";
                    info.errorType = ErrorType.EmptyString;
                    return info;
                }

                if (string.IsNullOrEmpty(data[i].english))
                {
                    info.column = "english";
                    info.errorType = ErrorType.EmptyString;
                    return info;
                }

                if (string.IsNullOrEmpty(data[i].chinese))
                {
                    info.column = "chinese";
                    info.errorType = ErrorType.EmptyString;
                    return info;
                }

                if (string.IsNullOrEmpty(data[i].japanese))
                {
                    info.column = "japanese";
                    info.errorType = ErrorType.EmptyString;
                    return info;
                }
            }

            return info;
        }

        public List<string> GetSearchedItemKeyList(string textToSearch)
        {
            List<string> result = new List<string>();

            foreach (var pair in localizationTextMap)
            {
                if(pair.Value.Contains(textToSearch))
                {
                    result.Add(textToSearch);
                }
            }

            return result;
        }
    }
}