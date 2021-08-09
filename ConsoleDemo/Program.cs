using System;
﻿using System.IO;
using System.Drawing;
using System.Windows.Forms; 
using DominantColor;
using Color = System.Drawing.Color;
using Image = System.Drawing.Image;

namespace ConsoleDemo {
    class Program {
        [STAThread]
        static int Main(string[] args) {
            if (args.Length >= 1) {
                return btnBrowse_Click(args);
            } else {
                return btnBrowse_Click(null);
            }
        }

        private static int btnBrowse_Click(string[] inputfilenames) {
            int returnCode = 0;
            string[] files;
            if (inputfilenames == null) {
                OpenFileDialog dialog = new OpenFileDialog(); 
                dialog.Filter = "All Supported Files (*.png;*.jpg;*.jpeg;*.bmp;*.gif;*.exif;*.tiff)|*.png;*.jpg;*.jpeg;*.gif|PNG Files (*.png)|*.png|JPEG Files (*.jpg;*.jpeg)|*.jpg;*.jpeg|Bitmap Images (*.bmp)|*.bmp|GIF Files (*.gif)|*.gif|EXIF Files (*.exif)|*.exif|TIFF Files (*.tiff)|*.tiff|All Files (*.*)|*.*";
                dialog.Multiselect = true;
                dialog.CheckFileExists = true;
                if (dialog.ShowDialog() == DialogResult.OK) {
                    files = dialog.FileNames;
                } else {
                    return 0;
                }
            } else {
                files = inputfilenames;
            }
            foreach (string file in files) {
                if (!File.Exists(file)) {
                    Console.Error.WriteLine("[Error] File does not exist: " + file);
                    returnCode = 1;
                } else {
                   Console.WriteLine("File: " + file);
                   Bitmap img = new Bitmap(file);
                   ProcessImage(img);
                }
            }
            return returnCode;
        }

        private static void ProcessImage(Bitmap bmp) {
            DominantRGBColorCalculator rgbColorCalculator = new DominantRGBColorCalculator();
            DominantHueColorCalculator hueColorCalculator = new DominantHueColorCalculator();
            Color rgbColor = rgbColorCalculator.CalculateDominantColor(bmp);
            Color hueColor = hueColorCalculator.CalculateDominantColor(bmp);

            Console.WriteLine("RGB: " + rgbColor.ToString());
            Console.WriteLine("Hue: " + hueColor.ToString());
        }
    }
}
