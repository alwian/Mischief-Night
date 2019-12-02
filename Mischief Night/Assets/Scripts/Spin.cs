using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] float spinSpeed = 5;

    private void Update()
    {
        this.transform.rotation = Quaternion.Euler(0f, spinSpeed * Time.deltaTime, 0f) * this.transform.rotation;
    }
}
