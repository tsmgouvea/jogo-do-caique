using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    // Refer�ncias ao painel do invent�rio e ao painel interno dos slots
    public GameObject inventoryPanel; // Painel principal do invent�rio
    public GameObject slotsPanel;     // Painel interno onde os slots ser�o colocados
    public GameObject slotPrefab;     // Prefab do slot vazio
    public Image itemPreview;          // Imagem de pr�-visualiza��o do item
    public TextMeshProUGUI itemDescription; // Texto para descri��o do item
    public Image avatarImage;         // Avatar do jogador
    public int numberOfSlots = 6;    // N�mero total de slots no invent�rio

    private List<GameObject> slots = new List<GameObject>(); // Lista de slots
    private List<string> collectedItems = new List<string>(); // Lista de itens coletados

    void Start()
    {
        // Inicialmente o invent�rio est� oculto
        inventoryPanel.SetActive(false);

        // Criar os slots vazios no painel interno
        CreateEmptySlots();
    }

    // Fun��o para criar os slots vazios no painel interno
    private void CreateEmptySlots()
    {
        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject newSlot = Instantiate(slotPrefab, slotsPanel.transform); // Slots no painel interno
            slots.Add(newSlot);
        }
    }

    // Fun��o para alternar o estado de visibilidade do invent�rio
    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    // Fun��o para adicionar um item ao primeiro slot vazio
    public void AddItem(string itemName, Sprite newItemIcon, string itemDescriptionText)
    {
        if (!collectedItems.Contains(itemName))
        {
            collectedItems.Add(itemName);

            foreach (GameObject slot in slots)
            {
                // Verifica se o slot est� vazio (sem sprite)
                Image itemImage = slot.transform.Find("ItemIcon").GetComponent<Image>();
                if (itemImage.sprite == null)
                {
                    // Preenche o slot com o �cone do item
                    itemImage.sprite = newItemIcon;
                    itemImage.enabled = true;

                    // Atualiza o texto do slot, se necess�rio
                    TextMeshProUGUI slotText = slot.GetComponentInChildren<TextMeshProUGUI>();
                    if (slotText != null)
                    {
                        slotText.text = itemName;
                    }

                    // Adiciona a��o ao bot�o do slot
                    Button slotButton = slot.GetComponent<Button>();
                    if (slotButton != null)
                    {
                        slotButton.onClick.RemoveAllListeners();
                        slotButton.onClick.AddListener(() =>
                        {
                            ShowItemDetails(new Item { name = itemName, description = itemDescriptionText, icon = newItemIcon });
                        });

                    }

                    return; // Sai ap�s preencher o primeiro slot vazio
                }
            }

            Debug.LogWarning("Invent�rio cheio!");
        }
    }

    // Fun��o para exibir os detalhes de um item
    public void ShowItemDetails(Item item)
    {
        if (item != null)
        {
            itemPreview.sprite = item.icon;
            itemPreview.enabled = true; // Ativa a imagem do �cone
            itemDescription.text = item.description;
        }
        else
        {
            itemPreview.sprite = null;
            itemPreview.enabled = false; // Oculta a imagem do �cone
            itemDescription.text = "Slot vazio";
        }
    }

    // Fun��o chamada ao usar o item (a l�gica pode ser expandida conforme necess�rio)
    void UseItem(string itemName)
    {
        Debug.Log("Usando o item: " + itemName);
        // Aqui voc� pode adicionar a l�gica de efeito do item, como alterar o avatar, etc.
    }
}


[System.Serializable]
public class Item
{
    public string name;
    public string description;
    public Sprite icon;
}