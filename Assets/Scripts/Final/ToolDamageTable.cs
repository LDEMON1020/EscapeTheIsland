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
                    baseDamage = 1;
                    if (block.IsWoodType())
                        baseDamage += 2; // Axe → Wood 보너스
                    break;

                case ItemType.Stone_Shovel:
                    baseDamage = 1;
                    if (block.IsDirtType())
                        baseDamage += 2; // Shovel → Dirt 보너스
                    break;
            case ItemType.Iron_Axe:
                    baseDamage = 1;
                    if (block.IsWoodType())
                        baseDamage += 4; // Iron Axe → Wood 보너스
                    break;
                case ItemType.iron_Shovel:
                    baseDamage = 1;
                    if (block.IsDirtType())
                        baseDamage += 4; // Iron Shovel → Dirt 보너스
                    break;
                case ItemType.Stone_Pickaxe:
                    baseDamage = 1;
                    if (block.IsStoneType())
                        baseDamage += 2; // Pickaxe → Stone 보너스
                    break;
                case ItemType.Iron_Pickaxe:
                    baseDamage = 1;
                    if (block.IsStoneType())
                        baseDamage += 4; // Iron Pickaxe → Stone 보너스
                    break;
        }

            return baseDamage;
        }
    }

