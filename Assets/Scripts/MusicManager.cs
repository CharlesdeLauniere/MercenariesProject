using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
namespace MercenariesProject
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] AudioSource _source;
        public List<AudioClip> _clips;
        public bool _soundOn;
        public TextMeshProUGUI _text;
        public void BouttonChangerMusique()
        {
           if(_soundOn==true)
           {
                _source.Stop();
                _soundOn = false;
                _text.text = "Musique : Non";
           }
           else if (_soundOn == false)
           {
                PlayMusic();
                _soundOn = true;
                _text.text = "Musique : Oui";
            }
        }

        private void Start()
        {
            _soundOn= true;
            PlayMusic();
        }

        public void PlayMusic() 
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (sceneIndex == 0)
            {
                //Start scene
                _source.PlayOneShot(_clips[0]);
            }
            else if (sceneIndex == 1 | sceneIndex == 3 | sceneIndex == 4 | sceneIndex == 5 | sceneIndex ==6 )
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
