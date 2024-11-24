using Newtonsoft.Json;

namespace DropZone_BackPanel.Helpers
{
    public class LangGenerate<T>
    {
        private T genericClassObj;
        private readonly string rootpath;

        public LangGenerate(string rootpath)
        {
            this.rootpath = rootpath;
        }

        public T PerseLang(string filename)
        {
            using (StreamReader r = new StreamReader(rootpath + "/wwwroot/Lang/" + filename))
            {
                string json = r.ReadToEnd();
                genericClassObj = JsonConvert.DeserializeObject<T>(json);
            }
            return genericClassObj;
        }

        public T PerseLang(string langEN, string langBN, string Lang)
        {
            string filename = langBN;
            if (Lang == "en") filename = langEN;
            using (StreamReader r = new StreamReader(rootpath + "/wwwroot/Lang/" + filename))
            {
                string json = r.ReadToEnd();
                genericClassObj = JsonConvert.DeserializeObject<T>(json);
            }
            return genericClassObj;
        }
    }
}
