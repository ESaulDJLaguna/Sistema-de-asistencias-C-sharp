using System.Xml;

namespace Sistema_de_asistencias.Logica
{
    public class Desencriptacion
    {
        static private Encriptacion aes = new Encriptacion();
        // Almacenará la conexión
        static public string CnString;
        // Qué valor se va a desencriptar
        static string dbcnString;
        // El cifrado AES-256 requiere de una llave única que se utiliza para encriptar y desencriptar la información
        static public string appPwdUnique = "5m3ERn5w3q@iTkyd";

        // Es un método que leera el archivo XML y lo desencripta. Pero el archivo YA debe existir en la computadora
        public static object CheckServer()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("ConnectionString.xml");
            XmlElement root = doc.DocumentElement;
            dbcnString = root.Attributes[0].Value;
            CnString = (aes.Decrypt(dbcnString, appPwdUnique, int.Parse("256")));
            return CnString;
        }
    }
}
