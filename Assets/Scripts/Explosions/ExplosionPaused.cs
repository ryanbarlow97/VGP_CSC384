using UnityEngine;

public class ExplosionPaused : MonoBehaviour
{
    public float duration = 0.8f;
    private float elapsedTime;

    private void Update()
    {
        elapsedTime += Time.unscaledDeltaTime;
        if (elapsedTime >= duration)
        {
            Destroy(gameObject);
        }
    }
}
