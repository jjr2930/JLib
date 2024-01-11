using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JLib
{
    public class SubScene : MonoBehaviour
    {
        [SerializeField] Object scene;
        [SerializeField] bool shouldActiveScene;

        public Object Scene { get => scene; set => scene = value; }
        public bool ShouldActiveScene { get => shouldActiveScene; set => shouldActiveScene = value; }

        public void Awake()
        {
            SceneManager.LoadSceneAsync(scene.name, LoadSceneMode.Additive).completed += 
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
