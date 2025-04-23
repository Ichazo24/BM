using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class GridGenerator : MonoBehaviour
{
    const int Col = 13;
    const int Row = 13;

    const int ColOff=4;
    const int RowOff=4;

    [SerializeField] GameObject UnbreakableWallPref;
    [SerializeField] GameObject breakableWallPref;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateBreakableWall();

        GenerateUnbreakableWall();
    }

    void GenerateBreakableWall()
    {
        bool generateThisTile = false;
        generateThisTile = GenerateSpecialFirstAndLAstBreakables(generateThisTile,0);
        generateThisTile = GenerateSpecialSecondFirstAndLastBreakables(generateThisTile,1);

        for (int i = 2; i < Row - 2; i++)
        {
            for (int j = 0; j < Col; j++)
            {
                if (generateThisTile)
                {
                    GameObject newBreakable = Instantiate(breakableWallPref, gameObject.transform);
                    newBreakable.transform.localPosition = new Vector3(j * ColOff, 2, -i * RowOff);
                }
                generateThisTile = !generateThisTile;
            }
        }
        generateThisTile = GenerateSpecialFirstAndLAstBreakables(generateThisTile, Row - 1);
        generateThisTile = GenerateSpecialSecondFirstAndLastBreakables(generateThisTile, Row - 2);
    }

    private bool GenerateSpecialSecondFirstAndLastBreakables(bool generateThisTile, int row)
    {
        for (int j = 0; j < Col; j++) // ROW 1
        {
            if (generateThisTile)
            {
                if (j != 0 && j != Col - 1)
                {
                    GameObject newBreakable = Instantiate(breakableWallPref, gameObject.transform);
                    newBreakable.transform.localPosition = new Vector3(j * ColOff, 2, -row * RowOff);
                }
            }
            generateThisTile = !generateThisTile;
        }

        return generateThisTile;
    }

    private bool GenerateSpecialFirstAndLAstBreakables(bool generateThisTile, int row)
    {
        for (int j = 0; j < Col; j++) // ROW 0
        {
            if (generateThisTile)
            {
                if (j != 1 && j != Col - 2)
                {
                    GameObject newBreakable = Instantiate(breakableWallPref, gameObject.transform);
                    newBreakable.transform.localPosition = new Vector3(j * ColOff, 2, -row * RowOff);
                }
            }
            generateThisTile = !generateThisTile;
        }

        return generateThisTile;
    }

    void GenerateUnbreakableWall()
    {
        for (int i = 0; i < Row; i += 2)
        {
            bool generateThisTile = false;

            for (int j = 0; j < Col; j++)
            {
                if (generateThisTile)
                {
                    GameObject newUnbreakable = Instantiate(UnbreakableWallPref, gameObject.transform);
                    newUnbreakable.transform.localPosition = new Vector3(j * ColOff, 2, -i * RowOff); 
                }
                generateThisTile = !generateThisTile;
            }
        }
    }

    
  
}
