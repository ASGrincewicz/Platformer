namespace Veganimus.Platformer
{
    [System.Serializable]
    public struct EnemyInfo
    {
        public int hitPoints;
        public float attackRange;
        public float chaseSpeed;
        public float sightDistance;
        public float speed;

        public EnemyInfo(int hp, float attackRange, float chaseSpeed, float sightDistance, float speed)
        {
            hitPoints = hp;
            this.attackRange = attackRange;
            this.chaseSpeed = chaseSpeed;
            this.sightDistance = sightDistance;
            this.speed = speed;
        }

        public static EnemyInfo operator+(EnemyInfo e1, EnemyInfo e2)
        {
            EnemyInfo newEnemyInfo = new EnemyInfo();
            newEnemyInfo.hitPoints = e1.hitPoints + e2.hitPoints;
            newEnemyInfo.attackRange = e1.attackRange + e2.attackRange;
            newEnemyInfo.chaseSpeed = e1.chaseSpeed + e2.chaseSpeed;
            newEnemyInfo.sightDistance = e1.sightDistance + e2.sightDistance;
            newEnemyInfo.speed = e1.speed + e2.speed;
            return newEnemyInfo;
        }
    }
}
