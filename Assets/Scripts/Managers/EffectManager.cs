using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using static UnityEngine.GraphicsBuffer;

namespace MercenariesProject
{
   
    public class EffectManager : MonoBehaviour
    {
        [SerializeField] GameObject _Effect1;
        [SerializeField] GameObject _HealEffect;
        [SerializeField] GameObject _HealCircle;
        [SerializeField] GameObject _CastEffect;
        [SerializeField] GameObject _BuffEffect;
        [SerializeField] GameObject _BuffCircle;
        [SerializeField] GameObject _SwordSlash;
        [SerializeField] GameObject _SwordHit;


        [SerializeField] GameObject _target;
        [SerializeField] GameObject _player;

        public AudioSource _source;
        public List<AudioClip> _clips;
        public bool _soundOn=true;
        public void findAbility(string animName, Vector3 playerPos, Vector3 targetPos)
        {
            //Vector3 playerPos = new Vector3(0,0,0);
            //Vector3 targetPos = new Vector3(0,0,0);
 
            switch (animName)
            {
                //Knight-----------------------------
                case "EpeeTranchante":
                    SwordSlash(playerPos, targetPos);
                    if(_soundOn==true) { _source.PlayOneShot(_clips[0]); }
                    break;
                case "CoupEscrime":
                    SwordSlash(playerPos, targetPos);
                    if (_soundOn == true) { _source.PlayOneShot(_clips[1]); }
                    break;
                case "CoeurDeLion":
                    SwordSlash(playerPos, targetPos);
                    if (_soundOn == true) { _source.PlayOneShot(_clips[2]); }
                    break;
                //Gragas-----------------------------
                case "GrosCoupEpee":
                    SwordSlash(playerPos, targetPos);
                    if (_soundOn == true) { _source.PlayOneShot(_clips[3]); }
                    break;
                case "Rage":
                    SwordSlash(playerPos, targetPos);
                    if (_soundOn == true) { _source.PlayOneShot(_clips[4]); }
                    break;
                case "CoupEtourdissant":
                    SwordSlash(playerPos, targetPos);
                    if (_soundOn == true) { _source.PlayOneShot(_clips[5]); }
                    break;
                //Vampire-----------------------------
                case "Griffe":
                    SwordSlash(playerPos, targetPos);
                    if (_soundOn == true) { _source.PlayOneShot(_clips[6]); }
                    break;
                case "Morsure":
                    SwordSlash(playerPos, targetPos);
                    if (_soundOn == true) { _source.PlayOneShot(_clips[7]); }
                    break;
                //Bard-------------------------------
                case "CoupDeGuitare":
                    SwordSlash(playerPos, targetPos);
                    if (_soundOn == true) { _source.PlayOneShot(_clips[8]); }
                    break;
                case "VoixDAnge":
                    SwordSlash(playerPos, targetPos);
                    if (_soundOn == true) { _source.PlayOneShot(_clips[9]); }
                    break;
                case "RallyEDesTroupes":
                    SwordSlash(playerPos, targetPos);
                    if (_soundOn == true) { _source.PlayOneShot(_clips[10]); }
                    break; 
                //Archer-----------------------------
                case "GrosseFleche":
                    SwordSlash(playerPos, targetPos);
                    if (_soundOn == true) { _source.PlayOneShot(_clips[11]); }
                    break;
                case "TripleFleche":
                    SwordSlash(playerPos, targetPos);
                    if (_soundOn == true) { _source.PlayOneShot(_clips[12]); }
                    break;
                case "FlecheMagique":
                    SwordSlash(playerPos, targetPos);
                    if (_soundOn == true) { _source.PlayOneShot(_clips[13]); }
                    break;
                //Wizzard----------------------------


                //Basic------------------------------
                case "Heal":
                    HealEffect(playerPos, targetPos);
                    if (_soundOn == true) { }
                    break;
                case "DefaultMagicAttack":
                    Cast_basicMagicAttack(playerPos, targetPos);
                    if (_soundOn == true) { }
                    break;
                case "Buff":
                    BuffEffect(playerPos, targetPos);
                    if (_soundOn == true) { }
                    break;
                default: 
                    break;
            }
        }
        public void GenerateEffect1(Vector3 playerPos, Vector3 targetPos)
        {
            StartCoroutine(wait(2));
            Instantiate(_Effect1, new Vector3(0f, 0f, 0f), Quaternion.identity);
            _Effect1.transform.Find("TargetEffect").transform.position = targetPos;
            _Effect1.transform.Find("PlayerEffect").transform.position = playerPos;
            Destroy(GameObject.FindGameObjectWithTag("Particule1"));

            Instantiate(_Effect1, new Vector3(0f, 0f, 0f), Quaternion.identity);
            _Effect1.transform.Find("TargetEffect").transform.position = targetPos;
            _Effect1.transform.Find("PlayerEffect").transform.position = playerPos;


        }
        public void HealEffect(Vector3 playerPos, Vector3 targetPos)
        {
            Instantiate(_HealCircle, playerPos, Quaternion.identity);
            StartCoroutine(wait(4));
            Instantiate(_HealEffect, targetPos, Quaternion.identity);
        }
        public void SwordSlash(Vector3 playerPos, Vector3 targetPos)
        {
           
            double insideTan = (targetPos.z - playerPos.z) / (targetPos.x - playerPos.x);
            float vectorAngle = -1*(float)(Mathf.Atan((float)insideTan) * (180 / Math.PI));
            
            Instantiate(_SwordHit, targetPos, Quaternion.identity);

            Instantiate(_SwordSlash, playerPos, Quaternion.identity);

            GameObject.FindGameObjectWithTag("StoneSlash").transform.Rotate(0f,vectorAngle, 0f, Space.World);

            Debug.Log(vectorAngle);


        }
        public void BuffEffect(Vector3 playerPos, Vector3 targetPos)
        {
            Instantiate(_BuffCircle, playerPos, Quaternion.identity);
            StartCoroutine(wait(2));
            Instantiate(_BuffEffect, targetPos, Quaternion.identity);

        }
        public void Cast_SpellEffect(Vector3 playerPos)
        {
            Instantiate(_CastEffect, new Vector3(playerPos.x, 0.2f, playerPos.z), Quaternion.identity);
        }
     
        public void Cast_basicMagicAttack(Vector3 playerPos, Vector3 targetPos)
        {

            Cast_SpellEffect(playerPos);
            GenerateEffect1(playerPos, targetPos);
        }
        public IEnumerator wait(float seconds)
        {
            yield return new WaitForSeconds(seconds);
        }
        public void Start()
        {
           //Only for test
           //findAbility("EpeeTranchante", _player.transform.position, _target.transform.position);
        }


    }
}

