using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    [Header("UI References")]
    public Image cardImage;
    public Text cardNameText;
    public Text descriptionText;
    public Button selectButton;

    [Header("Debug")]
    [SerializeField] private CardBase currentCard;

    private void Awake()
    {
        selectButton.onClick.AddListener(OnCardClicked);
    }

    public void SetupCard(CardBase card)
    {
        if (card == null)
        {
            return;
        }

        currentCard = card;

        if (cardImage != null)
        {
            cardImage.sprite = card.icon != null ? card.icon : null;
            cardImage.enabled = card.icon != null;
        }

        if (cardNameText != null)
        {
            cardNameText.text = string.IsNullOrEmpty(card.cardName) ? "No Name" : card.cardName;
        }

        if (descriptionText != null)
        {
            descriptionText.text = string.IsNullOrEmpty(card.description) ? "No Description" : card.description;
        }

        Debug.Log($"Card: {card.cardName}", this);
    }

    private void OnCardClicked()
    {
        if (currentCard == null)
        {
            Debug.LogError("Null card!", this);
            return;
        }

        CardSelectionManager.Instance?.OnCardSelected(currentCard);
    }
}