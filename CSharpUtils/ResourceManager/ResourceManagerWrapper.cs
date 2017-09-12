//------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2010 , Jirisoft , Ltd. 
//------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace CSharpUtils.ResourceManager
{
    public class ResourceManagerWrapper
    {
        private volatile static ResourceManagerWrapper instance = null;
        private static object locker = new Object();

        public static ResourceManagerWrapper Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new ResourceManagerWrapper();
                        }
                    }
                }
                return instance;
            }
        }

        private ResourceManager resourceManager;

        public ResourceManagerWrapper()
        {
        }

        public void LoadResources(string path)
        {
            resourceManager = ResourceManager.Instance;
            resourceManager.Init(path);
        }

        public string Get(string key)
        {
            return resourceManager.Get("", key);
        }

        public string Get(string language, string key)
        {
            return resourceManager.Get(language, key);
        }
        
        public Hashtable GetLanguages()
        {
            return resourceManager.GetLanguages();
        }

        public Hashtable GetLanguages(string path)
        {
            return resourceManager.GetLanguages(path);
        }

        public void Serialize(string path, string language, string key, string value)
        {
            Resources resources = this.GetResources(path, language);
            resources.Set(key, value);
            string filePath = path + "\\" + language + ".xml";
            resourceManager.Serialize(resources, filePath);
        }

        public Resources GetResources(string path, string language)
        {
            string filePath = path + "\\" + language + ".xml";
            return resourceManager.GetResources(filePath);
        }

        public Resources GetResources(string language)
        {
            return resourceManager.languageResources[language];
        }
    }
}