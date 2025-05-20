using Unity.VisualScripting;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    //constantes son no cambiantes
    const int COLLUMNS = 13;
    const int ROW = 13;
    const int COL_OFF = 4;
    const int ROW_OFF = 4;

    [SerializeField] GameObject UnbreakableWallPref;
    [SerializeField] GameObject BreakableWallPref;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateBreakableWall();
        GenerateUnBreakableWall();
    }

    void GenerateBreakableWall()
    {
        bool generateThisTile = false;
        generateThisTile = GenerateSpecialFirstAndLastBreakables(generateThisTile, 0);
        generateThisTile = GenerateSpecialSecondFirstAndLastBreakables(generateThisTile, 1);

        for (int i = 2; i < ROW -2; i++)
        {
            for (int j = 0; j < COLLUMNS; j++)
            {
                if (generateThisTile)
                {
                    GameObject newBreakable = Instantiate(BreakableWallPref, gameObject.transform);
                    newBreakable.transform.localPosition = new Vector3(j * COL_OFF, 2, -i * ROW_OFF);
                }
                generateThisTile = !generateThisTile;
            }
        }
        generateThisTile = GenerateSpecialSecondFirstAndLastBreakables(generateThisTile, ROW -2);
        generateThisTile = GenerateSpecialFirstAndLastBreakables(generateThisTile, ROW -1);
    }

    private bool GenerateSpecialSecondFirstAndLastBreakables(bool generateThisTile, int row)
    {
        for (int j = 0; j < COLLUMNS; j++) // ROW 1
        {
            if (generateThisTile)
            {
                if (j != 0 && j != COLLUMNS - 1)
                {
                    GameObject newBreakable = Instantiate(BreakableWallPref, gameObject.transform);
                    newBreakable.transform.localPosition = new Vector3(j * COL_OFF, 2, -row * ROW_OFF);
                }
            }
            generateThisTile = !generateThisTile;
        }

        return generateThisTile;
    }

    private bool GenerateSpecialFirstAndLastBreakables(bool generateThisTile, int row)
    {
        for (int j = 0; j < COLLUMNS; j++) 
        {
            if (generateThisTile)
            {
                if (j != 1 && j != COLLUMNS - 2)
                {
                    GameObject newBreakable = Instantiate(BreakableWallPref, gameObject.transform);
                    newBreakable.transform.localPosition = new Vector3(j * COL_OFF, 2, -row * ROW_OFF);
                }
            }
            generateThisTile = !generateThisTile;
        }

        return generateThisTile;
    }

    void GenerateUnBreakableWall()
    {
        for (int i = 1; i < ROW; i += 2)
        {
            bool generateThisTile = false;
            for (int j = 0; j < COLLUMNS; j++)
            {
                if (generateThisTile)
                {
                    GameObject newUnbreakable = Instantiate(UnbreakableWallPref, gameObject.transform);
                    newUnbreakable.transform.localPosition = new Vector3(j * COL_OFF, 2, -i * ROW_OFF);
                }
                generateThisTile = !generateThisTile;
            }
        }
    }
}
