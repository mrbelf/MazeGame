using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractLever : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision){
        var player = collision.gameObject.GetComponent<MovimentComponent>();

        Key[] array = GameObject.FindObjectsOfType<Key>();
        int temp = array[0].code;
        Color cTemp = array[0].GetComponent<SpriteRenderer>().color;
        array[0].GetComponent<SpriteRenderer>().color = array[1].GetComponent<SpriteRenderer>().color;
        array[1].GetComponent<SpriteRenderer>().color = cTemp;

    }
}
