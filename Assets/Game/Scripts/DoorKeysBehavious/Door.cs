using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [SerializeField] int code;
    public GameObject[] lights;
    MazeGenerator maze;

    public int Id => code;

    public void Winner() 
    {
        gameObject.GetComponent<WinnerOnCollision>().CallWinner(code);
        AudioManager.GetInstance().PlayAudio("win");
    }

    public void TriggerEnter2D(Collider2D collision)
    {
        var keyOwner = collision.gameObject.GetComponent<KeyOwner>();
        if (keyOwner && keyOwner.hasKey && CodesMatch(keyOwner, code))
        {
            Winner();
        }
    }

    public void CollisionEnter2D(Collision2D collision)
    {
        var keyOwner = collision.gameObject.GetComponent<KeyOwner>();
        if (keyOwner && keyOwner.hasKey && CodesMatch(keyOwner, code)) 
        {
            Winner();
        }
    }

    private bool CodesMatch(KeyOwner owner, int code) 
    {
        return owner.GetKeyCode() == code && owner.ownerCode == code;
    }

    public void Init(int id, Color c)
    {
        this.code = id;
        for(int i = 0; i < lights.Length; i++)
        {
            lights[i].GetComponent<Light>().color = c;
        }
        
    }
    public void Start()
    {
        var transform = gameObject.GetComponent<Transform>();
        var x = transform.position.x;
        var y = transform.position.y;
        maze = GameObject.Find("MazeGenerator").GetComponent<MazeGenerator>();
        int rows = maze.rows;
        float CellSize = MazeGenerator.CellSize;

        if (y == ( CellSize / 2 ))
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else if (x == ( CellSize / 2))
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if (x == ((rows * CellSize) - (CellSize / 2)))
        {
            transform.eulerAngles = new Vector3(0, 0, 270);
        }
    }
}
