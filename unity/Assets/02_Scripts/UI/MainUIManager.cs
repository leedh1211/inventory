using UnityEngine;

namespace _02_Scripts.UI
{
    public class MainUIManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject BtnPanel;
        
        public void ShowBtn()
        {
            BtnPanel.SetActive(true);
        }

        public void HideBtn()
        {
            BtnPanel.SetActive(false);
        }
    }
}