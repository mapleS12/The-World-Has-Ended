/* 
 * CSE 3310 || UTA Fall 2025 - Group 13
 * Project: THE WORLD HAS ENDED
 *
 * Check for Debugger and Root
 * Threat: Reverse Engineerings and Malicious Environment
 * Response: Game will immediately terminate.
 * Purpose: Monitors the runtime execution environment for signs of external tampering, including the presence 
 *          of managed debuggers, runtime breakpoints (via execution timing analysis), and heuristic indicators
 *          of a rooted or jailbroken operating system. Its primary role is to ensure the code is running in a 
 *          hostile-free, unmodified environment.
 */

using UnityEngine;                        // 
using System;                             //
using System.Diagnostics;                 // Access to debugger and stopwatch class.
using System.IO;
using System.Runtime.CompilerServices;    // For file system checks on mobile device.


// Purpose of namespace: Group related security monitoring classes together.
namespace Assets.Scripts.Security
{
    // Adding Monobehavior means this class can be attached to a GameObject in Unity and run in the game loop.
    public class Debugger_Root_Check : MonoBehaviour
    {
        // Purpose of configuration: ???
        // --- Configuration ---
        private const float CheckInterval = 5.0f;   // The frequency (seconds) to check the environment.
        private float nextCheckTime;                // Checking too often can hurt performance, too rarely can allow exploitation.
        
        // --- Detection Threshold ---
        // Instead of reacting instantly, a simple counter allows to help rpevent false positives and makes it harder
        // for the attacker to isolate the exact moment of detection.
        private int violationCounter = 0;           // Simple counter to track detected anomolies.
        private const int MaxViolations = 3;        //A high count can trigger a severe response (e.g., game shutdown, data corruption).

        // Function Prototypes
        /* To-Do-List:
         * 1. Create Tests for this
         * 2. Figure out how to do prototypes
         * 3. Simplify and add more code for understanding
         * 4. Add JNI-based root detection for Android (advanced)
         * 5. Purpose of configuration>
         */



        // Initial security check upon starting the game.
        void Start()
        {
            PerformCheck();
            nextCheckTime = Time.time + CheckInterval;
        }

        // Check if the scheduled time has arrived.
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
            // Step 1. Managed Debugger Checlk (catches tools like Android Studio debugger).
            if (CheckManagedDebugger())
            {
                LogViolation("Managed Debugger Detected.");
            }

            // Step 2. Root Check (Heuristic search for system files)
            #if UNITY_ANDROID // Only run when the game is built for Android
            if ((CheckRootORJailbreak())
            {
                LogViolation("Root Environment Detected.");
            }
            #endif

            // Step 3. Time-Based Delay Check (Catches human-induced breakpoints)
            if (CheckExecutionDelay())
            {
                LogViolation("Excessive Execution Delay Detected (Possible Breakpoint).");
            }
        }

        // --- Specific Check Methods

        // Checks for managed debuggers (Like VS or dnSpy) using the standard .NET framework
        private bool CheckManagedDebugger()
        {
            // Checks if a managed debugger is hooked into the C# runtime.
            return Debugger.IsAttached;
        }

        // Measures the time taken to execute a small sequence.
        // An unusually long delay (> 50ms) suggests a human-induced breakpoint was hit.
        private bool CheckExecutionDelay()
        {
            // Step 1. Get a high-resolution timestamp before the operation
            long startTime = Stopwatch.GetTimestamp();

            // Step 2. Execute a simple, non-optimizable operation (to force CPU usage)
            long sum = 0;
            for (int i = 0; i < 1000; i++)
            {
                sum += 1;
            }

            // Step 3. Get the timestamp after the operation.
            long endTime = Stopwatch.GetTimestamp();
            long elapsedTicks = endTime - startTime;

            // Step 4. Convert high-resolution ticks to milliseconds.
            double elapsedMs = (double)elapsedTicks / Stopwatch.Frequency * 1000.0;

            // If the time exceeds a safe threshold (50ms)
            return elapsedMs > 50.0;
        }

        // Performs heuristic checks to see if the device is rooted (Android).
        // THis is a basic C# file-based check. For production, you need JNI for deep inspection.
        private bool CheckRootOrJailbreak()
        {
            // Only execute this logic on Andoid builds
            #if UNITY_ANDROID
            string[] knownRootPaths = new string[]
            {
                // Common paths for Superusr, busybox, and root management applications
                "/system/app/Superuser.apk", // Common Superuser app paths
                "/system/bin/su",            // Common su binary path  
                "/sbin/su",                  // Another common su binary path
                "/etc/superuser.conf",       // Superuser configuration file
                "/data/local/tmp/su",        // Temporary su binary location
                "/system/xbin/ku.sud",       // Another su binary variant
                "/data/local/bin/su",        // Another su binary location
                "/data/local/xbin/su"        // Another su binary location
            };

            foreach (string path in knownRootPaths)
            {
                // Performs the file system checks
                if (System.IO.File.Exists(path))
                {
                    return true; // Root indicator found
                }
            }
            #endif
            return false;
        }

        // --- Response Handling ---

        // Increments the violation counter and handles the response.
        private void LogViolation(string message)
        {
            violationCounter++;
            UnityEngine.Debug.LogError($"[SECURITY VIOLATION] {message} - Count: {violationCounter}");

            if (violationCounter >= MaxViolations)
            {
                TriggerMildResponse();
            }
        }

        private void TriggerMildResponse()
        {
           // *** BEST PRACTICE: Implement a subtle, delated, and confusing response. ***

           // Example of a subtle response:
           // FindObjectOfType<GameManager>().CorruptPlayerData(violationCounter);

           // For immediate testing purposes, we use Application.Quit();
           UnityEngine.Debug.LogAssertion("[SECURITY FAIL] Max violations reached. Shutting down.");
           Application.Quit();
        }
    }
}