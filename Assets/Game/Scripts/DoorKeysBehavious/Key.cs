using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private int code;

    public int GetCode() => code;
}
