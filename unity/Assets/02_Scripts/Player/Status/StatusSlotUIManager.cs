using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _02_Scripts.Player.Status
{
    public class StatusSlotUIManager : MonoBehaviour
    {
        [SerializeField]
        private Image Icon;

        [SerializeField]
        private TMP_Text StatName;

        [SerializeField]
        private TMP_Text StatValue;

        public void SetSlot(StatData statData, StatData itemStatData)
        {
            StatMasterLoader.statMasterDict.TryGetValue(statData.stat_id, out var master);
            Sprite icon = Resources.Load<Sprite>($"Icon/{master.icon}");

            Icon.sprite = icon;
            StatName.text = master.name;
            if (itemStatData != null)
            {
                StatValue.text = (statData.stat_value+itemStatData.stat_value).ToString();
            }
            else
            {
                StatValue.text = statData.stat_value.ToString();    
            }
            
        }
    }
}