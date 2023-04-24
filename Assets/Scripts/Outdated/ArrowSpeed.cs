using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArrowSpeed : MonoBehaviour
{

    private float _speed = 200f;
    float realSpeed = 0f;
    [SerializeField] Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        /*targetPosition = GameObject.FindWithTag("Enemy").transform.position;
        realSpeed = _speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, realSpeed);
        if(transform.position == targetPosition)
        {
            Destroy(gameObject);
        }*/

    }
}
