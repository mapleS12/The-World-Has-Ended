/* 
 * CSE 3310 || UTA Fall 2025 - Group 13
 * Project: THE WORLD HAS ENDED
 *
 * Check Save File Hash
 * Threat: Save File Tampering
 * Response: Game will reject the file or reset the player's data to prevent cheating.
 * Purpose: Provides a cryptographic integrity layer for player save data. It utilizes SHA256 hashing,
 *          combined with a secret salt, to generate an unforgeable hash of the saved payload.Its sole
 *          purpose is to detect and prevent save file tampering by ensuring that the data loaded matches 
 *          the exact data that was originally saved by the game's logic.
 */

using UnityEngine;                   // Needed for JsonUtility and Debug.LogError.
using System;                        // Needed for basic system functions.
using System.Security.Cryptography;  // Needed for hashing (if implemented fully).
using System.Text;                   // Needed for hash conversions.

namespace Scripts.Security.Save_File_Check
{
    public class Save_File_Check : MonoBehaviour
    {
        // --- Inner Data Structure ---
        [Serializable]
        public class ProtectedData
        {
            // The actual player data, serialized as a string (JSON, XML, etc.)
            public string DataPayload;

            // The cryptographic hash of the DataPayload + Salt
            public string IntegrityHash;
        }

        // --- Hash Configuriation ---
        // A "salt" or key to make the hash unique, even if the payload is simple.
        // CHANGE THIS VALUE IN FINAL VUILD! Never leave it as defauly.
        private const string HashingSalt = "YourGameSecretKey!12345"; 

        // Calculates the SHA256 hash of the input string concatenated with the secret salt.
        private static string CalculateHash(string input)
        {
            // Append the secret salt to the input before hashing.
            // Prevents attackers from simply generating a matching hash for their corrupted data.
            string saltedInput = input + HashingSalt;

            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert the input string into a byte array.
                byte[] inputBytes = Encoding.UTF8.GetBytes(saltedInput);

                // Compute the hash.
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                // Convert the byte array to a hexadecimal string for storage.
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        // Takes a clean data string, calculates its hash, and wraps it in a protected object.
        public static string Protect(string cleanData)
        {
            // Step 1. Calculate the hash of the clean data + secret salt.
            string hash = CalculateHash(cleanData);

            // Step 2. Create the wrapper object.
            ProtectedData wrapper = new ProtectedData
            {
                DataPayload = cleanData,
                IntegrityHash = hash
            };

            // Step 3. Serialize the wrapper to a string (usually JSON) for disk storage.
            // In a real Unity app, this might be JsonUtility.ToJson(wrapper)
            return JsonUtility.ToJson(wrapper);
        } 

        // Attempts to load and verify data from a save file.
        public static string Verify(string saveDataJson)
        {
            if (string.IsNullOrEmpty(saveDataJson))
            {
                UnityEngine.Debug.LogWarning("[DataProtect] Attempted to verify empty save data.");
                return null;
            }

            // Step 1. Deserialize the wrapper from the save file.
            ProtectedData loadedData;
            try
            {
                loadedData = JsonUtility.FromJson<ProtectedData>(saveDataJson);
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError($"[DataProtect] Deserialization failed. Save file likely corrupted: {ex.Message}");
                return null;
            }

            // Step 2. Calculate the hash of the loaded payload at runtime.
            string runtimeHash = CalculateHash(loadedData.DataPayload);

            // Step 3. CRITICAL VERIFICATION STEP:
            if (runtimeHash != loadedData.IntegrityHash)
            {
                // *** TAMPERING DETECTED ***
                UnityEngine.Debug.LogError("[DataProtect] HASH MISMATCH! Save file tampering detected.");

                // You would log this event to a server here.
                return null; // Return null to force the game to start a fresh file or apply penalties.
            }

            // Step 4. Success: Return the original clean data.
            return loadedData.DataPayload;
        }
    }   
}