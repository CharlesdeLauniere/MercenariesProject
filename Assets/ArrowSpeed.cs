using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArrowSpeed : MonoBehaviour
{

    [SerializeField] private float _speed = 20f;
    [SerializeField] Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = GameObject.FindWithTag("Enemy").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition - new Vector3(0, 0, 2.98f), _speed);
    }
}
