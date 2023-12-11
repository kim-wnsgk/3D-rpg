using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Security.Cryptography;

namespace Kryz.CharacterStats.Examples
{
    public class BuffTooltip : MonoBehaviour
    {
        public static BuffTooltip Instance;

        [SerializeField] Image iconImage;
        [SerializeField] Text slotTypeText;
        [SerializeField] Text descriptionText;

        private StringBuilder sb = new StringBuilder();
        private StringBuilder sb2 = new StringBuilder();

        private UsableItem currentBuffItem;
        private float currentBuffDuration;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
            gameObject.SetActive(false);
        }

        public void ShowTooltip(UsableItem buffItem)
        {
            if (!(buffItem is UsableItem))
            {
                return;
            }

            StatBuffItemEffect statBuffEffect = buffItem.Effects.Find(effect => effect is StatBuffItemEffect) as StatBuffItemEffect;

            if (statBuffEffect != null)
            {
                // StatBuffItemEffect�� parameters�� ������
                BuffParameters parameters = new BuffParameters
                {
                    DexterityBuff = statBuffEffect.DexterityBuff,
                    StrengthBuff = statBuffEffect.StrengthBuff,
                    HPandMPBuff = statBuffEffect.HPandMPBuff,
                    DamageBuff = statBuffEffect.DamageBuff,
                    StrengthPercentBuff = statBuffEffect.StrengthPercentBuff,
                    DexterityPercentBuff = statBuffEffect.DexterityPercentBuff,
                    HPandMPPercentBuff = statBuffEffect.HPandMPPercentBuff,
                    DamagePercentBuff = statBuffEffect.DamagePercentBuff,
                    Duration = statBuffEffect.Duration,
                    DurationP = statBuffEffect.DurationP,
                };

                sb.Length = 0;

                if (parameters.DexterityBuff != 0 || parameters.StrengthBuff != 0 || parameters.HPandMPBuff != 0 || parameters.DamageBuff != 0)
                {
                    sb.AppendLine();
                    sb.Append("���� ���");
                }

               
                if (parameters.DexterityBuff != 0)
                    sb.AppendLine();
                    sb.Append("�ż� +" + parameters.DexterityBuff + "���� �ð� (" + parameters.Duration + ")");
                if (parameters.StrengthBuff != 0)
                    sb.AppendLine();
                    sb.Append("�� +" + parameters.StrengthBuff + "���� �ð� (" + parameters.Duration + ")");
                if (parameters.HPandMPBuff != 0)
                    sb.AppendLine();
                    sb.Append("ü�� +" + parameters.HPandMPBuff + "���� �ð� (" + parameters.Duration + ")");
                if (parameters.DamageBuff != 0)
                    sb.AppendLine();
                    sb.Append("������ +" + parameters.DamageBuff + "���� �ð� (" + parameters.Duration + ")");

                if (parameters.DexterityPercentBuff != 0 || parameters.StrengthPercentBuff != 0 || parameters.HPandMPPercentBuff != 0 || parameters.DamagePercentBuff != 0)
                {
                    sb.AppendLine();
                    sb.Append("���� ���");
                }
                if (parameters.DexterityBuff != 0)
                    sb.AppendLine();
                    sb.Append("�ż� +" + parameters.DexterityPercentBuff + "���� �ð� (" + parameters.DurationP + ")");
                if (parameters.StrengthBuff != 0)
                    sb.AppendLine();
                    sb.Append("�� +" + parameters.StrengthPercentBuff + "���� �ð� (" + parameters.DurationP + ")");
                if (parameters.HPandMPBuff != 0)
                    sb.AppendLine();
                    sb.Append("ü�� +" + parameters.HPandMPPercentBuff + "���� �ð� (" + parameters.DurationP + ")");
                if (parameters.DamageBuff != 0)
                    sb.AppendLine();
                    sb.Append("������ +" + parameters.DamagePercentBuff + "���� �ð� (" + parameters.DurationP + ")");

            }

            


            // Set the icon, name, and slot type.
            iconImage.sprite = buffItem.Icon;
            //nameText.text = buffItem.name;
            slotTypeText.text = buffItem.GetItemType();

            // Set initial description.
            descriptionText.text = buffItem.GetDescription();

            gameObject.SetActive(true);
        }

        private void Update()
        {
            if (currentBuffItem == null)
                return;

            // Update remaining duration and description.
            currentBuffDuration -= Time.deltaTime;
            if (currentBuffDuration <= 0f)
            {
                currentBuffItem = null;
                gameObject.SetActive(false);
            }
            //durationText.text = string.Format("Remaining: {0:F1}", currentBuffDuration);
            descriptionText.text = currentBuffItem.GetDescription();
        }
    }
}
