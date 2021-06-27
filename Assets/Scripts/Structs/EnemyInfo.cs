namespace Veganimus.Platformer
{
    [System.Serializable]
    public struct EnemyInfo
    {
        public int hitPoints;
        public float speed;
        public float chaseSpeed;
        public float sightDistance;
        public float attackRange;

        public EnemyInfo(int hp, float speed, float chaseSpeed, float sightDistance, float attackRange)
        {
            hitPoints = hp;
            this.speed = speed;
            this.chaseSpeed = chaseSpeed;
            this.sightDistance = sightDistance;
            this.attackRange = attackRange;
        }
    }
}
