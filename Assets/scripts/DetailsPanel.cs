using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailsPanel : MonoBehaviour
{
    [SerializeField]
    Text domain;
    [SerializeField]
    Text cluster;
    [SerializeField]
    Text sdescription;

    void Start()
    {
        
    }

    public void openPanel(BlockData b) {
        domain.text = b.domain;
        cluster.text = b.cluster;
        sdescription.text = b.standarddescription;
        LeanTween.moveLocalY(gameObject, 0, 0.2f).setEase(LeanTweenType.easeOutQuart);
    }

    public void closePanel() {
        LeanTween.moveLocalY(gameObject, -360, 0.2f).setEase(LeanTweenType.easeOutQuart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
