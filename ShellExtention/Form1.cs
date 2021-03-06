﻿using Microsoft.Win32;
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
        private string silentApkRenaming_configFile = "Silent APK Renaming.exe.config";
        private string aapt_executableName = "aapt.exe";
        private string iconFileName = "icon2.ico";

        private string toolsFolderName = "tools";
        private string appDataFolderName = "APK Renaming";

        private string appDataPath = Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData );

        public Form1 () {
            InitializeComponent();
            this.Icon = global::ShellExtention.Properties.Resources.icon2;
        }

        private void btnSetOption_Click ( object sender, EventArgs e ) {
            try {
                // Check if the appdata folder for this application exist
                if ( Directory.Exists( Path.Combine( appDataPath, appDataFolderName ) ) ) {
                    // Delete the appdata application folder
                    Directory.Delete( Path.Combine( appDataPath, appDataFolderName ), true );
                }
            } catch ( IOException ioex ) {
                DialogResult r = MessageBox.Show( ioex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error );
                if ( r == System.Windows.Forms.DialogResult.Retry ) {
                    btnSetOption_Click( sender, e );
                }
                return;
            } catch ( Exception ex ) {
                MessageBox.Show( ex.Message, "Error" );
                return;
            }


            try {
                // Create the appdata application folder
                Directory.CreateDirectory( Path.Combine( appDataPath, appDataFolderName ) );
            } catch ( IOException ioex ) {
                DialogResult r = MessageBox.Show( ioex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error );
                if ( r == System.Windows.Forms.DialogResult.Retry ) {
                    btnSetOption_Click( sender, e );
                }
                return;
            } catch ( Exception ex ) {
                MessageBox.Show( ex.Message, "Error" );
                return;
            }


            try {
                // Copy the silent apk renaming application to the appdata application folder
                File.Copy( Path.Combine( toolsFolderName, silentApkRenaming_executableName ),
                                Path.Combine( Path.Combine( appDataPath, appDataFolderName ),
                                                    silentApkRenaming_executableName ), true );
            } catch ( IOException ioex ) {
                DialogResult r = MessageBox.Show( ioex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error );
                if ( r == System.Windows.Forms.DialogResult.Retry ) {
                    btnSetOption_Click( sender, e );
                }
                return;
            } catch ( Exception ex ) {
                MessageBox.Show( ex.Message, "Error" );
                return;
            }

            try {
                // Copy the silent apk renaming application config file to the appdata application folder
                File.Copy( Path.Combine( toolsFolderName, silentApkRenaming_configFile ),
                                Path.Combine( Path.Combine( appDataPath, appDataFolderName ),
                                                    silentApkRenaming_configFile ), true );
            } catch ( IOException ioex ) {
                DialogResult r = MessageBox.Show( ioex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error );
                if ( r == System.Windows.Forms.DialogResult.Retry ) {
                    btnSetOption_Click( sender, e );
                }
                return;
            } catch ( Exception ex ) {
                MessageBox.Show( ex.Message, "Error" );
                return;
            }

            try {

            } catch ( IOException ioex ) {
                DialogResult r = MessageBox.Show( ioex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error );
                if ( r == System.Windows.Forms.DialogResult.Retry ) {
                    btnSetOption_Click( sender, e );
                }
                return;
            } catch ( Exception ex ) {
                MessageBox.Show( ex.Message, "Error" );
                return;
            }


            try {
                // Copy the aapt tool to the appdata application folder
                File.Copy( Path.Combine( toolsFolderName, aapt_executableName ),
                                Path.Combine( Path.Combine( appDataPath, appDataFolderName ),
                                                    aapt_executableName ), true );
            } catch ( IOException ioex ) {
                DialogResult r = MessageBox.Show( ioex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error );
                if ( r == System.Windows.Forms.DialogResult.Retry ) {
                    btnSetOption_Click( sender, e );
                }
                return;
            } catch ( Exception ex ) {
                MessageBox.Show( ex.Message, "Error" );
                return;
            }

            try {
                File.Copy( Path.Combine( toolsFolderName, iconFileName ),
                                            Path.Combine( Path.Combine( appDataPath, appDataFolderName ),
                                                                iconFileName ), true );
            } catch ( IOException ioex ) {
                DialogResult r = MessageBox.Show( ioex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error );
                if ( r == System.Windows.Forms.DialogResult.Retry ) {
                    btnSetOption_Click( sender, e );
                }
                return;
            } catch ( Exception ex ) {
                MessageBox.Show( ex.Message ,"Error"  );
                return;
            }


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

            key.SetValue( "icon", Path.Combine( Path.Combine( appDataPath, appDataFolderName ), iconFileName ) );

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

            key.SetValue( "icon", Path.Combine( Path.Combine( appDataPath, appDataFolderName ), iconFileName ) );

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
