using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MercenariesProject
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] AudioSource _source;
        public List<AudioClip> _clips;
        private void Start()
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (sceneIndex == 0)
            {
                //Start scene
                _source.PlayOneShot(_clips[0]);
            }
            else if (sceneIndex == 1)
            {
                //Hero selection scene
                _source.PlayOneShot(_clips[1]);
            }
            else if (sceneIndex == 2)
            {
                //Main scene
                _source.PlayOneShot(_clips[2]);
            }

        }
    }
}
