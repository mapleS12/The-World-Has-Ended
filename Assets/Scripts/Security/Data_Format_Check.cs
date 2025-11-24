/* 
 * CSE 3310 || UTA Fall 2025 - Group 13
 * Project: THE WORLD HAS ENDED
 *
 * Check Data Format
 * Threat: Human-Readable Data
 * Response: Game will immediately terminate.
 * Purpose: Applies simple, reversible data scrambling to the final save file string using the XOR cipher and Base64 encoding. 
 *          Its purpose is purely to obstruct casual save file tampering by rendering the data payload and its inetgrity hash.
 *          completely human-unreadable, effectively hiding the key data from quick text-editor searches.
 */

using UnityEngine;   // Needed for Debug.LogError
using System;        // Needed for basic system functions.
using System.Text;   // Needed for hash conversions.


namespace Assets.Scripts.Security
{
    public class Data_Format_Check : MonoBehaviour
    {
        // --- XOR Obfuscation Key ---
        // CRITICAL: CHANGE THIS KEY TO A LONG, RANDOM, SECRET STRING!
        // This key must be the same for encoding and decoding.
        private const string ObfuscationKey = "NanoBananaCipherKey2025!";

        // Applies the XOR cipher to a string using the fixed ObfuscationKey.
        // Applying this function twice returns the original string.
        private static string ScrambleXOR(string data)
        {
            if (string.IsNullOrEmpty(data)) return string.Empty;

            // Step 1. Convert the data and key to byte arrays
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            byte[] keyBytes = Encoding.UTF8.GetBytes(ObfuscationKey);

            // Step 2. Prepare the output byte array.
            byte[] outputBytes = new byte[dataBytes.Length];

            // Step 3. Iterate through data, applying XOR with the key.
            for (int i = 0; i < dataBytes.Length; i++)
            {
                // The modulo (%) operator ensures we cycle back through the keyBytes.
                outputBytes[i] = (byte)(dataBytes[i] ^ keyBytes[i % keyBytes.Length]);
            }

            // Step 4. Convert the scrambled bytes back into a string (using Base64 for safe storage).
            // Base64 ensures the scrambled bytes are represented as safe, printable ASCII characters.
            return Convert.ToBase64String(outputBytes);
        }

        // Encodes the final save data string (which already contains the integrity hash).
        // into an obfuscated, human-readable string using XOR + Base64.
        public static string Encode(string saveData)
        {
            if (string.IsNullOrEmpty(saveData))
            {
                UnityEngine.Debug.LogError("[Obfuscator] Cannod encode null data.");
                return string.Empty;
            }

            return ScrambleXOR(saveData);
        }

        // Decodes the obfuscated string read from the save file back into the
        // the original data string (Dataprotector will then verify it).
        public static string Decode(string encodedData)
        {
            if (string.IsNullOrEmpty(encodedData))
            {
                return string.Empty;
            }

            byte[] dataBytes;
            try
            {
                // Step 1. Convert the Base64 string back into the raw scrambled bytes.
                dataBytes = Convert.FromBase64String(encodedData);
            }
            catch (FormatException)
            {
                // This indicates the attacker may have modified the Base64 portion.
                UnityEngine.Debug.LogError("[Obfuscator] Failed to decode Base64. Data is corrupted or tampered.");
                return string.Empty;
            }

            // Step 2. Convert the bytes back to a string, then apply the XOR again to reverse the cipher.
            string dataAsString = Encoding.UTF8.GetString(dataBytes);

            return ScrambleXOR(dataAsString);
        }
// What images to put here?
// [Image of the XOR logic gate]
    }
}