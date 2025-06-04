using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class StatPanel : MonoBehaviour
{
    //  표시 정보들
    public Panel panel;
    public Sprite allyBackground;
    public Sprite enemyBackground;
    public Image background;
    public Image avatar;
    public TextMeshProUGUI nameLabel;
    public TextMeshProUGUI hpLabel;
    public TextMeshProUGUI mpLabel;
    public TextMeshProUGUI lvLabel;

    //  Dispaly를 통하여 전달
    public void Display(GameObject obj)
    {
        background.sprite = Random.value > 0.5f ? enemyBackground : allyBackground;
        nameLabel.text = obj.name;
        Stats stats = obj.GetComponent<Stats>();
        if (stats)
        {
            hpLabel.text = string.Format("HP {0} / {1}", stats[StatTypes.HP], stats[StatTypes.MHP]);
            mpLabel.text = string.Format("MP {0} / {1}", stats[StatTypes.MP], stats[StatTypes.MMP]);
            lvLabel.text = string.Format("LV. {0}", stats[StatTypes.LVL]);
        }
    }
}