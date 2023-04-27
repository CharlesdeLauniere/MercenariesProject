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
        [SerializeField] GameObject _BloodSlash;
        [SerializeField] GameObject _BloodHit;

        [SerializeField] GameObject _target;
        [SerializeField] GameObject _player;

        public AudioSource _source;
        public List<AudioClip> _clips;
        public bool _soundOn=true;

        public void findAbility(string animName, Vector3 playerPos, List<Hero> target)
        {
           
            List<Vector3> targetPos =new List<Vector3>();

            for(int i=0; i<target.Count; i++)
            {
                targetPos.Add(target[i].transform.position);
            }
 
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
                    PlayerBuffEffect(playerPos);
                    if (_soundOn == true) { _source.PlayOneShot(_clips[4]); }
                    break;
                case "CoupEtourdissant":
                    SwordSlash(playerPos, targetPos);
                    if (_soundOn == true) { _source.PlayOneShot(_clips[5]); }
                    break;
                //Vampire-----------------------------
                case "Griffe":
                    BloodSlash(playerPos, targetPos);
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
        public void GenerateEffect1(Vector3 playerPos, List<Vector3> targetPos)
        {
            StartCoroutine(wait(2));
            Instantiate(_Effect1, new Vector3(0f, 0f, 0f), Quaternion.identity);
            _Effect1.transform.Find("TargetEffect").transform.position = targetPos[0];
            _Effect1.transform.Find("PlayerEffect").transform.position = playerPos;
            Destroy(GameObject.FindGameObjectWithTag("Particule1"));

            Instantiate(_Effect1, new Vector3(0f, 0f, 0f), Quaternion.identity);
            _Effect1.transform.Find("TargetEffect").transform.position = targetPos[0];
            _Effect1.transform.Find("PlayerEffect").transform.position = playerPos;


        }
        public void HealEffect(Vector3 playerPos, List<Vector3> targetPos)
        {
            Instantiate(_HealCircle, playerPos, Quaternion.identity);
            StartCoroutine(wait(4));
            for(int i=0; i<targetPos.Count; i++) 
            {
                Instantiate(_HealEffect, targetPos[i], Quaternion.identity);
            }
           
        }
        public void SwordSlash(Vector3 playerPos, List<Vector3> targetPos)
        {
           
            double insideTan = (targetPos[0].z - playerPos.z) / (targetPos[0].x - playerPos.x);
            float vectorAngle = -1*(float)(Mathf.Atan((float)insideTan) * (180 / Math.PI));
            
            for(int i = 0; i < targetPos.Count; i++)
            {
                Instantiate(_SwordHit,new Vector3(targetPos[i].x, 0.84f , targetPos[i].z), Quaternion.identity);
            }
            
            Instantiate(_SwordSlash, new Vector3(playerPos.x, 0.6f ,playerPos.z), Quaternion.identity);

            GameObject.FindGameObjectWithTag("StoneSlash").transform.Rotate(0f,vectorAngle, 0f, Space.World);

            Debug.Log(vectorAngle);


        }
        public void BloodSlash(Vector3 playerPos, List<Vector3> targetPos)
        {

            double insideTan = (targetPos[0].z - playerPos.z) / (targetPos[0].x - playerPos.x);
            float vectorAngle = -1 * (float)(Mathf.Atan((float)insideTan) * (180 / Math.PI));

            for (int i = 0; i < targetPos.Count; i++)
            {
                Instantiate(_BloodHit, new Vector3(targetPos[i].x, 0.84f, targetPos[i].z), Quaternion.identity);
            }

            Instantiate(_BloodSlash, new Vector3(playerPos.x, 0.6f, playerPos.z), Quaternion.identity);

            GameObject.FindGameObjectWithTag("BloodSlash").transform.Rotate(0f, vectorAngle, 0f, Space.World);

            Debug.Log(vectorAngle);


        }
        public void BuffEffect(Vector3 playerPos, List<Vector3> targetPos)
        {
            Instantiate(_BuffCircle, playerPos, Quaternion.identity);
            StartCoroutine(wait(2));

            for(int i=0; i < targetPos.Count;i++) 
            {
                Instantiate(_BuffEffect, targetPos[i], Quaternion.identity);
            }
            

        }
        public void PlayerBuffEffect(Vector3 playerPos)
        {
            Instantiate(_BuffCircle, playerPos, Quaternion.identity);
        }

        public void Cast_SpellEffect(Vector3 playerPos)
        {
            Instantiate(_CastEffect, new Vector3(playerPos.x, 0.2f, playerPos.z), Quaternion.identity);
        }
     
        public void Cast_basicMagicAttack(Vector3 playerPos, List<Vector3> targetPos)
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

