using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CryptoSystems
{


    public enum CryptoLanguage
    {
        inverse,
        caesar,
        reverse,
        numeric,
        morse
    }

    Dictionary<char, string> MorseDictionary = new Dictionary<char, string>();

    public CryptoSystems()
    {
        MorseDictionary.Add('a', ".-");
        MorseDictionary.Add('b', "-...");
        MorseDictionary.Add('c', "-.-.");
        MorseDictionary.Add('d', "-..");
        MorseDictionary.Add('e', ".");
        MorseDictionary.Add('f', "..-.");
        MorseDictionary.Add('g', "--.");
        MorseDictionary.Add('h', "....");
        MorseDictionary.Add('i', "..");
        MorseDictionary.Add('j', ".--.");
        MorseDictionary.Add('k', "-.-");
        MorseDictionary.Add('l', ".-..");
        MorseDictionary.Add('m', "--");
        MorseDictionary.Add('n', "-.");
        MorseDictionary.Add('o', "---");
        MorseDictionary.Add('p', ".--.");
        MorseDictionary.Add('q', "--.-");
        MorseDictionary.Add('r', ".-.");
        MorseDictionary.Add('s', "...");
        MorseDictionary.Add('t', "-");
        MorseDictionary.Add('u', "..-");
        MorseDictionary.Add('v', "...-");
        MorseDictionary.Add('x', "-..-");
        MorseDictionary.Add('y', "-.--");
        MorseDictionary.Add('w', ".--");
        MorseDictionary.Add('z', "--..");
        MorseDictionary.Add('0', ".----");
        MorseDictionary.Add('1', ".----");
        MorseDictionary.Add('2', "..---");
        MorseDictionary.Add('3', "...--");
        MorseDictionary.Add('4', "....-");
        MorseDictionary.Add('5', ".....");
        MorseDictionary.Add('6', "-....");
        MorseDictionary.Add('7', "--...");
        MorseDictionary.Add('8', "---..");
        MorseDictionary.Add('9', "----.");
    }

    public string Morse(string plainIn)
    {
        char[] result = plainIn.ToCharArray();
        string output = "";
        for (int i = 0; i < result.Length; i++) {
            output += MorseChar(result[i]);
        }
        return output;
    }

    public string MorseChar(char charIn)
    {
        string output = "";
        char letter = charIn;

        if (letter >= '0' && letter <= '9') {
            output = MorseDictionary[letter] + " ";
        } else if (letter >= 'a' && letter <= 'z') {
            output = MorseDictionary[letter] + " ";
        } else if (letter >= 'A' && letter <= 'Z') {
            output = MorseDictionary[(char)(letter + 32)] + " ";
        } else {
            output += letter;
        }

        return output;
    }

    public string Inverse(string plainIn)
    {
        char[] result = plainIn.ToCharArray();
        for (int i = 0; i < result.Length; i++) {
            result[i] = InverseChar(result[i]);
        }
        return new string(result);
    }

    public char InverseChar(char charIn)
    {

        char letter = charIn;

        if (letter >= 'a' && letter <= 'z') {
            char offset = (char)(letter - 'a');

            letter = (char)('z' - offset);

        }

        if (letter >= 'A' && letter <= 'Z') {
            char offset = (char)(letter - 'A');

            letter = (char)('Z' - offset);

        }

        return letter;
    }

    public string Numeric(string plainIn)
    {
        char[] result = plainIn.ToCharArray();
        string output = "";
        for (int i = 0; i < result.Length; i++) {

            output += NumericChar(result[i]);

        }
        return output;
    }

    public string NumericChar(char charIn)
    {
        string output = "";
        char letter = charIn;

        if (letter >= 'a' && letter <= 'z') {
            output += (letter - 'a' + 1) + " ";

        } else if (letter >= 'A' && letter <= 'Z') {
            output += (letter - 'A' + 1) + " ";
        } else {
            output += letter;
        }

        return output;
    }

    public string Reverse(string plainIn)
    {
        char[] result = plainIn.ToCharArray();
        System.Array.Reverse(result);

        return new string(result);
    }

    /// <summary>
    /// Caesar the specified value and shift.
    /// </summary>
    /// <param name="value">Value.</param>
    /// <param name="shift">Shift.</param>
    public string Caesar(string value, int shift)
    {
        char[] result = value.ToCharArray();
        for (int i = 0; i < result.Length; i++) {
            result[i] = CaesarChar(result[i], shift);
        }
        return new string(result);
    }

    public char CaesarChar(char charIn, int shift)
    {
        char letter = charIn;

        if (letter >= 'a' && letter <= 'z') {
            letter = (char)(letter + shift);
            if (letter > 'z') {
                letter = (char)(letter - 26);
            } else if (letter < 'a') {
                letter = (char)(letter + 26);
            }

        }

        if (letter >= 'A' && letter <= 'Z') {
            letter = (char)(letter + shift);
            if (letter > 'Z') {
                letter = (char)(letter - 26);
            } else if (letter < 'A') {
                letter = (char)(letter + 26);
            }

        }

        return letter;
    }

}
