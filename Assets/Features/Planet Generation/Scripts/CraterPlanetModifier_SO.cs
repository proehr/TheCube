using UnityEngine;

namespace Features.Planet_Generation.Scripts
{
    [CreateAssetMenu(fileName = "CraterPlanetModifier", menuName = "PlanetModifiers/CraterPlanetModifier", order = 0)]
    public class CraterPlanetModifier_SO : PlanetModifier
    {
        
        [Tooltip("Min and Max Value of possible crater radius")]
        [SerializeField] private Vector2Int radiusInterval;
        [Tooltip("Amount of craters created by this modifier")]
        [SerializeField] private int amount;

        public override void ModifyPlanet(Resource_SO[][][] resources)
        {
            for (int i = 0; i < amount; i++)
            {
                int side = Random.Range(0, 6);
                Vector3Int craterPosition = new Vector3Int();
                Vector3Int craterDirection = new Vector3Int();
                Vector3Int craterTangentOne = new Vector3Int();
                Vector3Int craterTangentTwo = new Vector3Int();
                switch (side)
                {
                    case 0:
                        craterPosition.x = 0;
                        craterPosition.y = Random.Range(0, resources.Length);
                        craterPosition.z = Random.Range(0, resources.Length);
                        craterDirection = new Vector3Int(1, 0, 0);
                        craterTangentOne = new Vector3Int(0, 1, 0);
                        craterTangentTwo = new Vector3Int(0, 0, 1);
                        break;
                    case 1:
                        craterPosition.x = resources.Length - 1;
                        craterPosition.y = Random.Range(0, resources.Length);
                        craterPosition.z = Random.Range(0, resources.Length);
                        craterDirection = new Vector3Int(-1, 0, 0);
                        craterTangentOne = new Vector3Int(0, 1, 0);
                        craterTangentTwo = new Vector3Int(0, 0, 1);
                        break;
                    case 2:
                        craterPosition.x = Random.Range(0, resources.Length);
                        craterPosition.y = 0;
                        craterPosition.z = Random.Range(0, resources.Length);
                        craterDirection = new Vector3Int(0, 1, 0);
                        craterTangentOne = new Vector3Int(1, 0, 0);
                        craterTangentTwo = new Vector3Int(0, 0, 1);
                        break;
                    case 3:
                        craterPosition.x = Random.Range(0, resources.Length);
                        craterPosition.y = resources.Length - 1;
                        craterPosition.z = Random.Range(0, resources.Length);
                        craterDirection = new Vector3Int(0, -1, 0);
                        craterTangentOne = new Vector3Int(1, 0, 0);
                        craterTangentTwo = new Vector3Int(0, 0, 1);
                        break;
                    case 4:
                        craterPosition.x = Random.Range(0, resources.Length);
                        craterPosition.y = Random.Range(0, resources.Length);
                        craterPosition.z = 0;
                        craterDirection = new Vector3Int(0, 0, 1);
                        craterTangentOne = new Vector3Int(1, 0, 0);
                        craterTangentTwo = new Vector3Int(0, 1, 0);
                        break;
                    case 5:
                        craterPosition.x = Random.Range(0, resources.Length);
                        craterPosition.y = Random.Range(0, resources.Length);
                        craterPosition.z = resources.Length - 1;
                        craterDirection = new Vector3Int(0, 0, -1);
                        craterTangentOne = new Vector3Int(1, 0, 0);
                        craterTangentTwo = new Vector3Int(0, 1, 0);
                        break;
                }

                int craterRadius = Random.Range(radiusInterval.x, radiusInterval.y + 1);
                for (int j = 0; j < craterRadius; j++)
                {
                    for (int k = -craterRadius + j + 1; k <= craterRadius - j - 1; k++)
                    {
                        for (int l = -craterRadius + j + 1; l <= craterRadius - j - 1; l++)
                        {
                            Vector3Int removable = new Vector3Int(
                                craterPosition.x + craterDirection.x * j + craterTangentOne.x * k +
                                craterTangentTwo.x * l,
                                craterPosition.y + craterDirection.y * j + craterTangentOne.y * k +
                                craterTangentTwo.y * l,
                                craterPosition.z + craterDirection.z * j + craterTangentOne.z * k +
                                craterTangentTwo.z * l);
                            if (removable.x >= 0 && removable.x < resources.Length &&
                                removable.y >= 0 && removable.y < resources.Length &&
                                removable.z >= 0 && removable.z < resources.Length)
                            {
                                resources[removable.x][removable.y][removable.z] = null;
                            }
                        }
                    }
                }
            }
        }
    }
}