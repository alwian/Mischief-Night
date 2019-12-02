using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Lantern : MonoBehaviour
{
    [Header("Required References")]
    [SerializeField] Transform oilScaler;
    [SerializeField] GameObject fire;

    [Header("Held Options")]
    [SerializeField] Vector3 positionOffset;
    [SerializeField] Vector3 rotationOffset;

    bool pickedUp = false;

    public void SetOilLevel(float oilLevel)
    {
        oilLevel = Mathf.Clamp(oilLevel, 0f, 1f);

        var scale = oilScaler.localScale;
        scale.y = oilLevel;
        oilScaler.localScale = scale;

        if (oilLevel <= 0f)
            fire.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!pickedUp && other.CompareTag("Player"))
        {
            this.transform.SetParent(other.transform);
            this.transform.localPosition = positionOffset;
            this.transform.localRotation = Quaternion.Euler(rotationOffset);

            this.GetComponent<BoxCollider>().enabled = false;

            pickedUp = true;
        }
    }
}
