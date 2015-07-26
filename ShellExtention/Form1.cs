using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShellExtention {
    public partial class Form1 : Form {

        private string silentApkRenaming_executableName = "Silent APK Renaming.exe";
        private string aapt_executableName = "aapt.exe";

        private string toolsFolderName = "tools";
        private string appDataFolderName = "APK Renaming";

        private string appDataPath = Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData );

        public Form1 () {
            InitializeComponent();
        }

        private void btnSetOption_Click ( object sender, EventArgs e ) {
            // Check if the appdata folder for this application exist
            if ( Directory.Exists( Path.Combine( appDataPath, appDataFolderName ) ) ) {
                // Delete the appdata application folder
                Directory.Delete( Path.Combine( appDataPath, appDataFolderName ), true );
            }

            // Create the appdata application folder
            Directory.CreateDirectory( Path.Combine( appDataPath, appDataFolderName ) );

            // Copy the silent apk renaming application to the appdata application folder
            File.Copy( Path.Combine( toolsFolderName, silentApkRenaming_executableName ),
                            Path.Combine( Path.Combine( appDataPath, appDataFolderName ),
                                                silentApkRenaming_executableName ), true );

            // Copy the aapt tool to the appdata application folder
            File.Copy( Path.Combine( toolsFolderName, aapt_executableName ),
                            Path.Combine( Path.Combine( appDataPath, appDataFolderName ),
                                                aapt_executableName ), true );

            // Create the Registry
            RegistryKey key = Registry.ClassesRoot;
            if ( ( key = key.OpenSubKey( ".apk", true ) ) == null ) {
                key = Registry.ClassesRoot;
                key = key.CreateSubKey( ".apk" );
            }

            if ( ( key = key.OpenSubKey( "shell", true ) ) == null ) {
                key = Registry.ClassesRoot;
                key = key.OpenSubKey( ".apk", true );
                key = key.CreateSubKey( "shell" );
            }

            if ( ( key = key.OpenSubKey( "Rename APK with App Name", true ) ) == null ) {
                key = Registry.ClassesRoot;
                key = key.OpenSubKey( ".apk", true );
                key = key.OpenSubKey( "shell", true );
                key = key.CreateSubKey( "Rename APK with App Name" );
            }

            if ( ( key = key.OpenSubKey( "command", true ) ) == null ) {
                key = Registry.ClassesRoot;
                key = key.OpenSubKey( ".apk", true );
                key = key.OpenSubKey( "shell", true );
                key = key.OpenSubKey( "Rename APK with App Name", true );
                key = key.CreateSubKey( "command" );
            }

            key.SetValue( "", "\"" + Path.Combine( Path.Combine( appDataPath, appDataFolderName ),
                                            silentApkRenaming_executableName ) + "\"" + " \"%1\" -N" );

            key = Registry.ClassesRoot;
            key = key.OpenSubKey( ".apk", true );
            key = key.OpenSubKey( "shell", true );

            if ( ( key = key.OpenSubKey( "Rename APK with Package Name", true ) ) == null ) {
                key = Registry.ClassesRoot;
                key = key.OpenSubKey( ".apk", true );
                key = key.OpenSubKey( "shell", true );
                key = key.CreateSubKey( "Rename APK with Package Name" );
            }

            if ( ( key = key.OpenSubKey( "command", true ) ) == null ) {
                key = Registry.ClassesRoot;
                key = key.OpenSubKey( ".apk", true );
                key = key.OpenSubKey( "shell", true );
                key = key.OpenSubKey( "Rename APK with Package Name", true );
                key = key.CreateSubKey( "command" );
            }

            key.SetValue( "", "\"" + Path.Combine( Path.Combine( appDataPath, appDataFolderName ),
                                            silentApkRenaming_executableName ) + "\"" + " \"%1\" -P" );
            key.Close();

        }

        private void button3_Click ( object sender, EventArgs e ) {
            Environment.Exit( Environment.ExitCode );
        }

        private void btnRemoveOption_Click ( object sender, EventArgs e ) {
            RegistryKey key = Registry.ClassesRoot;
            key = key.OpenSubKey( ".apk", true );
            key.DeleteSubKeyTree( "shell" );
            key.Close();
        }





    }
}
