using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Trigger : MonoBehaviour
{
    public List<SpriteRenderer> activationIndicators;
    public Color activationColor;

    public UnityEvent onActivate;
    public UnityEvent onDeactivate;

    protected bool isActivated = false;

    private List<Color> indicatorColors = new List<Color>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        activationIndicators.ForEach(ind => indicatorColors.Add(ind.color));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Activate()
    {
        StartCoroutine(ActivationCo());

        onActivate.Invoke();

        isActivated = true;
    }

    public virtual void Deactivate()
    {
        StartCoroutine(DeactivationCo());

        onDeactivate.Invoke();

        isActivated = false;
    }

    IEnumerator ActivationCo()
    {
        if(activationIndicators == null)
            yield break;

        List<Color> dirs = new List<Color>();
        activationIndicators.ForEach(ind => dirs.Add((activationColor - ind.color) / 10.0f));

        for(int i = 0; i < 10; i++)
        {
            activationIndicators.ForEach(ind => ind.color += dirs[activationIndicators.IndexOf(ind)]);

            yield return new WaitForSeconds(0.1f);
        }

        activationIndicators.ForEach(ind => ind.color = activationColor);
    }

    IEnumerator DeactivationCo()
    {
        if(activationIndicators == null)
            yield break;
        Debug.Log(indicatorColors.Count);
        List<Color> dirs = new List<Color>();
        activationIndicators.ForEach(ind => dirs.Add((indicatorColors[activationIndicators.IndexOf(ind)] - ind.color) / 10.0f));

        for(int i = 0; i < 10; i++)
        {
            activationIndicators.ForEach(ind => ind.color += dirs[activationIndicators.IndexOf(ind)]);

            yield return new WaitForSeconds(0.1f);
        }

        activationIndicators.ForEach(ind => ind.color = indicatorColors[activationIndicators.IndexOf(ind)]);
    }
}
