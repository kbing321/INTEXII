

//namespace INTEXII.Models
//{
//    public class Helpers
//    {
//        public static string GetRDSConnectionString()
//        {
//            var appConfig = System.Configuration.ConfigurationManager.AppSettings;

//            string dbname = appConfig["ebdb"];

//            if (string.IsNullOrEmpty(dbname)) return null;

//            string username = appConfig["INTEXII"];
//            string password = appConfig["INTEXII-Group10"];
//            string hostname = appConfig["awseb-e-2gzt8g7ud7-stack-awsebrdsdatabase-uzhlmyn2oz9f.cd7fte0kduiv.us-east-1.rds.amazonaws.com"];
//            string port = appConfig["5432"];

//            return "Data Source=" + hostname + ";Initial Catalog=" + dbname + ";User ID=" + username + ";Password=" + password + ";";
//        }
//    } "DataSource=awseb-e-2gzt8g7ud7-stack-awsebrdsdatabase-uzhlmyn2oz9f.cd7fte0kduiv.us-east-1.rds.amazonaws.com;InitialCatalog=ebdb;UserID=INTEXII;Password=INTEXII-Group10;"
//}