using System.Collections;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] private float animationTime = 2;
    [SerializeField] private TextMeshPro text;

    public void Init(Color textColor, string textAmount)
    {
        text.color = textColor;
        text.text = textAmount;
    }

    private void Start()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        var currentTime = 0f;
        while (currentTime < animationTime)
        {
            currentTime += Time.deltaTime;
            var color = text.color;
            var progress = currentTime / animationTime;
            color.a = Mathf.Clamp(1, 0, progress);
            transform.position += Vector3.up * Time.deltaTime;
            yield return  new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}
