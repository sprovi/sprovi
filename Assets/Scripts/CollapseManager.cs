using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapseManager : MonoBehaviour
{
    private void Awake()
    {
        ActiveItem[] activeItems = FindObjectsOfType<ActiveItem>();
        foreach (ActiveItem item in activeItems)
        {
            item.Init(this);
        }
    }
    public void Collapse(ActiveItem activateItemA, ActiveItem activateItemB)
    {
        ActiveItem from = null;
        ActiveItem to = null;
        if (activateItemA.ItemType != ItemType.Ball && activateItemB.ItemType == ItemType.Ball)
        {
            from = activateItemB;
            to = activateItemA;
        }
        else if (activateItemA.ItemType == ItemType.Ball && activateItemB.ItemType != ItemType.Ball)
        {
            from = activateItemA;
            to = activateItemB;
        }
        else 
        {
            if (activateItemA.transform.position.y > activateItemB.transform.position.y)
            {
                from = activateItemA;
                to = activateItemB;
            }
            else
            {
                to = activateItemA;
                from = activateItemB;
            }
        }
        StartCoroutine(CollapseProcess(from, to));

    }

    private IEnumerator CollapseProcess(ActiveItem activaItemA, ActiveItem activeItemB)
    {
        Vector3 startPosition = activaItemA.transform.position;
        activaItemA.Deactivate();
        activeItemB.Deactivate();
        for (float t = 0; t < 1f; t += Time.deltaTime / 0.3f)
        {
            activaItemA.transform.position = Vector3.Lerp(startPosition, activeItemB.transform.position, t);
            yield return null;
        }
        Destroy(activaItemA.gameObject);
        activeItemB.Activate();
        activeItemB.DoEffect();
    }
}
