using UnityEngine;

public class ExpandEffect : MonoBehaviour
{
    [SerializeField] float duration = 1f;
    [SerializeField] Vector3 targetScale = new Vector3(1f, 1f, 1f);

    float startTime = Mathf.NegativeInfinity;
    
    public void StartExpand()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (Time.time > startTime + duration){
            transform.localScale = Vector3.zero;
            return;
        }
        float t = (Time.time - startTime) / duration;
        transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, t);
    }
}
