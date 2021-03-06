﻿using System;
using System.IO;
using DCP.Geosupport.DotNet.GeoX;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.PostedFile != null)
            {

                int counter = 1;
                Dictionary<int, Dictionary<string, string>> PageDictionary = new Dictionary<int, Dictionary<string, string>>();
                Dictionary<string, string> LineDictionary;
                string line;
                System.IO.StreamReader file =
                    new System.IO.StreamReader(FileUpload1.PostedFile.FileName);
                string filler = "";
                while ((line = file.ReadLine()) != null)
                {
                    LineDictionary = new Dictionary<string, string>();
                    LineDictionary["borough"] = line.Substring(0, 1);
                    LineDictionary["hn1"] = line.Substring(1, 11);
                    LineDictionary["hn2"] = line.Substring(13, 11);
                    LineDictionary["street"] = line.Substring(25, 31);
                    LineDictionary["district"] = line.Substring(57, 2);
                    LineDictionary["subsection"] = line.Substring(60, 2);
                    LineDictionary["filler"] = filler.PadRight(212, ' ');
                    PageDictionary[counter] = LineDictionary;
                    counter++;
                }

                counter = 1;
                Wa1 mywa1;
                geo mygeo;
                //Wa2F5 mywa2f5;
                Wa2F1ax mywa2f1ax;

                string ElevenSpaces = "           ";

                foreach (KeyValuePair<int,Dictionary<string,string>> entry in PageDictionary)
                {

                    mywa1 = new Wa1();
                    mygeo = new geo();
                    mywa2f1ax = new Wa2F1ax();

                    mywa1.in_boro1 = PageDictionary[counter]["borough"];
                    mywa1.in_hnd  = PageDictionary[counter]["hn1"];
                    mywa1.in_low_hnd = PageDictionary[counter]["hn2"];
                    mywa1.in_stname1 = PageDictionary[counter]["street"];

                    mywa1.in_func_code = "1A";
                    mywa1.in_platform_ind = "C";
                    mywa1.in_mode_switch = "X";

                    mygeo.GeoCall(ref mywa1, ref mywa2f1ax);

                    string strPath = Environment.GetFolderPath(
                         System.Environment.SpecialFolder.DesktopDirectory);

                    if (mywa1.out_grc == "00" || mywa1.out_grc == "01") {

                        string newLine = mywa2f1ax.gridkey1.ToString() + ElevenSpaces + PageDictionary[counter]["district"] + PageDictionary[counter]["subsection"] + PageDictionary[counter]["filler"];

                        string text = Environment.NewLine + newLine;

                        System.IO.File.AppendAllText(strPath + "\\results.txt", text);

                        counter++;
                    } else {
                        string text = Environment.NewLine + PageDictionary[counter]["borough"] + " " + PageDictionary[counter]["hn1"]  + PageDictionary[counter]["street"] + mywa1.out_error_message;
                        System.IO.File.AppendAllText(strPath + "\\errors.txt", text);
                    }
                     
                }

                file.Close();

            }
        }
    }
}