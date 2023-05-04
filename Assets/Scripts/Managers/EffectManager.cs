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
        [SerializeField] GameObject _BuffCircleR3;
        [SerializeField] GameObject _ManaBuff;
        [SerializeField] GameObject _Rage;
        [SerializeField] GameObject _MagicArrow;
        [SerializeField] GameObject _Music;

        [SerializeField] GameObject _target;
        [SerializeField] GameObject _player;

        public AudioSource _source;
        public List<AudioClip> _clips;
        public bool _soundOn{ get; set; }

        public void findAbility(string animName, Vector3 playerPos, List<Hero> target)
        {
            _soundOn = true;
            List<Vector3> targetPos =new List<Vector3>();

            for(int i=0; i<target.Count; i++)
            {
                targetPos.Add(target[i].transform.position);
            }
 
            switch (animName)
            {
                //Knight-----------------------------
                case "Excalibure":
                    SwordSlash(playerPos, targetPos);
                    if(_soundOn==true) { _source.PlayOneShot(_clips[0]); }
                    break;
                case "Aiguille":
                    SwordSlash(playerPos, targetPos);
                    if (_soundOn == true) { _source.PlayOneShot(_clips[0]); }
                    break;
                case "Courage":
                    StartCoroutine(CoeurDeLion(playerPos, targetPos));
                    if (_soundOn == true) { /*mettre un son de mana*/ }
                    break;
                //Gragas-----------------------------
                case "Glaive":
                    SwordSlash(playerPos, targetPos);
                    if (_soundOn == true) { _source.PlayOneShot(_clips[2]); }
                    break;
                case "Rage":
                    Rage(playerPos);
                    if (_soundOn == true) { _source.PlayOneShot(_clips[3]); }
                    break;
                case "Bonk":
                    SwordSlash(playerPos, targetPos);
                    if (_soundOn == true) { _source.PlayOneShot(_clips[4]); }
                    break;
                //Vampire-----------------------------
                case "Griffe":
                    BloodSlash(playerPos, targetPos);
                    if (_soundOn == true) { _source.PlayOneShot(_clips[5]); }
                    break;
                case "Morsure":
                    BloodSlash(playerPos, targetPos);
                    if (_soundOn == true) { _source.PlayOneShot(_clips[5]); }
                    break;
                case "ChauveSouris":

                    break;
                //Bard-------------------------------
                case "Fausser":
                    
                    if (_soundOn == true) { _source.PlayOneShot(_clips[7]); }
                    break;
                case "Hymme":
                    StartCoroutine(VoixAnge(playerPos,targetPos));
                    if (_soundOn == true) { _source.PlayOneShot(_clips[8]); }
                    break;
                case "Ralliement":
                    StartCoroutine(Troupe(playerPos,targetPos));
                    break; 
                //Archer-----------------------------
                case "Baliste":
                    SwordSlash(playerPos, targetPos);
                    if (_soundOn == true) { _source.PlayOneShot(_clips[11]); }
                    break;
                case "Slave":
                    SwordSlash(playerPos, targetPos);
                    if (_soundOn == true) { _source.PlayOneShot(_clips[12]); }
                    break;
                case "Artrium":
                    StartCoroutine(MagicArrow(playerPos, targetPos));
                    if (_soundOn == true) { _source.PlayOneShot(_clips[13]); }
                    break;
                //Wizzard----------------------------
                case "Eclaire":
                    StartCoroutine(Eclaire(playerPos, targetPos));
                    break;
                case "Soin":
                    break;
                case "invocateur":
                    break;
               
                default: 
                    break;
            }
        }
        IEnumerator GenerateEffect1(Vector3 playerPos, List<Vector3> targetPos)
        {
           
            Instantiate(_Effect1, new Vector3(0f, 0f, 0f), Quaternion.identity);
            _Effect1.transform.Find("TargetEffect").transform.position = new Vector3(targetPos[0].x, 2f, targetPos[0].z);
            _Effect1.transform.Find("PlayerEffect").transform.position = new Vector3(playerPos.x, 1f, playerPos.z);
            Destroy(GameObject.FindGameObjectWithTag("Particule1"));
              
            Instantiate(_Effect1, new Vector3(0f, 0f, 0f), Quaternion.identity);
            _Effect1.transform.Find("TargetEffect").transform.position =new Vector3(targetPos[0].x,2f,targetPos[0].z);
            _Effect1.transform.Find("PlayerEffect").transform.position = new Vector3(playerPos.x,1f, playerPos.z);

            yield return new WaitForSeconds(7f);
            Destroy(GameObject.FindGameObjectWithTag("Particule1"));
        }
        IEnumerator MagicArrow(Vector3 playerPos, List<Vector3> targetPos)
        {
            yield return new WaitForSeconds(2f);
            Instantiate(_MagicArrow, new Vector3(0f, 0f, 0f), Quaternion.identity);
            _MagicArrow.transform.Find("TargetEffect").transform.position = new Vector3(targetPos[0].x, 1f, targetPos[0].z);
            _MagicArrow.transform.Find("PlayerEffect").transform.position = new Vector3(playerPos.x, 1f, playerPos.z);
            Destroy(GameObject.FindGameObjectWithTag("Particule1"));

            Instantiate(_MagicArrow, new Vector3(0f, 0f, 0f), Quaternion.identity);
            _MagicArrow.transform.Find("TargetEffect").transform.position = new Vector3(targetPos[0].x, 1f, targetPos[0].z);
            _MagicArrow.transform.Find("PlayerEffect").transform.position = new Vector3(playerPos.x, 1f, playerPos.z);

            yield return new WaitForSeconds(7f);
            Destroy(GameObject.FindGameObjectWithTag("Particule1"));
        }
        public void HealEffect(Vector3 playerPos, List<Vector3> targetPos)
        {
            Instantiate(_HealCircle, playerPos, Quaternion.identity);
         
            for(int i=0; i<targetPos.Count; i++) 
            {
                Instantiate(_HealEffect, targetPos[i], Quaternion.identity);
            }
           
        }
        IEnumerator CoeurDeLion(Vector3 playerPos, List<Vector3> targetPos)
        {
            Instantiate(_BuffCircleR3, playerPos, Quaternion.identity);
            yield return new WaitForSeconds(4f);

            for (int i = 0; i < targetPos.Count; i++)
            {
                if (targetPos[i] != playerPos)
                {
                    Instantiate(_ManaBuff, targetPos[i], Quaternion.identity);
                }
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
        

            for(int i=0; i < targetPos.Count;i++) 
            {
                Instantiate(_BuffEffect, targetPos[i], Quaternion.identity);
            }
            

        }
        public void Rage(Vector3 playerPos)
        {
            Instantiate(_Rage, playerPos, Quaternion.identity);
        }

        public void Cast_SpellEffect(Vector3 playerPos)
        {
            Instantiate(_CastEffect, new Vector3(playerPos.x, 0.2f, playerPos.z), Quaternion.identity);
        }
     
        IEnumerator Eclaire(Vector3 playerPos, List<Vector3> targetPos)
        {

            Cast_SpellEffect(playerPos);
            yield return new WaitForSeconds(2f);
         
            StartCoroutine(GenerateEffect1(playerPos, targetPos));
        }
        IEnumerator VoixAnge(Vector3 playerPos, List<Vector3> targetPos)
        {

            Instantiate(_Music,new Vector3(playerPos.x,2f,playerPos.z), Quaternion.identity);
            Instantiate(_HealCircle, playerPos, Quaternion.identity);
            yield return new WaitForSeconds(3f);

            for(int i=0; i<targetPos.Count;i++)
            {
                Instantiate(_HealEffect, targetPos[i], Quaternion.identity);
            }
        }
        IEnumerator Troupe(Vector3 playerPos, List<Vector3> targetPos)
        {
            Instantiate(_Music, new Vector3(playerPos.x, 2f, playerPos.z), Quaternion.identity);
            if (_soundOn == true) { _source.PlayOneShot(_clips[9]); }
            yield return new WaitForSeconds(6f);
            for(int i = 0; i < targetPos.Count; i++)
            {
                if(targetPos[i]!=playerPos)
                {
                    Instantiate(_Rage, targetPos[i], Quaternion.identity);
                }
               
            }
            for (int i = 0; i < targetPos.Count-1; i++)
            {
                if (_soundOn == true) { _source.PlayOneShot(_clips[3]); }
                yield return new WaitForSeconds(1f);
            }

        }

        public void Start()
        {
            //Only for test
            //List<Vector3> list = new List<Vector3>();
            //list.Add(_target.transform.position);



            //StartCoroutine(MagicArrow(_player.transform.position, list));
        }


    }
}

