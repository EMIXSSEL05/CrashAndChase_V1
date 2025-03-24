using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [Header("Configuración del Temporizador")]
    public Image levelProgression;
    public float endTimeInSeconds = 10f; // Tiempo final en segundos
    public float levelTime1;
    public float difficultyLevel1;

    public float levelTime2;
    public float difficultyLevel2;

    public float levelTime3;
    public float difficultyLevel3;

    private float currentTime = 0f;
    private bool isCounting = true;

    private ObstacleType obstacleType;
    private PlayerController playerControllerScript;


    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        // Inicializa el contador en 0
        currentTime = 0f;
    }

    void Update()
    {
        if (isCounting && playerControllerScript.isAlive)
        {
            // Incrementa el tiempo actual
            currentTime += Time.deltaTime;

            // Convierte el tiempo actual a entero para Debug.Log
            int timeToDisplay = Mathf.FloorToInt(currentTime);

            // Detén el contador si alcanza el tiempo final
            if (currentTime >= endTimeInSeconds)
            {
                currentTime = endTimeInSeconds;
                isCounting = false;
                OnTimerEnd();
            }
        }

        if(obstacleType = FindObjectOfType<ObstacleType>())
        {
            if(currentTime >= levelTime1)
            {
                obstacleType.speedLeft = difficultyLevel1;
            }

            if (currentTime >= levelTime2)
            {
                obstacleType.speedLeft = difficultyLevel2;
            }

            if (currentTime >= levelTime3)
            {
                obstacleType.speedLeft = difficultyLevel3;
            }
        }

        // Normaliza currentTime de 0 a 1 y asigna a barraVida
        float normalizedScore = Mathf.Clamp01(currentTime / endTimeInSeconds);
        levelProgression.fillAmount = normalizedScore;
    }

    // Método llamado cuando el contador alcanza el tiempo final
    void OnTimerEnd()
    {
        Debug.Log("¡El contador ha terminado!");
        // Puedes implementar acciones adicionales aquí
    }
}


