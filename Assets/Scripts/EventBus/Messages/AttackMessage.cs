using UnityEngine;

namespace MyProject.Events
{
    public class AttackMessage : Message
    {
        public bool IsMobAttacking { get; private set; } = false;
        public bool IsPrepareAttack { get; private set; } = false;
        public float DamageCount { get; private set; }
        public Vector3 AttackingUnitPosition { get; private set; }
        public Vector3 AttackedUnitPosition { get; private set; }

        public AttackMessage(bool isMobAttacking, bool isPrepareAttack, float damageCount, Vector3 attackingUnitPosition, Vector3 attackedUnitPosition)
        {
            IsMobAttacking = isMobAttacking;
            IsPrepareAttack = isPrepareAttack;
            DamageCount = damageCount;
            AttackedUnitPosition = attackedUnitPosition;
            AttackingUnitPosition = attackingUnitPosition;
        }
    }
}