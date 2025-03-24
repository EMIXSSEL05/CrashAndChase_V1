using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunManager : MonoBehaviour
{
    public Image gunProgression;

    public GameObject projectilePrefab; // Prefab del proyectil
    public GameObject powerUpPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform firePointPowerUp;
    [SerializeField] private float cooldownTime = 10f; // Tiempo de espera entre disparos

    private float lastFireTime = -Mathf.Infinity; // Tiempo del último disparo
    private float cooldownCounter = 0f; // Contador progresivo
    private PlayerController playerControllerScript;

    private void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        cooldownCounter = cooldownTime;
    }

    void Update()
    {
        // Detecta si se presiona la tecla E
        if (Input.GetKeyDown(KeyCode.E) && playerControllerScript.isAlive)
        {
            // Verifica si el tiempo transcurrido desde el último disparo es mayor o igual al tiempo de espera
            if (Time.time >= lastFireTime + cooldownTime)
            {
                Fire(); // Dispara el arma
                lastFireTime = Time.time; // Actualiza el tiempo del último disparo
                cooldownCounter = 0f; // Reinicia el contador
            }

            if(playerControllerScript.numPowerUp >= 4)
            {
                PowerUp();
                playerControllerScript.numPowerUp = 0;
            }
        }

        // Actualiza el contador progresivo durante el enfriamiento
        if (Time.time < lastFireTime + cooldownTime && playerControllerScript.isAlive)
        {
            cooldownCounter = Mathf.Min(cooldownCounter + Time.deltaTime, cooldownTime);
            Debug.Log($"Contador de enfriamiento: {cooldownCounter:F2} segundos");
        }

        float normalizedScore = Mathf.Clamp01(cooldownCounter / cooldownTime);
        gunProgression.fillAmount = normalizedScore;

    }

    private void Fire()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Debug.Log("¡Disparo realizado!");
    }

    private void PowerUp()
    {
        Instantiate(powerUpPrefab, firePointPowerUp.position, firePointPowerUp.rotation);
    }
}

