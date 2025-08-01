using System.Collections.Generic;
using UnityEngine;

public class CardSelectionManager : MonoBehaviour
{
    public static CardSelectionManager Instance;

    [Header("UI References")]
    public GameObject cardSelectionPanel;
    public CardDisplay[] cardDisplays;

    [Header("Settings")]
    [SerializeField] private int cardsToShow = 3;

    private List<CardBase> availableCards = new List<CardBase>();
    private List<CardBase> selectedCards = new List<CardBase>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        LoadAllCards();
    }

    private void LoadAllCards()
    {
        availableCards.Clear();

        CardBase[] allCards = Resources.LoadAll<CardBase>("CardsObjects");

        if (allCards.Length == 0)
        {
            Debug.LogError("No card found in Resources/Cards!");
            return;
        }

        availableCards.AddRange(allCards);
        Debug.Log($"Cards uploaded: {availableCards.Count}");
    }

    public void ShowCardSelection()
    {
        if (availableCards.Count < cardsToShow)
        {
            Debug.LogError($"Not enough cards! Need {cardsToShow}, available {availableCards.Count}");
            return;
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
        cardSelectionPanel.SetActive(true);

        for (int i = 0; i < cardsToShow; i++)
        {
            int randomIndex = Random.Range(0, availableCards.Count);
            CardBase card = availableCards[randomIndex];
            cardDisplays[i].SetupCard(card);
            availableCards.RemoveAt(randomIndex);

            Debug.Log($"The map is shown {i + 1}: {card.cardName}");
        }
    }

    public void OnCardSelected(CardBase selectedCard)
    {
        if (selectedCard == null)
        {
            Debug.LogError("Attempt to select a null card!");
            return;
        }

        selectedCard.ApplyEffect(PlayerStats.Instance);
        selectedCards.Add(selectedCard);

        cardSelectionPanel.SetActive(false);
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log($"The card is selected: {selectedCard.cardName}");
    }
}