namespace BlazorDictionary.WebApp.Infrastructure.Constants
{
    public class CategoryConstants
    {
        public const string agenda = "gündem";
        public const string today = "bugün";
        public const string computer = "bilgisayar";
        public const string electric = "elektrik";
        public const string machine = "makine";
        public const string civil = "inşaat";
        public const string mechatronics = "mekatronik";
        public const string food = "gıda";
        public const string industry = "endüstri";
        public const string chemistry = "kimya";
        public const string environmental = "çevre";
        public const string space = "uzay";
        public const string material = "malzeme";
        public const string biomedical = "biyomedikal";
        public const string other = "diğer";

        public static string[] GetAllConstant()
        {
            string[] categoryList = new string[] {
                agenda,today,computer,electric,machine,civil,mechatronics,food,industry,chemistry,environmental,space,material,biomedical,other
            };

            return categoryList;
        }
    }
}
