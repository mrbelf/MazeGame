using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KeyOwner : MonoBehaviour
{
    [SerializeField] private Key ownedKey;
    [SerializeField] private int code;

    float lastSwitch = -1f;
    int lastKeySwitchCode = -1;
    bool canSwap = true;

    //[SerializeField] private float moveKeySpeed;

    public bool hasKey => ownedKey != null;
    public int ownerCode => code;
    public int GetKeyCode() 
    {
        if (hasKey)
            return ownedKey.GetCode();
        Debug.LogWarning("Tried to get key code without a key");
        return -1;
    }

    public void GetKey(Key key) 
    {
        key.gameObject.transform.parent = transform;
        key.transform.position = transform.position;

        ownedKey = key;
    }

    public void SwitchKeys(Key key) 
    {
        ownedKey.transform.parent = null;
        ownedKey.transform.position = key.transform.position;
        lastKeySwitchCode = ownedKey.GetCode();
        lastSwitch = Time.time;

        GetKey(key);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var key = collision.GetComponent<Key>();
        if (key) 
        {
            if (hasKey)
            {
                if (ShouldSwitch(key)) 
                {
                    canSwap = false;
                    SwitchKeys(key);
                    StartCoroutine(WaitSetSwapTrue());
                }

            }
            else 
            {
                GetKey(key);
            }
        }
    }

    IEnumerator WaitSetSwapTrue(float waitTime = 1f) 
    {
        yield return new WaitForSeconds(waitTime);
        canSwap = true;
    }

    private bool ShouldSwitch(Key key) 
    {
        return canSwap;
    }

    private void FixedUpdate()
    {
        //if (ownedKey) 
        //{
        //    Vector3 ownedKeyPosition = ownedKey.transform.position;
        //    ownedKey.transform.position += (transform.position - ownedKeyPosition) * moveKeySpeed;
        //}
    }
}
