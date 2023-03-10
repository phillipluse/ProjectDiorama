using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectDiorama
{
    public enum SceneIndex
    {
        Manager = 0,
        Title = 1,
        Core = 2,
        GrayBoxLevel = 3,
        UIBuildButtons = 4,
        UIGrayBox = 5,
    }
    
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] GameObject _loadingScreen;

        List<AsyncOperation> _scenesLoading;
        
            void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError($"There's more than one NAME {transform} - {Instance}");
                Destroy(gameObject);
                return;
            }

            Instance = this;

            _scenesLoading = new List<AsyncOperation>();
            LoadSceneAsync(SceneIndex.Title);
        }

        public void LoadGame()
        {
            _loadingScreen.SetActive(true);

            AddToList(UnLoadSceneAsync(SceneIndex.Title));
            AddToList(LoadSceneAsync(SceneIndex.Core));
            AddToList(LoadSceneAsync(SceneIndex.GrayBoxLevel));
            AddToList(LoadSceneAsync(SceneIndex.UIBuildButtons));
            AddToList(LoadSceneAsync(SceneIndex.UIGrayBox));

            StartCoroutine(GetSceneLoadProgress());
        }

        AsyncOperation LoadSceneAsync(SceneIndex sceneIndex)
        {
            return SceneManager.LoadSceneAsync((int) sceneIndex, LoadSceneMode.Additive);
        }

        AsyncOperation UnLoadSceneAsync(SceneIndex sceneIndex)
        {
            return SceneManager.UnloadSceneAsync((int) sceneIndex);
        }

        IEnumerator GetSceneLoadProgress()
        {
            foreach (AsyncOperation operation in _scenesLoading)
            {
                while (!operation.isDone)
                {
                    yield return null;
                }
            }

            yield return null;
            _loadingScreen.SetActive(false);
        }

        void AddToList(AsyncOperation operation)
        {
            if (!_scenesLoading.Contains(operation))
            {
                _scenesLoading.Add(operation);
            }
        }
    }
}
