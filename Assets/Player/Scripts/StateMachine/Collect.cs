using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collect : MonoBehaviour
{
    public int nbRamasser = 0;
    public TMP_Text objRamasser;
    private bool isCollected = false;
    public float moveDistance = 0.5f;
    public float moveDuration = 3f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isCollected && other.gameObject.CompareTag("Coin"))
        {
            isCollected = true;
            nbRamasser++;
            StartCoroutine(CollectAndAnimate(other.gameObject));
        }
    }

    // Coroutine pour gérer la collecte, l'animation et la désactivation
    IEnumerator CollectAndAnimate(GameObject obj)
    {
        // Définir la position d'origine
        Vector3 originalPosition = obj.transform.position;
        Vector3 targetPosition = originalPosition + new Vector3(0, moveDistance, 0);

        // Déplacement vers le haut
        float elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            obj.transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        obj.transform.position = targetPosition;

        // Revenir à la position d'origine
        elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            obj.transform.position = Vector3.Lerp(targetPosition, originalPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        obj.transform.position = originalPosition;

        // Désactiver l'objet après l'animation
        yield return new WaitForSeconds(0.01f);
        obj.SetActive(false);
        isCollected = false;
    }

    // Update is called once per frame
    void Update()
    {
        objRamasser.SetText(nbRamasser + "/21");
    }
}