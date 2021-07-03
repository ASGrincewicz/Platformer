namespace Veganimus.Platformer
{
    [System.Serializable]
    public struct EnemyInfo
    {
        public sbyte hitPoints;
        public float speed;
        public float chaseSpeed;
        public float sightDistance;
        public float attackRange;

        public EnemyInfo(sbyte hp, float speed, float chaseSpeed, float sightDistance, float attackRange)
        {
            hitPoints = hp;
            this.speed = speed;
            this.chaseSpeed = chaseSpeed;
            this.sightDistance = sightDistance;
            this.attackRange = attackRange;
        }
    }
}
