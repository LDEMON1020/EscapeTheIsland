using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public static class ToolDamageTable
    {
        public static int GetDamage(ItemType tool, Block block)
        {
            int baseDamage = 1;

            switch (tool)
            {
                case ItemType.Stone_Axe:
                    baseDamage = 3;
                    if (block.IsWoodType())
                        baseDamage += 2; // Axe → Wood 보너스
                    break;

                case ItemType.Stone_Shovel:
                    baseDamage = 2;
                    if (block.IsDirtType())
                        baseDamage += 2; // Shovel → Dirt 보너스
                    break;
            }

            return baseDamage;
        }
    }

