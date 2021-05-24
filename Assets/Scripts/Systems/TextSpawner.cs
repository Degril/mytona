using System;
using MyProject.Events;
using UnityEngine;

namespace Systems
{
    public class TextSpawner : MonoBehaviour
    {
        [SerializeField] private DamageText _damageText;
        
        private void Start()
        {
            EventBus<AttackMessage>.Sub(SpawnText);
        }

        private void OnDestroy()
        {
            EventBus<AttackMessage>.Unsub(SpawnText);
        }

        private void SpawnText(AttackMessage message)
        {
            if (message.IsPrepareAttack)
            {
                if(message.IsMobAttacking)
                     Instantiate(_damageText, message.AttackingUnitPosition, Quaternion.identity)
                         .Init(Color.red, "!");
            }
            else
            {
                Instantiate(_damageText, message.AttackedUnitPosition, Quaternion.identity)
                    .Init(message.IsMobAttacking ? Color.red : Color.green, message.DamageCount.ToString());
            }
        }
    }
}