using UnityEngine;
using System.Collections;
using TMPro;
public class _FpsCounter : MonoBehaviour
{
    [SerializeField] int frameCount = 0;
    [SerializeField] float dt = 0.0f, fps = 0.0f, updateFrequency=0f;
    [SerializeField, Tooltip("Updates/sec")] int updateRate = 4;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] string Format = "00.0";

    private void Start()
    {
        updateFrequency = 1.0f / updateRate;
    }

    void Update()
    {
        frameCount++;
        dt += Time.deltaTime;
        if (dt > updateFrequency)
        {
            fps = frameCount / dt;
            frameCount = 0;
            dt -= updateFrequency;
            _text.SetText(fps.ToString(Format));
        }
    }
}
