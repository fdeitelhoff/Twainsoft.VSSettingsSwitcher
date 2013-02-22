using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twainsoft.VSSettingsSwitcher.BLL.Options.Base;
using Twainsoft.VSSettingsSwitcher.DAL.Options.VSSettings.Manage;
using System.IO;
using System.Data;
using System.Xml;

namespace Twainsoft.VSSettingsSwitcher.BLL.Options.VSSettings.Manage
{
    public class ManageOptionsStore : BaseOptionsStore
    {
        public ManageDataSet ManageDataSet { get; private set; }
        private string DataPath { get; set; }

        public ManageOptionsStore()
        {
            ManageDataSet = new ManageDataSet();

            OptionsPage = new ManageOptionsPage(this);
            DiscardSettings += new DiscardSettingsDelegate(ManageOptionsStore_DiscardSettings);

            DataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "SettingsSwitcher");

            if (!Directory.Exists(DataPath))
            {
                Directory.CreateDirectory(DataPath);
            }

            DataPath = Path.Combine(DataPath, "SettingsSwitcher.config");

            LoadData();
        }

        void ManageOptionsStore_DiscardSettings(object sender, EventArgs e)
        {
            ManageDataSet.ConfiguredSettings.RejectChanges();
        }

        private void LoadData()
        {
            if (File.Exists((DataPath)))
            {
                try
                {
                    using (StreamReader streamReader =
                        new StreamReader(DataPath, Encoding.Default))
                    {
                        ManageDataSet.ReadXml(streamReader);
                        ManageDataSet.AcceptChanges();
                    }
                }
                catch (ConstraintException constraintException)
                {
                    System.Windows.Forms.MessageBox.Show("Fehler beim Laden: " + constraintException);
                }
                catch (XmlException xmlException)
                {
                    System.Windows.Forms.MessageBox.Show("Fehler beim Laden: " + xmlException);
                }
            }
        }

        public void SaveData()
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;
            xmlWriterSettings.IndentChars = "  ";
            xmlWriterSettings.NewLineHandling = NewLineHandling.Replace;
            xmlWriterSettings.NewLineChars = Environment.NewLine;

            using (XmlWriter xmlWriter =
                XmlWriter.Create(new StreamWriter(DataPath, false, Encoding.Default), xmlWriterSettings))
            {
                ManageDataSet.AcceptChanges();
                ManageDataSet.WriteXml(xmlWriter);
            }
        }

        protected override void OnApplySettings(EventArgs eventArgs)
        {
            base.OnApplySettings(eventArgs);

            SaveData();
        }
    }
}
