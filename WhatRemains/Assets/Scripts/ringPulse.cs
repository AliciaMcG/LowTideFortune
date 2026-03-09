using UnityEngine;

public class RingPulse : MonoBehaviour
{
    public Renderer ringRenderer;
    public float speed = 2f;
    public float min = 0.15f;
    public float max = 0.4f;

    void Update()
    {
        float alpha = Mathf.Lerp(min, max, (Mathf.Sin(Time.time * speed) + 1) / 2);
        Color colour = ringRenderer.material.color;
        colour.a = alpha;
        ringRenderer.material.color = colour;
    }
}
