/* 
 * CSE 3310 || UTA Fall 2025 - Group 13
 * Project: THE WORLD HAS ENDED
 *
 * Check for Game Logic Integrity
 * Threat: Code and Asset Tampering
 * Response: Game will immediately terminate.
 * Purpose: MMonitors the execution environment for signs of tampering on Android.
 *          Checks for managed debuggers, breakpoints, and rooted environments using heuristics. 
 */

using UnityEngine;
using System;
using System.Security.Cryptography;  // Needed for hashing (if implemented fully)
using System.Text;                   // Needed for hash conversions.
using System.IO;                     // Needed for reading assets/files.

namespace Assets.Scripts.Security
{
    /* To-Do-List:
     * 1. Understand floating point drift
     * 2. ...
     * 3. Figure out CheckGameLogicSanity implementation
     */


    public class Integrity_Check : MonoBehaviour
    {
        // --- Configuration ---   
        private const float CheckInterval = 3.0f;   // Frequency (s) to check integrity.
        private float nextCheckTime;                // Next scheduled check time.

        private int violationCounter = 0;
        private const int MaxViolations = 5;        // Higher threshold since logic checks can have minor floating point drift. <-- look up to understand better

        // --- HARDCODED Integrity Values (Code/Asset Tampering) ---
        // A known SHA256 hash of a critical, non-changing file (e.g., the main executable).
        // This MUST be pre-calculated and hardcoded after the final build. <-- why?
        private const string ExpectedAssetHash = "4A63B9E2C6A9280F810C17C0E185D982D4F0745C7B07E92A2814F2AC5C4217E6";


        // --- Unity Livecycle (Start/Update) ---

        void Start()
        {
            PerformCheck();
            nextCheckTime = Time.time + CheckInterval;
        }

        void Update()
        {
            if (Time.time >= nextCheckTime)
            {
                PerformCheck();
                nextCheckTime = Time.time + CheckInterval;
            }
        }

        // --- Core Check Logic ---
        private void PerformCheck()
        {
            // Step 1. Logic Integrity Check (MOST IMPORTANT for catching runtime hacks)
            /*
            if (CheckGameLogicSanity())
            {
                LogViolation("Detected illogical runtime state (speed, damage, etc.).");
            }
            */

            // Step 2. Code/Asset Tampering Check (Catches file modification)
            if (CheckAssetIntegrity())
            {
                LogViolation("Critical asset/code hash mismatch detected.");
            }
        }

        // Checks if critical game variables are within "sane" and expected ranges.
        // This directly combats speed hacks, god mode, and resource cheats.
        /*
        private bool CheckGameLogicSanity()
        {
            // Figure out what exactly needs to be checked as game build progresses
        }
        */

        // Verifies the integrity of a critical file (code assembly or asset bundle)
        // by comparing its runtime hash with a hardcoded expected hash.
        private bool CheckAssetIntegrity()
        {
            string targetFilePath = Application.dataPath + "/Managed/Assembly-CSharp.ddl"; // Example target

            // Step 1. Does the file even exist? (Deletion is also tampering)
            if(!File.Exists(targetFilePath))
            {
                return true; 
            }

            // Step 2. Hash comparison
            string runtimeHash = CalculateFileHash(targetFilePath);


            // Step 3. Compare the calculated hash with the expected hash.
            if (runtimeHash != ExpectedAssetHash)
            {
                UnityEngine.Debug.LogWarning($"[Asset Fail] Hash mismatch! Expected: {ExpectedAssetHash}, Actual: {runtimeHash}");
                return true;
            }

            return false; // Hash matches -> No tampering detected!
        }

        // Helper function to calculate the SHA256 hash of a file.
        private string CalculateFileHash(string filePath)
        {
            try
            {
                using (var sha256 = SHA256.Create())
                using (var stream = File.OpenRead(filePath))
                {
                    byte[] hashBytes = sha256.ComputeHash(stream);

                    // Convert the byte array to a hexadecimal string.
                    var sb = new StringBuilder();
                    foreach (byte b in hashBytes)
                    {
                        // "x2" means format as hexadecimal, padding with leading zero if needed.
                        sb.Append(b.ToString("x2"));
                    }

                    return sb.ToString().ToUpper(); // Return uppercase for consistency.
                }
            }

            catch (Exception ex)
            {
                // File access errors often occur on Andoid due to permissions or APK packaging.
                UnityEngine.Debug.LogError($"[Hashing Error] Could not calcualte hash for {filePath}: {ex.Message}");
                // If we can't read the file, it's safer to flag it as a potential violation.
                return "ERROR_READING_FILE";
            }
        }
        
        // --- Response Handling ---

        private void LogViolation(string message)
        {
            violationCounter++;
            UnityEngine.Debug.LogError($"[INTEGRITY VIOLATION] {message} - Count: {ciolationCounter}");

            if (violationCounter >= MaxViolations)
            {
                TriggerSevereResponse();
            }
        }

        private void TriggerSevereResponse()
        {
            // For a real game:
            // 1. Immediately corrupt the player's currency/score on the server.
            // 2. Queue a permanent game ban request.
            // 3. Apply permanent, sublte negative modifiers to join the player's character.

            UnityEngine.Debug.LogAssertion("[INTEGRITY FAIL] Max violations reached. Shutting down.");
            Application.Quit();
        }
    }
}