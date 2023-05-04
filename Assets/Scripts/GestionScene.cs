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
            SceneManager.LoadScene(1);
        }
        public void AllerScenePrincipale()
        {
            SceneManager.LoadScene(0);
        }
    }
}

