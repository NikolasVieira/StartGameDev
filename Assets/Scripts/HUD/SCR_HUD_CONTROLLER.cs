using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_HUD_CONTROLLER : MonoBehaviour
{
    //ITENS
    [SerializeField] private Image waterUIBar;
    [SerializeField] private Image woodUIBar;
    [SerializeField] private Image cropUIBar;

    //FERRAMENTAS
    [SerializeField] private List<Image> toolsUI = new List<Image>();
    
    [SerializeField] private Color alphaSelected;
    [SerializeField] private Color alphaNotSelected;

    SCR_PLAYER player;
    SCR_PLAYER_ITEMS playerItems;

    private void Awake()
    {
        playerItems = FindObjectOfType<SCR_PLAYER_ITEMS>();
        player = playerItems.GetComponent<SCR_PLAYER>();
    }

    void Start()
    {
        waterUIBar.fillAmount = 0f;
        woodUIBar.fillAmount = 0f;
        cropUIBar.fillAmount = 0f;
    }

    void Update()
    {
        waterUIBar.fillAmount = playerItems.propCurrentWater / playerItems.propWaterLimit;
        woodUIBar.fillAmount = playerItems.propCurrentWood / playerItems.propWoodLimit;
        cropUIBar.fillAmount = playerItems.propCurrentCrop / playerItems.propCropLimit;

        for (int i = 0; i < toolsUI.Count; i++)
        {
            if (i == player.propHandlingObj)
            {
                toolsUI[i].color = alphaSelected;
            }
            else
            {
                toolsUI[i].color = alphaNotSelected;
            }
        }
    }
}
