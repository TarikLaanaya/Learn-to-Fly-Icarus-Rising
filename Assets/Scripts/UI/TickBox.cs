using UnityEngine.EventSystems;
using UnityEngine;

public class TickBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private UnityEngine.UI.Image tickFilledImage;
    private TickBoxManager tickBoxManager;
    public int cost;
    [HideInInspector] public bool ticked = false;

    void Awake()
    {
        tickFilledImage = transform.GetChild(0).GetComponent<UnityEngine.UI.Image>();
    }

    void Start()
    {
        tickBoxManager = GetComponentInParent<TickBoxManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tickBoxManager.TickBoxHoveredOver(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tickBoxManager.Unhighlight();
    }

    public void OnClicked()
    {
        tickBoxManager.BoxClicked();
    }

    public void HalfHighlight()
    {
        tickFilledImage.color = new Color(0f, 0f, 0f, .5f);
    }
    public void Highlight()
    {
        tickFilledImage.color = new Color(0f, 0f, 0f, 1f);
    }

    public void HideHighlight()
    {
        tickFilledImage.color = new Color(0f, 0f, 0f, 0f);
    }
}
