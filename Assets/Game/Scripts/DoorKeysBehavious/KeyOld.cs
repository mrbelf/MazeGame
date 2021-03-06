using UnityEngine;

public class KeyOld : MonoBehaviour
{
    [SerializeField] public int code;

    public KeyOwner owner;

    public int GetCode() => code;

    public void Init(int id, Color c)
    {
        this.code = id;
        GetComponent<SpriteRenderer>().color = c;
    }
}