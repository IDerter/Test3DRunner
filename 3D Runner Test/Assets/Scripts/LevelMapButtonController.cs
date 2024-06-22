
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace QuizCinema
{
    public class LevelMapButtonController : MonoBehaviour
    {
        private const string _sceneLevelMap = "LevelMap";
        private const string _sceneMainMenu = "MainMenu";

        public void ReturnToMainMenu()
        {
            SceneManager.LoadScene(_sceneMainMenu);
        }

        public void LoadLevelMap()
        {
            SceneManager.LoadScene(_sceneLevelMap);
        }

        public void LoadSceneByName(string scene)
        {
            SceneManager.LoadScene(scene);
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


        public void Quit()
        {
            Application.Quit();
        }
    }
}