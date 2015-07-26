using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Silent_APK_Renaming {
    class Program {

        [DllImport( "kernel32.dll" )]
        private static extern IntPtr GetConsoleWindow ();

        [DllImport( "user32.dll" )]
        private static extern bool ShowWindow ( IntPtr hWnd, int nCmdShow );

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;
        private const int SW_SHOWMINIMIZED = 2;

        private static string AppName { get; set; }
        private static string PackageName { get; set; }
        private static string VersionCode { get; set; }
        private static string MinSDK { get; set; }
        private static string VersionName { get; set; }

        //private static string command = "aapt.exe dump badging ";
        private static string command2 = @"C:\Users\Ibrahim\AppData\Roaming\APK Renaming\aapt.exe";
        private static string command2parameters = " dump badging ";
        private static string apkPath = string.Empty;
        private static string renameMode = string.Empty;
        private static string result = string.Empty;

        private static string appDataPath = Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData );
        //private static string silentApkRenaming_executableName = "Silent APK Renaming.exe";
        //private static string aapt_executableName = "aapt.exe";

        //private static string apk = @"C:\Users\Ibrahim\Documents\Visual Studio 2013\Projects\APK Renaming\app-release.apk";

        static void Main ( string[] args ) {
            var handle = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;

            // Minimize Console
            ShowWindow( handle, SW_SHOWMINIMIZED | SW_HIDE );

            for ( int i = 0; i < args.Length; i++ ) {
                if ( args[i][0] == '-' ) {
                    args[i] = args[i].Substring( 1 );
                }

            }

            apkPath = args[0];
            renameMode = args[1];

            string parameters = command2parameters + "\"" + apkPath + "\"" + " " + "-" + renameMode;

            result = ExecuteCommandSync( command2, parameters );

            if ( result == null ) {
                return;
            }

            if ( result == "" ) {
                return;
            }

            AppName = getAppName( result );
            PackageName = getAppPackageName( result );
            VersionCode = getAppVersionCode( result );
            MinSDK = getMinSDKVersion( result );
            VersionName = getAppVersionName( result );

            string name = string.Empty;

            if ( renameMode == "P" ) {
                name = PackageName + "-"
                    + VersionName + "-"
                    + VersionCode + "-"
                    + "minAPI" + MinSDK
                    + ".apk";
            } else if ( renameMode == "N" ) {
                name = AppName + "-"
                    + VersionName + "-"
                    + VersionCode + "-"
                    + "minAPI" + MinSDK
                    + ".apk";

            }

            string path = apkPath.Substring( 0, apkPath.LastIndexOf( '\\' ) );


            if ( name != string.Empty ) {
                File.Move( apkPath, Path.Combine( path, name ) );
            }


        }

        // http://www.codeproject.com/Articles/25983/How-to-Execute-a-Command-in-C
        /// <summary>
        /// Executes a shell command synchronously.
        /// </summary>
        /// <param name="command">string command</param>
        /// <returns>string, as output of the command</returns>
        private static string ExecuteCommandSync ( string command ) {
            try {
                // create the ProcessStartInfo using "cmd" as the program to be run,
                // and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows,
                // and then exit.
                System.Diagnostics.ProcessStartInfo procStartInfo =
//new System.Diagnostics.ProcessStartInfo( "cmd", "/c " + command );
                    new System.Diagnostics.ProcessStartInfo( "cmd", command );


                // The following commands are needed to redirect the standard output.
                // This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                // Do not create the black window.
                //procStartInfo.CreateNoWindow = true;
                // Now we create a process, assign its ProcessStartInfo and start it
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();

                string s = proc.StandardOutput.ReadToEnd();
                Console.WriteLine( "Result: " );
                Console.WriteLine( s );
                proc.WaitForExit();
                // Get the output into a string
                return s;
            } catch ( Exception objException ) {
                // Log the exception
                Console.WriteLine( objException.Message );
                return null;
            }
        }

        private static string ExecuteCommandSync ( string command, string parameters ) {
            try {
                // create the ProcessStartInfo using "cmd" as the program to be run,
                // and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows,
                // and then exit.
                System.Diagnostics.ProcessStartInfo procStartInfo =
//new System.Diagnostics.ProcessStartInfo( "cmd", "/c " + command );
                    new System.Diagnostics.ProcessStartInfo( command, parameters );


                // The following commands are needed to redirect the standard output.
                // This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                // Do not create the black window.
                //procStartInfo.CreateNoWindow = true;
                // Now we create a process, assign its ProcessStartInfo and start it
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();

                string s = proc.StandardOutput.ReadToEnd();
                //Console.WriteLine( "Result: " );
                //Console.WriteLine( s );
                proc.WaitForExit();
                // Get the output into a string
                return s;
            } catch ( Exception objException ) {
                // Log the exception
                Console.WriteLine( objException.Message );
                return null;
            }
        }

        private static string getAppName ( string result ) {
            string appNameLabel = "application-label:";
            int index = result.IndexOf( appNameLabel ) + appNameLabel.Length + 1;
            int indexNewLine = result.IndexOf( '\'', index );
            string appName = result.Substring( index, indexNewLine - index );
            return appName;

        }

        private static string getAppPackageName ( string result ) {
            string appPackageNameLabel = "name=";
            int index = result.IndexOf( appPackageNameLabel ) + appPackageNameLabel.Length + 1;
            int indexQoute = result.IndexOf( '\'', index );
            string appPackageName = result.Substring( index, indexQoute - index );
            return appPackageName;

        }

        private static string getAppVersionCode ( string result ) {
            string appVersionCodeLabel = "versionCode=";
            int index = result.IndexOf( appVersionCodeLabel ) + appVersionCodeLabel.Length + 1;
            int indexQoute = result.IndexOf( '\'', index );
            string appVersionCode = result.Substring( index, indexQoute - index );
            return appVersionCode;

        }

        private static string getMinSDKVersion ( string result ) {
            string appMinSDKVersionLabel = "sdkVersion:";
            int index = result.IndexOf( appMinSDKVersionLabel ) + appMinSDKVersionLabel.Length + 1;
            int indexQoute = result.IndexOf( '\'', index );
            string appMinSDKVersion = result.Substring( index, indexQoute - index );
            return appMinSDKVersion;
        }

        private static string getAppVersionName ( string result ) {
            string appVersionNameLabel = "versionName=";
            int index = result.IndexOf( appVersionNameLabel ) + appVersionNameLabel.Length + 1;
            int indexQoute = result.IndexOf( '\'', index );
            string appVersionName = result.Substring( index, indexQoute - index );
            return appVersionName;
        }


    }
}
