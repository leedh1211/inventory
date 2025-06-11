using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace _02_Scripts.UI
{
    public class CharacterUIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text expFillText;
        [SerializeField] private Slider expFillBar;

        public void Start()
        {
            Init();
        }
        
        public void Init()
        {
            nameText.text = PlayerManager.Instance.playerData.name;
            levelText.text = PlayerManager.Instance.playerData.level.ToString();   
        }
    }
}