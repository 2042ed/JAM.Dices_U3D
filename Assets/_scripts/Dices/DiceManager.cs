using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DiceManager : MonoBehaviour
{
    public static DiceManager I;

    public Text ResultText;

    public GameObject DiceSelector;

    public GameObject DiceContainer;
    public Transform StartPosition;

    public GameObject PrefabD4;
    public GameObject PrefabD6;
    public GameObject PrefabD8;
    public GameObject PrefabD10;
    public GameObject PrefabD12;
    public GameObject PrefabD20;

    public float randomDirectionOffset = 0.1f;
    float throwStrength;
    float spinTorque;

    string resultString;

    bool hasThrown;

    void Awake ()
    {
        I = this;
        Reset ();
    }

    public void RollDice (int faces)
    {
//        int result = Random.Range (1, faces);
//        ResultText.text = result.ToString ();

        SpawnDice (faces);

    }

    public void SpawnDice (int faces)
    {
        ResetResult ();
        GameObject dicePrefab = null;
        switch (faces) {
        case 4:
            dicePrefab = PrefabD4;
            break;
        case 6:
            dicePrefab = PrefabD6;
            break;
        case 8:
            dicePrefab = PrefabD8;
            break;
        case 10:
            dicePrefab = PrefabD10;
            break;
        case 12:
            dicePrefab = PrefabD12;
            break;
        case 20:
            dicePrefab = PrefabD20;
            break;
        }
        GameObject spawnedDice = Instantiate (dicePrefab, StartPosition.position, Quaternion.identity) as GameObject;
        spawnedDice.transform.SetParent (DiceContainer.transform);
        spawnedDice.transform.rotation = Random.rotationUniform;
        //spawnedDice.GetComponent <DiceMaster.Dice> ().myCallback = OnDiceResult;

    }

    public void Reset ()
    {
        Debug.Log ("RESET");
        DiceSelector.SetActive (true);
        hasThrown = false;
        ResetResult ();
        foreach (Transform t in DiceContainer.transform) {
            Destroy (t.gameObject);
        }
       
    }

    public void ResetResult ()
    {
        resultString = "";
        ResultText.text = resultString;
    }

    public void OnDiceResult (int value)
    {
        if (hasThrown) {
            DiceSelector.SetActive (true);

            if (resultString != "") {
                resultString = resultString + " - ";
            }

            resultString = resultString + value;
            ResultText.text = resultString;
        }
    }

    public void OnSwipe (Lean.LeanFinger finger)
    {
        var swipe = finger.SwipeDelta;

        //Debug.Log ("SWIPE! " + swipe);
        ResetResult ();
        DiceSelector.SetActive (false);
        //throwStrength = swipe.sqrMagnitude;

        throwStrength = swipe.magnitude / 300f;
        spinTorque = swipe.magnitude / 300f;
        Debug.Log ("throwStrength " + throwStrength);

        Vector3 actualDir = new Vector3 (swipe.normalized.x, throwStrength / 3f, swipe.normalized.y);
        var actualAxis = Vector3.right;

        foreach (Transform t in DiceContainer.transform) {
            
            // Some randomization is useful to avoid always getting the same behaviour
            actualDir += new Vector3 (Random.Range (-1f, 1f),
                Random.Range (-1f, 1f),
                Random.Range (-1f, 1f)) * randomDirectionOffset;

            actualAxis += new Vector3 (Random.Range (-1f, 1f),
                Random.Range (-1f, 1f),
                Random.Range (-1f, 1f)) * 0.1f;
            actualAxis.Normalize ();

            // Add the force
            t.GetComponent<Rigidbody> ().AddForce (actualDir * throwStrength, ForceMode.Impulse);
            t.GetComponent<Rigidbody> ().AddTorque (actualAxis * spinTorque, ForceMode.Impulse);
        }

        hasThrown = true;
    }
}
