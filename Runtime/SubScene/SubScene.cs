using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JLib
{
    public class SubScene : MonoBehaviour
    {
        [SerializeField] Object scene;
        [SerializeField] bool shouldActiveScene;
        [SerializeField] string scenePath;
        [SerializeField] GUID sceneGuid;
        [SerializeField] string sceneName;

        public Object Scene { get => scene; set => scene = value; }
        public bool ShouldActiveScene { get => shouldActiveScene; set => shouldActiveScene = value; }
        public string ScenePath { get => scenePath; set => scenePath = value; }
        public GUID SceneGuid { get => sceneGuid; set => sceneGuid = value; }
        public string SceneName { get => sceneName; set => sceneName = value; }

        public void Awake()
        {
            if (string.IsNullOrEmpty(sceneName))
                return;

            SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive).completed += 
                (operation)=>
                {
                    operation.allowSceneActivation = true;
                    var loadedScene = SceneManager.GetSceneByName(scene.name);
                    if (loadedScene.IsValid())
                    {
                        Debug.LogError($"{scene.name} is invalid");
                        return;
                    }
                    if (loadedScene.isLoaded && shouldActiveScene)
                    {
                        SceneManager.SetActiveScene(loadedScene);
                    }
                };                        
        }
    }
}
