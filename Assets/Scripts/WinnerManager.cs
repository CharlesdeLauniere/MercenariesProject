using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MercenariesProject
{
    public class WinnerManager : MonoBehaviour
    {
        [SerializeField] Canvas _canvas_blueTeamWon;
        [SerializeField] Canvas _canvas_redTeamWon;
        
      
        public void BlueWon()
        {
            Destroy(GameObject.FindGameObjectWithTag("GameCanvas"));
            Instantiate(_canvas_blueTeamWon);

        }
        public void RedWon()
        {
            Destroy(GameObject.FindGameObjectWithTag("GameCanvas"));
            Instantiate(_canvas_redTeamWon);

        }
        

    }
}
