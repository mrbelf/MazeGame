using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractLever : MonoBehaviour
{
    private void Start()
    {
        int x = Random.Range(0, 19);
        int y = Random.Range(0, 19);

        Vector3 pos = new Vector3(x/2f + .5f,y/2f + .5f);

        transform.position = pos;
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
