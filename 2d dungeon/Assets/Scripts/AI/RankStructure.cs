using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class RankStructure : MonoBehaviour
    {
        public enum RoleType
        {
            COMMANDER, GUNNER, RUNNER, SQUADDIE, LOOTER, MEDIC
        }
        public class AIModel
        {
            public string Name;
            public float Health;
            public int WeaponAmmo;
            public int StoredAmmo;
            public bool Radio;
            public bool Active;
            public RoleType Role;
            //Loadout loadout;
            //int[,] inventory;
            public AIModel(string name, int health, int weaponammo, int storedammo, bool radio, bool active, RoleType role)
            {
                Name = name;
                Health = health;
                WeaponAmmo = weaponammo;
                StoredAmmo = storedammo;
                Radio = radio;
                Active = active;
                Role = role;

            }
        }

        public class AISquad
        {
            public string Name;
            public List<AIModel> Models;
            public AISquad(string name,List<AIModel> models)
            {
                Name=name;
                Models = models;
            }
        }

        public class AIPlatoon
        {
            public string Name;
            public List<AISquad> Squads;
            public AIPlatoon(string name, List<AISquad> squads)
            {
                Name = name;
                Squads = squads;
            }
        }
    }
}
