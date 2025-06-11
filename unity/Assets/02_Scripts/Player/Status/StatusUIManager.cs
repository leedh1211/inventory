using System.Linq;
using UnityEngine;

namespace _02_Scripts.Player.Status
{
    public class StatusUIManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject statusPanel;
        [SerializeField]
        private GameObject statRowSlotPrefab;
        [SerializeField]
        private GameObject content;

        public void MakeStatusRow()
        {
            PlayerStatData playerStatData = PlayerManager.Instance.playerStatData;
            if (statRowSlotPrefab == null)
            {
                Debug.LogError("statRowSlotPrefab이 할당되지 않았습니다.");
                return;
            }
            foreach (var stat in playerStatData.stats)
            {
                GameObject instance = Instantiate(statRowSlotPrefab, content.transform);
                StatusSlotUIManager slotUI = instance.GetComponent<StatusSlotUIManager>();

                if (slotUI == null)
                {
                    Debug.LogError("StatusSlotUIManager가 프리팹에 붙어있지 않습니다.");
                    return;
                }

                StatData itemStatData = PlayerManager.Instance.playerItemStatData.stats
                    .FirstOrDefault(s => s.stat_id == stat.stat_id);
                slotUI.SetSlot(stat,itemStatData);
            }
        }

        public void ShowStatusPanel()
        {
            statusPanel.SetActive(true);
        }

        public void HideStatusPanel()
        {
            statusPanel.SetActive(false);
        }
    }
}