using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] public int code;

    public KeyOwner owner;

    public int GetCode() => code;

    public void Init(int id, Color c)
    {
        this.code = id;
        var renderer = GetComponent<Renderer>();
        if (c == Color.green)
        {
           // renderer.material.SetColor("_Color", new Color32(65, 168, 65, 255));
           // renderer.material.SetColor("_EmissionColor", new Color(72f / 255f, 144f / 255f, 52f / 255f) * 0.7f);
        }
        else if (c == Color.blue)
        {
            //renderer.material.SetColor("_Color", new Color32(45, 45, 178, 255));
           // renderer.material.SetColor("_EmissionColor", new Color(40f / 255f, 40f / 255f, 195f / 255f) * 0.7f);
        }
        //else
        //{
            renderer.material.SetColor("_Color", c);
        //renderer.material.SetColor("_EmissionColor", c * 0.4f);
        //}

    }
}