using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextRandomizer : MonoBehaviour {

    private static int textIndex = 0;

	// Use this for initialization
	void Start () {
        switch (textIndex)
        {
            case 0: 
                GetComponent<Text>().text =
                    "Welcome to the Pet Garden Danger Zone.\n" +
                    "\n" +
                    "Please keep the bunnies alive - they're too cute to die :(\n" +
                    "\n" +
                    "Have fun and aim for the high score!";
                break;
            case 1:
                GetComponent<Text>().text =
                    "Welcome to the Pet Garden Hazard Zone.\n" +
                    "\n" +
                    "Please keep the bunnies alive - they're cuter without pneumonia :)\n" +
                    "\n" +
                    "Have fun and aim for the high score!";
                break;
            case 2:
                GetComponent<Text>().text =
                    "Welcome to the Bunny Garden Danger Zone.\n" +
                    "\n" +
                    "Please keep the bunnies alive - they're too cute to die :(\n" +
                    "\n" +
                    "Have fun and aim for the high score!";
                break;
            case 3:
                GetComponent<Text>().text =
                    "Welcome to the Danger Garden Danger Zone.\n" +
                    "\n" +
                    "Please keep the bunnies alive - we worked so hard on them :(\n" +
                    "\n" +
                    "Have fun and aim for the high score!";
                break;
            case 4:
                GetComponent<Text>().text =
                    "Welcome to the Pet Garden Pet Zone.\n" +
                    "\n" +
                    "Please keep the bunnies alive - they taste better that way :)\n" +
                    "\n" +
                    "Have fun and aim for the high score!";
                break;
            case 5:
                GetComponent<Text>().text =
                    "Bunny to the Pet Bunny Danger Bunny.\n" +
                    "\n" +
                    "Please bunny the bunnies alive - they're too bunny to die >:D\n" +
                    "\n" +
                    "\\(^^)/ Have fun and aim for the high score! \\(^^)/";
                break;
            default:
                GetComponent<Text>().text =
                    "Hey look, it's mister Sign! Hello, mister Sign!\n" +
                    "\n"+
                    "Looks like I've run out of text. Don't worry: it cycles!\n"+
                    "\n"+
                    "By the way, have you tried picking me up? I'm very useful as a shovel!";
                textIndex = -1;
                break;
        }

        textIndex++;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
