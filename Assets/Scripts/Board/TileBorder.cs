using UnityEngine;

public class TileBorder : MonoBehaviour
{
    [Header("Edges")]
    [SerializeField] private GameObject top;
    [SerializeField] private GameObject bottom;
    [SerializeField] private GameObject left;
    [SerializeField] private GameObject right;

    [Header("Corners")]
    [SerializeField] private GameObject topLeft;
    [SerializeField] private GameObject topRight;
    [SerializeField] private GameObject bottomLeft;
    [SerializeField] private GameObject bottomRight;

    public void HideAll()
    {
        top.SetActive(false);
        bottom.SetActive(false);
        left.SetActive(false);
        right.SetActive(false);

        topLeft.SetActive(false);
        topRight.SetActive(false);
        bottomLeft.SetActive(false);
        bottomRight.SetActive(false);
    }

    public void SetTop(bool value) => top.SetActive(value);
    public void SetBottom(bool value) => bottom.SetActive(value);
    public void SetLeft(bool value) => left.SetActive(value);
    public void SetRight(bool value) => right.SetActive(value);

    public void SetTopLeft(bool value) => topLeft.SetActive(value);
    public void SetTopRight(bool value) => topRight.SetActive(value);
    public void SetBottomLeft(bool value) => bottomLeft.SetActive(value);
    public void SetBottomRight(bool value) => bottomRight.SetActive(value);
}