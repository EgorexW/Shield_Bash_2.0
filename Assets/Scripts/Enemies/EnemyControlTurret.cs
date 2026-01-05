using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class EnemyControlTurret : MonoBehaviour
    {
        [BoxGroup("References")][Required][SerializeField] Enemy enemy;
        [BoxGroup("References")][Required][SerializeField] Turret turret;

        float bulletsLeft = Mathf.Infinity;

        [FoldoutGroup("Events")] public UnityEvent onRunOutOfBullets;

        public TurretStats TurretStats{
            get => turret.stats;
            set => turret.stats = value;
        }

        void Start()
        {
            turret.beforeShoot.AddListener(BeforeShoot);
            enemy.onChangeState.AddListener(OnChangeState);
        }

        void OnChangeState(EnemyState arg0)
        {
            if (arg0 == EnemyState.Inactive)
            {
                turret.autoShoot = false;
                Debug.Log("Turret disabled for inactive enemy", this);
            }
        }

        void BeforeShoot()
        {
            turret.teammates = enemy.GetTeammates().ConvertAll(e => e.gameObject);
            bulletsLeft -= 1;
            if (bulletsLeft <= 0)
            {
                StopShooting();
                onRunOutOfBullets.Invoke();
            }
        }

        void OnDestroy()
        {
            turret.beforeShoot.RemoveListener(BeforeShoot);
        }


        public void StopShooting()
        {
            turret.autoShoot = false;
        }

        public void ShootBullets(float bulletsNr)
        {
            bulletsLeft = bulletsNr;
            StartShooting();
        }

        void StartShooting()
        {
            if (enemy.GetState() == EnemyState.Active){
                turret.autoShoot = true;
            }
            else{
                Debug.LogWarning("Cannot start shooting: enemy is not active", this);
            }
        }
    }