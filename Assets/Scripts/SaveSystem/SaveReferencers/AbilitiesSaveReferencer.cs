using Sirenix.OdinInspector;
using UnityEngine;

public class AbilitiesSaveReferencer : SaveReferencer
{
    [BoxGroup("References")] [Required] [SerializeField] Shield shield;
        
        public override void OnSave(SaveData data)
        {
            data.abilitiesData.canShoot = shield.stats.canShoot;
            data.abilitiesData.canBlock = shield.stats.canBlock;
            data.abilitiesData.pierce = shield.stats.bulletStats.pierce;
        }
        public override void OnLoad(SaveData data)
        {
            shield.stats.canShoot = data.abilitiesData.canShoot;
            shield.stats.canBlock = data.abilitiesData.canBlock;
            shield.stats.bulletStats.pierce = data.abilitiesData.pierce;
        }
    }