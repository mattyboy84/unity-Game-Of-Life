using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject agentPrefab;
    public int width = 100;
    public int height = 100;

    private GameObject[,] agents;

    private bool[,] previous;
    private bool[,] current;

    void Start()
    {
        Camera.main.orthographicSize = width / 2;

        float agentSize = 1.0f;

        agents = new GameObject[width, height];
        previous = new bool[width, height];
        current = new bool[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float xOffset = -(width * agentSize) / 2 + i * agentSize + agentSize / 2;
                float yOffset = -(height * agentSize) / 2 + j * agentSize + agentSize / 2;

                agents[i, j] = Instantiate(agentPrefab, new Vector3(xOffset, yOffset, 0), Quaternion.identity);

                current[i, j] = (Random.Range(0f, 1f) <= 0.5f );

                agents[i, j].SetActive(current[i, j]);

            }
        }
    }
    
    private void FixedUpdate()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                previous[i, j] = current[i, j];
            }
        }
        
        
        
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
        
                int aliveNeighbours = 0;
                for (int k = -1; k < 2; k++)
                {
                    for (int l = -1; l < 2; l++)
                    {
                        if (k == 0 && l == 0) continue;
        
                        int x = i + k;
                        int y = j + l;
        
                        if (x >= 0 && x < width && y >= 0 && y < height)
                        {
                            if (previous[x, y])
                            {
                                aliveNeighbours++;
                            }
                        }
                    }
                }
                //
                //
                if (previous[i, j] && (aliveNeighbours < 2 || aliveNeighbours > 3))
                {
                    current[i, j] = false;
                }
                else if (!previous[i, j] && aliveNeighbours == 3)
                {
                    current[i, j] = true;
                }
        
                agents[i, j].SetActive(current[i, j]);
            }
        }
    }
    
}
