using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskIcon : MonoBehaviour
{
    public ItemType ItemType;
    public int CurrentScore;

    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private ItemIcons _itemIcons;

    [SerializeField] private AnimationCurve _scaleCurve;

    public void Setup(ItemType itemType, int number)
    {
        ItemType = itemType;
        CurrentScore = number;
        _image.sprite = _itemIcons.GetSprite(itemType);
        _text.text = CurrentScore.ToString();
    }

    //[ContextMenu("AddOne")]
    public void AddOne()
    {
        CurrentScore -= 1;
        if (CurrentScore < 0)
        {
            CurrentScore = 0;
        }
        _text.text = CurrentScore.ToString();
        StartCoroutine(AddAnimation());
    }

    private IEnumerator AddAnimation()
    {
        for (float t = 0; t < 1f; t += Time.deltaTime / 0.6f)
        {
            float scale = _scaleCurve.Evaluate(t);
            transform.localScale = Vector3.one * scale;
            yield return null;
        }
        transform.localScale = Vector3.one;
    }
}
