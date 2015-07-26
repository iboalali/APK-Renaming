using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent_APK_Renaming {
    class Program {

        private static string AppName { get; set; }
        private static string PackageName { get; set; }
        private static string VersionCode { get; set; }
        private static string MinSDK { get; set; }

        private static string command = "aapt.exe dump badging ";
        private static string apkPath = string.Empty;
        private static string renameMode = string.Empty;
        private static string result = string.Empty;

        private static string appDataPath = Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData );
        private static string silentApkRenaming_executableName = "Silent APK Renaming.exe";
        private static string aapt_executableName = "aapt.exe";

        private static string apk = @"C:\Users\Ibrahim\Documents\Visual Studio 2013\Projects\APK Renaming\app-release.apk";

        static void Main ( string[] args ) {

            Console.WriteLine( "Number of Argument: " + args.Length.ToString() );

            if ( args.Length == 0 ) {
                args = new string[]{
                    apk,
                    "P"
                };
            }

            foreach ( var item in args ) {
                Console.WriteLine( item );
            }

            for ( int i = 0; i < args.Length; i++ ) {
                if ( args[i][0] == '-' ) {
                    args[i] = args[i].Substring( 1 );
                }

            }

            foreach ( var item in args ) {
                Console.WriteLine( item );
            }

            apkPath = args[0];
            renameMode = args[1];

            Console.WriteLine( "\nPress Any Key to Continue" );
            Console.ReadLine();

            string com = String.Format( command + "\"{0}\"", apkPath );
            Console.WriteLine( "Command: " + com );

            result = ExecuteCommandSync( com );

            if ( result == null ) {
                Console.WriteLine( "No Result, result == null" );
                Console.WriteLine( "\nPress Any Key to Continue" );
                Console.ReadLine();
                return;
            }

            if ( result == "" ) {
                Console.WriteLine( "No Result, result == \"\"" );
                Console.WriteLine( "\nPress Any Key to Continue" );
                Console.ReadLine();
                return;
            }

            AppName = getAppName( result );
            PackageName = getAppPackageName( result );
            VersionCode = getAppVersionCode( result );
            MinSDK = getMinSDKVersion( result );

            Console.WriteLine();
            Console.WriteLine( "AppName: " + AppName );
            Console.WriteLine( "PackageName: " + PackageName );
            Console.WriteLine( "VersionCode: " + VersionCode );
            Console.WriteLine( "MinSDK: " + MinSDK );

            Console.WriteLine( "\nPress Any Key to Continue" );
            Console.ReadLine();

            if ( renameMode == "P" ) {

            } else if ( renameMode == "N" ) {

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
                    new System.Diagnostics.ProcessStartInfo( "cmd", "/c " + command );

                // The following commands are needed to redirect the standard output.
                // This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                // Do not create the black window.
                procStartInfo.CreateNoWindow = true;
                // Now we create a process, assign its ProcessStartInfo and start it
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                // Get the output into a string
                return proc.StandardOutput.ReadToEnd();
            } catch ( Exception objException ) {
                // Log the exception
                Console.WriteLine( objException.Message );
                return null;
            }
        }

        private static string getAppName ( string result ) {
            string appNameLabel = "application-label:";
            int index = result.IndexOf( appNameLabel ) + appNameLabel.Length + 1;
            int indexNewLine = result.IndexOf( Environment.NewLine, index );
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


    }
}
