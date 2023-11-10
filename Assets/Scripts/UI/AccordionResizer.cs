using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AccordionResizer : MonoBehaviour
{
    private void Start()
    {

    }

    public void ExpandImages(int id)
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            var element = transform.GetChild(i).GetComponent<LayoutElement>();
            if (i == id)
            {
                DOTween.To(() => element.preferredWidth, x => element.preferredWidth = x, 600, 0.5f);
            }
            else
            {
                DOTween.To(() => element.preferredWidth, x => element.preferredWidth = x, element.minWidth, 0.5f);
            }
        }
    }
}
