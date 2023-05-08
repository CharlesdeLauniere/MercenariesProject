using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace MercenariesProject
{
    public class GestionScene : MonoBehaviour
    {
        public void QuitterJeu()
        {
            Application.Quit();
        }
        public void AllerSceneDepart()
        {
            SceneManager.LoadScene(0);
        }
        public void AllerScenePrincipale()
        {
            SceneManager.LoadScene(1);
        }
        public void nextScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}

